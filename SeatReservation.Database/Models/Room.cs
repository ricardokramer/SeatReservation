using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeatReservation.Database.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(40)]
        public string Address1 { get; set; }

        [StringLength(40)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        [StringLength(7)]
        public string PostalCode { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
