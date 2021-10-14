﻿using ModularRestaurant.Menus.Domain.Entities;
using ModularRestaurant.Shared.Domain;
using ModularRestaurant.Shared.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularRestaurant.Menus.Domain.Rules
{
    internal class MenuCannotBeEmptyRule : IBusinessRule
    {
        public string Message => "Menu cannot contain 0 groups";

        private readonly List<Group> _groups;

        internal MenuCannotBeEmptyRule(List<Group> groups)
        {
            _groups = groups;
        }

        public bool IsBroken() => _groups is null || !_groups.Any();
    }
}
