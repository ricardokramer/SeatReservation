using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeatReservation.BusinessLogic.DTO
{
    public class SeatAddDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "eventId must be greater than 0")]
        public int EventId { get; set; }
        public List<SeatAddItemDTO> Items { get; set; }
    }
}
