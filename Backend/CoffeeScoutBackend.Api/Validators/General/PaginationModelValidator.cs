using CoffeeScoutBackend.Domain.ServiceModels;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.General;

public class PaginationModelValidator : AbstractValidator<PaginationModel>
{
    public PaginationModelValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0);
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);
    }
}