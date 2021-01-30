using System;
using System.Collections.Generic;
using System.Linq;
using SaltLakeCity.Framework.Alpakabroker.Config;
using SaltLakeCity.Framework.Alpakabroker.EventReceiver;
using SaltLakeCity.Framework.Logging;

namespace SaltLakeCity.Framework.Alpakabroker.Registry
{
    public static class AlpakaEventRegistry
    {
        private static Dictionary<Type, List<IEventReceiver>> _eventReceiver = new Dictionary<Type, List<IEventReceiver>>();
        
        /// <summary>
        /// Initialisiert den AlpakaEventBroker für das Projekt
        /// </summary>
        /// <param name="serviceProvider">ServiceProvider, über den die EventReceiver zuvor lokalisiert wurden</param>
        public static void Init(IServiceProvider serviceProvider)
        {
            Logger.Information("AlpakaEventBroker Registry wird initialisiert..", "43A6189F-6DB0-41FD-8BAC-BDE1B913892A");
            
            // => Event Receiver Types laden
            var eventReceiverTypes = AlpakaEventReceiverLocator.GetLocatedEventReceivers();
            
            // => Event Receiver Instanzen aus Provider ziehen
            foreach (var eventReceiverType in eventReceiverTypes)
                AddEventReceiver((IEventReceiver) serviceProvider.GetService(eventReceiverType));
            
            Logger.Information("AlpakaEventBroker Registry wurde initialisiert.", "E639CE11-E132-4B28-927F-FC132C159453");

        }

        public static void Emit(object @event)
        {
            // => EventType ermitteln
            var eventType = @event.GetType();
            
            // => Prüfen, ob ein Receiver für den EventType registriert wurde
            if (!_eventReceiver.ContainsKey(eventType))
                return;
            
            foreach (var eventReceiver in _eventReceiver[eventType])
                eventReceiver.Invoke(@event);


        }

        private static void AddEventReceiver(IEventReceiver eventReceiver)
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