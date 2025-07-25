using Microsoft.EntityFrameworkCore;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;
using Order.Delivery.App.Infra.Context;

namespace Order.Delivery.App.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(
            c => c.ProductId == id);
        return product!;
    }

    public async Task RemoveProductAsync(Product product)
    {
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
    }

    public async Task<int> UpdateProductAsync(Product product)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();
        return product.ProductId;
    }
}
