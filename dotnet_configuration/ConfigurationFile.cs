using NewRelic.AgentConfiguration.Parts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewRelic.AgentConfiguration
{
    /// <summary>
    /// Static class to contain the Xsd, Config, and Merged Element objects.
    /// </summary>
    public static class ConfigurationFile
    {
        public static Element Xsd = new Element();
        public static Element Config = new Element();
        public static Element Merged = new Element();

        /// <summary>
        /// Find and Element based on the path provided.
        /// </summary>
        /// <param name="path">Path to the element in the format: name.application.configuration.</param>
        /// <param name="element">Element to search within.</param>
        /// <returns>Element object that matches the path or null.</returns>
        public static Element FindElementByPath(string path, Element element)
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                var steps = path.Split('.').ToList<string>();
                steps.Reverse();
                steps.RemoveAt(0);
                return GetElement(steps, element);
            }
            return null;
        }

        /// <summary>
        /// Recursive method to locate an element based on a given path array.
        /// </summary>
        /// <param name="steps">Array of steps in the path to the element starting with the parent.</param>
        /// <param name="element">Element that was located.</param>
        /// <returns>Element object that matches the path or null.</returns>
        private static Element GetElement(List<string> steps, Element element)
        {
            if (steps.Count > 0)
            {
                string step = steps[0];
                steps.RemoveAt(0);
                return GetElement(steps, element.FindElement(step));
            }
            else
            {
                return element;
            }
        }
    }
}
