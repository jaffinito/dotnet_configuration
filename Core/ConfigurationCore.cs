using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using log4net;
using NewRelic.AgentConfiguration.Core.Parts;
using NewRelic.AgentConfiguration.Core.Processors;
using NewRelic.AgentConfiguration.Core.Singletons;

namespace NewRelic.AgentConfiguration.Core
{
    public class ConfigurationCore
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationCore));

        //public static Element ConfigFile = new Element();
        //public static Element SchemaFile = new Element();
        //public static Element MergedFile = new Element();
        //public static string CurrentSection { get; set; }
        public string PathRoot { get; private set; }

        public ConfigurationCore(string pathRoot)
        {
            PathRoot = pathRoot;
        }

        public void PopulateConfigFile()
        {
            var cproc = new ConfigProcessor();
            DataFile.Instance.ConfigFile = cproc.ProcessConfig(PathRoot);
            
        }

        public void PopulateSchemaFile()
        {
            var sproc = new SchemaProcessor();
            DataFile.Instance.SchemaFile = sproc.ProcessSchema(PathRoot);
        }

        public void MergeFiles()
        {
            DataFile.Instance.MergedFile = DataFile.Instance.SchemaFile.Clone();
            var merger = new MergeProcessor();
            merger.PrePopulateMergedFile(DataFile.Instance.SchemaFile, DataFile.Instance.MergedFile);
            merger.Merge(DataFile.Instance.ConfigFile);
            merger.SetMergedRootObject(DataFile.Instance.MergedFile);
        }

        /*public static Element FindElementByPath(string path, Element element)
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
        }*/
    }
}
