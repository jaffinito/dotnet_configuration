using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.FormElements;
using NewRelic.AgentConfiguration.Parts;

namespace DNCTests.FormElements
{
    [TestClass]
    public class MethodsTests
    {
        [TestMethod]
        public void Width_Empty()
        {
            Assert.IsTrue(60 == Methods.Width(""));
        }

        [TestMethod]
        public void Width_LessThan60()
        {
            Assert.IsTrue(60 == Methods.Width("1"));
        }

        [TestMethod]
        public void Width_MoreThan60()
        {
            Assert.IsTrue(80 == Methods.Width("1234567890"));
        }

        [TestMethod]
        public void ButtonWidth_Success()
        {
            Assert.IsTrue(12 == Methods.ButtonWidth("1"));
        }

        [TestMethod]
        public void StandAloneElement_Attribute()
        {
            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            Assert.IsFalse(Methods.StandAloneElement(attribute));
        }

        [TestMethod]
        public void StandAloneElement_ElementWithCount()
        {
            Element element = new Element();
            element.Elements.Add(new Element());
            Assert.IsFalse(Methods.StandAloneElement(element));
        }

        [TestMethod]
        public void StandAloneElement_ElementNoCountNotString()
        {
            Element element = new Element();
            element.Type = "int";
            Assert.IsFalse(Methods.StandAloneElement(element));
        }

        [TestMethod]
        public void StandAloneElement_ElementNoCountWithString()
        {
            Element element = new Element();
            element.Type = "string";
            Assert.IsTrue(Methods.StandAloneElement(element));
        }
    }
}
