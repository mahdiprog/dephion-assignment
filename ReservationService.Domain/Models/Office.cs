using System;
using System.Collections.Generic;

namespace ReservationService.Domain.Models
{
    public class Office
    {
        public int OfficeId { get; set; }
        public string Location { get; set; }
        public TimeSpan WorkingStartTime { get; set; }
        public TimeSpan WorkingEndTime { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<MovableResource> MovableResources { get; set; }
    }
}
