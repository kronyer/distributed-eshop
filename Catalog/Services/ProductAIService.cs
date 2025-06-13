using Microsoft.Extensions.AI;

namespace Catalog.Services
{
    public class ProductAIService(IChatClient chatClient)
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
    }
}
