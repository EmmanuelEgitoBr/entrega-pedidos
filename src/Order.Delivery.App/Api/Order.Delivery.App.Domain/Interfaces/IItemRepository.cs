using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Domain.Interfaces;

public interface IItemRepository
{
    Task<Item> GetItemByItemIdAsync(int id);
    Task<IList<Item>> GetItemsByOrderIdAsync(int orderId);
    Task<int> CreateItemAsync(Item item);
    Task<int> UpdateItemAsync(Item item);
    Task<int> RemoveItemAsync(int id);
}
