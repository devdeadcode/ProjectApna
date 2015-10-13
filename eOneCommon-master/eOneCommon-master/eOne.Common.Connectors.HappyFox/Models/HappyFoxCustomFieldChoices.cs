using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCustomFieldChoices : DataConnectorEntity
    {
        public int id { get; set; }

        public string text { get; set; }
    }
}
