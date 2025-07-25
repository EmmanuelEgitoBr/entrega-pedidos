using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Queries.Orders.GetOrderByCustomerId;

public class GetOrderByCustomerIdQueryHandler : IRequestHandler<GetOrderByCustomerIdQuery, ResponseBase<IList<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;

    public GetOrderByCustomerIdQueryHandler(IOrderRepository orderRepository,
        IItemRepository itemRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ResponseBase<IList<OrderDto>>> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = await _orderRepository.GetOrderByCustomerIdAsync(request.CustomerId);

            if (orderEntity == null)
            {
                return new ResponseBase<IList<OrderDto>>()
                {
                    Success = false,
                    ErrorMessage = "Pedido não encontrado"
                };
            }


            return new ResponseBase<IList<OrderDto>>()
            {
                Success = true,
                Result = null
            };
        }
        catch (Exception ex)
        {
            return new ResponseBase<IList<OrderDto>>()
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
