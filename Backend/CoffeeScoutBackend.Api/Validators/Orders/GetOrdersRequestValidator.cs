using CoffeeScoutBackend.Api.Requests.V1.Orders;
using CoffeeScoutBackend.Domain.ServiceModels;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Orders;

public class GetOrdersRequestValidator : AbstractValidator<GetOrdersRequest>
{
    public GetOrdersRequestValidator(IValidator<PaginationModel> paginationValidator)
    {
        RuleFor(x => x.Status)
            .IsInEnum();
        RuleFor(x => x.Pagination)
            .SetValidator(paginationValidator);
    }
}