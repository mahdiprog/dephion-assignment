using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Configurations
{
    public class RoomConfig: IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.RoomId);
            builder.Property(e => e.RoomId).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(50);
                       
            builder.HasOne(d => d.Office)
                .WithMany(p => p.Rooms)
                .HasForeignKey(d => d.OfficeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
