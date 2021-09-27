using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator mediator;

        public BaseApiController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
