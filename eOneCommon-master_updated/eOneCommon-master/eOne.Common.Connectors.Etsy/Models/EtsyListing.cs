using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyListing : ConnectorEntityModel
    {

        #region Enums

        public enum EtsyListingState
        {
            [Description("Active")]
            ACTIVE,
            [Description("Removed")]
            REMOVED,
            [Description("Sold out")]
            SOLD_OUT,
            [Description("Expired")]
            EXPIRED,
            [Description("Alchemy")]
            ALCHEMY,
            [Description("Edit")]
            EDIT,
            [Description("Create")]
            CREATE,
            [Description("Private")]
            PRIVATE,
            [Description("Unavailable")]
            UNAVAILABLE
        }
        public enum EtsyListingWhoMade
        {
            [Description("Me")]
            i_did,
            [Description("Collective")]
            collective,
            [Description("Someone else")]
            someone_else
        }
        public enum EtsyListingWhenMade
        {
            [Description("Made to order")]
            _made_to_order,
            [Description("2010 - 2015")]
            _2010_2015,
            [Description("2000 - 2009")]
            _2000_2009,
            [Description("1996 - 1999")]
            _1996_1999,
            [Description("Before 1996")]
            _before_1996,
            [Description("1990 - 1995")]
            _1990_1995,
            [Description("1980s")]
            _1980s,
            [Description("1970s")]
            _1970s,
            [Description("1960s")]
            _1960s,
            [Description("1950s")]
            _1950s,
            [Description("1940s")]
            _1940s,
            [Description("1930s")]
            _1930s,
            [Description("1920s")]
            _1920s,
            [Description("1910s")]
            _1910s,
            [Description("1900s")]
            _1900s,
            [Description("1800s")]
            _1800s,
            [Description("1700s")]
            _1700s,
            [Description("Before 1700")]
            _before_1700
        }
        public enum EtsyListingRecipient
        {
            [Description("Men")]
            men,
            [Description("Women")]
            women,
            [Description("Unisex adults")]
            unisex_adults,
            [Description("Teen boys")]
            teen_boys,
            [Description("Teen girls")]
            teen_girls,
            [Description("Teens")]
            teens,
            [Description("Boys")]
            boys,
            [Description("Girls")]
            girls,
            [Description("Children")]
            children,
            [Description("Baby boys")]
            baby_boys,
            [Description("Baby girls")]
            baby_girls,
            [Description("Babies")]
            babies,
            [Description("Birds")]
            birds,
            [Description("Cats")]
            cats,
            [Description("Dogs")]
            dogs,
            [Description("Pets")]
            pets,
            [Description("Not specified")]
            not_specified
        }
        public enum EtsyListingOccasion
        {
            [Description("Anniversary")]
            anniversary,
            [Description("Baptism")]
            baptism,
            [Description("Bar or bat mitzvah")]
            bar_or_bat_mitzvah,
            [Description("Birthday")]
            birthday,
            [Description("Canada day")]
            canada_day,
            [Description("Chinese new year")]
            chinese_new_year,
            [Description("Cinco de Mayo")]
            cinco_de_mayo,
            [Description("Confirmation")]
            confirmation,
            [Description("Christmas")]
            christmas,
            [Description("Day of the dead")]
            day_of_the_dead,
            [Description("Easter")]
            easter,
            [Description("Eid")]
            eid,
            [Description("Engagement")]
            engagement,
            [Description("Fathers day")]
            fathers_day,
            [Description("Get well")]
            get_well,
            [Description("Graduation")]
            graduation,
            [Description("Halloween")]
            halloween,
            [Description("Hanukkah")]
            hanukkah,
            [Description("Housewarming")]
            housewarming,
            [Description("Kwanzaa")]
            kwanzaa,
            [Description("Prom")]
            prom,
            [Description("4th of July")]
            july_4th,
            [Description("Mothers day")]
            mothers_day,
            [Description("New baby")]
            new_baby,
            [Description("New years")]
            new_years,
            [Description("Quinceanera")]
            quinceanera,
            [Description("Retirement")]
            retirement,
            [Description("St. Patrick's day")]
            st_patricks_day,
            [Description("Sweet 16")]
            sweet_16,
            [Description("Sympathy")]
            sympathy,
            [Description("Thanksgiving")]
            thanksgiving,
            [Description("Valentines day")]
            valentines,
            [Description("Wedding")]
            wedding
        }

        #endregion

        #region Default properties

        [FieldSettings("Title", DefaultField = true)]
        public string title { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("Price", DefaultField = true)]
        public string price { get; set; }

        [FieldSettings("Quantity", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int quantity { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Listing ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int listing_id { get; set; }

        [FieldSettings("Currency", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(IsoCurrency))]
        public IsoCurrency currency_code { get; set; }

        [FieldSettings("Link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Number of views", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int views { get; set; }

        [FieldSettings("Number of favorites", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_favorers { get; set; }

        [FieldSettings("Category path")]
        public string category_path { get; set; }

        [FieldSettings("Recipient", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(EtsyListingRecipient))]
        public EtsyListingRecipient recipient { get; set; }

        [FieldSettings("Occasion", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(EtsyListingOccasion))]
        public EtsyListingOccasion occasion { get; set; }

        [FieldSettings("Minimum number of days for processing", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int processing_min { get; set; }

        [FieldSettings("Maximum number of days for processing", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int processing_max { get; set; }

        [FieldSettings("Made by", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(EtsyListingOccasion))]
        public EtsyListingWhoMade who_made { get; set; }

        [FieldSettings("Supply", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_supply { get; set; }

        [FieldSettings("Item weight", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int item_weight { get; set; }

        [FieldSettings("Item weight unit")]
        public string item_weight_units { get; set; }

        [FieldSettings("Item length", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int item_length { get; set; }

        [FieldSettings("Item width", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int item_width { get; set; }

        [FieldSettings("Item height", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int item_height { get; set; }

        [FieldSettings("Item dimensions unit")]
        public string item_dimensions_unit { get; set; }

        [FieldSettings("Private", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_private { get; set; }

        [FieldSettings("Non-taxable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool non_taxable { get; set; }

        [FieldSettings("Customizable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_customizable { get; set; }

        [FieldSettings("Digital", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_digital { get; set; }

        [FieldSettings("Digital file data")]
        public string file_data { get; set; }

        [FieldSettings("Has variations", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool has_variations { get; set; }

        [FieldSettings("Automatic renewals", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool should_auto_renew { get; set; }

        [FieldSettings("Language")]
        public string language { get; set; }

        [FieldSettings("Feature order", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int featured_rank { get; set; }

        #endregion

        #region Hidden properties

        public string state { get; set; }
        public string when_made { get; set; }
        public List<string> style { get; set; }
        public List<string> tags { get; set; }
        public List<int> category_path_ids { get; set; }
        public List<string> taxonomy_path { get; set; }
        public List<string> materials { get; set; }
        public float creation_tsz { get; set; }
        public float ending_tsz { get; set; }
        public float original_creation_tsz { get; set; }
        public float last_modified_tsz { get; set; }
        public float state_tsz { get; set; }
        public EtsyTaxonomy taxonomy { get; set; }
        public EtsyTaxonomy suggested_taxonomy { get; set; }
        public EtsyShopSection shop_section { get; set; }
        public int user_id { get; set; }
        public int category_id { get; set; }
        public int taxonomy_id { get; set; }
        public int suggested_taxonomy_id { get; set; }
        public int shop_section_id { get; set; }
        public int shipping_template_id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(EtsyListingState))]
        public EtsyListingState listing_status => ParseEnum<EtsyListingState>(state.ToUpper());

        [FieldSettings("When made", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(EtsyListingWhenMade))]
        public EtsyListingWhenMade listing_when_made => ParseEnum<EtsyListingWhenMade>($"_{when_made}");

        [FieldSettings("Tags")]
        public string tag_list => CommaSeparatedValues(tags);

        [FieldSettings("Style 1")]
        public string style_1
        {
            get
            {
                if (style == null || style.Count == 0) return string.Empty;
                return style[0];
            }
        }

        [FieldSettings("Style 2")]
        public string style_2
        {
            get
            {
                if (style == null || style.Count < 2) return string.Empty;
                return style[1];
            }
        }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime creation_date => FromEpochSeconds(creation_tsz);

        [FieldSettings("Created time", FieldTypeId = Connector.FieldTypeIdTime, Created = true)]
        public DateTime creation_time => creation_date;

        [FieldSettings("Ending date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime ending_date => FromEpochSeconds(ending_tsz);

        [FieldSettings("Ending time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime ending_time => ending_date;

        [FieldSettings("Original creation date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime original_creation_date => FromEpochSeconds(original_creation_tsz);

        [FieldSettings("Original creation time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime original_creation_time => original_creation_date;

        [FieldSettings("Last modified date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime last_modified_date => FromEpochSeconds(last_modified_tsz);

        [FieldSettings("Last modified time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime last_modified_time => last_modified_date;

        [FieldSettings("Last state change date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime state_date => FromEpochSeconds(state_tsz);

        [FieldSettings("Last state change time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime state_time => state_date;

        [FieldSettings("Taxonomy paths")]
        public string taxonomy_path_list => CommaSeparatedValues(taxonomy_path);

        [FieldSettings("Materials")]
        public string materials_list => CommaSeparatedValues(materials);

        [FieldSettings("Featured", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_featured => featured_rank > 0;

        [FieldSettings("Taxonomy")]
        public string taxonomy_name => taxonomy == null ? string.Empty : taxonomy.name;

        [FieldSettings("Suggested taxonomy")]
        public string suggested_taxonomy_name => suggested_taxonomy == null ? string.Empty : suggested_taxonomy.name;

        [FieldSettings("Shop section")]
        public string shop_section_title => shop_section == null ? string.Empty : shop_section.title;

        #endregion

    }
}