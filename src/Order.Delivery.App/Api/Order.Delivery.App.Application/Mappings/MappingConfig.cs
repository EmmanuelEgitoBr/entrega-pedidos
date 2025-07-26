using AutoMapper;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Domain.Entities;
using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Customer, CustomerDto>().ReverseMap();
            config.CreateMap<Product, ProductDto>().ReverseMap();
            config.CreateMap<Item, ItemDto>();
            config.CreateMap<Entity.Order, OrderDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
