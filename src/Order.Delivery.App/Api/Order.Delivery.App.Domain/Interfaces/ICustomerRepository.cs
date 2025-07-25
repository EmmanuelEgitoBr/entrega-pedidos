using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerByIdAsync(int id);
    Task<int> CreateCustomerAsync(Customer customer);
    Task<int> UpdateCustomerAsync(Customer customer);
    Task<int> RemoveCustomerAsync(int id);
}
