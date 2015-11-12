using System.Collections.Generic;

namespace eOne.Common.Setup
{
    public class ConnectorSetup
    {

        public class ConnectorSetupStep
        {

            public ConnectorSetupStep()
            {
                Fields = new List<ConnectorSetupField>();
            }
            public ConnectorSetupStep(int number, string header = "")
            {
                Number = number;
                Header = header;
                Fields = new List<ConnectorSetupField>();
            }
            public int Number { get; set; }
            public string Header { get; set; }
            public string TopDescription { get; set; }
            public string BottomDescription { get; set; }
            public List<ConnectorSetupField> Fields { get; set; }

            public virtual string GetUrl()
            {
                return string.Empty;
            }

        }

        public ConnectorSetup()
        {
            Steps = new List<ConnectorSetupStep>();
        }

        public string Description { get; set; }
        public List<ConnectorSetupStep> Steps { get; set; }

    }
}
