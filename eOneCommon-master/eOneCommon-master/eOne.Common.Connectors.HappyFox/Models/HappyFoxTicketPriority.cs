using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketPriority : DataConnectorEntityModel
    {
        public bool @default { get; set; }

        public string name { get; set; }

        public int id { get; set; }

        public int order { get; set; }
    }
}
