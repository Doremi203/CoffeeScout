namespace CoffeeScoutBackend.Domain.Exceptions;

public class RegistrationException(
    string? message,
    IDictionary<string, string[]> registrationErrors
) : Exception(message)
{
    public IDictionary<string, string[]> RegistrationErrors { get; init; } = registrationErrors;
}