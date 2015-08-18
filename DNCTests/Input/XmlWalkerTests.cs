using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.Input;

namespace DNCTests.Input
{
    [TestClass]
    public class XmlWalkerTests
    {
        [TestMethod]
        public void ProcessXml_Success()
        {
            XmlWalker walker = new XmlWalker();
            Assert.IsTrue(walker.ProcessXml(FileLocation.ConfigFile));
        }

        [TestMethod]
        public void ProcessXml_Failure()
        {
            XmlWalker walker = new XmlWalker();
            Assert.IsFalse(walker.ProcessXml(""));
        }
    }
}
