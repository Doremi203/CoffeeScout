using CoffeeScoutBackend.Api;

namespace CoffeeScoutBackend.IntegrationTests;

[CollectionDefinition(nameof(ApiIntegrationTestCollection))]
public class ApiIntegrationTestCollection : ICollectionFixture<MyCustomWebApplicationFactory<StartupFake>>;