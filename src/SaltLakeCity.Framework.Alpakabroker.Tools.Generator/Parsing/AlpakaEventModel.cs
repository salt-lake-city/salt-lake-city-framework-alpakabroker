using System.Collections.Generic;

namespace SaltLakeCity.Framework.Alpakabroker.Tools.Generator.Parsing
{
    public class AlpakaEventModel
    {
        public string EventName { get; set; }

        public string Namespace { get; set; }
        
        public List<string> Usings { get; set; } = new List<string>();
    }
}
