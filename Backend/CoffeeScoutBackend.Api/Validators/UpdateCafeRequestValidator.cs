using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using CoffeeScoutBackend.Domain.Models;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators;

public class UpdateCafeRequestValidator : AbstractValidator<UpdateCafeRequest>
{
    public UpdateCafeRequestValidator(
        IValidator<WorkingHoursRequest[]> workingHoursValidator,
        IValidator<Location> locationValidator
    )
    {
        RuleFor(request => request.Name)
            .NotEmpty();
        RuleFor(request => request.Location)
            .SetValidator(locationValidator);
        RuleFor(request => request.Address)
            .NotEmpty();
        RuleFor(request => request.WorkingHours)
            .SetValidator(workingHoursValidator);
    }
}