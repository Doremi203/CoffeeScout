using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Domain.ServiceModels;

namespace CoffeeScoutBackend.UnitTests.Fakers;

public static class CustomerRegistrationDataFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<CustomerRegistrationData> Faker = new AutoFaker<CustomerRegistrationData>()
        .RuleFor(x => x.FirstName, f => f.Person.FirstName)
        .RuleFor(x => x.Email, f => f.Person.Email)
        .RuleFor(x => x.Password, f => f.Internet.Password());

    public static CustomerRegistrationData[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static CustomerRegistrationData WithFirstName(
        this CustomerRegistrationData customerRegistrationData,
        string firstName)
    {
        return customerRegistrationData with { FirstName = firstName };
    }

    public static CustomerRegistrationData WithEmail(
        this CustomerRegistrationData customerRegistrationData,
        string email)
    {
        return customerRegistrationData with { Email = email };
    }

    public static CustomerRegistrationData WithPassword(
        this CustomerRegistrationData customerRegistrationData,
        string password)
    {
        return customerRegistrationData with { Password = password };
    }
}