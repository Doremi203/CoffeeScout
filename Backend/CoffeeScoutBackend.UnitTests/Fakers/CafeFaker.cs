using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class CafeFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Cafe> Faker = new AutoFaker<Cafe>()
        .RuleFor(x => x.Id, f => f.Random.Long(1))
        .RuleFor(x => x.Name, f => f.Company.CompanyName())
        .RuleFor(x => x.Location, _ => LocationFaker.Generate()[0])
        .RuleFor(x => x.Address, f => f.Address.StreetAddress())
        .RuleFor(x => x.CoffeeChain, _ => CoffeeChainFaker.Generate()[0])
        .RuleFor(x => x.WorkingHours, _ => [])
        .RuleFor(x => x.Admins, _ => [])
        .RuleFor(x => x.MenuItems, _ => []);

    public static Cafe[] Generate(int count = 1)
    {
        lock (Lock)
        {
            var cafes = Faker.Generate(count).ToArray();
            for (int i = 0; i < count; i++)
            {
                cafes[i] = cafes[i] with { WorkingHours = SetWorkingHours(cafes[i]) };
            }

            return cafes;
        }
    }

    private static WorkingHours[] SetWorkingHours(Cafe cafe)
    {
        var workingHours = WorkingHoursFaker.GenerateWeek();
        for (var i = 0; i < 7; i++)
        {
            workingHours[i] = workingHours[i] with { Cafe = cafe };
        }

        return workingHours;
    }

    public static Cafe WithAdmins(
        this Cafe cafe,
        CafeAdmin[] admins)
        => cafe with { Admins = admins };
    
    public static Cafe WithWorkingHours(
        this Cafe cafe,
        WorkingHours[] workingHours)
        => cafe with { WorkingHours = workingHours };
    
    public static Cafe WithMenuItems(
        this Cafe cafe,
        MenuItem[] menuItems)
        => cafe with { MenuItems = menuItems };
}