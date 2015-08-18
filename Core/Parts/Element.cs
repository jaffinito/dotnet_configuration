using System;
using System.Collections.Generic;
using System.Linq;

namespace NewRelic.AgentConfiguration.Core.Parts
{
    public class Element:IXsdPart
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal MinOccurs { get; set; }
        public decimal MaxOccurs { get; set; }
        public string Documentation { get; set; }
        public List<Element> Elements;
        public List<Attribute> Attributes;
        public Element Parent { get; set; }
        public string Value { get; set; }
        public bool SpecialLength { get; set; }
        public bool RootElement { get; set; }
        public bool IsAdvanced { get; set; }
        public string RootObject { get; set; }

        public Element()
        {
            this.Elements = new List<Element>();
            this.Attributes = new List<Attribute>();
            this.SpecialLength = false;
            this.RootElement = false;
        }

        public Element Clone(string value = null)
        {
            var child = new Element
            {
                Name = this.Name,
                Documentation = this.Documentation,
                MaxOccurs = this.MaxOccurs,
                MinOccurs = this.MinOccurs,
                Type = this.Type,
                Parent = this.Parent,
                SpecialLength = this.SpecialLength,
                RootElement = this.RootElement,
                IsAdvanced = this.IsAdvanced,
                RootObject = this.RootObject
            };

            if(value !=null)
            {
                child.Value = value;
            }

            return child;
        }

        public string Path
        {
            get
            {
                var element = this;
                string path = "";
                while (element.Parent != null)
                {
                    path += element.Name + ".";
                    element = element.Parent;
                }
                path += element.Name;
                return path;
            }
        }

        public Attribute FindAttribute(Attribute attribute)
        {
            if(this.Attributes.Count <= 0)
            {
                return null;
            }

            var names = this.Attributes.Select(child => child.Name).ToList();

            if (!names.Contains(attribute.Name))
            {
                return null;
            }

            return this.Attributes[names.IndexOf(attribute.Name)];
        }

        public Element FindElement(string name)
        {
            return this.Elements.Count <= 0 ? null : this.Elements.FirstOrDefault(child => child.Name == name);
        }

        public void RemoveFirst(string name)
        {
            if (this.Elements.Count <= 0)
            {
                return;
            }

            var names = this.Elements.Select(child => child.Name).ToList();

            if (!names.Contains(name))
            {
                return;
            }

            this.Elements.RemoveAt(names.IndexOf(name));
        }

        public bool OnlyAttributeEmptyOrNull()
        {
            if(this.Attributes.Count == 1)
            {
                if(String.IsNullOrWhiteSpace(this.Attributes[0].Value))
                {
                    return true;
                }
                return false;
            }
            else if (this.Attributes.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void RemoveAllChildElements(string name)
        {
            //in order to prevent altered list errors when deleting we use the list to search
            var names = this.Elements.Select(child => child.Name).ToList();

            //for will work backwards
            for(var i = names.Count - 1; i >= 0; i--)
            {
                if(names[i] == name)
                {
                    this.Elements.RemoveAt(i);
                }
            }
        }

        public bool AllAttributesEmptyOrNull()
        {
            return this.Attributes.All(attribute => String.IsNullOrWhiteSpace(attribute.Value));
        }
    }
}
