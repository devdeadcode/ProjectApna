using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedStatusChange : DataConnectorEntityModel
    {
        public string from_status { get; set; }

        public string to_status { get; set; }
    }
}
