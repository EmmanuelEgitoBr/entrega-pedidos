using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Commands.Customers.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseBase<string>>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ResponseBase<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = new Customer
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };
            var result = await _customerRepository.CreateCustomerAsync(customer);

            if(result is null)
            {
                return new ResponseBase<string>
                {
                    Success = false,
                    ErrorMessage = "Não foi possível criar o cliente"
                };
            }

            ResponseBase<string> response = new()
            {
                Success = true,
                Result = result.CustomerId.ToString()
            };

            return response;
        }
        catch (Exception ex)
        {
            return new ResponseBase<string>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
