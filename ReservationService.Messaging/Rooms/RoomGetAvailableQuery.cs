using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Messaging.Rooms
{
    public interface RoomGetAvailableQuery
    {
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
