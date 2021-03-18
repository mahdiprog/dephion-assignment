using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Messaging.Offices
{
    public interface OfficeGetByIdQuery
    {
        public int OfficeId { get; set; }
    }
}
