using ModularRestaurant.Shared.Domain.Common;
using ModularRestaurant.Shared.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularRestaurant.Ratings.Domain.Events
{
    public class RestaurantCreatedEvent : DomainEvent
    {
        public RestaurantCreatedEvent(RestaurantId id)
        {
            Id = id;
        }

        public RestaurantId Id { get; }
    }
}
