using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NewRelic.AgentConfiguration.Core.Singletons;
using NewRelic.AgentConfiguration.Core.Parts;
using Attribute = NewRelic.AgentConfiguration.Core.Parts.Attribute;

namespace NewRelic.AgentConfiguration.IO
{
    public class ConfigWriter
    {
        //public string Content { get; set; }
        public XElement XRoot { get; set; }
        private XNamespace _xmlns = "urn:newrelic-config";
        private string _filepath;

        public ConfigWriter(string filePath = "")
        {
            _filepath = filePath; 
            //http://msdn.microsoft.com/en-us/library/bb387089.aspx
            XRoot = Build(DataFile.Instance.MergedFile, new XElement(_xmlns + DataFile.Instance.MergedFile.Name));
            //Content = XRoot.ToString();
        }

        public XElement Build(Element element, XElement parentXElement)
        {
            //IsNull stuff is broken for things that are empty in the middle.
            //Example: name.application.configuration

            var isNull = true;
            foreach (var attribute in element.Attributes.Where(NotNullOrHiddenOrDefaultValue))
            {
                parentXElement.Add(new XAttribute(attribute.Name, attribute.Value));
                isNull = false;
            }

            isNull = element.Elements.Where(HasAttributesWithValuesAndOwnValueAndChildrenAndMustOccur).Aggregate(isNull, (current, elementItem) => AddElement(parentXElement, current, elementItem));

            if (isNull && String.IsNullOrWhiteSpace(element.Value) && element.MinOccurs == 0)
            {
                return null;
            }

            return parentXElement;
        }

        private bool HasAttributesWithValuesAndOwnValueAndChildrenAndMustOccur(Element elementItem)
        {
            return !EmptyAttributes(elementItem.Attributes) || !String.IsNullOrWhiteSpace(elementItem.Value) || elementItem.Elements.Count > 0 || elementItem.MinOccurs > 0;
        }

        private bool AddElement(XElement parentXElement, bool isNull, Element elementItem)
        {
            //This seems to work now.
            var element = Build(elementItem, new XElement(_xmlns + elementItem.Name, elementItem.Value));
            if (elementItem.MinOccurs <= 0 && element == null) return isNull;
            parentXElement.Add(element);
            return false;
            
           
            /*
            if (String.IsNullOrWhiteSpace(elementItem.Value))
            {
                if (Build(elementItem, new XElement(_xmlns + elementItem.Name)) != null || elementItem.MinOccurs > 0)
                {
                    parentXElement.Add(Build(elementItem, new XElement(_xmlns + elementItem.Name)));
                    IsNull = false;
                }
            }
            else
            {
                if (Build(elementItem, new XElement(_xmlns + elementItem.Name)) != null)
                {
                    parentXElement.Add(Build(elementItem, new XElement(_xmlns + elementItem.Name, elementItem.Value)));
                    IsNull = false;
                }
            }
            return IsNull;
            */
        }

        private static bool NotNullOrHiddenOrDefaultValue(Attribute attribute)
        {
            return !String.IsNullOrWhiteSpace(attribute.Value) && attribute.Type != "hidden" && attribute.Value != attribute.Default;
        }

        public void Write(string newPath = null)
        {
            //<!-- Copyright (c) 2008-2014 New Relic, Inc.  All rights reserved. -->
            //<!-- For more information see: https://newrelic.com/docs/dotnet/dotnet-agent-configuration -->
            //XRoot.Document.Add(new XComment("Copyright (c) 2008-2014 New Relic, Inc.  All rights reserved."));
            //XRoot.Document.Add(new XComment("For more information see: https://newrelic.com/docs/dotnet/dotnet-agent-configuration"));

            if (XRoot != null)
            {
                var settings = new XmlWriterSettings {Indent = true};

                var path = newPath ?? _filepath;

                var writer = XmlWriter.Create(path, settings);
                WriteDocument(writer);
            }
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            if (XRoot == null) return output.ToString();
            var settings = new XmlWriterSettings {Indent = true};
            var writer = XmlWriter.Create(output, settings);

            WriteDocument(writer);
            return output.ToString();
        }

        private void WriteDocument(XmlWriter writer)
        {
            var Copyright = new XComment("Copyright (c) 2008-2015 New Relic, Inc.  All rights reserved.");
            Copyright.WriteTo(writer);

            var MoreInfo = new XComment("For more information see: https://newrelic.com/docs/dotnet/dotnet-agent-configuration");
            MoreInfo.WriteTo(writer);

            var CreatedBy = new XComment("Created or modified by the New Relic .Net Configuration Tool.");
            CreatedBy.WriteTo(writer);

            XRoot.WriteTo(writer);
            writer.Flush();
            writer.Close();
        }

        private static bool EmptyAttributes(List<Attribute> attributes)
        {
            return attributes.Count <= 0 || attributes.All(attribute => String.IsNullOrWhiteSpace(attribute.Value));
        }
    }
}