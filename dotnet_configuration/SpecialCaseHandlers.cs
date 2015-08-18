using NewRelic.AgentConfiguration.Parts;
using System.Collections.Generic;

namespace NewRelic.AgentConfiguration
{
    /// <summary>
    /// Handles special or one off situations when building the Xsd or Config objects.
    /// </summary>
    public static class SpecialCaseHandlers
    {
        /// <summary>
        /// List of parts that need textboxes set to longer lengths by default.  Format: parent.name
        /// </summary>
        private static List<string> SpecialLengthNames = new List<string>() { 
                "log.directory", 
                "log.fileName", 
                "ignoreErrors.exception", 
                "applicationPool.name", 
                "proxy.host", 
                "proxy.user", 
                "proxy.password", 
                "proxy.domain" ,
                "threadProfiling.ignoreMethod",
                "path.regex",
                "configuration.labels"
            };

        /// <summary>
        /// List of parts that should be hidden by default. Foramt: name.parent
        /// </summary>
        private static List<string> IsAdvancedNames = new List<string>(){
                "rootAgentEnabled.configuration",
                "maxStackTraceLines.configuration",
                "debugAgent.configuration",
                "threadProfilingEnabled.configuration",
                "crossApplicationTracingEnabled.configuration",
                "timingPrecision.configuration",
                "sendEnvironmentInfo.service",
                "host.service",
                "port.service",
                "syncStartup.service",
                "sendDataOnExit.service",
                "sendDataOnExitThreshold.service",
                "requestTimeout.service",
                "disableSamplers.application",
                "console.log",
                "fileLockingModel.log",
                "maximumSamplesPerMinute.analyticsEvents",
                "maximumSamplesStored.analyticsEvents",
                "maximumSamplesPerMinute.transactionEvents",
                "maximumSamplesStored.transactionEvents",
                "stackTraceThreshold.transactionTracer",
                "explainThreshold.transactionTracer",
                "maxSegments.transactionTracer",
                "maxStackTrace.transactionTracer",
                "maxExplainPlans.transactionTracer",
                "parameterGroups.configuration", //added to handle deprecated items
                "transactions.analyticsEvents", //added to handle deprecated items
            };

        /// <summary>
        /// Method that returns checks the SpecialLengthNames list to see which parts should be set to a specific length.
        /// </summary>
        /// <param name="part">The Attribute or Element.</param>
        /// <returns>Whether or not to use the static length.</returns>
        public static bool SpecialLength(IXsdPart part)
        {
            if(part.SpecialLength)
            {
                return true;
            }

            string validName = "";
            if (part.Parent != null)
            {
                validName = part.Parent.Name + "." + part.Name;
            }
            else
            {
                validName = part.Name;
            }

            if (SpecialLengthNames.Contains(validName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Event handler that prevents non-numbers from being used in textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void NumericNumberFilter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /// <summary>
        /// Determines if the element is the true root of the tree.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool RootElement(Element element)
        {
            if (element.Parent.Name == "configuration")
            {
                return true;
            }
            else
            {
                return false;   
            }
        }

        /// <summary>
        /// Method that uses the IsAdvancedNames list to determine if something should be hidden.
        /// </summary>
        /// <param name="part">The Attribute or Element.</param>
        /// <returns>Returns true to hide something and false to show.</returns>
        public static bool IsAdvanced(IXsdPart part)
        {
            var parent = part.Parent != null ? part.Parent.Name : "";
            var name = part.Name + "." + parent;
            if (IsAdvancedNames.Contains(name) || part.Documentation.ToLower().Contains("deprecated"))
            {
                return true;
            }
            return false;
        }

        public static string EnhancedDocumentation(string elementName)
        {
            switch(elementName)
            {
                case "threadProfiling":
                case "ignoreMethod":
                    return "Allows methods, such as Sleep(), to be ignored when running a thread profiling session to make the profile more accurately represent of the time spent performing work. Only applies to the thread profiler sessions initiated via the website.";
                case "labels":
                    return System.Environment.NewLine + "Each label comes in two parts, a name and a value, separated by a colon.  Separate each label using semi-colons." + System.Environment.NewLine + System.Environment.NewLine + "Example: foo:bar;zip:zap";
                default:
                    return "";
            }
        }
    }
}