namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class CustomerNotFoundException : UserNotFoundException
{
    public CustomerNotFoundException(string? message) : base(message)
    {
    }

    public CustomerNotFoundException(string? message, string userId, Exception? innerException) : base(message, userId,
        innerException)
    {
    }

    public CustomerNotFoundException(string? message, string userId) : base(message, userId)
    {
    }
}