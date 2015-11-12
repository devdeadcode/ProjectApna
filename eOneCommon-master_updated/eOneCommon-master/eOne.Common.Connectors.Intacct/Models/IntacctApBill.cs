using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctApBill : IntacctBase
    {

        #region Enums

        public enum IntacctApBillPaymentPriority
        {
            [Description("Low")]
            low,
            [Description("Normal")]
            normal,
            [Description("High")]
            high,
            [Description("Urgent")]
            urgent
        }

        #endregion

        #region Default properties

        [FieldSettings("Vendor name", DefaultField = true)]
        public string VENDORNAME { get; set; }

        [FieldSettings("Bill number", DefaultField = true, KeyNumber = 1, Description = true)]
        public string RECORDID { get; set; }

        [FieldSettings("Transaction amount", FieldTypeId = Connector.FieldTypeIdCurrency, DefaultField = true)]
        public decimal? TRX_TOTALENTERED { get; set; }

        [FieldSettings("Base amount", FieldTypeId = Connector.FieldTypeIdCurrency, DefaultField = true)]
        public decimal? TOTALENTERED { get; set; }

        [FieldSettings("State", DefaultField = true)]
        public string STATE { get; set; }

        #endregion

        #region Properties

        public string PRBATCH { get; set; }

        [FieldSettings("Vendor ID")]
        public string VENDORID { get; set; }

        [FieldSettings("GL posting date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENPOSTED { get; set; }

        [FieldSettings("Discount date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENDISCOUNT { get; set; }

        [FieldSettings("Due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENDUE { get; set; }

        [FieldSettings("Paid date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENPAID { get; set; }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime? AUWHENCREATED { get; set; }

        [FieldSettings("Exchange rate date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EXCHRATEDATE { get; set; }

        [FieldSettings("Date payment received", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? RECPAYMENTDATE { get; set; }

        [FieldSettings("Base currency")]
        public string BASECURR { get; set; }

        [FieldSettings("Currency")]
        public string CURRENCY { get; set; }

        public decimal? TOTALSELECTED { get; set; }
        public decimal? TOTALPAID { get; set; }
        public decimal? TOTALDUE { get; set; }
        public decimal? TRX_TOTALSELECTED { get; set; }
        public decimal? TRX_TOTALPAID { get; set; }
        public decimal? TRX_TOTALDUE { get; set; }
        public decimal? TRX_ENTITYDUE { get; set; }

        [FieldSettings("Exchange rate", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? EXCHRATE { get; set; }

        [FieldSettings("Reference number")]
        public string DOCNUMBER { get; set; }

        [FieldSettings("Description")]
        public string DESCRIPTION { get; set; }

        [FieldSettings("Description 2")]
        public string DESCRIPTION2 { get; set; }

        
        public string TERMNAME { get; set; }
        public string BILLTOPAYTOCONTACTNAME { get; set; }
        public string SHIPTORETURNTOCONTACTNAME { get; set; }
        public string PAYTOCONTACTNAME { get; set; }
        public string RETURNTOCONTACTNAME { get; set; }
        public bool? ONHOLD { get; set; }

        [FieldSettings("Payment priority", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(IntacctApBillPaymentPriority))]
        public IntacctApBillPaymentPriority PAYMENTPRIORITY { get; set; }

        #endregion

        public string FORM1099TYPE { get; set; }
        public string VENDTYPE1099TYPE { get; set; }

        #region Hidden fields

        public string EXCHRATETYPE { get; set; }
        public string RECORDTYPE { get; set; }
        public string FINANCIALENTITY { get; set; }
        public string RAWSTATE { get; set; }
        public string SYSTEMGENERATED { get; set; }

        #endregion

    }
}
