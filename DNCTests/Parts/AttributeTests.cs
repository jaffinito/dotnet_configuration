using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.Parts;

namespace DNCTests.Parts
{
    [TestClass]
    public class AttributeTests
    {
        [TestMethod]
        public void Clone_SuccessNoValue()
        {
            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Default = "baseDefault";
            attribute.Use = "baseUse";
            attribute.Name = "baseAttribute";
            attribute.Parent = null;

            Restriction restriction = new Restriction();
            restriction.Enumerations.Add("restrictionEnum");

            attribute.Restriction = restriction;
            attribute.SpecialLength = false;
            attribute.Type = "DataType.String";
            attribute.Value = "baseAttribute";
            attribute.Documentation = "baseAttribute";
            attribute.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute clone = attribute.Clone();

            if (attribute.Default != clone.Default)
            {
                Assert.Fail("Clone.Default does not match base.");
            }

            if (attribute.Use != clone.Use)
            {
                Assert.Fail("Clone.Use does not match base.");
            }

            if (attribute.Name != clone.Name)
            {
                Assert.Fail("Clone.Name does not match base.");
            }

            if (attribute.Parent != null)
            {
                if (attribute.Parent.Name != clone.Parent.Name)
                {
                    Assert.Fail("Clone.Parent does not match base.");
                }
            }
            else
            {
                if (clone.Parent != null)
                {
                    Assert.Fail("Clone.Parent not null, base is null");
                }
            }

            if (attribute.Restriction.Enumerations.Count != clone.Restriction.Enumerations.Count)
            {
                Assert.Fail("Clone.Restriction.Enumerations.Count does not match base.");
            }

            if (attribute.SpecialLength != clone.SpecialLength)
            {
                Assert.Fail("Clone.SpecialLength does not match base.");
            }

            if (attribute.Type != clone.Type)
            {
                Assert.Fail("Clone.Type does not match base.");
            }

            if (attribute.Documentation != clone.Documentation)
            {
                Assert.Fail("Clone.Documentation does not match base.");
            }

            if (attribute.IsAdvanced != clone.IsAdvanced)
            {
                Assert.Fail("Clone.IsAdvanced does not match base.");
            }

            if (attribute.Value == clone.Value)
            {
                Assert.Fail("Clone.Value matches base.");
            }
        }
    }
}
