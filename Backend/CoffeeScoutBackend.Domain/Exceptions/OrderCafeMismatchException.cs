namespace CoffeeScoutBackend.Domain.Exceptions;

public class OrderCafeMismatchException : Exception
{
    public OrderCafeMismatchException()
    {
    }

    public OrderCafeMismatchException(string? message) : base(message)
    {
    }

    public OrderCafeMismatchException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}