using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class CafeAdminFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<CafeAdmin> Faker = new AutoFaker<CafeAdmin>()
        .RuleFor(x => x.Id, f => f.Random.Guid().ToString())
        .RuleFor(x => x.Cafe, _ => null!);

    public static CafeAdmin[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static CafeAdmin WithCafe(
        this CafeAdmin cafeAdmin,
        Cafe cafe)
    {
        return cafeAdmin with { Cafe = cafe };
    }
}