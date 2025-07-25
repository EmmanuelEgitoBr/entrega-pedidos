using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Application.Utils;
using Order.Delivery.App.Domain.Enums;
using Order.Delivery.App.Domain.Interfaces;
using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseBase<string>>
{
    private readonly IOrderRepository _orderRepository;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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
