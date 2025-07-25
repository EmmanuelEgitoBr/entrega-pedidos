using Microsoft.EntityFrameworkCore;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;
using Order.Delivery.App.Infra.Context;

namespace Order.Delivery.App.Infra.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _db;
    public ItemRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Item> CreateItemAsync(Item item)
    {
        _db.Items.Add(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task<Item> GetItemByItemIdAsync(int id)
    {
        var item = await _db.Items.AsNoTracking().FirstOrDefaultAsync(
            c => c.ItemId == id);
        return item!;
    }

    public async Task<IList<Item>> GetItemsByOrderIdAsync(int orderId)
    {
        var items = await _db.Items.AsNoTracking().Where(c => c.OrderId == orderId).ToListAsync();
        return items!;
    }

    public async Task RemoveItemAsync(Item item)
    {
        _db.Items.Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task<int> UpdateItemAsync(Item item)
    {
        _db.Items.Update(item);
        await _db.SaveChangesAsync();
        return item.ItemId;
    }
}
