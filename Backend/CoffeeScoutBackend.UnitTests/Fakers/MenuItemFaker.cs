using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class MenuItemFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<MenuItem> Faker = new AutoFaker<MenuItem>()
        .RuleFor(x => x.Id, f => f.Random.Long(1))
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.BeverageType, f => BeverageTypeFaker.Generate()[0])
        .RuleFor(x => x.Price, f => f.Random.Decimal(1, 100))
        .RuleFor(x => x.Reviews, _ => [])
        .RuleFor(x => x.CustomersFavoredBy, _ => [])
        .RuleFor(x => x.Cafe, _ => null!);

    public static MenuItem[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static MenuItem WithId(
        this MenuItem menuItem,
        long id)
    {
        return menuItem with { Id = id };
    }

    public static MenuItem WithName(
        this MenuItem menuItem,
        string name)
    {
        return menuItem with { Name = name };
    }

    public static MenuItem WithBeverageType(
        this MenuItem menuItem,
        BeverageType beverageType)
    {
        return menuItem with { BeverageType = beverageType };
    }

    public static MenuItem WithPrice(
        this MenuItem menuItem,
        decimal price)
    {
        return menuItem with { Price = price };
    }

    public static MenuItem WithReviews(
        this MenuItem menuItem,
        Review[] reviews)
    {
        return menuItem with { Reviews = reviews };
    }

    public static MenuItem WithCustomersFavoredBy(
        this MenuItem menuItem,
        Customer[] customers)
    {
        return menuItem with { CustomersFavoredBy = customers };
    }

    public static MenuItem WithCafe(
        this MenuItem menuItem,
        Cafe cafe)
    {
        return menuItem with { Cafe = cafe };
    }
}