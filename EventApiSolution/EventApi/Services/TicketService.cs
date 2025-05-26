using System.Collections.Generic;
using EventApi.Models;
using EventApi.Repositories.Interfaces;
using EventApi.Services.Interfaces;

namespace EventApi.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public IList<TicketSale> GetTicketsByEventId(string eventId)
        {
            return _repository.GetTicketsByEventId(eventId);
        }

        public IList<object> GetTop5EventsBySalesAmountWithName()
        {
            return _repository.GetTop5EventsBySalesAmountWithName();
        }

        public IList<object> GetTop5EventsBySalesCountWithName()
        {
            return _repository.GetTop5EventsBySalesCountWithName();
        }
    }
}