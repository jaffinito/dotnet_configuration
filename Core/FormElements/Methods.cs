using System;
using System.ComponentModel;
using System.Windows.Forms;
using NewRelic.AgentConfiguration.Core.Parts;
using NewRelic.AgentConfiguration.Core.Singletons;

namespace NewRelic.AgentConfiguration.Core.FormElements
{
    public static class Methods
    {
        public static System.Drawing.Font BaseFont = new System.Drawing.Font("Verdana", 9.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public static System.Drawing.Font BoldFont = new System.Drawing.Font("Verdana", 9.00F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //error color 255, 195,194

        public static void MultilineTextBox_Validating(object sender, CancelEventArgs e)
        {
            MultilineTextBoxValidator(sender);
        }

        public static void MultilineTextBox_CausesValidation(object sender, EventArgs e)
        {
            MultilineTextBoxValidator(sender);
        }

        public static void MultilineTextBox_KeyPress(object sender, KeyEventArgs e)
        {
            MultilineTextBoxValidator(sender);
        }

        public static void MultilineTextBoxValidator(object sender)
        {
            var textBox = sender as TextBox;
            var element = textBox.Tag as Element;
            var count = Convert.ToDecimal(textBox.Lines.Length);
            var maxCount = element.MaxOccurs;

            if (count > maxCount && !textBox.Parent.Controls.ContainsKey("OccursError"))
            {
                textBox.BackColor = System.Drawing.Color.FromArgb(255, 195,194);
                textBox.Parent.Controls.Add(Controls.OccursLabel(maxCount));
            }
            else if (count <= maxCount && textBox.Parent.Controls.ContainsKey("OccursError"))
            {
                textBox.BackColor = System.Drawing.Color.White;
                textBox.Parent.Controls.RemoveByKey("OccursError");
            }
        }

        public static void MenuButton_MouseEnter(object sender, EventArgs e)
        {
            var menuButton = sender as Button;
            if (CurrentSection.Instance.Section.Contains(menuButton.Text) || String.IsNullOrWhiteSpace(CurrentSection.Instance.Section))
            {
                menuButton.ForeColor = System.Drawing.Color.FromArgb(47, 136, 154);
            }
        }

        public static void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            var menuButton = sender as Button;
            menuButton.ForeColor = System.Drawing.Color.Black;
        }

        public static void MenuButton_Click(object sender, EventArgs e)
        {
            var menuButton = sender as Button;
            Form mainForm = menuButton.FindForm();

            //menu
            var flow2Controls = mainForm.Controls.Find("MenuBarFlow", false);
            var flow2 = flow2Controls[0] as FlowLayoutPanel;
            foreach (Control control in flow2.Controls)
            {
                control.BackColor = System.Drawing.Color.FromArgb(234, 234, 234);
                control.Font = BaseFont;
            }
            menuButton.BackColor = System.Drawing.Color.White;
            menuButton.ForeColor = System.Drawing.Color.Black;
            menuButton.Font = BoldFont;

            //work section
            //(mainForm as MainForm).CurrentSection = "s" + menuButton.Text;
            CurrentSection.Instance.Section = "s" + menuButton.Text;

            var flowControls = mainForm.Controls.Find("ContentFlow", false);
            var flow = flowControls[0] as FlowLayoutPanel;

            var headerLabels = flow.Controls.Find("lHeaderLabel", false);
            var headerLabel = headerLabels[0] as Label;

            headerLabel.Text = (menuButton.Tag as Element).Documentation;

            foreach (Control control in flow.Controls)
            {
                control.Visible = false;
            }

            var controls = flow.Controls.Find("s" + menuButton.Text, false);
            controls[0].Visible = true;
            if (String.IsNullOrWhiteSpace(headerLabel.Text))
            {
                headerLabel.Visible = false;
            }
            else
            {
                headerLabel.Visible = true;
            }
        }

        public static int Width(string text)
        {
            var verifiedText = String.IsNullOrWhiteSpace(text) ? "" : text;
            return verifiedText.Length * 8 >= 60 ? verifiedText.Length * 8 : 60;
        }

        public static int ButtonWidth(string text)
        {
            return text.Length * 12;
        }

        public static bool StandAloneElement(IXsdPart part)
        {
            if((part as Parts.Attribute) != null)
            {
                return false;
            }

            Parts.Element element = part as Parts.Element;
            if(element.Elements.Count ==0 && element.Type.Contains("string"))
            {
                return true;
            }
            
            return false;
        }
    }
}
