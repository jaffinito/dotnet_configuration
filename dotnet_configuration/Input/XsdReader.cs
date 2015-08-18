using NewRelic.AgentConfiguration.Parts;
using System;
using System.Collections;
using System.Xml.Schema;

namespace NewRelic.AgentConfiguration.Input
{
    public class XsdReader
    {
        public bool ProcessSchema(string filePath)
        {
            if(String.IsNullOrWhiteSpace(filePath))
            {
                return false;
            }

            var schemaSet = new XmlSchemaSet();
            schemaSet.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);
            schemaSet.Add("urn:newrelic-config", filePath);
            schemaSet.Compile();

            XmlSchema schema = null;
            foreach (XmlSchema TempSchema in schemaSet.Schemas())
            {
                schema = TempSchema;
            }

            var configurationElement = schema.Items[0] as XmlSchemaElement;
            if (configurationElement != null)
            {
                ProcessElement(configurationElement, null);
                ConfigurationFile.Xsd.RootElement = true;
            }

            return true;
        }

        private void ProcessElement(XmlSchemaElement schemaElement, Element parent)
        {
            var parentName = parent == null ? "" : parent.Name;
            var element = new Element();
            element.Name = schemaElement.Name;
            element.Type = schemaElement.ElementType != null ? schemaElement.ElementType.GetType().Name : "";
            element.MinOccurs = schemaElement.MinOccurs;
            element.MaxOccurs = FixMaxOccurs(schemaElement, parentName);
            element.Documentation = GetDocumentation(schemaElement.Annotation, element.Name);
            element.RootObject = "Xsd";

            SetupParent(parent, element);
            element.IsAdvanced = SpecialCaseHandlers.IsAdvanced(element);

            if (schemaElement.SchemaType != null)
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

            //will break if moved about the Process* calls.
            //Need access to parent for those functions to get errorCollector.attributes.* working
            //SetupParent(parent, element);

            //XmlSchemaType schemaType = configurationElement.SchemaType;
            //XmlSchemaComplexType schemaComplexType = schemaType as XmlSchemaComplexType;
            //XmlSchemaSimpleType schemaSimpleType = schemaType as XmlSchemaSimpleType;

            
        }

        private static void SetupParent(Element parent, Element element)
        {
            try
            {
                if (parent != null)
                {
                    element.Parent = parent;
                    element.SpecialLength = SpecialCaseHandlers.SpecialLength(element);
                    element.RootElement = SpecialCaseHandlers.RootElement(element);
                    parent.Elements.Add(element);
                }
                else
                {
                    ConfigurationFile.Xsd = element;
                    //Add the required namespace
                    //xmlns="urn:newrelic-config"
                    var attribute = new Parts.Attribute();
                    attribute.Name = "xmlns";
                    attribute.Type = "hidden";
                    attribute.Default = "urn:newrelic-config";
                    attribute.Parent = element;
                    attribute.Value = "urn:newrelic-config";
                    attribute.RootObject = "Xsd";

                    ConfigurationFile.Xsd.Documentation = "These are the basic settings that govern how the .Net Agent behaves. The key setting is agentEnabled.";

                    ConfigurationFile.Xsd.Attributes.Add(attribute);

                }
            }
            catch(Exception ex)
            {
                
            }
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
                foreach (XmlSchemaDocumentation item in annotation.Items)
                {
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
                for (int i = 0; i < all.Items.Count; i++)
                {
                    var childElement = all.Items[i] as XmlSchemaElement;
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

                var attribute = new Parts.Attribute();
                attribute.Name = xmlAttribute.Name;
                attribute.Type = xmlAttribute.SchemaTypeName.Name;
                attribute.Default = xmlAttribute.DefaultValue == null ? "" : xmlAttribute.DefaultValue;
                attribute.Use = xmlAttribute.Use.ToString();
                attribute.Documentation = GetDocumentation(xmlAttribute.Annotation);

                attribute.Parent = element;
                attribute.IsAdvanced = SpecialCaseHandlers.IsAdvanced(attribute);
                attribute.RootObject = "Xsd";

                if(xmlAttribute.AttributeSchemaType.Content.GetType().ToString() == "System.Xml.Schema.XmlSchemaSimpleTypeRestriction")
                {
                    var xmlRestrication = xmlAttribute.AttributeSchemaType.Content as XmlSchemaSimpleTypeRestriction;
                    if (xmlRestrication != null)
                    {
                        var restriction = new Restriction();

                        foreach (XmlSchemaEnumerationFacet facet in xmlRestrication.Facets)
                        {
                            restriction.Enumerations.Add(facet.Value);
                        }

                        attribute.Restriction = restriction;
                    }
                }

                attribute.SpecialLength = SpecialCaseHandlers.SpecialLength(attribute);
                element.Attributes.Add(attribute);
            }
        }

        private static string CleanAnnotation(XmlSchemaDocumentation item)
        {

            var text = "";
            if(item.Markup[0].InnerText.Substring(0,1).Contains("\n"))
            {
                text = item.Markup[0].InnerText.Remove(0, 2);
            }
            else
            {
                text = item.Markup[0].InnerText;
            }
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
            text = text.Replace("\t","");

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
