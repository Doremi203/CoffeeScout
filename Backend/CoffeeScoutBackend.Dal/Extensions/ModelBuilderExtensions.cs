using CoffeeScoutBackend.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigureCustomerEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<CustomerEntity>();
        entity
            .HasKey(c => c.UserId);

        entity
            .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<CustomerEntity>(c => c.UserId);

        entity
            .HasMany(c => c.FavoriteItems)
            .WithMany(m => m.CustomersFavoredBy)
            .UsingEntity(j => j.ToTable("CustomerFavoriteItems"));

        return modelBuilder;
    }

    public static ModelBuilder ConfigureMenuItemEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<MenuItemEntity>();
        entity
            .HasOne(m => m.BeverageTypeEntity)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(m => m.CategoryId);

        return modelBuilder;
    }

    public static ModelBuilder ConfigureBeverageTypeEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BeverageTypeEntity>();
        entity
            .HasMany(c => c.MenuItems)
            .WithOne(m => m.BeverageTypeEntity)
            .HasForeignKey(m => m.CategoryId);
        entity
            .HasIndex(c => c.Name)
            .IsUnique();

        return modelBuilder;
    }
}