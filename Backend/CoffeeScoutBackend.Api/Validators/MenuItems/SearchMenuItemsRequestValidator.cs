using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.MenuItems;

public class SearchMenuItemsRequestValidator : AbstractValidator<SearchMenuItemsRequest>
{
    public SearchMenuItemsRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Limit)
            .GreaterThan(0);
    }
}