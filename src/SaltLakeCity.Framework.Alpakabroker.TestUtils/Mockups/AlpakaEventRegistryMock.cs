using System.Collections.Generic;
using SaltLakeCity.Framework.Alpakabroker.Registry;

namespace SaltLakeCity.Framework.Alpakabroker.TestUtils.Mockups
{
    public class AlpakaEventRegistryMock : IAlpakaEventRegistry
    {
        private readonly List<object> _emittedEvents = new List<object>();

        public List<object> EmittedEvents => _emittedEvents;

        public void Emit(object @event)
        {
            EmittedEvents.Add(@event);
        }
    }
}