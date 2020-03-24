using SeatReservation.BusinessLogic.DTO;
using SeatReservation.BusinessLogic.Interfaces;
using SeatReservation.Database.Interfaces;
using SeatReservation.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SeatReservation.BusinessLogic
{
    public class SeatAdd : BusinessLogicBase, ISeatAdd
    {
        public SeatAdd(ISeatReservationDataContext seatReservationDataContext)
           : base(seatReservationDataContext) { }

        public List<int> Create(int eventId, List<SeatAddItemDTO> items)
        {
            var validateEvent = ValidateEvent(eventId);
            if (validateEvent != "")
            {
                throw new ValidationException(validateEvent);
            }

            var validateSeatAllocation = ValidateSeatAllocation(eventId, items);
            if (validateSeatAllocation != "")
            {
                throw new ValidationException(validateSeatAllocation);
            }

            var validatePersonName = ValidatePersonName(eventId, items);
            if (validatePersonName != "")
            {
                throw new ValidationException(validatePersonName);
            }

            var validateEmail = ValidateEmail(eventId, items);
            if (validateEmail != "")
            {
                throw new ValidationException(validateEmail);
            }

            var eventSeats = new List<EventSeat>();

            foreach (var item in items)
            {
                eventSeats.Add(new EventSeat
                {
                    EventId = eventId,
                    SeatId = item.SeatId,
                    AllocationTypeId = (int)AllocationTypes.Booked,
                    PersonName = item.PersonName.Trim(),
                    Email = item.Email.Trim(),
                    Identifier = Guid.NewGuid().ToString().ToUpper().Replace("-", "")
                });

                _seatReservationDataContext.EventSeats.AddRange(eventSeats);
            }

            _seatReservationDataContext.SaveChanges();

            return eventSeats.Select(p => p.EventSeatId).ToList();
        }

        private string ValidateEvent(int eventId)
        {
            var result = _seatReservationDataContext.Events
                .Where(p => p.EventId == eventId)
                .Where(p => p.IsEnrolmentOpen)
                .Any();

            return result ? "" : "Enrolment is not open for this event";
        }

        private string ValidateSeatAllocation(int eventId, List<SeatAddItemDTO> items)
        {
            var seatList = items.Select(s => s.SeatId).ToList();

            var result = _seatReservationDataContext.EventSeats
                .Join(_seatReservationDataContext.Seats,
                ev => ev.SeatId,
                st => st.SeatId,
                (ev, st) => new { ev.EventId, st.SeatId, st.Name })
                .Where(p => p.EventId == eventId)
                .Where(p => seatList.Contains(p.SeatId))
                .Select(p => p.Name)
                .FirstOrDefault();

            return result == null ? "" : $"Seat {result} is already allocated for this event";
        }

        private string ValidatePersonName(int eventId, List<SeatAddItemDTO> items)
        {
            var people = items.Select(s => s.PersonName.ToLower().Trim());

            var result = _seatReservationDataContext.EventSeats
                .Where(p => p.EventId == eventId)
                .Where(p => people.Contains(p.PersonName.ToLower().Trim()))
                .Select(p => p.PersonName)
                .FirstOrDefault();

            return result == null ? "" : $"The person {result} is already registered for this event";
        }

        private string ValidateEmail(int eventId, List<SeatAddItemDTO> items)
        {
            var emails = items.Select(s => s.Email.ToLower().Trim()).ToList();

            var result = _seatReservationDataContext.EventSeats
                .Where(p => p.EventId == eventId)
                .Where(p => emails.Contains(p.Email.ToLower().Trim()))
                .Select(p => p.Email)
                .FirstOrDefault();

            return result == null ? "" : $"The email {result} is already registered for this event";
        }
    }
}
