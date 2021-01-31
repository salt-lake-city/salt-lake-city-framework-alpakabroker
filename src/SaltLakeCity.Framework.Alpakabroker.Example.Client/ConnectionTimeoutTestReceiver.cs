using SaltLakeCity.Framework.Alpakabroker.Example.Events.Connection;

namespace SaltLakeCity.Framework.Alpakabroker.Example.Client
{
    public class ConnectionTimeoutTestReceiver : ConnectionTimeoutEventReceiverBase
    {
        protected override void OnEvent(ConnectionTimeoutEvent @event)
        {
            
        }
    }
}