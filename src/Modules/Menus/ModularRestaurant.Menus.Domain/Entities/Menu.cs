﻿using ModularRestaurant.Shared.Domain.Common;
using ModularRestaurant.Shared.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using ModularRestaurant.Menus.Domain.Repositories;
using ModularRestaurant.Menus.Domain.Rules.Groups;
using ModularRestaurant.Menus.Domain.Rules.Menus;
using ModularRestaurant.Menus.Domain.Rules.Restaurants;
using ModularRestaurant.Menus.Domain.Types;
using ModularRestaurant.Shared.Domain.Extensions;

namespace ModularRestaurant.Menus.Domain.Entities
{
    public class Menu : AggregateRoot<MenuId>
    {
        public string InternalName { get; private set; }
        
        public RestaurantId RestaurantId { get; private set; }

        public IReadOnlyList<Group> Groups => _groups;
        private List<Group> _groups = new();
        public bool IsActive { get; private set; }
        
        private Menu(RestaurantId restaurantId, string internalName)
        {
            Id = new MenuId(Guid.NewGuid());
            InternalName = internalName;
            RestaurantId = restaurantId;
            IsActive = false;
        }
        
        private Menu()
        {
        }

        public Menu GetCopy(string newInternalName, IMenuRepository menuRepository)
        {
            CheckRule(new InternalNameMustBeUniqueInRestaurantMenusRule(RestaurantId, newInternalName, menuRepository));
            
            var menu = new Menu(RestaurantId, newInternalName)
            {
                _groups = _groups.Select(x => x.GetCopy()).ToList()
            };
            
            return menu;
        }
        
        public void Activate(IMenuRepository menuRepository)
        {
            CheckRule(new CannotActivateActiveMenuRule(IsActive));
            //CheckRule(new RestaurantCannotHaveMoreThanOneActiveMenuRule(RestaurantId, menuRepository));
            CheckRule(new ActiveMenuMustHaveAtLeastOneGroup(_groups));
            
            //TODO: Change that, this is not the best solution, Restaurant should be an aggregate
            var currentActiveMenu = menuRepository.GetActiveMenuInRestaurant(RestaurantId).Result;
            currentActiveMenu?.Deactivate();
            
            _groups.ForEach(x => x.CheckConsistency());
            
            IsActive = true;
        }

        public void Deactivate()
        {
            CheckRule(new CannotDeactivateInactiveMenuRule(IsActive));
            IsActive = false;
        }

        public static Menu Create(RestaurantId restaurantId, string internalName, IMenuRepository menuRepository)
        {
            CheckRule(new InternalNameMustBeUniqueInRestaurantMenusRule(restaurantId, internalName, menuRepository));
            return new Menu(restaurantId, internalName);
        }

        public void ChangeInternalName(string newInternalName, IMenuRepository menuRepository)
        {
            CheckRule(new InternalNameMustBeUniqueInRestaurantMenusRule(RestaurantId, newInternalName, menuRepository));
            InternalName = newInternalName;
        }
        
        //TODO: consider use full group object
        public void AddGroup(string groupName)
        {
            CheckRule(new CannotChangeActiveMenuRule(IsActive));
            CheckRule(new GroupNameMustBeUniqueRule(_groups, groupName));

            _groups.Add(Group.Create(groupName));
        }

        public void ChangeGroupName(GroupId groupId, string newGroupName)
        {
            CheckRule(new CannotChangeActiveMenuRule(IsActive));
            CheckRule(new GroupNameMustBeUniqueRule(_groups, newGroupName));

            var group = _groups.FindOrThrow(groupId);
            group.ChangeName(newGroupName);
        }

        public void AddItemToGroup(GroupId groupId, string itemName, string itemDescription)
        {
            CheckRule(new CannotChangeActiveMenuRule(IsActive));

            var group = _groups.FindOrThrow(groupId);
            group.AddItem(itemName, itemDescription);
        }

        public void RemoveItemFromGroup(GroupId groupId, ItemId itemId)
        {
            CheckRule(new CannotChangeActiveMenuRule(IsActive));

            var group = _groups.FindOrThrow(groupId);
            group.RemoveItem(itemId);
        }

        public void ChangeItemName(GroupId groupId, ItemId itemId, string newItemName)
        {
            CheckRule(new CannotChangeActiveMenuRule(IsActive));

            var group = _groups.FindOrThrow(groupId);
            group.ChangeItemName(itemId, newItemName);
        }

        public void ChangeItemDescription(GroupId groupId, ItemId itemId, string newItemDescription)
        {
            CheckRule(new CannotChangeActiveMenuRule(IsActive));

            var group = _groups.FindOrThrow(groupId);
            group.ChangeItemDescription(itemId, newItemDescription);
        }

        public void RemoveGroup(GroupId groupId)
        {
            CheckRule(new CannotChangeActiveMenuRule(IsActive));

            var group = _groups.FindOrThrow(groupId);
            _groups.Remove(group);
        }
    }
}