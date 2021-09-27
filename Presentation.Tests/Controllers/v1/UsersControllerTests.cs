using Domain.Dtos.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Repository.v1;
using Presentation.Tests;
using Security.Services;
using Security.Settings;
using System;
using System.Collections.Generic;
using WebApi.Controllers.v1;
using Xunit;

namespace HotDiggetyDogTests.Controllers.v1
{
    [Collection("Sequential")]
    public class UsersControllerTests : DatabaseBaseTest
    {
        private readonly UsersController _usersController;
        private const string SECRET = "JWT SECRET LONG KEY";

        public UsersControllerTests()
        {
            UsersRepository userRepository = new(dataContext);
            IOptions<SecuritySettings> securitySettings = Options.Create(new SecuritySettings());
            securitySettings.Value.Secret = SECRET;
            JwtService jwtService = new(securitySettings);
            _usersController = new UsersController(userRepository, jwtService);
        }

        [Fact]
        public async void GetUsers_ShouldReturn_OK()
        {
            // Act
            ActionResult<IEnumerable<User>> actionResult = await _usersController.GetUsersAsync();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetCustomers_ShouldReturn_OK()
        {
            // Act
            ActionResult<IEnumerable<User>> actionResult = await _usersController.GetCustomers();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetUserBy_Generated_Id_ShouldReturn_NotFound()
        {
            // Arrange
            Guid id = Guid.Parse("bf6f7c57-2716-4307-9ac7-d73fc7ca6901");

            // Act
            ActionResult<User> actionResult = await _usersController.GetUserById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetUserById_ShouldReturn_Ok()
        {
            // Arrange
            Guid id = Guid.Parse("7144f36f-3b31-4e74-984e-43e549351948");

            // Act
            ActionResult<User> actionResult = await _usersController.GetUserById(id);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void RegisterRequestFor_New_User_ShouldReturn_CreatedAtAction()
        {
            // Arrange
            RegisterRequest registerRequest = new()
            {
                Username = "admin2",
                Email = "admin2@gmail.com",
                Password = "admin2"
            };

            // Act
            ActionResult<User> actionResult = await _usersController.Register(registerRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public async void AuthenticateRequestFor_NonExisting_User_ShouldReturn_BadRequest()
        {
            // Arrange
            AuthenticateRequest authenticateRequest = new()
            {
                Username = "administrator",
                Password = "administrator"
            };

            // Act
            ActionResult<User> actionResult = await _usersController.Authenticate(authenticateRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Delete_NonExisting_User_ShouldReturn_NotFound()
        {
            // Arrange
            Guid id = Guid.Parse("88c9cb66-429e-4498-8f72-f336b35fb94a");

            // Act
            ActionResult<User> actionResult = await _usersController.DeleteUser(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }
    }
}
