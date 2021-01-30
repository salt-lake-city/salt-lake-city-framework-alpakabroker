namespace SaltLakeCity.Framework.Alpakabroker.EventReceiver
{
    public interface IEventReceiver
    {
        /// <summary>
        /// Löst das Event beim Receiver aus
        /// </summary>
        /// <param name="event">Event Parameter, der an den Receiver weitergegeben wird</param>
        void Invoke(object @event);
    }
}