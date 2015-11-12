using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.Stripe.Helpers;
using eOne.Common.Connectors.Stripe.Models;
using eOne.Common.DataConnectors.Rest;
using Newtonsoft.Json.Linq;

namespace eOne.Common.Connectors.Stripe
{
    public class StripeConnector : RestConnector
    {
        #region Entity IDs
        public const int EntityIdCustomer = 1;
        public const int EntityIdCharge = 2;
        public const int EntityIdDispute = 3;
        public const int EntityIdInvoice = 4;
        public const int EntityIdInvoiceItem = 5;
        public const int EntityIdOrder = 6;
        public const int EntityIdOrderItem = 7;
        public const int EntityIdRefund = 8;
        public const int EntityIdCoupon = 9;
        public const int EntityIdProduct = 10;
        public const int EntityIdSKU = 11;
        public const int EntityIdSubscriptionPlan = 12;
        public const int EntityIdSubscription = 13;
        public const int EntityIdTransfer = 14;
        public const int EntityIdTransferReversal = 15;
        #endregion

        public StripeConnector()
        {
            Name = "Stripe";
            Group = ConnectorGroup.Payments;
            AuthenticationType = RestConnectorAuthenticationType.Basic;
            BaseUrl = "https://api.stripe.com/v1/";
            SerializationType = RestConnectorSerializationType.Json;
            AddSetup();
        }

        public override void Initialise()
        {
            Username = Key;
            Password = Token;

            //add entities
            var customerEntity = AddEntity(EntityIdCustomer, "Customers", typeof(StripeCustomers));
            var chargeEntity = AddEntity(EntityIdCharge, "Charges", typeof(StripeCharges));
            var couponEntity = AddEntity(EntityIdCoupon, "Coupons", typeof(StripeCustomerDiscountCoupon));
            AddEntity(EntityIdDispute, "Disputes", typeof(StripeDisputesData));
            var invoiceEnitity = AddEntity(EntityIdInvoice, "Invoices", typeof(StripeInvoice));
            var invoiceItemEntity = AddEntity(EntityIdInvoiceItem, "Invoice items", typeof(StripeInvoiceLineData));
            var ordersEntity = AddEntity(EntityIdOrder, "Orders", typeof(StripeOrder));
            var orderItemsEntity = AddEntity(EntityIdOrderItem, "Order items", typeof(StripeOrderItem));
            var subscriptionsEntity = AddEntity(EntityIdSubscription, "Subscriptions",
                typeof (StripeSubscriptionsData));
            var productsEntity = AddEntity(EntityIdProduct, "Products", typeof (StripeProducts));
            var skuEntity = AddEntity(EntityIdSKU, "SKUs", typeof (StripeProductSKUData));
            var refundsEntity = AddEntity(EntityIdRefund, "Refunds", typeof(StripeChargeRefunds));
            var plansEntity = AddEntity(EntityIdSubscriptionPlan, "Subscription plans", typeof(StripePlans));
            var transferEntity = AddEntity(EntityIdTransfer, "Transfers", typeof (StripeTransfer));
            var transferReversalEntity = AddEntity(EntityIdTransferReversal, "Transfer reversals", typeof(StripeTransferReversalsData));

            //set default max records
            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;

            //Add relationships
            chargeEntity.AddRelatedEntity("Refunds", refundsEntity, "id", "charge_id");
            customerEntity.AddRelatedEntity("Invoices", invoiceEnitity, "id", "customer_id");
            customerEntity.AddRelatedEntity("Orders", ordersEntity, "id", "customer_id");
            customerEntity.AddRelatedEntity("Charges", chargeEntity, "id", "customer_id");
            invoiceEnitity.AddRelatedEntity("Invoice items", invoiceItemEntity, "id", "invoice_id");
            ordersEntity.AddRelatedEntity("Order items", orderItemsEntity, "id", "order_id");
            productsEntity.AddRelatedEntity("SKUs", skuEntity, "id", "sku_id");
            plansEntity.AddRelatedEntity("Subscriptions", subscriptionsEntity, "id", "plan_id");
            transferEntity.AddRelatedEntity("Reversals", transferReversalEntity, "id", "transfer_id");

            // add favorites
            StripeFavoriteHelper.AddCustomerFavorites(customerEntity);
            StripeFavoriteHelper.AddChargesFavorites(chargeEntity);
            StripeFavoriteHelper.AddCouponFavorites(couponEntity);
            StripeFavoriteHelper.AddInvoiceFavorite(invoiceEnitity);
            StripeFavoriteHelper.AddOrderFavorite(ordersEntity);
            StripeFavoriteHelper.AddSKUFavorite(skuEntity);
            StripeFavoriteHelper.AddSubscriptionFavorite(subscriptionsEntity);
            StripeFavoriteHelper.AddTransferFavorite(transferEntity);
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdCustomer:
                    return "customers";
                case EntityIdCharge:
                    return "charges";
                case EntityIdCoupon:
                    return "customers";
                case EntityIdDispute:
                    return "disputes";
                case EntityIdInvoice:
                case EntityIdInvoiceItem:
                    return "invoices";
                case EntityIdOrder:
                case EntityIdOrderItem:
                    return "orders";
                case EntityIdProduct:
                    return "products";
                case EntityIdSKU:
                    return "products";
                case EntityIdRefund:
                    return "charges";
                case EntityIdSubscriptionPlan:
                    return "plans";
                case EntityIdSubscription:
                    return "customers";
                case EntityIdTransfer:
                case EntityIdTransferReversal:
                    return "transfers";
            }
            
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdCustomer:
                    data = "[\n    " + data + "    \n]";
                    var customerResult = DeserializeJson<List<StripeCustomerCollections>>(data);
                    var customerResponse = new List<StripeCustomers>();

