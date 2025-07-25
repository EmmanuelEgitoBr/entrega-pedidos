namespace Order.Delivery.App.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order.Delivery.App.Domain.Aggregates.Order> GetOrderByIdAsync(int id);
    Task<int> CreateOrderAsync(Order.Delivery.App.Domain.Aggregates.Order order);
    Task<int> UpdateOrderAsync(Order.Delivery.App.Domain.Aggregates.Order order);
    Task<int> RemoveOrderAsync(int id);
}
