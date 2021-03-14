using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Configurations
{
    public class MovableResourceConfig : IEntityTypeConfiguration<MovableResource>
    {
        public void Configure(EntityTypeBuilder<MovableResource> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasOne(d => d.Office)
                .WithMany(p => p.MovableResources)
                .HasForeignKey(d => d.OfficeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
