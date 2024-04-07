﻿// <auto-generated />
using System;
using CoffeeScoutBackend.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeScoutBackend.Dal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("asp_net_users", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.BeverageTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_beverage_types");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_beverage_types_name");

                    b.ToTable("beverage_types", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.CafeAdminEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<long>("CafeId")
                        .HasColumnType("bigint")
                        .HasColumnName("cafe_id");

                    b.HasKey("Id")
                        .HasName("pk_cafe_admins");

                    b.HasIndex("CafeId")
                        .HasDatabaseName("ix_cafe_admins_cafe_id");

                    b.ToTable("cafe_admins", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.CafeEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Point>("Location")
                        .IsRequired()
                        .HasColumnType("geometry")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cafes");

                    b.ToTable("cafes", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.CustomerEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.MenuItemEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BeverageTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("beverage_type_id");

                    b.Property<long>("CafeId")
                        .HasColumnType("bigint")
                        .HasColumnName("cafe_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("SizeInMl")
                        .HasColumnType("integer")
                        .HasColumnName("size_in_ml");

                    b.HasKey("Id")
                        .HasName("pk_menu_items");

                    b.HasIndex("BeverageTypeId")
                        .HasDatabaseName("ix_menu_items_beverage_type_id");

                    b.HasIndex("CafeId")
                        .HasDatabaseName("ix_menu_items_cafe_id");

                    b.ToTable("menu_items", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.OrderEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint")
                        .HasColumnName("status_id");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_orders_customer_id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.OrderItemEntity", b =>
                {
                    b.Property<long>("OrderId")
                        .HasColumnType("bigint")
                        .HasColumnName("order_id");

                    b.Property<long>("MenuItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("menu_item_id");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.Property<decimal>("PricePerItem")
                        .HasColumnType("numeric")
                        .HasColumnName("price_per_item");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("OrderId", "MenuItemId")
                        .HasName("pk_order_items");

                    b.HasIndex("MenuItemId")
                        .HasDatabaseName("ix_order_items_menu_item_id");

                    b.ToTable("order_items", (string)null);
                });

            modelBuilder.Entity("CustomerEntityMenuItemEntity", b =>
                {
                    b.Property<string>("CustomersFavoredById")
                        .HasColumnType("text")
                        .HasColumnName("customers_favored_by_id");

                    b.Property<long>("FavoriteMenuItemsId")
                        .HasColumnType("bigint")
                        .HasColumnName("favorite_menu_items_id");

                    b.HasKey("CustomersFavoredById", "FavoriteMenuItemsId")
                        .HasName("pk_customer_favorite_items");

                    b.HasIndex("FavoriteMenuItemsId")
                        .HasDatabaseName("ix_customer_favorite_items_favorite_menu_items_id");

                    b.ToTable("customer_favorite_items", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("asp_net_roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_role_claims_role_id");

                    b.ToTable("asp_net_role_claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_claims_user_id");

                    b.ToTable("asp_net_user_claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_logins_user_id");

                    b.ToTable("asp_net_user_logins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_user_roles_role_id");

                    b.ToTable("asp_net_user_roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("asp_net_user_tokens", (string)null);
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.CafeAdminEntity", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.CafeEntity", "Cafe")
                        .WithMany("Admins")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cafe_admins_cafes_cafe_id");

                    b.HasOne("CoffeeScoutBackend.Dal.Entities.AppUser", "User")
                        .WithOne("CafeAdmin")
                        .HasForeignKey("CoffeeScoutBackend.Dal.Entities.CafeAdminEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cafe_admins_users_id");

                    b.Navigation("Cafe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.CustomerEntity", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.AppUser", "User")
                        .WithOne("Customer")
                        .HasForeignKey("CoffeeScoutBackend.Dal.Entities.CustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customers_users_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.MenuItemEntity", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.BeverageTypeEntity", "BeverageType")
                        .WithMany("MenuItems")
                        .HasForeignKey("BeverageTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_menu_items_beverage_types_beverage_type_id");

                    b.HasOne("CoffeeScoutBackend.Dal.Entities.CafeEntity", "Cafe")
                        .WithMany("MenuItems")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_menu_items_cafes_cafe_id");

                    b.Navigation("BeverageType");

                    b.Navigation("Cafe");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.OrderEntity", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.CustomerEntity", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_orders_customers_customer_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.OrderItemEntity", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.MenuItemEntity", "MenuItem")
                        .WithMany()
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_items_menu_items_menu_item_id");

                    b.HasOne("CoffeeScoutBackend.Dal.Entities.OrderEntity", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_items_orders_order_id");

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CustomerEntityMenuItemEntity", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.CustomerEntity", null)
                        .WithMany()
                        .HasForeignKey("CustomersFavoredById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customer_favorite_items_customers_customers_favored_by_id");

                    b.HasOne("CoffeeScoutBackend.Dal.Entities.MenuItemEntity", null)
                        .WithMany()
                        .HasForeignKey("FavoriteMenuItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customer_favorite_items_menu_items_favorite_menu_items_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                    b.HasOne("CoffeeScoutBackend.Dal.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CoffeeScoutBackend.Dal.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.AppUser", b =>
                {
                    b.Navigation("CafeAdmin");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.BeverageTypeEntity", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.CafeEntity", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.CustomerEntity", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CoffeeScoutBackend.Dal.Entities.OrderEntity", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
