using System;
using log4net;
using Microsoft.Win32;

namespace NewRelic.AgentConfiguration.Core.Singletons
{
    public sealed class FilePaths
    {
        private const string _keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\New Relic\.NET Agent";
        private const string _valueName = "NewRelicHome";
        private const string _programData = @"C:\ProgramData\New Relic\.NET Agent\";
        private static readonly ILog log = LogManager.GetLogger(typeof(FilePaths));

        private string _configPath;
        private string _schemapath;
        private string _basePath;

        private static readonly FilePaths _instance = new FilePaths();

        public static FilePaths Instance
        {
            get { return _instance; }
        }

        private FilePaths()
        {
            
        }

        public string SchemaFile
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_basePath))
                {
                    return System.IO.File.Exists(_programData + "newrelic.xsd") ? _programData + "newrelic.xsd" : null;
                }
                return _basePath + "newrelic.xsd";
            }
        }

        public string ConfigFile
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_basePath))
                {
                    if (System.IO.File.Exists(_programData + "newrelic.config"))
                    {
                        log.Debug(_programData + "newrelic.config");
                        return _programData + "newrelic.config";
                    }
                    log.Debug("null");
                    return null;
                }
                log.Debug(_basePath + "newrelic.config");
                return _basePath + "newrelic.config";
            }
        }

        public void BasePath()
        {
            _basePath = Registry.GetValue(_keyName, _valueName, null) as String;
        }
    }
}
