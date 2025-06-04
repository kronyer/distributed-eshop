using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services
{
    public class BasketService(IDistributedCache cache, CatalogApiClient catalogApiClient)
    {
        public async Task<ShoppingCart?> GetBasketAsync(string userName)
        {
            var basket = await cache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task UpdateBasketAsync(ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var product = await catalogApiClient.GetProductById(item.ProductId);
                item.Price = product.Price;
                item.ProductName = product.Name;
            }

           await cache.SetStringAsync(basket.UserName,
                JsonSerializer.Serialize(basket));
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await cache.RemoveAsync(userName);
        }

        internal async Task UpdateBasketItemProductPrices(int productId, decimal price)
        {
            //IDistributedCache not support list of keys function

                //get the basket
                var basket = await GetBasketAsync("teste");

                var item = basket?.Items.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    //update the item price
                    item.Price = price;
                    //update the basket
                    await cache.SetStringAsync("teste",
                        JsonSerializer.Serialize(basket));
                }
        }
    }
}
