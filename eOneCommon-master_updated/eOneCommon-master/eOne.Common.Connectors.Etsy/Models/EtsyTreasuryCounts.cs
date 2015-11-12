namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyTreasuryCounts : ConnectorEntityModel
    {

        public int clicks { get; set; }
        public int views { get; set; }
        public int shares { get; set; }
        public int reports { get; set; }

    }
}