                    foreach (var inst in customerResult[0].data)
                    {
                        inst.customerColl = customerResult[0];
                        customerResponse.Add(inst);
                    }
                    return customerResponse;

                case EntityIdCharge:
                    data = "[\n    " + data + "    \n]";
                    var chargeResult = DeserializeJson<List<StripeChargesCollection>>(data);
                    var chargeResponse = new List<StripeCharges>();

                    var customerData = GetResponse("customers");
                    customerData = "[\n    " + customerData + "    \n]";
                    var customers = DeserializeJson<List<StripeCustomerCollections>>(customerData);

                    foreach (var inst in chargeResult[0].data)
                    {
                        inst.chargesCollection = chargeResult[0];
                        foreach (var val in customers[0].data.Where(val => val.id == inst.customer))
                            inst.customersColl = val;
                        chargeResponse.Add(inst);
                    }
                    return chargeResponse;

                case EntityIdCoupon:
                    data = "[\n    " + data + "    \n]";
                    var couponResult = DeserializeJson<List<StripeCustomerCollections>>(data);
                    var customerList = new List<StripeCustomers>();
                    var couponResponse = new List<StripeCustomerDiscountCoupon>();

                    foreach (var inst in couponResult[0].data)
                    {
                        inst.customerColl = couponResult[0];
                        customerList.Add(inst);
                    }

                    foreach (var t in customerList)
                    {
                        foreach (var t1 in t.subscriptions.data)
                        {
                            t1.discount.coupon.cm = t;
                            couponResponse.Add(t1.discount.coupon);
                        }
                    }
                    return couponResponse;

                case EntityIdDispute:
                    data = "[\n    " + data + "    \n]";
                    var disputeResult = DeserializeJson<List<StripeDisputes>>(data);
                    var disputeResponse = new List<StripeDisputesData>();

                    foreach (var inst in disputeResult[0].data)
                    {
                        inst.disputesColl = disputeResult[0];
                        disputeResponse.Add(inst);
                    }

                    return disputeResponse;

                case EntityIdInvoice:
                    data = "[\n    " + data + "    \n]";
                    var invoiceResult = DeserializeJson<List<StripeInvoiceCollection>>(data);
                    var invoiceResponse = new List<StripeInvoice>();

                    var cmsData = GetResponse("customers");
                    cmsData = "[\n    " + cmsData + "    \n]";
                    var cms = DeserializeJson<List<StripeCustomerCollections>>(cmsData);

                    foreach (var inst in invoiceResult[0].data)
                    {
                        inst.invoiceColl = invoiceResult[0];
                        foreach (var val in cms[0].data.Where(val => val.id == inst.customer))
                            inst.customerColl = val;
                        invoiceResponse.Add(inst);
                    }

                    return invoiceResponse;

                case EntityIdInvoiceItem:
                    data = "[\n    " + data + "    \n]";
                    var invoiceItemResult = DeserializeJson<List<StripeInvoiceCollection>>(data);
                    var invoiceItemResponse = new List<StripeInvoiceLineData>();

                    var cmsItemData = GetResponse("customers");
                    cmsItemData = "[\n    " + cmsItemData + "    \n]";
                    var cmsItem = DeserializeJson<List<StripeCustomerCollections>>(cmsItemData);

