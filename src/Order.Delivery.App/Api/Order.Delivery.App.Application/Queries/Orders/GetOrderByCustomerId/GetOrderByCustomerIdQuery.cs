using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Queries.Orders.GetOrderByCustomerId;

public class GetOrderByCustomerIdQuery : IRequest<ResponseBase<OrderListDto>>
{
    public int CustomerId { get; set; }
}
