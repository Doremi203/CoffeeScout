using CoffeeScoutBackend.Api.Requests.V1.CoffeeChains;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.CoffeeChains;

public class AddCoffeeChainRequestValidator : AbstractValidator<AddCoffeeChainRequest>
{
    public AddCoffeeChainRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}