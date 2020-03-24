using SeatReservation.BusinessLogic.DTO;
using System.Collections.Generic;

namespace SeatReservation.BusinessLogic.Interfaces
{
    public interface ISeatAdd
    {
        List<int> Create(int eventId, List<SeatAddItemDTO> items);
    }
}
