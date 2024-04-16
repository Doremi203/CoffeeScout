using CoffeeScoutBackend.Api.Requests.V1.Accounts;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Accounts;

public class RegisterCustomerRequestValidator : AbstractValidator<RegisterCustomerRequest>
{
    public RegisterCustomerRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .MaximumLength(25);
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(request => request.Password)
            .NotEmpty();
    }
}