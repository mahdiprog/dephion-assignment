using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Messaging.Resources
{
    public class ResourceDto
    {
        public string Name { get; set; }
        public int ResourceId { get; set; }
        public int ResourceTypeId { get; set; }
    }
}
