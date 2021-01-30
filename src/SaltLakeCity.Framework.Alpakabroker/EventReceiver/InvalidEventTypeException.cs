using System;

namespace SaltLakeCity.Framework.Alpakabroker.EventReceiver
{
    public class InvalidEventTypeException : Exception
    {
        private Type _expectedType;
        private Type _receivedType;

        public InvalidEventTypeException(Type expectedType, Type receivedType)
        {
            _expectedType = expectedType;
            _receivedType = receivedType;
        }

        public override string ToString()
        {
            return $"Der übergebene EventTyp ist ungültig. Erwartet: {_expectedType} - Erhalten: {_receivedType}";
        }
    }
}