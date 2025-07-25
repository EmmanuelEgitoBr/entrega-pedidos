using Microsoft.EntityFrameworkCore;
using Order.Delivery.App.Domain.Interfaces;
using Order.Delivery.App.Infra.Context;
using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Infra.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;
    public OrderRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Entity.Order> CreateOrderAsync(Entity.Order order)
    {
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return order;
    }

    public async Task<Entity.Order> GetOrderByIdAsync(string id)
    {
        var order = await _db.Orders.AsNoTracking().FirstOrDefaultAsync(
            c => c.OrderId == id);
        return order!;
    }

    public async Task<IList<Entity.Order>> GetOrderByCustomerIdAsync(int customerId)
    {
        var order = await _db.Orders.AsNoTracking().Where(o => o.CustomerId == customerId).ToListAsync();
        return order!;
    }


    public async Task RemoveOrderAsync(Entity.Order order)
    {
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
    }

    public async Task<string> UpdateOrderAsync(Entity.Order order)
    {
        _db.Orders.Update(order);
        await _db.SaveChangesAsync();
        return order.OrderId;
    }
}
