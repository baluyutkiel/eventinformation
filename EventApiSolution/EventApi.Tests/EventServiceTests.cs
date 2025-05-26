using Xunit;
using EventApi.Models;
using EventApi.Services;
using Moq;
using EventApi.Repositories.Interfaces;
using System.Collections.Generic;

namespace EventApi.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void GetById_ReturnsEvent_WhenEventExists()
        {
            var mockRepo = new Mock<IEventRepository>();
            var expectedEvent = new Event { Id = "1", Name = "Test Event" };
            mockRepo.Setup(r => r.GetById("1")).Returns(expectedEvent);

            var service = new EventService(mockRepo.Object);

            var result = service.GetEventById("1");

            Assert.NotNull(result);
            Assert.Equal("Test Event", result.Name);
        }

        [Fact]
        public void GetById_ReturnsNull_WhenEventDoesNotExist()
        {
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetById("2")).Returns((Event?)null);

            var service = new EventService(mockRepo.Object);
            var result = service.GetEventById("2");
            Assert.Null(result);
        }

        [Fact]
        public void GetUpcomingEvents_ReturnsEventsWithinDays()
        {
            var mockRepo = new Mock<IEventRepository>();
            var events = new List<Event>
            {
                new Event { Id = "1", Name = "Event 1" },
                new Event { Id = "2", Name = "Event 2" }
            };
            mockRepo.Setup(r => r.GetUpcomingEvents(30)).Returns(events);

            var service = new EventService(mockRepo.Object);
            var result = service.GetUpcomingEvents(30);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.Name == "Event 1");
            Assert.Contains(result, e => e.Name == "Event 2");
        }

        [Fact]
        public void GetUpcomingEvents_ReturnsEmptyList_WhenNoEvents()
        {
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetUpcomingEvents(30)).Returns(new List<Event>());

            var service = new EventService(mockRepo.Object);
            var result = service.GetUpcomingEvents(30);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetUpcomingEvents_CallsRepositoryWithCorrectDays()
        {
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetUpcomingEvents(It.IsAny<int>())).Returns(new List<Event>());

            var service = new EventService(mockRepo.Object);
            service.GetUpcomingEvents(60);

            mockRepo.Verify(r => r.GetUpcomingEvents(60), Times.Once);
        }
    }
}