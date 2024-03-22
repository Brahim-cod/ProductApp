using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OrderProduct>()
        .HasKey(op => new { op.OrderId, op.ProductId });


        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", Description = "Description 1", Image = "Image 1", Price = 10.99, Quantity = 100, CategoryId = 1 },
            new Product { Id = 2, Name = "Product 2", Description = "Description 2", Image = "Image 1", Price = 20.50, Quantity = 50, CategoryId = 2 });

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CreateAt = DateTimeOffset.UtcNow, Amount = 150.0 },
            new Order { Id = 2, CreateAt = DateTimeOffset.UtcNow.AddDays(-1), Amount = 100.0 });

        modelBuilder.Entity<OrderProduct>().HasData(
            new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 5 },
            new OrderProduct { OrderId = 1, ProductId = 2, Quantity = 10 },
            new OrderProduct { OrderId = 2, ProductId = 1, Quantity = 2 });

        base.OnModelCreating(modelBuilder);

    }

}
