using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Schema;
using NewRelic.AgentConfiguration.Input;
using System.Xml;
using System.Reflection;
using log4net;

namespace NewRelic.AgentConfiguration
{
    /// <summary>
    /// Run the configuration file through an Xsd validation.
    /// </summary>
    public class ConfigValidator
    {
        private string _xsdPath;
        private string _configPath;
        private List<string> _errors;
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigValidator));

        /// <summary>
        /// Constructor for ConfigValidator.  Sets up the Xsd and Config paths.
        /// </summary>
        public ConfigValidator()
        {
            _xsdPath = GetXsdPath();
            _configPath = GetConfigPath();
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
                schemas.Add("urn:newrelic-config", _xsdPath);


                var settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schemas;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                XmlReader reader;
                if (isSaving)
                {
                    Output.ConfigWriter writer = new Output.ConfigWriter();
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

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\enabledebug") || File.Exists(System.Windows.Forms.Application.StartupPath + "\\enabledebug.txt"))
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
                writer.WriteLine("XSD: " + _xsdPath);
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
        private string GetConfigPath()
        {
            if (String.IsNullOrWhiteSpace(MainForm.ConfigPath))
            {
                if (File.Exists(FileLocation.ConfigFile))
                {
                    return FileLocation.ConfigFile;
                }
                else
                {
                    var openFile = new OpenFileDialog();
                    openFile.Title = "Select the newrelic.config file";
                    openFile.InitialDirectory = Path.GetDirectoryName(FileLocation.ConfigFile);
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
            }
            else
            {
                return MainForm.ConfigPath;
            }
        }

        /// <summary>
        /// Returns the Xsd path or prompt for it via OpenFileDialog.
        /// </summary>
        /// <returns>Xsd file as a string.</returns>
        private string GetXsdPath()
        {
            if (String.IsNullOrWhiteSpace(MainForm.XsdPath))
            {
                if (File.Exists(FileLocation.XsdFile))
                {
                    return FileLocation.XsdFile;
                }
                else
                {
                    var openFile = new OpenFileDialog();
                    openFile.Title = "Select the newrelic.xsd file";
                    openFile.InitialDirectory = Path.GetDirectoryName(FileLocation.XsdFile);
                    openFile.FileName = "newrelic.xsd";
                    openFile.Filter = "xsd files (*.xsd)|*.xsd|All files (*.*)|*.*";
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
            }
            else
            {
                return MainForm.XsdPath;
            }
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
