using System;
using System.Collections.Generic;
using System.Text;

namespace SeatReservation.BusinessLogic
{
    public enum AllocationTypes : Int32
    {
        Available = 1,
        Booked = 2,
        Reserved = 3,
        NotAvailable = 4
    }
}
