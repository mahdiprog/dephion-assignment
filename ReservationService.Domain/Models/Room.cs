using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Domain.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string  Name { get; set; }
        public int Capacity { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public ICollection<SteadyResource> Resources { get; set; }
    }
}
