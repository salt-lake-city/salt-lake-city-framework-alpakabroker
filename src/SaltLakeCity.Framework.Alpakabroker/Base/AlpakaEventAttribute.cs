using System;

namespace SaltLakeCity.Framework.Alpakabroker
{
    /// <summary>
    /// Attribut, mit dem Events gekennzeichnet und als solche erkannt werden
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AlpakaEventAttribute : Attribute
    {
        
    }
}