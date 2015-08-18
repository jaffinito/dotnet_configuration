using NewRelic.AgentConfiguration.Parts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace NewRelic.AgentConfiguration.Output
{
    public class ConfigWriter
    {
        //public string Content { get; set; }
        public XElement XRoot { get; set; }
        private XNamespace _xmlns = "urn:newrelic-config";

        public ConfigWriter()
        {
            //http://msdn.microsoft.com/en-us/library/bb387089.aspx
            XRoot = Build(ConfigurationFile.Merged, new XElement(_xmlns + ConfigurationFile.Merged.Name));
            //Content = XRoot.ToString();
        }

        public XElement Build(Element element, XElement parentXElement)
        {
            //IsNull stuff is broken for things that are empty in the middle.
            //Example: name.application.configuration

            var IsNull = true;
            foreach (var attribute in element.Attributes)
            {
                if (NotNullOrHiddenOrDefaultValue(attribute))
                {
                    parentXElement.Add(new XAttribute(attribute.Name, attribute.Value));
                    IsNull = false;
                }
            }

            foreach(Element elementItem in element.Elements)
            {
                if (HasAttributesWithValuesAndOwnValueAndChildrenAndMustOccur(elementItem))
                {
                    IsNull = AddElement(parentXElement, IsNull, elementItem);
                }
            }

            if (IsNull && String.IsNullOrWhiteSpace(element.Value) && element.MinOccurs == 0)
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
            bool returnNull = isNull;
            if (elementItem.MinOccurs > 0 || element != null)
            {
                parentXElement.Add(element);
                returnNull = false;
            }
            return returnNull;
            
           
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

        private bool NotNullOrHiddenOrDefaultValue(Parts.Attribute attribute)
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
                var settings = new XmlWriterSettings();
                settings.Indent = true;

                string path = null;
                if(newPath == null)
                {
                    path = MainForm.ConfigPath;
                }
                else
                {
                    path = newPath;
                }

                var writer = XmlWriter.Create(path, settings);
                WriteDocument(writer);
            }
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            if (XRoot != null)
            {
                var settings = new XmlWriterSettings();
                settings.Indent = true;
                var writer = XmlWriter.Create(output, settings);

                WriteDocument(writer);
            }
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

        private bool EmptyAttributes(List<Parts.Attribute> attributes)
        {
            if (attributes.Count > 0)
            {
                foreach (var attribute in attributes)
                {
                    if(!String.IsNullOrWhiteSpace(attribute.Value))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}