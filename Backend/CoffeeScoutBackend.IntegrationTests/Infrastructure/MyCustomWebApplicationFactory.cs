using CoffeeScoutBackend.Api.DbSeeders;
using CoffeeScoutBackend.Domain.Interfaces.Repositories;
using CoffeeScoutBackend.IntegrationTests.DbSeeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoffeeScoutBackend.IntegrationTests.Infrastructure;

public class MyCustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    public readonly Mock<IEmailSender> EmailSenderFake = new(MockBehavior.Strict);

    public IBeverageTypeRepository BeverageTypeRepository { get; set; }
    public ICafeRepository CafeRepository { get; set; }
    public ICoffeeChainRepository CoffeeChainRepository { get; set; }
    public ICustomerRepository CustomerRepository { get; set; }
    public IMenuItemRepository MenuItemRepository { get; set; }
    public IOrderRepository OrderRepository { get; set; }
    public IReviewRepository ReviewRepository { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("ApiTests");
        builder.ConfigureServices(services =>
            {
                services.Replace(new ServiceDescriptor(typeof(IEmailSender),
                    EmailSenderFake.Object));

                services.Replace(new ServiceDescriptor(typeof(IDbSeeder),
                    new TestingDbSeeder(services.BuildServiceProvider())));
            })
            .Configure(app =>
            {
                //app.UseMiddleware<TestAuthMiddleware>();
                var scope = app.ApplicationServices.CreateScope();
                var serviceProvider = scope.ServiceProvider;
                BeverageTypeRepository = serviceProvider.GetRequiredService<IBeverageTypeRepository>();
                CafeRepository = serviceProvider.GetRequiredService<ICafeRepository>();
                CoffeeChainRepository = serviceProvider.GetRequiredService<ICoffeeChainRepository>();
                CustomerRepository = serviceProvider.GetRequiredService<ICustomerRepository>();
                MenuItemRepository = serviceProvider.GetRequiredService<IMenuItemRepository>();
                OrderRepository = serviceProvider.GetRequiredService<IOrderRepository>();
                ReviewRepository = serviceProvider.GetRequiredService<IReviewRepository>();
            });
    }
}