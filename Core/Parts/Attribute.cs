
namespace NewRelic.AgentConfiguration.Core.Parts
{
    public class Attribute:IXsdPart
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public string Use { get; set; }
        public string Documentation { get; set; }
        public Restriction Restriction;
        public string Value { get; set; }
        public bool SpecialLength { get; set; }
        public Element Parent { get; set; }
        public bool IsAdvanced { get; set; }
        public string RootObject { get; set; }

        public Attribute()
        {
            Restriction = new Restriction();
            SpecialLength = false;
        }

        public Attribute Clone()
        {
            var childAttribute = new Attribute
            {
                Default = Default,
                Documentation = Documentation,
                Name = Name,
                Parent = Parent,
                SpecialLength = SpecialLength,
                Type = Type,
                Use = Use,
                Restriction = this.Restriction.Clone(),
                IsAdvanced = this.IsAdvanced,
                RootObject = this.RootObject
            };

            return childAttribute;
        }
    }
}
