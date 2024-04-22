using System.Net;
using System.Net.Http.Json;
using CoffeeScoutBackend.Api;
using CoffeeScoutBackend.Api.Responses.V1.Beverages;
using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.IntegrationTests.Fakers;
using CoffeeScoutBackend.IntegrationTests.Infrastructure;
using FluentAssertions;
using Mapster;

namespace CoffeeScoutBackend.IntegrationTests.Tests;

[Collection(nameof(ApiIntegrationTestsCollection))]
public class BeverageTypesControllerTests(
    MyCustomWebApplicationFactory<StartupFake> webApplicationFactory
)
{
    [Fact]
    public async Task AddBeverageType_ValidRequest_Returns201Created()
    {
        // Arrange
        var client = webApplicationFactory.CreateClient();

        var request = AddBeverageTypeRequestFaker.Generate()[0];
        await webApplicationFactory.BeverageTypeRepository
            .Add(request.Adapt<BeverageType>());

        // Act
/*        var login = await client.PostAsJsonAsync("login", new LoginRequest
            {
                Email = "SuperAdmin@gmail.com",
                Password = "AdminSuper!2"
            }
        );
        var loginResponse = await login.Content.ReadAsStringAsync();*/
        //var loginResponse = await login.Content.ReadFromJsonAsync<AccessTokenResponse>();
//        client.DefaultRequestHeaders.Add("Authorization", $"Bearer: {loginResponse!.AccessToken}");
        var response = await client.PostAsJsonAsync(RoutesV1.BeverageTypes, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var content = await response.Content.ReadFromJsonAsync<AddBeverageTypeResponse>();
        content.Should().NotBeNull();
        content!.Id.Should().Be(1);
        content.Name.Should().Be(request.Name);
        content.Description.Should().Be(request.Description);
    }
}