using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListMergeFieldOptions
    {

        public int default_country { get; set; }
        public string phone_format { get; set; }
        public string date_format { get; set; }
        public List<string> choices { get; set; }
        public int size { get; set; }

    }
}
