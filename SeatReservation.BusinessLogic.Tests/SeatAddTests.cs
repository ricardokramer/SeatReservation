using SeatReservation.BusinessLogic.DTO;
using SeatReservation.BusinessLogic.Tests.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SeatReservation.BusinessLogic.Tests
{
    public class SeatAddTests
    {
        public void Create_Seat_ShouldPass()
        {
            //Arrange
            var seatReservationDataContext = new InMemorySeatReservationDataContext("SeatAdd");
            var testDB = new TestDataset1();
            testDB.Load(seatReservationDataContext);
            var testData = GetDataset1();

            //Act
            var seatAdd = new SeatAdd(seatReservationDataContext).Create(2, testData);

            //Assert
            Assert.Single(seatAdd);
            Assert.True(seatAdd[0] > 0);

            var item = seatReservationDataContext.EventSeats
                .Where(p => p.EventSeatId == seatAdd[0])
                .FirstOrDefault();

            Assert.True(item.Identifier.Length > 0);


            seatReservationDataContext.Dispose();
        }

        private List<SeatAddItemDTO> GetDataset1()
        {
            return new List<SeatAddItemDTO>
            {
                new SeatAddItemDTO { SeatId = 16, PersonName = "Jason Bourne", Email = "jasonb@gmail.com" }
            };
        }
    }
}
