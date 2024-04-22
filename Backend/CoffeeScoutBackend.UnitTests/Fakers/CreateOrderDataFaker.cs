using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class CreateOrderDataFaker
{
    private static readonly object Lock = new();
    
    private static readonly Faker<CreateOrderData> Faker = new AutoFaker<CreateOrderData>()
        .RuleFor(x => x.CustomerId, f => f.Random.Guid().ToString())
        .RuleFor(x => x.CafeId, f => f.Random.Long(1))
        .RuleFor(x => x.MenuItems, _ => MenuItemDataFaker.Generate(2));

    public static CreateOrderData Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }
    
    public static CreateOrderData WithCustomerId(
        this CreateOrderData createOrderData,
        string customerId)
        => createOrderData with { CustomerId = customerId };
    
    public static CreateOrderData WithCafeId(
        this CreateOrderData createOrderData,
        long cafeId)
        => createOrderData with { CafeId = cafeId };
    
    public static CreateOrderData WithMenuItems(
        this CreateOrderData createOrderData,
        CreateOrderData.MenuItemData[] menuItems)
        => createOrderData with { MenuItems = menuItems };
}