// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReservationService.Infra.Persistence;

namespace ReservationService.Infra.Migrations
{
    [DbContext(typeof(ReservationDbContext))]
    partial class ReservationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReservationService.Domain.Models.Office", b =>
                {
                    b.Property<int>("OfficeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<TimeSpan>("WorkingEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("WorkingStartTime")
                        .HasColumnType("time");

                    b.HasKey("OfficeId");

                    b.ToTable("Offices");

                    b.HasData(
                        new
                        {
                            OfficeId = 1,
                            Location = "Amsterdam",
                            WorkingEndTime = new TimeSpan(0, 17, 0, 0, 0),
                            WorkingStartTime = new TimeSpan(0, 8, 30, 0, 0)
                        },
                        new
                        {
                            OfficeId = 2,
                            Location = "Berlin",
                            WorkingEndTime = new TimeSpan(0, 20, 0, 0, 0),
                            WorkingStartTime = new TimeSpan(0, 8, 30, 0, 0)
                        });
                });

            modelBuilder.Entity("ReservationService.Domain.Models.Resource", b =>
                {
                    b.Property<int>("ResourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ResourceTypeId")
                        .HasColumnType("int");

                    b.HasKey("ResourceId");

                    b.HasIndex("ResourceTypeId");

                    b.ToTable("Resources");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Resource");
                });

            modelBuilder.Entity("ReservationService.Domain.Models.ResourceType", b =>
                {
                    b.Property<int>("ResourceTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ResourceTypeId");

                    b.ToTable("ResourceTypes");

                    b.HasData(
                        new
                        {
                            ResourceTypeId = 1,
                            Name = "Presentation Device"
                        },
                        new
                        {
                            ResourceTypeId = 2,
                            Name = "Audio Device"
                        },
                        new
                        {
                            ResourceTypeId = 3,
                            Name = "Board"
                        });
                });

            modelBuilder.Entity("ReservationService.Domain.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            RoomId = 1,
                            Capacity = 10,
                            Name = "Meeting Room 1",
                            OfficeId = 1
                        },
                        new
                        {
                            RoomId = 2,
                            Capacity = 15,
                            Name = "Meeting Room 2",
                            OfficeId = 1
                        },
                        new
                        {
                            RoomId = 3,
                            Capacity = 18,
                            Name = "Meeting Room 1",
                            OfficeId = 2
                        },
                        new
                        {
                            RoomId = 4,
                            Capacity = 20,
                            Name = "Meeting Room 2",
                            OfficeId = 2
                        });
                });

            modelBuilder.Entity("ReservationService.Domain.Models.MovableResource", b =>
                {
                    b.HasBaseType("ReservationService.Domain.Models.Resource");

                    b.Property<int?>("OfficeId")
                        .HasColumnType("int");

                    b.HasIndex("OfficeId");

                    b.HasDiscriminator().HasValue("MovableResource");

                    b.HasData(
                        new
                        {
                            ResourceId = 8,
                            Name = "TV",
                            ResourceTypeId = 1,
                            OfficeId = 1
                        },
                        new
                        {
                            ResourceId = 9,
                            Name = "Beamer",
                            ResourceTypeId = 1,
                            OfficeId = 1
                        },
                        new
                        {
                            ResourceId = 10,
                            Name = "TV 1",
                            ResourceTypeId = 1,
                            OfficeId = 2
                        },
                        new
                        {
                            ResourceId = 11,
                            Name = "TV 2",
                            ResourceTypeId = 1,
                            OfficeId = 2
                        });
                });

            modelBuilder.Entity("ReservationService.Domain.Models.SteadyResource", b =>
                {
                    b.HasBaseType("ReservationService.Domain.Models.Resource");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.HasIndex("RoomId");

                    b.HasDiscriminator().HasValue("SteadyResource");

                    b.HasData(
                        new
                        {
                            ResourceId = 1,
                            Name = "White board",
                            ResourceTypeId = 3,
                            RoomId = 1
                        },
                        new
                        {
                            ResourceId = 2,
                            Name = "White board",
                            ResourceTypeId = 3,
                            RoomId = 2
                        },
                        new
                        {
                            ResourceId = 3,
                            Name = "Beamer",
                            ResourceTypeId = 1,
                            RoomId = 2
                        },
                        new
                        {
                            ResourceId = 4,
                            Name = "White board",
                            ResourceTypeId = 3,
                            RoomId = 3
                        },
                        new
                        {
                            ResourceId = 5,
                            Name = "White board",
                            ResourceTypeId = 3,
                            RoomId = 4
                        },
                        new
                        {
                            ResourceId = 6,
                            Name = "TV",
                            ResourceTypeId = 1,
                            RoomId = 4
                        },
                        new
                        {
                            ResourceId = 7,
                            Name = "Speaker",
                            ResourceTypeId = 2,
                            RoomId = 4
                        });
                });

            modelBuilder.Entity("ReservationService.Domain.Models.Resource", b =>
                {
                    b.HasOne("ReservationService.Domain.Models.ResourceType", "Type")
                        .WithMany()
                        .HasForeignKey("ResourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ReservationService.Domain.Models.Room", b =>
                {
                    b.HasOne("ReservationService.Domain.Models.Office", "Office")
                        .WithMany("Rooms")
                        .HasForeignKey("OfficeId")
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("ReservationService.Domain.Models.MovableResource", b =>
                {
                    b.HasOne("ReservationService.Domain.Models.Office", "Office")
                        .WithMany("MovableResources")
                        .HasForeignKey("OfficeId");

                    b.Navigation("Office");
                });

            modelBuilder.Entity("ReservationService.Domain.Models.SteadyResource", b =>
                {
                    b.HasOne("ReservationService.Domain.Models.Room", "Room")
                        .WithMany("Resources")
                        .HasForeignKey("RoomId");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("ReservationService.Domain.Models.Office", b =>
                {
                    b.Navigation("MovableResources");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("ReservationService.Domain.Models.Room", b =>
                {
                    b.Navigation("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}
