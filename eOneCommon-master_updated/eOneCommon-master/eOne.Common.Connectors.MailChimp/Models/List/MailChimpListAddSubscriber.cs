using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListAddSubscriber
    {

        public enum MailChimpListAddSubscriberStatus
        {
            subscribed,
            pending
        }

        public string email_address { get; set; }
        public MailChimpListAddSubscriberStatus status { get; set; }
        public List<Tuple<string, string>> merge_fields { get; set; }

    }
}
