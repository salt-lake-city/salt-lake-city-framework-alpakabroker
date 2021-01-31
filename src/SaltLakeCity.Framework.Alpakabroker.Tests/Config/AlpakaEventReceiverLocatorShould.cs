using System;
using System.Collections.Generic;
using SaltLakeCity.Framework.Alpakabroker.Config;
using SaltLakeCity.Framework.Alpakabroker.EventReceiver;
using SaltLakeCity.Framework.Core;
using Xunit;

namespace SaltLakeCity.Framework.Alpakabroker.Tests.Config
{
    public class AlpakaEventReceiverLocatorShould
    {
        [Fact]
        public void FindAllEventReceiverInAssembly()
        {
            // => Arrange
            var assemblyMock = new AssemblyMock()
            {
                Types = new List<Type>()
                {
                    typeof(TestEvent),
                    typeof(TestEventReceiverBase),
                    typeof(TestEventReceiver),
                }
            };
            
            // => Act
            var eventReceiver = AlpakaEventReceiverLocator.Locate(assemblyMock);

            // => Assert
            Assert.Single(eventReceiver);
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
        

        private class AssemblyMock : IAssembly
        {
            public List<Type> Types { get; set; }
            public IEnumerable<Type> GetTypes() => Types;
        }
    }
}