using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class DynamicsGpPurchasingModule : DynamicsGpModule
    {

        private const short GpSmartListVendors = 3;
        private const short GpSmartListPurchaseOrders = 7;
        private const short GpSmartListPayablesTrx = 9;
        private const short GpSmartListVendorAddresses = 16;
        private const short GpSmartListPurchaseLineItems = 21;
        private const short GpSmartListReceivingsTrx = 27;
        private const short GpSmartListReceivingsLineItems = 28;
        
        public DynamicsGpPurchasingModule(DynamicsGpConnector connector) : base(connector)
        {
            Name = "Purchasing";
            Installed = true;
            UserDefined1 = "User Defined 1";
            UserDefined2 = "User Defined 2";
            ParentConnector = connector;
        }

        public string UserDefined1 { get; set; }
        public string UserDefined2 { get; set; }

        public override void AddEntities()
        {
            Entities.Add(GetVendorEntity());
            Entities.Add(GetVendorAddressEntity());
            Entities.Add(GetPurchaseOrderEntity());
            Entities.Add(GetPurchaseOrderLineEntity());
            Entities.Add(GetPayablesTransactionEntity());
            Entities.Add(GetReceivingsTransactionEntity());
            Entities.Add(GetReceivingsTransactionLineEntity());
        }

        private DataConnectorEntity GetVendorEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListVendors, "Vendors", ParentConnector);
            var pm00200 = entity.AddTable("PM00200");
            var pm00201 = entity.AddTable("PM00201", "PM00200", DataConnectorTable.DataConnectorTableJoinType.Inner);
            pm00201.AddJoinFields("VENDORID", "VENDORID");
            AddVendorEntityFields(pm00200, pm00201);
            return entity;
        }
        private void AddVendorEntityFields(DataConnectorTable pm00200, DataConnectorTable pm00201)
        {
            pm00200.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("CITY", "City", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("STATE", "State", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("PHNUMBR1", "Phone Number 1", DataConnector.FieldTypeIdPhone, true);
            pm00200.AddField("VNDCHKNM", "Vendor Check Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor Address Code - Primary", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor Address Code - Purchase Address", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor Address Code - Ship From", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor Address Code - Remit To", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor Class ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor Contact", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR2", "Phone Number 2", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax Number", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum Order", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum Invoice Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment1", DataConnector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment2", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", UserDefined1, DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", UserDefined2, DataConnector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit Limit Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment Priority", DataConnector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject To PPS Deductions", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS Tax Rate", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction Exemption/Variation Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate Commencing Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate Expiration Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting Obligation Undertaken", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration Date Obligation", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed Payee", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts Payable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc Charges Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write Offs Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade Discount Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase Price Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pm00201.AddField("PM00201.HIESTBAL", "Highest Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.CURRBLNC", "Current Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.NOINVYTD", "Number of Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOINVLIF", "Number of Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOINVLYR", "Number of Invoices LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOPINYTD", "Number of Paid Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOPILIFE", "Number of Paid Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.AMBLDTYD", "Amount Billed YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMBLDLIF", "Amount Billed LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMBLDLYR", "Amount Billed LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMTPDYTD", "Amount Paid YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMTPDLIF", "Amount Paid LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMTPDLYR", "Amount Paid LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TEN99AYTD", "1099 Amount YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TEN99ALIF", "1099 Amount LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TEN99ALYR", "1099 Amount LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISAVYTD", "Discount Available YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISAVLIF", "Discount Available LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISAVLYR", "Discount Available LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISTKYTD", "Discount Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISTKNLF", "Discount Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISTKLYR", "Discount Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISLSYTD", "Discount Lost YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISLSTLF", "Discount Lost LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISLSLYR", "Discount Lost LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.FINCHLIF", "Finance Charge LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.FINCHLYR", "Finance Charge LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.FINCHYTD", "Finance Charge YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.RTRNSYTD", "Returns YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.RTRNSLIF", "Returns LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.RTRNSLYR", "Returns LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TRDTKLIF", "Trade Discounts Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TRDTLYR", "Trade Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TRDTYTD", "Trade Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.NFNCHLIF", "Number of Finance Charges LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NFNCHLYR", "Number of Finance Charges LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NFNCHYTD", "Number of Finance Charges YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.RTNGOWED", "Retainage Owed", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.LSTCHNUM", "Last Check Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("PM00201.LSTCHKDT", "Last Check Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("PM00201.LSTCHAMT", "Last Check Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.LSTINNUM", "Last Invoice Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("PM00201.LSTINVAM", "Last Invoice Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.LSTPURDT", "Last Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("PM00201.FSTPURDT", "First Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("PM00201.CURUNPBN", "Current Unapplied Payment Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.UNPDFNCH", "Unpaid Finance Charges", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DYCHTCLR", "Days Checks To Clear", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received", DataConnector.FieldTypeIdYesNo);
            pm00201.AddField("PM00201.Withholding_LIFE", "Withholding LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WITHLYR", "Withholding LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WITHYTD", "Withholding YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RM00101.CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString);

            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });

            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor Status", DataConnector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });

            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 Type", DataConnector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });

            var maximumInvoiceAmount = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", DataConnector.FieldTypeIdEnum);
            maximumInvoiceAmount.AddListItems(0, new List<string> { "No Maximum", "Amount" });

            var postToCashAccountFrom = pm00200.AddField("PTCSHACF", "Post To Cash Account From", DataConnector.FieldTypeIdEnum);
            postToCashAccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });

            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", DataConnector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });

            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", DataConnector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });

            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });

            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free On Board", DataConnector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
        }

        private DataConnectorEntity GetVendorAddressEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListVendorAddresses, "Vendor addresses", ParentConnector);

            var pm00300 = entity.AddTable("PM00300");

            var pm00200 = entity.AddTable("PM00200", "PM00300", DataConnectorTable.DataConnectorTableJoinType.Inner);
            pm00200.AddJoinFields("VENDORID", "VENDORID");

            var sy01200 = entity.AddScript("select * from {0}..SY01200 with (NOLOCK) where Master_Type = 'VND'", "SY01200", "PM00300");
            sy01200.AddJoinFields("Master_ID", "VENDORID");
            sy01200.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddVendorAddressEntityFields(pm00300, pm00200, sy01200);
            
            return entity;
        }
        private static void AddVendorAddressEntityFields(DataConnectorTable pm00300, DataConnectorTable pm00200, DataConnectorTable sy01200)
        {
            pm00300.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            pm00200.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString, true);
            pm00300.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString, true);
            pm00300.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString, true);
            pm00300.AddField("CITY", "City", DataConnector.FieldTypeIdString, true);
            pm00300.AddField("STATE", "State", DataConnector.FieldTypeIdString, true);
            pm00300.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString, true);
            pm00300.AddField("PHNUMBR1", "Phone Number 1", DataConnector.FieldTypeIdPhone, true);
            pm00300.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor Check Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor Class ID", DataConnector.FieldTypeIdString);
            pm00300.AddField("VNDCNTCT", "Vendor Contact", DataConnector.FieldTypeIdString);
            pm00300.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            pm00300.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            pm00300.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            pm00300.AddField("PHNUMBR2", "Phone Number 2", DataConnector.FieldTypeIdPhone);
            pm00300.AddField("FAXNUMBR", "Fax Number", DataConnector.FieldTypeIdPhone);
            pm00300.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pm00300.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET1", "INet1", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET2", "INet2", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET3", "INet3", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET4", "INet4", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET5", "INet5", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET6", "INet6", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET7", "INet7", DataConnector.FieldTypeIdString);
            sy01200.AddField("INET8", "INet8", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor Address Code - Primary", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor Address Code - Purchase Address", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor Address Code - Ship From", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor Address Code - Remit To", DataConnector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum Order", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum Invoice Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment1", DataConnector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment2", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit Limit Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment Priority", DataConnector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject To PPS Deductions", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS Tax Rate", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction Exemption/Variation Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate Commencing Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate Expiration Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting Obligation Undertaken", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration Date Obligation", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed Payee", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts Payable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc Charges Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write Offs Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade Discount Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase Price Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received", DataConnector.FieldTypeIdYesNo);

            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor Status", DataConnector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 Type", DataConnector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var maximumInvoiceAmount = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", DataConnector.FieldTypeIdEnum);
            maximumInvoiceAmount.AddListItems(0, new List<string> { "No Maximum", "Amount" });
            
            var postToCashAcccountFrom = pm00200.AddField("PTCSHACF", "Post To Cash Account From", DataConnector.FieldTypeIdEnum);
            postToCashAcccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", DataConnector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", DataConnector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });
            
            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free On Board", DataConnector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
        }

        private DataConnectorEntity GetPurchaseOrderEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListPurchaseOrders, "Purchase orders", ParentConnector);

            var svPopTrx = entity.AddTable("svPOPTrx");

            var pm00200 = entity.AddTable("PM00200", "svPOPTrx");
            pm00200.AddJoinFields("VENDORID", "VENDORID");

            var pm00201 = entity.AddTable("PM00201", "svPOPTrx");
            pm00201.AddJoinFields("VENDORID", "VENDORID");
            
            var rm00101 = entity.AddTable("RM00101", "svPOPTrx");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            var rm00103 = entity.AddTable("RM00103", "svPOPTrx");
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            var pop10100 = entity.AddTable("POP10100", "svPOPTrx");
            pop10100.AddJoinFields("PONUMBER", "PONUMBER");

            AddPurchaseOrderEntityFields(svPopTrx, pm00200, pm00201, rm00101, rm00103, pop10100);

            return entity;
        }
        private static void AddPurchaseOrderEntityFields(DataConnectorTable svPopTrx, DataConnectorTable pm00200, DataConnectorTable pm00201, DataConnectorTable rm00101, DataConnectorTable rm00103, DataConnectorTable pop10100)
        {
            svPopTrx.AddField("PONUMBER", "PO Number", DataConnector.FieldTypeIdString, true);
            var poStatus = svPopTrx.AddField("POSTATUS", "PO Status", DataConnector.FieldTypeIdEnum, true);
            poStatus.AddListItems(1, new List<string> { "New", "Released", "Change Order", "Received", "Closed", "Canceled" });
            var poType = svPopTrx.AddField("POTYPE", "PO Type", DataConnector.FieldTypeIdEnum, true);
            poType.AddListItems(1, new List<string> { "Standard", "Drop-Ship", "Blanket", "Drop-Ship Blanket" });
            svPopTrx.AddField("DOCDATE", "Document Date", DataConnector.FieldTypeIdDate, true);
            svPopTrx.AddField("REMSUBTO", "Remaining Subtotal", DataConnector.FieldTypeIdCurrency, true);
            svPopTrx.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svPopTrx.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString, true);
            pop10100.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo, true);

            svPopTrx.AddField("USER2ENT", "User To Enter", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CONFIRM1", "Confirm With", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("LSTEDTDT", "Last Edit Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("LSTPRTDT", "Last Printed Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("PRMDATE", "Promised Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("PRMSHPDTE", "Promised Ship Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("REQDATE", "Required Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("REQTNDT", "Requisition Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("SUBTOTAL", "Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("CANCSUB", "Canceled Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("MSCCHAMT", "Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("MINORDER", "Minimum Order", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("VADCDPAD", "Vendor Address Code - Purchase Address", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CMPANYID", "Company ID", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PRSTADCD", "Primary Shipto Address Code", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CMPNYNAM", "Company Name", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            svPopTrx.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            svPopTrx.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            svPopTrx.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svPopTrx.AddField("DISAMTAV", "Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("TRDPCTPR", "Trade Discount Percent (Precise)", DataConnector.FieldTypeIdPercentage);
            svPopTrx.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("TIMESPRT", "Times Printed", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("COMMNTID", "Comment ID", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svPopTrx.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svPopTrx.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svPopTrx.AddField("OREMSUBT", "Originating Remaining Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORSUBTOT", "Originating Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("Originating_Canceled_Sub", "Originating Canceled Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("OMISCAMT", "Originating Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ODISAMTAV", "Originating Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("BUYERID", "Buyer ID", DataConnector.FieldTypeIdString);
            pop10100.AddField("ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("ORORDAMT", "Originating On Order Amount", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("ONHOLDDATE", "On Hold Date", DataConnector.FieldTypeIdDate);
            pop10100.AddField("ONHOLDBY", "On Hold By", DataConnector.FieldTypeIdString);
            pop10100.AddField("HOLDREMOVEDATE", "Hold Remove Date", DataConnector.FieldTypeIdDate);
            pop10100.AddField("HOLDREMOVEBY", "Hold Remove By", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("ALLOWSOCMTS", "Allow SO Commitments", DataConnector.FieldTypeIdYesNo);
            svPopTrx.AddField("PONOTIDS_1", "PO Note ID - PO", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_2", "PO Note ID - Site", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_3", "PO Note ID - Vendor", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_4", "PO Note ID - Comment", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_5", "PO Note ID - Payment Terms", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_6", "PO Note ID - Shipping Method", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("VENDNAME", "Vendor Name from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor Check Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor Address Code - Primary", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor Address Code - Purchase Address from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor Address Code - Ship From", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor Address Code - Remit To", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor Class ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor Contact", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS1", "Address 1 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS2", "Address 2 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("CITY", "City from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("STATE", "State from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ZIPCODE", "Zip Code from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR1", "Phone Number 1 from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("PHNUMBR2", "Phone Number 2 from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax Number from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping Method from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax Schedule ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration Number from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum Order from Vendor Master", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment Terms ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum Invoice Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment1", DataConnector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment2", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit Limit Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment Priority", DataConnector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject To PPS Deductions", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS Tax Rate", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction Exemption/Variation Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate Commencing Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate Expiration Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting Obligation Undertaken", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration Date Obligation", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed Payee", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts Payable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc Charges Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write Offs Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade Discount Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase Price Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date from Vendor Master", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date from Vendor Master", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of Invoices LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of Paid Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of Paid Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount Billed YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount Billed LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount Billed LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount Paid YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount Paid LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount Paid LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 Amount YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 Amount LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 Amount LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount Available YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount Available LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount Available LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount Lost YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount Lost LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount Lost LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance Charge LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance Charge LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance Charge YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade Discounts Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of Finance Charges LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of Finance Charges LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of Finance Charges YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage Owed", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last Check Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last Check Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last Check Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last Invoice Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last Invoice Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current Unapplied Payment Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid Finance Charges", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days Checks To Clear", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On Order Amount from Vendor Master", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount Grace Period from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date Grace Period from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3 from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("Withholding_LIFE", "Withholding LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WITHLYR", "Withholding LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WITHYTD", "Withholding YTD", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
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
            rm00101.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Dollar from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent from Customer Master", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User Defined 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User Defined 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
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
            rm00101.AddField("HOLD", "Hold from Customer Master", DataConnector.FieldTypeIdYesNo);
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
            rm00103.AddField("AVDTPLYR", "Average Days to Pay - LYR from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average Days To Pay - Life from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average Days To Pay - Year from Customer Master", DataConnector.FieldTypeIdInteger);
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
            rm00103.AddField("WROFSLIF", "Write Offs LIFE from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("Change_Order_Flag", "Change Order Flag", DataConnector.FieldTypeIdYesNo);
            svPopTrx.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            pop10100.AddField("PO_Field_Changes", "PO Field Changes", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("Revision_Number", "Revision Number", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("TAXSCHID", "Tax Schedule ID from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("BSIVCTTL", "Based On Invoice Total", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("BCKTXAMT", "Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("OBTAXAMT", "Originating Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("BackoutFreightTaxAmt", "Backout Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("OrigBackoutFreightTaxAmt", "Originating Backout Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("BackoutMiscTaxAmt", "Backout Misc Tax Amount", DataConnector.FieldTypeIdCurrency); 
            svPopTrx.AddField("OrigBackoutMiscTaxAmt", "Originating Backout Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("BackoutTradeDiscTax", "Backout Trade Discount Tax Amount", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("OrigBackoutTradeDiscTax", "Originating Backout Trade Discount Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPopTrx.AddField("POPCONTNUM", "Contract Number", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("CONTENDDTE", "Contract Expiration Date", DataConnector.FieldTypeIdDate);
            svPopTrx.AddField("CNTRLBLKTBY", "Control Blanket By", DataConnector.FieldTypeIdInteger);
            svPopTrx.AddField("PURCHCMPNYNAM", "Purchase Company Name", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHCONTACT", "Purchase Contact", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHADDRESS1", "Purchase Address 1", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHADDRESS2", "Purchase Address 2", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHADDRESS3", "Purchase Address 3", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHCITY", "Purchase City", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHSTATE", "Purchase State", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHZIPCODE", "Purchase Zip Code", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHCOUNTRY", "Purchase Country", DataConnector.FieldTypeIdString);
            svPopTrx.AddField("PURCHPHONE1", "Purchase Phone 1", DataConnector.FieldTypeIdPhone);
            svPopTrx.AddField("PURCHPHONE2", "Purchase Phone 2", DataConnector.FieldTypeIdPhone);
            svPopTrx.AddField("PURCHPHONE3", "Purchase Phone 3", DataConnector.FieldTypeIdPhone);
            svPopTrx.AddField("PURCHFAX", "Purchase Fax", DataConnector.FieldTypeIdPhone);
            svPopTrx.AddField("TXSCHSRC", "Tax Schedule Source", DataConnector.FieldTypeIdString);

            var documentStatus = svPopTrx.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var rateCalculationMethod = svPopTrx.AddField("RATECALC", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svPopTrx.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var statusGroup = svPopTrx.AddField("STATGRP", "Status Group", DataConnector.FieldTypeIdEnum);
            statusGroup.AddListItems(1, new List<string> { "Active", "Closed" });
            
            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor Status", DataConnector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 Type", DataConnector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var maximumInvoiceAmountForVendors = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", DataConnector.FieldTypeIdEnum);
            maximumInvoiceAmountForVendors.AddListItems(0, new List<string> { "No Maximum", "Amount" });
            
            var postToCashAccountFrom = pm00200.AddField("PTCSHACF", "Post To Cash Account From", DataConnector.FieldTypeIdEnum);
            postToCashAccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", DataConnector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", DataConnector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });
            
            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free On Board", DataConnector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit Limit Type", DataConnector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var minimumPaymentTypeFromCustomerMaster = rm00101.AddField("MINPYTYP", "Minimum Payment Type from Customer Master", DataConnector.FieldTypeIdEnum);
            minimumPaymentTypeFromCustomerMaster.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
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
            
            var postResultsToFromCustomerMaster = rm00101.AddField("Post_Results_To", "Post Results To from Customer Master", DataConnector.FieldTypeIdEnum);
            postResultsToFromCustomerMaster.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });
            
            var poStatusOrig = pop10100.AddField("PO_Status_Orig", "PO Status Orig", DataConnector.FieldTypeIdEnum);
            poStatusOrig.AddListItems(1, new List<string> { "New", "Released", "Change Order", "Received", "Closed", "Canceled" });
            
            var purchaseFreightTaxable = svPopTrx.AddField("Purchase_Freight_Taxable", "Purchase Freight Taxable", DataConnector.FieldTypeIdEnum);
            purchaseFreightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var purchaseMiscTaxable = svPopTrx.AddField("Purchase_Misc_Taxable", "Purchase Misc Taxable", DataConnector.FieldTypeIdEnum);
            purchaseMiscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

        }

        private DataConnectorEntity GetPurchaseOrderLineEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListPurchaseLineItems, "Purchase order line items", ParentConnector);

            var svPopLine = entity.AddTable("svPOPLine");

            var pm00200 = entity.AddTable("PM00200", "svPOPLine");
            pm00200.AddJoinFields("VENDORID", "VENDORID");

            var pm00201 = entity.AddTable("PM00201", "svPOPLine");
            pm00201.AddJoinFields("VENDORID", "VENDORID");

            var iv00101 = entity.AddTable("IV00101", "svPOPLine");
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");

            var rm00101 = entity.AddTable("RM00101", "svPOPLine");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var rm00103 = entity.AddTable("RM00103", "svPOPLine");
            rm00103.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            var pop10100 = entity.AddTable("POP10100", "svPOPLine");
            pop10100.AddJoinFields("PONUMBER", "PONUMBER");

            var pop10110 = entity.AddTable("POP10110", "svPOPLine");
            pop10110.AddJoinFields("PONUMBER", "PONUMBER");
            pop10110.AddJoinFields("ORD", "ORD");

            var pop10500 = entity.AddScript("select PONUMBER, POLNENUM, SUM(QTYSHPPD) as QTYSHPPD, SUM(QTYINVCD) as QTYINVCD, SUM(QTYREJ) as QTYREJ, SUM(QTYRESERVED) as QTYRESERVED, " +
                        "SUM(QTYINVRESERVE) as QTYINVRESERVE, SUM(QTYMATCH) as QTYMATCH, SUM(Total_Landed_Cost_Amount) as Total_Landed_Cost_Amount " +
                        "from {0}..POP10500 where Status = 1 group by PONUMBER, POLNENUM", "POP10500", "svPOPLine");
            pop10500.AddJoinFields("PONUMBER", "PONUMBER");
            pop10500.AddJoinFields("POLNENUM", "ORD");

            AddPurchaseOrderLineEntityFields(svPopLine, pm00200, pm00201, iv00101, rm00101, rm00103, pop10100, pop10110, pop10500);

            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100)", "Item Shipping Weight", DataConnector.FieldTypeIdQuantity);
            entity.AddCalculation("svPOPLine.DECPLCUR - 1", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("svPOPLine.DECPLQTY - 1", "Decimal Places QTYS", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        private static void AddPurchaseOrderLineEntityFields(DataConnectorTable svPopLine, DataConnectorTable pm00200, DataConnectorTable pm00201, DataConnectorTable iv00101, DataConnectorTable rm00101, DataConnectorTable rm00103, DataConnectorTable pop10100, DataConnectorTable pop10110, DataConnectorTable pop10500)
        {
            svPopLine.AddField("PONUMBER", "PO Number", DataConnector.FieldTypeIdString, true);
            var poLineStatus = svPopLine.AddField("POLNESTA", "PO Line Status", DataConnector.FieldTypeIdEnum, true);
            poLineStatus.AddListItems(1, new List<string> { "New", "Released", "Change Order", "Received", "Closed", "Canceled" });
            var poType = svPopLine.AddField("POTYPE", "PO Type", DataConnector.FieldTypeIdEnum, true);
            poType.AddListItems(1, new List<string> { "Standard", "Drop-Ship", "Blanket", "Drop-Ship Blanket" });
            svPopLine.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svPopLine.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            svPopLine.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svPopLine.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svPopLine.AddField("QTYORDER", "QTY Ordered", DataConnector.FieldTypeIdQuantity, true);
            svPopLine.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency, true);
            svPopLine.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency, true);

            svPopLine.AddField("ORD", "Ord", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("VNDITNUM", "Vendor Item Number", DataConnector.FieldTypeIdString);
            svPopLine.AddField("VNDITDSC", "Vendor Item Description", DataConnector.FieldTypeIdString);
            svPopLine.AddField("NONINVEN", "Non IV", DataConnector.FieldTypeIdYesNo);
            svPopLine.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svPopLine.AddField("UMQTYINB", "U Of M QTY In Base", DataConnector.FieldTypeIdQuantity);
            svPopLine.AddField("QTYCANCE", "QTY Canceled", DataConnector.FieldTypeIdQuantity);
            pop10110.AddField("QTYCMTBASE", "QTY Committed In Base", DataConnector.FieldTypeIdQuantity);
            pop10110.AddField("QTYUNCMTBASE", "QTY Uncommitted In Base", DataConnector.FieldTypeIdQuantity);
            svPopLine.AddField("INVINDX", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svPopLine.AddField("REQDATE", "Required Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("PRMDATE", "Promised Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("PRMSHPDTE", "Promised Ship Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("REQSTDBY", "Requested By", DataConnector.FieldTypeIdString);
            svPopLine.AddField("COMMNTID", "Comment ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("DOCTYPE", "Document Type", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("BRKFLD1", "Break Field 1", DataConnector.FieldTypeIdInteger);
            pop10110.AddField("QTY_Canceled_Orig", "QTY Canceled Orig", DataConnector.FieldTypeIdQuantity);
            pop10110.AddField("OPOSTSUB", "Originating Posted Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("JOBNUMBR", "Job Number", DataConnector.FieldTypeIdString);
            svPopLine.AddField("COSTCODE", "Cost Code", DataConnector.FieldTypeIdString);
            svPopLine.AddField("COSTTYPE", "Cost Code Type", DataConnector.FieldTypeIdInteger);
            pop10110.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            pop10110.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            pop10110.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svPopLine.AddField("ORUNTCST", "Originating Unit Cost", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("LINEORIGIN", "Line Origin", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("ODECPLCU - 1", "Originating Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            pop10110.AddField("Capital_Item", "Capital Item", DataConnector.FieldTypeIdYesNo);
            svPopLine.AddField("Product_Indicator", "Product Indicator", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("Source_Document_Number", "Source Document Number", DataConnector.FieldTypeIdString);
            svPopLine.AddField("Source_Document_Line_Num", "Source Document Line Number", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_1", "Line Note 1", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_2", "Line Note 2", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_3", "Line Note 3", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_4", "Line Note 4", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_5", "Line Note 5", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("USER2ENT", "User To Enter", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CONFIRM1", "Confirm With", DataConnector.FieldTypeIdString);
            svPopLine.AddField("DOCDATE", "Document Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("LSTEDTDT", "Last Edit Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("LSTPRTDT", "Last Printed Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("PRMDATE", "Promised Date from Purchase Transactions", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("PRMSHPDTE", "Promised Ship Date from Purchase Transactions", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("REQDATE", "Required Date from Purchase Transactions", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("REQTNDT", "Requisition Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("SHIPMTHD", "Shipping Method from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            svPopLine.AddField("REMSUBTO", "Remaining Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("SUBTOTAL", "Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("CANCSUB", "Canceled Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("MSCCHAMT", "Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("TAXAMNT", "Tax Amount from Purchase Transactions", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString);
            svPopLine.AddField("MINORDER", "Minimum Order", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("VADCDPAD", "Vendor Address Code - Purchase Address", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CMPANYID", "Company ID", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            svPopLine.AddField("PRSTADCD", "Primary Shipto Address Code", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CMPNYNAM", "Company Name from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CONTACT", "Contact from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS1", "Address 1 from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS2", "Address 2 from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS3", "Address 3 from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CITY", "City from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("STATE", "State from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ZIPCODE", "Zip Code from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("COUNTRY", "Country from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("PHONE1", "Phone 1 from Purchase Transactions", DataConnector.FieldTypeIdPhone);
            svPopLine.AddField("PHONE2", "Phone 2 from Purchase Transactions", DataConnector.FieldTypeIdPhone);
            svPopLine.AddField("FAX", "Fax from Purchase Transactions", DataConnector.FieldTypeIdPhone);
            svPopLine.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svPopLine.AddField("DISAMTAV", "Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("TRDPCTPR", "Trade Discount Percent (Precise)", DataConnector.FieldTypeIdPercentage);
            svPopLine.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString);
            svPopLine.AddField("TIMESPRT", "Times Printed", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("COMMNTID", "Comment ID from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CURNCYID", "Currency ID from Purchase Transactions", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CURRNIDX", "Currency Index from Purchase Transactions", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("XCHGRATE", "Exchange Rate from Purchase Transactions", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svPopLine.AddField("DENXRATE", "Denomination Exchange Rate from Purchase Transactions", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("OREMSUBT", "Originating Remaining Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("ORSUBTOT", "Originating Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("Originating_Canceled_Sub", "Originating Canceled Subtotal", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("OMISCAMT", "Originating Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("ORTAXAMT", "Originating Tax Amount from Purchase Transactions", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("ODISAMTAV", "Originating Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("BUYERID", "Buyer ID", DataConnector.FieldTypeIdString);
            pop10100.AddField("ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("ORORDAMT", "Originating On Order Amount", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            pop10100.AddField("ONHOLDDATE", "On Hold Date", DataConnector.FieldTypeIdDate);
            pop10100.AddField("ONHOLDBY", "On Hold By", DataConnector.FieldTypeIdString);
            pop10100.AddField("HOLDREMOVEDATE", "Hold Remove Date", DataConnector.FieldTypeIdDate);
            pop10100.AddField("HOLDREMOVEBY", "Hold Remove By", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ALLOWSOCMTS", "Allow SO Commitments", DataConnector.FieldTypeIdYesNo);
            svPopLine.AddField("PONOTIDS_1", "PO Note ID - PO", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_2", "PO Note ID - Site", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_3", "PO Note ID - Vendor", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_4", "PO Note ID - Comment", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_5", "PO Note ID - Payment Terms", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_6", "PO Note ID - Shipping Method", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("VENDNAME", "Vendor Name from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor Check Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor Address Code - Primary", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor Address Code - Purchase Address from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor Address Code - Ship From", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor Address Code - Remit To", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor Class ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor Contact", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS1", "Address 1 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS2", "Address 2 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("CITY", "City from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("STATE", "State from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ZIPCODE", "Zip Code from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR1", "Phone Number 1 from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("PHNUMBR2", "Phone Number 2 from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax Number from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping Method from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax Schedule ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration Number from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum Order from Vendor Master", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment Terms ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum Invoice Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment1 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment2 from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit Limit Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment Priority", DataConnector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep Calendar History from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep Period History from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep Trx History from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject To PPS Deductions", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS Tax Rate", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction Exemption/Variation Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate Commencing Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate Expiration Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting Obligation Undertaken", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration Date Obligation", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed Payee", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts Payable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc Charges Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write Offs Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade Discount Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase Price Variance Account Number from Vendor Master", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date from Vendor Master", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date from Vendor Master", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of Invoices LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of Paid Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of Paid Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount Billed YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount Billed LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount Billed LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount Paid YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount Paid LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount Paid LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 Amount YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 Amount LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 Amount LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount Available YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount Available LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount Available LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount Lost YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount Lost LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount Lost LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance Charge LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance Charge LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance Charge YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade Discounts Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of Finance Charges LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of Finance Charges LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of Finance Charges YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage Owed", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last Check Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last Check Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last Check Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last Invoice Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last Invoice Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current Unapplied Payment Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid Finance Charges", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days Checks To Clear", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On Order Amount from Vendor Master", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount Grace Period from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date Grace Period from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3 from Vendor Master", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received", DataConnector.FieldTypeIdYesNo);
            pm00201.AddField("Withholding_LIFE", "Withholding LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WITHLYR", "Withholding LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WITHYTD", "Withholding YTD", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer Class", DataConnector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
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
            rm00101.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit Period", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Dollar from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent from Customer Master", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", DataConnector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Dollar", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff Amount", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User Defined 1 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User Defined 2 from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
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
            rm00101.AddField("HOLD", "Hold from Customer Master", DataConnector.FieldTypeIdYesNo);
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
            rm00103.AddField("AVDTPLYR", "Average Days to Pay - LYR from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average Days To Pay - Life from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average Days To Pay - Year from Customer Master", DataConnector.FieldTypeIdInteger);
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
            rm00103.AddField("WROFSLIF", "Write Offs LIFE from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement Date", DataConnector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement Amount", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount from Customer Master", DataConnector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", DataConnector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID from Customer Master", DataConnector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period from Customer Master", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from Customer Master", DataConnector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", DataConnector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", DataConnector.FieldTypeIdCurrency);
            pop10100.AddField("Change_Order_Flag", "Change Order Flag from Purchase Transactions", DataConnector.FieldTypeIdYesNo);
            svPopLine.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("PHONE3", "Phone 3 from Purchase Transactions", DataConnector.FieldTypeIdPhone);
            pop10100.AddField("PO_Field_Changes", "PO Field Changes", DataConnector.FieldTypeIdString);
            svPopLine.AddField("Revision_Number", "Revision Number", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            iv00101.AddField("ITEMDESC", "Item Description from Item Master", DataConnector.FieldTypeIdString);
            iv00101.AddField("NOTEINDX", "Note Index from Item Master", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("ITMSHNAM", "Item Short Name", DataConnector.FieldTypeIdString);
            iv00101.AddField("ITMGEDSC", "Item Generic Description", DataConnector.FieldTypeIdString);
            iv00101.AddField("STNDCOST", "Standard Cost", DataConnector.FieldTypeIdCurrency);
            iv00101.AddField("CURRCOST", "Current Cost", DataConnector.FieldTypeIdCurrency);
            iv00101.AddField("ITMTSHID", "Item Tax Schedule ID", DataConnector.FieldTypeIdString);
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
            iv00101.AddField("ITEMCODE", "Item Code", DataConnector.FieldTypeIdString);
            iv00101.AddField("LASTGENSN", "Last Generated Serial Number", DataConnector.FieldTypeIdString);
            iv00101.AddField("PriceGroup", "Price Group", DataConnector.FieldTypeIdString);
            iv00101.AddField("PINFLIDX", "Purch Inflation Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURMCIDX", "Purch Monetary Correction Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PRCHSUOM", "Purchasing U Of M", DataConnector.FieldTypeIdString);
            iv00101.AddField("SELNGUOM", "Selling U Of M", DataConnector.FieldTypeIdString);
            iv00101.AddField("TCC", "Tax Commodity Code", DataConnector.FieldTypeIdString);
            iv00101.AddField("UPPVIDX", "Unrealized Purchase Price Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("LOCNCODE", "Location Code from Item Master", DataConnector.FieldTypeIdString);
            pop10110.AddField("Change_Order_Flag", "Change Order Flag", DataConnector.FieldTypeIdYesNo);
            svPopLine.AddField("RELEASEBYDATE", "Release By Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("Released_Date", "Released Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("Purchase_Item_Tax_Schedu", "Purchase Item Tax Schedule ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("Purchase_Site_Tax_Schedu", "Purchase Site Tax Schedule ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("BSIVCTTL", "Based On Invoice Total", DataConnector.FieldTypeIdYesNo);
            svPopLine.AddField("TAXAMNT", "Tax Amount from Purchase Line Item", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("ORTAXAMT", "Originating Tax Amount from Purchase Line Item", DataConnector.FieldTypeIdCurrency);
            svPopLine.AddField("Landed_Cost_Group_ID", "Landed Cost Group ID", DataConnector.FieldTypeIdString);
            svPopLine.AddField("PLNNDSPPLID", "Planned Supply ID", DataConnector.FieldTypeIdInteger);
            svPopLine.AddField("SHIPMTHD", "Shipping Method from Purchase Line Item", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ORIGPRMDATE", "Original Promised Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("FSTRCPTDT", "First Receipt Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("LSTRCPTDT", "Last Receipt Date", DataConnector.FieldTypeIdDate);
            svPopLine.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CMPNYNAM", "Company Name", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svPopLine.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svPopLine.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svPopLine.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            svPopLine.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            svPopLine.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            svPopLine.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            svPopLine.AddField("ADDRSOURCE", "Address Source", DataConnector.FieldTypeIdString);
            svPopLine.AddField("PURCHSITETXSCHSRC", "Purchase Site Tax Schedule Source", DataConnector.FieldTypeIdString);
            svPopLine.AddField("ProjNum", "Project Number", DataConnector.FieldTypeIdString);
            svPopLine.AddField("CostCatID", "Cost Category ID", DataConnector.FieldTypeIdString);
            pop10500.AddField("QTYINVCD", "QTY Invoiced", DataConnector.FieldTypeIdQuantity);
            pop10500.AddField("QTYINVRESERVE", "QTY Invoice Reserve", DataConnector.FieldTypeIdQuantity);
            pop10500.AddField("QTYREJ", "QTY Rejected", DataConnector.FieldTypeIdQuantity);
            pop10500.AddField("QTYRESERVED", "QTY Reserved", DataConnector.FieldTypeIdQuantity);
            pop10500.AddField("QTYMATCH", "QTY Matched", DataConnector.FieldTypeIdQuantity);
            pop10500.AddField("QTYSHPPD", "QTY Shipped", DataConnector.FieldTypeIdQuantity);
            pop10500.AddField("Total_Landed_Cost_Amount", "Total Landed Cost Amount", DataConnector.FieldTypeIdCurrency);

            var itemTrackingOption = pop10110.AddField("ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
            
            var poLineStatusOrig = pop10110.AddField("PO_Line_Status_Orig", "PO Line Status Orig", DataConnector.FieldTypeIdEnum);
            poLineStatusOrig.AddListItems(1, new List<string> { "New", "Released", "Change Order", "Received", "Closed", "Canceled" });
            
            var rateCalculationMethod = pop10110.AddField("RATECALC", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var freeOnBoard = svPopLine.AddField("FREEONBOARD", "Free On Board", DataConnector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
            
            var documentStatus = svPopLine.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var poStatus = svPopLine.AddField("POSTATUS", "PO Status", DataConnector.FieldTypeIdEnum);
            poStatus.AddListItems(1, new List<string> { "New", "Released", "Change Order", "Received", "Closed", "Canceled" });

            var rateCalculationMethodFromPurchaseTransactions = svPopLine.AddField("RATECALC", "Rate Calculation Method from Purchase Transactions", DataConnector.FieldTypeIdEnum);
            rateCalculationMethodFromPurchaseTransactions.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svPopLine.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var statusGroup = svPopLine.AddField("STATGRP", "Status Group", DataConnector.FieldTypeIdEnum);
            statusGroup.AddListItems(1, new List<string> { "Active", "Closed" });
            
            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor Status", DataConnector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 Type", DataConnector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var maximumInvoiceAmountForVendors = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", DataConnector.FieldTypeIdEnum);
            maximumInvoiceAmountForVendors.AddListItems(0, new List<string> { "No Maximum", "Amount" });
            
            var postToCashAccountFrom = pm00200.AddField("PTCSHACF", "Post To Cash Account From", DataConnector.FieldTypeIdEnum);
            postToCashAccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", DataConnector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", DataConnector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });
            
            var freeOnBoardFromVendorMaster = pm00200.AddField("FREEONBOARD", "Free On Board from Vendor Master", DataConnector.FieldTypeIdEnum);
            freeOnBoardFromVendorMaster.AddListItems(1, new List<string> { "None", "Origin", "Destination" });

            var creditLimitTypeFromCustomerMaster = rm00101.AddField("CRLMTTYP", "Credit Limit Type from Customer Master", DataConnector.FieldTypeIdEnum);
            creditLimitTypeFromCustomerMaster.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var minimumPaymentTypeFromCustomerMaster = rm00101.AddField("MINPYTYP", "Minimum Payment Type from Customer Master", DataConnector.FieldTypeIdEnum);
            minimumPaymentTypeFromCustomerMaster.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
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
            
            var postResultsToFromCustomerMaster = rm00101.AddField("Post_Results_To", "Post Results To from Customer Master", DataConnector.FieldTypeIdEnum);
            postResultsToFromCustomerMaster.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });
            
            var poStatusOrig = pop10100.AddField("PO_Status_Orig", "PO Status Orig", DataConnector.FieldTypeIdEnum);
            poStatusOrig.AddListItems(1, new List<string> { "New", "Released", "Change Order", "Received", "Closed", "Canceled" });
            
            var itemType = iv00101.AddField("ITEMTYPE", "Item Type", DataConnector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales Inventory", "Discontinued", "Kit", "Misc Charges", "Services", "Flat Fee" });
            
            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax Options", DataConnector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var itemTrackingOptionFromItemMaster = iv00101.AddField("ITMTRKOP", "Item Tracking Option from Item Master", DataConnector.FieldTypeIdEnum);
            itemTrackingOptionFromItemMaster.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
            
            var valuationMethodFromItemMaster = iv00101.AddField("VCTNMTHD", "Valuation Method from Item Master", DataConnector.FieldTypeIdEnum);
            valuationMethodFromItemMaster.AddListItems(1, new List<string> { "FIFO Perpetual", "LIFO Perpetual", "Average Perpetual", "FIFO Periodic", "LIFO Periodic" });
            
            var abcCode = iv00101.AddField("ABCCODE", "ABC Code", DataConnector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });
            
            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS Account Source", DataConnector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From Component Item", "From Kit Item" });
            
            var priceMethod = iv00101.AddField("PRICMTHDw", "Price Method", DataConnector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency Amount", "% of List Price", "% Markup - Current Cost", "% Markup - Standard Cost", "% Margin - Current Cost", "% Margin - Standard Cost" });
            
            var valuationMethod = pop10110.AddField("VCTNMTHD", "Valuation Method", DataConnector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO Perpetual", "LIFO Perpetual", "Average Perpetual", "FIFO Periodic", "LIFO Periodic" });
            
            var purchaseIvItemTaxable = svPopLine.AddField("Purchase_IV_Item_Taxable", "Purchase IV Item Taxable", DataConnector.FieldTypeIdEnum);
            purchaseIvItemTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

        }

        private DataConnectorEntity GetPayablesTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListPayablesTrx, "Payables transactions", ParentConnector);

            var svPmTrx = entity.AddTable("svPMTrx");

            var pm00200 = entity.AddTable("PM00200", "svPMTrx");
            pm00200.AddJoinFields("VENDORID", "VENDORID");

            var pm00201 = entity.AddTable("PM00201", "svPMTrx");
            pm00201.AddJoinFields("VENDORID", "VENDORID");

            var mc020103 = entity.AddTable("MC020103", "svPMTrx");
            mc020103.AddJoinFields("VCHRNMBR", "VCHRNMBR");
            mc020103.AddJoinFields("DOCTYPE", "DOCTYPE");

            var pm10000 = entity.AddTable("PM10000", "svPMTrx");
            pm10000.AddJoinFields("VCHRNMBR", "VCHRNMBR");
            pm10000.AddJoinFields("DOCTYPE", "DOCTYPE");

            var pm30200 = entity.AddTable("PM30200", "svPMTrx");
            pm30200.AddJoinFields("VCHRNMBR", "VCHRNMBR");
            pm30200.AddJoinFields("DOCTYPE", "DOCTYPE");

            AddPayablesTransactionEntityFields(svPmTrx, pm00200, pm00201, mc020103, pm10000, pm30200);

            return entity;
        }
        private static void AddPayablesTransactionEntityFields(DataConnectorTable svPmTrx, DataConnectorTable pm00200, DataConnectorTable pm00201, DataConnectorTable mc020103, DataConnectorTable pm10000, DataConnectorTable pm30200)
        {
            svPmTrx.AddField("VCHRNMBR", "Voucher Number", DataConnector.FieldTypeIdString, true);
            svPmTrx.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            var documentType = svPmTrx.AddField("DOCTYPE", "Document Type", DataConnector.FieldTypeIdEnum, true);
            documentType.AddListItems(1, new List<string> { "Invoice", "Finance Charge", "Misc Charge", "Return", "Credit Memo", "Payment" });
            svPmTrx.AddField("DOCDATE", "Document Date", DataConnector.FieldTypeIdDate, true);
            svPmTrx.AddField("DOCNUMBR", "Document Number", DataConnector.FieldTypeIdString, true);
            svPmTrx.AddField("CURTRXAM", "Current Trx Amount", DataConnector.FieldTypeIdCurrency, true);

            svPmTrx.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("DISCAMNT", "Discount Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            svPmTrx.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            svPmTrx.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("TEN99AMNT", "1099 Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("WROFAMNT", "Write Off Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("DISAMTAV", "Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("TRXDSCRN", "Transaction Description", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("UN1099AM", "Unapplied 1099 Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("BKTPURAM", "Backout Purchases Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("BKTFRTAM", "Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("BKTMSCAM", "Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("VOIDED", "Voided", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("HOLD", "Hold", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("DINVPDOF", "Date Invoice Paid Off", DataConnector.FieldTypeIdDate);
            svPmTrx.AddField("PPSAMDED", "PPS Amount Deducted", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("PPSTAXRT", "PPS Tax Rate", DataConnector.FieldTypeIdQuantity);
            svPmTrx.AddField("PGRAMSBJ", "Percent Of Gross Amount Subject", DataConnector.FieldTypeIdPercentage);
            svPmTrx.AddField("GSTDSAMT", "GST Discount Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            svPmTrx.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            svPmTrx.AddField("MDFUSRID", "Modified User ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("CARDNAME", "Card Name", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("PRCHAMNT", "Purchases Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("MSCCHAMT", "Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("TTLPYMTS", "Total Payments", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("PCHSCHID", "Purchase Schedule ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("PSTGDATE", "Posting Date", DataConnector.FieldTypeIdDate);
            svPmTrx.AddField("DISAVTKN", "Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svPmTrx.AddField("PRCTDISC", "Percent Discount", DataConnector.FieldTypeIdPercentage);
            svPmTrx.AddField("RETNAGAM", "Retainage Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("SOURCDOC", "Source Document", DataConnector.FieldTypeIdString);
            pm10000.AddField("VADDCDPR", "Vendor Address Code - Primary", DataConnector.FieldTypeIdString);
            pm10000.AddField("CHRGAMNT", "Charge Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("CASHAMNT", "Cash Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("CAMCBKID", "Cash Amount Checkbook ID", DataConnector.FieldTypeIdString);
            pm10000.AddField("CDOCNMBR", "Cash Document Number", DataConnector.FieldTypeIdString);
            pm10000.AddField("CAMTDATE", "Cash Amount Date", DataConnector.FieldTypeIdDate);
            pm10000.AddField("CAMPMTNM", "Cash Amount Payment Number", DataConnector.FieldTypeIdString);
            pm10000.AddField("CHEKAMNT", "Check Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("CHAMCBID", "Check Amount Checkbook ID", DataConnector.FieldTypeIdString);
            pm10000.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            pm10000.AddField("CAMPYNBR", "Check Amount Payment Number", DataConnector.FieldTypeIdString);
            pm10000.AddField("CRCRDAMT", "Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("CCAMPYNM", "Credit Card Amount Payment Number", DataConnector.FieldTypeIdString);
            pm10000.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString);
            pm10000.AddField("CCRCTNUM", "Credit Card Receipt Number", DataConnector.FieldTypeIdString);
            pm10000.AddField("CRCARDDT", "Credit Card Date", DataConnector.FieldTypeIdDate);
            pm10000.AddField("APDSTKAM", "Applied Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("TXENGCLD", "Tax Engine Called", DataConnector.FieldTypeIdYesNo);
            pm10000.AddField("PMWRKMSG", "PM WORK Messages", DataConnector.FieldTypeIdString);
            pm10000.AddField("PMDSTMSG", "PM Distribution Messages", DataConnector.FieldTypeIdString);
            pm10000.AddField("POSTED", "Posted", DataConnector.FieldTypeIdYesNo);
            pm10000.AddField("APPLDAMT", "Applied Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("PRINTED", "Printed", DataConnector.FieldTypeIdYesNo);
            pm10000.AddField("ICTRX", "IC TRX", DataConnector.FieldTypeIdYesNo);
            pm10000.AddField("ICDISTS", "ICDists", DataConnector.FieldTypeIdYesNo);
            pm10000.AddField("PMICMSGS", "ICMessages", DataConnector.FieldTypeIdString);
            pm10000.AddField("Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            pm30200.AddField("VOIDPDATE", "Void GL Posting Date", DataConnector.FieldTypeIdDate);
            mc020103.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            mc020103.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            mc020103.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            mc020103.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            mc020103.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            mc020103.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            mc020103.AddField("ORCTRXAM", "Originating Current Trx Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("OPURAMT", "Originating Purchases Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("OMISCAMT", "Originating Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORCASAMT", "Originating Cash Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORCHKAMT", "Originating Check Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORCHKTTL", "Originating Check Total", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORCCDAMT", "Originating Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORAPPAMT", "Originating Applied Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORDISTKN", "Originating Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORDATKN", "Originating Discount Available Taken", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORWROFAM", "Originating Write Off Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("OBKPURAMT", "Originating Backout Purchases Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORBKTFRT", "Originating Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORBKTMSC", "Originating Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("UNGANLOS", "Unrealized Gain-Loss Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("RMMCERRS", "RM MC Posting Error Messages", DataConnector.FieldTypeIdString);
            mc020103.AddField("OCHGAMT", "Originating Charge Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ODISAMTAV", "Originating Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("ORGAPDISCTKN", "Originating Applied Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("OTOTPAY", "Originating Total Payments", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("OR1099AM", "Originating 1099 Amount", DataConnector.FieldTypeIdCurrency);
            mc020103.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            mc020103.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor Check Name from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor Address Code - Primary from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor Address Code - Purchase Address", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor Address Code - Ship From", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor Address Code - Remit To from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor Class ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor Contact", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            pm00200.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            pm00200.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            pm00200.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR1", "Phone Number 1", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("PHNUMBR2", "Phone Number 2", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax Number", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS Zone", DataConnector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping Method from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax Schedule ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", DataConnector.FieldTypeIdPercentage);
            pm00200.AddField("MINORDER", "Minimum Order", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment Terms ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum Payment Percent", DataConnector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum Payment Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum Invoice Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment1", DataConnector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment2", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit Limit Dollar", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment Priority", DataConnector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject To PPS Deductions", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS Tax Rate from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction Exemption/Variation Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate Commencing Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate Expiration Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting Obligation Undertaken", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration Date Obligation", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed Payee", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts Payable Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount Available Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount Taken Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance Charge Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc Charges Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write Offs Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade Discount Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued Purchases Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase Price Variance Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index from Vendor Master", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date from Vendor Master", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date from Vendor Master", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID from Vendor Master", DataConnector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of Invoices LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of Paid Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of Paid Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount Billed YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount Billed LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount Billed LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount Paid YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount Paid LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount Paid LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 Amount YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 Amount LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 Amount LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount Available YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount Available LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount Available LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount Lost YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount Lost LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount Lost LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance Charge LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance Charge LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance Charge YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade Discounts Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of Finance Charges LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of Finance Charges LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of Finance Charges YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage Owed", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last Check Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last Check Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last Check Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last Invoice Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last Invoice Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current Unapplied Payment Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid Finance Charges", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days Checks To Clear", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On Order Amount", DataConnector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00201.AddField("Withholding_LIFE", "Withholding LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WITHLYR", "Withholding LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WITHYTD", "Withholding YTD", DataConnector.FieldTypeIdCurrency);
            svPmTrx.AddField("APLYWITH", "Apply Withholding", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("CORRCTN", "Correction", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("DocPrinted", "DocPrinted", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("ECTRX", "EC Transaction", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("Electronic", "Electronic", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("PRCHDATE", "Purchase Date", DataConnector.FieldTypeIdDate);
            svPmTrx.AddField("SIMPLIFD", "Simplified", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("TaxInvReqd", "Tax Invoice Required", DataConnector.FieldTypeIdYesNo);
            svPmTrx.AddField("VNDCHKNM", "Vendor Check Name", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("BNKRCAMT", "Bank Receipts Amount", DataConnector.FieldTypeIdCurrency);
            pm10000.AddField("CORRNXST", "Correction to Nonexisting Transaction", DataConnector.FieldTypeIdYesNo);
            pm10000.AddField("PMWRKMS2", "PM WORK Messages 2", DataConnector.FieldTypeIdString);
            pm10000.AddField("VCHRNCOR", "Voucher Number Corrected", DataConnector.FieldTypeIdString);
            svPmTrx.AddField("VADCDTRO", "Vendor Address Code - Remit To", DataConnector.FieldTypeIdString);

            var documentStatus = svPmTrx.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "Posted", "History" });
            
            var paymentEntryType = pm30200.AddField("PYENTTYP", "Payment Entry Type", DataConnector.FieldTypeIdEnum);
            paymentEntryType.AddListItems(1, new List<string> { "Check", "Cash", "Credit Card" });
            
            var controlType = svPmTrx.AddField("CNTRLTYP", "Control Type", DataConnector.FieldTypeIdEnum);
            controlType.AddListItems(1, new List<string> { "Voucher", "Payment" });
            
            var rateCalculationMethod = mc020103.AddField("RTCLCMTD", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor Status", DataConnector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 Type", DataConnector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });

            var minimumInvoiceAmountForVendors = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", DataConnector.FieldTypeIdEnum);
            minimumInvoiceAmountForVendors.AddListItems(0, new List<string> { "No Maximum", "Amount" });
            
            var postToCashAccountFor = pm00200.AddField("PTCSHACF", "Post To Cash Account From", DataConnector.FieldTypeIdEnum);
            postToCashAccountFor.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", DataConnector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", DataConnector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", DataConnector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });
            
            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free On Board", DataConnector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });

        }

        private DataConnectorEntity GetReceivingsTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListReceivingsTrx, "Receivings transactions", ParentConnector);

            var svReceivingTrx = entity.AddTable("svReceivingTrx");

            var pm00201 = entity.AddTable("PM00201");
            pm00201.AddJoinFields("VENDORID", "VENDORID");

            var pop30300 = entity.AddTable("POP30300");
            pop30300.AddJoinFields("POPRCTNM", "POPRCTNM");
            
            var pop10306 = entity.AddTable("POP10306");
            pop10306.AddJoinFields("POPRCTNM", "POPRCTNM");

            AddReceivingsTransactionEntityFields(svReceivingTrx, pm00201, pop30300, pop10306);

            return entity;
        }
        private static void AddReceivingsTransactionEntityFields(DataConnectorTable svReceivingTrx, DataConnectorTable pm00201, DataConnectorTable pop30300, DataConnectorTable pop10306)
        {
            svReceivingTrx.AddField("POPRCTNM", "POP Receipt Number", DataConnector.FieldTypeIdString, true);
            var popType = svReceivingTrx.AddField("POPTYPE", "POP Type", DataConnector.FieldTypeIdEnum, true);
            popType.AddListItems(1, new List<string> { "Standard", "Drop-Ship", "Blanket", "Drop-Ship Blanket" });
            svReceivingTrx.AddField("receiptdate", "Receipt Date", DataConnector.FieldTypeIdDate, true);
            svReceivingTrx.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svReceivingTrx.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString, true);
            svReceivingTrx.AddField("SUBTOTAL", "Subtotal", DataConnector.FieldTypeIdCurrency, true);

            svReceivingTrx.AddField("VNDDOCNM", "Vendor Document Number", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("GLPOSTDT", "GL Posting Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("ACTLSHIP", "Actual Ship Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TRDPCTPR", "Trade Discount Percent (Precise", DataConnector.FieldTypeIdPercentage);
            svReceivingTrx.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("MISCAMNT", "Misc Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TEN99AMNT", "1099 Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svReceivingTrx.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISAVAMT", "Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("POPHDR1", "POP HDR Errors 1", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("POPHDR2", "POP HDR Errors 2", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("POPLNERR", "POP LINE Errors", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("USER2ENT", "User To Enter", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("VCHRNMBR", "Voucher Number", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svReceivingTrx.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svReceivingTrx.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svReceivingTrx.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svReceivingTrx.AddField("ORSUBTOT", "Originating Subtotal", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORMISCAMT", "Originating Misc Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OR1099AM", "Originating 1099 Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDAVAMT", "Originating Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("WITHHAMT", "Withholding Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("SIMPLIFD", "Simplified", DataConnector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("ECTRX", "EC Transaction", DataConnector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("BSIVCTTL", "Based On Invoice Total", DataConnector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            svReceivingTrx.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISCFRGT", "Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDAVFRT", "Originating Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISCMISC", "Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDAVMSC", "Originating Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("BackoutFreightTaxAmt", "Backout Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OrigBackoutFreightTaxAmt", "Originating Backout Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("BackoutMiscTaxAmt", "Backout Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OrigBackoutMiscTaxAmt", "Originating Backout Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("BCKTXAMT", "Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OBTAXAMT", "Originating Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TaxInvReqd", "Tax Invoice Required", DataConnector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("TaxInvRecvd", "Tax Invoice Received", DataConnector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("APLYWITH", "Apply Withholding", DataConnector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("PPSTAXRT", "PPS Tax Rate", DataConnector.FieldTypeIdInteger);
            svReceivingTrx.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of Invoices LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of Paid Invoices YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of Paid Invoices LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount Billed YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount Billed LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount Billed LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount Paid YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount Paid LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount Paid LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 Amount YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 Amount LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 Amount LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount Available YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount Available LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount Available LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount Lost YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount Lost LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount Lost LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance Charge LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance Charge LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance Charge YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write Offs YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write Offs LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write Offs LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade Discounts Taken LIFE", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade Discounts Taken LYR", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade Discounts Taken YTD", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of Finance Charges LIFE", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of Finance Charges LYR", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of Finance Charges YTD", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage Owed", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last Check Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last Check Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last Check Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last Invoice Number", DataConnector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last Invoice Amount", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First Purchase Date", DataConnector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current Unapplied Payment Balance", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid Finance Charges", DataConnector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days Checks To Clear", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average Days To Pay - Year", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average Days To Pay - Life", DataConnector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On Order Amount from Vendor Master", DataConnector.FieldTypeIdCurrency);
            pop10306.AddField("User_Defined_List01", "User Defined List01", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List02", "User Defined List02", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List03", "User Defined List03", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List04", "User Defined List04", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List05", "User Defined List05", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text01", "User Defined Text01", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text02", "User Defined Text02", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text03", "User Defined Text03", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text04", "User Defined Text04", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text05", "User Defined Text05", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text06", "User Defined Text06", DataConnector.FieldTypeIdString); 
            pop10306.AddField("User_Defined_Text07", "User Defined Text07", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text08", "User Defined Text08", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text09", "User Defined Text09", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text10", "User Defined Text10", DataConnector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Date01", "User Defined Date01", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date02", "User Defined Date02", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date03", "User Defined Date03", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date04", "User Defined Date04", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date05", "User Defined Date05", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date06", "User Defined Date06", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date07", "User Defined Date07", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date08", "User Defined Date08", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date09", "User Defined Date09", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date10", "User Defined Date10", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date11", "User Defined Date11", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date12", "User Defined Date12", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date13", "User Defined Date13", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date14", "User Defined Date14", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date15", "User Defined Date15", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date16", "User Defined Date16", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date17", "User Defined Date17", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date18", "User Defined Date18", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date19", "User Defined Date19", DataConnector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date20", "User Defined Date20", DataConnector.FieldTypeIdDate);
            svReceivingTrx.AddField("VADCDTRO", "Vendor Address Code - Remit To", DataConnector.FieldTypeIdString);

            var voidStatus = pop30300.AddField("VOIDSTTS", "Void Status", DataConnector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = svReceivingTrx.AddField("RATECALC", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svReceivingTrx.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var purchaseFreightTaxable = svReceivingTrx.AddField("Purchase_Freight_Taxable", "Purchase Freight Taxable", DataConnector.FieldTypeIdEnum);
            purchaseFreightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var purchaseMiscTaxable = svReceivingTrx.AddField("Purchase_Misc_Taxable", "Purchase Misc Taxable", DataConnector.FieldTypeIdEnum);
            purchaseMiscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
            
            var postingStatus = svReceivingTrx.AddField("ASI_Document_Status", "Posting Status", DataConnector.FieldTypeIdEnum);
            postingStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });

        }

        private DataConnectorEntity GetReceivingsTransactionLineEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListReceivingsLineItems, "Receivings transaction line items", ParentConnector);

            var svReceivingLine = entity.AddTable("svReceivingLine");

            AddReceivingsTransactionLineEntityFields(svReceivingLine);

            return entity;
        }
        private static void AddReceivingsTransactionLineEntityFields(DataConnectorTable svReceivingLine)
        {
            svReceivingLine.AddField("POPRCTNM", "POP Receipt Number", DataConnector.FieldTypeIdString, true);
            var popType = svReceivingLine.AddField("POPTYPE", "POP Type", DataConnector.FieldTypeIdEnum, true);
            popType.AddListItems(1, new List<string> { "Standard", "Drop-Ship", "Blanket", "Drop-Ship Blanket" });
            svReceivingLine.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svReceivingLine.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            svReceivingLine.AddField("QTYSHPPD", "QTY Shipped", DataConnector.FieldTypeIdQuantity, true);
            svReceivingLine.AddField("QTYINVCD", "QTY Invoiced", DataConnector.FieldTypeIdQuantity, true);
            svReceivingLine.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency, true);
            svReceivingLine.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency, true);
            svReceivingLine.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svReceivingLine.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString, true);

            svReceivingLine.AddField("RCPTLNNM", "Receipt Line Number", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("PONUMBER", "PO Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("QTYREJ", "QTY Rejected", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("Landed_Cost_Group_ID", "Landed Cost Group ID", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("Landed_Cost", "Landed Cost", DataConnector.FieldTypeIdYesNo);
            svReceivingLine.AddField("RCPTRETNUM", "Receipt Return Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("INVRETNUM", "Invoice Return Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("Total_Landed_Cost_Amount", "Total Landed Cost", DataConnector.FieldTypeIdCurrency);
            svReceivingLine.AddField("QTYRESERVED", "QTY Reserved", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("QTYINVRESERVE", "QTY Invoice Reserve", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("VNDITDSC", "Vendor Item Description", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("UMQTYINB", "U Of M QTY In Base", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("ACTLSHIP", "Actual Ship Date", DataConnector.FieldTypeIdDate);
            svReceivingLine.AddField("COMMNTID", "Comment ID", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("INVINDX", "Inventory Index", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svReceivingLine.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("NONINVEN", "Non IV", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("DECPLCUR", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("DECPLQTY", "Decimal Places QTYS", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("JOBNUMBR", "Job Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("COSTCODE", "Cost Code", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("COSTTYPE", "Cost Code Type", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("ORUNTCST", "Originating Unit Cost", DataConnector.FieldTypeIdCurrency);
            svReceivingLine.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            svReceivingLine.AddField("ODECPLCU", "Originating Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("BOLPRONUMBER", "BOL_PRO Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("Capital_Item", "Capital Item", DataConnector.FieldTypeIdYesNo);
            svReceivingLine.AddField("Product_Indicator", "Product Indicator", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("Purchase_IV_Item_Taxable", "Purchase IV Item Taxable", DataConnector.FieldTypeIdInteger);
            svReceivingLine.AddField("Purchase_Item_Tax_Schedu", "Purchase Item Tax Schedule ID", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("Purchase_Site_Tax_Schedu", "Purchase Site Tax Schedule ID", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("BSIVCTTL", "Based On Invoice Total", DataConnector.FieldTypeIdYesNo);
            svReceivingLine.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingLine.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svReceivingLine.AddField("PURPVIDX", "Purchase Price Variance Index", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svReceivingLine.AddField("VNDDOCNM", "Vendor Document Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("receiptdate", "Receipt Date", DataConnector.FieldTypeIdDate);
            svReceivingLine.AddField("GLPOSTDT", "GL Posting Date", DataConnector.FieldTypeIdDate);
            svReceivingLine.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            svReceivingLine.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("USER2ENT", "User To Enter", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            svReceivingLine.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            svReceivingLine.AddField("Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            svReceivingLine.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("QTYMATCH", "QTY Matched", DataConnector.FieldTypeIdQuantity);
            svReceivingLine.AddField("ProjNum", "Project Number", DataConnector.FieldTypeIdString);
            svReceivingLine.AddField("CostCatID", "Cost Category ID", DataConnector.FieldTypeIdString);

            var itemTrackingOption = svReceivingLine.AddField("ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
            
            var postingStatus = svReceivingLine.AddField("PSTGSTUS", "Posting Status", DataConnector.FieldTypeIdEnum);
            postingStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var rateCalculationMethod = svReceivingLine.AddField("RATECALC", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
        }

    }
}
