using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class WorkingHoursFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<WorkingHours> Faker = new AutoFaker<WorkingHours>()
        .RuleFor(x => x.DayOfWeek, f => f.PickRandom<DayOfWeek>())
        .RuleFor(x => x.OpeningTime, f => new TimeOnly(f.Random.Int(0, 12), f.Random.Int(0, 59)))
        .RuleFor(x => x.ClosingTime, f => new TimeOnly(f.Random.Int(13, 23), f.Random.Int(0, 59)))
        .RuleFor(x => x.Cafe, _ => null!);

    public static WorkingHours[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static WorkingHours[] GenerateWeek()
    {
        var days = Generate(7);
        for (var i = 0; i < 7; i++)
        {
            days[i] = days[i] with { DayOfWeek = (DayOfWeek)i };
        }

        return days;
    }
}