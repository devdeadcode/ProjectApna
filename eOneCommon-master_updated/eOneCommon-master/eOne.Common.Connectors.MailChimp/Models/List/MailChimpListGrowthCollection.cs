using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListGrowthCollection : MailChimpCollection
    {

        public List<MailChimpListGrowth> history { get; set; }
        public string list_id { get; set; }

    }
}
