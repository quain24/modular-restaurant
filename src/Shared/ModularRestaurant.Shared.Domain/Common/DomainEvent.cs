using System;

namespace ModularRestaurant.Shared.Domain.Common
{
    public abstract class DomainEvent
    {
        public DateTime Timestamp { get; set; }
    }
}
