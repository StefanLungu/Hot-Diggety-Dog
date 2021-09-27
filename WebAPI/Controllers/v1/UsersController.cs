using Application.Interfaces;
using Domain.Dtos.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Security.Authorization;
using Security.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Helpers;
using WebApi.Resources;

namespace WebApi.Controllers.v1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _repository;
        private readonly IJwtService _jwtService;

        public UsersController(IUsersRepository usersRepository, IJwtService jwtService)
        {
            _repository = usersRepository;
            _jwtService = jwtService;
        }

        [RoleAuthorize("ADMIN")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [RoleAuthorize("ADMIN,OPERATOR")]
        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<User>>> GetCustomers()
        {
            return Ok(await _repository.GetAllByRoleAsync(Role.CUSTOMER));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            User user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, id));
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterRequest registerRequest)
        {
            if (await _repository.ExistsByEmailAsync(registerRequest.Email) || await _repository.ExistsByUsernameAsync(registerRequest.Username))
            {
                return BadRequest(Messages.DuplicateUsernameOrEmail);
            }

            User user = new()
            {
                Email = registerRequest.Email,
                Username = registerRequest.Username,
                Password = Crypto.SHA256(registerRequest.Password),
                Role = Role.CUSTOMER
            };

            await _repository.CreateAsync(user);
            return CreatedAtAction("GetUserById", new { id = user.Id }, user);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticateRequest authenticateRequest)
        {
            User user = await _repository.GetByUsernameAndPassword(authenticateRequest.Username, authenticateRequest.Password);

            if (user == null)
            {
                return BadRequest(Messages.InvalidCredentials);
            }

            AuthenticateResult authenticateResult = new()
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.ToString(),
                Token = _jwtService.GenerateJwtToken(user)
            };

            return Ok(authenticateResult);
        }

        [RoleAuthorize("ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            User user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, id));
            }

            await _repository.RemoveAsync(user);

            return NoContent();
        }
    }
}