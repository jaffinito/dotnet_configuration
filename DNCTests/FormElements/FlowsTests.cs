using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.FormElements;
using NewRelic.AgentConfiguration.Parts;
using System;

namespace DNCTests.FormElements
{
    [TestClass]
    public class FlowsTests
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
        public void RadioButtonFlow_Success()
        {
            Assert.IsTrue(Flows.RadioButtonFlow(_element).Name == "p" + _element.Name);
            Assert.IsTrue(Flows.RadioButtonFlow(_element).Controls[0].Name == "l" + _element.Name);
            Assert.IsTrue(Flows.RadioButtonFlow(_element).Controls[1].Controls.Count == 2);
            Assert.IsTrue(Flows.RadioButtonFlow(_element).Controls[1].Controls[1].Name == "rb" + _element.Name + "False");
        }

        [TestMethod]
        public void TextBoxFlow_Success()
        {
            Assert.IsTrue(Flows.TextBoxFlow(_element).Name == "p" + _element.Name);
            Assert.IsTrue(Flows.TextBoxFlow(_element).Controls[0].Name == "l" + _element.Name);
            Assert.IsTrue(Flows.TextBoxFlow(_element).Controls[1].Name == "t" + _element.Name);
        }

        [TestMethod]
        public void MultiLineTextBoxFlow_Success()
        {
            Assert.IsTrue(Flows.MultiLineTextBoxFlow(_element).Name == "p" + _element.Name);
            Assert.IsTrue(Flows.MultiLineTextBoxFlow(_element).Controls[0].Name == "l" + _element.Name);
            Assert.IsTrue(Flows.MultiLineTextBoxFlow(_element).Controls[1].Name == "mlt" + _element.Name);
        }

        [TestMethod]
        public void NumericFlow_Success()
        {
            Assert.IsTrue(Flows.NumericFlow(_element).Name == "p" + _element.Name);
            Assert.IsTrue(Flows.NumericFlow(_element).Controls[0].Name == "l" + _element.Name);
            Assert.IsTrue(Flows.NumericFlow(_element).Controls[1].Name == "n" + _element.Name);
        }

        [TestMethod]
        public void ComboBoxFlow_Success()
        {
            string[] array = { "one", "two", "three" };
            Assert.IsTrue(Flows.ComboBoxFlow(_attribute, array).Name == "p" + _attribute.Name);
            Assert.IsTrue(Flows.ComboBoxFlow(_attribute, array).Controls[0].Name == "l" + _attribute.Name);
            Assert.IsTrue(Flows.ComboBoxFlow(_attribute, array).Controls[1].Name == "cb" + _attribute.Name);
        }

        [TestMethod]
        public void Container_Success()
        {
            Assert.IsTrue(Flows.Container(_element).Name == "p" + _element.Name);
            Assert.IsTrue(Flows.Container(_element).Controls[0].Name == "l" + _element.Name);
            Assert.IsTrue(Flows.Container(_element).FlowDirection == System.Windows.Forms.FlowDirection.TopDown);
            Assert.IsTrue(Flows.Container(_element).Font.Equals(Methods.BaseFont));
        }

        [TestMethod]
        public void Section_Success()
        {
            Assert.IsTrue(Flows.Section(_element).Name == "s" + _element.Name);
            Assert.IsTrue(Flows.Section(_element).Font.Equals(Methods.BaseFont));
            Assert.IsTrue(Flows.Section(_element).FlowDirection == System.Windows.Forms.FlowDirection.LeftToRight);
            
        }
    }
}
