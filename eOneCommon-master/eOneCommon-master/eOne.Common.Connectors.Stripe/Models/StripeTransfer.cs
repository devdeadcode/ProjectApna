using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeTransfer : DataConnectorEntityModel
    {
        #region Enum

        public enum TransferStatus
        {
            [Description("Paid")]
            paid,
            [Description("Pending")]
            pending,
            [Description("In transit")]
            in_transit,
            [Description("Canceled")]
            canceled,
            [Description("Failed")]
            failed
        }

        public enum TransferType
        {
            [Description("Card")]
            card,
            [Description("Bank account")]
            bank_account,
            [Description("Stripe account")]
            stripe_account
        }
        #endregion

        #region Default properties
        [FieldSettings("Transfer ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(TransferStatus), DefaultField = true)]
        public TransferStatus status { get; set; }
        #endregion

        #region General properties

        [FieldSettings("Bank name")]
        public string bank_name => bank_account?.bank_name;

        [FieldSettings("Routing number")]
        public string routing_number => bank_account?.routing_number;

        [FieldSettings("Bank country")]
        public string bank_country => bank_account?.country;

        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Description")]
        public string description { get; set; }

        [FieldSettings("Failure code")]
        public string failure_code { get; set; }

        [FieldSettings("Failure message")]
        public string failure_message { get; set; }

        [FieldSettings("Reversed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool reversed { get; set; }

        [FieldSettings("Type", EnumType = typeof(TransferType), FieldTypeId = DataConnector.FieldTypeIdEnum)]
        public TransferType type { get; set; }

        #endregion

        #region Hidden properties
        public StripeTransferBankAccount bank_account { get; set; }
        public long created { get; set; }
        public long date { get; set; }
        public decimal? amount { get; set; }
        public decimal? amount_reversed { get; set; }
        public decimal? application_fee { get; set; }

        public StripeTransferReversals reversals { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate, DefaultField = true)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Transfer date", FieldTypeId = DataConnector.FieldTypeIdDate, DefaultField = true)]
        public DateTime transfer_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(date);
            }
        }

        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? amt => amount/100;

        [FieldSettings("Amount reversed", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? amt_reversed => amount_reversed/100;

        [FieldSettings("Application fee", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? app_fee => application_fee/100;

        #endregion


    }
}
