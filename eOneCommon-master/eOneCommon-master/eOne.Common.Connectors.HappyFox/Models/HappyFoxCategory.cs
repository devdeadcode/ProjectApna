using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCategory : DataConnectorEntityModel
    {
        public int id { get; set; }
        
        public string description { get; set; }
        
        public string name { get; set; }
        
        public bool @public { get; set; } 
    }
}
