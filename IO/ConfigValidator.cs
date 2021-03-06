﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using log4net;
using NewRelic.AgentConfiguration.Core.Singletons;

namespace NewRelic.AgentConfiguration.IO
{
    /// <summary>
    /// Run the configuration file through an Xsd validation.
    /// </summary>
    public class ConfigValidator
    {
        private string _schemaPath, _configPath;
        private List<string> _errors;
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigValidator));

        /// <summary>
        /// Constructor for ConfigValidator.  Sets up the Xsd and Config paths.
        /// </summary>
        public ConfigValidator(string configPath, string schemapath)
        {
            _schemaPath = GetSchemaPath(schemapath);
            _configPath = GetConfigPath(configPath);
            _errors = new List<string>();
        }

        /// <summary>
        /// Performs the validation of the XDocument that is configured using the setup from the constructor.
        /// </summary>
        /// <param name="isSaving">Sets whether this is a standalone validation or part of save.</param>
        /// <returns>List of error strings from the validation attempt.</returns>
        public int Validate(bool isSaving = false)
        {
            //List<string> errors = new List<string>();
            try
            {
                var schemas = new XmlSchemaSet();
                schemas.Add("urn:newrelic-config", _schemaPath);


                var settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schemas;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                XmlReader reader;
                if (isSaving)
                {
                    ConfigWriter writer = new ConfigWriter();
                    reader = XmlReader.Create(StringToStream(writer.ToString()), settings);

                }
                else
                {
                    reader = XmlReader.Create(_configPath, settings);
                }

                while (reader.Read()) ;
                reader.Close();

                if (_errors.Count > 0)
                {
                    OutputErrors(_errors);
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\enabledebug") || File.Exists(Application.StartupPath + "\\enabledebug.txt"))
                {
                    return 0;
                }

                return _errors.Count;
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return -1;
            }
        }

        public void ValidationHandler(object sender, ValidationEventArgs e)
        {
            _errors.Add("Line:" + e.Exception.LineNumber + " Column:" + e.Exception.LinePosition + "  " + e.Message);
        }

        /// <summary>
        /// Writes the errors out to a file called validation in the application root. Overwrites if the file exists.
        /// </summary>
        /// <param name="errors">List of the errors to save.</param>
        private void OutputErrors(List<string> errors)
        {
            var savePath = Application.StartupPath + "\\validation.txt";
            using (var writer = new StreamWriter(savePath, false))
            {
                writer.WriteLine("newrelic.config validation on "+ 
                    DateTime.Now.Day + "-" +
                    DateTime.Now.Month + "-" +
                    DateTime.Now.Year + "  " +
                    DateTime.Now.Hour + ":" +
                    DateTime.Now.Minute + ":" +
                    DateTime.Now.Second
                );
                writer.WriteLine("XSD: " + _schemaPath);
                writer.WriteLine("CONFIG: " + _configPath);
                writer.WriteLine("");
                foreach (string error in errors)
                {
                    writer.WriteLine(error);
                }
            }
        }

        /// <summary>
        /// Returns the Config file path or prompt for it via OpenFileDialog.
        /// </summary>
        /// <returns>Config file as a string.</returns>
        private string GetConfigPath(string configPath)
        {
            if (String.IsNullOrWhiteSpace(configPath))
            {
                if (File.Exists(FilePaths.Instance.ConfigFile))
                {
                    return FilePaths.Instance.ConfigFile;
                }

                var openFile = new OpenFileDialog();
                openFile.Title = "Select the newrelic.config file";
                openFile.InitialDirectory = Path.GetDirectoryName(FilePaths.Instance.ConfigFile);
                openFile.FileName = "newrelic.config";
                openFile.Filter = "config files (*.config)|*.config|All files (*.*)|*.*";
                openFile.FilterIndex = 0;
                openFile.RestoreDirectory = true;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    return openFile.FileName;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return configPath;
            }
        }

        /// <summary>
        /// Returns the Xsd path or prompt for it via OpenFileDialog.
        /// </summary>
        /// <returns>Xsd file as a string.</returns>
        private string GetSchemaPath(string schemapath)
        {
            if (String.IsNullOrWhiteSpace(schemapath))
            {
                if (File.Exists(FilePaths.Instance.SchemaFile))
                {
                    return FilePaths.Instance.SchemaFile;
                }
                var openFile = new OpenFileDialog();
                openFile.Title = "Select the newrelic.xsd file";
                openFile.InitialDirectory = Path.GetDirectoryName(FilePaths.Instance.SchemaFile);
                openFile.FileName = "newrelic.xsd";
                openFile.Filter = "xsd files (*.xsd)|*.xsd|All files (*.*)|*.*";
                openFile.FilterIndex = 0;
                openFile.RestoreDirectory = true;

                return openFile.ShowDialog() == DialogResult.OK ? openFile.FileName : null;
            }
            return schemapath;
        }

        /// <summary>
        /// Converts a string into a stream for use by the XDocument.Load function.
        /// </summary>
        /// <param name="stringValue">String to convert.</param>
        /// <returns>Stream representing the string.</returns>
        public MemoryStream StringToStream(string stringValue)
        {
            return new MemoryStream(Encoding.GetEncoding("utf-16").GetBytes(stringValue));
        }
    }
}
