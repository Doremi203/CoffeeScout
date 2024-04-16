using CoffeeScoutBackend.Api.Requests.V1.Cafes;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Cafes;

public class MultipleWorkingHoursRequestValidator : AbstractValidator<WorkingHoursRequest[]>
{
    public MultipleWorkingHoursRequestValidator(IValidator<WorkingHoursRequest> workingHoursValidator)
    {
        var days = new HashSet<DayOfWeek>();
        RuleFor(request => request)
            .Must(workingHours =>
                workingHours.Length == 7 &&
                workingHours
                    .All(wh => days.Add(wh.DayOfWeek)))
            .WithMessage("Working hours must be provided for all days of the week.");

        RuleForEach(request => request)
            .SetValidator(workingHoursValidator);
    }
}