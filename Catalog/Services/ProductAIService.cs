using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using System.Threading;

namespace Catalog.Services
{
    public class ProductAIService(
        ProductDbContext dbContext,
        IChatClient chatClient,
        IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
        VectorStoreCollection<int, ProductVector> productVectorCollection
        )
    {
        public async Task<string> SupportAsync(string query)
        {
            var systemPrompt = """
                You are a useful assistant.
                You always reply with a short and funny message.
                If you do not know an ansswer, you say "I don't know, but I can help you with something else!".
                You can only answer questions related to products in the catalog.
                At the end, offer this product: Name = Apple iPhone 14-$999.99.
                Do not store memory of the chat conversation.
                """;

            var chatHistory = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, systemPrompt),
                new ChatMessage(ChatRole.User, query)
            };

            var resultPrompt = await chatClient.GetResponseAsync(chatHistory);
            return resultPrompt.Messages[0].ToString()!;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
        {
            if (!await productVectorCollection.CollectionExistsAsync())
            {
                await InitEmbeddingAsync();
            }

            var queryEmbedding = await embeddingGenerator.GenerateVectorAsync(query);

            var options = new VectorSearchOptions<ProductVector>
            {
                VectorProperty = x => x.Vector,
                IncludeVectors = false // ou true, se você quiser os vetores no resultado
            };

            var results = productVectorCollection.SearchAsync(searchValue: queryEmbedding, top: 5, options: options);

            List<Product> products = new List<Product>();

            await foreach (var result in results)
            {
                products.Add(new Product
                {
                    Id = result.Record.Id,
                    Name = result.Record.Name,
                    Description = result.Record.Description,
                    Price = result.Record.Price,
                    ImageUrl = result.Record.ImageUrl
                });
            }

            return products;
        }

        private async Task InitEmbeddingAsync()
        {
            await productVectorCollection.EnsureCollectionExistsAsync();

            var products = await dbContext.Products.ToListAsync();
            foreach (var product in products)
            {
                var prodInfo = $"[{product.Name}]] is a product that costs [{product.Price:C}] and has the following description: {product.Description}.";

                var productVector = new ProductVector
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Vector = await embeddingGenerator.GenerateVectorAsync(prodInfo)
                };
                await productVectorCollection.UpsertAsync(productVector);
            }
        }
    }
}
