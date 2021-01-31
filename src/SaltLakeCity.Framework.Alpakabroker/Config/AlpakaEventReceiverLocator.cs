using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SaltLakeCity.Framework.Alpakabroker.EventReceiver;
using SaltLakeCity.Framework.Core;

namespace SaltLakeCity.Framework.Alpakabroker.Config
{
    public static class AlpakaEventReceiverLocator
    {
        public static IServiceCollection Locate(AssemblyProxy assembly, IServiceCollection serviceCollection)
        {
            // => Event Receiver in Assembly suchen
            var eventReceiver = assembly.GetTypes().Where(x =>
                x.BaseType?.BaseType?.GetGenericTypeDefinition() ==
                typeof(EventReceiverBase<>).GetGenericTypeDefinition()).ToList();
            
            // => Event Receiver in ServiceCollection für DI registrieren
            foreach (var type in eventReceiver)
                serviceCollection.AddSingleton(typeof(IEventReceiver), type);

            return serviceCollection;
        }
    }
}