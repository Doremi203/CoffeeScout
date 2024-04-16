using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators;

public class TimeRequestValidator : AbstractValidator<TimeRequest>
{
    public TimeRequestValidator()
    {
        RuleFor(x => x.Hour)
            .InclusiveBetween(0, 23);
        RuleFor(x => x.Minute)
            .InclusiveBetween(0, 59);
    }
}