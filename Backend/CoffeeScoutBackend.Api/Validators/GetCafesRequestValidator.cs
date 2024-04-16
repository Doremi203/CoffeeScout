using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using CoffeeScoutBackend.Domain.Models;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators;

public class GetCafesRequestValidator : AbstractValidator<GetCafesRequest>
{
    public GetCafesRequestValidator(IValidator<Location> locationValidator)
    {
        RuleFor(request => request.Location)
            .SetValidator(locationValidator);
        RuleFor(request => request.RadiusInMeters)
            .InclusiveBetween(0, 5000);
    }
}