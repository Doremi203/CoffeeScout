using CoffeeScoutBackend.Dal.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal;

public class AppDbContext(
    DbContextOptions<AppDbContext> options
) : IdentityDbContext<AppUser>(options)
{
    public required DbSet<CustomerEntity> Customers { get; set; }
    public required DbSet<MenuItemEntity> MenuItems { get; set; }
    public required DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CustomerEntity>()
            .HasMany(c => c.FavoriteItems)
            .WithMany(m => m.CustomersFavoredBy)
            .UsingEntity(j => j.ToTable("CustomerFavoriteItems"))
            .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<CustomerEntity>(c => c.UserId);
        modelBuilder.Entity<MenuItemEntity>()
            .HasOne(m => m.CategoryEntity)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(m => m.CategoryId);
        modelBuilder.Entity<CategoryEntity>()
            .HasMany(c => c.MenuItems)
            .WithOne(m => m.CategoryEntity)
            .HasForeignKey(m => m.CategoryId);
    }
}