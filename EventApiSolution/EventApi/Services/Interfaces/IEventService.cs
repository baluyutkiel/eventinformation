using System.Collections.Generic;
using EventApi.Models;

namespace EventApi.Services.Interfaces
{
    public interface IEventService
    {
        IList<Event> GetUpcomingEvents(int daysAhead);
        Event GetEventById(string id);
    }
}