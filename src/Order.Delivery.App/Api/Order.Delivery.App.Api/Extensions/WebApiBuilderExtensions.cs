using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Order.Delivery.App.Application.Commands.Customers.CreateCustomer;
using Order.Delivery.App.Application.Mappings;
using Order.Delivery.App.Application.Services;
using Order.Delivery.App.Application.Services.Interfaces;
using Order.Delivery.App.Domain.Interfaces;
using Order.Delivery.App.Infra.Context;
using Order.Delivery.App.Infra.Repositories;
using System.Reflection;

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
        builder.Services.AddScoped<IPublisherService, PublisherService>();
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

    public static void AddSwaggerDoc(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API de gerenciamento de pedidos delivery",
                Description = "API feita com .NET Core 8 usando CQRS e Mediator",
                Contact = new OpenApiContact
                {
                    Name = "Página de contato",
                    Url = new Uri("https://www.google.com")
                },
                License = new OpenApiLicense
                {
                    Name = "Licenciamento",
                    Url = new Uri("https://www.google.com")
                }
            });
            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }
}
