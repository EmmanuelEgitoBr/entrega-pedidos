using MediatR;
using Order.Delivery.App.Application.Models;

namespace Order.Delivery.App.Application.Commands.Items.CreateItem
{
    public class CreateItemCommand : IRequest<ResponseBase<int>>
    {
        public int ItemId { get; set; }
        public string? OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
