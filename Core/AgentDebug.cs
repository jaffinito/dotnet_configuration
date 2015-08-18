using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using log4net;
using NewRelic.AgentConfiguration.Core.Singletons;

namespace NewRelic.AgentConfiguration.Core
{
    public class AgentDebug
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AgentDebug));

        public string GetLogLevel()
        {
            try
            {
                /*
                //since LogLevel cannot be null must assign it first.
                LogLevel level =  File.ReadAllLines(GetConfigPath())
                    .Where(line => line.Contains("log level"))
                    .Select(line => line.Split('"')[1])
                    .Select(logLevel => (LogLevel)Enum.Parse(typeof(LogLevel), logLevel))
                    .FirstOrDefault();

                return level != null ? level : LogLevel.info;
                */

                return GetConfigFile().Document.Elements()
                    .Where(rootElement => rootElement.Name.LocalName == "configuration")
                    .Elements()
                        .Where(logElement => logElement.Name.LocalName == "log")
                        .Attributes()
                            .Where(attribute => attribute.Name.LocalName == "level")
                            //.Select(logLevel => (LogLevel)Enum.Parse(typeof(LogLevel), logLevel.Value))
                            .FirstOrDefault()
                            .Value ?? "xnofile";
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return "nofile";
            }
        }

        public void SetLogLevel(LogLevel level)
        {
            List<string> lines = new List<string>();
            bool setLogLevel = false;

            try
            {
                foreach (string line in File.ReadAllLines(GetConfigPath()))
                {
                    if (line.Contains("log level"))
                    {
                        string logLine = "";
                        int logStart = line.IndexOf('"') + 1;
                        int logEnd = line.IndexOf('"', logStart);
                        logLine = line.Remove(logStart, logEnd - logStart);
                        logLine = logLine.Insert(logStart, level.ToString());
                        lines.Add(logLine);
                        setLogLevel = true;
                    }
                    else
                    {
                        lines.Add(line);
                    }
                }

                if (!setLogLevel)
                {
                    lines.Insert(lines.Count - 1, "  <log level=\"" + level.ToString() + "\" />");
                }

                File.WriteAllLines(GetConfigPath(), lines);
            }
            catch(Exception ex)
            {
                log.Error(ex);
                //Do nothing with it since there is no file to set things in.
            }
        }

        public XDocument GetConfigFile()
        {
            
            try
            {
                return XDocument.Load(FilePaths.Instance.ConfigFile);
            }
            catch (Exception ex)
            {
                log.Error(ex);

            }

            var xdoc = XDocument.Load(FilePaths.Instance.ConfigFile);
            try
            {
                var testing = xdoc.Document.Elements()
                    .Where(rootElement => rootElement.Name.LocalName == "configuration")
                    .Elements()
                        .Where(logElement => logElement.Name.LocalName == "log")
                        .Attributes()
                            .Where(attribute => attribute.Name.LocalName == "level")
                            .FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex);
               
            }

            return null;
        }

        public string GetConfigPath(string filePath=null)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(filePath))
                {
                    if (File.Exists(FilePaths.Instance.ConfigFile))
                    {
                        return FilePaths.Instance.ConfigFile;
                    }
                    else
                    {
                        /*
                        OpenFileDialog openFile = new OpenFileDialog();
                        openFile.Title = "Select the newrelic.config file";
                        openFile.InitialDirectory = Input.FileLocation.ConfigFile;
                        openFile.FileName = "newrelic.config";
                        openFile.Filter = "config files (*.config)|*.config|All files (*.*)|*.*";
                        openFile.FilterIndex = 0;
                        openFile.RestoreDirectory = true;

                        if (openFile.ShowDialog() == DialogResult.OK)
                        {
                            MainForm.ConfigPath = openFile.FileName;
                            return openFile.FileName;
                        }
                        else
                        {
                            return null;
                        }
                        */
                        return null;
                    }
                }
                return filePath;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        public enum LogLevel
        {
            off,
            info,
            debug,
            finest,
            trace,
            all,
            nofile
        }
    }
}
