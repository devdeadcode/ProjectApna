using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxContact : DataConnectorEntityModel
    {
        [FieldSettings("Contact ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }
        
        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Num of pending tickets", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int pending_tickets_count { get; set; }

        [FieldSettings("Number of tickets", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int tickets_count { get; set; }

        #region Hidden properties
        public List<HappyFoxStatus> status { get; set; }

        public HappyFoxTicketCollection ticketCollection { get; set; }

        public HappyFoxContactCollection contactCollection { get; set; }

        public List<HappyFoxCustomField> custom_fields { get; set; }

        public HappyFoxTicketSummary ticket_summary { get; set; }
        #endregion

    }
}
