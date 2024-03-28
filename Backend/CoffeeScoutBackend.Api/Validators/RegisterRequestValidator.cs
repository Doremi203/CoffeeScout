using CoffeeScoutBackend.Api.Requests;
using FluentValidation;

namespace CoffeeScoutBackend.Api.Validators;

public class RegisterRequestValidator : AbstractValidator<CustomRegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.Email).NotEmpty().EmailAddress();
        RuleFor(request => request.Password).NotEmpty();
    }
}