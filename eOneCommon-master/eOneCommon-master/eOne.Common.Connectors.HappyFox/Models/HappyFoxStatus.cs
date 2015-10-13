using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxStatus : DataConnectorEntityModel
    {
        public bool @default { get; set; }

        public string name { get; set; }

        public string behavior { get; set; }

        public int id { get; set; }

        public string color { get; set; }


    }
}
