using System;
using System.Windows.Forms;
using NewRelic.AgentConfiguration.Core;
using NewRelic.AgentConfiguration.Core.Singletons;
using NewRelic.AgentConfiguration.Core.Parts;
using FormElements = NewRelic.AgentConfiguration.Core.FormElements;
using Attribute = NewRelic.AgentConfiguration.Core.Parts.Attribute;

namespace NewRelic.AgentConfiguration.UserInterface
{

    /// <summary>
    /// Builds the UI objects and places them in their FlwoLayoutPanels
    /// </summary>
    public class UIBuilder
    {
        public FlowLayoutPanel ConfigPanel;
        private ToolTip _toolTipObj;
        private bool _showAdvanced;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flow">The main FlowLayoutPanel to house the selections.</param>
        /// <param name="tooltip">The tooltip to attach to each FormElement.</param>
        public UIBuilder(FlowLayoutPanel flow, ToolTip tooltip)
        {
            ConfigPanel = flow;
            _toolTipObj = tooltip;
            _showAdvanced = false;
        }

        /// <summary>
        /// Starts the build process. Calls the recursive methods. Adds the header item. Finally, validates the form items.
        /// </summary>
        /// <param name="showAdvanced">Controls whether or not to show Advanced items.</param>
        public void Build(bool showAdvanced)
        {
            _showAdvanced = showAdvanced;

            ConfigPanel.SuspendLayout();

            var label = new Element();
            label.Name = "HeaderLabel";
            label.Documentation ="Please select a section at the right to begin.";
            label.Parent = null;
            ConfigPanel.Controls.Add(FormElements.Controls.Label(label));

            var flow = FormElements.Flows.Section(DataFile.Instance.MergedFile, _toolTipObj);

            BuildAttributes(DataFile.Instance.MergedFile, flow);
            ConfigPanel.Controls.Add(flow);

            BuildElement(DataFile.Instance.MergedFile, ConfigPanel);
            ConfigPanel.ResumeLayout();

            //run validation after form is ready via a recursive function
            RunValidation(ConfigPanel);
        }

        /// <summary>
        /// Recursive method that confirms the contents of the textboxes and marks them as bad if needed.
        /// </summary>
        /// <param name="control"></param>
        private void RunValidation(Control control)
        {
            var textBox = control as TextBox;
            if (textBox != null && textBox.Multiline)
            {
                textBox.CausesValidation = false;
                textBox.CausesValidation = true;
            }
            else
            {
                foreach(Control child in control.Controls)
                {
                    RunValidation(child);
                }
            }
        }

        /// <summary>
        /// Recursuve method that takes the Element parts and builds them into sections, textboxes, or multiline textboxes.
        /// </summary>
        /// <param name="element">Element to build out.</param>
        /// <param name="parentFlow">FlowLayoutPanel to place the resulting form control into.</param>
        private void BuildElement(Element element, FlowLayoutPanel parentFlow)
        {
            foreach (var elementItem in element.Elements)
            {
                //if(elementItem.IsAdvanced && !_showAdvanced)
                //{
                //    continue;
                //}

                if (elementItem.Name == "labels")
                {
                    int sdfs = 0;
                }

                if (elementItem.MaxOccurs <= 1)
                {
                    var flow = FormElements.Flows.Section(elementItem, _toolTipObj);

                    //deal with elements that have text, will error and return null if no text.  This is good.

                    flow.Controls.Add(FormElements.Controls.TextBox(elementItem, _toolTipObj));
                    BuildAttributes(elementItem, flow);
                    BuildElement(elementItem, flow);

                    //works barely, but need MUCH polish...
                    //_toolTipObj.SetToolTip(flow, elementItem.Documentation);

                    parentFlow.Controls.Add(flow);
                }
                
                if (elementItem.MaxOccurs > 1)
                {
                    if(MultiLineCollection.Instance.Exists(QualifiedName(elementItem)))
                    {
                        if (elementItem.Attributes.Count > 0)
                        {
                            MultiLineCollection.Instance.GetTextBox(QualifiedName(elementItem)).Text += Environment.NewLine + elementItem.Attributes[0].Value;
                        }
                        else
                        {
                            MultiLineCollection.Instance.GetTextBox(QualifiedName(elementItem)).Text += Environment.NewLine + elementItem.Value;
                        }
                    }
                    else
                    {
                        if (elementItem.Attributes.Count > 0)
                        {
                            parentFlow.Controls.Add(FormElements.Flows.MultiLineTextBoxFlow(elementItem, _toolTipObj));
                            MultiLineCollection.Instance.Add(QualifiedName(elementItem), "mlt" + elementItem.Name, parentFlow);
                            MultiLineCollection.Instance.GetTextBox(QualifiedName(elementItem)).Text += elementItem.Attributes[0].Value;
                        }
                        else
                        {
                            parentFlow.Controls.Add(FormElements.Flows.MultiLineTextBoxFlow(elementItem, _toolTipObj));
                            MultiLineCollection.Instance.Add(QualifiedName(elementItem), "mlt" + elementItem.Name, parentFlow);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Builds the qualified name for an element.
        /// </summary>
        /// <param name="elementItem">Element to work from.</param>
        /// <returns>Qualified name as a string.</returns>
        private static string QualifiedName(Element elementItem)
        {
            return elementItem.Name + "." + elementItem.Parent.Name + "." + elementItem.Parent.Parent.Name;
        }

        /// <summary>
        /// Methods to build out the attributes into form controls./
        /// </summary>
        /// <param name="element">Element to work from.</param>
        /// <param name="flow">FlowLayoutPanel to place the resulting form control into.</param>
        private void BuildAttributes(Element element, FlowLayoutPanel flow)
        {
            foreach (Attribute attribute in element.Attributes)
            {
                if (attribute.IsAdvanced && !_showAdvanced)
                {
                    continue;
                }

                switch (attribute.Type)
                {
                    case "boolean":
                        flow.Controls.Add(FormElements.Flows.RadioButtonFlow(attribute, attribute.Default, _toolTipObj));
                        break;
                    case "unsignedInt":
                    case "float":
                    case "int":
                        flow.Controls.Add(FormElements.Flows.NumericFlow(attribute, _toolTipObj));
                        break;
                    case "string":
                        flow.Controls.Add(FormElements.Flows.TextBoxFlow(attribute, _toolTipObj));
                        break;
                    case "":
                        string[] array = attribute.Restriction.Enumerations.ToArray();
                        flow.Controls.Add(FormElements.Flows.ComboBoxFlow(attribute, array, _toolTipObj));
                        break;
                    case "hidden":
                        break;
                    default:
                        throw new ArgumentException();
                }

                //SetToolTip(flow, element.Documentation, attribute.Documentation);
            }
        }

        //also sorta works.  format is good but not hitting the flow's children
        /// <summary>
        /// Sets the tooltip for tooltip for a form control.
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        private void SetToolTip(FlowLayoutPanel flow, string element = "", string attribute = "")
        {
            string tip = "";

            if(!String.IsNullOrWhiteSpace(element))
            {
                tip = element;
            }

            if (!String.IsNullOrWhiteSpace(attribute))
            { 
                if(!String.IsNullOrWhiteSpace(tip))
                {
                    tip += Environment.NewLine + Environment.NewLine + attribute;
                }
                else
                {
                    tip = attribute;
                }
            }

            _toolTipObj.SetToolTip(flow, tip);
        }
    }
}
