using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.Xero.Models;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Xero
{
    public class XeroConnector : RestConnector
    {

        #region Constants

        public const int EntityIdItem = 1;
        public const int EntityIdInvoice = 2;
        public const int EntityIdInvoiceLine = 3;
        public const int EntityIdContact = 4;
        public const int EntityIdJournal = 5;
        public const int EntityIdJournalLine = 6;
        public const int EntityIdEmployee = 7;
        public const int EntityIdExpenseClaim = 8;
        public const int EntityIdCreditNote = 9;
        public const int EntityIdPrepayment = 10;
        public const int EntityIdOverpayment = 11;
        public const int EntityIdBankTransaction = 12;
        public const int EntityIdBankTransfer = 13;
        public const int EntityIdAccount = 14;
        public const int EntityIdContactGroup = 15;
        public const int EntityIdCurrency = 16;
        public const int EntityIdOrganisation = 17;
        public const int EntityIdReceipt = 18;
        public const int EntityIdReceiptLine = 19;
        public const int EntityIdCreditNoteLine = 20;
        public const int EntityIdPrepaymentLine = 21;
        public const int EntityIdOverpaymentLine = 22;
        public const int EntityIdUser = 23;
        public const int EntityIdContactPerson = 24;

        #endregion

        public XeroConnector()
        {
            Name = "Xero";
            Group = ConnectorGroup.ERP;
            BaseUrl = "https://api.xero.com/api.xro/2.0/";

            AuthenticationType = ServiceConnectorAuthenticationType.OAuth1;
            AccessTokenUri = "https://api.xero.com/oauth/AccessToken";
            AuthorizationUri = "https://api.xero.com/oauth/Authorize";
            Key = "QF8SIYMF9XE2WITIFPM6N0OQKECCEO";
            Secret = "S4AUYFBF0PBMSP4WTGP6CZJJPRR5XE";

            // rate limits - 1000 requests per day and 60 requests per minute
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 1000, ServiceConnectorRateLimiting.LimitPeriod.Day);
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 60, ServiceConnectorRateLimiting.LimitPeriod.Minute);
        }
        
        public override void Initialise()
        {
            base.Initialise();
            AddEntities();
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
                case EntityIdItem:
                    return "Items";
                case EntityIdContact:
                case EntityIdContactPerson:
                    return "Contacts";
                case EntityIdJournal:
                case EntityIdJournalLine:
                    return "Journals";
                case EntityIdAccount:
                    return "Accounts";
                case EntityIdUser:
                    return "Users";
                case EntityIdEmployee:
                    return "Employees";
                case EntityIdInvoice:
                case EntityIdInvoiceLine:
                    return "Invoices";
                case EntityIdBankTransfer:
                    return "BankTransfers";
                case EntityIdExpenseClaim:
                    return "ExpenseClaims";
                case EntityIdReceipt:
                case EntityIdReceiptLine:
                    return "Receipts";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            if (query == null || string.IsNullOrWhiteSpace(data)) return null;
            switch (query.Entity.Id)
            {
                case EntityIdItem:
                    return DeserializeXml<List<XeroItem>>(data);
                case EntityIdContact:
                    return DeserializeXml<List<XeroContact>>(data);
                case EntityIdContactPerson:
                    return DeserializeContactPeople(data);
                case EntityIdJournal:
                    return DeserializeXml<List<XeroJournal>>(data);
                case EntityIdJournalLine:
                    return DeserializeJournalLines(data);
                case EntityIdInvoice:
                    return DeserializeXml<List<XeroInvoice>>(data);
                case EntityIdInvoiceLine:
                    return DeserializeInvoiceLines(data);
                case EntityIdBankTransfer:
                    return DeserializeXml<List<XeroBankTransfer>>(data);
                case EntityIdExpenseClaim:
                    return DeserializeXml<List<XeroExpenseClaim>>(data);
                case EntityIdReceipt:
                    return DeserializeXml<List<XeroReceipt>>(data);
                case EntityIdReceiptLine:
                    return DeserializeReceiptLines(data);
            }
            return null;
        }

        private static IEnumerable<XeroContactPerson> DeserializeContactPeople(string data)
        {
            var contacts = DeserializeXml<List<XeroContact>>(data);
            var people = new List<XeroContactPerson>();
            foreach (var contact in contacts)
            {
                foreach (var person in contact.ContactPersons)
                {
                    person.Contact = contact;
                    people.Add(person);
                }
            }
            return people;
        }
        private static IEnumerable<XeroJournalLine> DeserializeJournalLines(string data)
        {
            var journals = DeserializeXml<List<XeroJournal>>(data);
            var lines = new List<XeroJournalLine>();
            foreach (var journal in journals)
            {
                foreach (var line in journal.JournalLines)
                {
                    line.Journal = journal;
                    lines.Add(line);
                }
            }
            return lines;
        }
        private static IEnumerable<XeroInvoiceLineItem> DeserializeInvoiceLines(string data)
        {
            var invoices = DeserializeXml<List<XeroInvoice>>(data);
            var lines = new List<XeroInvoiceLineItem>();
            foreach (var invoice in invoices)
            {
                foreach (var line in invoice.LineItems)
                {
                    line.Invoice = invoice;
                    lines.Add(line);
                }
            }
            return lines;
        }
        private static IEnumerable<XeroReceiptLineItem> DeserializeReceiptLines(string data)
        {
            var receipts = DeserializeXml<List<XeroReceipt>>(data);
            var lines = new List<XeroReceiptLineItem>();
            foreach (var receipt in receipts)
            {
                foreach (var line in receipt.LineItems)
                {
                    line.Receipt = receipt;
                    lines.Add(line);
                }
            }
            return lines;
        }

        private void AddEntities()
        {
            AddEntity(EntityIdItem, "Items", typeof(XeroItem));
            var contactEntity = AddEntity(EntityIdContact, "Contacts", typeof(XeroContact));
            var contactPersonEntity = AddEntity(EntityIdContactPerson, "Contact people", typeof(XeroContactPerson));
            AddEntity(EntityIdAccount, "Accounts", typeof(XeroAccount));
            var userEntity = AddEntity(EntityIdUser, "Users", typeof(XeroUser));
            AddEntity(EntityIdEmployee, "Employees", typeof(XeroEmployee));
            var journalEntity = AddEntity(EntityIdJournal, "Journals", typeof(XeroJournal));
            var journalLineEntity = AddEntity(EntityIdJournalLine, "Journal lines", typeof(XeroJournalLine));
            var invoiceEntity = AddEntity(EntityIdInvoice, "Invoices", typeof(XeroInvoice));
            var invoiceLineEntity = AddEntity(EntityIdInvoiceLine, "Invoice lines", typeof(XeroInvoiceLineItem));
            AddEntity(EntityIdBankTransfer, "Bank transfers", typeof(XeroBankTransfer));
            var expenseClaimEntity = AddEntity(EntityIdExpenseClaim, "Expense claims", typeof(XeroExpenseClaim));
            var receiptEntity = AddEntity(EntityIdReceipt, "Receipts", typeof(XeroReceipt));
            var receiptLineEntity = AddEntity(EntityIdReceiptLine, "Receipt lines", typeof(XeroReceiptLineItem));

            contactEntity.AddRelatedEntity("Contact people", contactPersonEntity, "ContactID", "ContactID");
            contactEntity.AddRelatedEntity("Invoices", contactPersonEntity, "ContactID", "ContactID");
            journalEntity.AddRelatedEntity("Journal lines", journalLineEntity, "JournalNumber", "JournalNumber");
            invoiceEntity.AddRelatedEntity("Invoice lines", invoiceLineEntity, "InvoiceID", "InvoiceID");
            receiptEntity.AddRelatedEntity("Receipt lines", receiptLineEntity, "ReceiptID", "ReceiptID");
            userEntity.AddRelatedEntity("Expense claims", expenseClaimEntity, "UserID", "UserID");

        }

    }
}
