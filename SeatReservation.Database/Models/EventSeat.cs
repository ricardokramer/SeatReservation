using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeatReservation.Database.Models
{
    public class EventSeat
    {
        public int EventSeatId { get; set; }

        public int EventId { get; set; }

        public int SeatId { get; set; }

        public int AllocationTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string PersonName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public string Identifier { get; set; }

        public virtual AllocationType AllocationType { get; set; }

        public virtual Event Event { get; set; }

        public virtual Seat Seat { get; set; }
    }
}
