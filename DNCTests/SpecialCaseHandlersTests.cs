using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration;
using NewRelic.AgentConfiguration.Parts;

namespace DNCTests
{
    [TestClass]
    public class SpecialCaseHandlersTests
    {
        private Element TestPart(string kind)
        {
            switch(kind)
            {
                case "root":
                    {
                        Element element = new Element();
                        element.MaxOccurs = 1;
                        element.MinOccurs = 0;
                        element.Name = "configuration";
                        element.Parent = null;
                        element.RootElement = true;
                        element.SpecialLength = true;
                        element.Type = "DataType.String";
                        element.Value = null;
                        return element;
                    }
                case "service":
                    {
                        Element element = new Element();
                        element.MaxOccurs = 1;
                        element.MinOccurs = 0;
                        element.Name = "service";
                        element.Parent = TestPart("root");
                        element.RootElement = true;
                        element.SpecialLength = false;
                        element.Type = "DataType.String";
                        element.Value = null;
                        return element;
                    }
                case "proxy":
                    {
                        Element element = new Element();
                        element.MaxOccurs = 1;
                        element.MinOccurs = 0;
                        element.Name = "proxy";
                        element.Parent = TestPart("service");
                        element.RootElement = false;
                        element.SpecialLength = false;
                        element.Type = "DataType.String";
                        element.Value = null;
                        return element;
                    }
                case"user":
                    {
                        Element element = new Element();
                        element.MaxOccurs = 1;
                        element.MinOccurs = 0;
                        element.Name = "user";
                        element.Parent = TestPart("proxy");
                        element.RootElement = false;
                        element.SpecialLength = false;
                        element.Type = "DataType.String";
                        element.Value = "Stuff";
                        return element;
                    }
                case "true":
                    {
                        Element element = new Element();
                        element.MaxOccurs = 1;
                        element.MinOccurs = 0;
                        element.Name = "true";
                        element.Parent = TestPart("proxy");
                        element.RootElement = false;
                        element.SpecialLength = true;
                        element.Type = "DataType.String";
                        element.Value = "Stuff";
                        return element;
                    }
                case "debugAgent":
                    {
                        Element element = new Element();
                        element.MaxOccurs = 1;
                        element.MinOccurs = 0;
                        element.Name = "debugAgent";
                        element.Parent = TestPart("root");
                        element.RootElement = false;
                        element.SpecialLength = true;
                        element.Type = "DataType.String";
                        element.Value = "Stuff";
                        return element;
                    }
            }
            return null;
        }

        [TestMethod]
        public void SpecialLength_True()
        {
            Assert.IsTrue(SpecialCaseHandlers.SpecialLength(TestPart("true")));
        }

        [TestMethod]
        public void SpecialLength_TrueFromFalse()
        {
            Assert.IsTrue(SpecialCaseHandlers.SpecialLength(TestPart("user")));
        }

        [TestMethod]
        public void SpecialLength_False()
        {
            Assert.IsFalse(SpecialCaseHandlers.SpecialLength(TestPart("proxy")));
        }


        [TestMethod]
        public void RootElement_True()
        {
            Assert.IsTrue(SpecialCaseHandlers.RootElement(TestPart("service")));
        }

        [TestMethod]
        public void RootElement_False()
        {
            Assert.IsFalse(SpecialCaseHandlers.RootElement(TestPart("proxy")));
        }

        [TestMethod]
        public void IsAdvanced_True()
        {
            Assert.IsTrue(SpecialCaseHandlers.IsAdvanced(TestPart("debugAgent")));
        }

        [TestMethod]
        public void IsAdvanced_False()
        {
            Assert.IsFalse(SpecialCaseHandlers.IsAdvanced(TestPart("root")));
        }
    }
}
