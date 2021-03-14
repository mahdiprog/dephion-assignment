using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Persistence
{
    public static class DatabaseSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Office>().HasData(new List<Office>
            {
                new()
                {
                    OfficeId = 1, Location = "Amsterdam",
                    WorkingStartTime = new TimeSpan(8, 30, 0),
                    WorkingEndTime = new TimeSpan(17, 0, 0)
                },
                new()
                {
                    OfficeId = 2, Location = "Berlin",
                    WorkingStartTime = new TimeSpan(8, 30, 0),
                    WorkingEndTime = new TimeSpan(20, 0, 0)
                },
            });
            modelBuilder.Entity<ResourceType>().HasData(new List<ResourceType>
            {
                new()
                {
                    ResourceTypeId = 1, Name = "Presentation Device"
                },
                new()
                {
                    ResourceTypeId = 2, Name = "Audio Device"
                },
                new()
                {
                    ResourceTypeId = 3, Name = "Board"
                }
            });
            modelBuilder.Entity<Room>().HasData(new List<Room>
            {
                new()
                {
                    RoomId = 1,
                    OfficeId = 1, Capacity = 10, Name = "Meeting Room 1",

                },
                new()
                {
                    RoomId = 2,
                    OfficeId = 1, Capacity = 15, Name = "Meeting Room 2"
                },
                new()
                {
                    RoomId = 3,
                    OfficeId = 2, Capacity = 18, Name = "Meeting Room 1",

                },
                new()
                {
                    RoomId = 4,
                    OfficeId = 2, Capacity = 20, Name = "Meeting Room 2"
                },
            });
            modelBuilder.Entity<SteadyResource>().HasData(new List<SteadyResource>
            {
                new() {RoomId = 1, Name = "White board", ResourceId = 1, ResourceTypeId = 3},
                new() {RoomId = 2, Name = "White board", ResourceId = 2, ResourceTypeId = 3},
                new() {RoomId = 2, Name = "Beamer", ResourceId = 3, ResourceTypeId = 1},
                new() {RoomId = 3, Name = "White board", ResourceId = 4, ResourceTypeId = 3},
                new() {RoomId = 4, Name = "White board", ResourceId = 5, ResourceTypeId = 3},
                new() {RoomId = 4, Name = "TV", ResourceId = 6, ResourceTypeId = 1},
                new() {RoomId = 4, Name = "Speaker", ResourceId = 7, ResourceTypeId = 2}
            });
            modelBuilder.Entity<MovableResource>().HasData(new List<MovableResource>
            {
                new()
                {
                    ResourceTypeId = 1, ResourceId = 8, OfficeId = 1, Name = "TV"
                },
                new()
                {
                    ResourceTypeId = 1, ResourceId = 9, OfficeId = 1, Name = "Beamer"
                },
                new()
                {
                    ResourceTypeId = 1, ResourceId = 10, OfficeId = 2, Name = "TV 1"
                },
                new()
                {
                    ResourceTypeId = 1, ResourceId = 11, OfficeId = 2, Name = "TV 2"
                },
            });
        }
    }
}
