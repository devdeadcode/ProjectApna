using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpList : DataConnectorEntityModel
    {

        public int total { get; set; }
        public List<MailChimpListData> data { get; set; }
        public List<MailChimpListError> errors { get; set; }

    }
}
