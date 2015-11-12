using System.Collections.Generic;

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
            Id = 3;
            Name = "Purchasing";
            Installed = true;
            UserDefined1 = "User defined 1";
            UserDefined2 = "User defined 2";
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

        private ConnectorEntity GetVendorEntity()
        {
            var entity = new ConnectorEntity(GpSmartListVendors, "Vendors", ParentConnector);
            var pm00200 = entity.AddTable("PM00200");
            var pm00201 = entity.AddTable("PM00201", "PM00200", ConnectorTable.ConnectorTableJoinType.Inner);
            pm00201.AddJoinFields("VENDORID", "VENDORID");
            AddVendorEntityFields(pm00200, pm00201);
            return entity;
        }
        private void AddVendorEntityFields(ConnectorTable pm00200, ConnectorTable pm00201)
        {
            var vendorId = pm00200.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            vendorId.KeyNumber = 1;

            pm00200.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString, true);
            pm00200.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString, true);
            pm00200.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString, true);
            pm00200.AddField("CITY", "City", Connector.FieldTypeIdString, true);
            pm00200.AddField("STATE", "State", Connector.FieldTypeIdString, true);
            pm00200.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString, true);
            pm00200.AddField("PHNUMBR1", "Phone number 1", Connector.FieldTypeIdPhone, true);
            pm00200.AddField("VNDCHKNM", "Vendor check name", Connector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor short name", Connector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor address code - primary", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor address code - purchase address", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor address code - ship from", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor address code - remit to", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor class ID", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor contact", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR2", "Phone number 2", Connector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax number", Connector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account number with vendor", Connector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID number", Connector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent vendor ID", Connector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade discount", Connector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum order", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum invoice amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment 1", Connector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment 2", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", UserDefined1, Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", UserDefined2, Connector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit limit amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment priority", Connector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL distribution history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum write off amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject to PPS deductions", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS tax rate", Connector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction exemption/variation number", Connector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate commencing date", Connector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate expiration date", Connector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting obligation undertaken", Connector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration date obligation", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed payee", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts payable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc charges account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write offs account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade discount account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);

            var modifyDate = pm00200.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            modifyDate.ModifyDate = true;

            var createDate = pm00200.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            createDate.CreateDate = true;

            pm00200.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pm00201.AddField("PM00201.HIESTBAL", "Highest balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.CURRBLNC", "Current balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.NOINVYTD", "Number of invoices - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOINVLIF", "Number of invoices - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOINVLYR", "Number of invoices - last year", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOPINYTD", "Number of paid invoices - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NOPILIFE", "Number of paid invoices - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.AMBLDTYD", "Amount billed - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMBLDLIF", "Amount billed - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMBLDLYR", "Amount billed - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMTPDYTD", "Amount paid - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMTPDLIF", "Amount paid - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.AMTPDLYR", "Amount paid - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TEN99AYTD", "1099 amount - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TEN99ALIF", "1099 amount - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TEN99ALYR", "1099 amount - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISAVYTD", "Discount available - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISAVLIF", "Discount available - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISAVLYR", "Discount available - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISTKYTD", "Discount taken - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISTKNLF", "Discount taken - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISTKLYR", "Discount taken - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISLSYTD", "Discount lost - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISLSTLF", "Discount lost - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DISLSLYR", "Discount lost - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.FINCHLIF", "Finance charges - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.FINCHLYR", "Finance charges - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.FINCHYTD", "Finance charges - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WROFSYTD", "Write offs - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WROFSLIF", "Write offs - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WROFSLYR", "Write offs - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.RTRNSYTD", "Returns - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.RTRNSLIF", "Returns - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.RTRNSLYR", "Returns - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TRDTKLIF", "Trade discounts taken - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TRDTLYR", "Trade discounts taken - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.TRDTYTD", "Trade discounts taken - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.NFNCHLIF", "Number of finance charges - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NFNCHLYR", "Number of finance charges - last year", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.NFNCHYTD", "Number of finance charges - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.RTNGOWED", "Retainage owed", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.LSTCHNUM", "Last check number", Connector.FieldTypeIdString);
            pm00201.AddField("PM00201.LSTCHKDT", "Last check date", Connector.FieldTypeIdDate);
            pm00201.AddField("PM00201.LSTCHAMT", "Last check amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.LSTINNUM", "Last invoice number", Connector.FieldTypeIdString);
            pm00201.AddField("PM00201.LSTINVAM", "Last invoice amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.LSTPURDT", "Last purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("PM00201.FSTPURDT", "First purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("PM00201.CURUNPBN", "Current unapplied payment balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.UNPDFNCH", "Unpaid finance charges", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.DYCHTCLR", "Days checks to clear", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.AVGDTPYR", "Average days to pay - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.AVDTPLIF", "Average days to pay - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("PM00201.ONORDAMT", "On order amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue vendor", Connector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax invoice received", Connector.FieldTypeIdYesNo);
            pm00201.AddField("PM00201.Withholding_LIFE", "Withholding - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WITHLYR", "Withholding - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("PM00201.WITHYTD", "Withholding - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RM00101.CUSTNMBR", "Customer number", Connector.FieldTypeIdString);

            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });

            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor status", Connector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });

            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 type", Connector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 vendor", "Dividend", "Interest", "Miscellaneous" });

            var maximumInvoiceAmount = pm00200.AddField("MXIAFVND", "Maximum invoice amount for vendors", Connector.FieldTypeIdEnum);
            maximumInvoiceAmount.AddListItems(0, new List<string> { "No maximum", "Amount" });

            var postToCashAccountFrom = pm00200.AddField("PTCSHACF", "Post to cash account from", Connector.FieldTypeIdEnum);
            postToCashAccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });

            var creditLimit = pm00200.AddField("CREDTLMT", "Credit limit", Connector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No credit", "Unlimited", "Amount" });

            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", Connector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not allowed", "Unlimited", "Maximum" });

            var postResultsTo = pm00200.AddField("Post_Results_To", "Post results to", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/discount account", "Purchasing offset account" });

            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free on board", Connector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
        }

        private ConnectorEntity GetVendorAddressEntity()
        {
            var entity = new ConnectorEntity(GpSmartListVendorAddresses, "Vendor addresses", ParentConnector);

            var pm00300 = entity.AddTable("PM00300");

            var pm00200 = entity.AddTable("PM00200", "PM00300", ConnectorTable.ConnectorTableJoinType.Inner);
            pm00200.AddJoinFields("VENDORID", "VENDORID");

            var sy01200 = entity.AddScript("select * from {0}..SY01200 with (NOLOCK) where Master_Type = 'VND'", "SY01200", "PM00300");
            sy01200.AddJoinFields("Master_ID", "VENDORID");
            sy01200.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddVendorAddressEntityFields(pm00300, pm00200, sy01200);
            
            return entity;
        }
        private static void AddVendorAddressEntityFields(ConnectorTable pm00300, ConnectorTable pm00200, ConnectorTable sy01200)
        {
            pm00300.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            pm00200.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString, true);
            pm00300.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString, true);
            pm00300.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString, true);
            pm00300.AddField("CITY", "City", Connector.FieldTypeIdString, true);
            pm00300.AddField("STATE", "State", Connector.FieldTypeIdString, true);
            pm00300.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString, true);
            pm00300.AddField("PHNUMBR1", "Phone number 1", Connector.FieldTypeIdPhone, true);
            pm00300.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor check name", Connector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor short name", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor class ID", Connector.FieldTypeIdString);
            pm00300.AddField("VNDCNTCT", "Vendor contact", Connector.FieldTypeIdString);
            pm00300.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            pm00300.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            pm00300.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            pm00300.AddField("PHNUMBR2", "Phone number 2", Connector.FieldTypeIdPhone);
            pm00300.AddField("FAXNUMBR", "Fax number", Connector.FieldTypeIdPhone);
            pm00300.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pm00300.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            sy01200.AddField("INET1", "INet1", Connector.FieldTypeIdString);
            sy01200.AddField("INET2", "INet2", Connector.FieldTypeIdString);
            sy01200.AddField("INET3", "INet3", Connector.FieldTypeIdString);
            sy01200.AddField("INET4", "INet4", Connector.FieldTypeIdString);
            sy01200.AddField("INET5", "INet5", Connector.FieldTypeIdString);
            sy01200.AddField("INET6", "INet6", Connector.FieldTypeIdString);
            sy01200.AddField("INET7", "INet7", Connector.FieldTypeIdString);
            sy01200.AddField("INET8", "INet8", Connector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor address code - primary", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor address code - purchase address", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor address code - ship from", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor address code - remit to", Connector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account number with vendor", Connector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID number", Connector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent vendor ID", Connector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade discount", Connector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum order", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum invoice amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment 1", Connector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment 2", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit limit amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment priority", Connector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL distribution history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum write off amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject to PPS deductions", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS tax rate", Connector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction exemption/variation number", Connector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate commencing date", Connector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate expiration date", Connector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting obligation undertaken", Connector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration date obligation", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed payee", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts payable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc charges account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write offs account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade discount account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pm00200.AddField("Revalue_Vendor", "Revalue vendor", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            pm00200.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            pm00200.AddField("TaxInvRecvd", "Tax invoice received", Connector.FieldTypeIdYesNo);

            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor status", Connector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 type", Connector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });
            
            var maximumInvoiceAmount = pm00200.AddField("MXIAFVND", "Maximum invoice amount for vendors", Connector.FieldTypeIdEnum);
            maximumInvoiceAmount.AddListItems(0, new List<string> { "No maximum", "Amount" });
            
            var postToCashAcccountFrom = pm00200.AddField("PTCSHACF", "Post to cash account from", Connector.FieldTypeIdEnum);
            postToCashAcccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit limit", Connector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", Connector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post results to", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/discount account", "Purchasing offset account" });
            
            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free on board", Connector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
        }

        private ConnectorEntity GetPurchaseOrderEntity()
        {
            var entity = new ConnectorEntity(GpSmartListPurchaseOrders, "Purchase orders", ParentConnector);

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
        private static void AddPurchaseOrderEntityFields(ConnectorTable svPopTrx, ConnectorTable pm00200, ConnectorTable pm00201, ConnectorTable rm00101, ConnectorTable rm00103, ConnectorTable pop10100)
        {
            svPopTrx.AddField("PONUMBER", "PO number", Connector.FieldTypeIdString, true);
            var poStatus = svPopTrx.AddField("POSTATUS", "PO status", Connector.FieldTypeIdEnum, true);
            poStatus.AddListItems(1, new List<string> { "New", "Released", "Change order", "Received", "Closed", "Canceled" });
            var poType = svPopTrx.AddField("POTYPE", "PO type", Connector.FieldTypeIdEnum, true);
            poType.AddListItems(1, new List<string> { "Standard", "Drop-ship", "Blanket", "Drop-ship blanket" });
            svPopTrx.AddField("DOCdate", "Document date", Connector.FieldTypeIdDate, true);
            svPopTrx.AddField("REMSUBTO", "Remaining subtotal", Connector.FieldTypeIdCurrency, true);
            svPopTrx.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svPopTrx.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString, true);
            pop10100.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo, true);

            svPopTrx.AddField("USER2ENT", "User to enter", Connector.FieldTypeIdString);
            svPopTrx.AddField("CONFIRM1", "Confirm with", Connector.FieldTypeIdString);
            svPopTrx.AddField("LSTEDTDT", "Last edit date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("LSTPRTDT", "Last printed date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("PRMdate", "Promised date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("PRMSHPDTE", "Promised ship date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("REQdate", "Required date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("REQTNDT", "Requisition date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svPopTrx.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            svPopTrx.AddField("SUBTOTAL", "Subtotal", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("CANCSUB", "Canceled subtotal", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("TRDISAMT", "Trade discount amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("MSCCHAMT", "Misc charges amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("MINORDER", "Minimum order", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("VADCDPAD", "Vendor address code - purchase address", Connector.FieldTypeIdString);
            svPopTrx.AddField("CMPANYID", "Company ID", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PRBTADCD", "Primary bill to address code", Connector.FieldTypeIdString);
            svPopTrx.AddField("PRSTADCD", "Primary ship to address code", Connector.FieldTypeIdString);
            svPopTrx.AddField("CMPNYNAM", "Company name", Connector.FieldTypeIdString);
            svPopTrx.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svPopTrx.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svPopTrx.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svPopTrx.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svPopTrx.AddField("CITY", "City", Connector.FieldTypeIdString);
            svPopTrx.AddField("STATE", "State", Connector.FieldTypeIdString);
            svPopTrx.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svPopTrx.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svPopTrx.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            svPopTrx.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            svPopTrx.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            svPopTrx.AddField("PYMTRMID", "Payment terms ID", Connector.FieldTypeIdString);
            svPopTrx.AddField("DSCDLRAM", "Discount amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("DSCPCTAM", "Discount percent amount", Connector.FieldTypeIdPercentage);
            svPopTrx.AddField("DISAMTAV", "Discount amount available", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("TRDPCTPR", "Trade discount percent (precise)", Connector.FieldTypeIdPercentage);
            svPopTrx.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString);
            svPopTrx.AddField("TIMESPRT", "Times printed", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("COMMNTID", "Comment ID", Connector.FieldTypeIdString);
            svPopTrx.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svPopTrx.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svPopTrx.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svPopTrx.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svPopTrx.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svPopTrx.AddField("DENXRATE", "Denomination exchange rate", Connector.FieldTypeIdQuantity);
            svPopTrx.AddField("OREMSUBT", "Originating remaining subtotal", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORSUBTOT", "Originating subtotal", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("Originating_Canceled_Sub", "Originating canceled subtotal", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORTDISAM", "Originating trade discount amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("OMISCAMT", "Originating misc charges amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORDDLRAT", "Originating discount amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ODISAMTAV", "Originating discount amount available", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("BUYERID", "Buyer ID", Connector.FieldTypeIdString);
            pop10100.AddField("ONORDAMT", "On order amount", Connector.FieldTypeIdCurrency);
            pop10100.AddField("ORORDAMT", "Originating on order amount", Connector.FieldTypeIdCurrency);
            pop10100.AddField("ONHOLDdate", "On hold date", Connector.FieldTypeIdDate);
            pop10100.AddField("ONHOLDBY", "On hold by", Connector.FieldTypeIdString);
            pop10100.AddField("HOLDREMOVEdate", "Hold remove date", Connector.FieldTypeIdDate);
            pop10100.AddField("HOLDREMOVEBY", "Hold remove by", Connector.FieldTypeIdString);
            svPopTrx.AddField("ALLOWSOCMTS", "Allow sales order commitments", Connector.FieldTypeIdYesNo);
            svPopTrx.AddField("PONOTIDS_1", "PO note ID - PO", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_2", "PO note ID - site", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_3", "PO note ID - vendor", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_4", "PO note ID - comment", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_5", "PO note ID - payment terms", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PONOTIDS_6", "PO note ID - shipping method", Connector.FieldTypeIdInteger);
            pm00200.AddField("VENDNAME", "Vendor name from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor check name", Connector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor short name", Connector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor address code - primary", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor address code - purchase address from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor address code - ship from", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor address code - remit to", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor class ID", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor contact", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS1", "Address 1 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS2", "Address 2 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("CITY", "City from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("STATE", "State from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ZIPCODE", "Zip Code from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR1", "Phone number 1 from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("PHNUMBR2", "Phone number 2 from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax Number from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping Method from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax schedule ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account number with vendor", Connector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID number", Connector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax registration number from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent vendor ID", Connector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade discount", Connector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum order from vendor master", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment terms ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum payment percent", Connector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum payment amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum invoice amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment 1", Connector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment 2", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit limit amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment priority", Connector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL distribution history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum write off amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject to PPS deductions", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS tax rate", Connector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction exemption/variation number", Connector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate commencing date", Connector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate expiration date", Connector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting obligation undertaken", Connector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration date obligation", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed payee", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts payable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc charges account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write offs account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade discount account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note index from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified date from vendor master", Connector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created date from vendor master", Connector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate type ID from vendor master", Connector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of invoices - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of invoices - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of invoices - last year", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of paid invoices - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of paid invoices - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount billed - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount billed - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount billed - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount paid - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount paid - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount paid - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 amount - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 amount - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 amount - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount available - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount available - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount available - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount taken - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount taken - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount taken - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount lost - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount lost - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount lost - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance charge - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance charge - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance charge - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write offs - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write offs - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write offs - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade discounts taken - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade discounts taken - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade discounts taken - year to date", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of finance charges - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of finance charges - last year", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of finance charges - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage owed", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last check number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last check date", Connector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last check amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last invoice number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last invoice amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current unapplied payment balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid finance charges", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days checks to clear", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average days to pay - year to date", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average days to pay - life", Connector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On order amount from vendor master", Connector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue vendor", Connector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount grace period from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due date grace period from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental corporate ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental individual ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3 from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax invoice received", Connector.FieldTypeIdYesNo);
            pm00200.AddField("Withholding_LIFE", "Withholding - life", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WITHLYR", "Withholding - last year", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WITHYTD", "Withholding - year to date", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate customer number", Connector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact person", Connector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement name", Connector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
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
            rm00101.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment terms ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit limit amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit limit period", Connector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit limit period amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate type ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer discount", Connector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum payment amount from customer master", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum payment percent from customer master", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance charge percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance charge amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max writeoff amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User defined 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User defined 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax exempt 1", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax exempt 2", Connector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax registration number from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", Connector.FieldTypeIdString);
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
            rm00101.AddField("HOLD", "Hold from customer master", Connector.FieldTypeIdYesNo);
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
            rm00103.AddField("AVDTPLYR", "Average days to pay - last year from customer master", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average days to pay - life from customer master", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average days to pay - year to date from customer master", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number of ADTP documents - life", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number of ADTP documents - year to date", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number of ADTP documents - last year", Connector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total Discounts Taken YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total Discounts Taken LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total Discounts Taken LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total Discounts Available YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total Amount Of NSF Checks YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number Of NSF Checks YTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted Sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted Cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted Other Sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted Other Cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non Current Scheduled Payments", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total Sales YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total Sales LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total Sales LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total Costs YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total Costs LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total Costs LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total Cash Received YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total Cash Received LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total Cash Received LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total Finance Charges YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total Finance Charges LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total Finance Charges LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance Charges CYTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance Charges LYR Calendar", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total Bad Debt YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total Bad Deb LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total Bad Debt LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total Waived FC YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total Waived FC LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total Waived FC LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total Writeoffs YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total Writeoffs LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total Writeoffs LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total number of invoices YTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total number of invoices LTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total number of invoices LYR", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Toal number of finance charges YTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Toal number of finance charges LTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Toal number of finance charges LYR", Connector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write Offs LIFE from customer master", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR from customer master", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD from customer master", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", Connector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount from customer master", Connector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", Connector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", Connector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from customer master", Connector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", Connector.FieldTypeIdCurrency);
            pop10100.AddField("Change_Order_Flag", "Change Order Flag", Connector.FieldTypeIdYesNo);
            svPopTrx.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("DUEGRPER", "Due Date grace period", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            pop10100.AddField("PO_Field_Changes", "PO Field Changes", Connector.FieldTypeIdString);
            svPopTrx.AddField("Revision_Number", "Revision number", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            svPopTrx.AddField("TAXSCHID", "Tax Schedule ID from Purchase Transactions", Connector.FieldTypeIdString);
            svPopTrx.AddField("BSIVCTTL", "Based On Invoice total", Connector.FieldTypeIdString);
            svPopTrx.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            svPopTrx.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdString);
            svPopTrx.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORFRTTAX", "Originating Freight tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("ORMSCTAX", "Originating Misc tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("BCKTXAMT", "Backout tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("OBTAXAMT", "Originating Backout tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("BackoutFreightTaxAmt", "Backout Freight tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("OrigBackoutFreightTaxAmt", "Originating Backout Freight tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("BackoutMiscTaxAmt", "Backout Misc tax amount", Connector.FieldTypeIdCurrency); 
            svPopTrx.AddField("OrigBackoutMiscTaxAmt", "Originating Backout Misc tax amount", Connector.FieldTypeIdCurrency);
            pop10100.AddField("BackoutTradeDiscTax", "Backout Trade Discount tax amount", Connector.FieldTypeIdCurrency);
            pop10100.AddField("OrigBackoutTradeDiscTax", "Originating Backout Trade Discount tax amount", Connector.FieldTypeIdCurrency);
            svPopTrx.AddField("POPCONTNUM", "Contract number", Connector.FieldTypeIdString);
            svPopTrx.AddField("CONTENDDTE", "Contract Expiration date", Connector.FieldTypeIdDate);
            svPopTrx.AddField("CNTRLBLKTBY", "Control Blanket By", Connector.FieldTypeIdInteger);
            svPopTrx.AddField("PURCHCMPNYNAM", "Purchase Company name", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHCONTACT", "Purchase Contact", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHADDRESS1", "Purchase Address 1", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHADDRESS2", "Purchase Address 2", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHADDRESS3", "Purchase Address 3", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHCITY", "Purchase City", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHSTATE", "Purchase State", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHZIPCODE", "Purchase Zip code", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHCOUNTRY", "Purchase Country", Connector.FieldTypeIdString);
            svPopTrx.AddField("PURCHPHONE1", "Purchase Phone 1", Connector.FieldTypeIdPhone);
            svPopTrx.AddField("PURCHPHONE2", "Purchase Phone 2", Connector.FieldTypeIdPhone);
            svPopTrx.AddField("PURCHPHONE3", "Purchase Phone 3", Connector.FieldTypeIdPhone);
            svPopTrx.AddField("PURCHFAX", "Purchase Fax", Connector.FieldTypeIdPhone);
            svPopTrx.AddField("TXSCHSRC", "Tax Schedule source", Connector.FieldTypeIdString);

            var documentStatus = svPopTrx.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var rateCalculationMethod = svPopTrx.AddField("RATECALC", "Rate Calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svPopTrx.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var statusGroup = svPopTrx.AddField("STATGRP", "Status Group", Connector.FieldTypeIdEnum);
            statusGroup.AddListItems(1, new List<string> { "Active", "Closed" });
            
            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor status", Connector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 type", Connector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var maximumInvoiceAmountForVendors = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", Connector.FieldTypeIdEnum);
            maximumInvoiceAmountForVendors.AddListItems(0, new List<string> { "No Maximum", "Amount" });
            
            var postToCashAccountFrom = pm00200.AddField("PTCSHACF", "Post To Cash Account From", Connector.FieldTypeIdEnum);
            postToCashAccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", Connector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", Connector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });
            
            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free On Board", Connector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
            
            var creditLimitType = rm00101.AddField("CRLMTTYP", "Credit Limit type", Connector.FieldTypeIdEnum);
            creditLimitType.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var minimumPaymentTypeFromCustomerMaster = rm00101.AddField("MINPYTYP", "Minimum Payment Type from customer master", Connector.FieldTypeIdEnum);
            minimumPaymentTypeFromCustomerMaster.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance Charge Amount type", Connector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum Writeoff type", Connector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", Connector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open Item", "Balance Forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement Cycle", Connector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No Statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default Cash Account type", Connector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsToFromCustomerMaster = rm00101.AddField("Post_Results_To", "Post Results To from customer master", Connector.FieldTypeIdEnum);
            postResultsToFromCustomerMaster.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });
            
            var poStatusOrig = pop10100.AddField("PO_Status_Orig", "PO Status Orig", Connector.FieldTypeIdEnum);
            poStatusOrig.AddListItems(1, new List<string> { "New", "Released", "Change order", "Received", "Closed", "Canceled" });
            
            var purchaseFreightTaxable = svPopTrx.AddField("Purchase_Freight_Taxable", "Purchase Freight Taxable", Connector.FieldTypeIdEnum);
            purchaseFreightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var purchaseMiscTaxable = svPopTrx.AddField("Purchase_Misc_Taxable", "Purchase Misc Taxable", Connector.FieldTypeIdEnum);
            purchaseMiscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

        }

        private ConnectorEntity GetPurchaseOrderLineEntity()
        {
            var entity = new ConnectorEntity(GpSmartListPurchaseLineItems, "Purchase order line items", ParentConnector);

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

            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100)", "Item Shipping Weight", Connector.FieldTypeIdQuantity);
            entity.AddCalculation("svPOPLine.DECPLCUR - 1", "Decimal Places Currency", Connector.FieldTypeIdInteger);
            entity.AddCalculation("svPOPLine.DECPLQTY - 1", "Decimal Places QTYS", Connector.FieldTypeIdInteger);

            return entity;
        }
        private static void AddPurchaseOrderLineEntityFields(ConnectorTable svPopLine, ConnectorTable pm00200, ConnectorTable pm00201, ConnectorTable iv00101, ConnectorTable rm00101, ConnectorTable rm00103, ConnectorTable pop10100, ConnectorTable pop10110, ConnectorTable pop10500)
        {
            svPopLine.AddField("PONUMBER", "PO number", Connector.FieldTypeIdString, true);
            var poLineStatus = svPopLine.AddField("POLNESTA", "PO Line status", Connector.FieldTypeIdEnum, true);
            poLineStatus.AddListItems(1, new List<string> { "New", "Released", "Change order", "Received", "Closed", "Canceled" });
            var poType = svPopLine.AddField("POTYPE", "PO type", Connector.FieldTypeIdEnum, true);
            poType.AddListItems(1, new List<string> { "Standard", "Drop-Ship", "Blanket", "Drop-Ship Blanket" });
            svPopLine.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svPopLine.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            svPopLine.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svPopLine.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svPopLine.AddField("QTYORDER", "Quantity Ordered", Connector.FieldTypeIdQuantity, true);
            svPopLine.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency, true);
            svPopLine.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency, true);

            svPopLine.AddField("ORD", "Ord", Connector.FieldTypeIdInteger);
            svPopLine.AddField("VNDITNUM", "Vendor Item number", Connector.FieldTypeIdString);
            svPopLine.AddField("VNDITDSC", "Vendor Item description", Connector.FieldTypeIdString);
            svPopLine.AddField("NONINVEN", "Non IV", Connector.FieldTypeIdYesNo);
            svPopLine.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svPopLine.AddField("UMQTYINB", "U of M QTY In Base", Connector.FieldTypeIdQuantity);
            svPopLine.AddField("QTYCANCE", "Quantity Canceled", Connector.FieldTypeIdQuantity);
            pop10110.AddField("QTYCMTBASE", "Quantity Committed In Base", Connector.FieldTypeIdQuantity);
            pop10110.AddField("QTYUNCMTBASE", "Quantity Uncommitted In Base", Connector.FieldTypeIdQuantity);
            svPopLine.AddField("INVINDX", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svPopLine.AddField("REQdate", "Required date", Connector.FieldTypeIdDate);
            svPopLine.AddField("PRMdate", "Promised date", Connector.FieldTypeIdDate);
            svPopLine.AddField("PRMSHPDTE", "Promised Ship date", Connector.FieldTypeIdDate);
            svPopLine.AddField("REQSTDBY", "Requested By", Connector.FieldTypeIdString);
            svPopLine.AddField("COMMNTID", "Comment ID", Connector.FieldTypeIdString);
            svPopLine.AddField("DOCTYPE", "Document type", Connector.FieldTypeIdInteger);
            svPopLine.AddField("BRKFLD1", "Break Field 1", Connector.FieldTypeIdInteger);
            pop10110.AddField("QTY_Canceled_Orig", "Quantity Canceled Orig", Connector.FieldTypeIdQuantity);
            pop10110.AddField("OPOSTSUB", "Originating Posted subtotal", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("JOBNUMBR", "Job number", Connector.FieldTypeIdString);
            svPopLine.AddField("COSTCODE", "Cost code", Connector.FieldTypeIdString);
            svPopLine.AddField("COSTTYPE", "Cost Code type", Connector.FieldTypeIdInteger);
            pop10110.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svPopLine.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            pop10110.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            pop10110.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svPopLine.AddField("ORUNTCST", "Originating Unit cost", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("OREXTCST", "Originating Extended cost", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("LINEORIGIN", "Line Origin", Connector.FieldTypeIdInteger);
            svPopLine.AddField("ODECPLCU - 1", "Originating Decimal Places Currency", Connector.FieldTypeIdInteger);
            pop10110.AddField("Capital_Item", "Capital Item", Connector.FieldTypeIdYesNo);
            svPopLine.AddField("Product_Indicator", "Product Indicator", Connector.FieldTypeIdInteger);
            svPopLine.AddField("Source_Document_Number", "Source Document number", Connector.FieldTypeIdString);
            svPopLine.AddField("Source_Document_Line_Num", "Source Document Line number", Connector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_1", "Line Note 1", Connector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_2", "Line Note 2", Connector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_3", "Line Note 3", Connector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_4", "Line Note 4", Connector.FieldTypeIdInteger);
            svPopLine.AddField("POLNEARY_5", "Line Note 5", Connector.FieldTypeIdInteger);
            svPopLine.AddField("USER2ENT", "User To Enter", Connector.FieldTypeIdString);
            svPopLine.AddField("CONFIRM1", "Confirm With", Connector.FieldTypeIdString);
            svPopLine.AddField("DOCdate", "Document date", Connector.FieldTypeIdDate);
            svPopLine.AddField("LSTEDTDT", "Last Edit date", Connector.FieldTypeIdDate);
            svPopLine.AddField("LSTPRTDT", "Last Printed date", Connector.FieldTypeIdDate);
            svPopLine.AddField("PRMdate", "Promised Date from Purchase Transactions", Connector.FieldTypeIdDate);
            svPopLine.AddField("PRMSHPDTE", "Promised Ship Date from Purchase Transactions", Connector.FieldTypeIdDate);
            svPopLine.AddField("REQdate", "Required Date from Purchase Transactions", Connector.FieldTypeIdDate);
            svPopLine.AddField("REQTNDT", "Requisition date", Connector.FieldTypeIdDate);
            svPopLine.AddField("SHIPMTHD", "Shipping Method from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("TXRGNNUM", "Tax Registration number", Connector.FieldTypeIdString);
            svPopLine.AddField("REMSUBTO", "Remaining subtotal", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("SUBTOTAL", "Subtotal", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("CANCSUB", "Canceled subtotal", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("MSCCHAMT", "Misc Charges amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("TAXAMNT", "Tax Amount from Purchase Transactions", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString);
            svPopLine.AddField("MINORDER", "Minimum order", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("VADCDPAD", "Vendor address code - Purchase Address", Connector.FieldTypeIdString);
            svPopLine.AddField("CMPANYID", "Company ID", Connector.FieldTypeIdInteger);
            svPopLine.AddField("PRBTADCD", "Primary Billto Address code", Connector.FieldTypeIdString);
            svPopLine.AddField("PRSTADCD", "Primary Shipto Address code", Connector.FieldTypeIdString);
            svPopLine.AddField("CMPNYNAM", "Company Name from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("CONTACT", "Contact from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS1", "Address 1 from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS2", "Address 2 from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS3", "Address 3 from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("CITY", "City from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("STATE", "State from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("ZIPCODE", "Zip Code from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("COUNTRY", "Country from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("PHONE1", "Phone 1 from Purchase Transactions", Connector.FieldTypeIdPhone);
            svPopLine.AddField("PHONE2", "Phone 2 from Purchase Transactions", Connector.FieldTypeIdPhone);
            svPopLine.AddField("FAX", "Fax from Purchase Transactions", Connector.FieldTypeIdPhone);
            svPopLine.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            svPopLine.AddField("DSCDLRAM", "Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("DSCPCTAM", "Discount Percent amount", Connector.FieldTypeIdPercentage);
            svPopLine.AddField("DISAMTAV", "Discount Amount Available", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            svPopLine.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            svPopLine.AddField("TRDPCTPR", "Trade Discount Percent (Precise)", Connector.FieldTypeIdPercentage);
            svPopLine.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString);
            svPopLine.AddField("TIMESPRT", "Times Printed", Connector.FieldTypeIdInteger);
            svPopLine.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            svPopLine.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            svPopLine.AddField("COMMNTID", "Comment ID from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("CURNCYID", "Currency ID from Purchase Transactions", Connector.FieldTypeIdString);
            svPopLine.AddField("CURRNIDX", "Currency Index from Purchase Transactions", Connector.FieldTypeIdInteger);
            svPopLine.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svPopLine.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svPopLine.AddField("XCHGRATE", "Exchange Rate from Purchase Transactions", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svPopLine.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svPopLine.AddField("DENXRATE", "Denomination Exchange Rate from Purchase Transactions", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("OREMSUBT", "Originating Remaining subtotal", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("ORSUBTOT", "Originating subtotal", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("Originating_Canceled_Sub", "Originating Canceled subtotal", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("OMISCAMT", "Originating Misc Charges amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("ORTAXAMT", "Originating Tax Amount from Purchase Transactions", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("ORDDLRAT", "Originating Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("ODISAMTAV", "Originating Discount Amount Available", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("BUYERID", "Buyer ID", Connector.FieldTypeIdString);
            pop10100.AddField("ONORDAMT", "On Order amount", Connector.FieldTypeIdCurrency);
            pop10100.AddField("ORORDAMT", "Originating On Order amount", Connector.FieldTypeIdCurrency);
            pop10100.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            pop10100.AddField("ONHOLDdate", "On Hold date", Connector.FieldTypeIdDate);
            pop10100.AddField("ONHOLDBY", "On Hold By", Connector.FieldTypeIdString);
            pop10100.AddField("HOLDREMOVEdate", "Hold Remove date", Connector.FieldTypeIdDate);
            pop10100.AddField("HOLDREMOVEBY", "Hold Remove By", Connector.FieldTypeIdString);
            svPopLine.AddField("ALLOWSOCMTS", "Allow SO Commitments", Connector.FieldTypeIdYesNo);
            svPopLine.AddField("PONOTIDS_1", "PO Note ID - PO", Connector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_2", "PO Note ID - Site", Connector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_3", "PO Note ID - Vendor", Connector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_4", "PO Note ID - Comment", Connector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_5", "PO Note ID - Payment Terms", Connector.FieldTypeIdInteger);
            svPopLine.AddField("PONOTIDS_6", "PO Note ID - Shipping method", Connector.FieldTypeIdInteger);
            pm00200.AddField("VENDNAME", "Vendor Name from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor Check name", Connector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short name", Connector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor address code - Primary", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor address code - Purchase Address from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor address code - Ship From", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor address code - Remit To", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor Class ID", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor Contact", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS1", "Address 1 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS2", "Address 2 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("CITY", "City from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("STATE", "State from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ZIPCODE", "Zip Code from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR1", "Phone number 1 from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("PHNUMBR2", "Phone number 2 from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax Number from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping Method from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax Schedule ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", Connector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID number", Connector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration Number from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", Connector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", Connector.FieldTypeIdInteger);
            pm00200.AddField("MINORDER", "Minimum Order from vendor master", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment Terms ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum Payment Percent", Connector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum Payment Dollar", Connector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum Invoice Dollar", Connector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment1 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment2 from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit Limit Dollar", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment Priority", Connector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep Calendar History from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep Period History from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep Trx History from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum Write Off amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject To PPS Deductions", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS Tax rate", Connector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction Exemption/Variation number", Connector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate Commencing date", Connector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate Expiration date", Connector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting Obligation Undertaken", Connector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration Date Obligation", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed Payee", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts Payable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount Available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount Taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance Charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc Charges account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write Offs account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade Discount account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued Purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase Price Variance Account Number from vendor master", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date from vendor master", Connector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date from vendor master", Connector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID from vendor master", Connector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of Invoices YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of Invoices LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of Invoices LYR", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of Paid Invoices YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of Paid Invoices LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount Billed YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount Billed LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount Billed LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount Paid YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount Paid LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount Paid LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 Amount YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 Amount LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 Amount LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount Available YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount Available LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount Available LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount Taken YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount Taken LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount Taken LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount Lost YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount Lost LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount Lost LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance Charge LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance Charge LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance Charge YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write Offs YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write Offs LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write Offs LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade Discounts Taken LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade Discounts Taken LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade Discounts Taken YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of Finance Charges LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of Finance Charges LYR", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of Finance Charges YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage Owed", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last Check number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last Check date", Connector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last Check amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last Invoice number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last Invoice amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last Purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First Purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current Unapplied Payment Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid Finance Charges", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days Checks To Clear", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average Days To Pay - Year", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average Days To Pay - Life", Connector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On Order Amount from vendor master", Connector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", Connector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount Grace Period from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", Connector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date Grace Period from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3 from vendor master", Connector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received", Connector.FieldTypeIdYesNo);
            pm00201.AddField("Withholding_LIFE", "Withholding LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WITHLYR", "Withholding LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WITHYTD", "Withholding YTD", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTCLAS", "Customer class", Connector.FieldTypeIdString);
            rm00101.AddField("CPRCSTNM", "Corporate Customer number", Connector.FieldTypeIdString);
            rm00101.AddField("CNTCPRSN", "Contact Person", Connector.FieldTypeIdString);
            rm00101.AddField("STMTNAME", "Statement name", Connector.FieldTypeIdString);
            rm00101.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            rm00101.AddField("ADRSCODE", "Address Code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("UPSZONE", "UPS Zone from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("SHIPMTHD", "Shipping Method from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXSCHID", "Tax Schedule ID from customer master", Connector.FieldTypeIdString);
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
            rm00101.AddField("PRBTADCD", "Primary Billto address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("PRSTADCD", "Primary Shipto address code from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("STADDRCD", "Statement Address code", Connector.FieldTypeIdString);
            rm00101.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            rm00101.AddField("CHEKBKID", "Checkbook ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("PYMTRMID", "Payment Terms ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CRLMTAMT", "Credit Limit amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CRLMTPER", "Credit Limit period", Connector.FieldTypeIdInteger);
            rm00101.AddField("CRLMTPAM", "Credit Limit Period amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("CURNCYID", "Currency ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("RATETPID", "Rate Type ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("CUSTDISC", "Customer Discount", Connector.FieldTypeIdPercentage);
            rm00101.AddField("PRCLEVEL", "PriceLevel", Connector.FieldTypeIdString);
            rm00101.AddField("MINPYDLR", "Minimum Payment Dollar from customer master", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MINPYPCT", "Minimum Payment Percent from customer master", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FNCHPCNT", "Finance Charge Percent", Connector.FieldTypeIdPercentage);
            rm00101.AddField("FINCHDLR", "Finance Charge Dollar", Connector.FieldTypeIdCurrency);
            rm00101.AddField("MXWROFAM", "Max Writeoff amount", Connector.FieldTypeIdCurrency);
            rm00101.AddField("COMMENT1", "Comment1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("COMMENT2", "Comment2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF1", "User defined 1 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("USERDEF2", "User defined 2 from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT1", "Tax Exempt 1", Connector.FieldTypeIdString);
            rm00101.AddField("TAXEXMT2", "Tax Exempt 2", Connector.FieldTypeIdString);
            rm00101.AddField("TXRGNNUM", "Tax Registration Number from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            rm00101.AddField("BNKBRNCH", "Bank Branch", Connector.FieldTypeIdString);
            rm00101.AddField("SALSTERR", "Sales Territory", Connector.FieldTypeIdString);
            rm00101.AddField("RMCSHACC", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMARACC", "Accounts Receivable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMSLSACC", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMIVACC", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMCOSACC", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMTAKACC", "Discounts Taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMAVACC", "Discounts Available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMFCGACC", "Finance Charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("RMWRACC", "Writeoff account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            rm00101.AddField("FRSTINDT", "First Invoice date", Connector.FieldTypeIdDate);
            rm00101.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            rm00101.AddField("HOLD", "Hold from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("CRCARDID", "Credit Card ID", Connector.FieldTypeIdString);
            rm00101.AddField("CCRDXPDT", "Credit Card Exp date", Connector.FieldTypeIdDate);
            rm00101.AddField("KPDSTHST", "Keep Distribution History from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPCALHST", "Keep Calendar History from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPERHIST", "Keep Period History from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("KPTRXHST", "Keep Trx History from customer master", Connector.FieldTypeIdYesNo);
            rm00101.AddField("NOTEINDX", "Note Index from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("CREATDDT", "Created Date from customer master", Connector.FieldTypeIdDate);
            rm00101.AddField("MODIFDT", "Modified Date from customer master", Connector.FieldTypeIdDate);
            rm00103.AddField("TNSFCLIF", "Total Amount Of NSF Checks Life", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFLIF", "Number Of NSF Checks Life", Connector.FieldTypeIdInteger);
            rm00103.AddField("CUSTBLNC", "Customer Balance", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_1", "Aging Period Amount 1", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_2", "Aging Period Amount 2", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_3", "Aging Period Amount 3", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_4", "Aging Period Amount 4", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_5", "Aging Period Amount 5", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_6", "Aging Period Amount 6", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AGPERAMT_7", "Aging Period Amount 7", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTAGED", "Last Aged", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTNSFCD", "Last NSF Check date", Connector.FieldTypeIdDate);
            rm00103.AddField("LPYMTAMT", "Last Payment amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTPYDT", "Last Payment date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXDT", "Last Transaction date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTTRXAM", "Last Transaction amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LSTFCHAM", "Last Finance Charge amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UPFCHYTD", "Unpaid Finance Charges YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("AVDTPLYR", "Average Days to Pay - LYR from customer master", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVDTPLIF", "Average Days To Pay - Life from customer master", Connector.FieldTypeIdInteger);
            rm00103.AddField("AVGDTPYR", "Average Days To Pay - Year from customer master", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPL", "Number ADTP Documents - Life", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPY", "Number ADTP Documents - Year", Connector.FieldTypeIdInteger);
            rm00103.AddField("NUMADTPR", "Number ADTP Documents - LYR", Connector.FieldTypeIdInteger);
            rm00103.AddField("TDTKNYTD", "Total Discounts Taken YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLYR", "Total Discounts Taken LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDTKNLTD", "Total Discounts Taken LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TDISAYTD", "Total Discounts Available YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("RETAINAG", "Retainage", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TNSFCYTD", "Total Amount Of NSF Checks YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NONSFYTD", "Number Of NSF Checks YTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("UNPSTDSA", "Unposted Sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTDCA", "Unposted Cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOSA", "Unposted Other Sales amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("UNPSTOCA", "Unposted Other Cash amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("NCSCHPMT", "Non Current Scheduled Payments", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLYTD", "Total Sales YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLTD", "Total Sales LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLSLLYR", "Total Sales LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTYTD", "Total Costs YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLTD", "Total Costs LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCOSTLYR", "Total Costs LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRYTD", "Total Cash Received YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLTD", "Total Cash Received LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TCSHRLYR", "Total Cash Received LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHYTD", "Total Finance Charges YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLTD", "Total Finance Charges LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TFNCHLYR", "Total Finance Charges LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHCYTD", "Finance Charges CYTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("FNCHLYRC", "Finance Charges LYR Calendar", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTYTD", "Total Bad Debt YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLYR", "Total Bad Deb LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TBDDTLTD", "Total Bad Debt LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCYTD", "Total Waived FC YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLTD", "Total Waived FC LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWVFCLYR", "Total Waived FC LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFYTD", "Total Writeoffs YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLTD", "Total Writeoffs LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TWROFLYR", "Total Writeoffs LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLINYTD", "Total number of invoices YTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLTD", "Total number of invoices LTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLINLYR", "Total number of invoices LYR", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCYTD", "Toal number of finance charges YTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLTD", "Toal number of finance charges LTD", Connector.FieldTypeIdInteger);
            rm00103.AddField("TTLFCLYR", "Toal number of finance charges LYR", Connector.FieldTypeIdInteger);
            rm00103.AddField("WROFSLIF", "Write Offs LIFE from customer master", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSLYR", "Write Offs LYR from customer master", Connector.FieldTypeIdCurrency);
            rm00103.AddField("WROFSYTD", "Write Offs YTD from customer master", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLYR", "High Balance LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALYTD", "High Balance YTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("HIBALLTD", "High Balance LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("LASTSTDT", "Last Statement date", Connector.FieldTypeIdDate);
            rm00103.AddField("LSTSTAMT", "Last Statement amount", Connector.FieldTypeIdCurrency);
            rm00103.AddField("DEPRECV", "Deposits Received", Connector.FieldTypeIdCurrency);
            rm00103.AddField("ONORDAMT", "On Order Amount from customer master", Connector.FieldTypeIdCurrency);
            rm00101.AddField("Revalue_Customer", "Revalue Customer", Connector.FieldTypeIdYesNo);
            rm00101.AddField("FINCHID", "Finance Charge ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVCRPID", "Governmental Corporate ID", Connector.FieldTypeIdString);
            rm00101.AddField("GOVINDID", "Governmental Individual ID", Connector.FieldTypeIdString);
            rm00101.AddField("DISGRPER", "Discount Grace Period from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("DOCFMTID", "Document Format ID from customer master", Connector.FieldTypeIdString);
            rm00101.AddField("DUEGRPER", "Due Date Grace Period from customer master", Connector.FieldTypeIdInteger);
            rm00101.AddField("PHONE3", "Phone 3 from customer master", Connector.FieldTypeIdPhone);
            rm00103.AddField("TTLRTLTD", "Total Returns LTD", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTLYR", "Total Returns LYR", Connector.FieldTypeIdCurrency);
            rm00103.AddField("TTLRTYTD", "Total Returns YTD", Connector.FieldTypeIdCurrency);
            pop10100.AddField("Change_Order_Flag", "Change Order Flag from Purchase Transactions", Connector.FieldTypeIdYesNo);
            svPopLine.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            svPopLine.AddField("DUEGRPER", "Due Date grace period", Connector.FieldTypeIdInteger);
            svPopLine.AddField("PHONE3", "Phone 3 from Purchase Transactions", Connector.FieldTypeIdPhone);
            pop10100.AddField("PO_Field_Changes", "PO Field Changes", Connector.FieldTypeIdString);
            svPopLine.AddField("Revision_Number", "Revision number", Connector.FieldTypeIdInteger);
            svPopLine.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            iv00101.AddField("ITEMDESC", "Item Description from item master", Connector.FieldTypeIdString);
            iv00101.AddField("NOTEINDX", "Note Index from item master", Connector.FieldTypeIdInteger);
            iv00101.AddField("ITMSHNAM", "Item Short name", Connector.FieldTypeIdString);
            iv00101.AddField("ITMGEDSC", "Item Generic description", Connector.FieldTypeIdString);
            iv00101.AddField("STNDCOST", "Standard cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("CURRCOST", "Current cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("ITMTSHID", "Item Tax Schedule ID", Connector.FieldTypeIdString);
            iv00101.AddField("IVIVINDX", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVIVOFIX", "Inventory Offset account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVCOGSIX", "COGS account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLSIDX", "Sales account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLDSIX", "Sales Discounts account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVSLRNIX", "Sales Returns account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINUSIX", "In Use account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINSVIX", "In Service account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVDMGIDX", "Damaged account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVVARIDX", "Variances account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("DPSHPIDX", "Drop Ship account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURPVIDX", "Purchase Price Variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVRETIDX", "Inventory Returns account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ASMVRIDX", "Assembly Variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITMCLSCD", "Item Class code", Connector.FieldTypeIdString);
            iv00101.AddField("LOTTYPE", "Lot type", Connector.FieldTypeIdString);
            iv00101.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPCALHST", "Keep Calendar history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPDSTHST", "Keep Distribution history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("ALWBKORD", "Allow Back Orders", Connector.FieldTypeIdYesNo);
            iv00101.AddField("UOMSCHDL", "U of M Schedule", Connector.FieldTypeIdString);
            iv00101.AddField("ALTITEM1", "Alternate Item 1", Connector.FieldTypeIdString);
            iv00101.AddField("ALTITEM2", "Alternate Item 2", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_1", "User Category Value 1", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_2", "User Category Value 2", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_3", "User Category Value 3", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_4", "User Category Value 4", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_5", "User Category Value 5", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_6", "User Category Value 6", Connector.FieldTypeIdString);
            iv00101.AddField("MSTRCDTY", "Master Record type", Connector.FieldTypeIdInteger);
            iv00101.AddField("MODIFDT", "Modified Date from item master", Connector.FieldTypeIdDate);
            iv00101.AddField("CREATDDT", "Created Date from item master", Connector.FieldTypeIdDate);
            iv00101.AddField("WRNTYDYS", "Warranty Days", Connector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "PriceLevel from item master", Connector.FieldTypeIdString);
            iv00101.AddField("CGSINFLX", "COGS Inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("CGSMCIDX", "COGS Monetary Correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINFIDX", "Inventory Inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("INVMCIDX", "Inventory Monetary Correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITEMCODE", "Item code", Connector.FieldTypeIdString);
            iv00101.AddField("LASTGENSN", "Last Generated Serial number", Connector.FieldTypeIdString);
            iv00101.AddField("PriceGroup", "Price Group", Connector.FieldTypeIdString);
            iv00101.AddField("PINFLIDX", "Purch Inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURMCIDX", "Purch Monetary Correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PRCHSUOM", "Purchasing U of M", Connector.FieldTypeIdString);
            iv00101.AddField("SELNGUOM", "Selling U of M", Connector.FieldTypeIdString);
            iv00101.AddField("TCC", "Tax Commodity code", Connector.FieldTypeIdString);
            iv00101.AddField("UPPVIDX", "Unrealized Purchase Price Variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("LOCNCODE", "Location Code from item master", Connector.FieldTypeIdString);
            pop10110.AddField("Change_Order_Flag", "Change Order Flag", Connector.FieldTypeIdYesNo);
            svPopLine.AddField("RELEASEBYdate", "Release By date", Connector.FieldTypeIdDate);
            svPopLine.AddField("Released_date", "Released date", Connector.FieldTypeIdDate);
            svPopLine.AddField("Purchase_Item_Tax_Schedu", "Purchase Item Tax Schedule ID", Connector.FieldTypeIdString);
            svPopLine.AddField("Purchase_Site_Tax_Schedu", "Purchase Site Tax Schedule ID", Connector.FieldTypeIdString);
            svPopLine.AddField("BSIVCTTL", "Based On Invoice total", Connector.FieldTypeIdYesNo);
            svPopLine.AddField("TAXAMNT", "Tax Amount from Purchase Line Item", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("ORTAXAMT", "Originating Tax Amount from Purchase Line Item", Connector.FieldTypeIdCurrency);
            svPopLine.AddField("Landed_Cost_Group_ID", "Landed Cost Group ID", Connector.FieldTypeIdString);
            svPopLine.AddField("PLNNDSPPLID", "Planned Supply ID", Connector.FieldTypeIdInteger);
            svPopLine.AddField("SHIPMTHD", "Shipping Method from Purchase Line Item", Connector.FieldTypeIdString);
            svPopLine.AddField("ORIGPRMdate", "Original Promised date", Connector.FieldTypeIdDate);
            svPopLine.AddField("FSTRCPTDT", "First Receipt date", Connector.FieldTypeIdDate);
            svPopLine.AddField("LSTRCPTDT", "Last Receipt date", Connector.FieldTypeIdDate);
            svPopLine.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            svPopLine.AddField("CMPNYNAM", "Company name", Connector.FieldTypeIdString);
            svPopLine.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svPopLine.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svPopLine.AddField("CITY", "City", Connector.FieldTypeIdString);
            svPopLine.AddField("STATE", "State", Connector.FieldTypeIdString);
            svPopLine.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svPopLine.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svPopLine.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            svPopLine.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            svPopLine.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            svPopLine.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            svPopLine.AddField("ADDRSOURCE", "Address source", Connector.FieldTypeIdString);
            svPopLine.AddField("PURCHSITETXSCHSRC", "Purchase Site Tax Schedule source", Connector.FieldTypeIdString);
            svPopLine.AddField("ProjNum", "Project number", Connector.FieldTypeIdString);
            svPopLine.AddField("CostCatID", "Cost Category ID", Connector.FieldTypeIdString);
            pop10500.AddField("QTYINVCD", "Quantity Invoiced", Connector.FieldTypeIdQuantity);
            pop10500.AddField("QTYINVRESERVE", "Quantity Invoice Reserve", Connector.FieldTypeIdQuantity);
            pop10500.AddField("QTYREJ", "Quantity Rejected", Connector.FieldTypeIdQuantity);
            pop10500.AddField("QTYRESERVED", "Quantity Reserved", Connector.FieldTypeIdQuantity);
            pop10500.AddField("QTYMATCH", "Quantity Matched", Connector.FieldTypeIdQuantity);
            pop10500.AddField("QTYSHPPD", "Quantity Shipped", Connector.FieldTypeIdQuantity);
            pop10500.AddField("Total_Landed_Cost_Amount", "Total Landed Cost amount", Connector.FieldTypeIdCurrency);

            var itemTrackingOption = pop10110.AddField("ITMTRKOP", "Item Tracking Option", Connector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
            
            var poLineStatusOrig = pop10110.AddField("PO_Line_Status_Orig", "PO Line Status Orig", Connector.FieldTypeIdEnum);
            poLineStatusOrig.AddListItems(1, new List<string> { "New", "Released", "Change order", "Received", "Closed", "Canceled" });
            
            var rateCalculationMethod = pop10110.AddField("RATECALC", "Rate Calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var freeOnBoard = svPopLine.AddField("FREEONBOARD", "Free On Board", Connector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });
            
            var documentStatus = svPopLine.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var poStatus = svPopLine.AddField("POSTATUS", "PO status", Connector.FieldTypeIdEnum);
            poStatus.AddListItems(1, new List<string> { "New", "Released", "Change order", "Received", "Closed", "Canceled" });

            var rateCalculationMethodFromPurchaseTransactions = svPopLine.AddField("RATECALC", "Rate Calculation Method from Purchase Transactions", Connector.FieldTypeIdEnum);
            rateCalculationMethodFromPurchaseTransactions.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svPopLine.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var statusGroup = svPopLine.AddField("STATGRP", "Status Group", Connector.FieldTypeIdEnum);
            statusGroup.AddListItems(1, new List<string> { "Active", "Closed" });
            
            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor status", Connector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 type", Connector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var maximumInvoiceAmountForVendors = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", Connector.FieldTypeIdEnum);
            maximumInvoiceAmountForVendors.AddListItems(0, new List<string> { "No Maximum", "Amount" });
            
            var postToCashAccountFrom = pm00200.AddField("PTCSHACF", "Post To Cash Account From", Connector.FieldTypeIdEnum);
            postToCashAccountFrom.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", Connector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", Connector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });
            
            var freeOnBoardFromVendorMaster = pm00200.AddField("FREEONBOARD", "Free On Board from vendor master", Connector.FieldTypeIdEnum);
            freeOnBoardFromVendorMaster.AddListItems(1, new List<string> { "None", "Origin", "Destination" });

            var creditLimitTypeFromCustomerMaster = rm00101.AddField("CRLMTTYP", "Credit Limit Type from customer master", Connector.FieldTypeIdEnum);
            creditLimitTypeFromCustomerMaster.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var minimumPaymentTypeFromCustomerMaster = rm00101.AddField("MINPYTYP", "Minimum Payment Type from customer master", Connector.FieldTypeIdEnum);
            minimumPaymentTypeFromCustomerMaster.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });
            
            var financeChargeAmountType = rm00101.AddField("FNCHATYP", "Finance Charge Amount type", Connector.FieldTypeIdEnum);
            financeChargeAmountType.AddListItems(0, new List<string> { "None", "Percent", "Amount" });
            
            var maximumWriteoffType = rm00101.AddField("MXWOFTYP", "Maximum Writeoff type", Connector.FieldTypeIdEnum);
            maximumWriteoffType.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var balanceType = rm00101.AddField("BALNCTYP", "Balance type", Connector.FieldTypeIdEnum);
            balanceType.AddListItems(0, new List<string> { "Open Item", "Balance Forward" });
            
            var statementCycle = rm00101.AddField("STMTCYCL", "Statement Cycle", Connector.FieldTypeIdEnum);
            statementCycle.AddListItems(1, new List<string> { "No Statement", "Weekly", "Biweekly", "Semimonthly", "Monthly", "Bimonthly", "Quarterly" });
            
            var defaultCashAccountType = rm00101.AddField("DEFCACTY", "Default Cash Account type", Connector.FieldTypeIdEnum);
            defaultCashAccountType.AddListItems(0, new List<string> { "Checkbook", "Customer" });
            
            var postResultsToFromCustomerMaster = rm00101.AddField("Post_Results_To", "Post Results To from customer master", Connector.FieldTypeIdEnum);
            postResultsToFromCustomerMaster.AddListItems(0, new List<string> { "Receivables/Discount Account", "Sales Offset Account" });
            
            var poStatusOrig = pop10100.AddField("PO_Status_Orig", "PO Status Orig", Connector.FieldTypeIdEnum);
            poStatusOrig.AddListItems(1, new List<string> { "New", "Released", "Change order", "Received", "Closed", "Canceled" });
            
            var itemType = iv00101.AddField("ITEMTYPE", "Item type", Connector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales Inventory", "Discontinued", "Kit", "Misc Charges", "Services", "Flat Fee" });
            
            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax Options", Connector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var itemTrackingOptionFromItemMaster = iv00101.AddField("ITMTRKOP", "Item Tracking Option from item master", Connector.FieldTypeIdEnum);
            itemTrackingOptionFromItemMaster.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
            
            var valuationMethodFromItemMaster = iv00101.AddField("VCTNMTHD", "Valuation Method from item master", Connector.FieldTypeIdEnum);
            valuationMethodFromItemMaster.AddListItems(1, new List<string> { "FIFO Perpetual", "LIFO Perpetual", "Average Perpetual", "FIFO Periodic", "LIFO Periodic" });
            
            var abcCode = iv00101.AddField("ABCCODE", "ABC code", Connector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });
            
            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS Account source", Connector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From Component Item", "From Kit Item" });
            
            var priceMethod = iv00101.AddField("PRICMTHDw", "Price method", Connector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency amount", "% of List price", "% Markup - Current cost", "% Markup - Standard cost", "% Margin - Current cost", "% Margin - Standard cost" });
            
            var valuationMethod = pop10110.AddField("VCTNMTHD", "Valuation method", Connector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO Perpetual", "LIFO Perpetual", "Average Perpetual", "FIFO Periodic", "LIFO Periodic" });
            
            var purchaseIvItemTaxable = svPopLine.AddField("Purchase_IV_Item_Taxable", "Purchase IV Item Taxable", Connector.FieldTypeIdEnum);
            purchaseIvItemTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

        }

        private ConnectorEntity GetPayablesTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListPayablesTrx, "Payables transactions", ParentConnector);

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
        private static void AddPayablesTransactionEntityFields(ConnectorTable svPmTrx, ConnectorTable pm00200, ConnectorTable pm00201, ConnectorTable mc020103, ConnectorTable pm10000, ConnectorTable pm30200)
        {
            svPmTrx.AddField("VCHRNMBR", "Voucher number", Connector.FieldTypeIdString, true);
            svPmTrx.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            var documentType = svPmTrx.AddField("DOCTYPE", "Document type", Connector.FieldTypeIdEnum, true);
            documentType.AddListItems(1, new List<string> { "Invoice", "Finance Charge", "Misc Charge", "Return", "Credit Memo", "Payment" });
            svPmTrx.AddField("DOCdate", "Document date", Connector.FieldTypeIdDate, true);
            svPmTrx.AddField("DOCNUMBR", "Document number", Connector.FieldTypeIdString, true);
            svPmTrx.AddField("CURTRXAM", "Current Trx amount", Connector.FieldTypeIdCurrency, true);

            svPmTrx.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("DISCAMNT", "Discount amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("DSCDLRAM", "Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svPmTrx.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            svPmTrx.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            svPmTrx.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            svPmTrx.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            svPmTrx.AddField("PORDNMBR", "Purchase Order number", Connector.FieldTypeIdString);
            svPmTrx.AddField("TEN99AMNT", "1099 amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("WROFAMNT", "Write Off amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("DISAMTAV", "Discount Amount Available", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("TRXDSCRN", "Transaction description", Connector.FieldTypeIdString);
            svPmTrx.AddField("UN1099AM", "Unapplied 1099 amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("BKTPURAM", "Backout Purchases amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("BKTFRTAM", "Backout freight amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("BKTMSCAM", "Backout Misc amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("VOIDED", "Voided", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("HOLD", "Hold", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("DINVPDOF", "Date Invoice Paid Off", Connector.FieldTypeIdDate);
            svPmTrx.AddField("PPSAMDED", "PPS Amount Deducted", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("PPSTAXRT", "PPS Tax rate", Connector.FieldTypeIdQuantity);
            svPmTrx.AddField("PGRAMSBJ", "Percent Of Gross Amount Subject", Connector.FieldTypeIdPercentage);
            svPmTrx.AddField("GSTDSAMT", "GST Discount amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            svPmTrx.AddField("PTDUSRID", "Posted User ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            svPmTrx.AddField("MDFUSRID", "Modified User ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("CARDNAME", "Card name", Connector.FieldTypeIdString);
            svPmTrx.AddField("PRCHAMNT", "Purchases amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("MSCCHAMT", "Misc Charges amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("TTLPYMTS", "Total Payments", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svPmTrx.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("PCHSCHID", "Purchase Schedule ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdString);
            svPmTrx.AddField("PSTGdate", "Posting date", Connector.FieldTypeIdDate);
            svPmTrx.AddField("DISAVTKN", "Discount Available Taken", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svPmTrx.AddField("PRCTDISC", "Percent Discount", Connector.FieldTypeIdPercentage);
            svPmTrx.AddField("RETNAGAM", "Retainage amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("SOURCDOC", "Source Document", Connector.FieldTypeIdString);
            pm10000.AddField("VADDCDPR", "Vendor address code - Primary", Connector.FieldTypeIdString);
            pm10000.AddField("CHRGAMNT", "Charge amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("CASHAMNT", "Cash amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("CAMCBKID", "Cash Amount Checkbook ID", Connector.FieldTypeIdString);
            pm10000.AddField("CDOCNMBR", "Cash Document number", Connector.FieldTypeIdString);
            pm10000.AddField("CAMTdate", "Cash Amount date", Connector.FieldTypeIdDate);
            pm10000.AddField("CAMPMTNM", "Cash Amount Payment number", Connector.FieldTypeIdString);
            pm10000.AddField("CHEKAMNT", "Check amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("CHAMCBID", "Check Amount Checkbook ID", Connector.FieldTypeIdString);
            pm10000.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            pm10000.AddField("CAMPYNBR", "Check Amount Payment number", Connector.FieldTypeIdString);
            pm10000.AddField("CRCRDAMT", "Credit Card amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("CCAMPYNM", "Credit Card Amount Payment number", Connector.FieldTypeIdString);
            pm10000.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString);
            pm10000.AddField("CCRCTNUM", "Credit Card Receipt number", Connector.FieldTypeIdString);
            pm10000.AddField("CRCARDDT", "Credit Card date", Connector.FieldTypeIdDate);
            pm10000.AddField("APDSTKAM", "Applied Discount Taken amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("TXENGCLD", "Tax Engine Called", Connector.FieldTypeIdYesNo);
            pm10000.AddField("PMWRKMSG", "PM WORK Messages", Connector.FieldTypeIdString);
            pm10000.AddField("PMDSTMSG", "PM Distribution Messages", Connector.FieldTypeIdString);
            pm10000.AddField("POSTED", "Posted", Connector.FieldTypeIdYesNo);
            pm10000.AddField("APPLDAMT", "Applied amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("PRINTED", "Printed", Connector.FieldTypeIdYesNo);
            pm10000.AddField("ICTRX", "IC TRX", Connector.FieldTypeIdYesNo);
            pm10000.AddField("ICDISTS", "ICDists", Connector.FieldTypeIdYesNo);
            pm10000.AddField("PMICMSGS", "ICMessages", Connector.FieldTypeIdString);
            pm10000.AddField("Tax_date", "Tax date", Connector.FieldTypeIdDate);
            pm30200.AddField("VOIDPdate", "Void GL Posting date", Connector.FieldTypeIdDate);
            mc020103.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            mc020103.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            mc020103.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            mc020103.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            mc020103.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            mc020103.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            mc020103.AddField("ORCTRXAM", "Originating Current Trx amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("OPURAMT", "Originating Purchases amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("OMISCAMT", "Originating Misc Charges amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORCASAMT", "Originating Cash amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORCHKAMT", "Originating Check amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORCHKTTL", "Originating Check total", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORCCDAMT", "Originating Credit Card amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORAPPAMT", "Originating Applied amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORDISTKN", "Originating Discount Taken amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORDATKN", "Originating Discount Available Taken", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORDDLRAT", "Originating Discount Dollar amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORWROFAM", "Originating Write Off amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("OBKPURAMT", "Originating Backout Purchases amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORBKTFRT", "Originating Backout freight amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORBKTMSC", "Originating Backout Misc amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("UNGANLOS", "Unrealized Gain-Loss amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("RMMCERRS", "RM MC Posting Error Messages", Connector.FieldTypeIdString);
            mc020103.AddField("OCHGAMT", "Originating Charge amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORDOCAMT", "Originating Document amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ODISAMTAV", "Originating Discount Amount Available", Connector.FieldTypeIdCurrency);
            mc020103.AddField("ORGAPDISCTKN", "Originating Applied Discount Taken amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("OTOTPAY", "Originating Total Payments", Connector.FieldTypeIdCurrency);
            mc020103.AddField("OR1099AM", "Originating 1099 amount", Connector.FieldTypeIdCurrency);
            mc020103.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            mc020103.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            pm00200.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor Check Name from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short name", Connector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor address code - Primary from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor address code - Purchase Address", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor address code - Ship From", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor address code - Remit To from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor Class ID", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor Contact", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            pm00200.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            pm00200.AddField("CITY", "City", Connector.FieldTypeIdString);
            pm00200.AddField("STATE", "State", Connector.FieldTypeIdString);
            pm00200.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            pm00200.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            pm00200.AddField("PHNUMBR1", "Phone number 1", Connector.FieldTypeIdPhone);
            pm00200.AddField("PHNUMBR2", "Phone number 2", Connector.FieldTypeIdPhone);
            pm00200.AddField("FAXNUMBR", "Fax number", Connector.FieldTypeIdPhone);
            pm00200.AddField("UPSZONE", "UPS zone", Connector.FieldTypeIdString);
            pm00200.AddField("SHIPMTHD", "Shipping Method from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax Schedule ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", Connector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID number", Connector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration number", Connector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", Connector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", Connector.FieldTypeIdPercentage);
            pm00200.AddField("MINORDER", "Minimum order", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMTRMID", "Payment Terms ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("MINPYPCT", "Minimum Payment Percent", Connector.FieldTypeIdPercentage);
            pm00200.AddField("MINPYDLR", "Minimum Payment Dollar", Connector.FieldTypeIdCurrency);
            pm00200.AddField("MAXINDLR", "Maximum Invoice Dollar", Connector.FieldTypeIdCurrency);
            pm00200.AddField("COMMENT1", "Comment1", Connector.FieldTypeIdString);
            pm00200.AddField("COMMENT2", "Comment2", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            pm00200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            pm00200.AddField("CRLMTDLR", "Credit Limit Dollar", Connector.FieldTypeIdCurrency);
            pm00200.AddField("PYMNTPRI", "Payment Priority", Connector.FieldTypeIdString);
            pm00200.AddField("KPCALHST", "Keep Calendar history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            pm00200.AddField("HOLD", "Hold from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("MXWOFAMT", "Maximum Write Off amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("SBPPSDED", "Subject To PPS Deductions", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PPSTAXRT", "PPS Tax Rate from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("DXVARNUM", "Deduction Exemption/Variation number", Connector.FieldTypeIdString);
            pm00200.AddField("CRTCOMDT", "Certificate Commencing date", Connector.FieldTypeIdDate);
            pm00200.AddField("CRTEXPDT", "Certificate Expiration date", Connector.FieldTypeIdDate);
            pm00200.AddField("RTOBUTKN", "Reporting Obligation Undertaken", Connector.FieldTypeIdYesNo);
            pm00200.AddField("XPDTOBLG", "Expiration Date Obligation", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PRSPAYEE", "Prescribed Payee", Connector.FieldTypeIdYesNo);
            pm00200.AddField("PMAPINDX", "Accounts Payable account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMCSHIDX", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDAVIDX", "Discount Available account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMDTKIDX", "Discount Taken account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFINIDX", "Finance Charge account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMMSCHIX", "Misc Charges account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMFRTIDX", "Freight account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTAXIDX", "Tax account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMWRTIDX", "Write Offs account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMPRCHIX", "Purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMRTNGIX", "Retainage account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PMTDSCIX", "Trade Discount account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("ACPURIDX", "Accrued Purchases account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("PURPVIDX", "Purchase Price Variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index from vendor master", Connector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID from vendor master", Connector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date from vendor master", Connector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date from vendor master", Connector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID from vendor master", Connector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of Invoices YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of Invoices LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of Invoices LYR", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of Paid Invoices YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of Paid Invoices LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount Billed YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount Billed LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount Billed LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount Paid YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount Paid LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount Paid LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 Amount YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 Amount LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 Amount LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount Available YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount Available LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount Available LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount Taken YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount Taken LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount Taken LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount Lost YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount Lost LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount Lost LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance Charge LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance Charge LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance Charge YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write Offs YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write Offs LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write Offs LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade Discounts Taken LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade Discounts Taken LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade Discounts Taken YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of Finance Charges LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of Finance Charges LYR", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of Finance Charges YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage Owed", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last Check number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last Check date", Connector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last Check amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last Invoice number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last Invoice amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last Purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First Purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current Unapplied Payment Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid Finance Charges", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days Checks To Clear", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average Days To Pay - Year", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average Days To Pay - Life", Connector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On Order amount", Connector.FieldTypeIdCurrency);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", Connector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", Connector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID", Connector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID", Connector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received from vendor master", Connector.FieldTypeIdYesNo);
            pm00201.AddField("Withholding_LIFE", "Withholding LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WITHLYR", "Withholding LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WITHYTD", "Withholding YTD", Connector.FieldTypeIdCurrency);
            svPmTrx.AddField("APLYWITH", "Apply Withholding", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("CORRCTN", "Correction", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("DocPrinted", "DocPrinted", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("ECTRX", "EC Transaction", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("Electronic", "Electronic", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("PRCHdate", "Purchase date", Connector.FieldTypeIdDate);
            svPmTrx.AddField("SIMPLIFD", "Simplified", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("TaxInvReqd", "Tax Invoice Required", Connector.FieldTypeIdYesNo);
            svPmTrx.AddField("VNDCHKNM", "Vendor Check name", Connector.FieldTypeIdString);
            svPmTrx.AddField("BNKRCAMT", "Bank Receipts amount", Connector.FieldTypeIdCurrency);
            pm10000.AddField("CORRNXST", "Correction to Nonexisting Transaction", Connector.FieldTypeIdYesNo);
            pm10000.AddField("PMWRKMS2", "PM WORK Messages 2", Connector.FieldTypeIdString);
            pm10000.AddField("VCHRNCOR", "Voucher Number Corrected", Connector.FieldTypeIdString);
            svPmTrx.AddField("VADCDTRO", "Vendor address code - Remit To", Connector.FieldTypeIdString);

            var documentStatus = svPmTrx.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "Posted", "History" });
            
            var paymentEntryType = pm30200.AddField("PYENTTYP", "Payment Entry type", Connector.FieldTypeIdEnum);
            paymentEntryType.AddListItems(1, new List<string> { "Check", "Cash", "Credit Card" });
            
            var controlType = svPmTrx.AddField("CNTRLTYP", "Control type", Connector.FieldTypeIdEnum);
            controlType.AddListItems(1, new List<string> { "Voucher", "Payment" });
            
            var rateCalculationMethod = mc020103.AddField("RTCLCMTD", "Rate Calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor status", Connector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });
            
            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 type", Connector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });
            
            var minimumPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment type", Connector.FieldTypeIdEnum);
            minimumPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });

            var minimumInvoiceAmountForVendors = pm00200.AddField("MXIAFVND", "Maximum Invoice Amount For Vendors", Connector.FieldTypeIdEnum);
            minimumInvoiceAmountForVendors.AddListItems(0, new List<string> { "No Maximum", "Amount" });
            
            var postToCashAccountFor = pm00200.AddField("PTCSHACF", "Post To Cash Account From", Connector.FieldTypeIdEnum);
            postToCashAccountFor.AddListItems(0, new List<string> { "Checkbook", "Vendor" });
            
            var creditLimit = pm00200.AddField("CREDTLMT", "Credit Limit", Connector.FieldTypeIdEnum);
            creditLimit.AddListItems(0, new List<string> { "No Credit", "Unlimited", "Amount" });
            
            var writeoff = pm00200.AddField("WRITEOFF", "Writeoff", Connector.FieldTypeIdEnum);
            writeoff.AddListItems(0, new List<string> { "Not Allowed", "Unlimited", "Maximum" });
            
            var postResultsTo = pm00200.AddField("Post_Results_To", "Post Results To", Connector.FieldTypeIdEnum);
            postResultsTo.AddListItems(0, new List<string> { "Payables/Discount Account", "Purchasing Offset Account" });
            
            var freeOnBoard = pm00200.AddField("FREEONBOARD", "Free On Board", Connector.FieldTypeIdEnum);
            freeOnBoard.AddListItems(1, new List<string> { "None", "Origin", "Destination" });

        }

        private ConnectorEntity GetReceivingsTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListReceivingsTrx, "Receivings transactions", ParentConnector);

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
        private static void AddReceivingsTransactionEntityFields(ConnectorTable svReceivingTrx, ConnectorTable pm00201, ConnectorTable pop30300, ConnectorTable pop10306)
        {
            svReceivingTrx.AddField("POPRCTNM", "POP Receipt number", Connector.FieldTypeIdString, true);
            var popType = svReceivingTrx.AddField("POPTYPE", "POP type", Connector.FieldTypeIdEnum, true);
            popType.AddListItems(1, new List<string> { "Standard", "Drop-Ship", "Blanket", "Drop-Ship Blanket" });
            svReceivingTrx.AddField("receiptdate", "Receipt date", Connector.FieldTypeIdDate, true);
            svReceivingTrx.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svReceivingTrx.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString, true);
            svReceivingTrx.AddField("SUBTOTAL", "Subtotal", Connector.FieldTypeIdCurrency, true);

            svReceivingTrx.AddField("VNDDOCNM", "Vendor Document number", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("GLPOSTDT", "GL Posting date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("ACTLSHIP", "Actual Ship date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TRDPCTPR", "Trade Discount Percent (Precise", Connector.FieldTypeIdPercentage);
            svReceivingTrx.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("MISCAMNT", "Misc amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TEN99AMNT", "1099 amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("DSCPCTAM", "Discount Percent amount", Connector.FieldTypeIdPercentage);
            svReceivingTrx.AddField("DSCDLRAM", "Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISAVAMT", "Discount Available amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("POPHDR1", "POP HDR Errors 1", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("POPHDR2", "POP HDR Errors 2", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("POPLNERR", "POP LINE Errors", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("PTDUSRID", "Posted User ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("USER2ENT", "User To Enter", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("VCHRNMBR", "Voucher number", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("Tax_date", "Tax date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svReceivingTrx.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svReceivingTrx.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svReceivingTrx.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svReceivingTrx.AddField("ORSUBTOT", "Originating subtotal", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORMISCAMT", "Originating Misc amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OR1099AM", "Originating 1099 amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDDLRAT", "Originating Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDAVAMT", "Originating Discount Available amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("WITHHAMT", "Withholding amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("SIMPLIFD", "Simplified", Connector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("ECTRX", "EC Transaction", Connector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("TXRGNNUM", "Tax Registration number", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("BSIVCTTL", "Based On Invoice total", Connector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdString);
            svReceivingTrx.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORFRTTAX", "Originating Freight tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORMSCTAX", "Originating Misc tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISCFRGT", "Discount Available Freight", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDAVFRT", "Originating Discount Available Freight", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("DISCMISC", "Discount Available Misc", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("ORDAVMSC", "Originating Discount Available Misc", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("BackoutFreightTaxAmt", "Backout Freight tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OrigBackoutFreightTaxAmt", "Originating Backout Freight tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("BackoutMiscTaxAmt", "Backout Misc tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OrigBackoutMiscTaxAmt", "Originating Backout Misc tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("BCKTXAMT", "Backout tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("OBTAXAMT", "Originating Backout tax amount", Connector.FieldTypeIdCurrency);
            svReceivingTrx.AddField("TaxInvReqd", "Tax Invoice Required", Connector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("TaxInvRecvd", "Tax Invoice Received", Connector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("APLYWITH", "Apply Withholding", Connector.FieldTypeIdYesNo);
            svReceivingTrx.AddField("PPSTAXRT", "PPS Tax rate", Connector.FieldTypeIdInteger);
            svReceivingTrx.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pm00201.AddField("HIESTBAL", "Highest Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("CURRBLNC", "Current Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NOINVYTD", "Number of Invoices YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLIF", "Number of Invoices LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOINVLYR", "Number of Invoices LYR", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPINYTD", "Number of Paid Invoices YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("NOPILIFE", "Number of Paid Invoices LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("AMBLDTYD", "Amount Billed YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLIF", "Amount Billed LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMBLDLYR", "Amount Billed LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDYTD", "Amount Paid YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLIF", "Amount Paid LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("AMTPDLYR", "Amount Paid LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99AYTD", "1099 Amount YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALIF", "1099 Amount LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TEN99ALYR", "1099 Amount LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVYTD", "Discount Available YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLIF", "Discount Available LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISAVLYR", "Discount Available LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKYTD", "Discount Taken YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKNLF", "Discount Taken LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISTKLYR", "Discount Taken LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSYTD", "Discount Lost YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSTLF", "Discount Lost LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DISLSLYR", "Discount Lost LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLIF", "Finance Charge LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHLYR", "Finance Charge LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("FINCHYTD", "Finance Charge YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSYTD", "Write Offs YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLIF", "Write Offs LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("WROFSLYR", "Write Offs LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSYTD", "Returns YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLIF", "Returns LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("RTRNSLYR", "Returns LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTKLIF", "Trade Discounts Taken LIFE", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTLYR", "Trade Discounts Taken LYR", Connector.FieldTypeIdCurrency);
            pm00201.AddField("TRDTYTD", "Trade Discounts Taken YTD", Connector.FieldTypeIdCurrency);
            pm00201.AddField("NFNCHLIF", "Number of Finance Charges LIFE", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHLYR", "Number of Finance Charges LYR", Connector.FieldTypeIdInteger);
            pm00201.AddField("NFNCHYTD", "Number of Finance Charges YTD", Connector.FieldTypeIdInteger);
            pm00201.AddField("RTNGOWED", "Retainage Owed", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTCHNUM", "Last Check number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTCHKDT", "Last Check date", Connector.FieldTypeIdDate);
            pm00201.AddField("LSTCHAMT", "Last Check amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTINNUM", "Last Invoice number", Connector.FieldTypeIdString);
            pm00201.AddField("LSTINVAM", "Last Invoice amount", Connector.FieldTypeIdCurrency);
            pm00201.AddField("LSTPURDT", "Last Purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("FSTPURDT", "First Purchase date", Connector.FieldTypeIdDate);
            pm00201.AddField("CURUNPBN", "Current Unapplied Payment Balance", Connector.FieldTypeIdCurrency);
            pm00201.AddField("UNPDFNCH", "Unpaid Finance Charges", Connector.FieldTypeIdCurrency);
            pm00201.AddField("DYCHTCLR", "Days Checks To Clear", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVGDTPYR", "Average Days To Pay - Year", Connector.FieldTypeIdInteger);
            pm00201.AddField("AVDTPLIF", "Average Days To Pay - Life", Connector.FieldTypeIdInteger);
            pm00201.AddField("ONORDAMT", "On Order Amount from vendor master", Connector.FieldTypeIdCurrency);
            pop10306.AddField("User_Defined_List01", "User defined List01", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List02", "User defined List02", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List03", "User defined List03", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List04", "User defined List04", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_List05", "User defined List05", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text01", "User defined Text01", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text02", "User defined Text02", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text03", "User defined Text03", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text04", "User defined Text04", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text05", "User defined Text05", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text06", "User defined Text06", Connector.FieldTypeIdString); 
            pop10306.AddField("User_Defined_Text07", "User defined Text07", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text08", "User defined Text08", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text09", "User defined Text09", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Text10", "User defined Text10", Connector.FieldTypeIdString);
            pop10306.AddField("User_Defined_Date01", "User defined Date01", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date02", "User defined Date02", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date03", "User defined Date03", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date04", "User defined Date04", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date05", "User defined Date05", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date06", "User defined Date06", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date07", "User defined Date07", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date08", "User defined Date08", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date09", "User defined Date09", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date10", "User defined Date10", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date11", "User defined Date11", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date12", "User defined Date12", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date13", "User defined Date13", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date14", "User defined Date14", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date15", "User defined Date15", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date16", "User defined Date16", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date17", "User defined Date17", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date18", "User defined Date18", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date19", "User defined Date19", Connector.FieldTypeIdDate);
            pop10306.AddField("User_Defined_Date20", "User defined Date20", Connector.FieldTypeIdDate);
            svReceivingTrx.AddField("VADCDTRO", "Vendor address code - Remit To", Connector.FieldTypeIdString);

            var voidStatus = pop30300.AddField("VOIDSTTS", "Void status", Connector.FieldTypeIdEnum);
            voidStatus.AddListItems(0, new List<string> { "Normal", "Voided" });
            
            var rateCalculationMethod = svReceivingTrx.AddField("RATECALC", "Rate Calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svReceivingTrx.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var purchaseFreightTaxable = svReceivingTrx.AddField("Purchase_Freight_Taxable", "Purchase Freight Taxable", Connector.FieldTypeIdEnum);
            purchaseFreightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var purchaseMiscTaxable = svReceivingTrx.AddField("Purchase_Misc_Taxable", "Purchase Misc Taxable", Connector.FieldTypeIdEnum);
            purchaseMiscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
            
            var postingStatus = svReceivingTrx.AddField("ASI_Document_Status", "Posting status", Connector.FieldTypeIdEnum);
            postingStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });

        }

        private ConnectorEntity GetReceivingsTransactionLineEntity()
        {
            var entity = new ConnectorEntity(GpSmartListReceivingsLineItems, "Receivings transaction line items", ParentConnector);

            var svReceivingLine = entity.AddTable("svReceivingLine");

            AddReceivingsTransactionLineEntityFields(svReceivingLine);

            return entity;
        }
        private static void AddReceivingsTransactionLineEntityFields(ConnectorTable svReceivingLine)
        {
            svReceivingLine.AddField("POPRCTNM", "POP Receipt number", Connector.FieldTypeIdString, true);
            var popType = svReceivingLine.AddField("POPTYPE", "POP type", Connector.FieldTypeIdEnum, true);
            popType.AddListItems(1, new List<string> { "Standard", "Drop-Ship", "Blanket", "Drop-Ship Blanket" });
            svReceivingLine.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svReceivingLine.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            svReceivingLine.AddField("QTYSHPPD", "Quantity Shipped", Connector.FieldTypeIdQuantity, true);
            svReceivingLine.AddField("QTYINVCD", "Quantity Invoiced", Connector.FieldTypeIdQuantity, true);
            svReceivingLine.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency, true);
            svReceivingLine.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency, true);
            svReceivingLine.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svReceivingLine.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString, true);

            svReceivingLine.AddField("RCPTLNNM", "Receipt Line number", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("PONUMBER", "PO number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("QTYREJ", "Quantity Rejected", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("Landed_Cost_Group_ID", "Landed Cost Group ID", Connector.FieldTypeIdString);
            svReceivingLine.AddField("Landed_Cost", "Landed cost", Connector.FieldTypeIdYesNo);
            svReceivingLine.AddField("RCPTRETNUM", "Receipt Return number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("INVRETNUM", "Invoice Return number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("Total_Landed_Cost_Amount", "Total Landed cost", Connector.FieldTypeIdCurrency);
            svReceivingLine.AddField("QTYRESERVED", "Quantity Reserved", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("QTYINVRESERVE", "Quantity Invoice Reserve", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("VNDITDSC", "Vendor Item description", Connector.FieldTypeIdString);
            svReceivingLine.AddField("UMQTYINB", "U of M QTY In Base", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("ACTLSHIP", "Actual Ship date", Connector.FieldTypeIdDate);
            svReceivingLine.AddField("COMMNTID", "Comment ID", Connector.FieldTypeIdString);
            svReceivingLine.AddField("INVINDX", "Inventory index", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svReceivingLine.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svReceivingLine.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString);
            svReceivingLine.AddField("NONINVEN", "Non IV", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("DECPLCUR", "Decimal Places Currency", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("DECPLQTY", "Decimal Places QTYS", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            svReceivingLine.AddField("JOBNUMBR", "Job number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("COSTCODE", "Cost code", Connector.FieldTypeIdString);
            svReceivingLine.AddField("COSTTYPE", "Cost Code type", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("ORUNTCST", "Originating Unit cost", Connector.FieldTypeIdCurrency);
            svReceivingLine.AddField("OREXTCST", "Originating Extended cost", Connector.FieldTypeIdCurrency);
            svReceivingLine.AddField("ODECPLCU", "Originating Decimal Places Currency", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("BOLPRONUMBER", "BOL_PRO number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("Capital_Item", "Capital Item", Connector.FieldTypeIdYesNo);
            svReceivingLine.AddField("Product_Indicator", "Product Indicator", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("Purchase_IV_Item_Taxable", "Purchase IV Item Taxable", Connector.FieldTypeIdInteger);
            svReceivingLine.AddField("Purchase_Item_Tax_Schedu", "Purchase Item Tax Schedule ID", Connector.FieldTypeIdString);
            svReceivingLine.AddField("Purchase_Site_Tax_Schedu", "Purchase Site Tax Schedule ID", Connector.FieldTypeIdString);
            svReceivingLine.AddField("BSIVCTTL", "Based On Invoice total", Connector.FieldTypeIdYesNo);
            svReceivingLine.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svReceivingLine.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svReceivingLine.AddField("PURPVIDX", "Purchase Price Variance index", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svReceivingLine.AddField("VNDDOCNM", "Vendor Document number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("receiptdate", "Receipt date", Connector.FieldTypeIdDate);
            svReceivingLine.AddField("GLPOSTDT", "GL Posting date", Connector.FieldTypeIdDate);
            svReceivingLine.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            svReceivingLine.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            svReceivingLine.AddField("PTDUSRID", "Posted User ID", Connector.FieldTypeIdString);
            svReceivingLine.AddField("USER2ENT", "User To Enter", Connector.FieldTypeIdString);
            svReceivingLine.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            svReceivingLine.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            svReceivingLine.AddField("Tax_date", "Tax date", Connector.FieldTypeIdDate);
            svReceivingLine.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svReceivingLine.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("QTYMATCH", "Quantity Matched", Connector.FieldTypeIdQuantity);
            svReceivingLine.AddField("ProjNum", "Project number", Connector.FieldTypeIdString);
            svReceivingLine.AddField("CostCatID", "Cost Category ID", Connector.FieldTypeIdString);

            var itemTrackingOption = svReceivingLine.AddField("ITMTRKOP", "Item Tracking Option", Connector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
            
            var postingStatus = svReceivingLine.AddField("PSTGSTUS", "Posting status", Connector.FieldTypeIdEnum);
            postingStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });
            
            var rateCalculationMethod = svReceivingLine.AddField("RATECALC", "Rate Calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
        }

    }
}
