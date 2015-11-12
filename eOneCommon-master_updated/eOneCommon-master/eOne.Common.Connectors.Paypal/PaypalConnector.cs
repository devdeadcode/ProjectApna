using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Paypal.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Paypal
{
    public class PaypalConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityIdPayment = 1;
        public const int EntityIdBillingPlan = 2;
        public const int EntityIdTransaction = 3;
        public const int EntityIdInvoice = 4;
        public const int EntityIdInvoiceItems = 5;

        #endregion

        public PaypalConnector()
        {
            Name = "PayPal";
            Group = ConnectorGroup.Payments;
            BaseUrl = "https://api.paypal.com/v1/";

            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;

            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();
            AddEntity(EntityIdPayment, "Payments", typeof(PaypalPayment));
            AddEntity(EntityIdBillingPlan, "Billing plans", typeof(PaypalBillingPlan));
            AddEntity(EntityIdTransaction, "Transactions", typeof(PaypalTransaction));
            var invoiceEntity = AddEntity(EntityIdInvoice, "Invoices", typeof(PaypalInvoice));
            var invoiceLineEntity = AddEntity(EntityIdInvoice, "Invoice items", typeof(PaypalInvoiceItem));
            invoiceEntity.AddRelatedEntity("Invoice items", invoiceLineEntity, "invoice_number", "number");
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdPayment:
                case EntityIdTransaction:
                    return "payments/payment";
                case EntityIdBillingPlan:
                    var restriction = query.FindRestrictionByFieldAndType("status", ConnectorRestriction.ConnectorRestrictionType.Equals);
                    return restriction != null ? $"payments/billing-plans?status={restriction.Values[0].Constant}" : "payments/billing-plans";
                case EntityIdInvoice:
                case EntityIdInvoiceItems:
                    return "invoicing/invoices";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdPayment:
                    return DeserializeJson<List<PaypalPayment>>(data);
                case EntityIdBillingPlan:
                    return DeserializeJson<List<PaypalBillingPlan>>(data);
                case EntityIdTransaction:
                case EntityIdInvoice:
                    return DeserializeJson<List<PaypalInvoice>>(data);
                case EntityIdInvoiceItems:
                    var invoices = DeserializeJson<List<PaypalInvoice>>(data);
                    var invoiceItems = new List<PaypalInvoiceItem>();
                    foreach (var invoice in invoices)
                    {
                        foreach (var item in invoice.items)
                        {
                            item.invoice = invoice;
                            invoiceItems.Add(item);
                        }
                    }
                    return invoiceItems;
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
