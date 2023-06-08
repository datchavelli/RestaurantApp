using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DataAccess.Configurations
{
    public class MenuItemConfiguration : EntityConfiguration<MenuItem>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<MenuItem> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);
            
            builder.HasIndex(x => x.Name);

            builder.Property(x => x.Description).HasMaxLength(250);

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.MenuItems)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.OrderItems)
                   .WithOne(x => x.MenuItem)
                   .HasForeignKey(x => x.MenuItemId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
