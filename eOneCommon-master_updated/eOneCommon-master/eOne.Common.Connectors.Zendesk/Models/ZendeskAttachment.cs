using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskAttachment : ZendeskImage
    {

        public List<ZendeskImage> thumbnails { get; set; }

    }
}
