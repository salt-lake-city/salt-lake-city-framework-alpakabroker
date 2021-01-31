using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SaltLakeCity.Framework.Alpakabroker.EventReceiver;
using SaltLakeCity.Framework.Core;

namespace SaltLakeCity.Framework.Alpakabroker.Config
{
    public static class AlpakaBrokerConfigExtensions
    {
        public static IServiceCollection ConfigureAlpakaBroker(this IServiceCollection serviceCollection)
        {
            var eventReceiver = AlpakaEventReceiverLocator.Locate(new AssemblyProxy(Assembly.GetExecutingAssembly()));
            
            // => Event Receiver in ServiceCollection für DI registrieren
            foreach (var type in eventReceiver)
                serviceCollection.AddSingleton(typeof(IEventReceiver), type);

            return serviceCollection;

        }
    }
}