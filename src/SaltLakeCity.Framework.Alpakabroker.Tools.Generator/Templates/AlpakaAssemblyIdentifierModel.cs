using System.Collections.Generic;
using System.Linq;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Templates
{
    public class AlpakaAssemblyIdentifierModel
    {
        public string Namespace { get; set; }
        public List<string> Usings { get; set; } = new();

        public List<string> AlpakaEvents { get; set; }

        public static AlpakaAssemblyIdentifierModel From(string ns, List<AlpakaEventModel> alpakaEventModels)
        {
            var namespaces = alpakaEventModels.Select(x => x.Namespace);
            return new()
            {
                Namespace = ns,
                Usings = new List<string>(namespaces)
                {
                    "SaltLakeCity.Framework.Alpakabroker.Registry",
                    "System.Collections.Generic"
                },
                AlpakaEvents = alpakaEventModels.Select(x => x.EventName).ToList()
            };
        }
    }
}