using System.Collections.Generic;
using EventApi.Models;

namespace EventApi.Repositories.Interfaces
{
    public interface IEventRepository
    {
        IList<Event> GetUpcomingEvents(int days);
        Event GetById(string id);
    }
}