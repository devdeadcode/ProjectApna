using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripePlans : DataConnectorEntityModel
    {
        #region enum
        public enum StripePlanInterval
        {
            [Description("Day")]
            day,
            [Description("Week")]
            week,
            [Description("Month")]
            month,
            [Description("Year")]
            year
        }
        #endregion

        #region Default properties
        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }
        #endregion

        #region General properties
        [FieldSettings("Plan ID")]
        public string id { get; set; }

        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Interval", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripePlanInterval))]
        public StripePlanInterval interval { get; set; }

        [FieldSettings("Trial period days", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? trial_period_days { get; set; }
        #endregion

        #region Hidden properties

        public decimal amount { get; set; }
        public long created { get; set; }
        public int interval_count { get; set; }
        public StripePlanCollection planColl { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal amount_per
        {
            get
            {
                if (amount != 0) return amount / 100;
                return 0;
            }
        }

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Frequency", DefaultField = true)]
        public string frequency
        {
            get
            {
                if (interval_count > 1) return interval_count + " " + interval + "s";
                return interval_count + " " + interval;

            }
        }
        #endregion
    }
}
