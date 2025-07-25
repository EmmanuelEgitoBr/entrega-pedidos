using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommand : IRequest<ResponseBase<string>>
{
    public IList<Item>? Items { get; set; }
    public int CustomerId { get; set; }
}
