using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    public class DynamicsGpSalesModule : DynamicsGpModule
    {

        private const short GpSmartListCustomers = 2;
        private const short GpSmartListSalesTrx = 6;
        private const short GpSmartListReceivablesTrx = 8;
        private const short GpSmartListCustomerAddresses = 15;
        private const short GpSmartListProspects = 17;
        private const short GpSmartListSalesLineItems = 20;

        public DynamicsGpSalesModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 2;
            ProductId = 0;
            Name = "Sales";
            Installed = true;
            UserDefined1 = "User defined 1";
            UserDefined2 = "User defined 2";
            AgingPeriods = new List<string>();
            // get user defined
            // get aging buckets 
            ParentConnector = connector;
        }

        public string UserDefined1 { get; set; }
        public string UserDefined2 { get; set; }
        public List<string> AgingPeriods { get; set; }

        public override void AddEntities()
        {
            Entities.Add(GetCustomerEntity());
            Entities.Add(GetCustomerAddressEntity());
            Entities.Add(GetProspectEntity());
            Entities.Add(GetSalesTransactionEntity());
            Entities.Add(GetSalesLineItemEntity());
            Entities.Add(GetReceivablesTransactionEntity());
        }

        private ConnectorEntity GetCustomerEntity()
        {
            var entity = new ConnectorEntity(GpSmartListCustomers, "Customers", ParentConnector);
            
            var rm00101 = entity.AddTable("RM00101");
            
            var rm00103 = entity.AddTable("RM00103", "RM00101", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddCustomerEntityFields(rm00101, rm00103);
            
            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority", Connector.FieldTypeIdString);

            return entity;
        }
        private void AddCustomerEntityFields(ConnectorTable rm00101, ConnectorTable rm00103)
        {
            var customerNumber = rm00101.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            customerNumber.KeyNumber = 1;

            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString, true);
            rm00101.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString, true);
            rm00101.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString, true);
            rm00101.AddField("CITY", "City", Connector.FieldTypeIdString, true);
            rm00101.AddField("STATE", "State", Connector.FieldTypeIdString, true);
            rm00101.AddField("ZIP", "Zip", Connector.FieldTypeIdString, true);
            rm00101.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone, true);
            rm00101.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate customer number", Connector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact person", Connector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement name", Connector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            rm00101.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary bill to address code", Connector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary ship to address code", Connector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement address code", Connector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit limit amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit limit period", Connector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit limit period amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer discount", Connector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance charge percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance charge amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Maximum writeoff amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment 1", Connector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment 2", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", UserDefined1, Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", UserDefined2, Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax exempt 1", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax exempt 2", Connector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank branch", Connector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales territory", Connector.FieldTypeIdString);
            rm00101.AddField("FRSTINDT", "First invoice date", Connector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit card ID", Connector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit card expiry date", Connector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep distribution history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total amount of NSF checks life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number of NSF checks life", Connector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer balance", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last aged", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF check date", Connector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last payment amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last payment date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last transaction date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last transaction amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last finance charge amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average days to pay - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average days to pay - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average days to pay - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number of ADTP documents - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number of ADTP documents - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number of ADTP documents - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total discounts taken - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total discounts taken - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total discounts taken - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total discounts available - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total amount of NSF checks - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number of NSF checks - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted other sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted other cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non current scheduled payments", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total sales - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total sales - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total sales - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total costs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total costs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total costs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total cash received - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total cash received - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total cash received - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total finance charges - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total finance charges - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance charges - calendar year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance charges - last calendar year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total bad debt - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total bad debt - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total bad debt - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total waived finance changes - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total waived finance changes - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total waived finance changes - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total writeoffs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total writeoffs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total writeoffs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total number of invoices - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total number of invoices - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total number of invoices - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Total number of finance changes - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Total number of finance changes - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Total number of finance changes - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write offs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write offs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write offs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High balance - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High balance - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High balance - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last statement date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last statement amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits received", Connector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On order amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue customer", Connector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance charge ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total returns - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total returns - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total returns - year to date", Connector.FieldTypeIdCurrency);
            rm00101.AddField("Send_Email_Statements", "Send email statements", Connector.FieldTypeIdYesNo);
            rm00101.AddField("SHIPCOMPLETE", "Ship complete document", Connector.FieldTypeIdYesNo);
            rm00101.AddField("RMCSHACC", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts receivable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSORACC", "Sales order returns account number", DynamicsGpConnector.FieldTypeIdAccountIndex);

            // add aging periods
            int periodNumber = 1;
            foreach (string period in AgingPeriods)
            {
                rm00103.AddField($"AGPERAMT_{periodNumber}", period, Connector.FieldTypeIdCurrency);
                periodNumber++;
            }

            // add list fields
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit limit type", Connector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });

            var minPaymentType = rm00101.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });

            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance charge amount type", Connector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });

            var maxWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum writeoff type", Connector.FieldTypeIdEnum);
            maxWriteoffType.AddListItems(0, new List<string> { "Not allowed", "Unlimited", "Maximum" });

            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", Connector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open item", "Balance forward" });

            var statementCycle = rm00101.AddField("STMTCYCL", "Statement cycle", Connector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });

            var defaultCashType = rm00101.AddField("DEFCACTY", "Default cash account type", Connector.FieldTypeIdEnum);
            defaultCashType.AddListItems(0, new List<string> { "Checkbook", "Customer" });

            var postResultsTo = rm00101.AddField("Post_Results_To", "Post results to", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/discount account", "Sales offset account" });

            var orderFulfillmentShortage = rm00101.AddField("ORDERFULFILLDEFAULT", "Order fulfillment shortage default", Connector.FieldTypeIdEnum);
            orderFulfillmentShortage.AddListItems(1, new List<string> { "None", "Back order remaining", "Cancel remaining" });
        }

        private ConnectorEntity GetCustomerAddressEntity()
        {
            var entity = new ConnectorEntity(GpSmartListCustomerAddresses, "Customer addresses", ParentConnector);

            var rm00102 = entity.AddTable("RM00102");

            var rm00101 = entity.AddTable("RM00101", "RM00102", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var sy01200 = entity.AddScript("select * from {0}..SY01200 with (NOLOCK) where Master_Type = 'CUS'", "SY01200", "RM00102");
            sy01200.AddJoinFields("Master_ID", "CUSTNMBR");
            sy01200.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddCustomerAddressEntityFields(rm00102, rm00101, sy01200);

            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority", Connector.FieldTypeIdString);  

            return entity;
        }
        private static void AddCustomerAddressEntityFields(ConnectorTable rm00102, ConnectorTable rm00101, ConnectorTable sy01200)
        {
            var customerNumber = rm00102.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            customerNumber.KeyNumber = 1;

            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString, true);
            rm00102.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString, true);
            rm00102.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString, true);
            rm00102.AddField("CITY", "City", Connector.FieldTypeIdString, true);
            rm00102.AddField("STATE", "State", Connector.FieldTypeIdString, true);
            rm00102.AddField("ZIP", "Zip", Connector.FieldTypeIdString, true);
            rm00102.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone, true);
            rm00101.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate customer number", Connector.FieldTypeIdString);
            rm00102.AddField("CNTCPRSN", "Contact person", Connector.FieldTypeIdString);

            var addressCode = rm00102.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            addressCode.KeyNumber = 2;

            rm00102.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            rm00102.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            rm00102.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            rm00102.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            rm00102.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            rm00102.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            rm00102.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            rm00102.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);

            var modifyDate = rm00102.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            modifyDate.ModifyDate = true;

            var createDate = rm00102.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            createDate.CreateDate = true;

            sy01200.AddField("INET1", "INet1", Connector.FieldTypeIdString);
            sy01200.AddField("INET2", "INet2", Connector.FieldTypeIdString);
            sy01200.AddField("INET3", "INet3", Connector.FieldTypeIdString);
            sy01200.AddField("INET4", "INet4", Connector.FieldTypeIdString);
            sy01200.AddField("INET5", "INet5", Connector.FieldTypeIdString);
            sy01200.AddField("INET6", "INet6", Connector.FieldTypeIdString);
            sy01200.AddField("INET7", "INet7", Connector.FieldTypeIdString);
            sy01200.AddField("INET8", "INet8", Connector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement name", Connector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            rm00101.AddField("PRBTADCD", "Primary bill to address code", Connector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary ship to address code", Connector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement address code", Connector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit limit amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit limit period", Connector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit limit period amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer discount", Connector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance charge percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance charge amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max writeoff amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment 1", Connector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment 2", Connector.FieldTypeIdString);
            rm00102.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            rm00102.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax exempt 1", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax exempt 2", Connector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank branch", Connector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales territory", Connector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts receivable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First invoice date", Connector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit card ID", Connector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit card expiry date", Connector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep distribution history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            rm00101.AddField("Revalue_Customer", "Revalue customer", Connector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance charge ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            rm00102.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            rm00102.AddField("SALSTERR", "Territory ID", Connector.FieldTypeIdString);
            rm00102.AddField("LOCNCODE", "Site ID", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User defined 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User defined 2 from customer master", Connector.FieldTypeIdString);

            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit limit type", Connector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No credit", "Unlimited", "Amount" });

            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });

            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance charge amount type", Connector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });

            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum writeoff type", Connector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not allowed", "Unlimited", "Maximum" });

            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", Connector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open item", "Balance forward" });

            var statementCycle = rm00101.AddField("STMTCYCL", "Statement cycle", Connector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });

            var defaultCashAccountType = rm00101.AddField("DEFCACT", "Default cash account type", Connector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });

            var postResultsTo = rm00101.AddField("Post_Results_To", "Post results to", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/discount account", "Sales offset account" });

            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order fulfillment shortage default", Connector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back order remaining", "Cancel remaining" });
        }
        
        private ConnectorEntity GetProspectEntity()
        {
            var entity = new ConnectorEntity(GpSmartListProspects, "Prospects", ParentConnector);
            var sop00200 = entity.AddTable("SOP00200");
            AddProspectEntityFields(sop00200);
            return entity;
        }
        private static void AddProspectEntityFields(ConnectorTable sop00200)
        {
            var prospectId = sop00200.AddField("PROSPID", "Prospect ID", Connector.FieldTypeIdString, true);
            prospectId.KeyNumber = 1;

            sop00200.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString, true);
            sop00200.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString, true);
            sop00200.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString, true);
            sop00200.AddField("CITY", "City", Connector.FieldTypeIdString, true);
            sop00200.AddField("STATE", "State", Connector.FieldTypeIdString, true);
            sop00200.AddField("ZIP", "Zip", Connector.FieldTypeIdString, true);
            sop00200.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone, true);
            sop00200.AddField("CNTCPRSN", "Contact person", Connector.FieldTypeIdString);
            sop00200.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            sop00200.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            sop00200.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            sop00200.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            sop00200.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            sop00200.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            sop00200.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            sop00200.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            sop00200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            sop00200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            sop00200.AddField("USER2ENT", "User to enter", Connector.FieldTypeIdString);

            var createDate = sop00200.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            createDate.CreateDate = true;

            var modifyDate = sop00200.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            modifyDate.ModifyDate = true;

            sop00200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            sop00200.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
        }

        private ConnectorEntity GetSalesTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListSalesTrx, "Sales transactions", ParentConnector);

            var svSopTrx = entity.AddTable("svSOPTrx");

            var rm00101 = entity.AddTable("RM00101", "svSOPTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var rm00103 = entity.AddTable("RM00103", "svSOPTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var sop10106 = entity.AddTable("SOP10106", "svSOPTrx");
            sop10106.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop10106.AddJoinFields("SOPNUMBE", "SOPNUMBE");

            var sop10100 = entity.AddTable("SOP10100", "svSOPTrx");
            sop10100.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop10100.AddJoinFields("SOPNUMBE", "SOPNUMBE");

            AddSalesTransactionEntityFields(svSopTrx, rm00101, rm00103, sop10106, sop10100);

            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority from customer master", Connector.FieldTypeIdString);

            return entity;
        }
        private void AddSalesTransactionEntityFields(ConnectorTable svSopTrx, ConnectorTable rm00101, ConnectorTable rm00103, ConnectorTable sop10106, ConnectorTable sop10100)
        {
            var sopType = svSopTrx.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            sopType.KeyNumber = 1;

            var sopNumber = svSopTrx.AddField("SOPNUMBE", "SOP number", Connector.FieldTypeIdString, true);
            sopNumber.KeyNumber = 2;

            svSopTrx.AddField("DOCDATE", "Document date", Connector.FieldTypeIdDate, true);
            svSopTrx.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            svSopTrx.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString, true);
            svSopTrx.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString, true);
            svSopTrx.AddField("PRSTADCD", "Primary ship to address code", Connector.FieldTypeIdString, true);
            svSopTrx.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency, true);

            svSopTrx.AddField("ORIGNUMB", "Original number", Connector.FieldTypeIdString);
            svSopTrx.AddField("DOCID", "Document ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("GLPOSTDT", "GL posting date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("QUOTEDAT", "Quote date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("QUOEXPDA", "Quote expiration date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ORDRdate", "Order date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("INVOdate", "Invoice date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("BACKdate", "Back order date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ReqShipdate", "Requested ship date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("FUFILDAT", "Fulfillment date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ACTLSHIP", "Actual ship date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DTLSTREP", "Date last repeated", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DSTBTCH1", "Destination batch 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("DSTBTCH2", "Destination batch 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID1", "Use document ID 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID2", "Use document ID 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("DISCFRGT", "Discount available freight", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCMISC", "Discount available misc", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVAMT", "Discount available amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCRTND", "Discount returned", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISTKNAM", "Discount taken amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DSCPCTAM", "Discount percent amount", Connector.FieldTypeIdPercentage);
            svSopTrx.AddField("DSCDLRAM", "Discount amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVTKN", "Discount available taken", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            svSopTrx.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString);
            svSopTrx.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            svSopTrx.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svSopTrx.AddField("PROSPECT", "Prospect", Connector.FieldTypeIdString);
            svSopTrx.AddField("MSTRNUMB", "Master number", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("PCKSLPNO", "Packing slip number", Connector.FieldTypeIdString);
            svSopTrx.AddField("PICTICNU", "Picking ticket number", Connector.FieldTypeIdString);
            svSopTrx.AddField("MRKDNAMT", "Markdown amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("PRBTADCD", "Primary bill to address code", Connector.FieldTypeIdString);
            svSopTrx.AddField("CNTCPRSN", "Contact person", Connector.FieldTypeIdString);
            svSopTrx.AddField("ShipToName", "Ship to name", Connector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("CITY", "City", Connector.FieldTypeIdString);
            svSopTrx.AddField("STATE", "State", Connector.FieldTypeIdString);
            svSopTrx.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svSopTrx.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svSopTrx.AddField("PHNUMBR1", "Phone number 1", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("PHNUMBR2", "Phone number 2", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("FAXNUMBR", "Fax number", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("COMMAMNT", "Commission amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("CMMSLAMT", "Commission sale amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("NCOMAMNT", "Non-commissioned amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svSopTrx.AddField("TRDISAMT", "Trade discount amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TRDISPCT", "Trade discount percent", Connector.FieldTypeIdPercentage);
            svSopTrx.AddField("SUBTOTAL", "Subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("REMSUBTO", "Remaining subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("MISCAMNT", "Misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXEXMT1", "Tax exempt 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("TAXEXMT2", "Tax exempt 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            svSopTrx.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("FRTSCHID", "Freight schedule ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("MSCSCHID", "Misc schedule ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTFRTAM", "Backout freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTMSCAM", "Backout misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BCKTXAMT", "Backout tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TXBTXAMT", "Taxable tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPCOMPLETE", "Ship complete document", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("PYMTRCVD", "Payment received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DEPRECVD", "Deposit Received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("CODAMNT", "COD amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ACCTAMNT", "Account amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("SALSTERR", "Sales territory", Connector.FieldTypeIdString);
            svSopTrx.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            sop10106.AddField("USRDAT01", "User defined date 1", Connector.FieldTypeIdDate);
            sop10106.AddField("USRDAT02", "User defined date 2", Connector.FieldTypeIdDate);
            sop10106.AddField("USRTAB01", "User defined table 1", Connector.FieldTypeIdString);
            sop10106.AddField("USRTAB09", "User defined table 2", Connector.FieldTypeIdString);
            sop10106.AddField("USRTAB03", "User defined table 3", Connector.FieldTypeIdString);
            sop10106.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            sop10106.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            sop10106.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            sop10106.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            sop10106.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svSopTrx.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            svSopTrx.AddField("COMMNTID", "Comment ID", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_1", "Comment 1", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_2", "Comment 2", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_3", "Comment 3", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_4", "Comment 4", Connector.FieldTypeIdString);
            svSopTrx.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svSopTrx.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("PTDUSRID", "Posted user ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("USER2ENT", "User to enter", Connector.FieldTypeIdString);
            svSopTrx.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ORDAVFRT", "Originating discount available freight", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVMSC", "Originating discount available misc", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVAMT", "Originating discount available amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISRTD", "Originating discount returned", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISTKN", "Originating discount taken amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDDLRAT", "Originating discount amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDATKN", "Originating discount available Taken", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMRKDAM", "Originating markdown amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OCOMMAMT", "Originating commission amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCOSAMT", "Originating commission sales amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORNCMAMT", "Originating non-commissioned amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTDISAM", "Originating trade discount amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORSUBTOT", "Originating subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREMSUBT", "Originating remaining subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREXTCST", "Originating extended cost", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMISCAMT", "Originating misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BSIVCTTL", "Based on invoice total", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORFRTTAX", "Originating freight tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMSCTAX", "Originating misc tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTFRT", "Originating backout freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTMSC", "Originating backout misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OBTAXAMT", "Originating backout tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OTAXTAMT", "Originating taxable tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ECTRX", "EC transaction", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORDOCAMT", "Originating document amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORPMTRVD", "Originating payment received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDEPRVD", "Originating deposit received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCODAMT", "Originating COD amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORACTAMT", "Originating account amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svSopTrx.AddField("DENXRATE", "Denomination exchange rate", Connector.FieldTypeIdQuantity);
            svSopTrx.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("Tax_date", "Tax date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("REPTING", "Repeating", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("TIMEREPD", "Times repeated", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("TIMETREP", "Times to repeat", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("DYSTINCR", "Days to increment", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("TXENGCLD", "Tax engine called", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TIMESPRT", "Times printed", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svSopTrx.AddField("SOPHDRE1", "SOP header errors 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRE2", "SOP header errors 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRFL", "SOP header flags", Connector.FieldTypeIdString);
            svSopTrx.AddField("SOPLNERR", "SOP line errors", Connector.FieldTypeIdString);
            sop10100.AddField("SOPMCERR", "SOP multicompany posting error messages", Connector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTNAME", "Customer name from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate customer number", Connector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact person from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement name", Connector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS zone from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping method from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax schedule ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS1", "Address 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS2", "Address 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CITY", "City from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("STATE", "State from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ZIP", "Zip from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("PHONE1", "Phone 1 from customer master", Connector.FieldTypeIdPhone);
            rm00101.AddField("PHONE2", "Phone 2 from customer master", Connector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax from customer master", Connector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary bill to address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary ship to address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement address code", Connector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment terms ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit limit amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit limit period", Connector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit limit period amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate type ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer discount", Connector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "Price level from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance charge percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance charge amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max writeoff amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User defined 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User defined 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax exempt 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax exempt 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax registration number from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank branch", Connector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales territory from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts receivable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First invoice date", Connector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit card ID", Connector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit card expiry date", Connector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep distribution history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note index from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created date from customer master", Connector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified date from customer master", Connector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total amount of NSF checks - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number of NSF checks - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer balance", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_1", "Aging period amount 1", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_2", "Aging period amount 2", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_3", "Aging period amount 3", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_4", "Aging period amount 4", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_5", "Aging period amount 5", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_6", "Aging period amount 6", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_7", "Aging period amount 7", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last aged", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF check date", Connector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last payment amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last payment date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last transaction date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last transaction amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last finance charge amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average days to pay - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average days To pay - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average days To pay - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number of ADTP documents - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number of ADTP documents - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number of ADTP documents - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total discounts taken - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total discounts taken - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total discounts taken - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total discounts available - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total amount of NSF checks - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number of NSF checks - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted other sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted other cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non current scheduled payments", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total sales - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total sales - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total sales - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total costs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total costs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total costs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total cash received - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total cash received - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total cash received - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total finance charges - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total finance charges - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance charges - calendar year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance charges - last calendar year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total bad debt - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total bad debt - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total bad debt - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total waived finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total waived finance charges - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total waived finance charges - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total writeoffs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total writeoffs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total writeoffs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total number of invoices - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total number of invoices - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total number of invoices - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Toal number of finance charges - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Toal number of finance charges - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Toal number of finance charges - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write offs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write offs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write offs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High balance - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High balance - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High balance - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last statement date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last statement amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits received", Connector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On order amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue customer", Connector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance charge ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from customer master", Connector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total returns - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total returns - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total returns - year to date", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPCOMPLETE", "Ship complete document from customer master", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("APLYWITH", "Apply withholding", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("CORRCTN", "Correction", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("DOCNCORR", "Document number corrected", Connector.FieldTypeIdString);
            svSopTrx.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("SALEdate", "Sale date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("SEQNCORR", "Sequence number corrected", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("SHPPGDOC", "Shipping document", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("SIMPLIFD", "Simplified", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("WITHHAMT", "Withholding amount", Connector.FieldTypeIdCurrency);
            sop10100.AddField("CORRNXST", "Correction to nonexisting transaction", Connector.FieldTypeIdYesNo);
            sop10100.AddField("EXCEPTIONALDEMAND", "Exceptional demand from sales transaction", Connector.FieldTypeIdYesNo);

            var documentStatus = svSopTrx.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum); 
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });

            var originalType = svSopTrx.AddField("ORIGTYPE", "Original type", Connector.FieldTypeIdEnum);
            originalType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            
            var voidStatus = svSopTrx.AddField("VOIDSTTS", "Void status", Connector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = svSopTrx.AddField("RTCLCMTD", "Rate calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var transactionFrequency = svSopTrx.AddField("TRXFREQU", "Transaction frequency", Connector.FieldTypeIdEnum);
            transactionFrequency.AddListItems(1, new List<string> { "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly", "Miscellaneous" });
            
            var taxScheduleSource = svSopTrx.AddField("TXSCHSRC", "Tax schedule source", Connector.FieldTypeIdEnum);
            taxScheduleSource.AddListItems(1, new List<string> { "No error", "Using site schedule ID", "Using ship to schedule ID", "Using single schedule", "Schedule ID empty", "Schedule ID not found", "Shipping method not found", "Setup file missing/damaged", "Site location not found", "Address record not found" });
            
            var commissionAppliedTo = svSopTrx.AddField("COMAPPTO", "Commission applied to", Connector.FieldTypeIdEnum);
            commissionAppliedTo.AddListItems(0, new List<string> { "Sales", "Total invoice" });
            
            var miscTaxable = svSopTrx.AddField("MISCTXBL", "Misc taxable", Connector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var postingStatus = svSopTrx.AddField("PSTGSTUS", "Posting status", Connector.FieldTypeIdEnum);
            postingStatus.AddListItems(1, new List<string> { "Unposted", "Unposted", "Posted", "Posted with error" });
            
            var allocateBy = svSopTrx.AddField("ALLOCABY", "Allocate by", Connector.FieldTypeIdEnum);
            allocateBy.AddListItems(0, new List<string> { "Line item", "Document/batch" });
            
            var mcTransactionState = svSopTrx.AddField("MCTRXSTT", "Multicurrency transaction state", Connector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-denomination to non-denomination", "Non-denomination to Euro", "Non-denomination to denomination", "Denomination to non-denomination", "Denomination to denomination", "Denomination to Euro", "Euro to denomination", "Euro to non-denomination" });
            
            var freightTaxable = svSopTrx.AddField("FRGTTXBL", "Freight taxable", Connector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit limit type", Connector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No credit", "Unlimited", "Amount" });
            
            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance charge amount type", Connector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum writeoff type", Connector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", Connector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open item", "Balance forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement cycle", Connector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default cash account type", Connector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsTo = rm00101.AddField("Post_Results_To", "Post results to", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/discount account", "Sales offset account" });
            
            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order fulfillment shortage default", Connector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back order remaining", "Cancel remaining" });

        }

        private ConnectorEntity GetSalesLineItemEntity()
        {
            var entity = new ConnectorEntity(GpSmartListSalesLineItems, "Sales line items", ParentConnector);

            var svSopLine = entity.AddTable("svSOPLine");

            var svSopTrx = entity.AddTable("svSOPTrx", "svSOPLine");
            svSopTrx.AddJoinFields("SOPTYPE", "SOPTYPE");
            svSopTrx.AddJoinFields("SOPNUMBE", "SOPNUMBE");
            svSopTrx.AddJoinFields("ASI_Document_Status", "ASI_Document_Status");

            var rm00101 = entity.AddTable("RM00101", "svSOPTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var rm00103 = entity.AddTable("RM00103", "svSOPTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var iv00101 = entity.AddTable("IV00101", "svSOPLine");
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");

            var sop10106 = entity.AddTable("SOP10106", "svSOPTrx");
            sop10106.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop10106.AddJoinFields("SOPNUMBE", "SOPNUMBE");

            var sop10100 = entity.AddTable("SOP10100", "svSOPTrx");
            sop10100.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop10100.AddJoinFields("SOPNUMBE", "SOPNUMBE");

            var sop30300 = entity.AddTable("SOP30300", "svSOPLine");
            sop30300.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop30300.AddJoinFields("SOPNUMBE", "SOPNUMBE");
            sop30300.AddJoinFields("LNITMSEQ", "LNITMSEQ");
            sop30300.AddJoinFields("CMPNTSEQ", "CMPNTSEQ");

            var sop10200 = entity.AddTable("SOP10200", "svSOPLine");
            sop10200.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop10200.AddJoinFields("SOPNUMBE", "SOPNUMBE");
            sop10200.AddJoinFields("LNITMSEQ", "LNITMSEQ");
            sop10200.AddJoinFields("CMPNTSEQ", "CMPNTSEQ");

            AddSalesLineItemEntityFields(svSopLine, svSopTrx, rm00101, rm00103, iv00101, sop10106, sop10100, sop30300, sop10200);

            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item shipping weight", Connector.FieldTypeIdQuantity);
            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority from customer master", Connector.FieldTypeIdString);
            svSopLine.AddField("DECPLCUR - 1", "Decimal places currency", Connector.FieldTypeIdInteger);
            svSopLine.AddField("DECPLQTY - 1", "Decimal places quantities", Connector.FieldTypeIdInteger);

            return entity;
        }
        private void AddSalesLineItemEntityFields(ConnectorTable svSopLine, ConnectorTable svSopTrx, ConnectorTable rm00101, ConnectorTable rm00103, ConnectorTable iv00101, ConnectorTable sop10106, ConnectorTable sop10100, ConnectorTable sop30300, ConnectorTable sop10200)
        {
            var sopType = svSopLine.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            sopType.KeyNumber = 1;

            var sopNumber = svSopLine.AddField("SOPNUMBE", "SOP number", Connector.FieldTypeIdString, true);
            sopNumber.KeyNumber = 2;

            svSopTrx.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            svSopLine.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svSopLine.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            svSopLine.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency, true);
            svSopLine.AddField("UNITPRCE", "Unit price", Connector.FieldTypeIdCurrency, true);
            svSopLine.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            svSopLine.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency, true);
            svSopLine.AddField("XTNDPRCE", "Extended price", Connector.FieldTypeIdCurrency, true);

            svSopLine.AddField("ACTLSHIP", "Actual ship date", Connector.FieldTypeIdDate);
            svSopLine.AddField("BKTSLSAM", "Backout sales amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("BSIVCTTL", "Based on invoice total", Connector.FieldTypeIdYesNo);
            svSopLine.AddField("BRKFLD1", "Break field 1", Connector.FieldTypeIdInteger);
            svSopLine.AddField("BRKFLD2", "Break field 2", Connector.FieldTypeIdInteger);
            svSopLine.AddField("BRKFLD3", "Break field 3", Connector.FieldTypeIdInteger);
            svSopLine.AddField("COMMNTID", "Comment ID", Connector.FieldTypeIdString);
            
            svSopLine.AddField("CSLSINDX", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svSopLine.AddField("DMGDINDX", "Damaged account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("DISCSALE", "Discount available Sales", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("DROPSHIP", "Drop ship", Connector.FieldTypeIdInteger);
            svSopLine.AddField("EXTQTYAL", "Existing quantity available", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("EXTQTYSEL", "Existing quantity selected", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("FUFILDAT", "Fulfillment date", Connector.FieldTypeIdDate);
            svSopLine.AddField("INSRINDX", "In service account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("INUSINDX", "In use account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("INVINDX", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("ITMTSHID", "Item tax schedule ID", Connector.FieldTypeIdString);

            var lineItem = svSopLine.AddField("LNITMSEQ", "Line Item Sequence", Connector.FieldTypeIdInteger);
            lineItem.KeyNumber = 3;

            var componentSequence = svSopLine.AddField("CMPNTSEQ", "Component Sequence", Connector.FieldTypeIdInteger);
            componentSequence.KeyNumber = 4;

            svSopLine.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString);
            svSopLine.AddField("MRKDNAMT", "Markdown amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("MKDNINDX", "Markdown account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("MRKDNPCT", "Markdown percent", Connector.FieldTypeIdPercentage);
            svSopLine.AddField("NONINVEN", "Non inventoried item", Connector.FieldTypeIdYesNo);
            svSopLine.AddField("ORBKTSLS", "Originating backout sales amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("ORDAVSLS", "Originating discount available Sales", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("OREXTCST", "Originating extended cost", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("OXTNDPRC", "Originating extended price", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("ORMRKDAM", "Originating markdown amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("OREPRICE", "Originating remaining price", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("OTAXTAMT", "Originating taxable tax amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("ORTDISAM", "Originating trade discount amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("ORUNTCST", "Originating unit cost", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("ORUNTPRC", "Originating unit price", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            svSopLine.AddField("ATYALLOC", "Quantity allocated", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYCANCE", "Quantity canceled", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYCANOT", "Quantity canceled other", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYDMGED", "Quantity damaged", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYFULFI", "Quantity fulfilled", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYBSUOM", "Quantity in base U of M", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYINSVC", "Quantity in service", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYINUSE", "Quantity in use", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYONHND", "Quantity on hand", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYORDER", "Quantity ordered", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRBAC", "Quantity previously back ordered", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRBOO", "Quantity previously BO on order", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRINV", "Quantity previously invoiced", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRORD", "Quantity previously ordered", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRVRECVD", "Quantity previously received", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYRECVD", "Quantity received", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYREMAI", "Quantity remaining", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYREMBO", "Quantity remaining on BO", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYRTRND", "Quantity returned", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYSLCTD", "Quantity selected", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYTBAOR", "Quantity to back order", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYTOINV", "Quantity to invoice", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYTORDR", "Quantity to order", Connector.FieldTypeIdQuantity);
            svSopLine.AddField("REMPRICE", "Remaining price", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("ReqShipdate", "Requested ship date", Connector.FieldTypeIdDate);
            svSopLine.AddField("RTNSINDX", "Returns account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("SLSINDX", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("SALSTERR", "Sales territory", Connector.FieldTypeIdString);
            svSopLine.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svSopLine.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svSopLine.AddField("SOPLNERR", "SOP line errors", Connector.FieldTypeIdString);
            svSopLine.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("TXBTXAMT", "Taxable tax amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("TRDISAMT", "Trade discount amount", Connector.FieldTypeIdCurrency);
            svSopLine.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            svSopLine.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            sop10200.AddField("QTYONPO", "Quantity on PO", Connector.FieldTypeIdQuantity);
            iv00101.AddField("NOTEINDX", "Note index from item master", Connector.FieldTypeIdInteger);
            iv00101.AddField("ITMSHNAM", "Item short name", Connector.FieldTypeIdString);
            iv00101.AddField("ITMGEDSC", "Item generic description", Connector.FieldTypeIdString);
            iv00101.AddField("STNDCOST", "Standard cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("CURRCOST", "Current cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("DECPLQTY - 1", "Decimal places quantities from item master", Connector.FieldTypeIdInteger);
            iv00101.AddField("DECPLCUR - 1", "Decimal places currency from item master", Connector.FieldTypeIdInteger);
            iv00101.AddField("ITMTSHID", "Item tax schedule ID from item master", Connector.FieldTypeIdString);
            iv00101.AddField("IVIVINDX", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVIVOFIX", "Inventory offset account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVCOGSIX", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLSIDX", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLDSIX", "Sales discounts account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLRNIX", "Sales returns account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINUSIX", "In use account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINSVIX", "In service account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVDMGIDX", "Damaged account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVVARIDX", "Variances account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("DPSHPIDX", "Drop ship account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURPVIDX", "Purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVRETIDX", "Inventory returns account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ASMVRIDX", "Assembly variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITMCLSCD", "Item class code", Connector.FieldTypeIdString);
            iv00101.AddField("LOTTYPE", "Lot type", Connector.FieldTypeIdString);
            iv00101.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPDSTHST", "Keep distribution history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("ALWBKORD", "Allow back orders", Connector.FieldTypeIdYesNo);
            iv00101.AddField("UOMSCHDL", "U of M schedule", Connector.FieldTypeIdString);
            iv00101.AddField("ALTITEM1", "Alternate item 1", Connector.FieldTypeIdString);
            iv00101.AddField("ALTITEM2", "Alternate item 2", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_1", "User category value 1", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_2", "User category value 2", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_3", "User category value 3", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_4", "User category value 4", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_5", "User category value 5", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_6", "User category value 6", Connector.FieldTypeIdString);
            iv00101.AddField("MSTRCDTY", "Master record type", Connector.FieldTypeIdInteger);
            iv00101.AddField("MODIFDT", "Modified date from item master", Connector.FieldTypeIdDate);
            iv00101.AddField("CREATDDT", "Created date from item master", Connector.FieldTypeIdDate);
            iv00101.AddField("WRNTYDYS", "Warranty days", Connector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "Price level from item master", Connector.FieldTypeIdString);
            iv00101.AddField("CGSINFLX", "COGS inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("CGSMCIDX", "COGS monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINFIDX", "Inventory inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("INVMCIDX", "Inventory monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITEMCODE", "Item code from item master", Connector.FieldTypeIdString);
            iv00101.AddField("LASTGENSN", "Last generated serial number", Connector.FieldTypeIdString);
            iv00101.AddField("PriceGroup", "Price group", Connector.FieldTypeIdString);
            iv00101.AddField("PINFLIDX", "Purch inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURMCIDX", "Purch monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PRCHSUOM", "Purchasing U of M", Connector.FieldTypeIdString);
            iv00101.AddField("SELNGUOM", "Selling U of M", Connector.FieldTypeIdString);
            iv00101.AddField("TCC", "Tax commodity code", Connector.FieldTypeIdString);
            iv00101.AddField("UPPVIDX", "Unrealized purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("LOCNCODE", "Location code from item master", Connector.FieldTypeIdString);
            svSopTrx.AddField("ORIGNUMB", "Original number", Connector.FieldTypeIdString);
            svSopTrx.AddField("DOCID", "Document ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("DOCdate", "Document date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("GLPOSTDT", "GL posting date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("QUOTEDAT", "Quote date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("QUOEXPDA", "Quote expiration date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ORDRdate", "Order date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("INVOdate", "Invoice date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("BACKdate", "Back order date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ReqShipdate", "Requested ship date from sales transaction", Connector.FieldTypeIdDate);
            svSopTrx.AddField("FUFILDAT", "Fulfillment date from sales transaction", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ACTLSHIP", "Actual ship date from sales transaction", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DTLSTREP", "Date last repeated", Connector.FieldTypeIdDate);
            svSopTrx.AddField("DSTBTCH1", "Destination batch 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("DSTBTCH2", "Destination batch 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID1", "Use document ID 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID2", "Use document ID 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("DISCFRGT", "Discount available freight", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCMISC", "Discount available misc", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVAMT", "Discount available amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCRTND", "Discount returned", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISTKNAM", "Discount taken amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DSCPCTAM", "Discount percent amount", Connector.FieldTypeIdPercentage);
            svSopTrx.AddField("DSCDLRAM", "Discount amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVTKN", "Discount Available Taken", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("PRCLEVEL", "Price level from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("LOCNCODE", "Location code from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            svSopTrx.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svSopTrx.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            svSopTrx.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString);
            svSopTrx.AddField("PROSPECT", "Prospect", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("MSTRNUMB", "Master number", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("PCKSLPNO", "Packing slip number", Connector.FieldTypeIdString);
            svSopTrx.AddField("PICTICNU", "Picking ticket number", Connector.FieldTypeIdString);
            svSopTrx.AddField("MRKDNAMT", "Markdown amount from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("PRBTADCD", "Primary bill to address code", Connector.FieldTypeIdString);
            svSopTrx.AddField("PRSTADCD", "Primary ship to address code from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("CNTCPRSN", "Contact person from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("ShipToName", "Ship to name from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS1", "Address 1 from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS2", "Address 2 from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("CITY", "City from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("STATE", "State from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("ZIPCODE", "Zip Code from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("COUNTRY", "Country from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("PHNUMBR1", "Phone number 1 from sales transaction", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("PHNUMBR2", "Phone number 2 from sales transaction", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("FAXNUMBR", "Fax number from sales transaction", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("COMMAMNT", "Commission amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("CMMSLAMT", "Commission sale amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("NCOMAMNT", "Non-commissioned amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPMTHD", "Shipping method from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("TRDISAMT", "Trade discount amount from sales transaction", Connector.FieldTypeIdPercentage);
            svSopTrx.AddField("TRDISPCT", "Trade discount percent", Connector.FieldTypeIdPercentage);
            svSopTrx.AddField("SUBTOTAL", "Subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("REMSUBTO", "Remaining subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("EXTDCOST", "Extended cost from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("MISCAMNT", "Misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXEXMT1", "Tax exempt 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("TAXEXMT2", "Tax exempt 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            svSopTrx.AddField("TAXSCHID", "Tax schedule ID from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("FRTSCHID", "Freight schedule ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("MSCSCHID", "Misc schedule ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTFRTAM", "Backout freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTMSCAM", "Backout misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BCKTXAMT", "Backout tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TXBTXAMT", "Taxable tax amount from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXAMNT", "Tax amount from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("PYMTRCVD", "Payment received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("DEPRECVD", "Deposit received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("CODAMNT", "COD amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ACCTAMNT", "Account amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("SALSTERR", "Sales territory from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("SLPRSNID", "Salesperson ID from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            sop10106.AddField("USRDAT01", "User defined date 1", Connector.FieldTypeIdDate);
            sop10106.AddField("USRDAT02", "User defined date 2", Connector.FieldTypeIdDate);
            sop10106.AddField("USRTAB01", "User defined table 1", Connector.FieldTypeIdString);
            sop10106.AddField("USRTAB09", "User defined table 2", Connector.FieldTypeIdString);
            sop10106.AddField("USRTAB03", "User defined table 3", Connector.FieldTypeIdString);
            sop10106.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            sop10106.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            sop10106.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            sop10106.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            sop10106.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svSopTrx.AddField("TRXSORCE", "Transaction source from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("COMMNTID", "Comment ID from sales transaction", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_1", "Comment 1", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_2", "Comment 2", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_3", "Comment 3", Connector.FieldTypeIdString);
            sop10106.AddField("COMMENT_4", "Comment 4", Connector.FieldTypeIdString);
            svSopTrx.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svSopTrx.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("PTDUSRID", "Posted user ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("USER2ENT", "User to enter", Connector.FieldTypeIdString);
            svSopTrx.AddField("CREATDDT", "Created date from sales transaction", Connector.FieldTypeIdDate);
            svSopTrx.AddField("MODIFDT", "Modified date from sales transaction", Connector.FieldTypeIdDate);
            svSopTrx.AddField("ORDAVFRT", "Originating discount available freight", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVMSC", "Originating discount available misc", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVAMT", "Originating discount available amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISRTD", "Originating discount returned", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISTKN", "Originating discount taken amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDDLRAT", "Originating discount amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDATKN", "Originating discount available taken", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMRKDAM", "Originating markdown amount from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OCOMMAMT", "Originating commission amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCOSAMT", "Originating commission sales amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORNCMAMT", "Originating non-commissioned amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTDISAM", "Originating trade discount amount from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORSUBTOT", "Originating subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREMSUBT", "Originating remaining subtotal", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREXTCST", "Originating extended cost from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMISCAMT", "Originating misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("BSIVCTTL", "Based on invoice total from sales transaction", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORFRTTAX", "Originating freight tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMSCTAX", "Originating misc tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTFRT", "Originating backout freight amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTMSC", "Originating backout misc amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OBTAXAMT", "Originating backout tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("OTAXTAMT", "Originating taxable tax amount from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTAXAMT", "Originating tax amount from sales transaction", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ECTRX", "EC Transaction", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORDOCAMT", "Originating document amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORPMTRVD", "Originating payment received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDEPRVD", "Originating deposit received", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCODAMT", "Originating COD amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORACTAMT", "Originating account amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svSopTrx.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svSopTrx.AddField("DENXRATE", "Denomination exchange rate", Connector.FieldTypeIdQuantity);
            svSopTrx.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("Tax_date", "Tax date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("REPTING", "Repeating", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("TIMEREPD", "Times repeated", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("TIMETREP", "Times to repeat", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("DYSTINCR", "Days to increment", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("TXENGCLD", "Tax engine called", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            svSopTrx.AddField("TIMESPRT", "Times printed", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("NOTEINDX", "Note index from sales transaction", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("CURRNIDX", "Currency index from sales transaction", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svSopTrx.AddField("SOPHDRE1", "SOP header errors 1", Connector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRE2", "SOP header errors 2", Connector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRFL", "SOP header Flags", Connector.FieldTypeIdString);
            svSopTrx.AddField("SOPLNERR", "SOP line Errors from sales transaction", Connector.FieldTypeIdString);
            sop10100.AddField("SOPMCERR", "SOP multicompany posting error messages", Connector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS3", "Address 3 from sales transaction", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTNAME", "Customer name from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate customer number", Connector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact person from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement name", Connector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS zone from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping method from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax schedule ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS1", "Address 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS2", "Address 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CITY", "City from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("STATE", "State from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ZIP", "Zip from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("PHONE1", "Phone 1 from customer master", Connector.FieldTypeIdPhone);
            rm00101.AddField("PHONE2", "Phone 2 from customer master", Connector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax from customer master", Connector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary bill to address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary ship to address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement address code", Connector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment terms ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit limit amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit limit period", Connector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit limit period amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate type ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer discount", Connector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "Price level from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance charge percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance charge amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max writeoff amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User defined 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User defined 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax exempt 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax exempt 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax registration number from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank branch", Connector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales territory from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts receivable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First invoice date", Connector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit card ID", Connector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit card expiry date", Connector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep distribution history from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep calendar history from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep period history from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep transaction history from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note index from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created date from customer master", Connector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified date from customer master", Connector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total amount of NSF checks - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number of NSF checks - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer balance", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_1", "Aging period amount 1", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_2", "Aging period amount 2", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_3", "Aging period amount 3", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_4", "Aging period amount 4", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_5", "Aging period amount 5", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_6", "Aging period amount 6", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_7", "Aging period amount 7", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last aged", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF check date", Connector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last payment amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last payment date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last transaction date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last transaction amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last finance charge amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average days to pay - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average days to pay - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average days to pay - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number of ADTP documents - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number of ADTP documents - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number of ADTP documents - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total discounts taken - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total discounts taken - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total discounts taken - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total discounts available - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total amount of NSF checks - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number of NSF checks - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted other sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted other cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non current scheduled payments", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total sales - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total sales - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total sales - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total costs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total costs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total costs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total cash received - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total cash received - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total cash received - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total finance charges - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total finance charges - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance charges - calendar year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance charges - last calendar year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total bad debt - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total bad debt - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total bad debt - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total waived finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total waived finance charges - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total waived finance charges LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total writeoffs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total writeoffs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total writeoffs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total number of invoices - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total number of invoices - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total number of invoices - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Toal number of finance charges - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Toal number of finance charges - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Toal number of finance charges - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write offs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write offs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write offs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High balance - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High balance - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High balance - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last statement date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last statement amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits received", Connector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On order amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue customer", Connector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance charge ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from customer master", Connector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total returns - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total returns - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total returns - year to date", Connector.FieldTypeIdCurrency);
            rm00101.AddField("SHIPCOMPLETE", "Ship complete document from customer master", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("APLYWITH", "Apply withholding", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("CORRCTN", "Correction", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("DOCNCORR", "Document number corrected from sales transaction", Connector.FieldTypeIdString);
            svSopTrx.AddField("PHONE3", "Phone 3 from sales transaction", Connector.FieldTypeIdPhone);
            svSopTrx.AddField("SALEdate", "Sale date", Connector.FieldTypeIdDate);
            svSopTrx.AddField("SEQNCORR", "Sequence number corrected", Connector.FieldTypeIdInteger);
            svSopTrx.AddField("SHPPGDOC", "Shipping document", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("SIMPLIFD", "Simplified", Connector.FieldTypeIdYesNo);
            svSopTrx.AddField("WITHHAMT", "Withholding amount", Connector.FieldTypeIdCurrency);
            sop10100.AddField("CORRNXST", "Correction to nonexisting transaction", Connector.FieldTypeIdYesNo);
            sop10100.AddField("SOPHDRE3", "SOP header errors 3", Connector.FieldTypeIdString);
            svSopTrx.AddField("SHIPCOMPLETE", "Ship complete document", Connector.FieldTypeIdYesNo);
            sop30300.AddField("DOCNCORR", "Document number corrected", Connector.FieldTypeIdString);
            svSopLine.AddField("ITEMCODE", "Item code", Connector.FieldTypeIdString);
            svSopLine.AddField("ORGSEQNM", "Original sequence number corrected", Connector.FieldTypeIdInteger);
            svSopLine.AddField("ODECPLCU - 1", "Originating decimal places currency", Connector.FieldTypeIdInteger);
            svSopLine.AddField("TAXSCHID", "Tax schedule ID from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("PRSTADCD", "Primary ship to address code from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("ShipToName", "Ship to name from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("CNTCPRSN", "Contact person from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("ADDRESS1", "Address 1 from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("ADDRESS2", "Address 2 from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("ADDRESS3", "Address 3 from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("CITY", "City from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("STATE", "State from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("ZIPCODE", "Zip Code from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("COUNTRY", "Country from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("PHONE1", "Phone 1 from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("PHONE2", "Phone 2 from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("PHONE3", "Phone 3 from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("FAXNUMBR", "Fax number from sales line item", Connector.FieldTypeIdString);
            svSopLine.AddField("EXCEPTIONALDEMAND", "Exceptional demand from sales line item", Connector.FieldTypeIdYesNo);
            svSopLine.AddField("CONTNBR", "Contract number", Connector.FieldTypeIdString);
            svSopLine.AddField("CONTSTARTDTE", "Contract start date", Connector.FieldTypeIdDate);
            svSopLine.AddField("CONTENDDTE", "Contract end date", Connector.FieldTypeIdDate);
            svSopLine.AddField("CONTITEMNBR", "Contract item number", Connector.FieldTypeIdString);
            svSopLine.AddField("CONTSERIALNBR", "Contract serial number", Connector.FieldTypeIdString);

            var ivItemTaxable = svSopLine.AddField("IVITMTXB", "IV item taxable", Connector.FieldTypeIdEnum);
            ivItemTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var markdownType = svSopLine.AddField("MRKDNTYP", "Markdown type", Connector.FieldTypeIdEnum);
            markdownType.AddListItems(0, new List<string> { "Percent", "Amount" });
            
            var purchasingStatus = svSopLine.AddField("PURCHSTAT", "Purchasing status", Connector.FieldTypeIdEnum);
            purchasingStatus.AddListItems(1, new List<string> { "None", "Needs purchase", "Purchased", "Partially received", "Fully received" });
            
            var documentStatus = svSopLine.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var itemType = iv00101.AddField("ITEMTYPE", "Item type", Connector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales inventory", "Discontinued", "Kit", "Misc charges", "Services", "Flat fee" });
            
            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax options", Connector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var itemTrackingOption = iv00101.AddField("ITMTRKOP", "Item tracking option", Connector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
            
            var valuationMethod = iv00101.AddField("VCTNMTHD", "Valuation method", Connector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO perpetual", "LIFO perpetual", "Average perpetual", "FIFO periodic", "LIFO periodic" });
            
            var abcCode = iv00101.AddField("ABCCODE", "ABC code", Connector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });
            
            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS account source", Connector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From component item", "From kit item" });
            
            var priceMethod = iv00101.AddField("PRICMTHD", "Price method", Connector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency amount", "Percentage of list price", "Percentage markup - current cost", "Percentage markup - standard cost", "Percentage margin - current cost", "Percentage margin - standard cost" });
            
            var originalType = svSopTrx.AddField("ORIGTYPE", "Original type", Connector.FieldTypeIdEnum);
            originalType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            
            var voidStatus = svSopTrx.AddField("VOIDSTTS", "Void status", Connector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = svSopTrx.AddField("RTCLCMTD", "Rate calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var transactionFrequency = svSopTrx.AddField("TRXFREQU", "Transaction frequency", Connector.FieldTypeIdEnum);
            transactionFrequency.AddListItems(1, new List<string> { "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly", "Miscellaneous" });
            
            var taxScheduleSourceFromSalesTransaction = svSopTrx.AddField("TXSCHSRC", "Tax schedule source from sales transaction", Connector.FieldTypeIdEnum);
            taxScheduleSourceFromSalesTransaction.AddListItems(1, new List<string> { "No Error", "Using site schedule ID", "Using ship to schedule ID", "Using single schedule", "Schedule ID empty", "Schedule ID not found", "Shipping method not found", "Setup file missing/damaged", "Site location not found", "Address record not found" });
            
            var commissionAppliedTo = svSopTrx.AddField("COMAPPTO", "Commission applied to", Connector.FieldTypeIdEnum);
            commissionAppliedTo.AddListItems(0, new List<string> { "Sales", "Total invoice" });
            
            var miscTaxable = svSopTrx.AddField("MISCTXBL", "Misc taxable", Connector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var postingStatus = svSopTrx.AddField("PSTGSTUS", "Posting status", Connector.FieldTypeIdEnum);
            postingStatus.AddListItems(1, new List<string> { "Unposted", "Unposted", "Posted", "Posted with error" });
            
            var allocateBy = svSopTrx.AddField("ALLOCABY", "Allocate By", Connector.FieldTypeIdEnum);
            allocateBy.AddListItems(0, new List<string> { "Line item", "Document/batch" });
            
            var mcTransactionState = svSopTrx.AddField("MCTRXSTT", "Multicurrency transaction state", Connector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-denomination to non-denomination", "Non-denomination to Euro", "Non-denomination to denomination", "Denomination to non-denomination", "Denomination to denomination", "Denomination to Euro", "Euro to denomination", "Euro to non-denomination" });
            
            var freightTaxable = svSopTrx.AddField("FRGTTXBL", "Freight taxable", Connector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit limit type", Connector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No credit", "Unlimited", "Amount" });
            
            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance charge amount type", Connector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum writeoff type", Connector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", Connector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open item", "Balance forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement cycle", Connector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default cash account type", Connector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsTo = rm00101.AddField("Post_Results_To", "Post results to", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/discount account", "Sales offset account" });
            
            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order fulfillment shortage default", Connector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back order remaining", "Cancel remaining" });
            
            var taxScheduleSourceFromSalesLinesItem = svSopLine.AddField("TXSCHSRC", "Tax schedule source from sales line item", Connector.FieldTypeIdEnum);
            taxScheduleSourceFromSalesLinesItem.AddListItems(1, new List<string> { "No error", "Using site schedule ID", "Using ship to schedule ID", "Using single schedule", "Schedule ID empty", "Schedule ID not found", "Shipping method not found", "Setup file missing/damaged", "Site location not found", "Address record not found" });

        }
            
        private ConnectorEntity GetReceivablesTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListReceivablesTrx, "Receivables transactions", ParentConnector);

            var svRmTrx = entity.AddTable("svRMTrx");

            var rm00101 = entity.AddTable("RM00101", "svRMTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var rm00103 = entity.AddTable("RM00103", "svRMTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var mc020102 = entity.AddTable("MC020102", "svRMTrx");
            mc020102.AddJoinFields("RMDTYPAL", "RMDTYPAL");
            mc020102.AddJoinFields("DOCNUMBR", "DOCNUMBR");

            var rm10301 = entity.AddTable("RM10301", "svRMTrx");
            rm10301.AddJoinFields("RMDTYPAL", "RMDTYPAL");
            rm10301.AddJoinFields("DOCNUMBR", "DOCNUMBR");

            var rm20101 = entity.AddTable("RM20101", "svRMTrx");
            rm20101.AddJoinFields("RMDTYPAL", "RMDTYPAL");
            rm20101.AddJoinFields("DOCNUMBR", "DOCNUMBR");

            AddReceivablesTransactionEntityFields(svRmTrx, rm00101, rm00103, mc020102, rm10301, rm20101);

            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority from customer master", Connector.FieldTypeIdString);

            return entity;
        }
        private void AddReceivablesTransactionEntityFields(ConnectorTable svRmTrx, ConnectorTable rm00101, ConnectorTable rm00103, ConnectorTable mc020102, ConnectorTable rm10301, ConnectorTable rm20101)
        {
            svRmTrx.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString, true);
            svRmTrx.AddField("DOCNUMBR", "Document number", Connector.FieldTypeIdString, true);
            var documentType = svRmTrx.AddField("RMDTYPAL", "Document type", Connector.FieldTypeIdEnum, true);
            documentType.AddListItems(1, new List<string> { "Sales/invoices", "Scheduled payments", "Debit memos", "Finance charges", "Service/repairs", "Warranty", "Credit memo", "Returns", "Payments" });
            svRmTrx.AddField("DOCdate", "Document date", Connector.FieldTypeIdDate, true);
            svRmTrx.AddField("SLSAMNT", "Sales amount", Connector.FieldTypeIdCurrency, true);

            rm10301.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            svRmTrx.AddField("CPRCSTNM", "Corporate customer number", Connector.FieldTypeIdString);
            rm10301.AddField("DOCPRFIX", "Document prefix", Connector.FieldTypeIdString);
            svRmTrx.AddField("DOCDESCR", "Document description", Connector.FieldTypeIdString);
            svRmTrx.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString);
            svRmTrx.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svRmTrx.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            svRmTrx.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            svRmTrx.AddField("CBKIDCRD", "Checkbook ID - credit card", Connector.FieldTypeIdString);
            svRmTrx.AddField("CBKIDCSH", "Checkbook ID - cash", Connector.FieldTypeIdString);
            svRmTrx.AddField("CBKIDCHK", "Checkbook ID - check", Connector.FieldTypeIdString);
            svRmTrx.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            rm10301.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            rm10301.AddField("PTDUSRID", "Posted user ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("GLPOSTDT", "GL posting date", Connector.FieldTypeIdDate);
            svRmTrx.AddField("LSTEDTDT", "Last edit date", Connector.FieldTypeIdDate);
            svRmTrx.AddField("LSTUSRED", "Last user to edit", Connector.FieldTypeIdString);
            svRmTrx.AddField("ORTRXAMT", "Original transaction amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("CURTRXAM", "Current transaction amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("COSTAMNT", "Cost amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("MISCAMNT", "Misc amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("COMDLRAM", "Commission amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("NCOMAMNT", "Non-commissioned amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("CASHAMNT", "Cash amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISTKNAM", "Discount taken amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISAVAMT", "Discount available amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISAVTKN", "Discount available taken", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISCRTND", "Discount returned", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            svRmTrx.AddField("DSCDLRAM", "Discount amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("DSCPCTAM", "Discount percent amount", Connector.FieldTypeIdPercentage);
            svRmTrx.AddField("WROFAMNT", "Write off amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("SALSTERR", "Sales territory", Connector.FieldTypeIdString);
            svRmTrx.AddField("DINVPDOF", "Date invoice paid off", Connector.FieldTypeIdDate);
            svRmTrx.AddField("PPSAMDED", "PPS amount deducted", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("GSTDSAMT", "GST discount amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("CSPORNBR", "Customer PO number", Connector.FieldTypeIdString);
            rm10301.AddField("BKTSLSAM", "Backout sales amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("BKTFRTAM", "Backout freight amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("BKTMSCAM", "Backout misc amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("TXENGCLD", "Tax engine called", Connector.FieldTypeIdYesNo);
            rm10301.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("APPLDAMT", "Applied amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("CASHdate", "Cash date", Connector.FieldTypeIdDate);
            rm10301.AddField("DCNUMCSH", "Document number - cash", Connector.FieldTypeIdString);
            rm10301.AddField("CHEKAMNT", "Check amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            rm10301.AddField("DCNUMCHK", "Document number - check", Connector.FieldTypeIdString);
            rm10301.AddField("CRCRDAMT", "Credit card amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("CRCRDNAM", "Credit card name", Connector.FieldTypeIdString);
            rm10301.AddField("RCTNCCRD", "Receipt number - credit card", Connector.FieldTypeIdString);
            rm10301.AddField("CRCARDDT", "Credit card date", Connector.FieldTypeIdDate);
            rm10301.AddField("DCNUMCRD", "Document number - credit card", Connector.FieldTypeIdString);
            rm10301.AddField("ACCTAMNT", "Account amount", Connector.FieldTypeIdCurrency);
            rm10301.AddField("TRDDISCT", "Trade discount", Connector.FieldTypeIdInteger);
            rm20101.AddField("DELETE1", "Delete", Connector.FieldTypeIdYesNo);
            rm20101.AddField("AGNGBUKT", "Aging bucket", Connector.FieldTypeIdInteger);
            svRmTrx.AddField("VOIDdate", "Void date", Connector.FieldTypeIdDate);
            svRmTrx.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svRmTrx.AddField("TRDISAMT", "Trade discount amount", Connector.FieldTypeIdCurrency);
            svRmTrx.AddField("SLSCHDID", "Sales schedule ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("FRTSCHID", "Freight schedule ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("MSCSCHID", "Misc schedule ID", Connector.FieldTypeIdString);
            svRmTrx.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svRmTrx.AddField("Tax_date", "Tax date", Connector.FieldTypeIdDate);
            rm10301.AddField("POSTED", "Posted", Connector.FieldTypeIdYesNo);
            svRmTrx.AddField("APLYWITH", "Apply withholding", Connector.FieldTypeIdYesNo);
            svRmTrx.AddField("SALEdate", "Sale date", Connector.FieldTypeIdDate);
            svRmTrx.AddField("CORRCTN", "Correction", Connector.FieldTypeIdYesNo);
            svRmTrx.AddField("SIMPLIFD", "Simplified", Connector.FieldTypeIdYesNo);
            rm10301.AddField("BALFWDNM", "Balance forward number", Connector.FieldTypeIdString);
            rm10301.AddField("RMTREMSG", "RM transaction posting error messages", Connector.FieldTypeIdString);
            rm10301.AddField("RMDPEMSG", "RM distribution posting error messages", Connector.FieldTypeIdString);
            mc020102.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            mc020102.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            mc020102.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            mc020102.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            mc020102.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            mc020102.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            mc020102.AddField("ORCTRXAM", "Originating current transaction amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORSLSAMT", "Originating sales amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORCSTAMT", "Originating cost amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORMISCAMT", "Originating misc amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORCASAMT", "Originating cash amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORCHKAMT", "Originating check amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORCCDAMT", "Originating credit card amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORAPPAMT", "Originating applied amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORDISTKN", "Originating discount taken amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORDAVAMT", "Originating discount available amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORDATKN", "Originating discount available taken", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORDISRTD", "Originating discount returned", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORDDLRAT", "Originating discount amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORTDISAM", "Originating trade discount amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORORGTRX", "Originating original transaction amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORWROFAM", "Originating write off amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORCOMAMT", "Originating commission amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORBKTSLS", "Originating backout sales amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORBKTFRT", "Originating backout freight amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("ORBKTMSC", "Originating backout misc amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("UNGANLOS", "Unrealized gain-Loss amount", Connector.FieldTypeIdCurrency);
            mc020102.AddField("RMMCERRS", "RM multicurrency posting error messages", Connector.FieldTypeIdString);
            mc020102.AddField("DENXRATE", "Denomination exchange rate", Connector.FieldTypeIdQuantity);
            mc020102.AddField("MCTRXSTT", "Multicurrency transaction state", Connector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer name from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate customer number from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact person", Connector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement name", Connector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping method from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax schedule ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            rm00101.AddField("CITY", "City", Connector.FieldTypeIdString);
            rm00101.AddField("STATE", "State", Connector.FieldTypeIdString);
            rm00101.AddField("ZIP", "Zip", Connector.FieldTypeIdString);
            rm00101.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            rm00101.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary bill to address code", Connector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary ship to address code", Connector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement address code", Connector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment terms ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit limit amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit limit period", Connector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit limit period amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate type ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer discount", Connector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance charge percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance charge amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Maximum writeoff amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment 1", Connector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment 2", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax exempt 1", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax exempt 2", Connector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank branch", Connector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales territory from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts receivable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First invoice date", Connector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit card ID", Connector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit card expiry date", Connector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep distribution history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note index from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created date from customer master", Connector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified date from customer master", Connector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total amount of NSF checks - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number of NSF checks - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer balance", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_1", "Aging period amount 1", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_2", "Aging period amount 2", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_3", "Aging period amount 3", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_4", "Aging period amount 4", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_5", "Aging period amount 5", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_6", "Aging period amount 6", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_7", "Aging period amount 7", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last aged", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF check date", Connector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last payment amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last payment date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last transaction date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last transaction amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last finance charge amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average days to pay - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average days to pay - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average days to pay - year  to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number of ADTP documents - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number of ADTP documents - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number of ADTP documents - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total discounts taken - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total discounts taken - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total discounts taken - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total discounts available - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total amount of NSF checks - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number of NSF checks - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted other sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted other cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non current scheduled payments", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total sales - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total sales - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total sales - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total costs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total costs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total costs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total cash received - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total cash received - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total cash received - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total finance charges - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total finance charges - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance charges - calendar year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance charges - last calendar year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total bad debt - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total bad debt - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total bad debt - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total waived finance charges - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total waived finance charges - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total waived finance charges - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total writeoffs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total writeoffs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total writeoffs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total number of invoices - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total number of invoices - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total number of invoices - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Toal number of finance charges - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Toal number of finance charges - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Toal number of finance charges - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write offs - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write offs - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write offs - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High balance - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High balance - year to date", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High balance - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last statement date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last statement amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits received", Connector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On order amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue customer", Connector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance charge ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount grace period from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due date grace period from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total returns - life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total returns - last year", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total returns - year to date", Connector.FieldTypeIdCurrency);
            rm10301.AddField("CORRNXST", "Correction to nonexisting transaction", Connector.FieldTypeIdYesNo);
            rm10301.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            rm10301.AddField("DocPrinted", "Document printed", Connector.FieldTypeIdYesNo);
            rm10301.AddField("DOCNCORR", "Document number corrected", Connector.FieldTypeIdString);
            rm10301.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            svRmTrx.AddField("ECTRX", "EC transaction", Connector.FieldTypeIdYesNo);
            svRmTrx.AddField("Electronic", "Electronic", Connector.FieldTypeIdYesNo);
            rm10301.AddField("PSTGSTUS", "Posting status", Connector.FieldTypeIdInteger);
            rm10301.AddField("RMTREMSG2", "RM transaction posting error messages 2", Connector.FieldTypeIdString);
            svRmTrx.AddField("Factoring", "Factoring", Connector.FieldTypeIdYesNo);

            var documentStatus = svRmTrx.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "Posted", "History" });
            
            var cashReceiptType = svRmTrx.AddField("CSHRCTYP", "Cash receipt type", Connector.FieldTypeIdEnum);
            cashReceiptType.AddListItems(1, new List<string> { "Check", "Cash", "Credit Card" });
            
            var commissionAppliedTo = rm10301.AddField("COMAPPTO", "Commission applied to", Connector.FieldTypeIdEnum);
            commissionAppliedTo.AddListItems(0, new List<string> { "Sales", "Total Invoice" });
            
            var voidStatus = svRmTrx.AddField("VOIDSTTS", "Void status", Connector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = mc020102.AddField("RTCLCMTD", "Rate calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit limit type", Connector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No credit", "Unlimited", "Amount" });
            
            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance charge amount type", Connector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum writeoff type", Connector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", Connector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open item", "Balance forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement cycle", Connector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default cash account type", Connector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsTo = rm00101.AddField("Post_Results_To", "Post results to", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/discount account", "Sales offset account" });
            
            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order fulfillment shortage default", Connector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back order remaining", "Cancel remaining" });

        }

    }
}
