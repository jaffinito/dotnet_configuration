using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NewRelic.AgentConfiguration.Core.Singletons
{
    /// <summary>
    /// A collection to hold the name and location of multiline textboxes and their relation to Xml elements.
    /// </summary>
    public sealed class MultiLineCollection
    {
        private Dictionary<string, MultiLineSet> _collection = new Dictionary<string, MultiLineSet>();

        private static readonly MultiLineCollection _instance = new MultiLineCollection();

        public static MultiLineCollection Instance
        { get { return _instance; } }

        private MultiLineCollection()
        {
            
        }

        /// <summary>
        /// Struct to hold the textbox name and FlowLayoutPanel it belongs to.
        /// </summary>
        struct MultiLineSet
        {
            public String TextBoxName;
            public FlowLayoutPanel Parent;
        }

        /// <summary>
        /// Adds a new textbox to the collection after checking if it already exists.
        /// </summary>
        /// <param name="qualifiedName">Element name in the format of name.parent.parent.</param>
        /// <param name="textBoxName">Name of the textbox being added.</param>
        /// <param name="parentFlow">FlowLayoutPanel where the textbox is located.</param>
        public void Add(string qualifiedName, string textBoxName, FlowLayoutPanel parentFlow)
        {
            if(!_collection.ContainsKey(qualifiedName))
            {
                MultiLineSet set;
                set.TextBoxName = textBoxName;
                set.Parent = parentFlow;
                _collection.Add(qualifiedName, set);
            }
        }

        /// <summary>
        /// Public method to check if an element exists in the collection.
        /// </summary>
        /// <param name="qualifiedName">Element name in the format of name.parent.parent.</param>
        /// <returns>Returns true if the item exists and false if not.</returns>
        public bool Exists(string qualifiedName)
        {
            return _collection.ContainsKey(qualifiedName);
        }

        //throwing errors about no object.... search is wrong... (?FIXED?)
        /// <summary>
        /// Gets the textbox referenced in the collection using the qualified name.
        /// </summary>
        /// <param name="qualifiedName">Element name in the format of name.parent.parent.</param>
        /// <returns>Textbox contained in the collection.</returns>
        public TextBox GetTextBox(string qualifiedName)
        {
            if (Exists(qualifiedName))
            {
                var controls = _collection[qualifiedName].Parent.Controls.Find(_collection[qualifiedName].TextBoxName, true);
                return controls[0] as TextBox;
            }
            return null;
        }

        /// <summary>
        /// Clears out the collection.
        /// </summary>
        public void Clear()
        {
            _collection.Clear();
        }
    }
}
