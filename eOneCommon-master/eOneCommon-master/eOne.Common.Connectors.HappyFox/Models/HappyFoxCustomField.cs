using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCustomField : DataConnectorEntityModel
    {
        public string name { get; set; }

        public bool depends_on_choice { get; set; }

        public bool required { get; set; }

        public int id { get; set; }

        public List<HappyFoxCustomFieldChoices> choices { get; set; }

        public string type { get; set; }

        public int order { get; set; }

        public bool visible_to_staff_only { get; set; }
    }
}
