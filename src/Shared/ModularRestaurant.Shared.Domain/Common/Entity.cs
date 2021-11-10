using ModularRestaurant.Shared.Domain.Exceptions;
using System.Collections.Generic;

namespace ModularRestaurant.Shared.Domain.Common
{
    public abstract class Entity<T> : Entity
        where T : TypeId
    {
        public T Id { get; protected set; }
    }

    public abstract class Entity
    {
        public ICollection<DomainEvent> Events { get; } = new List<DomainEvent>();

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken()) throw new BusinessRuleException(rule);
        }
    }
}