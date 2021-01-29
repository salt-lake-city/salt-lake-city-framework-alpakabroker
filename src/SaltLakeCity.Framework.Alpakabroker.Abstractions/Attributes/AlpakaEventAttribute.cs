using System;

namespace SaltLakeCity.Framework.Alpakabroker.Abstractions.Attributes
{
    /// <summary>
    /// Attribut, mit dem Events gekennzeichnet und als solche erkannt werden
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AlpakaEventAttribute : Attribute
    {
        public static string Name = nameof(AlpakaEventAttribute).Replace("Attribute", string.Empty);
    }
}