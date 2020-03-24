using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeatReservation.Database.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public int RoomId { get; set; }

        public bool IsEnrolmentOpen { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime DateTimeStart { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public string ContentEnrolment { get; set; }

        public string ContentCancellation { get; set; }

        public virtual Room Room { get; set; }
        public virtual ICollection<EventSeat> EventSeats { get; set; }
    }
}
