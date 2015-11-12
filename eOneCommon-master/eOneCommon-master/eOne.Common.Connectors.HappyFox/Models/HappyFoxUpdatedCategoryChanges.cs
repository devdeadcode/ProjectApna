using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;
using Newtonsoft.Json;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedCategoryChanges : DataConnectorEntityModel
    {
        [JsonProperty("new")]
        public string New { get; set; }

        public string old { get; set; }
    }
}
