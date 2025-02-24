﻿using System;
using ModularRestaurant.Menus.Domain.Types;
using ModularRestaurant.Shared.Domain.Common;

namespace ModularRestaurant.Menus.Domain.Entities
{
    public class Item : Entity<ItemId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Item(string name, string description)
        {
            Id = new ItemId(Guid.NewGuid());
            Name = name;
            Description = description;
        }

        private Item()
        {
        }

        internal Item GetCopy()
        {
            return new(Name, Description);
        }

        public static Item Create(string name, string description)
        {
            return new Item(name, description);
        }

        //TODO: Consider, cannot change to same name as exists?
        internal void ChangeName(string newName)
        {
            Name = newName;
        }

        internal void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }
    }
}