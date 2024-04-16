using CoffeeScoutBackend.Domain.Models;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Cafes;

public class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90);
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180);
    }
}