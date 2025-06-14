using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext, IBus bus)
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await dbContext.Products.ToListAsync();
    }
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }
    public async Task AddProductAsync(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
    }
    public async Task UpdateProductAsync(Product product, Product productDto)
    {
#warning use the outbox pattern and make it atomic
        if (productDto.Price != product.Price)
        {
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = product.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ImageUrl = productDto.ImageUrl
            };
            await bus.Publish(integrationEvent);
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.ImageUrl = productDto.ImageUrl;


        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
    }
    public async Task DeleteProductByIdAsync(int id)
    {
        var product = await GetProductByIdAsync(id);
        if (product != null)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteProdutoAsync(Product product)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductAsync(string query)
    {
        return await dbContext.Products
            .Where(product => product.Name.Contains(query))
            .ToListAsync();
    }
}
