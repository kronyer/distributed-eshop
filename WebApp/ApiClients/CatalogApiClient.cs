﻿using Catalog.Models;

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

    public async Task<string> SupportProducts(string query)
    {
        var response = await httpClient.GetFromJsonAsync<string>($"/products/support/{query}");
        return response!;
    }

    public async Task<List<Product>?> SearchProducts(string query, bool aiSearch = false)
    {
        if (aiSearch)
        {
            return await httpClient.GetFromJsonAsync<List<Product>>($"/products/aisearch/{query}");
        }
        else
        {
            return await httpClient.GetFromJsonAsync<List<Product>>($"/products/search/{query}");
        }
    }

}
