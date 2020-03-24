using System;
using System.Collections.Generic;
using System.Text;

namespace SeatReservation.BusinessLogic.DTO
{
    public class SeatListDTO
    {
        public int EventId { get; set; }
        public int SeatId { get; set; }
        public string Name { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int AllocationTypeId { get; set; }
        public string AllocationTypeDescription { get; set; }
    }
}
