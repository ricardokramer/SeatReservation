using Microsoft.EntityFrameworkCore;
using SeatReservation.Database.Interfaces;
using SeatReservation.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeatReservation.Database
{
    public class SeatReservationDataContext : DbContext, ISeatReservationDataContext
    {
        public SeatReservationDataContext()
        {
        }

        public SeatReservationDataContext(DbContextOptions<SeatReservationDataContext> options)
            : base(options)
        {
        }

        void ISeatReservationDataContext.SaveChanges()
        {
            this.SaveChanges();
        }

        public virtual DbSet<AllocationType> AllocationType { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventSeat> EventSeats { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
    }
}
