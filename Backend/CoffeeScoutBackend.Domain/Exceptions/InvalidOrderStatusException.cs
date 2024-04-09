using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Exceptions;

public class InvalidOrderStatusException : Exception
{
    public OrderStatus? Status { get; init; }
    public long? Id { get; init; }

    public InvalidOrderStatusException(OrderStatus? status, long? id)
    {
        Status = status;
        Id = id;
    }

    public InvalidOrderStatusException(string? message, OrderStatus? status, long? id)
        : this(message, status, id, null)
    {
    }

    public InvalidOrderStatusException(
        string? message,
        OrderStatus? status,
        long? id,
        Exception? innerException
    ) : base(message, innerException)
    {
        Status = status;
        Id = id;
    }
}