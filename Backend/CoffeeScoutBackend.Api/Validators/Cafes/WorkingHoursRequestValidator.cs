using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using FluentValidation;
using Mapster;

namespace CoffeeScoutBackend.Api.Validators.Cafes;

public class WorkingHoursRequestValidator : AbstractValidator<WorkingHoursRequest>
{
    public WorkingHoursRequestValidator(IValidator<TimeRequest> timeValidator)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(request => request.DayOfWeek).IsInEnum();
        RuleFor(request => request.OpeningTime)
            .SetValidator(timeValidator);
        RuleFor(request => request.ClosingTime)
            .SetValidator(timeValidator);
        
        RuleFor(request => request.OpeningTime)
            .Must((request, openingTime) =>
            {
                var opening = openingTime.Adapt<TimeOnly>();
                var closing = request.ClosingTime.Adapt<TimeOnly>();
                return opening < closing;
            })
            .WithMessage("Opening time must be before closing time.");
    }
}