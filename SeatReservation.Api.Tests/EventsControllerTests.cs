using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SeatReservation.BusinessLogic;
using SeatReservation.BusinessLogic.DTO;
using SeatReservation.Controllers.Api;
using System;
using System.Collections.Generic;
using Xunit;

namespace SeatReservation.Api.Tests
{
    public class EventsControllerTests
    {
        [Fact]
        public void Get_OneEvent_ShouldPass()
        {
            //Arrange
            var testData = new List<EventListDTO>
            {
                new EventListDTO
                {
                    EventId = 1,
                    Name = "Event1",
                    RoomName = "Room1",
                    Address1 ="Address1",
                    Address2 = "",
                    PostalCode = "SO12AB",
                    City = "Southampton",
                    DateStart = new DateTime(2020,1,1,17,0,0)
                }
            };

            var mockEventList = new Mock<EventList>();
            mockEventList.Setup(x => x.List())
                    .Returns(testData);

            var controller = new EventsController(mockEventList.Object, new NullLogger<EventsController>());

            //Act
            var result = controller.Get();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<EventListDTO>>(objectResult.Value);
            Assert.Single(model);
            Assert.Equal(1, model[0].EventId);
            Assert.Equal("Event1", model[0].Name);
        }

        [Fact]
        public void Get_NoEvent_ShouldPass()
        {
            //Arrange
            var testData = new List<EventListDTO>();

            var mockEventList = new Mock<EventList>();
            mockEventList.Setup(x => x.List())
                    .Returns(testData);

            var controller = new EventsController(mockEventList.Object, new NullLogger<EventsController>());

            //Act
            var result = controller.Get();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<EventListDTO>>(objectResult.Value);
            Assert.Empty(model);
        }

        [Fact]
        public void Get_DBError_ShouldFail()
        {
            //Arrange
            var mockEventList = new Mock<EventList>();
            mockEventList.Setup(x => x.List())
                    .Throws(new Exception());

            var controller = new EventsController(mockEventList.Object, new NullLogger<EventsController>());

            //Act
            var result = controller.Get();

            //Assert
            var statusCode = Assert.IsType<Microsoft.AspNetCore.Mvc.StatusCodeResult>(result);
            Assert.Equal(500, statusCode.StatusCode);
        }
    }
}
