using MediatR;
using System;

namespace ModularRestaurant.Shared.Domain.Common
{
    public abstract class DomainEvent : INotification
    {        
        public DateTime Timestamp { get; set; }
    }
}
