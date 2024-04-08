using CoffeeScoutBackend.Api.Requests.V1.Accounts;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterCustomerRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.Email).NotEmpty().EmailAddress();
        RuleFor(request => request.Password).NotEmpty();
    }
}