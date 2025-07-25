using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<ResponseBase<CustomerDto>>
{
    public int CustomerId { get; set; }
}
