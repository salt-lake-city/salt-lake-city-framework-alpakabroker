using System;
using System.Collections.Generic;
using System.Linq;
using SaltLakeCity.Framework.Alpakabroker.EventReceiver;
using SaltLakeCity.Framework.Core;

namespace SaltLakeCity.Framework.Alpakabroker.Config
{
    public static class AlpakaEventReceiverLocator
    {
        public static IEnumerable<Type> Locate(IAssembly assembly) =>
            assembly.GetTypes().Where(x => typeof(IEventReceiver).IsAssignableFrom(x) && !x.IsAbstract).ToList();
    }
}