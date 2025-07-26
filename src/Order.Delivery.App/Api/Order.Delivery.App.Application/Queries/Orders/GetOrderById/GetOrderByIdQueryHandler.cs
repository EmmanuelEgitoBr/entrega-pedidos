using AutoMapper;
using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Queries.Orders.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ResponseBase<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository,
        IItemRepository itemRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ResponseBase<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            
            if (orderEntity == null)
            {
                return new ResponseBase<OrderDto>()
                {
                    Success = false,
                    ErrorMessage = "Pedido não encontrado"
                };
            }

            OrderDto orderDto = new OrderDto();
            orderDto = _mapper.Map<OrderDto>(orderEntity);
            IList<Item> items = await _itemRepository.GetItemsByOrderIdAsync(request.OrderId);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Product = await _productRepository.GetProductByIdAsync(item.ProductId);
                }
                orderDto.Items = items;
            }

            orderDto.Customer = await _customerRepository.GetCustomerByIdAsync(orderEntity.CustomerId);

            return new ResponseBase<OrderDto>()
            {
                Success = true,
                Result = orderDto
            };
        }
        catch (Exception ex)
        {
            return new ResponseBase<OrderDto>()
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
