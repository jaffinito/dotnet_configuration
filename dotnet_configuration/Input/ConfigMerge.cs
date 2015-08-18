using NewRelic.AgentConfiguration.Parts;
using System;

namespace NewRelic.AgentConfiguration.Input
{
    public class ConfigMerge
    {
        public ConfigMerge()
        {
            ConfigurationFile.Merged = ConfigurationFile.Xsd.Clone();
            PreFormatMerge(ConfigurationFile.Xsd, ConfigurationFile.Merged, null);

            Merge(ConfigurationFile.Config);
            SetMergedRootObject(ConfigurationFile.Merged);
        }

        private void Merge(Element config)
        {
            var xsdElement = ConfigurationFile.FindElementByPath(config.Path, ConfigurationFile.Xsd);
            var mergeElement = ConfigurationFile.FindElementByPath(config.Path, ConfigurationFile.Merged);

            if(mergeElement.Elements.Count == 0)
            {
                //if (String.IsNullOrWhiteSpace(mergeElement.Value) && mergeElement.OnlyAttributeEmptyOrNull())
                if(String.IsNullOrWhiteSpace(mergeElement.Value) && mergeElement.AllAttributesEmptyOrNull())
                {
                    //if items is empty its a template, overwrite
                    mergeElement.Value = config.Value;
                    UpdateAttributeValues(config, mergeElement);
                }
                else
                {
                    //creates the element from template and passes it the value from the config
                    mergeElement.Parent.Elements.Add(xsdElement.Clone(config.Value));

                    //creates the empty attributes from the template
                    foreach (Parts.Attribute attribute in xsdElement.Attributes)
                    {
                        mergeElement.Parent.Elements[mergeElement.Parent.Elements.Count - 1].Attributes.Add(attribute.Clone());
                    }

                    //fils in the values for above
                    foreach (var attribute in config.Attributes)
                    {
                        mergeElement.Parent.Elements[mergeElement.Parent.Elements.Count - 1].FindAttribute(attribute).Value = attribute.Value;
                    }
                }
            }
            else
            {
                UpdateAttributeValues(config, mergeElement);
            }

            foreach (var child in config.Elements)
            {
                Merge(child);
            }
        }

        private void UpdateAttributeValues(Element config, Element mergeElement)
        {
            // do not add elements, no element.value needed, just attributes are updated.
            foreach (var attribute in config.Attributes)
            {
                mergeElement.FindAttribute(attribute).Value = attribute.Value;
            }
        }

        private void SetMergedRootObject(Element element)
        {
            element.RootObject = "Merged";
            foreach (var attribute in element.Attributes)
            {
                attribute.RootObject = "Merged";
            }
            foreach (var childElement in element.Elements)
            {
                SetMergedRootObject(childElement);
            }
        }

        private void PreFormatMerge(Element xsdElement, Element mergeElement, Element mergeParent)
        {
            mergeElement.Parent = mergeParent;

            foreach (var attribute in xsdElement.Attributes)
            {
                mergeElement.Attributes.Add(attribute.Clone());
            }

            foreach (var child in xsdElement.Elements)
            {
                mergeElement.Elements.Add(child.Clone());
                PreFormatMerge(child, mergeElement.FindElement(child.Name), mergeElement);
            }
        }
    }
}
