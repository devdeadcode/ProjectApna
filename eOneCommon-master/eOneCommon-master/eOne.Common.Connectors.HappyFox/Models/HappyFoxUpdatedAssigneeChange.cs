using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedAssigneeChange : DataConnectorEntityModel
    {
        public string assigned_from { get; set; }

        public string assigned_to { get; set; }
    }
}
