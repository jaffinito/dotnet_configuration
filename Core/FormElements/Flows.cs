using System;
using System.Windows.Forms;
using log4net;
using NewRelic.AgentConfiguration.Core.Parts;

namespace NewRelic.AgentConfiguration.Core.FormElements
{
    public static class Flows
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Flows));

        public static FlowLayoutPanel RadioButtonFlow(IXsdPart part, string defaultSelected = "", ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name))
                {
                    throw new ArgumentNullException();
                }
                var panel = Container(part, tooltip);
                panel.SuspendLayout();

                var container = new FlowLayoutPanel();
                container.SuspendLayout();
                container.FlowDirection = FlowDirection.LeftToRight;
                container.AutoSize = true;
                container.Font = Methods.BaseFont;
                container.Name = "container" + part.Name;
                container.Size = new System.Drawing.Size(208, 53);

                //true false
                string selected = !String.IsNullOrWhiteSpace(part.Value) ? part.Value : defaultSelected;

                container.Controls.Add(Controls.RadioButton(part, "True", selected, tooltip));
                container.Controls.Add(Controls.RadioButton(part, "False", selected, tooltip));
                container.ResumeLayout();
                panel.Controls.Add(container);
                panel.ResumeLayout();
                return panel;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static FlowLayoutPanel TextBoxFlow(IXsdPart part, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name))
                {
                    throw new ArgumentNullException();
                }
                var panel = Container(part);
                panel.SuspendLayout();
                panel.Controls.Add(Controls.TextBox(part, tooltip));
                panel.ResumeLayout();
                return panel;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static FlowLayoutPanel MultiLineTextBoxFlow(Element element, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(element.Name))
                {
                    throw new ArgumentNullException();
                }
                var panel = Container(element);
                panel.SuspendLayout();
                panel.Controls.Add(Controls.MultiLineTextBox(element, tooltip));
                panel.ResumeLayout();
                return panel;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static FlowLayoutPanel NumericFlow(IXsdPart part, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name))
                {
                    throw new ArgumentNullException();
                }
                var panel = Container(part);
                panel.SuspendLayout();
                panel.Controls.Add(Controls.Numeric(part, tooltip));
                panel.ResumeLayout();
                return panel;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static FlowLayoutPanel ComboBoxFlow(Parts.Attribute attribute, string[] items, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(attribute.Name))
                {
                    throw new ArgumentNullException();
                }
                var panel = Container(attribute);
                panel.SuspendLayout();
                panel.Controls.Add(Controls.ComboBox(attribute, items, tooltip));
                panel.ResumeLayout();
                return panel;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static FlowLayoutPanel Container(IXsdPart part, ToolTip tooltip = null)
        {
            var panel = new FlowLayoutPanel();
            panel.SuspendLayout();
            panel.FlowDirection = FlowDirection.TopDown;
            panel.AutoSize = true;
            panel.Font = Methods.BaseFont;
            panel.Name = "p" + part.Name;
            panel.Size = new System.Drawing.Size(208, 53);
            panel.AutoSize = true;

            //panel.BorderStyle = BorderStyle.Fixed3D;
            panel.BorderStyle = BorderStyle.None;

            panel.Controls.Add(Controls.Label(part, tooltip));
            panel.ResumeLayout();
            return panel;
        }

        public static FlowLayoutPanel Section(Element element, ToolTip tooltip = null)
        {

            var panel = new FlowLayoutPanel();
            panel.SuspendLayout();
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.AutoSize = true;
            panel.Font = Methods.BaseFont;
            panel.Name = "s" + element.Name;
            panel.Tag = element;

            //panel.BorderStyle = BorderStyle.Fixed3D;
            panel.BorderStyle = BorderStyle.None;
            if (element.Parent !=null && element.Parent.Name != "configuration")
            {
                panel.BackColor = System.Drawing.Color.FromArgb(238, 238, 238);
            }

            panel.Size = new System.Drawing.Size(208, 53);
            panel.AutoSize = true;
            panel.Location = new System.Drawing.Point(0, 24);
            if (tooltip != null)
            {
                tooltip.SetToolTip(panel, element.Documentation);
            }

            if (!element.RootElement)
            {
                panel.Controls.Add(Controls.Label(element, tooltip));
            }
            else
            {
                // hide the root to create the menu selections...
                panel.Visible = false;
            }
            panel.ResumeLayout();
            return panel;
        }
    }
}