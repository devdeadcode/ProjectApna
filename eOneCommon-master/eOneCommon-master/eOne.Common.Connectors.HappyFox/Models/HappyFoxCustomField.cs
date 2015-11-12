using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCustomField : DataConnectorEntityModel
    {
        #region ticket_custom_fields
        public string name { get; set; }

        public bool depends_on_choice { get; set; }

        public bool required { get; set; }

        public bool compulsory_on_completed { get; set; }

        public List<HappyFoxCustomFieldChoices> choices { get; set; }

        public string type { get; set; }

        public int id { get; set; }

        public List<HappyFoxTicketCustomFieldCategories> categories { get; set; }

        public bool visible_to_staff_only { get; set; }
        #endregion


        

        

        

        

        public int order { get; set; }

        
    }
}
