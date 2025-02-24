﻿using System;

namespace ModularRestaurant.Shared.Domain.Common
{
    public abstract class TypeId : IEquatable<TypeId>
    {
        public Guid Value { get; private set; }

        protected TypeId(Guid value)
        {
            if (value == Guid.Empty) throw new InvalidOperationException("Id value cannot be empty!");

            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            return obj is TypeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(TypeId other)
        {
            return Value == other?.Value;
        }

        public static bool operator ==(TypeId obj1, TypeId obj2)
        {
            if (Equals(obj1, null))
            {
                if (Equals(obj2, null)) return true;

                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(TypeId x, TypeId y)
        {
            return !(x == y);
        }
    }
}