using NewRelic.AgentConfiguration.Core.Parts;

namespace NewRelic.AgentConfiguration.Core.Singletons
{
    public sealed class DataFile
    {
        private Element _configFile = new Element();
        private Element _schemaFile = new Element();
        private Element _mergedFile = new Element();

        private static readonly DataFile _instance = new DataFile();

        public static DataFile Instance
        {
            get { return _instance; }
        }

        public Element ConfigFile
        {
            get { return _configFile; }
            set { _configFile = value; }
        }

        public Element SchemaFile
        {
            get { return _schemaFile; }
            set { _schemaFile = value; }
        }

        public Element MergedFile
        {
            get { return _mergedFile; }
            set { _mergedFile = value; }
        }

        private DataFile()
        {
            
        }
    }
}
