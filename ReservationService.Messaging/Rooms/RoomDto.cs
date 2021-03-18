using System;
using System.Collections.Generic;
using System.Text;
using ReservationService.Messaging.Resources;

namespace ReservationService.Messaging.Rooms
{
    public class RoomDto
    {
        public string Name { get; set; }
        public int RoomId { get; set; }
        public IEnumerable<ResourceDto> Resources { get; set; }
    }
}
