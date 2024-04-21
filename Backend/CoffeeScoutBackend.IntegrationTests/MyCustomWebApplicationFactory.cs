using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoffeeScoutBackend.IntegrationTests;

public class MyCustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{

    public readonly Mock<IBeverageTypeRepository> BeverageTypeRepositoryFake = new(MockBehavior.Strict);
    public readonly Mock<ICafeRepository> CafeRepositoryFake = new(MockBehavior.Strict);
    public readonly Mock<ICoffeeChainRepository> CoffeeChainRepositoryFake = new(MockBehavior.Strict);
    public readonly Mock<ICustomerRepository> CustomerRepositoryFake = new(MockBehavior.Strict);
    public readonly Mock<IMenuItemRepository> MenuItemRepositoryFake = new(MockBehavior.Strict);
    public readonly Mock<IOrderRepository> OrderRepositoryFake = new(MockBehavior.Strict);
    public readonly Mock<IReviewRepository> ReviewRepositoryFake = new(MockBehavior.Strict);

    public MyCustomWebApplicationFactory()
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Replace(new ServiceDescriptor(typeof(IBeverageTypeRepository), BeverageTypeRepositoryFake.Object));
            services.Replace(new ServiceDescriptor(typeof(ICafeRepository), CafeRepositoryFake.Object));
            services.Replace(new ServiceDescriptor(typeof(ICoffeeChainRepository), CoffeeChainRepositoryFake.Object));
            services.Replace(new ServiceDescriptor(typeof(ICustomerRepository), CustomerRepositoryFake.Object));
            services.Replace(new ServiceDescriptor(typeof(IMenuItemRepository), MenuItemRepositoryFake.Object));
            services.Replace(new ServiceDescriptor(typeof(IOrderRepository), OrderRepositoryFake.Object));
            services.Replace(new ServiceDescriptor(typeof(IReviewRepository), ReviewRepositoryFake.Object));
        });
    }
}
