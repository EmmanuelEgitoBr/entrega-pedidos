using Microsoft.EntityFrameworkCore;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;
using Order.Delivery.App.Infra.Context;

namespace Order.Delivery.App.Infra.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _db;
    public CustomerRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        var customer = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(
            c => c.CustomerId == id);
        return customer!;
    }

    public async Task RemoveCustomerAsync(Customer customer)
    {
        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
    }

    public async Task<int> UpdateCustomerAsync(Customer customer)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync();
        return customer.CustomerId;
    }
}
