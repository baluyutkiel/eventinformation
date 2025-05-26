using System.Collections.Generic;
using EventApi.Models;

namespace EventApi.Services.Interfaces
{
    public interface ITicketService
    {
        IList<TicketSale> GetTicketsByEventId(string eventId);
        IList<object> GetTop5EventsBySalesAmountWithName();
        IList<object> GetTop5EventsBySalesCountWithName();
    }
}