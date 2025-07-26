using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Commands.Orders.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ResponseBase<string>>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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
