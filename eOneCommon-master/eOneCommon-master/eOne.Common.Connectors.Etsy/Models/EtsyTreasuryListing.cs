using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyTreasuryListing : DataConnectorEntityModel
    {

        public EtsyTreasuryListingData data { get; set; }
        public float creation_tsz { get; set; }

        public int data_user_id => data.user_id;

        public string data_title => data.title;

        public decimal data_price => data.price;

        public IsoCurrency data_currency_code => data.currency_code;

        public int data_listing_id => data.listing_id;

        public EtsyTreasuryListingData.EtsyTreasuryListingDataState data_state => data.state;

        public string data_shop_name => data.shop_name;

        public int data_image_id => data.image_id;

        public string data_image_url_75x75 => data.image_url_75x75;

        public string data_image_url_170x135 => data.image_url_170x135;

        public DateTime creation_date => FromEpochSeconds(creation_tsz).Date;

        public DateTime creation_time => Time(FromEpochSeconds(creation_tsz));
    }
}