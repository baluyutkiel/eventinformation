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
        public ActionResult<IList<Event>> GetUpcomingEvents(
            int days,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (days != 30 && days != 60 && days != 180)
                return BadRequest("Days parameter must be 30, 60, or 180.");

            var allEvents = _eventService.GetUpcomingEvents(days);
            var pagedEvents = allEvents
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var total = allEvents.Count;

            return Ok(new
            {
                total,
                page,
                pageSize,
                events = pagedEvents
            });
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