using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class CustomerFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Customer> Faker = new AutoFaker<Customer>()
        .RuleFor(x => x.Id, f => f.Random.Guid().ToString())
        .RuleFor(x => x.FirstName, f => f.Person.FirstName)
        .RuleFor(x => x.FavoriteMenuItems, _ => []);

    public static Customer[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static Customer WithFavoriteMenuItems(
        this Customer customer,
        MenuItem[] menuItems)
    {
        return customer with { FavoriteMenuItems = menuItems };
    }

    public static Customer WithId(
        this Customer customer,
        string id)
    {
        return customer with { Id = id };
    }

    public static Customer WithFirstName(
        this Customer customer,
        string firstName)
    {
        return customer with { FirstName = firstName };
    }
}