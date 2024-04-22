using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class CoffeeChainFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<CoffeeChain> Faker = new AutoFaker<CoffeeChain>()
        .RuleFor(x => x.Id, f => f.Random.Long(1))
        .RuleFor(x => x.Name, f => f.Company.CompanyName());

    public static CoffeeChain[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }
}