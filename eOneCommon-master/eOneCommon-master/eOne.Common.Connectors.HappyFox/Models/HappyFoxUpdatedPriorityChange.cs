using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedPriorityChange : DataConnectorEntityModel
    {
        public string from_priority { get; set; }

        public string to_priority { get; set; }
    }
}
