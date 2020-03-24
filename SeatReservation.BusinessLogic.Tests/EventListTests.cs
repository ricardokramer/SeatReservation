using SeatReservation.BusinessLogic.Tests.Database;
using System;
using Xunit;

namespace SeatReservation.BusinessLogic.Tests
{
    public class EventListTests
    {

        [Fact]
        public void EventList_ListEvents_ShouldReturnMarch()
        {
            //Arrange
            var seatReservationDataContext = new InMemorySeatReservationDataContext("EventList_ListEvents");
            var testDB = new TestDataset1();
            testDB.Load(seatReservationDataContext);

            //Act
            var items = new EventList(seatReservationDataContext).List();

            //Assert
            Assert.Single(items);
            Assert.Equal("March Dev Meeting", items[0].Name);
            Assert.Equal("Room1", items[0].RoomName);
        }
    }
}
