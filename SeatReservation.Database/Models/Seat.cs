using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeatReservation.Database.Models
{
    public class Seat
    {
        public int SeatId { get; set; }

        public int RoomId { get; set; }

        [StringLength(5)]
        public string Name { get; set; }

        public int SeatRow { get; set; }

        public int SeatColumn { get; set; }
        public virtual ICollection<EventSeat> EventSeats { get; set; }

        public virtual Room Room { get; set; }

    }
}
