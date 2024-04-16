using CoffeeScoutBackend.Api.Requests.V1.Beverages;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Beverages;

public class AddBeverageTypeRequestValidator : AbstractValidator<AddBeverageTypeRequest>
{
    public AddBeverageTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(25);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(200);
    }
}