using System.Collections.Generic;
using eOne.Common.DataConnectors;

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
            UserDefined1 = "User Defined 1";
            UserDefined2 = "User Defined 2";
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

        private DataConnectorEntity GetCustomerEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListCustomers, "Customers", ParentConnector);
            
            var rm00101 = entity.AddTable("RM00101");
            
            var rm00103 = entity.AddTable("RM00103", "RM00101", DataConnectorTable.DataConnectorTableJoinType.Inner);
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddCustomerEntityFields(rm00101, rm00103);
            
            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority", DataConnector.FieldTypeIdString);

            return entity;
        }
        private void AddCustomerEntityFields(DataConnectorTable rm00101, DataConnectorTable rm00103)
        {
            rm00101.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("CITY", "City", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("STATE", "State", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("ZIP", "Zip", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone, true);
            rm00101.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            rm00101.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary Shipto Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1", DataConnector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", UserDefined1, DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", UserDefined2, DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            rm00101.AddField("FRSTINDT", "First Invoice Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit Card ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit Card Exp Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep Distribution History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total Amount Of NSF Checks Life", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number Of NSF Checks Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer Balance", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last Aged", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF Check Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last Payment Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last Payment Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last Transaction Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last Transaction Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last Finance Charge Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average Days to Pay - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number ADTP Documents - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number ADTP Documents - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number ADTP Documents - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total Discounts Taken LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total Discounts Available YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total Amount Of NSF Checks YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number Of NSF Checks YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted Other Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted Other Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non Current Scheduled Payments", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total Sales YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total Sales LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total Sales LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total Costs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total Costs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total Costs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total Cash Received YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total Cash Received LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total Cash Received LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total Finance Charges LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total Finance Charges LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance Charges CYTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance Charges LYR Calendar", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total Bad Debt YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total Bad Deb LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total Bad Debt LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total Waived FC YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total Waived FC LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total Waived FC LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total Writeoffs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total Writeoffs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total Writeoffs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total # Invoices YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total # Invoices LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total # Invoices LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Total # FC YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Total # FC LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Total # FC LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("Send_Email_Statements", "Send Email Statements", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("SHIPCOMPLETE", "Ship Complete Document", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("RMCSHACC", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts Receivable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSORACC", "Sales Order Returns Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);

            // add aging periods
            int periodNumber = 1;
            foreach (string period in AgingPeriods)
            {
                rm00103.AddField($"AGPERAMT_{periodNumber}", period, DataConnector.FieldTypeIdCurrency);
                periodNumber++;
            }

            // add list fields
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit Limit Type", DataConnector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });

            var minPaymentType = rm00101.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });

            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance Charge Amt Type", DataConnector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });

            var maxWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum Writeoff Type", DataConnector.FieldTypeIdEnum);
            maxWriteoffType.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });

            var balanceType = rm00101.AddField("BALNCTYP", "Balance Type", DataConnector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open Item", "Balance Forward" });

            var statementCycle = rm00101.AddField("STMTCYCL", "Statement Cycle", DataConnector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No Statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });

            var defaultCashType = rm00101.AddField("DEFCACTY", "Default Cash Account Type", DataConnector.FieldTypeIdEnum);
            defaultCashType.AddListItems(0, new List<string> { "Checkbook", "Customer" });

            var postResultsTo = rm00101.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });

            var orderFulfillmentShortage = rm00101.AddField("ORDERFULFILLDEFAULT", "Order Fulfillment Shortage Default", DataConnector.FieldTypeIdEnum);
            orderFulfillmentShortage.AddListItems(1, new List<string> { "None", "Back Order Remaining", "Cancel Remaining" });
        }

        private DataConnectorEntity GetCustomerAddressEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListCustomerAddresses, "Customer addresses", ParentConnector);

            var rm00102 = entity.AddTable("RM00102");

            var rm00101 = entity.AddTable("RM00101", "RM00102", DataConnectorTable.DataConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var sy01200 = entity.AddScript("select * from {0}..SY01200 with (NOLOCK) where Master_Type = 'CUS'", "SY01200", "RM00102");
            sy01200.AddJoinFields("Master_ID", "CUSTNMBR");
            sy01200.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddCustomerAddressEntityFields(rm00102, rm00101, sy01200);

            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority", DataConnector.FieldTypeIdString);  

            return entity;
        }
        private static void AddCustomerAddressEntityFields(DataConnectorTable rm00102, DataConnectorTable rm00101, DataConnectorTable sy01200)
        {
            rm00102.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString, true);
            rm00102.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString, true);
            rm00102.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString, true);
            rm00102.AddField("CITY", "City", DataConnector.FieldTypeIdString, true);
            rm00102.AddField("STATE", "State", DataConnector.FieldTypeIdString, true);
            rm00102.AddField("ZIP", "Zip", DataConnector.FieldTypeIdString, true);
            rm00102.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone, true);
            rm00101.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            rm00102.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            rm00102.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            rm00102.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            rm00102.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            rm00102.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            rm00102.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            rm00102.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            rm00102.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            rm00102.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            rm00102.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            rm00102.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            rm00102.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            sy01200.AddField("INET1", "INet1", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET2", "INet2", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET3", "INet3", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET4", "INet4", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET5", "INet5", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET6", "INet6", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET7", "INet7", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET8", "INet8", DataConnector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary Shipto Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1", DataConnector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2", DataConnector.FieldTypeIdString);
            rm00102.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            rm00102.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts Receivable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts Avail Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First Invoice Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit Card ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit Card Exp Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep Distribution History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            rm00102.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            rm00102.AddField("SALSTERR", "Territory ID", DataConnector.FieldTypeIdString);
            rm00102.AddField("LOCNCODE", "Site ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);

            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit Limit Type", DataConnector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });

            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });

            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance Charge Amount Type", DataConnector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });

            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum Writeoff Type", DataConnector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });

            var balanceType = rm00101.AddField("BALNCTYP", "Balance Type", DataConnector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open Item", "Balance Forward" });

            var statementCycle = rm00101.AddField("STMTCYCL", "Statement Cycle", DataConnector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No Statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });

            var defaultCashAccountType = rm00101.AddField("DEFCACT", "Default Cash Account Type", DataConnector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });

            var postResultsTo = rm00101.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });

            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order Fulfillment Shortage Default", DataConnector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back Order Remaining", "Cancel Remaining" });
        }
        
        private DataConnectorEntity GetProspectEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListProspects, "Prospects", ParentConnector);
            var sop00200 = entity.AddTable("SOP00200");
            AddProspectEntityFields(sop00200);
            return entity;
        }
        private static void AddProspectEntityFields(DataConnectorTable sop00200)
        {
            sop00200.AddField("PROSPID", "Prospect ID", DataConnector.FieldTypeIdString, true);
            sop00200.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString, true);
            sop00200.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString, true);
            sop00200.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString, true);
            sop00200.AddField("CITY", "City", DataConnector.FieldTypeIdString, true);
            sop00200.AddField("STATE", "State", DataConnector.FieldTypeIdString, true);
            sop00200.AddField("ZIP", "Zip", DataConnector.FieldTypeIdString, true);
            sop00200.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone, true);
            sop00200.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            sop00200.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            sop00200.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            sop00200.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            sop00200.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            sop00200.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            sop00200.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            sop00200.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            sop00200.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            sop00200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            sop00200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            sop00200.AddField("USER2ENT", "User To Enter", DataConnector.FieldTypeIdString);
            sop00200.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            sop00200.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            sop00200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            sop00200.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
        }

        private DataConnectorEntity GetSalesTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListSalesTrx, "Sales transactions", ParentConnector);

            var svSopTrx = entity.AddTable("svSOPTrx");

            var rm00101 = entity.AddTable("RM00101", "svSOPTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var rm00103 = entity.AddTable("RM00103", "svSOPTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var sop10106 = entity.AddTable("SOP10106", "svSOPTrx");
            sop10106.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop10106.AddJoinFields("SOPNUMBE", "SOPNUMBE");

            var sop10100 = entity.AddTable("SOP10100", "svSOPTrx");
            sop10100.AddJoinFields("SOPTYPE", "SOPTYPE");
            sop10100.AddJoinFields("SOPNUMBE", "SOPNUMBE");

            AddSalesTransactionEntityFields(svSopTrx, rm00101, rm00103, sop10106, sop10100);

            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority from Customer Master", DataConnector.FieldTypeIdString);

            return entity;
        }
        private void AddSalesTransactionEntityFields(DataConnectorTable svSopTrx, DataConnectorTable rm00101, DataConnectorTable rm00103, DataConnectorTable sop10106, DataConnectorTable sop10100)
        {
            var sopType = svSopTrx.AddField("SOPTYPE", "SOP type", DataConnector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            svSopTrx.AddField("SOPNUMBE", "SOP number", DataConnector.FieldTypeIdString, true);
            svSopTrx.AddField("DOCDATE", "Document date", DataConnector.FieldTypeIdDate, true);
            svSopTrx.AddField("CUSTNMBR", "Customer number", DataConnector.FieldTypeIdString, true);
            svSopTrx.AddField("CUSTNAME", "Customer name", DataConnector.FieldTypeIdString, true);
            svSopTrx.AddField("CSTPONBR", "Customer PO number", DataConnector.FieldTypeIdString, true);
            svSopTrx.AddField("PRSTADCD", "Primary ship to address code", DataConnector.FieldTypeIdString, true);
            svSopTrx.AddField("DOCAMNT", "Document amount", DataConnector.FieldTypeIdCurrency, true);

            svSopTrx.AddField("ORIGNUMB", "Original number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("DOCID", "Document ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("GLPOSTDT", "GL posting date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("QUOTEDAT", "Quote date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("QUOEXPDA", "Quote expiration date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ORDRDATE", "Order date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("INVODATE", "Invoice date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("BACKDATE", "Back order date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("RETUDATE", "Return date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ReqShipDate", "Requested ship date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("FUFILDAT", "Fulfillment date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ACTLSHIP", "Actual Ship date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DISCDATE", "Discount date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DUEDATE", "Due date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DTLSTREP", "Date last repeated", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DSTBTCH1", "Dest batch 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("DSTBTCH2", "Dest batch 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID1", "Use document ID 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID2", "Use document ID 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("DISCFRGT", "Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCMISC", "Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVAMT", "Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCRTND", "Discount Returned", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svSopTrx.AddField("DSCDLRAM", "Discount Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVTKN", "Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PROSPECT", "Prospect", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("MSTRNUMB", "Master Number", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("PCKSLPNO", "Packing Slip Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PICTICNU", "Picking Ticket Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("MRKDNAMT", "Markdown Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ShipToName", "ShipToName", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PHNUMBR1", "Phone Number 1", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("PHNUMBR2", "Phone Number 2", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("FAXNUMBR", "Fax Number", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("COMMAMNT", "Commission Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("CMMSLAMT", "Commission Sale Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("NCOMAMNT", "Non-Commissioned Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TRDISPCT", "Trade Discount Percent", DataConnector.FieldTypeIdPercentage);
            svSopTrx.AddField("SUBTOTAL", "Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("REMSUBTO", "Remaining Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("MISCAMNT", "Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTFRTAM", "Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTMSCAM", "Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BCKTXAMT", "Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TXBTXAMT", "Taxable Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPCOMPLETE", "Ship Complete Document", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("PYMTRCVD", "Payment Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DEPRECVD", "Deposit Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("CODAMNT", "COD Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ACCTAMNT", "Account Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDAT01", "User Defined Date 1", DataConnector.FieldTypeIdDate);
            sop10106.AddField("USRDAT02", "User Defined Date 2", DataConnector.FieldTypeIdDate);
            sop10106.AddField("USRTAB01", "User Defined Table 1", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRTAB09", "User Defined Table 2", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRTAB03", "User Defined Table 3", DataConnector.FieldTypeIdString);
            sop10106.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            sop10106.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("COMMNTID", "Comment ID", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_1", "Comment 1", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_2", "Comment 2", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_3", "Comment 3", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_4", "Comment 4", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("USER2ENT", "User To Enter", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ORDAVFRT", "Originating Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVMSC", "Originating Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVAMT", "Originating Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISRTD", "Originating Discount Returned", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISTKN", "Originating Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDATKN", "Originating Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMRKDAM", "Originating Markdown Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OCOMMAMT", "Originating Commission Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCOSAMT", "Originating Commission Sales Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORNCMAMT", "Originating Non-Commissioned Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORSUBTOT", "Originating Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREMSUBT", "Originating Remaining Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMISCAMT", "Originating Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BSIVCTTL", "Based On Invoice Total", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTFRT", "Originating Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTMSC", "Originating Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OBTAXAMT", "Originating Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OTAXTAMT", "Originating Taxable Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ECTRX", "EC Transaction", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORPMTRVD", "Originating Payment Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDEPRVD", "Originating Deposit Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCODAMT", "Originating COD Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORACTAMT", "Originating Account Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svSopTrx.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svSopTrx.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("REPTING", "Repeating", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("TIMEREPD", "Times Repeated", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("TIMETREP", "Times To Repeat", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("DYSTINCR", "Days to Increment", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("TXENGCLD", "Tax Engine Called", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TIMESPRT", "Times Printed", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svSopTrx.AddField("SOPHDRE1", "SOP HDR Errors 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRE2", "SOP HDR Errors 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRFL", "SOP HDR Flags", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SOPLNERR", "SOP LINE Errors", DataConnector.FieldTypeIdString);
            sop10100.AddField("SOPMCERR", "SOP MC Posting Error Messages", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTNAME", "Customer Name from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact Person from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address Code from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS Zone from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping Method from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax Schedule ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS1", "Address 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS2", "Address 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CITY", "City from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("STATE", "State from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ZIP", "Zip from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("PHONE1", "Phone 1 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("PHONE2", "Phone 2 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary Billto Address Code from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary Shipto Address Code from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User Defined 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User Defined 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts Receivable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First Invoice Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit Card ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit Card Exp Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep Distribution History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note Index from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created Date from Customer Master", DataConnector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified Date from Customer Master", DataConnector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total Amount Of NSF Checks Life", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number Of NSF Checks Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer Balance", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_1", "Aging Period Amount 1", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_2", "Aging Period Amount 2", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_3", "Aging Period Amount 3", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_4", "Aging Period Amount 4", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_5", "Aging Period Amount 5", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_6", "Aging Period Amount 6", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_7", "Aging Period Amount 7", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last Aged", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF Check Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last Payment Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last Payment Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last Transaction Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last Transaction Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last Finance Charge Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average Days to Pay - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number ADTP Documents - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number ADTP Documents - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number ADTP Documents - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total Discounts Taken LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total Discounts Available YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total Amount Of NSF Checks YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number Of NSF Checks YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted Other Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted Other Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non Current Scheduled Payments", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total Sales YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total Sales LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total Sales LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total Costs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total Costs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total Costs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total Cash Received YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total Cash Received LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total Cash Received LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total Finance Charges LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total Finance Charges LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance Charges CYTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance Charges LYR Calendar", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total Bad Debt YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total Bad Deb LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total Bad Debt LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total Waived FC YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total Waived FC LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total Waived FC LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total Writeoffs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total Writeoffs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total Writeoffs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total # Invoices YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total # Invoices LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total # Invoices LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Total # FC YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Total # FC LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Total # FC LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPCOMPLETE", "Ship Complete Document from Customer Master", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("APLYWITH", "Apply Withholding", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("CORRCTN", "Correction", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("DOCNCORR", "Document Number Corrected", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("SALEDATE", "Sale date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("SEQNCORR", "Sequence number corrected", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("SHPPGDOC", "Shipping document", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("SIMPLIFD", "Simplified", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("WITHHAMT", "Withholding amount", DataConnector.FieldTypeIdCurrency);
            sop10100.AddField("CORRNXST", "Correction to nonexisting transaction", DataConnector.FieldTypeIdYesNo);
            sop10100.AddField("EXCEPTIONALDEMAND", "Exceptional demand from sales transaction", DataConnector.FieldTypeIdYesNo);

            var documentStatus = svSopTrx.AddField("ASI_Document_Status", "Document status", DataConnector.FieldTypeIdEnum); 
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });

            var originalType = svSopTrx.AddField("ORIGTYPE", "Original type", DataConnector.FieldTypeIdEnum);
            originalType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            
            var voidStatus = svSopTrx.AddField("VOIDSTTS", "Void status", DataConnector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = svSopTrx.AddField("RTCLCMTD", "Rate calculation method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var transactionFrequency = svSopTrx.AddField("TRXFREQU", "Transaction frequency", DataConnector.FieldTypeIdEnum);
            transactionFrequency.AddListItems(1, new List<string> { "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly", "Miscellaneous" });
            
            var taxScheduleSource = svSopTrx.AddField("TXSCHSRC", "Tax schedule source", DataConnector.FieldTypeIdEnum);
            taxScheduleSource.AddListItems(1, new List<string> { "No Error", "Using Site Schedule ID", "Using Ship To Schedule ID", "Using Single Schedule", "Schedule ID Empty", "Schedule ID Not Found", "Shipping Method Not Found", "Setup File Missing/Damaged", "Site Location Not Found", "Address Record Not Found" });
            
            var commissionAppliedTo = svSopTrx.AddField("COMAPPTO", "Commission applied to", DataConnector.FieldTypeIdEnum);
            commissionAppliedTo.AddListItems(0, new List<string> { "Sales", "Total Invoice" });
            
            var miscTaxable = svSopTrx.AddField("MISCTXBL", "Misc taxable", DataConnector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var postingStatus = svSopTrx.AddField("PSTGSTUS", "Posting status", DataConnector.FieldTypeIdEnum);
            postingStatus.AddListItems(1, new List<string> { "Unposted", "Unposted", "Posted", "Posted With Error" });
            
            var allocateBy = svSopTrx.AddField("ALLOCABY", "Allocate by", DataConnector.FieldTypeIdEnum);
            allocateBy.AddListItems(0, new List<string> { "Line Item", "Document/Batch" });
            
            var mcTransactionState = svSopTrx.AddField("MCTRXSTT", "Multicurrency transaction state", DataConnector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var freightTaxable = svSopTrx.AddField("FRGTTXBL", "Freight taxable", DataConnector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit limit type", DataConnector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum payment type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance charge amount type", DataConnector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum writeoff type", DataConnector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", DataConnector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open Item", "Balance Forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement cycle", DataConnector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No Statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default cash account type", DataConnector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsTo = rm00101.AddField("Post_Results_To", "Post results to", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });
            
            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order fulfillment shortage default", DataConnector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back Order Remaining", "Cancel Remaining" });

        }

        private DataConnectorEntity GetSalesLineItemEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListSalesLineItems, "Sales line items", ParentConnector);

            var svSopLine = entity.AddTable("svSOPLine");

            var svSopTrx = entity.AddTable("svSOPTrx", "svSOPLine");
            svSopTrx.AddJoinFields("SOPTYPE", "SOPTYPE");
            svSopTrx.AddJoinFields("SOPNUMBE", "SOPNUMBE");
            svSopTrx.AddJoinFields("ASI_Document_Status", "ASI_Document_Status");

            var rm00101 = entity.AddTable("RM00101", "svSOPTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var rm00103 = entity.AddTable("RM00103", "svSOPTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
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

            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item Shipping Weight", DataConnector.FieldTypeIdQuantity);
            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority from Customer Master", DataConnector.FieldTypeIdString);
            svSopLine.AddField("DECPLCUR - 1", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("DECPLQTY - 1", "Decimal Places QTYS", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        private void AddSalesLineItemEntityFields(DataConnectorTable svSopLine, DataConnectorTable svSopTrx, DataConnectorTable rm00101, DataConnectorTable rm00103, DataConnectorTable iv00101, DataConnectorTable sop10106, DataConnectorTable sop10100, DataConnectorTable sop30300, DataConnectorTable sop10200)
        {
            var sopType = svSopLine.AddField("SOPTYPE", "SOP Type", DataConnector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            svSopLine.AddField("SOPNUMBE", "SOP Number", DataConnector.FieldTypeIdString, true);
            svSopTrx.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            svSopLine.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svSopLine.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            svSopLine.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency, true);
            svSopLine.AddField("UNITPRCE", "Unit Price", DataConnector.FieldTypeIdCurrency, true);
            svSopLine.AddField("QUANTITY", "Quantity", DataConnector.FieldTypeIdQuantity, true);
            svSopLine.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency, true);
            svSopLine.AddField("XTNDPRCE", "Extended Price", DataConnector.FieldTypeIdCurrency, true);

            svSopLine.AddField("ACTLSHIP", "Actual Ship Date", DataConnector.FieldTypeIdDate);
            svSopLine.AddField("BKTSLSAM", "Backout Sales Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("BSIVCTTL", "Based On Invoice Total", DataConnector.FieldTypeIdYesNo);
            svSopLine.AddField("BRKFLD1", "Break Field 1", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("BRKFLD2", "Break Field 2", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("BRKFLD3", "Break Field 3", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("COMMNTID", "Comment ID", DataConnector.FieldTypeIdString);
            svSopLine.AddField("CMPNTSEQ", "Component Sequence", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("CSLSINDX", "COGS Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("DMGDINDX", "Damaged Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("DISCSALE", "Discount Available Sales", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("DROPSHIP", "Drop Ship", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("EXTQTYAL", "Existing Qty Available", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("EXTQTYSEL", "Existing Qty Selected", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("FUFILDAT", "Fulfillment Date", DataConnector.FieldTypeIdDate);
            svSopLine.AddField("INSRINDX", "In Service Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("INUSINDX", "In Use Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("INVINDX", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("ITMTSHID", "Item Tax Schedule ID", DataConnector.FieldTypeIdString);
            svSopLine.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString);
            svSopLine.AddField("MRKDNAMT", "Markdown Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("MKDNINDX", "Markdown Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("MRKDNPCT", "Markdown Percent", DataConnector.FieldTypeIdPercentage);
            svSopLine.AddField("NONINVEN", "Non IV", DataConnector.FieldTypeIdYesNo);
            svSopLine.AddField("ORBKTSLS", "Originating Backout Sales Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("ORDAVSLS", "Originating Discount Available Sales", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("OXTNDPRC", "Originating Extended Price", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("ORMRKDAM", "Originating Markdown Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("OREPRICE", "Originating Remaining Price", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("OTAXTAMT", "Originating Taxable Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("ORUNTCST", "Originating Unit Cost", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("ORUNTPRC", "Originating Unit Price", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ATYALLOC", "QTY Allocated", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYCANCE", "QTY Canceled", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYCANOT", "QTY Canceled Other", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYDMGED", "QTY Damaged", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYFULFI", "QTY Fulfilled", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYBSUOM", "QTY In Base U Of M", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYINSVC", "QTY In Service", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYINUSE", "QTY In Use", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYONHND", "QTY On Hand", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYORDER", "QTY Ordered", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRBAC", "QTY Prev Back Ordered", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRBOO", "QTY Prev BO On Order", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRINV", "QTY Prev Invoiced", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRORD", "QTY Prev Ordered", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYPRVRECVD", "QTY Prev Received", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYRECVD", "QTY Received", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYREMAI", "QTY Remaining", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYREMBO", "QTY Remaining On BO", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYRTRND", "QTY Returned", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYSLCTD", "QTY Selected", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYTBAOR", "QTY To Back Order", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYTOINV", "QTY To Invoice", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("QTYTORDR", "QTY To Order", DataConnector.FieldTypeIdQuantity);
            svSopLine.AddField("REMPRICE", "Remaining Price", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("ReqShipDate", "Requested Ship Date", DataConnector.FieldTypeIdDate);
            svSopLine.AddField("RTNSINDX", "Returns Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("SLSINDX", "Sales Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svSopLine.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            svSopLine.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svSopLine.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svSopLine.AddField("SOPLNERR", "SOP LINE Errors", DataConnector.FieldTypeIdString);
            svSopLine.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("TXBTXAMT", "Taxable Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svSopLine.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svSopLine.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            sop10200.AddField("QTYONPO", "QTY On PO", DataConnector.FieldTypeIdQuantity);
            iv00101.AddField("NOTEINDX", "Note Index from Item Master", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("ITMSHNAM", "Item Short Name", DataConnector.FieldTypeIdString);
            iv00101.AddField("ITMGEDSC", "Item Generic Description", DataConnector.FieldTypeIdString);
            iv00101.AddField("STNDCOST", "Standard Cost", DataConnector.FieldTypeIdCurrency);
            iv00101.AddField("CURRCOST", "Current Cost", DataConnector.FieldTypeIdCurrency);
            iv00101.AddField("DECPLQTY - 1", "Decimal Places QTYS from Item Master", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("DECPLCUR - 1", "Decimal Places Currency from Item Master", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("ITMTSHID", "Item Tax Schedule ID from Item Master", DataConnector.FieldTypeIdString);
            iv00101.AddField("IVIVINDX", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVIVOFIX", "Inventory Offset Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVCOGSIX", "COGS Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLSIDX", "Sales Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLDSIX", "Sales Discounts Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLRNIX", "Sales Returns Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINUSIX", "In Use Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINSVIX", "In Service Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVDMGIDX", "Damaged Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVVARIDX", "Variances Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("DPSHPIDX", "Drop Ship Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURPVIDX", "Purchase Price Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVRETIDX", "Inventory Returns Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ASMVRIDX", "Assembly Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITMCLSCD", "Item Class Code", DataConnector.FieldTypeIdString);
            iv00101.AddField("LOTTYPE", "Lot Type", DataConnector.FieldTypeIdString);
            iv00101.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("KPDSTHST", "Keep Distribution History", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("ALWBKORD", "Allow Back Orders", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("UOMSCHDL", "U Of M Schedule", DataConnector.FieldTypeIdString);
            iv00101.AddField("ALTITEM1", "Alternate Item 1", DataConnector.FieldTypeIdString);
            iv00101.AddField("ALTITEM2", "Alternate Item 2", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_1", "User Category Value 1", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_2", "User Category Value 2", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_3", "User Category Value 3", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_4", "User Category Value 4", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_5", "User Category Value 5", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_6", "User Category Value 6", DataConnector.FieldTypeIdString);
            iv00101.AddField("MSTRCDTY", "Master Record Type", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("MODIFDT", "Modified Date from Item Master", DataConnector.FieldTypeIdDate);
            iv00101.AddField("CREATDDT", "Created Date from Item Master", DataConnector.FieldTypeIdDate);
            iv00101.AddField("WRNTYDYS", "Warranty Days", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "PriceLevel from Item Master", DataConnector.FieldTypeIdString);
            iv00101.AddField("CGSINFLX", "COGS Inflation Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("CGSMCIDX", "COGS Monetary Correction Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINFIDX", "Inventory Inflation Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("INVMCIDX", "Inventory Monetary Correction Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITEMCODE", "Item Code from Item Master", DataConnector.FieldTypeIdString);
            iv00101.AddField("LASTGENSN", "Last Generated Serial Number", DataConnector.FieldTypeIdString);
            iv00101.AddField("PriceGroup", "Price Group", DataConnector.FieldTypeIdString);
            iv00101.AddField("PINFLIDX", "Purch Inflation Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURMCIDX", "Purch Monetary Correction Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PRCHSUOM", "Purchasing U Of M", DataConnector.FieldTypeIdString);
            iv00101.AddField("SELNGUOM", "Selling U Of M", DataConnector.FieldTypeIdString);
            iv00101.AddField("TCC", "Tax Commodity Code", DataConnector.FieldTypeIdString);
            iv00101.AddField("UPPVIDX", "Unrealized Purchase Price Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("LOCNCODE", "Location Code from Item Master", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ORIGNUMB", "Original Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("DOCID", "Document ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("DOCDATE", "Document Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("GLPOSTDT", "GL Posting Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("QUOTEDAT", "Quote Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("QUOEXPDA", "Quote Expiration Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ORDRDATE", "Order Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("INVODATE", "Invoice Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("BACKDATE", "Back Order Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("RETUDATE", "Return Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ReqShipDate", "Requested Ship Date from Sales Transaction", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("FUFILDAT", "Fulfillment Date from Sales Transaction", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ACTLSHIP", "Actual Ship Date from Sales Transaction", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DTLSTREP", "Date Last Repeated", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("DSTBTCH1", "Dest Batch 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("DSTBTCH2", "Dest Batch 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID1", "Use Document ID 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("USDOCID2", "Use Document ID 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("DISCFRGT", "Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCMISC", "Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVAMT", "Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISCRTND", "Discount Returned", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svSopTrx.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DISAVTKN", "Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PRCLEVEL", "PriceLevel from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("LOCNCODE", "Location Code from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PROSPECT", "Prospect", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("MSTRNUMB", "Master Number", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("PCKSLPNO", "Packing Slip Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PICTICNU", "Picking Ticket Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("MRKDNAMT", "Markdown Amount from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PRSTADCD", "Primary Shipto Address Code from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CNTCPRSN", "Contact Person from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ShipToName", "ShipToName from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS1", "Address 1 from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS2", "Address 2 from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CITY", "City from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("STATE", "State from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ZIPCODE", "Zip Code from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("COUNTRY", "Country from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PHNUMBR1", "Phone Number 1 from Sales Transaction", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("PHNUMBR2", "Phone Number 2 from Sales Transaction", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("FAXNUMBR", "Fax Number from Sales Transaction", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("COMMAMNT", "Commission Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("CMMSLAMT", "Commission Sale Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("NCOMAMNT", "Non-Commissioned Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("SHIPMTHD", "Shipping Method from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TRDISAMT", "Trade Discount Amount from Sales Transaction", DataConnector.FieldTypeIdPercentage);
            svSopTrx.AddField("TRDISPCT", "Trade Discount Percent", DataConnector.FieldTypeIdPercentage);
            svSopTrx.AddField("SUBTOTAL", "Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("REMSUBTO", "Remaining Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("EXTDCOST", "Extended Cost from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("MISCAMNT", "Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TAXSCHID", "Tax Schedule ID from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTFRTAM", "Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BKTMSCAM", "Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BCKTXAMT", "Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TXBTXAMT", "Taxable Tax Amount from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TAXAMNT", "Tax Amount from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("PYMTRCVD", "Payment Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("DEPRECVD", "Deposit Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("CODAMNT", "COD Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ACCTAMNT", "Account Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("SALSTERR", "Sales Territory from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SLPRSNID", "Salesperson ID from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDAT01", "User Defined Date 1", DataConnector.FieldTypeIdDate);
            sop10106.AddField("USRDAT02", "User Defined Date 2", DataConnector.FieldTypeIdDate);
            sop10106.AddField("USRTAB01", "User Defined Table 1", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRTAB09", "User Defined Table 2", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRTAB03", "User Defined Table 3", DataConnector.FieldTypeIdString);
            sop10106.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            sop10106.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            sop10106.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("TRXSORCE", "TRX Source from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("COMMNTID", "Comment ID from Sales Transaction", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_1", "Comment 1", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_2", "Comment 2", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_3", "Comment 3", DataConnector.FieldTypeIdString);
            sop10106.AddField("COMMENT_4", "Comment 4", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("USER2ENT", "User To Enter", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("CREATDDT", "Created Date from Sales Transaction", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("MODIFDT", "Modified Date from Sales Transaction", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("ORDAVFRT", "Originating Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVMSC", "Originating Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDAVAMT", "Originating Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISRTD", "Originating Discount Returned", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDISTKN", "Originating Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDATKN", "Originating Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMRKDAM", "Originating Markdown Amount from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OCOMMAMT", "Originating Commission Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCOSAMT", "Originating Commission Sales Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORNCMAMT", "Originating Non-Commissioned Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTDISAM", "Originating Trade Discount Amount from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORSUBTOT", "Originating Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREMSUBT", "Originating Remaining Subtotal", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OREXTCST", "Originating Extended Cost from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMISCAMT", "Originating Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("BSIVCTTL", "Based On Invoice Total from Sales Transaction", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTFRT", "Originating Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORBKTMSC", "Originating Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OBTAXAMT", "Originating Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("OTAXTAMT", "Originating Taxable Tax Amount from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORTAXAMT", "Originating Tax Amount from Sales Transaction", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ECTRX", "EC Transaction", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORPMTRVD", "Originating Payment Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORDEPRVD", "Originating Deposit Received", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORCODAMT", "Originating COD Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("ORACTAMT", "Originating Account Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svSopTrx.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svSopTrx.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("REPTING", "Repeating", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("TIMEREPD", "Times Repeated", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("TIMETREP", "Times To Repeat", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("DYSTINCR", "Days to Increment", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("TXENGCLD", "Tax Engine Called", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svSopTrx.AddField("TIMESPRT", "Times Printed", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("NOTEINDX", "Note Index from Sales Transaction", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("CURRNIDX", "Currency Index from Sales Transaction", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svSopTrx.AddField("SOPHDRE1", "SOP HDR Errors 1", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRE2", "SOP HDR Errors 2", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SOPHDRFL", "SOP HDR Flags", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SOPLNERR", "SOP LINE Errors from Sales Transaction", DataConnector.FieldTypeIdString);
            sop10100.AddField("SOPMCERR", "SOP MC Posting Error Messages", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("ADDRESS3", "Address 3 from Sales Transaction", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTNAME", "Customer Name from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact Person from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address Code from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS Zone from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping Method from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax Schedule ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS1", "Address 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS2", "Address 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CITY", "City from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("STATE", "State from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ZIP", "Zip from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("PHONE1", "Phone 1 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("PHONE2", "Phone 2 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary Billto Address Code from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary Shipto Address Code from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User Defined 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User Defined 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts Receivable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First Invoice Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit Card ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit Card Exp Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep Distribution History from Customer Master", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep Calendar History from Customer Master", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep Period History from Customer Master", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep Trx History from Customer Master", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note Index from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created Date from Customer Master", DataConnector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified Date from Customer Master", DataConnector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total Amount Of NSF Checks Life", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number Of NSF Checks Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer Balance", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_1", "Aging Period Amount 1", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_2", "Aging Period Amount 2", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_3", "Aging Period Amount 3", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_4", "Aging Period Amount 4", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_5", "Aging Period Amount 5", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_6", "Aging Period Amount 6", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_7", "Aging Period Amount 7", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last Aged", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF Check Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last Payment Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last Payment Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last Transaction Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last Transaction Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last Finance Charge Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average Days to Pay - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number ADTP Documents - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number ADTP Documents - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number ADTP Documents - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total Discounts Taken LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total Discounts Available YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total Amount Of NSF Checks YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number Of NSF Checks YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted Other Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted Other Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non Current Scheduled Payments", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total Sales YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total Sales LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total Sales LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total Costs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total Costs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total Costs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total Cash Received YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total Cash Received LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total Cash Received LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total Finance Charges LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total Finance Charges LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance Charges CYTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance Charges LYR Calendar", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total Bad Debt YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total Bad Deb LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total Bad Debt LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total Waived FC YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total Waived FC LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total Waived FC LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total Writeoffs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total Writeoffs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total Writeoffs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total # Invoices YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total # Invoices LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total # Invoices LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Total # FC YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Total # FC LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Total # FC LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("SHIPCOMPLETE", "Ship Complete Document from Customer Master", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("APLYWITH", "Apply Withholding", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("CORRCTN", "Correction", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("DOCNCORR", "Document Number Corrected from Sales Transaction", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("PHONE3", "Phone 3 from Sales Transaction", DataConnector.FieldTypeIdPhone);
            svSopTrx.AddField("SALEDATE", "Sale Date", DataConnector.FieldTypeIdDate);
            svSopTrx.AddField("SEQNCORR", "Sequence Number Corrected", DataConnector.FieldTypeIdInteger);
            svSopTrx.AddField("SHPPGDOC", "Shipping Document", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("SIMPLIFD", "Simplified", DataConnector.FieldTypeIdYesNo);
            svSopTrx.AddField("WITHHAMT", "Withholding Amount", DataConnector.FieldTypeIdCurrency);
            sop10100.AddField("CORRNXST", "Correction to Nonexisting Transaction", DataConnector.FieldTypeIdYesNo);
            sop10100.AddField("SOPHDRE3", "SOP HDR Errors 3", DataConnector.FieldTypeIdString);
            svSopTrx.AddField("SHIPCOMPLETE", "Ship Complete Document", DataConnector.FieldTypeIdYesNo);
            sop30300.AddField("DOCNCORR", "Document Number Corrected", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ITEMCODE", "Item Code", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ORGSEQNM", "Original Sequence Number Corrected", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("ODECPLCU - 1", "Originating Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            svSopLine.AddField("TAXSCHID", "Tax Schedule ID from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("PRSTADCD", "Primary Shipto Address Code from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ShipToName", "ShipToName from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("CNTCPRSN", "Contact Person from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ADDRESS1", "Address 1 from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ADDRESS2", "Address 2 from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ADDRESS3", "Address 3 from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("CITY", "City from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("STATE", "State from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("ZIPCODE", "Zip Code from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("COUNTRY", "Country from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("PHONE1", "Phone 1 from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("PHONE2", "Phone 2 from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("PHONE3", "Phone 3 from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("FAXNUMBR", "Fax Number from Sales Line Item", DataConnector.FieldTypeIdString);
            svSopLine.AddField("EXCEPTIONALDEMAND", "Exceptional Demand from Sales Line Item", DataConnector.FieldTypeIdYesNo);
            svSopLine.AddField("CONTNBR", "Contract Number", DataConnector.FieldTypeIdString);
            svSopLine.AddField("CONTSTARTDTE", "Contract Start Date", DataConnector.FieldTypeIdDate);
            svSopLine.AddField("CONTENDDTE", "Contract End Date", DataConnector.FieldTypeIdDate);
            svSopLine.AddField("CONTITEMNBR", "Contract Item Number", DataConnector.FieldTypeIdString);
            svSopLine.AddField("CONTSERIALNBR", "Contract Serial Number", DataConnector.FieldTypeIdString);

            var ivItemTaxable = svSopLine.AddField("IVITMTXB", "IV Item Taxable", DataConnector.FieldTypeIdEnum);
            ivItemTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var markdownType = svSopLine.AddField("MRKDNTYP", "Markdown Type", DataConnector.FieldTypeIdEnum);
            markdownType.AddListItems(0, new List<string> { "Percent", "Dollar" });
            
            var purchasingStatus = svSopLine.AddField("PURCHSTAT", "Purchasing Status", DataConnector.FieldTypeIdEnum);
            purchasingStatus.AddListItems(1, new List<string> { "None", "Needs Purchase", "Purchased", "Partially Received", "Fully Received" });
            
            var documentStatus = svSopLine.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var itemType = iv00101.AddField("ITEMTYPE", "Item Type", DataConnector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales Inventory", "Discontinued", "Kit", "Misc Charges", "Services", "Flat Fee" });
            
            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax Options", DataConnector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var itemTrackingOption = iv00101.AddField("ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
            
            var valuationMethod = iv00101.AddField("VCTNMTHD", "Valuation Method", DataConnector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO Perpetual", "LIFO Perpetual", "Average Perpetual", "FIFO Periodic", "LIFO Periodic" });
            
            var abcCode = iv00101.AddField("ABCCODE", "ABC Code", DataConnector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });
            
            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS Account Source", DataConnector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From Component Item", "From Kit Item" });
            
            var priceMethod = iv00101.AddField("PRICMTHD", "Price Method", DataConnector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency Amount", "% of List Price", "% Markup - Current Cost", "% Markup - Standard Cost", "% Margin - Current Cost", "% Margin - Standard Cost" });
            
            var originalType = svSopTrx.AddField("ORIGTYPE", "Original Type", DataConnector.FieldTypeIdEnum);
            originalType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            
            var voidStatus = svSopTrx.AddField("VOIDSTTS", "Void Status", DataConnector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = svSopTrx.AddField("RTCLCMTD", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var transactionFrequency = svSopTrx.AddField("TRXFREQU", "Transaction Frequency", DataConnector.FieldTypeIdEnum);
            transactionFrequency.AddListItems(1, new List<string> { "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly", "Miscellaneous" });
            
            var taxScheduleSourceFromSalesTransaction = svSopTrx.AddField("TXSCHSRC", "Tax Schedule Source from Sales Transaction", DataConnector.FieldTypeIdEnum);
            taxScheduleSourceFromSalesTransaction.AddListItems(1, new List<string> { "No Error", "Using Site Schedule ID", "Using Ship To Schedule ID", "Using Single Schedule", "Schedule ID Empty", "Schedule ID Not Found", "Shipping Method Not Found", "Setup File Missing/Damaged", "Site Location Not Found", "Address Record Not Found" });
            
            var commissionAppliedTo = svSopTrx.AddField("COMAPPTO", "Commission Applied To", DataConnector.FieldTypeIdEnum);
            commissionAppliedTo.AddListItems(0, new List<string> { "Sales", "Total Invoice" });
            
            var miscTaxable = svSopTrx.AddField("MISCTXBL", "Misc Taxable", DataConnector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var postingStatus = svSopTrx.AddField("PSTGSTUS", "Posting Status", DataConnector.FieldTypeIdEnum);
            postingStatus.AddListItems(1, new List<string> { "Unposted", "Unposted", "Posted", "Posted With Error" });
            
            var allocateBy = svSopTrx.AddField("ALLOCABY", "Allocate By", DataConnector.FieldTypeIdEnum);
            allocateBy.AddListItems(0, new List<string> { "Line Item", "Document/Batch" });
            
            var mcTransactionState = svSopTrx.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var freightTaxable = svSopTrx.AddField("FRGTTXBL", "Freight Taxable", DataConnector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit Limit Type", DataConnector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance Charge Amount Type", DataConnector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum Writeoff Type", DataConnector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance Type", DataConnector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open Item", "Balance Forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement Cycle", DataConnector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No Statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default Cash Account Type", DataConnector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsTo = rm00101.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });
            
            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order Fulfillment Shortage Default", DataConnector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back Order Remaining", "Cancel Remaining" });
            
            var taxScheduleSourceFromSalesLinesItem = svSopLine.AddField("TXSCHSRC", "Tax Schedule Source from Sales Line Item", DataConnector.FieldTypeIdEnum);
            taxScheduleSourceFromSalesLinesItem.AddListItems(1, new List<string> { "No Error", "Using Site Schedule ID", "Using Ship To Schedule ID", "Using Single Schedule", "Schedule ID Empty", "Schedule ID Not Found", "Shipping Method Not Found", "Setup File Missing/Damaged", "Site Location Not Found", "Address Record Not Found" });

        }
            
        private DataConnectorEntity GetReceivablesTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListReceivablesTrx, "Receivables transactions", ParentConnector);

            var svRmTrx = entity.AddTable("svRMTrx");

            var rm00101 = entity.AddTable("RM00101", "svRMTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var rm00103 = entity.AddTable("RM00103", "svRMTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
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

            entity.AddCalculation("case RM00101.CUSTPRIORITY when 1 then 'None' else cast(RM00101.CUSTPRIORITY - 1 as varchar(3)) end", "Priority from customer master", DataConnector.FieldTypeIdString);

            return entity;
        }
        private void AddReceivablesTransactionEntityFields(DataConnectorTable svRmTrx, DataConnectorTable rm00101, DataConnectorTable rm00103, DataConnectorTable mc020102, DataConnectorTable rm10301, DataConnectorTable rm20101)
        {
            svRmTrx.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString, true);
            svRmTrx.AddField("DOCNUMBR", "Document Number", DataConnector.FieldTypeIdString, true);
            var documentType = svRmTrx.AddField("RMDTYPAL", "Document Type", DataConnector.FieldTypeIdEnum, true);
            documentType.AddListItems(1, new List<string> { "Sales / Invoices", "Scheduled Payments", "Debit Memos", "Finance Charges", "Service / Repairs", "Warranty", "Credit Memo", "Returns", "Payments" });
            svRmTrx.AddField("DOCDATE", "Document Date", DataConnector.FieldTypeIdDate, true);
            svRmTrx.AddField("SLSAMNT", "Sales Amount", DataConnector.FieldTypeIdCurrency, true);

            rm10301.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            rm10301.AddField("DOCPRFIX", "Document Prefix", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("DOCDESCR", "Document Description", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("CBKIDCRD", "Checkbook ID Credit Card", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("CBKIDCSH", "Checkbook ID Cash", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("CBKIDCHK", "Checkbook ID Check", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            rm10301.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            rm10301.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("GLPOSTDT", "GL Posting Date", DataConnector.FieldTypeIdDate);
            svRmTrx.AddField("LSTEDTDT", "Last Edit Date", DataConnector.FieldTypeIdDate);
            svRmTrx.AddField("LSTUSRED", "Last User to Edit", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("ORTRXAMT", "Original Trx Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("CURTRXAM", "Current Trx Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("COSTAMNT", "Cost Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("MISCAMNT", "Misc Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("COMDLRAM", "Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("NCOMAMNT", "Non-Commissioned Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("CASHAMNT", "Cash Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISAVAMT", "Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISAVTKN", "Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISCRTND", "Discount Returned", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            svRmTrx.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svRmTrx.AddField("WROFAMNT", "Write Off Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("DINVPDOF", "Date Invoice Paid Off", DataConnector.FieldTypeIdDate);
            svRmTrx.AddField("PPSAMDED", "PPS Amount Deducted", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("GSTDSAMT", "GST Discount Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("CSPORNBR", "Customer PO Number", DataConnector.FieldTypeIdString);
            rm10301.AddField("BKTSLSAM", "Backout Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("BKTFRTAM", "Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("BKTMSCAM", "Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("TXENGCLD", "Tax Engine Called", DataConnector.FieldTypeIdYesNo);
            rm10301.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("APPLDAMT", "Applied Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("CASHDATE", "Cash Date", DataConnector.FieldTypeIdDate);
            rm10301.AddField("DCNUMCSH", "Document Number Cash", DataConnector.FieldTypeIdString);
            rm10301.AddField("CHEKAMNT", "Check Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            rm10301.AddField("DCNUMCHK", "Document Number Check", DataConnector.FieldTypeIdString);
            rm10301.AddField("CRCRDAMT", "Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("CRCRDNAM", "Credit Card Name", DataConnector.FieldTypeIdString);
            rm10301.AddField("RCTNCCRD", "Receipt Number Credit Card", DataConnector.FieldTypeIdString);
            rm10301.AddField("CRCARDDT", "Credit Card Date", DataConnector.FieldTypeIdDate);
            rm10301.AddField("DCNUMCRD", "Document Number Credit Card", DataConnector.FieldTypeIdString);
            rm10301.AddField("ACCTAMNT", "Account Amount", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("TRDDISCT", "Trade Discount", DataConnector.FieldTypeIdInteger);
            rm20101.AddField("DELETE1", "Delete", DataConnector.FieldTypeIdYesNo);
            rm20101.AddField("AGNGBUKT", "Aging Bucket", DataConnector.FieldTypeIdInteger);
            svRmTrx.AddField("VOIDDATE", "Void Date", DataConnector.FieldTypeIdDate);
            svRmTrx.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svRmTrx.AddField("SLSCHDID", "Sales Schedule ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svRmTrx.AddField("Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            rm10301.AddField("POSTED", "Posted", DataConnector.FieldTypeIdYesNo);
            svRmTrx.AddField("APLYWITH", "Apply Withholding", DataConnector.FieldTypeIdYesNo);
            svRmTrx.AddField("SALEDATE", "Sale Date", DataConnector.FieldTypeIdDate);
            svRmTrx.AddField("CORRCTN", "Correction", DataConnector.FieldTypeIdYesNo);
            svRmTrx.AddField("SIMPLIFD", "Simplified", DataConnector.FieldTypeIdYesNo);
            rm10301.AddField("BALFWDNM", "Balance Forward Number", DataConnector.FieldTypeIdString);
            rm10301.AddField("RMTREMSG", "RM TRX Posting Error Messages", DataConnector.FieldTypeIdString);
            rm10301.AddField("RMDPEMSG", "RM Dist Posting Error Messages", DataConnector.FieldTypeIdString);
            mc020102.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            mc020102.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            mc020102.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            mc020102.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            mc020102.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            mc020102.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            mc020102.AddField("ORCTRXAM", "Originating Current Trx Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORSLSAMT", "Originating Sales Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORCSTAMT", "Originating Cost Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORMISCAMT", "Originating Misc Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORCASAMT", "Originating Cash Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORCHKAMT", "Originating Check Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORCCDAMT", "Originating Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORAPPAMT", "Originating Applied Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORDISTKN", "Originating Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORDAVAMT", "Originating Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORDATKN", "Originating Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORDISRTD", "Originating Discount Returned", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORORGTRX", "Originating Original Trx Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORWROFAM", "Originating Write Off Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORCOMAMT", "Originating Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORBKTSLS", "Originating Backout Sales Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORBKTFRT", "Originating Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("ORBKTMSC", "Originating Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("UNGANLOS", "Unrealized Gain-Loss Amount", DataConnector.FieldTypeIdCurrency);
            mc020102.AddField("RMMCERRS", "RM MC Posting Error Messages", DataConnector.FieldTypeIdString);
            mc020102.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            mc020102.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer Name from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer Number from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address Code from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping Method from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax Schedule ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            rm00101.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            rm00101.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            rm00101.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            rm00101.AddField("ZIP", "Zip", DataConnector.FieldTypeIdString);
            rm00101.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            rm00101.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary Shipto Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement Address Code", DataConnector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1", DataConnector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts Receivable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First Invoice Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit Card ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit Card Exp Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep Distribution History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note Index from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created Date from Customer Master", DataConnector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified Date from Customer Master", DataConnector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total Amount Of NSF Checks Life", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number Of NSF Checks Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer Balance", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_1", "Aging Period Amount 1", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_2", "Aging Period Amount 2", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_3", "Aging Period Amount 3", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_4", "Aging Period Amount 4", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_5", "Aging Period Amount 5", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_6", "Aging Period Amount 6", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_7", "Aging Period Amount 7", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last Aged", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF Check Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last Payment Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last Payment Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last Transaction Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last Transaction Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last Finance Charge Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average Days to Pay - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number ADTP Documents - Life", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number ADTP Documents - Year", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number ADTP Documents - LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total Discounts Taken LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total Discounts Available YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total Amount Of NSF Checks YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number Of NSF Checks YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted Other Sales Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted Other Cash Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non Current Scheduled Payments", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total Sales YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total Sales LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total Sales LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total Costs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total Costs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total Costs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total Cash Received YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total Cash Received LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total Cash Received LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total Finance Charges YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total Finance Charges LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total Finance Charges LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance Charges CYTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance Charges LYR Calendar", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total Bad Debt YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total Bad Deb LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total Bad Debt LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total Waived FC YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total Waived FC LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total Waived FC LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total Writeoffs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total Writeoffs LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total Writeoffs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total # Invoices YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total # Invoices LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total # Invoices LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Total # FC YTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Total # FC LTD", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Total # FC LYR", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", DataConnector.FieldTypeIdCurrency);
            rm10301.AddField("CORRNXST", "Correction to Nonexisting Transaction", DataConnector.FieldTypeIdYesNo);
            rm10301.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            rm10301.AddField("DocPrinted", "DocPrinted", DataConnector.FieldTypeIdYesNo);
            rm10301.AddField("DOCNCORR", "Document Number Corrected", DataConnector.FieldTypeIdString);
            rm10301.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            svRmTrx.AddField("ECTRX", "EC Transaction", DataConnector.FieldTypeIdYesNo);
            svRmTrx.AddField("Electronic", "Electronic", DataConnector.FieldTypeIdYesNo);
            rm10301.AddField("PSTGSTUS", "Posting Status", DataConnector.FieldTypeIdInteger);
            rm10301.AddField("RMTREMSG2", "RM TRX Posting Error Messages 2", DataConnector.FieldTypeIdString);
            svRmTrx.AddField("Factoring", "Factoring", DataConnector.FieldTypeIdYesNo);

            var documentStatus = svRmTrx.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "Posted", "History" });
            
            var cashReceiptType = svRmTrx.AddField("CSHRCTYP", "Cash Receipt Type", DataConnector.FieldTypeIdEnum);
            cashReceiptType.AddListItems(1, new List<string> { "Check", "Cash", "Credit Card" });
            
            var commissionAppliedTo = rm10301.AddField("COMAPPTO", "Commission Applied To", DataConnector.FieldTypeIdEnum);
            commissionAppliedTo.AddListItems(0, new List<string> { "Sales", "Total Invoice" });
            
            var voidStatus = svRmTrx.AddField("VOIDSTTS", "Void Status", DataConnector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = mc020102.AddField("RTCLCMTD", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit Limit Type", DataConnector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var minimumPaymentType = rm00101.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance Charge Amount Type", DataConnector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum Writeoff Type", DataConnector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance Type", DataConnector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open Item", "Balance Forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement Cycle", DataConnector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No Statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default Cash Account Type", DataConnector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsTo = rm00101.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });
            
            var orderFulfillmentShortageDefault = rm00101.AddField("ORDERFULFILLDEFAULT", "Order Fulfillment Shortage Default", DataConnector.FieldTypeIdEnum);
            orderFulfillmentShortageDefault.AddListItems(1, new List<string> { "None", "Back Order Remaining", "Cancel Remaining" });

        }

    }
}
