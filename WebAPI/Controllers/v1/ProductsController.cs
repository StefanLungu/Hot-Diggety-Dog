using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Security.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resources;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            Product product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, id));
            }

            return Ok(product);
        }

        [RoleAuthorize("ADMIN")]
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            await _repository.CreateAsync(product);
            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        [RoleAuthorize("ADMIN")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            if (!await _repository.ExistsAsync(product.Id))
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, product.Id));
            }

            await _repository.UpdateAsync(product);
            return NoContent();
        }

        [RoleAuthorize("ADMIN")]
        [HttpDelete]
        public async Task<ActionResult> RemoveProduct(Guid id)
        {
            Product product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, id));
            }

            await _repository.RemoveAsync(product);
            return NoContent();
        }
    }
}
