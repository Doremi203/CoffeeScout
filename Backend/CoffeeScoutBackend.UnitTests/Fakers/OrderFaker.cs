using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class OrderFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Order> Faker = new AutoFaker<Order>()
        .RuleFor(x => x.Id, f => f.Random.Long(1))
        .RuleFor(x => x.Customer, _ => CustomerFaker.Generate()[0])
        .RuleFor(x => x.Date, f => f.Date.Past())
        .RuleFor(x => x.Cafe, _ => CafeFaker.Generate()[0])
        .RuleFor(x => x.OrderItems, _ => [])
        .RuleFor(x => x.Status, f => f.PickRandom<OrderStatus>());

    public static Order Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }

    public static Order WithId(
        this Order order,
        long id)
        => order with { Id = id };

    public static Order WithCustomer(
        this Order order,
        Customer customer)
        => order with { Customer = customer };

    public static Order WithDate(
        this Order order,
        DateTime date)
        => order with { Date = date };

    public static Order WithCafe(
        this Order order,
        Cafe cafe)
        => order with { Cafe = cafe };

    public static Order WithOrderItems(
        this Order order,
        OrderItem[] orderItems)
        => order with { OrderItems = orderItems.ToList() };

    public static Order WithStatus(
        this Order order,
        OrderStatus status)
        => order with { Status = status };
}