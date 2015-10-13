using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroExpenseClaim : DataConnectorEntityModel
    {

        #region Enums

        public enum XeroExpenseClaimStatus
        {
            [Description("Submitted")]
            SUBMITTED,
            [Description("Authorised")]
            AUTHORISED,
            [Description("Paid")]
            PAID
        }

        #endregion

        #region Default properties

        [FieldSettings("Expense claim ID", DefaultField = true)]
        public string ExpenseClaimID { get; set; }

        [FieldSettings("User name", DefaultField = true)]
        public string UserName => User.Name;

        [FieldSettings("Total", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal Total { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroExpenseClaimStatus))]
        public XeroExpenseClaimStatus Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Amount due", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal AmountDue { get; set; }

        [FieldSettings("Amount paid", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal AmountPaid { get; set; }

        [FieldSettings("Payment due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime PaymentDueDate { get; set; }

        [FieldSettings("Reporting date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime ReportingDate { get; set; }

        #endregion

        #region Hidden properties

        public List<XeroReceipt> Receipts { get; set; }
        public XeroUser User { get; set; }
        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Email address", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string UserEmail => User.EmailAddress;

        [FieldSettings("Role", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroUser.XeroUserRole))]
        public XeroUser.XeroUserRole OrganisationRole { get; set; }

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate, Modified = true)]
        public DateTime UpdatedDate => UpdatedDateUTC.Date;

        [FieldSettings("Updated time", FieldTypeId = DataConnector.FieldTypeIdTime, Modified = true)]
        public DateTime UpdatedTime => Time(UpdatedDateUTC);

        [FieldSettings("Total tax", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalTax
        {
            get
            {
                return Receipts.Sum(receipt => receipt.TotalTax);
            }
        }

        [FieldSettings("User ID")]
        public string UserID => User.UserID;

        #endregion

    }
}