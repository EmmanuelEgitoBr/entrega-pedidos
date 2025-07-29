using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Resources.Constants;
using Order.Delivery.App.Application.Services.Interfaces;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Commands.Customers.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseBase<int>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPublisherService _publisherService; 

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository,
        IPublisherService publisherService)
    {
        _customerRepository = customerRepository;
        _publisherService = publisherService;
    }

    public async Task<ResponseBase<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
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
                return new ResponseBase<int>
                {
                    Success = false,
                    ErrorMessage = "Não foi possível criar o cliente"
                };
            }

            OrderMessage orderMessage = new()
            {
                Action = ActionConstants.CustomerCreated,
                Email = request.Email,
                Order = null
            };
            await _publisherService.PublishMessageToTopicAsync(orderMessage);

            ResponseBase<int> response = new()
            {
                Success = true,
                Result = result.CustomerId
            };

            return response;
        }
        catch (Exception ex)
        {
            return new ResponseBase<int>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
