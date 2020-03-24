using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SeatReservation.BusinessLogic;
using SeatReservation.BusinessLogic.DTO;
using SeatReservation.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SeatReservation.Api.Tests
{
    public class SeatsControllerTests
    {
        [Fact]
        public void Get_OneEvent_ShouldReturnItems()
        {
            //Arrange
            var testData = GetSeatList();

            var mockSeatList = new Mock<SeatList>();
            mockSeatList.Setup(x => x.List(1))
                    .Returns(testData);

            var controller = new SeatsController(mockSeatList.Object, null, null, new FakeEmailProvider(), new NullLogger<SeatsController>());

            //Act
            var result = controller.Get(1);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<SeatListDTO>>(objectResult.Value);
            Assert.Equal(3, model.Count);
            Assert.Equal(1, model[0].EventId);
            Assert.Equal("A3", model[2].Name);
        }

        private List<SeatListDTO> GetSeatList()
        {
            return new List<SeatListDTO>
            {
                new SeatListDTO
                {
                    EventId = 1,
                    SeatId = 1,
                    Name = "A1",
                    Row = 1,
                    Column = 1,
                    AllocationTypeId = 1,
                    AllocationTypeDescription = "Available",
                },
                new SeatListDTO
                {
                    EventId = 1,
                    SeatId = 2,
                    Name = "A2",
                    Row = 1,
                    Column = 2,
                    AllocationTypeId = 1,
                    AllocationTypeDescription = "Available",
                },
                new SeatListDTO
                {
                    EventId = 1,
                    SeatId = 3,
                    Name = "A3",
                    Row = 1,
                    Column = 3,
                    AllocationTypeId = 1,
                    AllocationTypeDescription = "Available",
                }
            };
        }
    }
}
