using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Delivery.App.Application.Commands.Orders.CreateOrder;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Api.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<ResponseBase<string>>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
