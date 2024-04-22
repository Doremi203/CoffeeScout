using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class ReviewFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Review> Faker = new AutoFaker<Review>()
        .RuleFor(x => x.Id, f => f.Random.Long(1))
        .RuleFor(x => x.Customer, _ => null!)
        .RuleFor(x => x.MenuItem, _ => null!)
        .RuleFor(x => x.Rating, f => f.Random.Int(1, 5))
        .RuleFor(x => x.Content, f => f.Lorem.Sentence());

    public static Review[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static Review WithId(
        this Review review,
        long id)
    {
        return review with { Id = id };
    }

    public static Review WithCustomer(
        this Review review,
        Customer customer)
    {
        return review with { Customer = customer };
    }

    public static Review WithMenuItem(
        this Review review,
        MenuItem menuItem)
    {
        return review with { MenuItem = menuItem };
    }

    public static Review WithRating(
        this Review review,
        int rating)
    {
        return review with { Rating = rating };
    }

    public static Review WithComment(
        this Review review,
        string content)
    {
        return review with { Content = content };
    }
}