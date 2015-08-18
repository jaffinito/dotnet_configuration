using System;
using System.Linq;
using System.Windows.Forms;
using NewRelic.AgentConfiguration.Core.Extensions;
using NewRelic.AgentConfiguration.Core.Parts;
using NewRelic.AgentConfiguration.Core.Singletons;

namespace NewRelic.AgentConfiguration.Core.Processors
{
    public class FormProcessor
    {
        public FormProcessor(Control root)
        {
            ReadControl(root);
        }

        public void ReadControl(Control control)
        {
            if(control.Tag != null)
            {
                GetData(control);
            }

            foreach(Control child in control.Controls)
            {
                ReadControl(child);
            }
        }

        private void GetData(Control control)
        {
            try
            {
                switch (control.GetType().ToString())
                {
                    case "System.Windows.Forms.RadioButton":
                        var rb = control as RadioButton;
                        if (rb.Checked)
                        {
                            (rb.Tag as IXsdPart).Value = rb.Text.ToLower();
                        }
                        break;
                    case "System.Windows.Forms.TextBox":
                        var tb = control as TextBox;
                        if (tb.Multiline)
                        {
                            var lines = tb.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                            var type = tb.Tag.GetType();
                            if (type.Name == "Element")
                            {
                                var path = (tb.Tag as Element).Path; //path for xsd templating
                                var parent = (tb.Tag as Element).Parent; //parent to re-add items
                                (tb.Tag as Element).Parent.RemoveAllChildElements((tb.Tag as Element).Name); //removes old elements by name

                                SetValueForMultiline(path, lines, parent);
                            }
                        }
                        else
                        {
                            (tb.Tag as IXsdPart).Value = tb.Text;
                        }
                        break;
                    case "System.Windows.Forms.ComboBox":
                        var cb = control as ComboBox;
                        (cb.Tag as IXsdPart).Value = cb.SelectedItem as string;
                        break;
                    case "System.Windows.Forms.Button":
                    case "System.Windows.Forms.FlowLayoutPanel":
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException nullref)
            {
                throw new ArgumentException();
            }
        }

        private static void SetValueForMultiline(string path, string[] lines, Element parent)
        {
            //This switch controls what is created for each type of multiline textbox.
            //The sections are pretty generic
            switch (path)
            {
                case "path.requestPathsExcluded.browserMonitoring.configuration":
                case "application.applications.instrumentation.configuration":
                    //this section handles elements with a single attribute that takes a string.
                    //element/attribute name not important.
                    foreach (var line in lines)
                    {
                        parent.Elements.Add(DataFile.Instance.SchemaFile.GetElement(path).Clone());
                        parent.Elements[parent.Elements.Count - 1].Attributes.Add(DataFile.Instance.SchemaFile.GetElement(path).Attributes[0].Clone());
                        parent.Elements[parent.Elements.Count - 1].Attributes[0].Value = line;
                        parent.Elements[parent.Elements.Count - 1].Attributes[0].RootObject = "Merged";
                        parent.Elements[parent.Elements.Count - 1].Attributes[0].Parent =
                            parent.Elements[parent.Elements.Count - 1];
                        parent.Elements[parent.Elements.Count - 1].RootObject = "Merged";
                        parent.Elements[parent.Elements.Count - 1].Parent = parent;
                    }
                    break;
                case "applicationPool.applicationPools.configuration":
                    //this section handles elements with two attributes
                    //pretty specific to teh case, but a good template for future items.
                    foreach (var line in lines)
                    {
                        string defaultBehavior = DataFile.Instance.MergedFile.GetElement("defaultBehavior.applicationPools.configuration")
                            .Attributes[0].Value == "true"
                            ? "false"
                            : "true"; //swapped on purpose

                        parent.Elements.Add(DataFile.Instance.SchemaFile.GetElement(path).Clone());

                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            //set name first
                            parent.Elements[parent.Elements.Count - 1].Attributes.Add(DataFile.Instance.SchemaFile.GetElement(path).Attributes[0].Clone());
                            parent.Elements[parent.Elements.Count - 1].Attributes[0].Value = line;
                            parent.Elements[parent.Elements.Count - 1].Attributes[0].RootObject = "Merged";
                            parent.Elements[parent.Elements.Count - 1].Attributes[0].Parent =
                                parent.Elements[parent.Elements.Count - 1];

                            //set instrument second
                            parent.Elements[parent.Elements.Count - 1].Attributes.Add(DataFile.Instance.SchemaFile.GetElement(path).Attributes[1].Clone());
                            parent.Elements[parent.Elements.Count - 1].Attributes[1].Value = defaultBehavior;
                            parent.Elements[parent.Elements.Count - 1].Attributes[1].RootObject = "Merged";
                            parent.Elements[parent.Elements.Count - 1].Attributes[1].Parent =
                                parent.Elements[parent.Elements.Count - 1];
                        }
                        //finish up
                        parent.Elements[parent.Elements.Count - 1].RootObject = "Merged";
                        parent.Elements[parent.Elements.Count - 1].Parent = parent;
                    }
                    break;


                default:
                    foreach (var line in lines)
                    {
                        parent.Elements.Add(DataFile.Instance.SchemaFile.GetElement(path).Clone(line));
                        parent.Elements[parent.Elements.Count - 1].RootObject = "Merged";
                        parent.Elements[parent.Elements.Count - 1].Parent = parent;
                    }
                    break;
            }
        }

        private int GetTrueCount(Element element)
        {
            int count = 0;
            foreach (var elementItem in element.Parent.Elements)
            {
                if (elementItem.Name == element.Name)
                {
                    count += 1;
                }
            }

            return count;
        }
    }
}
