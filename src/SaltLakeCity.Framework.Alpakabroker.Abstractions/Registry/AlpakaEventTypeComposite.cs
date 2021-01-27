using System;

namespace SaltLakeCity.Framework.Alpakabroker.Abstractions.Registry
{
    public class AlpakaEventTypeComposite
    {
        public AlpakaEventTypeComposite(Type alpakaEventType, Type alpakaEventReceiverBaseType, Type alpakaEventEmitterType)
        {
            AlpakaEventType = alpakaEventType;
            AlpakaEventReceiverBaseType = alpakaEventReceiverBaseType;
            AlpakaEventEmitterType = alpakaEventEmitterType;
        }

        public Type AlpakaEventType { get; }
        public Type AlpakaEventReceiverBaseType { get; }
        public Type AlpakaEventEmitterType { get; }
    }
}