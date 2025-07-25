using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Entity.Order> GetOrderByIdAsync(string id);
    Task<Entity.Order> CreateOrderAsync(Entity.Order order);
    Task<string> UpdateOrderAsync(Entity.Order order);
    Task RemoveOrderAsync(Entity.Order order);
}
