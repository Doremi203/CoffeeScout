using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Dal.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal;

public class AppDbContext(
    DbContextOptions<AppDbContext> options
) : IdentityDbContext<AppUser>(options)
{
    public required DbSet<CustomerEntity> Customers { get; set; }
    public required DbSet<CafeAdminEntity> CafeAdmins { get; set; }
    public required DbSet<OrderEntity> Orders { get; set; }
    public required DbSet<OrderItemEntity> OrderItems { get; set; }
    public required DbSet<MenuItemEntity> MenuItems { get; set; }
    public required DbSet<ReviewEntity> Reviews { get; set; }
    public required DbSet<BeverageTypeEntity> BeverageTypes { get; set; }
    public required DbSet<CafeEntity> Cafes { get; set; }
    public required DbSet<CoffeeChainEntity> CoffeeChains { get; set; }
    public required DbSet<WorkingHoursEntity> WorkingHours { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ConfigureTablesNaming()
            .ConfigureOrderEntity()
            .ConfigureOrderItemEntity()
            .ConfigureCustomerEntity()
            .ConfigureMenuItemEntity()
            .ConfigureBeverageTypeEntity()
            .ConfigureCafeEntity()
            .ConfigureCafeAdminEntity();
    }
}