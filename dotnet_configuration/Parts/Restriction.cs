using System.Collections.Generic;

namespace NewRelic.AgentConfiguration.Parts
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
