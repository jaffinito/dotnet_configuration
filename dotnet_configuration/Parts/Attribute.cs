
namespace NewRelic.AgentConfiguration.Parts
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
            var childAttribute = new Attribute();
            childAttribute.Default = Default;
            childAttribute.Documentation = Documentation;
            childAttribute.Name = Name;
            childAttribute.Parent = Parent;
            childAttribute.SpecialLength = SpecialLength;
            childAttribute.Type = Type;
            childAttribute.Use = Use;
            childAttribute.Restriction = this.Restriction.Clone();
            childAttribute.IsAdvanced = this.IsAdvanced;
            childAttribute.RootObject = this.RootObject;

            return childAttribute;
        }
    }
}
