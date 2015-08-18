using System;
using System.Windows.Forms;
using log4net;
using NewRelic.AgentConfiguration.Core.Parts;

namespace NewRelic.AgentConfiguration.Core.FormElements
{
    public static class Controls
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Controls));

        public static Label OccursLabel(decimal maxCount)
        {
            try
            {
                var label = new Label();
                label.SuspendLayout();
                label.ForeColor = System.Drawing.Color.Black;
                label.AutoSize = true;
                label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                label.Font = Methods.BaseFont;
                label.BackColor = System.Drawing.Color.FromArgb(255, 195, 194);
                label.Size = new System.Drawing.Size(41, 13);
                label.Text = "Maximum of entries " + maxCount + " allowed";
                label.Name = "OccursError";
                label.Margin = new Padding(0, 0, 0, 0);
                label.Visible = true;
                label.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                label.Location = new System.Drawing.Point(0, 0);
                label.ResumeLayout();
                return label;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static Label Label(IXsdPart part, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name))
                {
                    throw new ArgumentNullException();
                }
                var label = new Label();
                label.SuspendLayout();
                label.ForeColor = System.Drawing.Color.Black;
                label.AutoSize = true;
                label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                label.Font = Methods.BaseFont;
                label.Size = new System.Drawing.Size(41, 13);
                label.Text = part.Name;
                label.Name = "l" + part.Name;

                if (tooltip != null)
                {
                    tooltip.SetToolTip(label, part.Documentation);
                }

                if (part.Name == "HeaderLabel")
                {
                    label.Visible = false;
                    label.Margin = new Padding(0, 0, 0, 10);
                }

                label.ResumeLayout();
                return label;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static Button MenuButton(IXsdPart part, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name))
                {
                    throw new ArgumentNullException();
                }
                var button = new Button();
                button.SuspendLayout();
                //button.AutoSize = true;
                button.BackColor = System.Drawing.Color.FromArgb(234, 234, 234);
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
                button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                button.FlatStyle = FlatStyle.Flat;
                button.Font = Methods.BaseFont;
                button.UseVisualStyleBackColor = false;
                button.Size = new System.Drawing.Size(152, 23);
                button.Margin = new Padding(0, 0, 0, 0);
                button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                button.Name = "b" + part.Name;
                button.Text = part.Name;
                button.Tag = part;

                if (tooltip != null)
                {
                    tooltip.SetToolTip(button, part.Documentation);
                }

                button.Click += Methods.MenuButton_Click;
                button.MouseEnter += Methods.MenuButton_MouseEnter;
                button.MouseLeave += Methods.MenuButton_MouseLeave;

                button.ResumeLayout();
                return button;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static ComboBox ComboBox(Parts.Attribute attribute, string[] items, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(attribute.Name) || items.Length == 0)
                {
                    throw new ArgumentNullException();
                }
                var comboBox = new ComboBox();
                comboBox.SuspendLayout();
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.ForeColor = System.Drawing.Color.Black;
                comboBox.FlatStyle = FlatStyle.Flat;
                comboBox.Font = Methods.BaseFont;
                comboBox.FormattingEnabled = true;
                comboBox.Name = "cb" + attribute.Name;
                comboBox.Size = new System.Drawing.Size(121, 21);
                comboBox.Items.AddRange(items);
                comboBox.Tag = attribute;



                if (String.IsNullOrWhiteSpace(attribute.Value))
                {
                    comboBox.SelectedIndex = comboBox.Items.IndexOf(attribute.Default);
                }
                else
                {
                    comboBox.SelectedIndex = comboBox.Items.IndexOf(attribute.Value);
                }

                if (tooltip != null)
                {
                    tooltip.SetToolTip(comboBox, attribute.Documentation);
                }

                comboBox.ResumeLayout();
                return comboBox;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static RadioButton RadioButton(IXsdPart part, string text, string selected, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name) || String.IsNullOrWhiteSpace(text))
                {
                    throw new ArgumentNullException();
                }
                var radioButton = new RadioButton();
                radioButton.SuspendLayout();
                radioButton.ForeColor = System.Drawing.Color.Black;
                radioButton.AutoSize = true;
                radioButton.Font = Methods.BaseFont;
                radioButton.UseVisualStyleBackColor = true;
                radioButton.Size = new System.Drawing.Size(98, 17);
                radioButton.TabStop = true;
                radioButton.Text = text;
                radioButton.Name = "rb" + part.Name + text;
                radioButton.Tag = part;

                if (selected == text.ToLower())
                {
                    radioButton.Checked = true;
                }

                if (tooltip != null)
                {
                    tooltip.SetToolTip(radioButton, part.Documentation);
                }

                radioButton.ResumeLayout();
                return radioButton;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static TextBox TextBox(IXsdPart part, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name))
                {
                    throw new ArgumentNullException();
                }
                var textBox = new TextBox();
                textBox.SuspendLayout();
                textBox.BackColor = System.Drawing.Color.White;
                textBox.ForeColor = System.Drawing.Color.Black;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = Methods.BaseFont;
                textBox.Name = "t" + part.Name;

                if (String.IsNullOrWhiteSpace(part.Value) && !Methods.StandAloneElement(part))
                {
                    textBox.Text = (part as Parts.Attribute).Default;
                }
                else
                {
                    textBox.Text = part.Value;
                }

                //Some textboxes will be empty but need to contain long strings.
                if (part.SpecialLength)
                {
                    textBox.Size = new System.Drawing.Size(200, 21);
                }
                else
                {
                    textBox.Size = new System.Drawing.Size(Methods.Width(textBox.Text), 21);
                }

                if (tooltip != null)
                {
                    tooltip.SetToolTip(textBox, part.Documentation);
                }

                textBox.Tag = part;

                textBox.ResumeLayout();
                return textBox;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static TextBox MultiLineTextBox(Element element, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(element.Name))
                {
                    throw new ArgumentNullException();
                }
                var textBox = new TextBox();
                textBox.SuspendLayout();
                textBox.BackColor = System.Drawing.Color.White;
                textBox.ForeColor = System.Drawing.Color.Black;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = Methods.BaseFont;
                textBox.Name = "mlt" + element.Name;
                textBox.Multiline = true;
                textBox.ScrollBars = ScrollBars.Both;
                textBox.WordWrap = false;
                textBox.Validating += Methods.MultilineTextBox_Validating;
                textBox.KeyUp += Methods.MultilineTextBox_KeyPress;
                textBox.CausesValidationChanged += Methods.MultilineTextBox_CausesValidation;

                if (String.IsNullOrWhiteSpace(element.Value))
                {
                    //textBox.Text = (part as Attribute).Default;
                }
                else
                {
                    textBox.Text = element.Value;
                }

                textBox.Size = new System.Drawing.Size(300, 100);

                if (tooltip != null)
                {
                    tooltip.SetToolTip(textBox, element.Documentation);
                }

                textBox.Tag = element;



                textBox.ResumeLayout();
                return textBox;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }

        public static TextBox Numeric(IXsdPart part, ToolTip tooltip = null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(part.Name))
                {
                    throw new ArgumentNullException();
                }
                var numeric = new TextBox();
                numeric.SuspendLayout();
                numeric.ForeColor = System.Drawing.Color.Black;
                numeric.BorderStyle = BorderStyle.FixedSingle;
                numeric.Font = Methods.BaseFont;
                numeric.Size = new System.Drawing.Size(60, 21);
                //numeric.Minimum = 0;
                //numeric.Maximum = 65000;
                numeric.Name = "n" + part.Name;
                numeric.Text = String.IsNullOrWhiteSpace(part.Value) ? "" : part.Value;
                numeric.Tag = part;
                numeric.KeyPress += SpecialCaseHandlers.NumericNumberFilter;
                numeric.ResumeLayout();

                if (tooltip != null)
                {
                    tooltip.SetToolTip(numeric, part.Documentation);
                }

                return numeric;
            }
            catch (Exception ex)
            {
                log.Debug(ex);
                return null;
            }
        }
    }
}
