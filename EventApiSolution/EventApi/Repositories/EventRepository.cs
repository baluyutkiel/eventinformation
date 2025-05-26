using System.Collections.Generic;
using System.Linq;
using EventApi.Models;
using NHibernate;
using NHibernate.Linq;

using NHibernateSession = NHibernate.ISession;

namespace EventApi.Repositories
{
    public class EventRepository : Interfaces.IEventRepository
    {
        private readonly NHibernateSession _session;

        public EventRepository(NHibernateSession session)
        {
            _session = session;
        }

        public IList<Event> GetUpcomingEvents(int days)
        {
            var now = System.DateTime.Now;
            var endDate = now.AddDays(days);

            return _session.Query<Event>()
                .Where(e => e.StartsOn >= now && e.StartsOn <= endDate)
                .OrderBy(e => e.StartsOn)
                .ToList();
        }

        public Event GetById(string id)
        {
            return _session.Get<Event>(id);
        }
    }
}