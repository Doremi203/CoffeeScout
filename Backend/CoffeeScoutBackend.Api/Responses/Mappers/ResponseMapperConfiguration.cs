using CoffeeScoutBackend.Domain.Models;
using Mapster;

namespace CoffeeScoutBackend.Api.Responses.Mappers;

public static class ResponseMapperConfiguration
{
    public static void Configure()
    {
        TypeAdapterConfig<OrderItem, OrderItemResponse>.NewConfig()
            .Map(dest => dest.Name, 
                src => src.MenuItem.Name)
            .Map(dest => dest.SizeInMl, 
                src => src.MenuItem.SizeInMl)
            .Map(dest => dest.BeverageType, 
                src => src.MenuItem.BeverageType.Adapt<BeverageTypeResponse>());
    }
}