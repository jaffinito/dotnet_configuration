using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.FormElements;
using NewRelic.AgentConfiguration.Parts;
using System.Windows.Forms;

namespace DNCTests.FormElements
{
    [TestClass]
    public class ControlsTests
    {
        private Element _element
        {
            get
            {
                Element element = new Element();
                element.Name = "testLabel";
                element.Value = "testValue";
                return element;
            }
        }

        private NewRelic.AgentConfiguration.Parts.Attribute _attribute
        {
            get
            {
                NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
                attribute.Name = "testAttribute";
                attribute.Default = "testDefault";
                return attribute;
            }
        }

        [TestMethod]
        public void OccursLabel_Success()
        {
            Assert.IsTrue("OccursError" == Controls.OccursLabel(2).Name);
            Assert.IsTrue(Controls.OccursLabel(2).Text.Contains("2"));
        }

        [TestMethod]
        public void Label_Success()
        {
            Assert.IsTrue(Controls.Label(_element).Name == "l" + _element.Name);
            Assert.IsTrue(Controls.Label(_element).Text == _element.Name);
        }

        [TestMethod]
        public void MenuButton_Success()
        {
            Assert.IsTrue(Controls.MenuButton(_element).Name == "b" + _element.Name);
            Assert.IsTrue(Controls.MenuButton(_element).Text == _element.Name);
        }

        [TestMethod]
        public void ComboBox_Success()
        {
            string[] array = { "one", "two", "three" };
            Assert.IsTrue(Controls.ComboBox(_attribute, array).Name == "cb" + _attribute.Name);
            Assert.IsTrue(Controls.ComboBox(_attribute, array).Items.Count == 3);
            Assert.IsTrue(Controls.ComboBox(_attribute, array).Items[1].ToString() == array[1]);
        }

        [TestMethod]
        public void RadioButton_Success()
        {
            Assert.IsTrue(Controls.RadioButton(_element, "one","one").Name == "rb" + _element.Name + "one");
            Assert.IsTrue(Controls.RadioButton(_element, "one", "one").Checked);
            Assert.IsFalse(Controls.RadioButton(_element, "one", "two").Checked);
        }

        [TestMethod]
        public void TextBox_Success()
        {
            Assert.IsTrue(Controls.TextBox(_element).Name == "t" + _element.Name);
            Assert.IsTrue(Controls.TextBox(_element).Text == _element.Value);

            Assert.IsTrue(Controls.TextBox(_attribute).Name == "t" + _attribute.Name);
            Assert.IsTrue(Controls.TextBox(_attribute).Text == _attribute.Default);
        }

        [TestMethod]
        public void MultiLineTextBox_Success()
        {
            Assert.IsTrue(Controls.MultiLineTextBox(_element).Name == "mlt" + _element.Name);
            Assert.IsTrue(Controls.MultiLineTextBox(_element).Text == _element.Value);
        }

        [TestMethod]
        public void Numeric_Success()
        {
            Assert.IsTrue(Controls.Numeric(_element).Name == "n" + _element.Name);
            Assert.IsTrue(Controls.Numeric(_element).Text == _element.Value);
        }
    }
}
