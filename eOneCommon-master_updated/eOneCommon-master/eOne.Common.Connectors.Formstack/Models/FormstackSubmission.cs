using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Formstack.Models
{
    public class FormstackSubmission
    {

        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public string user_agent { get; set; }
        public string remote_addr { get; set; }
        public int form { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public List<FormstackSubmissionData> data { get; set; }

    }
}
