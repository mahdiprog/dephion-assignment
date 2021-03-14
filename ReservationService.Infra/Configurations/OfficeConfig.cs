using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Configurations
{
    public class OfficeConfig: IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.OfficeId);
            builder.Property(e => e.OfficeId).ValueGeneratedOnAdd();
            
            builder.Property(e => e.Location)
                .HasMaxLength(50);

        }
    }
}
