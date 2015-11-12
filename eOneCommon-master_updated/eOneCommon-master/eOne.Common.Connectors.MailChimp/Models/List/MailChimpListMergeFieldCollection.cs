using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListMergeFieldCollection : MailChimpCollection
    {

        public List<MailChimpListMergeField> merge_fields { get; set; }
        public string list_id { get; set; }

    }
}
