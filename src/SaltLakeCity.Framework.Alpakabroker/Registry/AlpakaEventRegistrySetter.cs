namespace SaltLakeCity.Framework.Alpakabroker.Registry
{
    public static class AlpakaEventRegistrySetter
    {
        public static void Set(IAlpakaEventRegistry registry) => AlpakaEventRegistry.EventRegistry = registry;
    }
}