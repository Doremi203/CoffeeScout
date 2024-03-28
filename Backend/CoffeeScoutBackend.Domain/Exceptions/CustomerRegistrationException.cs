namespace CoffeeScoutBackend.Domain.Exceptions;

public class CustomerRegistrationException(
    string? message,
    IDictionary<string, string[]> registrationErrors
) : Exception(message)
{
    public IDictionary<string, string[]> RegistrationErrors { get; init; } = registrationErrors;
}