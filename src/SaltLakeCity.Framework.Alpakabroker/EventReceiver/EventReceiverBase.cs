namespace SaltLakeCity.Framework.Alpakabroker.EventReceiver
{
    public abstract class EventReceiverBase<TEvent>
    {
        /// <summary>
        /// Wenn das Event eingeht
        /// </summary>
        /// <param name="event">Informationen zum eingehenden Event</param>
        public abstract void OnEvent(TEvent @event);
    }
}