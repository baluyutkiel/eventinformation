using Xunit;
using Moq;
using System.Collections.Generic;
using EventApi.Models;
using EventApi.Services;
using EventApi.Repositories.Interfaces;

namespace EventApi.Tests
{
    public class TicketServiceTests
    {
        [Fact]
        public void GetTicketsByEventId_ReturnsTickets()
        {
            var mockRepo = new Mock<ITicketRepository>();
            var tickets = new List<TicketSale>
            {
                new TicketSale { Id = "1", EventId = "E1", UserId = "U1", PriceInCents = 1000 },
                new TicketSale { Id = "2", EventId = "E1", UserId = "U2", PriceInCents = 2000 }
            };
            mockRepo.Setup(r => r.GetTicketsByEventId("E1")).Returns(tickets);

            var service = new TicketService(mockRepo.Object);
            var result = service.GetTicketsByEventId("E1");

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, t => Assert.Equal("E1", t.EventId));
        }

        [Fact]
        public void GetTicketsByEventId_ReturnsEmptyList_WhenNoTickets()
        {
            var mockRepo = new Mock<ITicketRepository>();
            mockRepo.Setup(r => r.GetTicketsByEventId("E2")).Returns(new List<TicketSale>());

            var service = new TicketService(mockRepo.Object);
            var result = service.GetTicketsByEventId("E2");

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetTop5EventsBySalesAmountWithName_ReturnsList()
        {
            var mockRepo = new Mock<ITicketRepository>();
            var expected = new List<object>
            {
                new { eventId = "E1", eventName = "Event 1", totalAmountInCents = 5000 },
                new { eventId = "E2", eventName = "Event 2", totalAmountInCents = 4000 }
            };
            mockRepo.Setup(r => r.GetTop5EventsBySalesAmountWithName()).Returns(expected);

            var service = new TicketService(mockRepo.Object);
            var result = service.GetTop5EventsBySalesAmountWithName();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetTop5EventsBySalesCountWithName_ReturnsList()
        {
            var mockRepo = new Mock<ITicketRepository>();
            var expected = new List<object>
            {
                new { eventId = "E1", eventName = "Event 1", salesCount = 10 },
                new { eventId = "E2", eventName = "Event 2", salesCount = 8 }
            };
            mockRepo.Setup(r => r.GetTop5EventsBySalesCountWithName()).Returns(expected);

            var service = new TicketService(mockRepo.Object);
            var result = service.GetTop5EventsBySalesCountWithName();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}