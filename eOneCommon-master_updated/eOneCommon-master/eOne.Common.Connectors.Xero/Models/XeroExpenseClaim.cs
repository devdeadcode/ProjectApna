using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroExpenseClaim : ConnectorEntityModel
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

        [FieldSettings("Total", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal Total { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroExpenseClaimStatus))]
        public XeroExpenseClaimStatus Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Amount due", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal AmountDue { get; set; }

        [FieldSettings("Amount paid", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal AmountPaid { get; set; }

        [FieldSettings("Payment due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime PaymentDueDate { get; set; }

        [FieldSettings("Reporting date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime ReportingDate { get; set; }

        [FieldSettings("Updated date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Hidden properties

        public List<XeroReceipt> Receipts { get; set; }
        public XeroUser User { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Email address", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string UserEmail => User.EmailAddress;

        [FieldSettings("Role", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroUser.XeroUserRole))]
        public XeroUser.XeroUserRole OrganisationRole { get; set; }

        [FieldSettings("Updated time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime UpdatedTime => UpdatedDateUTC;

        [FieldSettings("Total tax", FieldTypeId = Connector.FieldTypeIdCurrency)]
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