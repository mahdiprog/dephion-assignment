using System;
using System.Collections.Generic;
using System.Text;
using ReservationService.Messaging.Resources;
using ReservationService.Messaging.Rooms;

namespace ReservationService.Messaging.Offices
{
    public class OfficeDto
    {
        public int OfficeId { get; set; }
        public string Location { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; }
        public IEnumerable<ResourceDto> MovableResources { get; set; }
    }
}
