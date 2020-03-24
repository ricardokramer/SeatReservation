using SeatReservation.BusinessLogic.Tests.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SeatReservation.BusinessLogic.Tests
{
    public class SeatListTests
    {
        [Theory]
        [InlineData(1, 1, 3, "Reserved")]
        [InlineData(1, 9, 3, "Reserved")]
        [InlineData(2, 5, 1, "Available")]
        [InlineData(3, 7, 4, "Not Available")]
        [InlineData(4, 3, 1, "Available")]
        [InlineData(4, 4, 2, "Booked")]
        [InlineData(4, 5, 2, "Booked")]
        [InlineData(4, 6, 2, "Booked")]
        [InlineData(4, 7, 1, "Available")]
        public void List_CheckStatus_ReturnSeat(int row, int column, int allocationTypeId, string allocationTypeDescription)
        {
            //Arrange
            var seatReservationDataContext = new InMemorySeatReservationDataContext($"SeatList{row}{column}");
            var testDB = new TestDataset1();
            testDB.Load(seatReservationDataContext);

            //Act
            var eventList = new SeatList(seatReservationDataContext).List(2);
            var item = eventList.Where(p => p.Row == row && p.Column == column).FirstOrDefault();

            //Assert
            Assert.NotNull(item);
            Assert.Equal(item.AllocationTypeId, allocationTypeId);
            Assert.Equal(item.AllocationTypeDescription, allocationTypeDescription);
        }
    }
}
