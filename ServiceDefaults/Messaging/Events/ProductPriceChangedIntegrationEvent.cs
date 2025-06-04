using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDefaults.Messaging.Events
{
#warning we should create a separated project for messaging events, and also use the domain events
    public record ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
