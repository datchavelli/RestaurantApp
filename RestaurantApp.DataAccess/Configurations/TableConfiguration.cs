using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DataAccess.Configurations
{
    public class TableConfiguration : EntityConfiguration<Table>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Table> builder)
        {
            builder.HasMany(x => x.Orders)
                   .WithOne(x => x.Table)
                   .HasForeignKey(x => x.TableId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasOne(x => x.Reservation)
                   .WithMany(x => x.Tables)
                   .HasForeignKey(x => x.ReservationId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
