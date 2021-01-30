namespace SaltLakeCity.Framework.Alpakabroker.EventReceiver
{
    public abstract class EventReceiverBase<TEvent> : IEventReceiver
    {
        /// <summary>
        /// Wenn das Event eingeht
        /// </summary>
        /// <param name="event">Informationen zum eingehenden Event</param>
        protected abstract void OnEvent(TEvent @event);

        /// <summary>
        /// Löst das Event beim Receiver aus
        /// </summary>
        /// <param name="event">Event Parameter, der an den Receiver weitergegeben wird</param>
        public void Invoke(object @event)
        {
            // => Prüfen, ob das Eventobjekt passt
            if (@event.GetType() != typeof(TEvent))
                throw new InvalidEventTypeException(typeof(TEvent), @event.GetType());
            
            // => Event an Implementierung weitergeben
            OnEvent((TEvent) @event);
        }
    }
}