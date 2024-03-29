namespace CoffeeScoutBackend.Domain.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public string? UserId { get; init; }
    
    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, string userId, Exception? innerException) : base(message,
        innerException)
    {
        UserId = userId;
    }

    public UserNotFoundException(string? message, string userId) : base(message)
    {
        UserId = userId;
    }
}