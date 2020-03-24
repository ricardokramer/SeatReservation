using SeatReservation.BusinessLogic.DTO;
using SeatReservation.BusinessLogic.Tests.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace SeatReservation.BusinessLogic.Tests
{
    public class SeatAddTests
    {
        [Fact]
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
        }

        [Fact]
        public void Create_MultipleSeats_ShouldPass()
        {
            //Arrange
            var seatReservationDataContext = new InMemorySeatReservationDataContext("SeatAddMultiple");
            var testDB = new TestDataset1();
            testDB.Load(seatReservationDataContext);
            var testData = GetDataset2();

            //Act
            var seatAdd = new SeatAdd(seatReservationDataContext).Create(2, testData);

            //Assert
            Assert.True(seatAdd.Count == 4);
        }

        [Theory]
        [InlineData("John Smith", "random@gmail.com", "The person")]
        [InlineData("Random Guy", "jamescook@gmail.com", "The email")]
        public void Create_Seat_ValidationShouldFail(string name, string email, string expected)
        {
            //Arrange
            var seatReservationDataContext = new InMemorySeatReservationDataContext($"SeatAddValidation{name.Substring(0,3)}");
            var testDB = new TestDataset1();
            testDB.Load(seatReservationDataContext);
            var testData = new List<SeatAddItemDTO>
            { 
                new SeatAddItemDTO  { SeatId = 16, PersonName = name, Email = email } 
            };

            //Act
            Exception ex = Assert.Throws<ValidationException>(() => new SeatAdd(seatReservationDataContext).Create(2, testData));

             //Assert
            Assert.Contains(expected, ex.Message);
        }

        private List<SeatAddItemDTO> GetDataset1()
        {
            return new List<SeatAddItemDTO>
            {
                new SeatAddItemDTO { SeatId = 16, PersonName = "Jason Bourne", Email = "jasonb@gmail.com" }
            };
        }

        private List<SeatAddItemDTO> GetDataset2()
        {
            return new List<SeatAddItemDTO>
            {
                new SeatAddItemDTO { SeatId = 16, PersonName = "Jason Bourne1", Email = "jasonb1@gmail.com" },
                new SeatAddItemDTO { SeatId = 17, PersonName = "Jason Bourne2", Email = "jasonb2@gmail.com" },
                new SeatAddItemDTO { SeatId = 18, PersonName = "Jason Bourne3", Email = "jasonb3@gmail.com" },
                new SeatAddItemDTO { SeatId = 19, PersonName = "Jason Bourne4", Email = "jasonb4@gmail.com" }
            };
        }
    }
}
