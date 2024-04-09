using CoffeeScoutBackend.Domain.Interfaces.Services;

namespace CoffeeScoutBackend.Bll.Services;

public class PaymentService : IPaymentService
{
    // This is a dummy implementation of the payment service.
    public Task ProcessPayment(string userId, decimal amount)
    {
        return Task.CompletedTask;
    }
}