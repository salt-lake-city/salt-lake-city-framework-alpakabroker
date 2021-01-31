using System.Collections.Generic;
using SaltLakeCity.Framework.Alpakabroker.EventReceiver;
using SaltLakeCity.Framework.Alpakabroker.Registry;
using Xunit;

namespace SaltLakeCity.Framework.Alpakabroker.Tests.Registry
{
    public class AlpakaEventRegistryImplementationShould
    {
        [Fact]
        public void Initialize()
        {
            // => Arrange
            var eventReceivers = new List<IEventReceiver>()
            {
                new TestEventReceiver()
            };

            // => Act
            var unused = new AlpakaEventRegistryImplementation(eventReceivers);
        }
        
        [Fact]
        public void EmitToReceiver()
        {
            // => Arrange
            var testEventReceiver = new TestEventReceiver();
            var eventReceivers = new List<IEventReceiver>()
            {
                testEventReceiver
            };
            var registry = new AlpakaEventRegistryImplementation(eventReceivers);

            // => Act
            registry.Emit(new TestEvent());
            
            // => Assert
            Assert.Single(testEventReceiver.ReceivedEvents);

        }
        
        private class TestEvent
        {
            
        }

        private abstract class TestEventReceiverBase : EventReceiverBase<TestEvent>
        {
        }

        private class TestEventReceiver : TestEventReceiverBase
        {
            public List<TestEvent> ReceivedEvents { get; set; } = new();
            protected override void OnEvent(TestEvent @event)
            {
                ReceivedEvents.Add(@event);
            }
        }
    }
}