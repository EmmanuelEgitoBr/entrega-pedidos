using Microsoft.EntityFrameworkCore;
using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Order.Delivery.App.Domain.Aggregates.Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Product> Products { get; set; }
}
