using SeatReservation.BusinessLogic.DTO;
using System.Collections.Generic;

namespace SeatReservation.BusinessLogic.Interfaces
{
    public interface IEventList
    {
        List<EventListDTO> List();
    }
}
