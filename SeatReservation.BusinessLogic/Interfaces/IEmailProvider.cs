namespace SeatReservation.BusinessLogic.Interfaces
{
    public interface IEmailProvider
    {
        void SendCancellationEmail(int eventSeatId);
        void SendConfirmationEmail(int eventSeatId);
    }
}
