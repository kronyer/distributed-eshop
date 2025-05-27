namespace Catalog.Data
{
    public static class Extensions
    {
        public static void UseMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            dbContext.Database.Migrate();
            DataSeeder.Seed(dbContext);
        }
    }
}

public class DataSeeder
{
    public static void Seed(ProductDbContext dbContext)
    {
        if (!dbContext.Products.Any())
        {
            dbContext.Products.AddRange(Products);
            dbContext.SaveChanges();
        }
    }

    public static IEnumerable<Product> Products =>
        [
            new Product
            {
                Name = "Apple iPhone 14",
                Description = "Latest model with A15 Bionic chip, 5G capable, and improved camera system.",
                Price = 999.99m,
                ImageUrl = "https://example.com/images/iphone14.jpg"
            },
            new Product
            {
                Name = "Samsung Galaxy S23",
                Description = "High-performance smartphone with AMOLED display and advanced camera features.",
                Price = 899.99m,
                ImageUrl = "https://example.com/images/galaxys23.jpg"
            },
            new Product
            {
                Name = "Google Pixel 7",
                Description = "Smartphone with Google Tensor chip, exceptional camera, and stock Android experience.",
                Price = 799.99m,
                ImageUrl = "https://example.com/images/pixel7.jpg"
            },
            new Product
            {
                Name = "OnePlus 11 Pro",
                Description = "Flagship phone with Snapdragon processor and fast charging.",
                Price = 749.99m,
                ImageUrl = "https://example.com/images/oneplus11pro.jpg"
            },
            new Product
            {
                Name = "Sony WH-1000XM5",
                Description = "Wireless noise-cancelling headphones with premium sound.",
                Price = 399.99m,
                ImageUrl = "https://example.com/images/sonywh1000xm5.jpg"
            },
            new Product
            {
                Name = "Apple MacBook Air M2",
                Description = "Ultra-light laptop with Apple Silicon M2 chip.",
                Price = 1199.99m,
                ImageUrl = "https://example.com/images/macbookairm2.jpg"
            },
            new Product
            {
                Name = "Dell XPS 13",
                Description = "Compact and powerful ultrabook with InfinityEdge display.",
                Price = 1099.99m,
                ImageUrl = "https://example.com/images/dellxps13.jpg"
            },
            new Product
            {
                Name = "Samsung Galaxy Tab S8",
                Description = "High-end Android tablet with S Pen support.",
                Price = 699.99m,
                ImageUrl = "https://example.com/images/galaxytabs8.jpg"
            },
            new Product
            {
                Name = "Amazon Kindle Paperwhite",
                Description = "E-reader with high-resolution display and waterproof design.",
                Price = 149.99m,
                ImageUrl = "https://example.com/images/kindlepaperwhite.jpg"
            },
            new Product
            {
                Name = "Logitech MX Master 3S",
                Description = "Advanced wireless mouse with ergonomic design.",
                Price = 99.99m,
                ImageUrl = "https://example.com/images/mxmaster3s.jpg"
            },
            new Product
            {
                Name = "Apple Watch Series 8",
                Description = "Smartwatch with health tracking and always-on display.",
                Price = 499.99m,
                ImageUrl = "https://example.com/images/applewatch8.jpg"
            },
            new Product
            {
                Name = "Google Nest Hub 2nd Gen",
                Description = "Smart display with Google Assistant and sleep tracking.",
                Price = 129.99m,
                ImageUrl = "https://example.com/images/nesthub2.jpg"
            },
            new Product
            {
                Name = "Bose SoundLink Revolve+ II",
                Description = "Portable Bluetooth speaker with 360° sound.",
                Price = 199.99m,
                ImageUrl = "https://example.com/images/bosesoundlinkrevolve.jpg"
            },
            new Product
            {
                Name = "Canon EOS R10",
                Description = "Mirrorless camera with fast autofocus and 4K video.",
                Price = 979.99m,
                ImageUrl = "https://example.com/images/canoneosr10.jpg"
            },
            new Product
            {
                Name = "Microsoft Surface Pro 9",
                Description = "Versatile 2-in-1 laptop with detachable keyboard.",
                Price = 1299.99m,
                ImageUrl = "https://example.com/images/surfacepro9.jpg"
            }
        ];
}
