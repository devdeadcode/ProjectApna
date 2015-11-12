using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedStatusChange : DataConnectorEntityModel
    {
        
        public string new_name { get; set; }

        public string old_name { get; set; }
    }
}
