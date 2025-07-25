using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Queries.Items.GetItemByOrderId;

public class GetItemByOrderIdQuery : IRequest<ResponseBase<IList<ItemDto>>>
{
    public string? OrderId { get; set; }
}
