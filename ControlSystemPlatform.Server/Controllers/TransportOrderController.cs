using ControlSystemPlatform.BLL.TransportOrderDomain.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystemPlatform.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportOrderController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Get(CreateTransportOrderCommand.CreateRequest request,
            CancellationToken cancellationToken)
        {
            // TODO: Add mediatr logging and validation patterns to ensure the incoming data is valid
            var result = await mediator.Send(new CreateTransportOrderCommand(request), cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}