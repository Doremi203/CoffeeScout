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
    public required DbSet<MenuItemEntity> MenuItems { get; set; }
    public required DbSet<BeverageTypeEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .ConfigureCustomerEntity()
            .ConfigureMenuItemEntity()
            .ConfigureCategoryEntity();
    }
}