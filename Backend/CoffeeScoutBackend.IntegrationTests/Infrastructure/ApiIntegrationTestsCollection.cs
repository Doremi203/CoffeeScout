using CoffeeScoutBackend.Api;

namespace CoffeeScoutBackend.IntegrationTests.Infrastructure;

[CollectionDefinition(nameof(ApiIntegrationTestsCollection))]
public class ApiIntegrationTestsCollection : ICollectionFixture<MyCustomWebApplicationFactory<StartupFake>>;