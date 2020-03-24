using SeatReservation.BusinessLogic.Interfaces;
using SeatReservation.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SeatReservation.BusinessLogic
{
    public class SeatRemove : BusinessLogicBase, ISeatRemove
    {
        public SeatRemove(ISeatReservationDataContext seatReservationDataContext)
            : base(seatReservationDataContext) { }

        public int Remove(string identifier)
        {
            var item = _seatReservationDataContext.EventSeats
                .Where(p => p.Identifier == identifier)
                .FirstOrDefault();

            if (item == null)
            {
                return 0;
            }

            _seatReservationDataContext.EventSeats.Remove(item);
            _seatReservationDataContext.SaveChanges();

            return item.EventSeatId;
        }
    }
}
