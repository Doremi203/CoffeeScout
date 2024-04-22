using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.ServiceModels;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class MenuItemDataFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<CreateOrderData.MenuItemData> Faker = new AutoFaker<CreateOrderData.MenuItemData>()
        .RuleFor(x => x.Id, f => f.Random.Long(1))
        .RuleFor(x => x.Quantity, f => f.Random.Int(1, 10));

    public static CreateOrderData.MenuItemData[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static CreateOrderData.MenuItemData WithId(
        this CreateOrderData.MenuItemData menuItemData,
        long id)
    {
        return menuItemData with { Id = id };
    }

    public static CreateOrderData.MenuItemData WithQuantity(
        this CreateOrderData.MenuItemData menuItemData,
        int quantity)
    {
        return menuItemData with { Quantity = quantity };
    }
}