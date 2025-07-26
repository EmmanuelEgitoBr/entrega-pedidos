using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Delivery.App.Application.Commands.Orders.CreateOrder;
using Order.Delivery.App.Application.Commands.Orders.UpdateOrderStatus;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Queries.Orders.GetOrderByCustomerId;
using Order.Delivery.App.Application.Queries.Orders.GetOrderById;

namespace Order.Delivery.App.Api.Controllers;

[Route("api/v1/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("order-id/{orderId}")]
    public async Task<ActionResult<ResponseBase<OrderDto>>> GetOrderById(string orderId)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery { OrderId = orderId });

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("customer-id/{customerId:int}")]
    public async Task<ActionResult<ResponseBase<OrderDto>>> GetOrderByCustomerId(int customerId)
    {
        var result = await _mediator.Send(new GetOrderByCustomerIdQuery { CustomerId = customerId });

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("create-order")]
    public async Task<ActionResult<ResponseBase<string>>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-order-status")]
    public async Task<ActionResult<ResponseBase<string>>> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }
}
