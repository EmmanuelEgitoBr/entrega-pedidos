using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Enums;

namespace Order.Delivery.App.Application.Commands.Orders.UpdateOrderStatus;

public class UpdateOrderStatusCommand : IRequest<ResponseBase<string>>
{
    public string OrderId { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }
}
