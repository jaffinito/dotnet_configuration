using System.Text;

namespace NewRelic.AgentConfiguration.Core.Singletons
{
    public sealed class CurrentSection
    {
        private StringBuilder _section = new StringBuilder();

        private static readonly CurrentSection _instance = new CurrentSection();

        public static CurrentSection Instance { get { return _instance; } }

        private CurrentSection()
        {
            
        }

        public string Section
        {
            get { return _section.ToString(); }
            set { _section.Clear();
                _section.Append(value);
            }
        }
    }
}
