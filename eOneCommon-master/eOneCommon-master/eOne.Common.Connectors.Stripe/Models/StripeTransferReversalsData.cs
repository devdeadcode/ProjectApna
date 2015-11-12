using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeTransferReversalsData : DataConnectorEntityModel
    {
        #region Default properties
        [FieldSettings("Reversal ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Transfer ID", DefaultField = true)]
        public string transfer_id => transfer.id;
        
        #endregion

        #region General properties
        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Bank name")]
        public string bank_name => transfer.bank_name;

        [FieldSettings("Routing number")]
        public string routing_number => transfer.routing_number;

        [FieldSettings("Bank country")]
        public string bank_country => transfer.bank_country;

        [FieldSettings("Transfer description")]
        public string transfer_description => transfer.description;

        [FieldSettings("Transfer status", FieldTypeId = DataConnector.FieldTypeIdEnum,
            EnumType = typeof(StripeTransfer.TransferStatus))]
        public StripeTransfer.TransferStatus status => transfer.status;

        [FieldSettings("Transfer type", FieldTypeId = DataConnector.FieldTypeIdEnum,
            EnumType = typeof(StripeTransfer.TransferType))]
        public StripeTransfer.TransferType tpye => transfer.type;

        #endregion

        #region Hidden properties
        public StripeTransfer transfer { get; set; }
        public decimal amount { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? amt => amount / 100;

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? created_date => transfer.created_date;

        [FieldSettings("Transfer date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? transfer_date => transfer.transfer_date;

        [FieldSettings("Transfer amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? transfer_amt => transfer.amount;

        #endregion
    }
}
