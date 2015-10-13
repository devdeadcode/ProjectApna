using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCannedActionStatusTo
    {
        public string name { get; set; }

        public string color { get; set; }

        public int order { get; set; }

        public bool @default { get; set; }

        public string behavior { get; set; }

        public int id { get; set; }
    }
}
