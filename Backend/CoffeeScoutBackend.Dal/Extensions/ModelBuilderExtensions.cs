using System.Text.RegularExpressions;
using CoffeeScoutBackend.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Dal.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigureTablesNaming(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
            entity.SetTableName(ToSnakeCase(entity.GetTableName()));

        return modelBuilder;
    }

    private static string? ToSnakeCase(string? tableName)
    {
        if (tableName == null)
            return null;

        var startUnderscores = Regex.Match(tableName, @"^_+");
        return startUnderscores + Regex.Replace(tableName, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

    public static ModelBuilder ConfigureCustomerEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<CustomerEntity>();

        entity
            .HasMany(c => c.FavoriteMenuItems)
            .WithMany(m => m.CustomersFavoredBy)
            .UsingEntity(j => j.ToTable("customer_favorite_items"));

        return modelBuilder;
    }

    public static ModelBuilder ConfigureCafeAdminEntity(this ModelBuilder modelBuilder)
    {
        return modelBuilder;
    }

    public static ModelBuilder ConfigureCafeEntity(this ModelBuilder modelBuilder)
    {
        return modelBuilder;
    }

    public static ModelBuilder ConfigureMenuItemEntity(this ModelBuilder modelBuilder)
    {
        return modelBuilder;
    }

    public static ModelBuilder ConfigureBeverageTypeEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BeverageTypeEntity>();

        entity
            .HasIndex(c => c.Name)
            .IsUnique();

        return modelBuilder;
    }

    public static ModelBuilder ConfigureOrderEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<OrderEntity>();

        return modelBuilder;
    }
    
    public static ModelBuilder ConfigureOrderItemEntity(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<OrderItemEntity>();
        
        entity
            .HasKey(o => new { o.OrderId, o.MenuItemId });
        
        return modelBuilder;
    }
}