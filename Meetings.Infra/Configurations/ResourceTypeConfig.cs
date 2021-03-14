using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Configurations
{
    public class ResourceTypeConfig: IEntityTypeConfiguration<ResourceType>
    {
        public void Configure(EntityTypeBuilder<ResourceType> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.ResourceTypeId);
            builder.Property(e => e.ResourceTypeId).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(50);
          

        }
    }
}
