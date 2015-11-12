using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListAbuseCollection : MailChimpCollection
    {

        public List<MailChimpListAbuse> abuse_reports { get; set; }
        public string list_id { get; set; }

    }
}
