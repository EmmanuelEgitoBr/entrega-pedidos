using AutoMapper;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Customer, CustomerDto>().ReverseMap();
            config.CreateMap<Product, ProductDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
