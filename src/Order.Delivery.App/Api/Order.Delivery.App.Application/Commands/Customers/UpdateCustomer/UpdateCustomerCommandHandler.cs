using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Resources.Constants;
using Order.Delivery.App.Application.Services.Interfaces;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Commands.Customers.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ResponseBase<int>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPublisherService _publisherService;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository,
        IPublisherService publisherService)
    {
        _customerRepository = customerRepository;
        _publisherService = publisherService;
    }

    public async Task<ResponseBase<int>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Customer customerEntity = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);

            if (customerEntity == null)
            {
                return new ResponseBase<int>()
                {
                    Success = false,
                    ErrorMessage = "Cliente não encontrado"
                };
            }

            customerEntity.Name = request.Name;
            customerEntity.Email = request.Email;
            customerEntity.PhoneNumber = request.PhoneNumber;

            var id = await _customerRepository.UpdateCustomerAsync(customerEntity);
            OrderMessage message = new()
            {
                Action = ActionConstants.UpdateStatusOrder,
                Email = request.Email
            };

            await _publisherService.PublishMessageToTopicAsync(message);

            return new ResponseBase<int>()
            {
                Success = true,
                Result = id
            };
        }
        catch (Exception ex)
        {
            return new ResponseBase<int>()
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
