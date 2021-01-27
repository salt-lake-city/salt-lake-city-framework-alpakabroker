using System.Collections.Generic;
using SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Templates
{
    public class AlpakaEventReceiverBaseModel
    {
        public string EventName { get; set; }

        public string Namespace { get; set; }
        
        public List<string> Usings { get; set; } = new();
        public static AlpakaEventReceiverBaseModel From(AlpakaEventModel model)
        {
            return new()
            {
                EventName = model.EventName,
                Namespace = model.Namespace,
                Usings = new List<string>(model.Usings)
                {
                    "SaltLakeCity.Framework.Alpakabroker.Abstractions.EventReceiver"
                }
            };
        }
    }
}