namespace CoffeeScoutBackend.Bll.Infrastructure;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}