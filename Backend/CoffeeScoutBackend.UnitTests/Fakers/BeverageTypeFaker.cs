using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class BeverageTypeFaker
{
    private static readonly object Lock = new();
    
    private static readonly Faker<BeverageType> Faker = new AutoFaker<BeverageType>()
        .RuleFor(x => x.Id, f => f.Random.Int(1))
        .RuleFor(x => x.Name, f => f.Commerce.ProductName());
    
    public static BeverageType[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }
    
    public static BeverageType WithId(
        this BeverageType beverageType,
        int id)
        => beverageType with { Id = id };
    
    public static BeverageType WithName(
        this BeverageType beverageType,
        string name)
        => beverageType with { Name = name };
}