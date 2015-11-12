using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeDisputesData : DataConnectorEntityModel
    {
        public enum StripeDisputesReason
        {
            [Description("Duplicate")]
            duplicate,
            [Description("Fraudulent")]
            fraudulent,
            [Description("Subscription canceled")]
            subscription_canceled,
            [Description("Product unacceptable")]
            product_unacceptable,
            [Description("Product not received")]
            product_not_received,
            [Description("Unrecognized")]
            unrecognized,
            [Description("Credit not processed")]
            credit_not_processed,
            [Description("Incorrect account details")]
            incorrect_account_details,
            [Description("Insufficient funds")]
            insufficient_funds,
            [Description("Bank cannot process")]
            bank_cannot_process,
            [Description("Debit not authorized")]
            debit_not_authorized,
            [Description("General")]
            general
        }

        public enum StripeDisputesStatus
        {
            [Description("Warning needs response")]
            warning_needs_response,
            [Description("Warning under review")]
            warning_under_review,
            [Description("Warning closed")]
            warning_closed,
            [Description("Needs response")]
            needs_response,
            [Description("Response disabled")]
            response_disabled,
            [Description("Under review")]
            under_review,
            [Description("Charge refunded")]
            charge_refunded,
            [Description("Won")]
            won,
            [Description("Lost")]
            lost,
        }


        #region Default properties
        [FieldSettings("Invoice ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Reason", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripeDisputesReason))]
        public StripeDisputesReason reason { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripeDisputesStatus))]
        public StripeDisputesStatus status { get; set; }
        #endregion

        #region General properties
        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("charge refundable", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool is_charge_refundable { get; set; }

        [FieldSettings("Has evidence", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool has_evi => evidence_details != null && evidence_details.has_evidence;

        [FieldSettings("Evidence past due", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? past_due => evidence_details?.past_due;

        [FieldSettings("Number of evidence submissions", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? submission_count => evidence_details != null ? evidence_details.submission_count : 0;

        #endregion

        #region Hidden properties

        public decimal amount { get; set; }
        public StripeDisputesEvidenceDetails evidence_details { get; set; }
        public long created { get; set; }
        public StripeDisputes disputesColl { get; set; }
        public long evidence_due_by { get; set; }
        
            #endregion

        #region Calculations
        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal amt => amount / 100;

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Evidence due by date", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public DateTime? due_by_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(evidence_due_by);
            }
        }
        #endregion
    }
}
