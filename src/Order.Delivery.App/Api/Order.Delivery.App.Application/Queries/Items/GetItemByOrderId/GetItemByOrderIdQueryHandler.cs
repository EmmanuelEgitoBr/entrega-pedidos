using AutoMapper;
using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Queries.Items.GetItemByOrderId;

public class GetItemByOrderIdQueryHandler : IRequestHandler<GetItemByOrderIdQuery, ResponseBase<IList<ItemDto>>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetItemByOrderIdQueryHandler(IItemRepository itemRepository, 
        IProductRepository productRepository, 
        IMapper mapper)
    {
        _itemRepository = itemRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ResponseBase<IList<ItemDto>>> Handle(GetItemByOrderIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var itemEntity = await _itemRepository.GetItemsByOrderIdAsync(request.OrderId!);

            if (itemEntity == null)
            {
                return new ResponseBase<IList<ItemDto>>
                {
                    Success = false,
                    ErrorMessage = "Não foi possível encontrar o item"
                };
            }

            foreach (var item in itemEntity)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                item.Product = product;
            }

            var itemDto = _mapper.Map<IList<ItemDto>>(itemEntity);

            return new ResponseBase<IList<ItemDto>>
            {
                Success = true,
                Result = itemDto
            };
        }
        catch (Exception ex)
        {
            return new ResponseBase<IList<ItemDto>>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
