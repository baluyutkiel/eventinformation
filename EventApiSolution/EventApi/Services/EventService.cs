using System.Collections.Generic;
using EventApi.Models;
using EventApi.Repositories.Interfaces;
using EventApi.Services.Interfaces;

namespace EventApi.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }

        public IList<Event> GetUpcomingEvents(int daysAhead)
        {
            return _repository.GetUpcomingEvents(daysAhead);
        }

        public Event? GetEventById(string id)
        {
            return _repository.GetById(id);
        }
    }
}