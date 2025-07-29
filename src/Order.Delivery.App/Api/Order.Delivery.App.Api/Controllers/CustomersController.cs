using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Delivery.App.Application.Commands.Customers.CreateCustomer;
using Order.Delivery.App.Application.Commands.Customers.UpdateCustomer;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Queries.Customers.GetCustomerById;

namespace Order.Delivery.App.Api.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ResponseBase<CustomerDto>>> GetCustomerById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { CustomerId = id});

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("create-customer")]
        public async Task<ActionResult<ResponseBase<int>>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update-customer")]
        public async Task<ActionResult<ResponseBase<int>>> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
