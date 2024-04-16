using CoffeeScoutBackend.Api.Requests.V1.Accounts;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators.Accounts;

public class RegisterCafeAdminRequestValidator : AbstractValidator<RegisterCafeAdminRequest>
{
    public RegisterCafeAdminRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(request => request.Password)
            .NotEmpty();
        RuleFor(request => request.CafeId)
            .GreaterThan(0);
    }
}