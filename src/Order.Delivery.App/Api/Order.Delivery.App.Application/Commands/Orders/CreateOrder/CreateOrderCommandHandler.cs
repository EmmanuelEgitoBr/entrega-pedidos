using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Resources.Constants;
using Order.Delivery.App.Application.Services.Interfaces;
using Order.Delivery.App.Application.Utils;
using Order.Delivery.App.Domain.Enums;
using Order.Delivery.App.Domain.Interfaces;
using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseBase<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPublisherService _publisherService;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IPublisherService publisherService)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _publisherService = publisherService;
    }

    public async Task<ResponseBase<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = new Entity.Order
            {
                OrderId = Guid.NewGuid().ToString(),
                OrderCreateDate = DateTime.Now,
                OrderLastUpdate = DateTime.Now,
                OrderSituation = OrderStatus.CREATED.ToString(),
                Items = request.Items,
                CustomerId = request.CustomerId,
                TotalPrice = PriceCalculator.GetTotalPrice(request.Items!)
            };

            var order = await _orderRepository.CreateOrderAsync(orderEntity);
            order.Customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);

            OrderMessage message = new()
            {
                Action = ActionConstants.NewOrderCreated,
                Email = order.Customer.Email,
                Order = order
            };

            await _publisherService.PublishMessageToTopicAsync(message);

            return new ResponseBase<string>
            {
                Success = true,
                Result = order.OrderId
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