                    foreach (var inst in invoiceItemResult[0].data)
                    {
                        foreach (var val in cmsItem[0].data)
                        {
                            if (val.id != inst.customer) continue;
                            foreach (var line in inst.lines.data)
                            {
                                line.customerColl = val;
                                line.invoice = inst;
                                invoiceItemResponse.Add(line);
                            }
                        }
                    }
                    return invoiceItemResponse;

                case EntityIdOrder:
                    data = "[\n    " + data + "    \n]";
                    var orderResult = DeserializeJson<List<StripeOrderCollection>>(data);

                    return orderResult[0].data.ToList();

                case EntityIdOrderItem:
                    data = "[\n    " + data + "    \n]";
                    var orderItemResult = DeserializeJson<List<StripeOrderCollection>>(data);

                    return orderItemResult[0].data.SelectMany(inst => inst.items).ToList();

                case EntityIdSubscription:
                    data = "[\n    " + data + "    \n]";
                    var subscriptionResult = DeserializeJson<List<StripeCustomerCollections>>(data);
                    var subscriptionResponse = new List<StripeSubscriptionsData>();

                    foreach (var inst in subscriptionResult[0].data)
                    {
                        inst.customerColl = subscriptionResult[0];
                        foreach (var val in inst.subscriptions.data)
                        {
                            val.customers = inst;
                            subscriptionResponse.Add(val);
                        }
                    }
                    return subscriptionResponse;

                case EntityIdProduct:
                    data = "[\n    " + data + "    \n]";
                    var productResult = DeserializeJson<List<StripeProductCollection>>(data);
                    return productResult[0].data.ToList();

                case EntityIdSKU:
                    var jObj = JObject.Parse(data);
                    var projNames = new List<string>();
                    var projValues = new List<string>();
                    var skuData = new List<StripeProductSKUData>();

                    for(var i=0; i< jObj["data"].Count();i++)
                    {
                        for (var j = 0; j < jObj["data"][i]["skus"]["data"].Count(); j++)
                        {
                            projNames = jObj["data"][i]["skus"]["data"][j]["attributes"].Select(x => ((JProperty)x).Name).ToList();
                            projValues = jObj["data"][i]["skus"]["data"][j]["attributes"].Select(x => ((JProperty)x).Value.ToString()).ToList();
                        }
                    }

                    data = "[\n    " + data + "    \n]";
                    var productSKUResult = DeserializeJson<List<StripeProductCollection>>(data);

                    foreach (var val1 in productSKUResult[0].data.SelectMany(inst => inst.skus.data))
                    {
                        foreach (var name in projNames)
                        {
                            val1.names.Add(name);
                        }
                        foreach (var value in projValues)
                        {
                            val1.values.Add(value);
                        }
                        skuData.Add(val1);
                    }
                    return skuData;

                case EntityIdRefund:
                    data = "[\n    " + data + "    \n]";
                    var refundResult = DeserializeJson<List<StripeChargesCollection>>(data);
                    var refundResponse = new List<StripeChargeRefunds>();
                    var chargesList = new List<StripeCharges>();

                    foreach (var inst in refundResult[0].data)
                    {
                        inst.chargesCollection = refundResult[0];
                        chargesList.Add(inst);
                    }

                    foreach (var t in chargesList)
                    {
                        foreach (var t1 in t.refunds)
                        {
                            t1.ch = t;
                            refundResponse.Add(t1);
                        }
                    }
                    return refundResponse;

                case EntityIdSubscriptionPlan:
                    data = "[\n    " + data + "    \n]";
                    var planResult = DeserializeJson<List<StripePlanCollection>>(data);
                    var planResponse = new List<StripePlans>();

                    foreach (var inst in planResult[0].data)
                    {
                        inst.planColl = planResult[0];
                        planResponse.Add(inst);
                    }
                    return planResponse;

                case EntityIdTransfer:
                    data = "[\n    " + data + "    \n]";
                    var transferResult = DeserializeJson<List<StripeTransferCollection>>(data);
                    return transferResult[0].data.ToList();

                case EntityIdTransferReversal:
                    data = "[\n    " + data + "    \n]";
                    var transferReversalResult = DeserializeJson<List<StripeTransferCollection>>(data);

                    return transferReversalResult[0].data.SelectMany(inst => inst.reversals.data).ToList();
            }

            return null;
        }
    }
}
