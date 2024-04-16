using CoffeeScoutBackend.Api.Requests.V1.MenuItems;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.MenuItems;

public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
{
    public UpdateReviewRequestValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);
        RuleFor(x => x.Content)
            .MaximumLength(500);
    }
}