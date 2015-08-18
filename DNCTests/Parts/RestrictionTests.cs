using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.Parts;

namespace DNCTests.Parts
{
    [TestClass]
    public class RestrictionTests
    {
        [TestMethod]
        public void Clone_SuccessNoValue()
        {
            Restriction restriction = new Restriction();
            restriction.Enumerations.Add("restrictionEnum");
            restriction.Enumerations.Add("restrictionEnum2");
            restriction.Enumerations.Add("restrictionEnum3");

            Restriction clone = restriction.Clone();

            if (restriction.Enumerations.Count != clone.Enumerations.Count)
            {
                Assert.Fail("Clone.Enumerations.Count does not match base.");
            }
            else
            {
                for (int i = 0; i < restriction.Enumerations.Count; i++ )
                {
                    if(restriction.Enumerations[i] != clone.Enumerations[i])
                    {
                        Assert.Fail("Clone.Enumeration: " + clone.Enumerations[i] + " does not match base of: " + restriction.Enumerations[i]  + ".");
                    }
                }
            }
        }
    }
}
