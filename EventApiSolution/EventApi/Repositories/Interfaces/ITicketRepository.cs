using System.Collections.Generic;
using EventApi.Models;

namespace EventApi.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        IList<TicketSale> GetTicketsByEventId(string eventId);
        IList<object> GetTop5EventsBySalesAmountWithName();
        IList<object> GetTop5EventsBySalesCountWithName();

    }
}