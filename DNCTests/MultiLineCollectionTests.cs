using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration;
using NewRelic.AgentConfiguration.FormElements;
using NewRelic.AgentConfiguration.Parts;
using System.Windows.Forms;

namespace DNCTests
{
    [TestClass]
    public class MultiLineCollectionTests
    {
        private string _qualifiedName = "step1.step2.step3";
        private string _textBoxName = "tstep1";
        private Element TestElement
        {
            get
            {
                Element element = new Element();
                element.MaxOccurs = 1;
                element.MinOccurs = 0;
                element.Name = "step3";
                element.Parent = null;
                element.RootElement = true;
                element.SpecialLength = false;
                element.Type = "DataType.String";
                element.Value = null;
                element.Documentation = "step3d";

                Element childElement1 = new Element();
                childElement1.MaxOccurs = 1;
                childElement1.MinOccurs = 0;
                childElement1.Name = "step2";
                childElement1.RootElement = false;
                childElement1.SpecialLength = false;
                childElement1.Type = "DataType.String";
                childElement1.Value = "data2";
                childElement1.Parent = element;
                childElement1.RootElement = false;
                childElement1.Documentation = "step2d";
                element.Elements.Add(childElement1);

                Element childElement2 = new Element();
                childElement2.MaxOccurs = 1;
                childElement2.MinOccurs = 0;
                childElement2.Name = "step1";
                childElement2.RootElement = false;
                childElement2.SpecialLength = false;
                childElement2.Type = "DataType.String";
                childElement2.Value = "data1";
                childElement2.Parent = childElement1;
                childElement2.RootElement = false;
                childElement2.Documentation = "step1d";
                childElement1.Elements.Add(childElement2);

                return childElement2;
            }
        }

        private FlowLayoutPanel TestPanel
        {
            get
            {
                FlowLayoutPanel panel = Flows.Section(TestElement);
                TextBox textBox = Controls.TextBox(TestElement);
                panel.Controls.Add(textBox);
                return panel;
            }
        }

        [TestMethod]
        public void Add_NewItem()
        {
            MultiLineCollection.Add(_qualifiedName, _textBoxName, Flows.Section(TestElement));
            Assert.IsTrue(MultiLineCollection.Exists(_qualifiedName));
            MultiLineCollection.Clear();
        }

        //Should not add the item.
        [TestMethod]
        public void Add_PreExistItem()
        {
            MultiLineCollection.Add(_qualifiedName, _textBoxName, TestPanel);
            MultiLineCollection.Add(_qualifiedName, _textBoxName + "_A", TestPanel);
            Assert.AreEqual(MultiLineCollection.GetTextBox(_qualifiedName).Name, _textBoxName);
            MultiLineCollection.Clear();
        }

        [TestMethod]
        public void Clear_ConfirmNothing()
        {
            MultiLineCollection.Add(_qualifiedName, _textBoxName, TestPanel);
            MultiLineCollection.Clear();
            Assert.IsFalse(MultiLineCollection.Exists(_qualifiedName));
            MultiLineCollection.Clear();
        }
    }
}
