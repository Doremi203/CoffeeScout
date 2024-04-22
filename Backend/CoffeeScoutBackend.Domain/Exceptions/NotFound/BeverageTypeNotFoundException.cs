namespace CoffeeScoutBackend.Domain.Exceptions.NotFound;

public class BeverageTypeNotFoundException : NotFoundException
{
    public string? Name { get; init; }
    public long? Id { get; init; }

    public BeverageTypeNotFoundException()
    {
    }

    public BeverageTypeNotFoundException(string? message, string? name)
        : this(message, name, null)
    {
    }

    public BeverageTypeNotFoundException(string? message, long? id)
        : this(message, null, id, null)
    {
    }

    public BeverageTypeNotFoundException(string? message, string? name, long? id)
        : this(message, name, id, null)
    {
    }

    public BeverageTypeNotFoundException(string? message, string? name, long? id, Exception? innerException)
        : base(message, innerException)
    {
        Id = id;
        Name = name;
    }
}