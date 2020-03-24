using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeatReservation.Database.Models
{
    public class AllocationType
    {
        public int AllocationTypeId { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public virtual ICollection<EventSeat> EventSeats { get; set; }
    }
}
