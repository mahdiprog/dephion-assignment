using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReservationService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ReservationService.Application.Interfaces
{
    public interface IReservationDbContext
    {
        DbSet<Office> Offices { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Resource> Resources { get; set; }
        DbSet<SteadyResource> SteadyResources { get; set; }
        DbSet<MovableResource> MovableResources { get; set; }
        DbSet<ResourceType> ResourceTypes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
