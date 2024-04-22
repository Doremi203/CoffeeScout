using CoffeeScoutBackend.Api.Requests.V1.CoffeeChains;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.CoffeeChains;

public class UpdateCoffeeChainRequestValidator : AbstractValidator<UpdateCoffeeChainRequest>
{
    public UpdateCoffeeChainRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}