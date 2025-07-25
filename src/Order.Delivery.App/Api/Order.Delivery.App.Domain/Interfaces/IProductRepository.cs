using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task<int> UpdateProductAsync(Product product);
    Task RemoveProductAsync(Product product);
}
