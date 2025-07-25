using MediatR;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Commands.Products.CreateProduct;

public class CreateProductCommand : IRequest<ResponseBase<int>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
