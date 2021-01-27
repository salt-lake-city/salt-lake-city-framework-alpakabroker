using System.Collections.Generic;

namespace SaltLakeCity.Framework.Alpakabroker.Abstractions.Registry
{
    public interface IAlpakaAssemblyIdentifier
    {
        /// <summary>
        /// Gibt die Liste der in der Assembly vorhandenen AlpakaEventTypeComposites wieder
        /// </summary>
        /// <returns></returns>
        IEnumerable<AlpakaEventTypeComposite> GetAlpakaEventTypeComposites();
    }
}