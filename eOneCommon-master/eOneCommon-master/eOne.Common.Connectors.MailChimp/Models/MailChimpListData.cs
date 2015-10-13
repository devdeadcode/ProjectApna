using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpListData : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Default subject", DefaultField = true)]
        public string default_subject { get; set; }

        [FieldSettings("Default from name", DefaultField = true)]
        public string default_from_name { get; set; }

        [FieldSettings("Default from email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string default_from_email { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Email type option", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool email_type_option { get; set; }

        [FieldSettings("Use awesomebar", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool use_awesomebar { get; set; }

        [FieldSettings("Default language")]
        public string default_language { get; set; }

        [FieldSettings("List rating", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double list_rating { get; set; }

        [FieldSettings("Subscribe URL (short)", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string subscribe_url_short { get; set; }

        [FieldSettings("Subscribe URL (long)", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string subscribe_url_long { get; set; }

        [FieldSettings("Beamer address")]
        public string beamer_address { get; set; }

        [FieldSettings("Visibility")]
        public string visibility { get; set; }

        #endregion

        #region Hidden properties

        public int web_id { get; set; }
        public MailChimpListStatistics stats { get; set; }
        public DateTime? date_created { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Number of members", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_member_count => stats.member_count;

        [FieldSettings("Number of unsubscribed members", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_unsubscribe_count => stats.unsubscribe_count;

        [FieldSettings("Number of cleaned members", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_cleaned_count => stats.cleaned_count;

        [FieldSettings("Number of members since last campaign", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_member_count_since_send => stats.member_count_since_send;

        [FieldSettings("Number of unsubscribed members since last campaign", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_unsubscribe_count_since_send => stats.unsubscribe_count_since_send;

        [FieldSettings("Number of cleaned members since last campaign", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_cleaned_count_since_send => stats.cleaned_count_since_send;

        [FieldSettings("Number of campaigns", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_campaign_count => stats.campaign_count;

        [FieldSettings("Number of interest groupings", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_grouping_count => stats.grouping_count;

        [FieldSettings("Number of interest groups", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_group_count => stats.group_count;

        [FieldSettings("Number of merge vars", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public double stats_merge_var_count => stats.merge_var_count;

        [FieldSettings("Average subscriptions per month", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double stats_avg_sub_rate => stats.avg_sub_rate;

        [FieldSettings("Average unsubscribes per month", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double stats_avg_unsub_rate => stats.avg_unsub_rate;

        [FieldSettings("Target subscription rate", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double stats_target_sub_rate => stats.target_sub_rate;

        [FieldSettings("Open rate", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double stats_open_rate => stats.open_rate;

        [FieldSettings("Click rate", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public double stats_click_rate => stats.click_rate;

        #endregion

    }
}
