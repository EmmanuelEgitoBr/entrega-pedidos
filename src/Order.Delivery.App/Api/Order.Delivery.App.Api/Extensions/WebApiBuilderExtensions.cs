using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Delivery.App.Application.Commands.Customers.CreateCustomer;
using Order.Delivery.App.Application.Mappings;
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
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
    }

    public static void AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateCustomerCommand).Assembly));
    }

    public static void AddMapperConfiguration(this WebApplicationBuilder builder)
    {
        IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
        builder.Services.AddScoped<IMapper>(_ => mapper);
        builder.Services.AddAutoMapper(typeof(MappingConfig));
    }
}
