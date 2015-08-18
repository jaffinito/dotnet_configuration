using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.Input;

namespace DNCTests.Input
{
    [TestClass]
    public class FileLocationTests
    {
        [TestMethod]
        public void XsdFile_Success()
        {
            if (System.IO.File.Exists(@"C:\ProgramData\New Relic\.NET Agent\newrelic.xsd"))
            {
                Assert.IsTrue(@"C:\ProgramData\New Relic\.NET Agent\newrelic.xsd" == FileLocation.XsdFile);
            }
        }

        [TestMethod]
        public void ConfigFile_Success()
        {
            if (System.IO.File.Exists(@"C:\ProgramData\New Relic\.NET Agent\newrelic.config"))
            {
                Assert.IsTrue(@"C:\ProgramData\New Relic\.NET Agent\newrelic.config" == FileLocation.ConfigFile);
            }
        }
    }
}
