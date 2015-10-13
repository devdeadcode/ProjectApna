using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsAdwords : DataConnectorEntityModel
    {
        public enum Format
        {
            [Description("Text")]
            Text,
            [Description("Image")]
            Image,
            [Description("Flash")]
            Flash,
            [Description("Video")]
            Video
        }


        [FieldSettings("Search query", DefaultField = true)]
        public string adMatchedQuery { get; set; }

        [FieldSettings("Impressions", DefaultField =true)]
        public int impressions { get; set; }

        [FieldSettings("Clicks", DefaultField = true)]
        public int adClicks { get; set; }

        [FieldSettings("Cost",DefaultField =true)]
        public decimal adCost { get; set; }

        [FieldSettings("Format", EnumType = typeof(Format))]
        public Format adFormat { get; set; }

        [FieldSettings("Display URL")]
        public string adDisplayUrl { get; set; }

        [FieldSettings("Destnation URL")]
        public string adDestinationUrl { get; set; }

        [FieldSettings("Match type")]
        public string adMatchType { get; set; }

        [FieldSettings("Network")]
        public string adDistributionNetwork { get; set; }

        #region Calculations
        [FieldSettings("Coust per thousand impressions")]
        public decimal cost_per_1000_imps {
            get {
                decimal costPer1000 = 0;
                if(impressions != 0) {
                    costPer1000 = adCost / (impressions * 1000);
                }
                return costPer1000;
            }
        }

        [FieldSettings("Cost pers click")]
        public decimal cost_per_click {
            get {
                decimal costPerClick = 0;
                if(adClicks != 0) {
                    costPerClick =  adCost / adClicks;
                }
                return costPerClick;
            }
        }

        [FieldSettings("Click through rate")]
        public decimal click_through_rate {
            get {
                decimal clickThroughRate = 0;
                if(impressions != 0) {
                    clickThroughRate =  adClicks / impressions;
                }
                return clickThroughRate;
            }
        }
        #endregion


    }
}
