using NewRelic.AgentConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNCTests
{
    [TestClass]
    public class AgentDebugTests
    {
        [TestMethod]
        public void GetDebugLevel_ReturnInfo()
        {
            //AgentDebug debug = new AgentDebug();
            //AgentDebug.LogLevel level = debug.GetLogLevel();
            //Assert.IsNotNull(level);
        }

        [TestMethod]
        public void SetDebugLevel_SetDebug()
        {
            AgentDebug debug = new AgentDebug();
            AgentDebug.LogLevel infolevel = AgentDebug.LogLevel.info;
            AgentDebug.LogLevel offLevel = AgentDebug.LogLevel.off;
            debug.SetLogLevel(offLevel);
            Assert.AreEqual(offLevel, debug.GetLogLevel());
            debug.SetLogLevel(infolevel);
            Assert.AreNotEqual(offLevel, debug.GetLogLevel());
        }
    }
}
