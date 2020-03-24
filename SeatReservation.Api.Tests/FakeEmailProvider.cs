using SeatReservation.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeatReservation.Api.Tests
{
    public class FakeEmailProvider : IEmailProvider
    {
        public FakeEmailProvider()
        {
        }

        public void SendCancellationEmail(int eventSeatId)
        {
        }

        public void SendConfirmationEmail(int eventSeatId)
        {
        }
    }
}
