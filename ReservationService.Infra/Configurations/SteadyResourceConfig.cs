using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Configurations
{
    public class SteadyResourceConfig : IEntityTypeConfiguration<SteadyResource>
    {
        public void Configure(EntityTypeBuilder<SteadyResource> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasOne(d => d.Room)
                .WithMany(p => p.Resources)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
