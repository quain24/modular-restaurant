﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModularRestaurant.Menus.Domain.Entities;
using ModularRestaurant.Menus.Domain.Repositories;
using ModularRestaurant.Shared.Domain.Types;
using System.Threading;
using System.Threading.Tasks;
using ModularRestaurant.Shared.Domain.Exceptions;

namespace ModularRestaurant.Menus.Infrastructure.EF.Repositories
{
    internal class MenuRepository : IMenuRepository
    {
        private readonly DbSet<Menu> _menus;

        public MenuRepository(MenusDbContext dbContext)
        {
            _menus = dbContext.Menus;
        }

        public async Task AddAsync(Menu menu, CancellationToken token)
        {
            await _menus.AddAsync(menu, token);
        }

        public Task<Menu> GetActiveMenu()
        {
            // todo depends on implemenation in Menu - check todo there
            return _menus.FirstOrDefaultAsync(x => x.IsActive);
        }

        public async Task<Menu> GetAsync(MenuId menuId, CancellationToken token)
        {
            var menu = await _menus.SingleOrDefaultAsync(x => x.Id == menuId, token);
            if (menu is null) throw new ObjectNotFoundException(typeof(Menu), menuId.Value);
            return menu;
        }
    }
}