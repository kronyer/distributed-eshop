using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Basket.EventHandlers
{
    public class ProductPriceChangedIntegrationEventHandler(BasketService service) : IConsumer<ProductPriceChangedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
        {
            var @event = context.Message;
            // Chamada do método para atualizar o preço do produto em todos os carrinhos
            await service.UpdateBasketItemProductPrices(@event.ProductId, @event.Price);
        }
    }
}
