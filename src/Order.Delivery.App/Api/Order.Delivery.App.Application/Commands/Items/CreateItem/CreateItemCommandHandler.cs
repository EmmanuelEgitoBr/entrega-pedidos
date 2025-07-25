using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Commands.Items.CreateItem;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ResponseBase<int>>
{
    private readonly IItemRepository _itemRepository;

    public CreateItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ResponseBase<int>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Item
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Count = request.Count
            };
            var result = await _itemRepository.CreateItemAsync(item);

            if (result is null)
            {
                return new ResponseBase<int>
                {
                    Success = false,
                    ErrorMessage = "Não foi possível criar o item"
                };
            }

            ResponseBase<int> response = new()
            {
                Success = true,
                Result = result.ItemId
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
