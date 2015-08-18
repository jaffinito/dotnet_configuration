using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration;
using System.Windows.Forms;
using NewRelic.AgentConfiguration.FormElements;
using NewRelic.AgentConfiguration.Parts;
using NewRelic.AgentConfiguration.Input;

namespace DNCTests
{
    [TestClass]
    public class UIBuilderTests
    {
        [TestMethod]
        public void Build_Sucessful()
        {
            XsdReader reader = new XsdReader();
            ConfigurationFile.Xsd = new Element();

            if (reader.ProcessSchema(FileLocation.XsdFile))
            {
                NewRelic.AgentConfiguration.MainForm.XsdPath = FileLocation.XsdFile;
            }

            XmlWalker xm = new XmlWalker();
            xm.ProcessXml(FileLocation.ConfigFile);
            NewRelic.AgentConfiguration.MainForm.ConfigPath = FileLocation.ConfigFile;;
            ConfigMerge cm = new ConfigMerge();

            FlowLayoutPanel panel = new FlowLayoutPanel();
            ToolTip mainFormToolTip = new ToolTip();

            UIBuilder ui = new UIBuilder(panel, mainFormToolTip);
            ui.Build(true);

            Assert.IsTrue(panel.Controls.Count > 0, panel.Controls.Count.ToString());
        }
    }
}
