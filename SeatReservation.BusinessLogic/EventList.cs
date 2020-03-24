using SeatReservation.BusinessLogic.DTO;
using SeatReservation.BusinessLogic.Interfaces;
using SeatReservation.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatReservation.BusinessLogic
{
    public class EventList : BusinessLogicBase, IEventList
    {
        public EventList() { }
        public EventList(ISeatReservationDataContext seatReservationDataContext)
            : base(seatReservationDataContext) { }

        public virtual List<EventListDTO> List()
        {
            var result = _seatReservationDataContext.Events
                .Join(_seatReservationDataContext.Rooms,
                ev => ev.RoomId,
                room => room.RoomId,
                (ev, room) => new { ev, room })
                .Where(p => p.ev.IsEnrolmentOpen)
                .OrderBy(p => p.ev.DateTimeStart)
                .Select(p => new EventListDTO
                {
                    EventId = p.ev.EventId,
                    Name = p.ev.Name,
                    RoomName = p.room.Name,
                    Address1 = p.room.Address1,
                    Address2 = p.room.Address2,
                    PostalCode = p.room.PostalCode,
                    City = p.room.City,
                    DateStart = p.ev.DateTimeStart
                }).ToList();

            return result;
        }
    }
}
