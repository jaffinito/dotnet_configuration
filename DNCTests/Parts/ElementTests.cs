using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelic.AgentConfiguration.Parts;

namespace DNCTests.Parts
{
    [TestClass]
    public class ElementTests
    {
        [TestMethod]
        public void Clone_SuccessNoValue()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element clone = element.Clone();

            if(element.MaxOccurs != clone.MaxOccurs)
            {
                Assert.Fail("Clone.MaxOccurs does not match base.");
            }

            if (element.MinOccurs != clone.MinOccurs)
            {
                Assert.Fail("Clone.MinOccurs does not match base.");
            }

            if (element.Name != clone.Name)
            {
                Assert.Fail("Clone.Name does not match base.");
            }

            if (element.Parent != null)
            {
                if (element.Parent.Name != clone.Parent.Name)
                {
                    Assert.Fail("Clone.Parent does not match base.");
                }
            }
            else
            {
                if(clone.Parent != null)
                {
                    Assert.Fail("Clone.Parent not null, base is null");
                }
            }

            if (element.RootElement != clone.RootElement)
            {
                Assert.Fail("Clone.RootElement does not match base.");
            }

            if (element.SpecialLength != clone.SpecialLength)
            {
                Assert.Fail("Clone.SpecialLength does not match base.");
            }

            if (element.Type != clone.Type)
            {
                Assert.Fail("Clone.Type does not match base.");
            }

            if (element.Documentation != clone.Documentation)
            {
                Assert.Fail("Clone.Documentation does not match base.");
            }

            if (element.IsAdvanced != clone.IsAdvanced)
            {
                Assert.Fail("Clone.IsAdvanced does not match base.");
            }

            if (element.Value == clone.Value)
            {
                Assert.Fail("Clone.Value matches base.");
            }
        }

        [TestMethod]
        public void Clone_SuccessWithValue()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element clone = element.Clone("cloneElement");

            if (element.MaxOccurs != clone.MaxOccurs)
            {
                Assert.Fail("Clone.MaxOccurs does not match base.");
            }

            if (element.MinOccurs != clone.MinOccurs)
            {
                Assert.Fail("Clone.MinOccurs does not match base.");
            }

            if (element.Name != clone.Name)
            {
                Assert.Fail("Clone.Name does not match base.");
            }

            if (element.Parent != null)
            {
                if (element.Parent.Name != clone.Parent.Name)
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

            if (element.RootElement != clone.RootElement)
            {
                Assert.Fail("Clone.RootElement does not match base.");
            }

            if (element.SpecialLength != clone.SpecialLength)
            {
                Assert.Fail("Clone.SpecialLength does not match base.");
            }

            if (element.Type != clone.Type)
            {
                Assert.Fail("Clone.Type does not match base.");
            }

            if (element.Documentation != clone.Documentation)
            {
                Assert.Fail("Clone.Documentation does not match base.");
            }

            if (element.IsAdvanced != clone.IsAdvanced)
            {
                Assert.Fail("Clone.IsAdvanced does not match base.");
            }

            if (clone.Value != "cloneElement")
            {
                Assert.Fail("Clone.Value:" + clone.Value + " does not match provide string: cloneElement.");
            }
        }

