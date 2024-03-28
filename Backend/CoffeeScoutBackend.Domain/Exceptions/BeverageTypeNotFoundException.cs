namespace CoffeeScoutBackend.Domain.Exceptions;

public class BeverageTypeNotFoundException : NotFoundException
{
    public BeverageTypeNotFoundException()
    {
    }

    public BeverageTypeNotFoundException(string? message, string? name)
        : this(message, name, null)
    {
    }

    public BeverageTypeNotFoundException(string? message, string? name, Exception? innerException)
        : base(message, innerException)
    {
        Name = name;
    }

    public string? Name { get; init; }
}