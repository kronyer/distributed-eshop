using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDefaults.Messaging.Events;

#warning we should create a separated project for messaging events, and also use the domain events
public record IntegrationEvent
{
    public Guid EventGuid => Guid.NewGuid();
    public DateTime OcurredOn { get; } = DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}
