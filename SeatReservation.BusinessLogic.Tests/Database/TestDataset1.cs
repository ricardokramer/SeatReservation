using SeatReservation.Database;
using SeatReservation.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatReservation.BusinessLogic.Tests.Database
{
    public class TestDataset1
    {
        public void Load(SeatReservationDataContext seatReservationDataContext)
        {
            //add 1 room
            var room1 = new Room
            {
                Name = "Room1",
                Address1 = "10 My Office",
                Address2 = "40 Main Road",
                City = "Southampton",
                PostalCode = "SO153DS",
            };

            seatReservationDataContext.Rooms.Add(room1);

            //add seat layout for the room
            int totalRows = 10;
            int TotalColumns = 10;
            for (var row = 0; row < totalRows; row++)
            {
                for (var column = 0; column < TotalColumns; column++)
                {
                    seatReservationDataContext.Seats.Add(new Seat
                    {
                        RoomId = room1.RoomId,
                        Name = $"{(char)(65 + row)}{column + 1}",
                        SeatRow = row + 1,
                        SeatColumn = column + 1
                    });
                }
            }

            //add 2 events
            var eventFebruary = new Event
            {
                RoomId = room1.RoomId,
                IsEnrolmentOpen = false,
                Name = "February Dev Meeting",
                DateTimeStart = new DateTime(2020, 2, 21, 17, 0, 0),
                Description = "Our March 2020 monthly meeting.",
                ContentEnrolment = "",
                ContentCancellation = ""
            };
            seatReservationDataContext.Events.Add(eventFebruary);

            var eventMarch = new Event
            {
                RoomId = room1.RoomId,
                IsEnrolmentOpen = true,
                Name = "March Dev Meeting",
                DateTimeStart = new DateTime(2020, 3, 20, 17, 0, 0),
                Description = "Our March 2020 monthly meeting.",
                ContentEnrolment = "",
                ContentCancellation = ""
            };
            seatReservationDataContext.Events.Add(eventMarch);

            var availableSeatState = new AllocationType { Name = "Available" };
            var bookedSeatState = new AllocationType { Name = "Booked" };
            var reservedSeatState = new AllocationType { Name = "Reserved" };
            var notAvailableSeatState = new AllocationType { Name = "Not Available" };

            seatReservationDataContext.AllocationType.Add(availableSeatState);
            seatReservationDataContext.AllocationType.Add(bookedSeatState);
            seatReservationDataContext.AllocationType.Add(reservedSeatState);
            seatReservationDataContext.AllocationType.Add(notAvailableSeatState);

            //need this here or the inmemorydb will not work properly
            seatReservationDataContext.SaveChanges();

            //make first row for march event reserved for VIPs
            seatReservationDataContext.Seats
                .Where(p => p.RoomId == room1.RoomId && p.SeatRow == 1)
                .ToList()
                .ForEach(p =>
                {
                    seatReservationDataContext.EventSeats.Add(new EventSeat
                    {
                        EventId = eventMarch.EventId,
                        SeatId = p.SeatId,
                        AllocationTypeId = reservedSeatState.AllocationTypeId
                    });
                });

            //C7 seat is unavailable
            var C7SeatId = seatReservationDataContext.Seats
                .Where(p => p.RoomId == room1.RoomId && p.SeatRow == 3 && p.SeatColumn == 7)
                .Select(p => p.SeatId).First();

            seatReservationDataContext.EventSeats.Add(new EventSeat { EventId = eventMarch.EventId, SeatId = C7SeatId, AllocationTypeId = notAvailableSeatState.AllocationTypeId });

            //D4, D5, D7 are booked
            var D4SeatId = seatReservationDataContext.Seats
                .Where(p => p.RoomId == room1.RoomId && p.SeatRow == 4 && p.SeatColumn == 4)
                .Select(p => p.SeatId).First();

            var D5SeatId = seatReservationDataContext.Seats
                .Where(p => p.RoomId == room1.RoomId && p.SeatRow == 4 && p.SeatColumn == 5)
                .Select(p => p.SeatId).First();

            var D6SeatId = seatReservationDataContext.Seats
                .Where(p => p.RoomId == room1.RoomId && p.SeatRow == 4 && p.SeatColumn == 6)
                .Select(p => p.SeatId).First();

            seatReservationDataContext.EventSeats.AddRange(
                new EventSeat
                {
                    EventId = eventMarch.EventId,
                    SeatId = D4SeatId,
                    AllocationTypeId = bookedSeatState.AllocationTypeId,
                    PersonName = "John Smith",
                    Email = "jsmith@gmail.com"
                },
                new EventSeat
                {
                    EventId = eventMarch.EventId,
                    SeatId = D5SeatId,
                    AllocationTypeId = bookedSeatState.AllocationTypeId,
                    PersonName = "James Cook",
                    Email = "jamescook@gmail.com"
                },
                new EventSeat
                {
                    EventId = eventMarch.EventId,
                    SeatId = D6SeatId,
                    AllocationTypeId = bookedSeatState.AllocationTypeId,
                    PersonName = "Michael Taylor",
                    Email = "mtaylor@gmail.com"
                });

            seatReservationDataContext.SaveChanges();
        }
    }
}
