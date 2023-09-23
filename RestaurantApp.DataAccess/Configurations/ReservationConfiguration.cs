using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DataAccess.Configurations
{
    public class ReservationConfiguration : EntityConfiguration<Reservation>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.CustomerName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.ReservationDate).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Receptionist)
                   .WithMany(x => x.CreatedReservations)
                   .HasForeignKey(x => x.ReceptionistId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Tables)
                   .WithOne(x => x.Reservation)
                   .HasForeignKey(x => x.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
