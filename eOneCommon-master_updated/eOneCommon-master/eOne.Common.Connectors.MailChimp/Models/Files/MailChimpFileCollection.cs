using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.Files
{
    public class MailChimpFileCollection : MailChimpCollection
    {

        public List<MailChimpFile> files { get; set; }
        public int total_file_size { get; set; }

    }
}