using System;
using System.Collections.Generic;
using System.Text;

namespace SeatReservation.BusinessLogic.DTO
{
    public class EventListDTO
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string RoomName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime DateStart { get; set; }
    }
}
