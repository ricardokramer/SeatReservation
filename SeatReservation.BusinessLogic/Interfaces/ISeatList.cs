using SeatReservation.BusinessLogic.DTO;
using System.Collections.Generic;

namespace SeatReservation.BusinessLogic.Interfaces
{
    public interface ISeatList
    {
        List<SeatListDTO> List(int eventId);
    }
}