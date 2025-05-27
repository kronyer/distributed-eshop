namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext)
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
}
