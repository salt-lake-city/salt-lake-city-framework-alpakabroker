namespace SaltLakeCity.Framework.Alpakabroker.Registry
{
    public static class AlpakaEventRegistry
    {
        internal static IAlpakaEventRegistry EventRegistry { get; set; }

        public static void Emit(object @event) => EventRegistry.Emit(@event);
    }
}