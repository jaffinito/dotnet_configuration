using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace NewRelic.AgentConfiguration.Input
{
    public static class FileLocation
    {
        const string _keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\New Relic\.NET Agent";
        const string _valueName = "NewRelicHome";
        private static readonly ILog log = LogManager.GetLogger(typeof(FileLocation));
        static private string _path;

        static public string XsdFile
        {
            get 
            {
                if(String.IsNullOrWhiteSpace(_path))
                {
                    if(System.IO.File.Exists(@"C:\ProgramData\New Relic\.NET Agent\newrelic.xsd"))
                    {
                        return @"C:\ProgramData\New Relic\.NET Agent\newrelic.xsd";
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return _path + @"\newrelic.xsd";
                }
            }
        }

        static public string ConfigFile
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_path))
                {
                    if (System.IO.File.Exists(@"C:\ProgramData\New Relic\.NET Agent\newrelic.config"))
                    {
                        log.Debug(@"01 - C:\ProgramData\New Relic\.NET Agent\newrelic.config");
                        return @"C:\ProgramData\New Relic\.NET Agent\newrelic.config";
                    }
                    else
                    {
                        log.Debug("02 - null");
                        return null;
                    }
                }
                else
                {
                    log.Debug("03 - " + _path + @"\newrelic.config");
                    return _path + @"\newrelic.config";
                }
            }
        }

        static public void BasePath()
        {
            _path = Registry.GetValue(_keyName, _valueName, null) as String;
        }
    }
}
