namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyCartListing : ConnectorEntityModel
    {

        public enum EtsyCartListingPurchaseState
        {
            valid, 
            invalid_quantity, 
            invalid_shipping, 
            not_active, 
            edited, 
            invalid_currency, 
            invalid_shipping_currency
        }

        public int listing_id { get; set; }
        public int purchase_quantity { get; set; }
        public EtsyCartListingPurchaseState purchase_state { get; set; }
        public bool is_digital { get; set; }
        public string file_data { get; set; }
        public int listing_customization_id { get; set; }
        public bool variations_available { get; set; }
        public bool has_variations { get; set; }

    }
}


//selected_variations	private	cart_rw	array(Variations_SelectedProperty)	An array of selected Variations for this listing.
