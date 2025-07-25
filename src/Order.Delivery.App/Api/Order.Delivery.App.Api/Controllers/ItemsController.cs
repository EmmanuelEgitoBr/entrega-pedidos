using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Delivery.App.Application.Commands.Items.CreateItem;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Queries.Items.GetItemByOrderId;

namespace Order.Delivery.App.Api.Controllers
{
    [Route("api/v1/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-by-order/{orderId}")]
        public async Task<ActionResult<ResponseBase<IList<ItemDto>>>> GetItemsByOrderId(string orderId)
        {
            var result = await _mediator.Send(new GetItemByOrderIdQuery { OrderId = orderId });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("create-item")]
        public async Task<ActionResult<ResponseBase<int>>> CreateItem([FromBody] CreateItemCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
