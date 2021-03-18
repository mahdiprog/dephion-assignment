using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.Messaging.Offices;
using ReservationService.Messaging.Resources;
using ReservationService.Messaging.Rooms;

namespace Meetings.Web.Models
{
    public class MeetingAddViewModel
    {
        public int RoomId { get; set; }
        public int? MovableResourceId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
