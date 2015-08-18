using NewRelic.AgentConfiguration.Parts;
using System;
using System.Xml;
using System.Xml.Linq;

namespace NewRelic.AgentConfiguration.Input
{
    public class XmlWalker
    {
        public bool ProcessXml(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                return false;
            }

            XmlReader reader = XmlReader.Create(filePath);
            XElement startingXElement = XElement.Load(filePath);
            ConfigurationFile.Config = ProcessXElement(startingXElement, null);
            reader.Close();
            return true;
        }

        private Element ProcessXElement(XElement xElement, Element parent)
        {
            var element = new Element();
            element.Name = xElement.Name.LocalName;
            element.Parent = parent;
            element.RootObject = "Config";
            if(!xElement.HasElements && !xElement.IsEmpty)
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
            else
            {
                return element;
            }
            
        }

        private void ProcessXAttributes(XAttribute xAttribute, Element parent)
        {
            var attribute = new Parts.Attribute();
            attribute.Name = xAttribute.Name.LocalName;
            attribute.Value = xAttribute.Value;
            attribute.Parent = parent;
            attribute.RootObject = "Config";
            parent.Attributes.Add(attribute);
        }
    }
}
