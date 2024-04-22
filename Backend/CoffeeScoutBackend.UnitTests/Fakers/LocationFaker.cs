using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class LocationFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Location> Faker = new AutoFaker<Location>()
        .RuleFor(x => x.Latitude, f => f.Address.Latitude())
        .RuleFor(x => x.Longitude, f => f.Address.Longitude());

    public static Location[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }
}