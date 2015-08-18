
namespace NewRelic.AgentConfiguration.Core.Parts
{
    public interface IXsdPart
    {
        string Name {get; set;}
        string Documentation { get; set; }
        string Value { get; set; }
        bool SpecialLength { get; set; }
        bool IsAdvanced { get; set; }
        Element Parent { get; set; }
        string RootObject { get; set; }
    }
}
