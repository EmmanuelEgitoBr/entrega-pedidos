using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<int> CreateProductAsync(Product product);
    Task<int> UpdateProductAsync(Product product);
    Task<int> RemoveProductAsync(int id);
}
