using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SeatReservation.BusinessLogic.DTO
{
    public class SeatAddItemDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "seatId must be greater than 0")]
        public int SeatId { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "personName is too short")]
        public string PersonName { get; set; }
        [Required]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "email is invalid")]
        public string Email { get; set; }
    }
}
