using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SaltLakeCity.Framework.Alpakabroker.Abstractions.EventReceiver;
using SaltLakeCity.Framework.Core;

namespace SaltLakeCity.Framework.Alpakabroker.Abstractions.Config
{
    public static class AlpakaEventReceiverLocator
    {
        private static List<Type> _locatedEventReceivers = new List<Type>();

        public static IEnumerable<Type> GetLocatedEventReceivers() => _locatedEventReceivers;

        public static IServiceCollection Locate(AssemblyProxy assembly, IServiceCollection serviceCollection)
        {
            // => Event Receiver in Assembly suchen
            var eventReceiver = assembly.GetTypes().Where(x =>
                x.BaseType?.BaseType?.GetGenericTypeDefinition() ==
                typeof(EventReceiverBase<>).GetGenericTypeDefinition()).ToList();
            
            // => Event Receiver in ServiceCollection für DI registrieren
            foreach (var type in eventReceiver)
            {
                serviceCollection.AddSingleton(type);
                _locatedEventReceivers.Add(type);
            }
            
            return serviceCollection;
        }
    }
}