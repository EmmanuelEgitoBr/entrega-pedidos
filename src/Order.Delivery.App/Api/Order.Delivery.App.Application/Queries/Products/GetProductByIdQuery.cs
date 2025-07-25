using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Queries.Products;

public class GetProductByIdQuery : IRequest<ResponseBase<ProductDto>>
{
    public int ProductId { get; set; }
}
