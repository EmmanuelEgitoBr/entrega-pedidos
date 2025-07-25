using Microsoft.EntityFrameworkCore;
using Order.Delivery.App.Domain.Interfaces;
using Order.Delivery.App.Infra.Context;
using Order.Delivery.App.Infra.Repositories;

namespace Order.Delivery.App.Api.Extensions;

public static class WebApiBuilderExtensions
{
    public static void AddSqlConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            options.EnableSensitiveDataLogging();
        });
    }

    public static void AddApplicationConfig(this WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<ICartDetailService, CartDetailService>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
    }
}
