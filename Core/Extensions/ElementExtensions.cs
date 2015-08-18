using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NewRelic.AgentConfiguration.Core.Parts;
using NewRelic.AgentConfiguration.Core.Processors;

namespace NewRelic.AgentConfiguration.Core.Extensions
{
    public static class ElementExtensions
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ElementExtensions));

        public static Element GetElement(this Element element, Element elementToFind)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(elementToFind.Path))
                {
                    var steps = elementToFind.Path.Split('.').ToList<string>();
                    steps.Reverse();
                    steps.RemoveAt(0);
                    return GetElementPrivate(steps, element);
                }
            }
            catch (Exception ex)
            {
                log.Debug(ex);
            }
            return null;
        }

        public static Element GetElement(this Element element, string path)
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                var steps = path.Split('.').ToList<string>();
                steps.Reverse();
                steps.RemoveAt(0);
                return GetElementPrivate(steps, element);
            }
            return null;
        }

        private static Element GetElementPrivate(List<string> steps, Element element)
        {
            if (steps.Count > 0)
            {
                string step = steps[0];
                steps.RemoveAt(0);
                return GetElementPrivate(steps, element.FindElement(step));
            }
            else
            {
                return element;
            }
        }
    }
}
