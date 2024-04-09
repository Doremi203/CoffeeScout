namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface IPaymentService
{
    Task ProcessPayment(string userId, decimal amount);
}