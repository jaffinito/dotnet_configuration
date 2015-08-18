using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration;
using NewRelic.AgentConfiguration.Parts;

namespace DNCTests
{
    [TestClass]
    public class ConfigurationFileTests
    {
        private Element TestElement
        {
            get
            {
                Element element = new Element();
                element.MaxOccurs = 1;
                element.MinOccurs = 0;
                element.Name = "step1";
                element.Parent = null;
                element.RootElement = true;
                element.SpecialLength = false;
                element.Type = "DataType.String";
                element.Value = null;

                for(int i = 2;i<4;i++)
                {
                    Element childElement = new Element();
                    childElement.MaxOccurs = 1;
                    childElement.MinOccurs = 0;
                    childElement.Name = "step" + i.ToString();
                    childElement.Parent = null;
                    childElement.RootElement = true;
                    childElement.SpecialLength = false;
                    childElement.Type = "DataType.String";
                    childElement.Value = "data" + i.ToString();
                    childElement.Parent = element;
                    childElement.RootElement = false;
                    element.Elements.Add(childElement);
                }

                return element;
            }
        }

        [TestMethod]
        public void FineElementByPath_Exists()
        {
            Element element = ConfigurationFile.FindElementByPath("step2.step1", TestElement);
            Assert.AreEqual(element.Name, "step2");

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FineElementByPath_NotExists()
        {
            Element element = ConfigurationFile.FindElementByPath("step0.step1", TestElement);
            Assert.IsNull(element, element.Name);
        }
    }
}
