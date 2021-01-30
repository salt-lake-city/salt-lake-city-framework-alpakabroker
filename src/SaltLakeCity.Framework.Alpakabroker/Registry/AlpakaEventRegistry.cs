using System;
using System.Collections.Generic;
using System.Linq;
using SaltLakeCity.Framework.Alpakabroker.Abstractions.Config;

namespace SaltLakeCity.Framework.Alpakabroker.Registry
{
    public static class AlpakaEventRegistry
    {
        private static Dictionary<Type, List<object>> _eventReceiver = new Dictionary<Type, List<object>>();
        
        /// <summary>
        /// Initialisiert den AlpakaEventBroker für das Projekt
        /// </summary>
        /// <param name="serviceProvider">ServiceProvider, über den die EventReceiver zuvor lokalisiert wurden</param>
        public static void Init(IServiceProvider serviceProvider)
        {
            // => Event Receiver Types laden
            var eventReceiverTypes = AlpakaEventReceiverLocator.GetLocatedEventReceivers();
            
            // => Event Receiver Instanzen aus Provider ziehen
            foreach (var eventReceiverType in eventReceiverTypes)
                AddEventReceiver(serviceProvider.GetService(eventReceiverType));
        }

        public static void Emit(object @event)
        {
            // => EventType ermitteln
            var eventType = @event.GetType();
            
            // => Prüfen, ob ein Receiver für den EventType registriert wurde
            if (!_eventReceiver.ContainsKey(eventType))
                return;
            

        }

        private static void AddEventReceiver(object eventReceiver)
        {
            // => EventType des Receivers ermitteln
            var eventType = eventReceiver
                .GetType() // => Implementierung
                .BaseType? // => Generierte Base Implementierung des Events
                .BaseType? // => BaseType für EventReceiver
                .GetGenericArguments()
                .First(); // => Type des Events als erster Generic Parameter

            if (eventType == null)
                throw new InvalidOperationException();
            
            
            if(!_eventReceiver.ContainsKey(eventType))
                _eventReceiver.Add(eventType, new List<object>());
            _eventReceiver[eventType].Add(eventReceiver);
            
        }
    }
}