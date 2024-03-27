namespace CoffeeScoutBackend.Domain.Exceptions;

public class RegistrationException(
    string? message,
    IDictionary<String, String[]> registrationErrors
) : Exception(message)
{
    public IDictionary<string, string[]> RegistrationErrors { get; init; } = registrationErrors;
}