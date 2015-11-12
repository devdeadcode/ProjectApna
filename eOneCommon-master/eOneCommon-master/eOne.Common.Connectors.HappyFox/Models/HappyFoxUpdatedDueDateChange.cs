using eOne.Common.DataConnectors;
using Newtonsoft.Json;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedDueDateChange : DataConnectorEntityModel
    {
        [JsonProperty("new")]
        public string New { get; set; }

        public string old { get; set; }
    }
}
