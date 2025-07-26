using AutoMapper;
using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;
using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Application.Queries.Orders.GetOrderByCustomerId;

public class GetOrderByCustomerIdQueryHandler : IRequestHandler<GetOrderByCustomerIdQuery, ResponseBase<OrderListDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetOrderByCustomerIdQueryHandler(IOrderRepository orderRepository,
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

    public async Task<ResponseBase<OrderListDto>> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var ordersEntity = await _orderRepository.GetOrderByCustomerIdAsync(request.CustomerId);
            
            if (ordersEntity == null)
            {
                return new ResponseBase<OrderListDto> ()
                {
                    Success = false,
                    ErrorMessage = "Pedido não encontrado"
                };
            }

            OrderListDto ordersListDto = new OrderListDto();
            IList<OrderDto> orders = [];

            foreach (var order in ordersEntity)
            {
                var orderDto = await GetOrderDtoAsync(order);
                orders.Add(orderDto);
            }

            ordersListDto.Orders = orders;
            var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);
            ordersListDto.Customer = _mapper.Map<CustomerDto>(customer);

            return new ResponseBase<OrderListDto>()
            {
                Success = true,
                Result = ordersListDto
            };
        }
        catch (Exception ex)
        {
            return new ResponseBase<OrderListDto>()
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    private async Task<OrderDto> GetOrderDtoAsync(Entity.Order orderEntity)
    {
        OrderDto orderDto = new OrderDto();
        orderDto = _mapper.Map<OrderDto>(orderEntity);

        IList<Item> items = await _itemRepository.GetItemsByOrderIdAsync(orderDto.OrderId);

        if (items != null && items.Count > 0)
        {
            foreach (var item in items)
            {
                item.Product = await _productRepository.GetProductByIdAsync(item.ProductId);
            }
            orderDto.Items = items;
        }

        return orderDto;
    }
}
