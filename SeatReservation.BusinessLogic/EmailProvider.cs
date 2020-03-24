using Microsoft.Extensions.Logging;
using SeatReservation.BusinessLogic.Interfaces;
using SeatReservation.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatReservation.BusinessLogic
{
    public class EmailProvider : IEmailProvider
    {
        //this would come from some configuration file
        private readonly string _emailFrom = "events@zupa.co.uk";

        private readonly ISeatReservationDataContext _seatReservationDataContext;
        private readonly ILogger<EmailProvider> _logger;

        public EmailProvider(ISeatReservationDataContext dataContext, ILogger<EmailProvider> logger)
        {
            _seatReservationDataContext = dataContext;
        }

        public void SendConfirmationEmail(int eventSeatId)
        {
            FormatEmail(eventSeatId, false);
        }

        public void SendCancellationEmail(int eventSeatId)
        {
            FormatEmail(eventSeatId, true);
        }

        private void FormatEmail(int eventSeatId, bool isCancelation)
        {
            try
            {
                var result = _seatReservationDataContext.EventSeats
                    .Join(_seatReservationDataContext.Events,
                    es =>es.EventId,
                    ev => ev.EventId,
                    (es, ev) => new { es, ev})
                    .Where(p => p.es.EventSeatId == eventSeatId)
                    .Select(p => p)
                    .First();

                var content = (isCancelation ? result.ev.ContentCancellation : result.ev.ContentEnrolment)
                    .Replace("{PersonName}", result.es.PersonName)
                    .Replace("{EventName}", result.ev.Name)
                    .Replace("{Date}", result.ev.DateTimeStart.ToString("dd/MM/YYYY"))
                    .Replace("{Time}", result.ev.DateTimeStart.ToString("hh:mm"))
                    .Replace("{Identifier}", result.es.Identifier);

                var subject = result.ev.Name;
                subject += (isCancelation ? " cancellation confirmation" : " enrollment confirmation");

                Submit(_emailFrom, result.es.Email, subject, content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private void Submit(string emailFrom, string emailTo, string subject, string content)
        {
            //this method would put the email in a queue to be sent later
            //this should not send an email directly throught smpt server because this would block the service
            //throw new NotImplementedException();
        }
    }
}
