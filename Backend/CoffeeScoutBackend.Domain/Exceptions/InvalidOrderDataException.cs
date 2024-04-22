namespace CoffeeScoutBackend.Domain.Exceptions;

public class InvalidOrderDataException : Exception
{
    public InvalidOrderDataException()
    {
    }

    public InvalidOrderDataException(string? message) : base(message)
    {
    }

    public InvalidOrderDataException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}