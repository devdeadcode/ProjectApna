using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyTreasuryCounts : DataConnectorEntityModel
    {

        public int clicks { get; set; }
        public int views { get; set; }
        public int shares { get; set; }
        public int reports { get; set; }

    }
}


