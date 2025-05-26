using EventApi.Models;
using EventApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{days}")]
        public ActionResult<IList<Event>> GetUpcomingEvents(int days)
        {
            if (days != 30 && days != 60 && days != 180)
                return BadRequest("Days parameter must be 30, 60, or 180.");

            var events = _eventService.GetUpcomingEvents(days);
            return Ok(events);
        }

        [HttpGet("event/{id}")]
        public ActionResult<Event?> GetEventById(string id)
        {
            var ev = _eventService.GetEventById(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }
    }
}