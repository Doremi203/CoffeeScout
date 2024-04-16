using CoffeeScoutBackend.Api.Requests.V1.Customers;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Customers;

public class UpdateCustomerInfoRequestValidator : AbstractValidator<UpdateCustomerInfoRequest>
{
    public UpdateCustomerInfoRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(25);
    }
}