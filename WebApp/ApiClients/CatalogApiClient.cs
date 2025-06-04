using Catalog.Models;

namespace WebApp.ApiClients;

public class CatalogApiClient(HttpClient httpClient)
{
    public async Task<List<Product>> GetProductsAsync()
    {
        var response = await httpClient.GetFromJsonAsync<List<Product>>("/products");
        if (response == null)
        {
            throw new Exception("Failed to retrieve products from the API.");
        }
        return response;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var response = await httpClient.GetFromJsonAsync<Product>($"/products/{id}");
        if (response == null)
        {
            throw new Exception($"Product with ID {id} not found.");
        }
        return response;
    }
}
