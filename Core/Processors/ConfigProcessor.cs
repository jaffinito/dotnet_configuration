using System;
using System.Xml.Linq;
using log4net;
using NewRelic.AgentConfiguration.Core.Parts;

namespace NewRelic.AgentConfiguration.Core.Processors
{
    public class ConfigProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigProcessor));

        public Element ProcessConfig(string filePath)
        {
            return ProcessXElement(Load(filePath), null);
        }

        private Element ProcessXElement(XElement xElement, Element parent)
        {
            var element = new Element {Name = xElement.Name.LocalName, Parent = parent, RootObject = "Config"};
            if (!xElement.HasElements && !xElement.IsEmpty)
            {
                element.Value = xElement.Value;
            }

            foreach (var attribute in xElement.Attributes())
            {
                ProcessXAttributes(attribute, element);
            }

            foreach (var childxElement in xElement.Elements())
            {
                ProcessXElement(childxElement, element);
            }

            if (parent != null)
            {
                parent.Elements.Add(element);
                return null;
            }

            return element;
        }

        private void ProcessXAttributes(XAttribute xAttribute, Element parent)
        {
            var attribute = new Parts.Attribute
            {
                Name = xAttribute.Name.LocalName,
                Value = xAttribute.Value,
                Parent = parent,
                RootObject = "Config"
            };
            parent.Attributes.Add(attribute);
        }

        private XElement Load(string filePath)
        {
            try
            {
                return XElement.Load(filePath);
            }
            catch (Exception exception)
            {
                log.Debug(exception);
                return null;
            }
        }
    }
}
