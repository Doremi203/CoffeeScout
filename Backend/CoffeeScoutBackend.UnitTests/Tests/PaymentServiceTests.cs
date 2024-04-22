using CoffeeScoutBackend.Bll.Services;
using CoffeeScoutBackend.Domain.Interfaces.Services;

namespace CoffeeScoutBackend.UnitTests.Tests;

public class PaymentServiceTests
{
    private readonly IPaymentService _paymentService;

    public PaymentServiceTests()
    {
        _paymentService = new PaymentService();
    }

    [Fact]
    public async Task ProcessPayment_Valid_Success()
    {
        // Arrange
        var userId = "1";
        var amount = 10.0m;

        // Act
        await _paymentService.ProcessPayment(userId, amount);

        // Assert
        // No exception should be thrown
    }
}