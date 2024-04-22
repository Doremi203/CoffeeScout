using CoffeeScoutBackend.Api.Requests.V1.Beverages;
using CoffeeScoutBackend.Domain.ServiceModels;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Beverages;

public class GetBeverageTypeRequestValidator : AbstractValidator<GetBeverageTypesRequest>
{
    public GetBeverageTypeRequestValidator(IValidator<PaginationModel> paginationValidator)
    {
        RuleFor(x => x.Pagination)
            .SetValidator(paginationValidator);
    }
}