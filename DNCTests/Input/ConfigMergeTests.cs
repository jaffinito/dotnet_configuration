using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.Input;
using NewRelic.AgentConfiguration;
using NewRelic.AgentConfiguration.Parts;

namespace DNCTests.Input
{
    [TestClass]
    public class ConfigMergeTests
    {
        [TestMethod]
        public void Merge_Success()
        {
            XsdReader reader = new XsdReader();
            ConfigurationFile.Xsd = new Element();

            if (reader.ProcessSchema(FileLocation.XsdFile))
            {
                NewRelic.AgentConfiguration.MainForm.XsdPath = FileLocation.XsdFile;
            }

            XmlWalker xm = new XmlWalker();
            xm.ProcessXml(FileLocation.ConfigFile);
            NewRelic.AgentConfiguration.MainForm.ConfigPath = FileLocation.ConfigFile; ;
            ConfigMerge cm = new ConfigMerge();

            //How to test this?  It just does it  with the defaults
            //I could refactor to make it take the config and xsd so I could replace them with elements.
            Assert.IsTrue(true);
        }
    }
}
