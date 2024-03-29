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
            new Category { Id = 1, Name = "Women", ImageUrl = "https://images.asos-media.com/products/asos-design-leather-look-suit-blazer-in-green/200896864-1-green?$n_1280w$&wid=1125&fit=constrain" },
            new Category { Id = 2, Name = "Men", ImageUrl = "Image 2" },
            new Category { Id = 3, Name = "Watches", ImageUrl = "Image 3" },
            new Category { Id = 4, Name = "Babies", ImageUrl = "Image 4" });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Elegant Lace Dress", 
                Description = @"This elegant women's dress is perfect for any special occasion. It features delicate lace detailing and a flattering silhouette. Made from high-quality materials for comfort and style. With its timeless design and impeccable craftsmanship, this dress will make you stand out from the crowd. Whether you're attending a wedding, cocktail party, or formal dinner, this dress is sure to turn heads. Pair it with heels and statement jewelry for a glamorous look that will make you feel like a million dollars.

Elevate your wardrobe with this stunning lace dress. The intricate lace overlay adds a touch of sophistication, while the classic silhouette ensures a flattering fit. Whether you're dancing the night away at a gala or enjoying a romantic dinner, this dress is the perfect choice for making a statement. Crafted from luxurious fabric with a silky lining, it offers both style and comfort. Add it to your collection and be prepared to dazzle on any occasion.

Make a lasting impression with this timeless lace dress. The elegant design and exquisite detailing make it a standout piece in any wardrobe. Whether you're celebrating a special milestone or attending a formal event, this dress will ensure you look and feel your best. With its versatile style, you can easily dress it up or down to suit any occasion. Complete your look with heels and accessories for a polished ensemble that exudes confidence and grace.", 
                Image = "Image 1", Price = 59.99, Quantity = 50, CategoryId = 1 },
            new Product { Id = 2, Name = "Slim Fit Button-down Shirt", 
                Description = @"This stylish men's shirt is a wardrobe essential. With its slim fit design and classic button-down collar, it's perfect for both casual and formal occasions. Made from premium cotton for all-day comfort. Whether you're heading to the office, going out for drinks with friends, or enjoying a weekend brunch, this shirt will keep you looking sharp and feeling confident. Pair it with chinos or jeans for a polished yet relaxed look that effortlessly transitions from day to night.

Elevate your style with this modern slim fit shirt. The tailored silhouette and sleek design create a polished look that's perfect for any occasion. Crafted from premium cotton fabric, it offers a comfortable fit and exceptional durability. Whether you're dressing for work or play, this shirt is sure to impress. Add it to your wardrobe and enjoy effortless style wherever you go.

Stay on-trend with this versatile button-down shirt. The slim fit silhouette and timeless design make it a must-have for every man's closet. Whether you're dressing for a business meeting or a night out on the town, this shirt will ensure you look your best. Made from high-quality materials, it offers both style and comfort. Pair it with trousers for a sophisticated look or wear it with jeans for a more casual vibe.", 
                Image = "Image 2", Price = 39.99, Quantity = 40, CategoryId = 2 });

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CreateAt = DateTimeOffset.UtcNow, Amount = 399.9 },
            new Order { Id = 2, CreateAt = DateTimeOffset.UtcNow.AddDays(-1), Amount = 119.98 });

        modelBuilder.Entity<OrderProduct>().HasData(
            new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 5 },
            new OrderProduct { OrderId = 1, ProductId = 2, Quantity = 10 },
            new OrderProduct { OrderId = 2, ProductId = 1, Quantity = 2 });

        base.OnModelCreating(modelBuilder);

    }

}