        [TestMethod]
        public void Path_OneLevelWithNullParent()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Assert.IsTrue(element.Path == "baseElement", element.Path);
        }

        [TestMethod]
        public void Path_TwoLevels()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element element2 = new Element();
            element2.MaxOccurs = 1;
            element2.MinOccurs = 0;
            element2.Name = "element2";
            element2.Parent = element;
            element2.RootElement = true;
            element2.SpecialLength = false;
            element2.Type = "DataType.String";
            element2.Value = "element2";
            element2.Documentation = "element2";
            element2.IsAdvanced = true;

            element.Elements.Add(element2);

            Assert.IsTrue(element2.Path == "element2.baseElement", element2.Path);
        }


        [TestMethod]
        public void Path_ThreeLevels()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element element2 = new Element();
            element2.MaxOccurs = 1;
            element2.MinOccurs = 0;
            element2.Name = "element2";
            element2.Parent = element;
            element2.RootElement = true;
            element2.SpecialLength = false;
            element2.Type = "DataType.String";
            element2.Value = "element2";
            element2.Documentation = "element2";
            element2.IsAdvanced = true;

            element.Elements.Add(element2);

            Element element3 = new Element();
            element3.MaxOccurs = 1;
            element3.MinOccurs = 0;
            element3.Name = "element3";
            element3.Parent = element2;
            element3.RootElement = true;
            element3.SpecialLength = false;
            element3.Type = "DataType.String";
            element3.Value = "element3";
            element3.Documentation = "element3";
            element3.IsAdvanced = true;

            element2.Elements.Add(element3);

            Assert.IsTrue(element3.Path == "element3.element2.baseElement", element3.Path);
        }

        [TestMethod]
        public void FindAttribute_Success()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;

            NewRelic.AgentConfiguration.Parts.Attribute attribute2 = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute2.Name = "attributeName2";
            attribute2.Parent = element;

            element.Attributes.Add(attribute);
            element.Attributes.Add(attribute2);

            Assert.IsTrue(attribute.Name == element.FindAttribute(attribute).Name);
        }

        [TestMethod]
        public void FindElement_Success()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element element2 = new Element();
            element2.MaxOccurs = 1;
            element2.MinOccurs = 0;
            element2.Name = "element2";
            element2.Parent = element;
            element2.RootElement = true;
            element2.SpecialLength = false;
            element2.Type = "DataType.String";
            element2.Value = "element2";
            element2.Documentation = "element2";
            element2.IsAdvanced = true;

            element.Elements.Add(element2);

            Assert.IsTrue(element2.Name == element.FindElement("element2").Name);
        }

        [TestMethod]
        public void RemoveFirst_OneMatching()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element element2 = new Element();
            element2.MaxOccurs = 1;
            element2.MinOccurs = 0;
            element2.Name = "element2";
            element2.Parent = element;
            element2.RootElement = true;
            element2.SpecialLength = false;
            element2.Type = "DataType.String";
            element2.Value = "element2";
            element2.Documentation = "element2";
            element2.IsAdvanced = true;

            element.Elements.Add(element2);

            Element element3 = new Element();
            element3.MaxOccurs = 1;
            element3.MinOccurs = 0;
            element3.Name = "element3";
            element3.Parent = element;
            element3.RootElement = true;
            element3.SpecialLength = false;
            element3.Type = "DataType.String";
            element3.Value = "element3";
            element3.Documentation = "element3";
            element3.IsAdvanced = true;

            element.Elements.Add(element3);

            element.RemoveFirst("element2");

            Assert.IsTrue(element.FindElement("element2") == null);
        }

        [TestMethod]
        public void RemoveFirst_TwoMatching()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element element2 = new Element();
            element2.MaxOccurs = 1;
            element2.MinOccurs = 0;
            element2.Name = "element2";
            element2.Parent = element;
            element2.RootElement = true;
            element2.SpecialLength = false;
            element2.Type = "DataType.String";
            element2.Value = "element2";
            element2.Documentation = "element2";
            element2.IsAdvanced = true;

            element.Elements.Add(element2);

            Element element3 = new Element();
            element3.MaxOccurs = 1;
            element3.MinOccurs = 0;
            element3.Name = "element3";
            element3.Parent = element;
            element3.RootElement = true;
            element3.SpecialLength = false;
            element3.Type = "DataType.String";
            element3.Value = "element3";
            element3.Documentation = "element3";
            element3.IsAdvanced = true;

            element.Elements.Add(element3);

            Element element2a = new Element();
            element2a.MaxOccurs = 1;
            element2a.MinOccurs = 0;
            element2a.Name = "element2";
            element2a.Parent = element;
            element2a.RootElement = true;
            element2a.SpecialLength = false;
            element2a.Type = "DataType.String";
            element2a.Value = "element2a";
            element2a.Documentation = "element2a";
            element2a.IsAdvanced = true;

            element.Elements.Add(element2a);

            element.RemoveFirst("element2");

            Assert.IsTrue(element.FindElement("element2").Value == "element2a");
        }

        [TestMethod]
        public void OnlyAttributeEmptyOrNull_NoAttribute()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Assert.IsTrue(element.OnlyAttributeEmptyOrNull());
        }

        [TestMethod]
        public void OnlyAttributeEmptyOrNull_OneAttributeOnlyNoValue()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;

            element.Attributes.Add(attribute);

            Assert.IsTrue(element.OnlyAttributeEmptyOrNull());
        }

        [TestMethod]
        public void OnlyAttributeEmptyOrNull_OneAttributeOnlyWithValue()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;
            attribute.Value = "attributeValue";

            element.Attributes.Add(attribute);

            Assert.IsFalse(element.OnlyAttributeEmptyOrNull());
        }

        [TestMethod]
        public void OnlyAttributeEmptyOrNull_TwoAttributesNoValues()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;
            
            element.Attributes.Add(attribute);

            NewRelic.AgentConfiguration.Parts.Attribute attribute2 = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute2.Name = "attribute2Name";
            attribute2.Parent = element;

            element.Attributes.Add(attribute2);

            Assert.IsFalse(element.OnlyAttributeEmptyOrNull());
        }

        [TestMethod]
        public void OnlyAttributeEmptyOrNull_TwoAttributesWithValues()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;
            attribute.Value = "attributeValue";

            element.Attributes.Add(attribute);

            NewRelic.AgentConfiguration.Parts.Attribute attribute2 = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute2.Name = "attribute2Name";
            attribute2.Parent = element;
            attribute2.Value = "attribute2Value";

            element.Attributes.Add(attribute2);

            Assert.IsFalse(element.OnlyAttributeEmptyOrNull());
        }

        [TestMethod]
        public void RemoveAllChildElements_Success()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Element element2 = new Element();
            element2.MaxOccurs = 1;
            element2.MinOccurs = 0;
            element2.Name = "element2";
            element2.Parent = element;
            element2.RootElement = true;
            element2.SpecialLength = false;
            element2.Type = "DataType.String";
            element2.Value = "element2";
            element2.Documentation = "element2";
            element2.IsAdvanced = true;

            element.Elements.Add(element2);

            Element element3 = new Element();
            element3.MaxOccurs = 1;
            element3.MinOccurs = 0;
            element3.Name = "element3";
            element3.Parent = element;
            element3.RootElement = true;
            element3.SpecialLength = false;
            element3.Type = "DataType.String";
            element3.Value = "element3";
            element3.Documentation = "element3";
            element3.IsAdvanced = true;

            element.Elements.Add(element3);

            Element element2a = new Element();
            element2a.MaxOccurs = 1;
            element2a.MinOccurs = 0;
            element2a.Name = "element2";
            element2a.Parent = element;
            element2a.RootElement = true;
            element2a.SpecialLength = false;
            element2a.Type = "DataType.String";
            element2a.Value = "element2a";
            element2a.Documentation = "element2a";
            element2a.IsAdvanced = true;

            element.Elements.Add(element2a);

            element.RemoveAllChildElements("element2");

            Assert.IsTrue(element.Elements.Count == 1, element.Elements.Count.ToString());
        }

        [TestMethod]
        public void AllAttributesEmptyOrNull_NoAttributes()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            Assert.IsTrue(element.AllAttributesEmptyOrNull());
        }

        [TestMethod]
        public void AllAttributesEmptyOrNull_TwoAttributesNoValues()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;

            element.Attributes.Add(attribute);

            NewRelic.AgentConfiguration.Parts.Attribute attribute2 = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute2.Name = "attribute2Name";
            attribute2.Parent = element;

            element.Attributes.Add(attribute2);

            Assert.IsTrue(element.AllAttributesEmptyOrNull());
        }

        [TestMethod]
        public void AllAttributesEmptyOrNull_TwoAttributesWithValues()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;
            attribute.Value = "attributeValue";

            element.Attributes.Add(attribute);

            NewRelic.AgentConfiguration.Parts.Attribute attribute2 = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute2.Name = "attribute2Name";
            attribute2.Parent = element;
            attribute2.Value = "attribute2Value";

            element.Attributes.Add(attribute2);

            Assert.IsFalse(element.AllAttributesEmptyOrNull());
        }

        [TestMethod]
        public void AllAttributesEmptyOrNull_TwoAttributesOneWithValues()
        {
            Element element = new Element();
            element.MaxOccurs = 1;
            element.MinOccurs = 0;
            element.Name = "baseElement";
            element.Parent = null;
            element.RootElement = true;
            element.SpecialLength = false;
            element.Type = "DataType.String";
            element.Value = "baseElement";
            element.Documentation = "baseElement";
            element.IsAdvanced = true;

            NewRelic.AgentConfiguration.Parts.Attribute attribute = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute.Name = "attributeName";
            attribute.Parent = element;

            element.Attributes.Add(attribute);

            NewRelic.AgentConfiguration.Parts.Attribute attribute2 = new NewRelic.AgentConfiguration.Parts.Attribute();
            attribute2.Name = "attribute2Name";
            attribute2.Parent = element;
            attribute2.Value = "attribute2Value";

            element.Attributes.Add(attribute2);

            Assert.IsFalse(element.AllAttributesEmptyOrNull());
        }
    }
}
