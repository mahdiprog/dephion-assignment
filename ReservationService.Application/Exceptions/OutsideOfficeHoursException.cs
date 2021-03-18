using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationService.Application.Exceptions
{
    public class OutsideOfficeHoursException:Exception
    {
        public OutsideOfficeHoursException() : base("Selected time is outside office working time range")
        {
        }
    }
}
