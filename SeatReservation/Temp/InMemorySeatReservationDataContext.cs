using Microsoft.EntityFrameworkCore;
using SeatReservation.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Temp
{
    public class InMemorySeatReservationDataContext : SeatReservationDataContext
    {
        public InMemorySeatReservationDataContext()
            : base(new DbContextOptionsBuilder<SeatReservationDataContext>()
                .UseInMemoryDatabase(databaseName: "memorydb")
                .Options)
                {
                    TestDataset1.Load(this);
                }
    }
}
