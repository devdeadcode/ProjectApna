using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketCollection : DataConnectorEntityModel
    {
        public List<HappyFoxTicket> data { get; set; }

        public List<HappyFoxTicketMessage> messages { get; set; }

        public List<HappyFoxTicketSummary> summary { get; set; }

        public List<HappyFoxTicketUpdate> updates { get; set; }

        public HappyFoxPageInfo page_info { get; set; }

        public HappyFoxTicketCollection()
        {
            messages = new List<HappyFoxTicketMessage>();
            data = new List<HappyFoxTicket>();
            summary = new List<HappyFoxTicketSummary>();
            updates = new List<HappyFoxTicketUpdate>();
        }
    }
}
