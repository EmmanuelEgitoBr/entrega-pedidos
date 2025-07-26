using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Queries.Orders.GetOrderById;

public class GetOrderByIdQuery : IRequest<ResponseBase<OrderDto>>
{
    public string OrderId { get; set; } = string.Empty;
}
