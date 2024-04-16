using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.MenuItems;

public class UpdateMenuItemRequestValidator : AbstractValidator<UpdateMenuItemRequest>
{
    public UpdateMenuItemRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(25);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.SizeInMl)
            .GreaterThan(0);
        RuleFor(x => x.BeverageTypeId)
            .GreaterThan(0);
    }
}