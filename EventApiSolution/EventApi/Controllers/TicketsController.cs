using EventApi.Models;
using EventApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("by-event/{eventId}")]
        public ActionResult<IList<TicketSale>> GetTicketsByEventId(string eventId)
        {
            return Ok(_ticketService.GetTicketsByEventId(eventId));
        }

        [HttpGet("top5-by-amount-with-name")]
        public IActionResult GetTop5BySalesAmountWithName()
        {
            var result = _ticketService.GetTop5EventsBySalesAmountWithName();
            return Ok(result);
        }

        [HttpGet("top5-by-count-with-name")]
        public IActionResult GetTop5BySalesCountWithName()
        {
            var result = _ticketService.GetTop5EventsBySalesCountWithName();
            return Ok(result);
        }
    }
}