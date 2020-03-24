using SeatReservation.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeatReservation.BusinessLogic
{
    public abstract class BusinessLogicBase
    {
        protected ISeatReservationDataContext _seatReservationDataContext;

        public BusinessLogicBase()
        {
        }

        public BusinessLogicBase(ISeatReservationDataContext seatReservationDataContext)
        {
            _seatReservationDataContext = seatReservationDataContext;
        }
    }
}
