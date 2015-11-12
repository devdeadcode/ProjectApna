using System;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListGrowth
    {

        #region Default properties

        [FieldSettings("List name", DefaultField = true)]
        public string list_name => list.name;

        [FieldSettings("Month", DefaultField = true, KeyNumber = 3)]
        public string month_name
        {
            get
            {
                switch (month_number)
                {
                    case 1:
                        return "January";
                    case 2:
                        return "February";
                    case 3:
                        return "March";
                    case 4:
                        return "April";
                    case 5:
                        return "May";
                    case 6:
                        return "June";
                    case 7:
                        return "July";
                    case 8:
                        return "August";
                    case 9:
                        return "September";
                    case 10:
                        return "October";
                    case 11:
                        return "November";
                    case 12:
                        return "December";
                }
                return string.Empty;
            }
        }

        [FieldSettings("Year", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 2)]
        public int year
        {
            get
            {
                var split = month.Split('-');
                return int.Parse(split[0]);
            }
        }

        [FieldSettings("Existing subscribers", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int existing { get; set; }

        [FieldSettings("Imported subscribers", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int imports { get; set; }

        [FieldSettings("Opt-in subscribers", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int optins { get; set; }

        [FieldSettings("Total subscribers", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_subs => existing + imports + optins;

        #endregion

        #region Properties

        [FieldSettings("List ID", KeyNumber = 1)]
        public string list_id { get; set; }

        #endregion

        #region Hidden properties

        public string month { get; set; }
        public int month_number
        {
            get
            {
                var split = month.Split('-');
                return int.Parse(split[1]);
            }
        }
        public MailChimpList list { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("New subscribers", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int new_subs => imports + optins;

        [FieldSettings("Last sent date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? list_stats_campaign_last_sent => list.stats_campaign_last_sent;

        [FieldSettings("Default subject")]
        public string list_campaign_defaults_subject => list.campaign_defaults.subject;

        [FieldSettings("Default from name")]
        public string list_campaign_defaults_from_name => list.campaign_defaults_from_name;

        [FieldSettings("Default from email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string list_campaign_defaults_from_email => list.campaign_defaults_from_email;

        [FieldSettings("List rating", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double list_rating => list.list_rating;

        [FieldSettings("Subscribe URL (short)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string list_subscribe_url_short => list.subscribe_url_short;

        [FieldSettings("Subscribe URL (long)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string list_subscribe_url_long => list.subscribe_url_long;

        [FieldSettings("Visibility", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpList.MailChimpListVisibility))]
        public MailChimpList.MailChimpListVisibility list_visibility => list.visibility;

        [FieldSettings("List created date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? list_date_created => list.date_created;

        #endregion

    }
}
