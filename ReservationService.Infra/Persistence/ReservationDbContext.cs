using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationService.Application.Interfaces;
using ReservationService.Domain.Models;

namespace ReservationService.Infra.Persistence
{
    public class ReservationDbContext:DbContext,IReservationDbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<SteadyResource> SteadyResources { get; set; }
        public DbSet<MovableResource> MovableResources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationDbContext).Assembly);
            // Seed data
            modelBuilder.Seed();
        }
    }
}
