using Application.Features.InventoryProductFeatures.Commands;
using Application.Features.InventoryProductFeatures.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Security.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resources;
using WebAPI.Controllers;

namespace WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class InventoryProductsController : BaseApiController
    {
        public InventoryProductsController(IMediator mediator) : base(mediator)
        {
        }

        [RoleAuthorize("SUPPLIER")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetInventoryProductsQuery()));
        }

        [RoleAuthorize("SUPPLIER")]
        [HttpPut]
        public async Task<ActionResult> Update(IEnumerable<InventoryProduct> inventoryProducts)
        {
            foreach (InventoryProduct inventoryProduct in inventoryProducts)
            {
                Guid result = await mediator.Send(new UpdateInventoryProductCommand { ProductId = inventoryProduct.ProductId, Quantity = inventoryProduct.Quantity });
                if (result.Equals(Guid.Empty))
                {
                    return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, inventoryProduct.ProductId));
                }
            }

            return NoContent();
        }
    }
}
