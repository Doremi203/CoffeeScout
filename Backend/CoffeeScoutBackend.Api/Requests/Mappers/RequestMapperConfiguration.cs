using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using Mapster;

namespace CoffeeScoutBackend.Api.Requests.Mappers;

public class RequestMapperConfiguration
{
    public static void Configure()
    {
        TypeAdapterConfig<WorkingHoursRequest.TimeRequest, TimeOnly>.NewConfig()
            .MapWith(dest => new TimeOnly(dest.Hour, dest.Minute));
    }
}