using System.Collections.Generic;
using System.Linq;
using EventApi.Models;
using NHibernate.Linq;

namespace EventApi.Repositories
{
    public class TicketRepository : Interfaces.ITicketRepository
    {
        private readonly NHibernate.ISession _session;

        public TicketRepository(NHibernate.ISession session)
        {
            _session = session;
        }

        public IList<TicketSale> GetTicketsByEventId(string eventId)
        {
            return _session.Query<TicketSale>()
                .Where(t => t.EventId == eventId)
                .ToList();
        }
        public IList<object> GetTop5EventsBySalesAmountWithName()
        {
            var allTickets = _session.Query<TicketSale>().ToList();
            var allEvents = _session.Query<Event>().ToList();

            var top5 = allTickets
                .GroupBy(t => t.EventId)
                .Select(g => new
                {
                    EventId = g.Key,
                    TotalAmountInCents = g.Sum(t => t.PriceInCents)
                })
                .OrderByDescending(x => x.TotalAmountInCents)
                .Take(5)
                .ToList();

            var result = top5
                .Join(allEvents,
                    sale => sale.EventId,
                    ev => ev.Id,
                    (sale, ev) => new
                    {
                        EventId = sale.EventId,
                        EventName = ev.Name,
                        TotalAmountInCents = sale.TotalAmountInCents
                    })
                .ToList<object>();

            return result;
        }

        public IList<object> GetTop5EventsBySalesCountWithName()
        {
            var allTickets = _session.Query<TicketSale>().ToList();
            var allEvents = _session.Query<Event>().ToList();

            var top5 = allTickets
                .GroupBy(t => t.EventId)
                .Select(g => new
                {
                    EventId = g.Key,
                    SalesCount = g.Count()
                })
                .OrderByDescending(x => x.SalesCount)
                .Take(5)
                .ToList();

            var result = top5
                .Join(allEvents,
                    sale => sale.EventId,
                    ev => ev.Id,
                    (sale, ev) => new
                    {
                        eventId = sale.EventId,
                        eventName = ev.Name,
                        salesCount = sale.SalesCount
                    })
                .ToList<object>();

            return result;
        }
        
    }
}