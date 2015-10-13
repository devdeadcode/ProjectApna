using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyFeedbackInfo : DataConnectorEntityModel
    {

        public int count { get; set; }
        public int score { get; set; }

    }
}
