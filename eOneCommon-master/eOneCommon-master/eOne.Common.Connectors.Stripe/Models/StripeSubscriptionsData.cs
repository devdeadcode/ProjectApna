using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeSubscriptionsData : DataConnectorEntityModel
    {
        #region Default properties
        [FieldSettings("Subscription ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Plan name", DefaultField = true)]
        public string plan_name => plan?.name;

        [FieldSettings("Status", DefaultField = true)]
        public string status { get; set; }

        #endregion

        #region General properties
        [FieldSettings("Application fee percent", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? application_fee_percent { get; set; }

        [FieldSettings("Cancel at period end", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool cancel_at_period_end { get; set; }

        [FieldSettings("Tax percent", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? tax_percent { get; set; }

        [FieldSettings("Currency")]
        public string plan_currency => plan?.currency;

        [FieldSettings("Customer ID")]
        public string customer { get; set; }

        [FieldSettings("Plan interval", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripePlans.StripePlanInterval))]
        public StripePlans.StripePlanInterval interval => plan.interval;

        [FieldSettings("Number of interval", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? plan_count => plan.interval_count;

        [FieldSettings("Trial period days", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? plan_trial_period => plan.trial_period_days;

        #endregion

        #region Calculations

        [FieldSettings("Amount", FieldTypeId = DataConnector.FieldTypeIdQuantity, DefaultField = true)]
        public decimal? plan_amount => plan.amount_per;

        [FieldSettings("Customer email", DefaultField = true)]
        public string customer_email => customers.email;

        [FieldSettings("Customer description")]
        public string customer_description => customers.description;

        [FieldSettings("Customer account balance")]
        public decimal account_balance => customers.acc_balance;

        [FieldSettings("Delinquent")]
        public bool delinquent => customers.delinquent;

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? created_date => plan?.created_date;

        [FieldSettings("Cancelled date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? canceled_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if(canceled_at != null) return epoch.AddSeconds(Convert.ToDouble(canceled_at));
                return null;
            }
        }

        [FieldSettings("Current period start date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? period_start_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (current_period_start != null) return epoch.AddSeconds(Convert.ToDouble(current_period_start));
                return null;
            }
        }

        [FieldSettings("Current period end date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? period_end_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (current_period_end != null) return epoch.AddSeconds(Convert.ToDouble(current_period_end));
                return null;
            }
        }

        [FieldSettings("End date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? end_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (ended_at != null) return epoch.AddSeconds(Convert.ToDouble(ended_at));
                return null;
            }
        }

        [FieldSettings("Start date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? start_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (start != null) return epoch.AddSeconds(Convert.ToDouble(start));
                return null;
            }
        }

        [FieldSettings("Trail start date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? trail_start_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (trial_start != null) return epoch.AddSeconds(Convert.ToDouble(trial_start));
                return null;
            }
        }

        [FieldSettings("Trail end date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? trial_end_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (trial_end != null) return epoch.AddSeconds(Convert.ToDouble(trial_end));
                return null;
            }
        }

        [FieldSettings("Plan frequency")]
        public string plan_frequency => plan.frequency;

        [FieldSettings("Days left on trial", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public double days_left
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (trial_end == null) return 0;
                return epoch.AddSeconds(Convert.ToDouble(trial_end)) < DateTime.Now ? (DateTime.Now - epoch.AddSeconds(Convert.ToDouble(trial_end))).TotalDays : 0;
            }
        }

        #endregion

        #region Hidden properties

        public StripePlans plan { get; set; }
        public StripeCustomers customers { get; set; }
        public long? canceled_at { get; set; }
        public long? current_period_start { get; set; }
        public long? current_period_end { get; set; }
        public long? ended_at { get; set; }
        public long? start { get; set; }
        public long? trial_start { get; set; }
        public long? trial_end { get; set; }
        public StripeCustomerDiscount discount { get; set; }
        #endregion
    }
}
