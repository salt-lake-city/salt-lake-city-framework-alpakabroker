namespace SaltLakeCity.Framework.Alpakabroker.Registry
{
    public interface IAlpakaEventRegistry
    {
        void Emit(object @event);
    }
}