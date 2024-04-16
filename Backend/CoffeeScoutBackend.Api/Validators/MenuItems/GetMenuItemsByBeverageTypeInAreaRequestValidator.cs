using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using CoffeeScoutBackend.Domain.Models;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.MenuItems;

public class GetMenuItemsByBeverageTypeInAreaRequestValidator
    : AbstractValidator<GetMenuItemsByBeverageTypeInAreaRequest>
{
    public GetMenuItemsByBeverageTypeInAreaRequestValidator(
        IValidator<Location> locationValidator
    )
    {
        RuleFor(x => x.Location)
            .SetValidator(locationValidator);
        RuleFor(x => x.RadiusInMeters)
            .GreaterThan(0);
        RuleFor(x => x.BeverageTypeId)
            .GreaterThan(0);
    }
}