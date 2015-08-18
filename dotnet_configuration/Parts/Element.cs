using System;
using System.Collections.Generic;

namespace NewRelic.AgentConfiguration.Parts
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
            var child = new Element();
            child.Name = this.Name;
            child.Documentation = this.Documentation;
            child.MaxOccurs = this.MaxOccurs;
            child.MinOccurs = this.MinOccurs;
            child.Type = this.Type;
            child.Parent = this.Parent;
            child.SpecialLength = this.SpecialLength;
            child.RootElement = this.RootElement;
            child.IsAdvanced = this.IsAdvanced;
            child.RootObject = this.RootObject;

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

            var names = new List<string>();
            foreach (var child in this.Attributes)
            {
                names.Add(child.Name);
            }

            if (!names.Contains(attribute.Name))
            {
                return null;
            }

            return this.Attributes[names.IndexOf(attribute.Name)];
        }

        public Element FindElement(string name)
        {
            if (this.Elements.Count <= 0)
            {
                return null;
            }

            var names = new List<string>();
            foreach (var child in this.Elements)
            {
                names.Add(child.Name);
            }

            if (!names.Contains(name))
            {
                return null;
            }

            return this.Elements[names.IndexOf(name)];
        }

        public void RemoveFirst(string name)
        {
            if (this.Elements.Count <= 0)
            {
                return;
            }

            var names = new List<string>();
            foreach (var child in this.Elements)
            {
                names.Add(child.Name);
            }

            if (!names.Contains(name))
            {
                return;
            }

            this.Elements.RemoveAt(name.IndexOf(name));
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
            var names = new List<string>();
            foreach (var child in this.Elements)
            {
                names.Add(child.Name);
            }

            //for will work backwards
            for(int i = names.Count - 1; i >= 0; i--)
            {
                if(names[i] == name)
                {
                    this.Elements.RemoveAt(i);
                }
            }
        }

        public bool AllAttributesEmptyOrNull()
        {
            foreach (var attribute in this.Attributes)
            {
                if (!String.IsNullOrWhiteSpace(attribute.Value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
