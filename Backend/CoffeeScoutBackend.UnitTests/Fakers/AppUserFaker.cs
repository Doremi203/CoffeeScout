using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Dal.Entities;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class AppUserFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<AppUser> Faker = new AutoFaker<AppUser>()
        .RuleFor(x => x.Id, f => f.Random.Guid().ToString())
        .RuleFor(x => x.UserName, f => f.Person.Email)
        .RuleFor(x => x.Email, f => f.Person.Email);

    public static AppUser[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }
}