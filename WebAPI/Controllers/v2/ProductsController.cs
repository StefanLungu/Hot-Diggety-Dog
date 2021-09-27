using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Security.Authorization;
using System;
using System.Threading.Tasks;
using WebApi.Resources;
using WebAPI.Controllers;

namespace WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class ProductsController : BaseApiController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetProductsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Product product = await mediator.Send(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, id));
            }

            return Ok(product);
        }

        [RoleAuthorize("ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            if (command == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            return Ok(await mediator.Send(command));
        }

        [RoleAuthorize("ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            Guid productId = await mediator.Send(command);
            if (productId == Guid.Empty)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, id));
            }

            return NoContent();
        }

        [RoleAuthorize("ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            Guid productId = await mediator.Send(new DeleteProductCommand { Id = id });
            if (productId == Guid.Empty)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, id));
            }

            return NoContent();
        }
    }
}
