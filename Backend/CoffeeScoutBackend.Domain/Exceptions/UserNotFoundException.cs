namespace CoffeeScoutBackend.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public string CustomerUserId { get; init; }

    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public UserNotFoundException(string? message, string customerUserId) : base(message)
    {
        CustomerUserId = customerUserId;
    }
}