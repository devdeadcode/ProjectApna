namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroTrackIngCategoryOption
    {

        public enum XeroTrackIngCategoryOptionStatus
        {
            ACTIVE,
            ARCHIVED
        }

        public string TrackingOptionID { get; set; }
        public string Name { get; set; }
        public XeroTrackIngCategoryOptionStatus Status { get; set; }

    }
}