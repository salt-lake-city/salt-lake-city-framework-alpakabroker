using System;
using System.Collections.Generic;
using System.Linq;
using SaltLakeCity.Framework.Alpakabroker.EventReceiver;
using SaltLakeCity.Framework.Logging;

namespace SaltLakeCity.Framework.Alpakabroker.Registry
{
    public class AlpakaEventRegistryImplementation : IAlpakaEventRegistry
    {
        /// <summary>
        /// Registrierte EventReceiver
        /// </summary>
        private Dictionary<Type, List<IEventReceiver>> _eventReceiver = new Dictionary<Type, List<IEventReceiver>>();

        public AlpakaEventRegistryImplementation(IEnumerable<IEventReceiver> eventReceivers)
        {
            foreach (var eventReceiver in eventReceivers)
                AddEventReceiver(eventReceiver);
        }
        
        public void Emit(object @event)
        {
            // => EventType ermitteln
            var eventType = @event.GetType();
            
            // => Prüfen, ob ein Receiver für den EventType registriert wurde
            if (!_eventReceiver.ContainsKey(eventType))
                return;
            
            foreach (var eventReceiver in _eventReceiver[eventType])
                eventReceiver.Invoke(@event);
        }
        
        private void AddEventReceiver(IEventReceiver eventReceiver)
        {
            // => EventType des Receivers ermitteln
            var eventType = eventReceiver
                .GetType() // => Implementierung
                .BaseType? // => Generierte Base Implementierung des Events
                .BaseType? // => BaseType für EventReceiver
                .GetGenericArguments()
                .First(); // => Type des Events als erster Generic Parameter
            
            Logger.Information($"Event Receiver wird hinzugefügt: {eventReceiver}", "B60ECF4C-5EBA-40D8-B04C-B5FBA3ACDF5D");

            if (eventType == null)
                throw new InvalidOperationException();
            
            
            if(!_eventReceiver.ContainsKey(eventType))
                _eventReceiver.Add(eventType, new List<IEventReceiver>());
            _eventReceiver[eventType].Add(eventReceiver);
            
        }
    }
}