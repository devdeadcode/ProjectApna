using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyTreasury : ConnectorEntityModel
    {

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float homepage { get; set; }
        public bool mature { get; set; }
        public bool @private { get; set; }
        public string locale { get; set; }
        public int comment_count { get; set; }
        public List<string> tags { get; set; }
        public float hotness { get; set; }
        public string hotness_color { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public int user_avatar_id { get; set; }
        public float creation_tsz { get; set; }
        public float became_public_date { get; set; }
        public List<EtsyTreasuryListing> listings { get; set; }
        public EtsyTreasuryCounts counts { get; set; }

        public int number_of_listings => listings.Count;

        public int number_of_sold_out_listings
        {
            get
            {
                return listings.Count(listing => listing.data_state == EtsyTreasuryListingData.EtsyTreasuryListingDataState.sold_out);
            }
        }

        public int counts_clicks => counts.clicks;
        public int counts_views => counts.views;
        public int counts_shares => counts.shares;
        public int counts_reports => counts.reports;
    }
}
