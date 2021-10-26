﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularRestaurant.Menus.Domain.Entities;
using ModularRestaurant.Shared.Domain.Types;

namespace ModularRestaurant.Menus.Infrastructure.EF.Configuration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasConversion(id => id.Value, id => new MenuId(id));

            builder.Property(m => m.RestaurantId)
                .HasConversion(rId => rId.Value, rId => new RestaurantId(rId));

            builder.OwnsMany(x => x.Groups, x => x.ToTable("Groups")
                .OwnsMany(x => x.Items, x => x.ToTable("Items")));
        }
    }
}