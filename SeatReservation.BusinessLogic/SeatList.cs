using SeatReservation.BusinessLogic.DTO;
using SeatReservation.BusinessLogic.Interfaces;
using SeatReservation.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatReservation.BusinessLogic
{
    public class SeatList : BusinessLogicBase, ISeatList
    {
        public SeatList() { }

        public SeatList(ISeatReservationDataContext seatReservationDataContext)
            : base(seatReservationDataContext) { }

        public virtual List<SeatListDTO> List(int eventId)
        {
            //get event information
            var eventRoom = _seatReservationDataContext.Events
                .Where(p => p.EventId == eventId)
                .Where(p => p.IsEnrolmentOpen)
                .Select(p => new { p.RoomId })
                .FirstOrDefault();

            if (eventRoom == null)
            {
                return new List<SeatListDTO>();
            }

            //get label for available
            var AllocationType1 = _seatReservationDataContext.AllocationType
                .Where(p => p.AllocationTypeId == 1).Select(p => p.Name).First();

            //NOTICE: This should have been implemented as an outter join 
            //But I couldn't note make this work in EF (I got really close but gave up)
            //TODO: refactor to use a outter join

            var eventSeats = _seatReservationDataContext.EventSeats
                .Where(p => p.EventId == eventId)
                .Select(p => new
                {
                    p.SeatId,
                    p.AllocationTypeId,
                    AllocationTypeDescription = p.AllocationType.Name
                })
                .ToList();

            var seats = _seatReservationDataContext.Seats
                .Where(p => p.RoomId == eventRoom.RoomId)
                .Select(p => new
                {
                    p.SeatId,
                    p.Name,
                    p.SeatRow,
                    p.SeatColumn
                })
                .ToList();

            var result = new List<SeatListDTO>();

            seats.ForEach(p =>
            {
                var allocation = eventSeats
                    .Where(a => a.SeatId == p.SeatId)
                    .FirstOrDefault();

                result.Add(new SeatListDTO
                {
                    EventId = eventId,
                    SeatId = p.SeatId,
                    Name = p.Name,
                    Row = p.SeatRow,
                    Column = p.SeatColumn,
                    AllocationTypeId = allocation == null ? 1 : allocation.AllocationTypeId,
                    AllocationTypeDescription = allocation == null ? AllocationType1 : allocation.AllocationTypeDescription
                });

            });

            return result;
        }
    }
}
