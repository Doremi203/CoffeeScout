using System.Net;
using System.Net.Http.Json;
using CoffeeScoutBackend.Api;
using CoffeeScoutBackend.IntegrationTests.Fakers;
using CoffeeScoutBackend.IntegrationTests.Infrastructure;
using FluentAssertions;

namespace CoffeeScoutBackend.IntegrationTests.Tests;

[Collection(nameof(ApiIntegrationTestsCollection))]
public class AccountsControllerTests(
    MyCustomWebApplicationFactory<StartupFake> webApplicationFactory
)
{
    [Fact]
    public async Task RegisterCustomer_WithValidData_ReturnsOk()
    {
        // Arrange
        var client = webApplicationFactory.CreateClient();
        var request = RegistrationCustomerRequestFaker.Generate()[0];
        request.WithEmail("maxim@gmail.com");

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/accounts/customer/register", request);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RegisterCustomer_WithInvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        var client = webApplicationFactory.CreateClient();
        var request = RegistrationCustomerRequestFaker.Generate()[0];
        request.WithEmail("invalid-email");

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/accounts/customer/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}