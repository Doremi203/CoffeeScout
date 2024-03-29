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
            .WithOne(u => u.Customer)
            .HasForeignKey<CustomerEntity>(c => c.UserId)
            .IsRequired();

        entity
            .HasMany(c => c.FavoriteMenuItems)
            .WithMany(m => m.CustomersFavoredBy)
            .UsingEntity(j => j.ToTable("CustomerFavoriteItems"));

        return modelBuilder;
    }
    
    public static ModelBuilder ConfigureCafeAdminEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<CafeAdminEntity>();
        entity
            .HasKey(c => c.UserId);

        entity
            .HasOne(c => c.User)
            .WithOne(u => u.CafeAdmin)
            .HasForeignKey<CafeAdminEntity>(c => c.UserId)
            .IsRequired();

        entity
            .HasOne(c => c.Cafe)
            .WithMany(c => c.Admins)
            .HasForeignKey(c => c.CafeId)
            .IsRequired();

        return modelBuilder;
    }
    
    public static ModelBuilder ConfigureCafeEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<CafeEntity>();

        entity
            .HasMany(c => c.MenuItems)
            .WithOne(m => m.Cafe)
            .HasForeignKey(m => m.CafeId);
        
        entity
            .HasMany(c => c.Admins)
            .WithOne(a => a.Cafe)
            .HasForeignKey(a => a.CafeId);

        return modelBuilder;
    }

    public static ModelBuilder ConfigureMenuItemEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<MenuItemEntity>();
        entity
            .HasOne(m => m.BeverageType)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(m => m.BeverageTypeId)
            .IsRequired();
        
        entity
            .HasOne(m => m.Cafe)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(m => m.CafeId)
            .IsRequired();
        
        return modelBuilder;
    }

    public static ModelBuilder ConfigureBeverageTypeEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BeverageTypeEntity>();
        entity
            .HasMany(c => c.MenuItems)
            .WithOne(m => m.BeverageType)
            .HasForeignKey(m => m.BeverageTypeId);
        entity
            .HasIndex(c => c.Name)
            .IsUnique();

        return modelBuilder;
    }
}