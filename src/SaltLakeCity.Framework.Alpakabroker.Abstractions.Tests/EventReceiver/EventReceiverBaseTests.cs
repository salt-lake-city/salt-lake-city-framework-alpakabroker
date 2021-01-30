using System.Collections.Generic;
using System.Linq;
using SaltLakeCity.Framework.Alpakabroker.Abstractions.EventReceiver;
using Xunit;

namespace SaltLakeCity.Framework.Alpakabroker.Abstractions.Tests.EventReceiver
{
    public class EventReceiverBaseTests
    {
        [Fact]
        public void Invoke_Success()
        {
            // => Arrange
            var receiver = new TestEventReceiver();

            // => Act
            receiver.Invoke(new TestEvent {Name = "NyghtX"});

            // => Assert
            Assert.Single(receiver.ReceivedEvents);
            Assert.Equal("NyghtX", receiver.ReceivedEvents.First().Name);
        }
        
        [Fact]
        public void Invoke_Fails_InvalidEvent()
        {
            // => Arrange
            var receiver = new TestEventReceiver();

            // => Act
            Assert.Throws<InvalidEventTypeException>(() => receiver.Invoke(new WrongTestEvent()));

            // => Assert
            Assert.Empty(receiver.ReceivedEvents);
        }

        private class TestEvent
        {
            public string Name { get; init; }
        }

        private class WrongTestEvent
        {
        }

        private class TestEventReceiver : EventReceiverBase<TestEvent>
        {
            public List<TestEvent> ReceivedEvents { get; set; } = new();
            protected override void OnEvent(TestEvent @event)
            {
                ReceivedEvents.Add(@event);
            }
        }
    }
}