using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroTrackingCategory
    {

        public enum XeroTrackingCategoryStatus
        {
            ACTIVE,
            ARCHIVED
        }

        public string Name { get; set; }
        public string TrackingCategoryID { get; set; }
        public XeroTrackingCategoryStatus Status { get; set; }
        public List<XeroTrackIngCategoryOption> Options { get; set; }

    }
}