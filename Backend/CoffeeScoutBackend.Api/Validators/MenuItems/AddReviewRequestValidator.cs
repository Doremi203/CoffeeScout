using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.MenuItems;

public class AddReviewRequestValidator : AbstractValidator<AddReviewRequest>
{
    public AddReviewRequestValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);
        RuleFor(x => x.Content)
            .MaximumLength(500);
    }
}