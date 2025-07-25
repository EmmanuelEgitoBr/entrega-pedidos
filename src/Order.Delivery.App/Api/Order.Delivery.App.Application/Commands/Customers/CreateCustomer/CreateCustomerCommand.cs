using MediatR;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Commands.Customers.CreateCustomer;

public class CreateCustomerCommand : IRequest<ResponseBase<string>>
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
