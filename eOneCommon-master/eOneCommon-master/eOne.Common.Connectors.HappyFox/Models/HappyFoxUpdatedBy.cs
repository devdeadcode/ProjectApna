using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedBy : DataConnectorEntityModel
    {
        public string type { get; set; }

        public int id { get; set; }

        public string email { get; set; }

        public string name { get; set; }      
    }
}
