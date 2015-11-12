using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTickets : ZendeskCore
    {

        public List<ZendeskTicket> tickets { get; set; }

    }
}
