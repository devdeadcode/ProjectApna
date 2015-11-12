using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedAssigneeChange : DataConnectorEntityModel
    {
        public string new_name { get; set; }

        public string old_name { get; set; }
    }
}
