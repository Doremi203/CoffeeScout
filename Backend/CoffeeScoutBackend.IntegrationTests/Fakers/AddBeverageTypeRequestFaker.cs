using AutoBogus;
using Bogus;
using CoffeeScoutBackend.Api.Requests.V1.Beverages;

namespace CoffeeScoutBackend.IntegrationTests.Fakers;

public static class AddBeverageTypeRequestFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<AddBeverageTypeRequest> Faker = new AutoFaker<AddBeverageTypeRequest>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Description, f => f.Lorem.Sentence());

    public static AddBeverageTypeRequest[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Faker.Generate(count).ToArray();
        }
    }

    public static AddBeverageTypeRequest WithName(
        this AddBeverageTypeRequest src,
        string name)
    {
        return src with { Name = name };
    }

    public static AddBeverageTypeRequest WithDescription(
        this AddBeverageTypeRequest src,
        string description)
    {
        return src with { Description = description };
    }
}