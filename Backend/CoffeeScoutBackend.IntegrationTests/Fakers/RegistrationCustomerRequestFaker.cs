using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Api.Requests.V1.Accounts;

namespace CoffeeScoutBackend.IntegrationTests.Fakers;

public static class RegistrationCustomerRequestFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<RegisterCustomerRequest> Faker = new AutoFaker<RegisterCustomerRequest>()
        .RuleFor(x => x.FirstName, f => f.Person.FirstName)
        .RuleFor(x => x.Email, f => f.Person.Email)
        .RuleFor(x => x.Password, f => "M_dffsdfsfsdgs@!1");

    public static RegisterCustomerRequest[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static RegisterCustomerRequest WithFirstName(
        this RegisterCustomerRequest src,
        string firstName)
    {
        return src with { FirstName = firstName };
    }

    public static RegisterCustomerRequest WithEmail(
        this RegisterCustomerRequest src,
        string email)
    {
        return src with { Email = email };
    }

    public static RegisterCustomerRequest WithPassword(
        this RegisterCustomerRequest src,
        string password)
    {
        return src with { Password = password };
    }
}