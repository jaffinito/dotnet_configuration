using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.Input;

namespace DNCTests.Input
{
    [TestClass]
    public class XsdReaderTests
    {
        [TestMethod]
        public void ProcessSchema_Success()
        {
            XsdReader reader = new XsdReader();
            Assert.IsTrue(reader.ProcessSchema(FileLocation.XsdFile));
        }

        [TestMethod]
        public void ProcessSchema_EmptyPath()
        {
            XsdReader reader = new XsdReader();
            Assert.IsFalse(reader.ProcessSchema(""));
        }
    }
}
