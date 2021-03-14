using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Configurations
{
    public class ResourceConfig: IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.ResourceId);
            builder.Property(e => e.ResourceId).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(50);


        }
    }
}
