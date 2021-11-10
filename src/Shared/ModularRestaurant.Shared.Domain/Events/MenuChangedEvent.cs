using ModularRestaurant.Shared.Domain.Common;
using ModularRestaurant.Shared.Domain.Types;
using MediatR;

namespace ModularRestaurant.Shared.Domain.Events
{
    public class MenuChangedEvent : DomainEvent
    {
        public MenuId Id { get; }
        public MenuChangedEvent(MenuId id)
        {
            Id = id;
        }
    }
}
