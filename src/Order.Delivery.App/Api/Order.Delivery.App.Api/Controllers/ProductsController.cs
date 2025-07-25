using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Delivery.App.Application.Commands.Products.CreateProduct;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Queries.Products;

namespace Order.Delivery.App.Api.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ResponseBase<ProductDto>>> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { ProductId = id});

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<ResponseBase<int>>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
