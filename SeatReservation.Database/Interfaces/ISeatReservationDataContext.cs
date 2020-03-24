using Microsoft.EntityFrameworkCore;
using SeatReservation.Database.Models;

namespace SeatReservation.Database.Interfaces
{
    public interface ISeatReservationDataContext
    {
        DbSet<AllocationType> AllocationType { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<EventSeat> EventSeats { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Seat> Seats { get; set; }
        void SaveChanges();
    }
}