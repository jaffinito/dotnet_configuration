using System.Collections.Generic;
using System.Linq;

namespace NewRelic.AgentConfiguration.Core.Parts
{
    public class Restriction
    {
        public List<string> Enumerations;

        public Restriction()
        {
            Enumerations = new List<string>();
        }

        public Restriction Clone()
        {
            var restriction = new Restriction();
            foreach (var item in Enumerations)
            {
                restriction.Enumerations.Add(item);
            }

            return restriction;
        }
    }
}
