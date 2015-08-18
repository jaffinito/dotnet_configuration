using System;
using System.CodeDom;
using System.Data;
using log4net;
using NewRelic.AgentConfiguration.Core.Parts;
using NewRelic.AgentConfiguration.Core.Singletons;
using NewRelic.AgentConfiguration.Core.Extensions;

namespace NewRelic.AgentConfiguration.Core.Processors
{
    public class MergeProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MergeProcessor));

        public void PrePopulateMergedFile(Element schemaFile, Element mergedFile)
        {
            PrePopulateMergedFileInternal(schemaFile, mergedFile, null);
        }

        public void Merge(Element config)
        {
            try
            {
                var schemaElement = DataFile.Instance.SchemaFile.GetElement(config);
                var mergeElement = DataFile.Instance.MergedFile.GetElement(config);

                if (mergeElement.Elements.Count == 0)
                {
                    //if (String.IsNullOrWhiteSpace(mergeElement.Value) && mergeElement.OnlyAttributeEmptyOrNull())
                    if (String.IsNullOrWhiteSpace(mergeElement.Value) && mergeElement.AllAttributesEmptyOrNull())
                    {
                        //if items is empty its a template, overwrite
                        mergeElement.Value = config.Value;
                        UpdateAttributeValues(config, mergeElement);
                    }
                    else
                    {
                        //creates the element from template and passes it the value from the config
                        mergeElement.Parent.Elements.Add(schemaElement.Clone(config.Value));

                        //creates the empty attributes from the template
                        foreach (Parts.Attribute attribute in schemaElement.Attributes)
                        {
                            mergeElement.Parent.Elements[mergeElement.Parent.Elements.Count - 1].Attributes.Add(
                                attribute.Clone());
                        }

                        //fils in the values for above
                        foreach (var attribute in config.Attributes)
                        {
                            mergeElement.Parent.Elements[mergeElement.Parent.Elements.Count - 1].FindAttribute(attribute)
                                .Value = attribute.Value;
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
            catch (Exception ex)
            {
                log.Debug(ex);
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

        public void SetMergedRootObject(Element mergeElement)
        {
            mergeElement.RootObject = "Merged";

            foreach (var attribute in mergeElement.Attributes)
            {
                attribute.RootObject = "Merged";
            }

            foreach (var childElement in mergeElement.Elements)
            {
                SetMergedRootObject(childElement);
            }
        }

        private void PrePopulateMergedFileInternal(Element schemaElement, Element mergeElement, Element mergeParent)
        {
            try
            {
                mergeElement.Parent = mergeParent;

                foreach (var attribute in schemaElement.Attributes)
                {
                    mergeElement.Attributes.Add(attribute.Clone());
                }

                foreach (var child in schemaElement.Elements)
                {
                    mergeElement.Elements.Add(child.Clone());
                    PrePopulateMergedFileInternal(child, mergeElement.FindElement(child.Name), mergeElement);
                }
            }
            catch (Exception ex)
            {
                log.Debug(ex);
            }
        }
    }
}
