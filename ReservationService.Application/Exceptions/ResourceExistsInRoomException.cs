using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Application.Exceptions
{
    public class ResourceExistsInRoomException:Exception
    {
        public ResourceExistsInRoomException(string message) : base(message)
        {
        }
    }
}
