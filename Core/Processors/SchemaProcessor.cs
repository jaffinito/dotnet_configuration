using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using log4net;
using NewRelic.AgentConfiguration.Core.Parts;

namespace NewRelic.AgentConfiguration.Core.Processors
{
    public class SchemaProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SchemaProcessor));

        public Element ProcessSchema(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            var schemaSet = new XmlSchemaSet();
            schemaSet.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);
            schemaSet.Add("urn:newrelic-config", filePath);
            schemaSet.Compile();

            XmlSchema schema = null;
            foreach (XmlSchema tempSchema in schemaSet.Schemas())
            {
                schema = tempSchema;
            }

            var configurationElement = schema.Items[0] as XmlSchemaElement;

            return ProcessElement(configurationElement, null);
        }

        private Element ProcessElement(XmlSchemaElement schemaElement, Element parent)
        {
            var element = new Element
            {
                Name = schemaElement.Name,
                Type = schemaElement.ElementType == null ? "" : schemaElement.ElementType.GetType().Name,
                MinOccurs = schemaElement.MinOccurs,
                MaxOccurs = FixMaxOccurs(schemaElement, parent == null ? "" : parent.Name),
                Documentation = GetDocumentation(schemaElement.Annotation, schemaElement.Name),
                RootObject = "Xsd",
                RootElement = SpecialCaseHandlers.RootElement(parent)
            };

            element.IsAdvanced = SpecialCaseHandlers.IsAdvanced(element, parent);
            element.SpecialLength = SpecialCaseHandlers.SpecialLength(element, parent);
            element.Parent = parent ?? FillOutRootElement(element);

            if (schemaElement.SchemaType != null)
            {
                ProcessChildren(schemaElement, element);
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

            //will break if moved about the Process* calls.
            //Need access to parent for those functions to get errorCollector.attributes.* working
            //FillOutRootElement(parent, element);

            //XmlSchemaType schemaType = configurationElement.SchemaType;
            //XmlSchemaComplexType schemaComplexType = schemaType as XmlSchemaComplexType;
            //XmlSchemaSimpleType schemaSimpleType = schemaType as XmlSchemaSimpleType;
        }

        private void ProcessChildren(XmlSchemaElement schemaElement, Element element)
        {
            if (schemaElement.SchemaType.GetType().Name == "XmlSchemaComplexType")
            {
                var schemaComplexType = schemaElement.SchemaType as XmlSchemaComplexType;

                if (String.IsNullOrWhiteSpace(element.Documentation))
                {
                    element.Documentation = GetDocumentation(schemaComplexType.Annotation);
                }

                if (schemaComplexType.AttributeUses.Count > 0)
                {
                    ProcessAttributes(schemaComplexType, element);
                }

                ProcessElementAll(schemaElement, element);

                ProcessElementSequence(schemaComplexType.Particle as XmlSchemaSequence, element);
            }
        }

        private static Element FillOutRootElement(Element element)
        {
            try
            {
                var attribute = new Parts.Attribute
                {
                    Name = "xmlns",
                    Type = "hidden",
                    Default = "urn:newrelic-config",
                    Parent = element,
                    Value = "urn:newrelic-config",
                    RootObject = "Xsd"
                };
                element.Documentation = "These are the basic settings that govern how the .Net Agent behaves. The key setting is agentEnabled.";
                element.RootElement = true;
                element.Attributes.Add(attribute);
            }
            catch (Exception ex)
            {
                log.Debug(ex);
            }
            return null;
        }

        private static decimal FixMaxOccurs(XmlSchemaElement schemaElement, string parentName)
        {
            if (schemaElement.Name == "name" && parentName == "application")
            {
                return 3;
            }
            else
            {
                return schemaElement.MaxOccurs;
            }
        }

        private string GetDocumentation(XmlSchemaAnnotation annotation, string elementName = "")
        {
            string documentation = "";
            if (annotation != null)
            {
                foreach (var schemaObject in annotation.Items)
                {
                    var item = (XmlSchemaDocumentation)schemaObject;
                    documentation = CleanAnnotation(item);
                }
            }

            documentation += SpecialCaseHandlers.EnhancedDocumentation(elementName);
            return documentation;
        }

        //private void ProcessElementSequence(XmlSchemaElement schemaElement, Element element)
        private void ProcessElementSequence(XmlSchemaSequence sequenceElement, Element element)
        {
            //XmlSchemaSequence sequenceElement = (schemaElement.SchemaType as XmlSchemaComplexType).Particle as XmlSchemaSequence;
            if (sequenceElement != null)
            {
                for (int i = 0; i < sequenceElement.Items.Count; i++)
                {
                    var childElement = sequenceElement.Items[i] as XmlSchemaElement;
                    var innerSequence = sequenceElement.Items[i] as XmlSchemaSequence;
                    //XmlSchemaAll innerAll = sequence.Items[i] as XmlSchemaAll;

                    if (childElement != null)
                    {
                        if (childElement.MinOccurs == 1 && childElement.MaxOccurs == 1)
                        {
                            childElement.MinOccurs = sequenceElement.MinOccurs;
                            childElement.MaxOccurs = sequenceElement.MaxOccurs;
                        }

                        ProcessElement(childElement, element);
                    }
                    else if (innerSequence != null)
                    {
                        ProcessElementSequence(innerSequence, element);
                    }
                }
            }
        }

        private void ProcessElementAll(XmlSchemaElement schemaElement, Element element)
        {
            var all = (schemaElement.SchemaType as XmlSchemaComplexType).Particle as XmlSchemaAll;
            if (all != null)
            {
                foreach (var schemaObject in all.Items)
                {
                    var childElement = schemaObject as XmlSchemaElement;
                    //XmlSchemaSequence innerSequence = all.Items[i] as XmlSchemaSequence;
                    //XmlSchemaAll innerAll = all.Items[i] as XmlSchemaAll;

                    if (childElement != null)
                    {
                        ProcessElement(childElement, element);
                    }
                    //else OutputElements(all.Items[i] as XmlSchemaParticle);
                }
            }
        }

        private void ProcessAttributes(XmlSchemaComplexType schemaComplexType, Element element)
        {
            var enumerator = schemaComplexType.AttributeUses.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var xmlAttribute = (XmlSchemaAttribute)enumerator.Value;
                var attribute = new Parts.Attribute
                {
                    Name = xmlAttribute.Name,
                    Type = xmlAttribute.SchemaTypeName.Name,
                    Default = xmlAttribute.DefaultValue ?? "",
                    Use = xmlAttribute.Use.ToString(),
                    Documentation = GetDocumentation(xmlAttribute.Annotation),
                    Parent = element,
                };
                attribute.IsAdvanced = SpecialCaseHandlers.IsAdvanced(attribute, element);
                attribute.SpecialLength = SpecialCaseHandlers.SpecialLength(attribute, element);
                attribute.RootObject = "Xsd";

                if (xmlAttribute.AttributeSchemaType.Content.GetType().ToString() == "System.Xml.Schema.XmlSchemaSimpleTypeRestriction")
                {
                    var xmlRestrication = xmlAttribute.AttributeSchemaType.Content as XmlSchemaSimpleTypeRestriction;
                    if (xmlRestrication != null)
                    {
                        var restriction = new Restriction();

                        foreach (var facet in xmlRestrication.Facets.Cast<XmlSchemaEnumerationFacet>())
                        {
                            restriction.Enumerations.Add(facet.Value);
                        }

                        attribute.Restriction = restriction;
                    }
                }

                element.Attributes.Add(attribute);
            }
        }

        private static string CleanAnnotation(XmlSchemaDocumentation item)
        {
            var text = "";
            text = item.Markup[0].InnerText.Substring(0, 1).Contains("\n") ? item.Markup[0].InnerText.Remove(0, 2) : item.Markup[0].InnerText;
            text = text.Replace("\n                  ", " ");
            text = text.Replace("\n                 ", " ");
            text = text.Replace("\n                ", " ");
            text = text.Replace("\n               ", " ");
            text = text.Replace("\n              ", " ");
            text = text.Replace("\n             ", " ");
            text = text.Replace("\n            ", " ");
            text = text.Replace("\n           ", " ");
            text = text.Replace("\n          ", " ");
            text = text.Replace("\n         ", " ");
            text = text.Replace("\n        ", " ");
            text = text.Replace("\n       ", " ");
            text = text.Replace("\n      ", " ");
            text = text.Replace("\n     ", " ");
            text = text.Replace("\n    ", " ");
            text = text.Replace("\n   ", " ");
            text = text.Replace("\n  ", " ");
            text = text.Replace("\n ", " ");
            text = text.Replace("\n", " ").Trim();
            text = text.Replace("'", "''");
            text = text.Replace("\t", "");
            return text;
        }

        private static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
            {
                //MessageBox.Show("WARNING: " + args.Message);
            }
            else if (args.Severity == XmlSeverityType.Error)
            {
                // MessageBox.Show("ERROR: " + args.Message);
            }
        }
    }
}
