using Microsoft.EntityFrameworkCore;
using SeatReservation.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeatReservation.BusinessLogic.Tests.Database
{
    public class InMemorySeatReservationDataContext : SeatReservationDataContext
    {
        public InMemorySeatReservationDataContext(string databaseName)
            : base(new DbContextOptionsBuilder<SeatReservationDataContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options)
        {
        }
    }
}
