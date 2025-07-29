using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Resources.Constants;
using Order.Delivery.App.Application.Services.Interfaces;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Commands.Orders.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ResponseBase<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPublisherService _publisherService;
    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IPublisherService publisherService)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _publisherService = publisherService;
    }

    public async Task<ResponseBase<string>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = await _orderRepository.GetOrderByIdAsync(request.OrderId);

            if (orderEntity == null)
            {
                return new ResponseBase<string>()
                {
                    Success = false,
                    ErrorMessage = "Pedido não encontrado"
                };
            }

            orderEntity.OrderSituation = request.OrderStatus.ToString();
            var id = await _orderRepository.UpdateOrderAsync(orderEntity);

            orderEntity.Customer = await _customerRepository.GetCustomerByIdAsync(orderEntity.CustomerId);

            OrderMessage message = new()
            {
                Action = ActionConstants.UpdateStatusOrder,
                Email = orderEntity.Customer.Email,
                Order = orderEntity
            };

            await _publisherService.PublishMessageToTopicAsync(message);

            return new ResponseBase<string>()
            {
                Success = true,
                Result = id
            };
        }
        catch (Exception ex)
        {
            return new ResponseBase<string>()
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
