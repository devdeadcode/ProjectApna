using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedDueDateChange : DataConnectorEntityModel
    {
        public string from_due_date { get; set; }

        public string to_due_date { get; set; }
    }
}
