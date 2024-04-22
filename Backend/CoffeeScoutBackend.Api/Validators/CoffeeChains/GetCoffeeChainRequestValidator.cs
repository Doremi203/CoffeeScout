using CoffeeScoutBackend.Api.Requests.V1.CoffeeChains;
using CoffeeScoutBackend.Domain.ServiceModels;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.CoffeeChains;

public class GetCoffeeChainRequestValidator : AbstractValidator<GetCoffeeChainsRequest>
{
    public GetCoffeeChainRequestValidator(IValidator<PaginationModel> paginationValidator)
    {
        RuleFor(x => x.Pagination)
            .SetValidator(paginationValidator);
    }
}