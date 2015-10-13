using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyTransaction : DataConnectorEntityModel
    {

        public int transaction_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public IsoCurrency currency_code { get; set; }
        public int quantity { get; set; }
        public decimal shipping_cost { get; set; }
        public bool is_digital { get; set; }
        public string file_data { get; set; }
        public bool is_quick_sale { get; set; }
        public string url { get; set; }
        
        public int seller_user_id { get; set; }
        public int buyer_user_id { get; set; }
        public float creation_tsz { get; set; }
        public float paid_tsz { get; set; }
        public float shipped_tsz { get; set; }
        public List<string> tags { get; set; }
        public List<string> materials { get; set; }
        public int image_listing_id { get; set; }
        public int receipt_id { get; set; }
        public int listing_id { get; set; }
        public int seller_feedback_id { get; set; }
        public int buyer_feedback_id { get; set; }

        public string tag_list => CommaSeparatedValues(tags);
        public string materials_list => CommaSeparatedValues(materials);
    }
}

//transaction_type	private	transactions_r	string	The type of transaction, usually "listing".
//variations	public	none	array(Variations_SelectedProperty)	Purchased variations for this transaction.
