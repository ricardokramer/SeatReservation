using SeatReservation.BusinessLogic.Tests.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SeatReservation.BusinessLogic.Tests
{
    public class SeatRemoveTests
    {
        [Fact]
        public void Remove_Seat_ShouldPass()
        {
            //Arrange
            var seatReservationDataContext = new InMemorySeatReservationDataContext("SeatRemove");
            var testDB = new TestDataset1();
            testDB.Load(seatReservationDataContext);
            var testData = "101010";

            //Act
            var seatAdd = new SeatRemove(seatReservationDataContext).Remove(testData);

            //Assert
            Assert.True(seatAdd > 0);
        }

        [Fact]
        public void Remove_Seat_ShouldNotDelete()
        {
            //Arrange
            var seatReservationDataContext = new InMemorySeatReservationDataContext("SeatRemoveNot");
            var testDB = new TestDataset1();
            testDB.Load(seatReservationDataContext);
            var testData = "ABC";

            //Act
            var seatAdd = new SeatRemove(seatReservationDataContext).Remove(testData);

            //Assert
            Assert.Equal(0, seatAdd);
        }
    }
}
