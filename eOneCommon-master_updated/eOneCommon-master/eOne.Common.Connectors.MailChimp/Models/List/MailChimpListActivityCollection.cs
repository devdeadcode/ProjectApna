using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListActivityCollection : MailChimpCollection
    {

        public List<MailChimpListActivity> activity { get; set; }
        public string list_id { get; set; }

    }
}
