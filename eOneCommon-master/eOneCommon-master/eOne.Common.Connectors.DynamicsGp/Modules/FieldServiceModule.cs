using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class FieldServiceModule : DynamicsGpModule
    {

        private const short FieldServiceSmartListFieldServiceCalls = 26005;
        private const short FieldServiceSmartListEquipment = 26006;
        private const short FieldServiceSmartListContracts = 26007;
        private const short FieldServiceSmartListContractLines = 26008;
        private const short FieldServiceSmartListRma = 26009;
        private const short FieldServiceSmartListRtv = 26010;
        private const short FieldServiceSmartListWorkOrders = 26011;
        private const short FieldServiceSmartListHistFieldServiceCalls = 26012;
        private const short FieldServiceSmartListHistContracts = 26013;
        private const short FieldServiceSmartListHistContractLines = 26014;
        private const short FieldServiceSmartListRmaLines = 26015;
        private const short FieldServiceSmartListHistRma = 26016;
        private const short FieldServiceSmartListHistRmaLines = 26017;
        private const short FieldServiceSmartListHistRtv = 26018;
        private const short FieldServiceSmartListRtvLines = 26019;
        private const short FieldServiceSmartListHistRtvLines = 26020;
        
        public FieldServiceModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 949;
            Name = "Field service";
            Installed = true;
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetServiceCallEntity());
            Entities.Add(GetEquipmentEntity());
            Entities.Add(GetContractEntity());
            Entities.Add(GetContractLineEntity());
            Entities.Add(GetRmaEntity());
            Entities.Add(GetRtvEntity());
            Entities.Add(GetWorkOrderEntity());
            Entities.Add(GetHistoricalServiceCallEntity());
            Entities.Add(GetHistoricalContractEntity());
            Entities.Add(GetHistoricalContractLineEntity());
            Entities.Add(GetRmaLineEntity());
            Entities.Add(GetHistoricalRmaEntity());
            Entities.Add(GetHistoricalRmaLineEntity());
            Entities.Add(GetHistoricalRtvEntity());
            Entities.Add(GetRtvLineEntity());
            Entities.Add(GetHistoricalRtvLineEntity());
        }

        public DataConnectorEntity GetServiceCallEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListFieldServiceCalls), "Service calls", ParentConnector);

            var svc00200 = entity.AddTable("SVC00200");

            var svc00202 = entity.AddScript("(select SRVRECTYPE, CALLNBR, EQUIPID from {0}..SVC00202 with (NOLOCK) where EQPLINE = 1)", "SVC00202", "SVC00200");
            svc00202.AddJoinFields("SRVRECTYPE", "SRVRECTYPE");
            svc00202.AddJoinFields("CALLNBR", "CALLNBR");

            var svc00300 = entity.AddTable("SVC00300", "SVC00202");
            svc00300.AddJoinFields("EQUIPID", "EQUIPID");

            AddServiceCallEntityFields(svc00200, svc00300);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddServiceCallEntityFields(DataConnectorTable svc00200, DataConnectorTable svc00300)
        {
            svc00200.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString, true);
            var serviceRecordType = svc00200.AddField("SRVRECTYPE", "Service Record Type", DataConnector.FieldTypeIdEnum, true);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
            svc00200.AddField("SRVSTAT", "Service Call Status", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("SRVTYPE", "Service Type", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("SVCDESCR", "Service Description", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("priorityLevel", "priorityLevel", DataConnector.FieldTypeIdInteger, true);
            svc00200.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("Customer_Reference", "Customer Reference", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("CUSTNAME", "Name", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("SVCAREA", "Service Area", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("TECHID", "Tech ID", DataConnector.FieldTypeIdString, true);
            svc00200.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc00200.AddField("TOTAL", "Total", DataConnector.FieldTypeIdCurrency, true);
            svc00200.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc00200.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc00200.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc00200.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc00200.AddField("ZIP", "Zip", DataConnector.FieldTypeIdString);
            svc00200.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            svc00200.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            svc00200.AddField("TIMEZONE", "TimeZone", DataConnector.FieldTypeIdString);
            svc00200.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("Notify_Date", "Notify Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("Notify_Time", "Notify Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("ETADTE", "ETA Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("ETATME", "ETA Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("DISPDTE", "Dispatch Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("DISPTME", "Dispatch Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("ARRIVDTE", "Arrival Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("ARRIVTME", "Arrival Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("Response_Date", "Response Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("Response_Time", "Response Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("PRICELVL", "Price Level", DataConnector.FieldTypeIdString);
            svc00200.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("LABSTOTPRC", "Labor Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("LABPCT", "Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc00200.AddField("LABSTOTCST", "Labor Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("PARSTOTPRC", "Part Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("PARTPCT", "Part Pct", DataConnector.FieldTypeIdPercentage);
            svc00200.AddField("PARSTOTCST", "Part Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("MSCSTOTPRC", "Misc Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("MISCPCT", "Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc00200.AddField("MISSTOTCST", "Misc Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svc00200.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svc00200.AddField("PRETAXTOT", "PreTax Total", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("Invoiced_Amount", "Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("METER1", "Meter 1", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("METER2", "Meter 2", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("METER3", "Meter 3", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc00200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("NOTFYFLAG", "Notify Flag", DataConnector.FieldTypeIdYesNo);
            svc00200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc00200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc00200.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc00200.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc00200.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc00200.AddField("MSTRCALLNBR", "Master Service Call Number", DataConnector.FieldTypeIdString);
            svc00200.AddField("ESCDATE", "Escalation Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("ESCTIME", "Escalation Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("Escalation_Level", "Escalation Level", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("SPLTTERMS", "Split Terms Code", DataConnector.FieldTypeIdString);
            svc00200.AddField("Callback", "Callback", DataConnector.FieldTypeIdYesNo);
            svc00200.AddField("PROJCTID", "Project ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("ProjectRef1_1", "ProjectRef1", DataConnector.FieldTypeIdString);
            svc00200.AddField("CONTNBR", "Contract Number", DataConnector.FieldTypeIdString);
            svc00200.AddField("SVC_Contract_Line_SEQ", "Contract Line Sequence Number", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ETTR", "ETTR", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("SVC_On_Hold", "On Hold", DataConnector.FieldTypeIdYesNo);
            svc00200.AddField("Print_to_Web", "Print to Web", DataConnector.FieldTypeIdYesNo);
            svc00200.AddField("Meters_1", "Meters", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc00200.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc00200.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc00200.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc00200.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc00200.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc00200.AddField("ORIGMISSTOTCST", "Originating Misc Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGMSCSTOTPRC", "Originating Misc Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGLABSUBTOTCOST", "Originating Labor Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGLABSTOTPRC", "Originating Labor Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGPARSTOTCST", "Originating Part Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGPARSTOTPRC", "Originating Part Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGPRETAXTOT", "Originating PreTax Total", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGTOTAL", "Originating Service Total", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("Orig_Invoiced_Amount", "Originating Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc00200.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc00200.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc00200.AddField("SVC_Pre600", "SVC_Pre600", DataConnector.FieldTypeIdYesNo);
            svc00200.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            svc00200.AddField("Replaces_1", "Replaces", DataConnector.FieldTypeIdString);
            svc00300.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString);
            svc00300.AddField("SERLNMBR", "Equipment/Serial Number", DataConnector.FieldTypeIdString);

            var masterServiceRecordType = svc00200.AddField("MSTRRECTYPE", "Master Service Record Type", DataConnector.FieldTypeIdEnum);
            masterServiceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var contractRecordType = svc00200.AddField("CONSTS", "Contract Record Type", DataConnector.FieldTypeIdEnum);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
        }

        public DataConnectorEntity GetEquipmentEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListEquipment), "Equipment", ParentConnector);

            var svc00300 = entity.AddTable("SVC00300");

            var rm00101 = entity.AddTable("RM00101", "SVC00300");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddEquipmentEntityFields(svc00300, rm00101);

            return entity;
        }
        public void AddEquipmentEntityFields(DataConnectorTable svc00300, DataConnectorTable rm00101)
        {
            svc00300.AddField("EQUIPID", "Equipment ID", DataConnector.FieldTypeIdInteger, true);
            svc00300.AddField("SERLNMBR", "Serial Number", DataConnector.FieldTypeIdString, true);
            svc00300.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svc00300.AddField("SRLSTAT", "Equipment Status", DataConnector.FieldTypeIdString, true);
            svc00300.AddField("INSTDTE", "Install Date", DataConnector.FieldTypeIdDate, true);
            svc00300.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc00300.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc00300.AddField("SVC_Serial_ID", "Serial ID", DataConnector.FieldTypeIdString, true);
            svc00300.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity, true);
            svc00300.AddField("SVC_Asset_Tag", "Asset Tag", DataConnector.FieldTypeIdString,true);
            svc00300.AddField("Shipped_Date", "Shipped Date", DataConnector.FieldTypeIdDate, true);
            svc00300.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svc00300.AddField("Version", "Version", DataConnector.FieldTypeIdString);
            svc00300.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc00300.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc00300.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc00300.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc00300.AddField("ZIP", "Zip", DataConnector.FieldTypeIdString);
            svc00300.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc00300.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            svc00300.AddField("LSTPMDTE", "Last PM Date", DataConnector.FieldTypeIdDate);
            svc00300.AddField("LSTSRVDTE", "Last Service Date", DataConnector.FieldTypeIdDate);
            svc00300.AddField("TECHID", "Tech ID", DataConnector.FieldTypeIdString);
            svc00300.AddField("TECHID2", "Tech ID 2", DataConnector.FieldTypeIdString);
            svc00300.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString);
            svc00300.AddField("SVCAREA", "Service Area", DataConnector.FieldTypeIdString);
            svc00300.AddField("TIMEZONE", "TimeZone", DataConnector.FieldTypeIdString);
            svc00300.AddField("WARRCDE", "Warranty Code", DataConnector.FieldTypeIdString);
            svc00300.AddField("WARREND", "Warranty End", DataConnector.FieldTypeIdDate);
            svc00300.AddField("WARRSTART", "Warranty Start", DataConnector.FieldTypeIdDate);
            svc00300.AddField("SLRWARR", "Seller Warranty Code", DataConnector.FieldTypeIdString);
            svc00300.AddField("SLRWEND", "Seller Warranty End", DataConnector.FieldTypeIdDate);
            svc00300.AddField("SLRWSTART", "Seller Warranty Start", DataConnector.FieldTypeIdDate);
            svc00300.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("MTTR", "MTTR", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("MTBF", "MTBF", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("MTBI", "MTBI", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("Last_Calc_Date", "Last Calc Date", DataConnector.FieldTypeIdDate);
            svc00300.AddField("Meters_1", "Meters", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("Dailys_1", "Dailys", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("MTBFs_1", "MTBFs", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("Meter_Deltas_1", "Meter Deltas", DataConnector.FieldTypeIdInteger);
            var pmMonth = svc00300.AddField("SVC_PM_Date", "PM Month", DataConnector.FieldTypeIdEnum);
            pmMonth.AddListItems(1, new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            svc00300.AddField("SVC_PM_Day", "PM Day", DataConnector.FieldTypeIdInteger);
            svc00300.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString);
            svc00300.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc00300.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc00300.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc00300.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc00300.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc00300.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc00300.AddField("SVC_Register_Date", "Register Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
        }

        public DataConnectorEntity GetContractEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListContracts), "Contracts", ParentConnector);

            var svc00600 = entity.AddTable("SVC00600");

            var rm00101 = entity.AddTable("RM00101", "SVC00600");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddContractEntityFields(svc00600, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddContractEntityFields(DataConnectorTable svc00600, DataConnectorTable rm00101)
        {
            var contractRecordType = svc00600.AddField("CONSTS", "Contract Record Type", DataConnector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc00600.AddField("CONTNBR", "Contract Number", DataConnector.FieldTypeIdString, true);
            svc00600.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc00600.AddField("BILSTRT", "Bill Start", DataConnector.FieldTypeIdDate, true);
            svc00600.AddField("BILEND", "Bill End", DataConnector.FieldTypeIdDate, true);
            svc00600.AddField("BILLNGTH", "Bill Length", DataConnector.FieldTypeIdInteger, true);
            var billPeriod = svc00600.AddField("BILPRD", "Bill Period", DataConnector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc00600.AddField("TOTAL", "Total", DataConnector.FieldTypeIdCurrency, true);
            svc00600.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc00600.AddField("BILONDY", "Bill On Day", DataConnector.FieldTypeIdInteger, true);
            var billingCycle = svc00600.AddField("BILCYC", "Billing Cycle", DataConnector.FieldTypeIdEnum, true); 
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc00600.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate, true);
            svc00600.AddField("ENDDATE", "End Date", DataConnector.FieldTypeIdDate, true);
            var liabilityType = svc00600.AddField("SVC_Liability_Type", "Liability Type", DataConnector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block Time", "Retainage", "Based on Calls", "Metered" });
            svc00600.AddField("Contract_Transfer_Date", "Contract Transfer Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("priorityLevel", "priorityLevel", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("TIMEZONE", "TimeZone", DataConnector.FieldTypeIdString);
            svc00600.AddField("CONTPRC", "Contract Price", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("RENPRCSCHD", "Renewing Price Schedule", DataConnector.FieldTypeIdString);
            svc00600.AddField("PCTCRYFWD", "Percentage Carried Forward", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("FRZEND", "Frozen End", DataConnector.FieldTypeIdDate);
            svc00600.AddField("FRXSTRT", "Frozen Start", DataConnector.FieldTypeIdDate);
            svc00600.AddField("MXINCPCT", "Max Increase Percentage", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("BLKTIM", "Blocked Time", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("VALTIM", "Value of Time", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("COMDLRAM", "Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc00600.AddField("PRCSTAT", "Status of Price", DataConnector.FieldTypeIdString);
            svc00600.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc00600.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            svc00600.AddField("PARTPCT", "Part Pct", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("LABPCT", "Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("MISCPCT", "Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("PMMSCPCT", "PM Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("PMPRTPCT", "PM Part Pct", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("PMLABPCT", "PM Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("RETNAGAM", "Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("RTNBILLD", "Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("COMMCODE", "Commission Code", DataConnector.FieldTypeIdString);
            svc00600.AddField("COMPRCNT", "Commission Percent", DataConnector.FieldTypeIdPercentage);
            svc00600.AddField("FRSTBLDTE", "First Bill Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("Last_Amount_Billed", "Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("LSTBLDTE", "Last Bill Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("NBRCAL", "Max Number of Calls", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("ACTCAL", "Actual Number of Calls", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("TOTVALCAL", "Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc00600.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("NXTBLDTE", "Next Bill Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("CNTTYPE", "Contract Type", DataConnector.FieldTypeIdString);
            svc00600.AddField("PRICSHED", "Price Schedule", DataConnector.FieldTypeIdString);
            svc00600.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("MINBIL", "Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("MAXBIL", "Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("MAXBILL", "Max Billable", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("AUTOREN", "Auto Renewing", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("MSTCNTRCT", "Master Contract Number", DataConnector.FieldTypeIdString);
            svc00600.AddField("SRVTYPE", "Service Type", DataConnector.FieldTypeIdString);
            svc00600.AddField("BILFRRET", "Bill For Retainer", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("TOTBIL", "Total Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("PREPAID", "Pre Paid", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svc00600.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc00600.AddField("Contract_Length", "Contract Length", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("Invoiced_Amount", "Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Liabiltiy_Reduction", "Liability Reduction", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("Amount_To_Invoice", "Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Liability_Amount", "Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Total_Liability_Amount", "Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("NUMOFINV", "Number of Invoices", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("Quote_Status", "Quote Status", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("QUOEXPDA", "Quote Expiration Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("Credit_Hold", "Credit Hold", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svc00600.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svc00600.AddField("New_PO_Required", "New PO Required", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", DataConnector.FieldTypeIdString);
            svc00600.AddField("Source_Contract_Number", "Source Contract Number", DataConnector.FieldTypeIdString);
            svc00600.AddField("Contract_Response_Time", "Contract Response Time", DataConnector.FieldTypeIdString);
            svc00600.AddField("Liability_Months", "Liability Months", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("Next_Liability_Date", "Next Liability Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("Last_Liability_Date", "Last Liability Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("Total_Liability_Billed", "Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Total_Unit", "Total Unit", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Created_User_ID", "SVC_Created User ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("Source_User_ID", "Source User ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", DataConnector.FieldTypeIdDate);
            svc00600.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString);
            svc00600.AddField("Location_Segment", "Location Segment", DataConnector.FieldTypeIdString);
            svc00600.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("SVC_Invoice_Detail", "Invoice Detail", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc00600.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc00600.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc00600.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc00600.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc00600.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc00600.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("SVC_Paid_Contract", "Paid Contract", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_Discount_Recognized", "Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("SVC_Discount_Remaining", "Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("SVC_Discount_Next", "Discount Next", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc00600.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc00600.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc00600.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc00600.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("ORIGVALTIM", "Originating Value of Time", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORCOMAMT", "Originating Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGRETNAGAM", "Originating Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGRTNBILLD", "Originating Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGTOTAL", "Originating Service Total", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGMINBIL", "Originating Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGMAXBIL", "Originating Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Originating_Max_Billable", "Originating Max Billable", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGTOTBIL", "Originating Total Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_Invoiced_Amount", "Originating Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_Liability_Amount", "Originating Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("OrigTotLiabilityAmount", "Originating Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Originating_Total_Unit", "Originating Total Unit", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("OrigDiscountReceived", "Originating Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("OrigDiscountRemaining", "Originating Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("OrigDiscountNext", "Originating Discount Next", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("SmoothInvoiceCalc", "Smooth Invoice Calculation", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("SmoothRevenueCalc", "Smooth Revenue Calculation", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_Use_Same_Number", "Use Same Number", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            svc00600.AddField("SVC_Invoiced_Cost", "Invoiced Cost", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_SVC_Invoiced_Cost", "Originating SVC Invoiced Cost", DataConnector.FieldTypeIdCurrency);
            svc00600.AddField("AUTOREN", "Auto Renewing", DataConnector.FieldTypeIdString);
            svc00600.AddField("SVC_New_Contract_Number", "New Contract Number", DataConnector.FieldTypeIdString);
            svc00600.AddField("SVC_Evergreen_Contract", "Evergreen Contract", DataConnector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_Evergreen_RenewLimit", "Evergreen Renew Limit", DataConnector.FieldTypeIdInteger);
            svc00600.AddField("SVC_Evergreen_Renewals", "Evergreen Renewals", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var contractTransferStatus = svc00600.AddField("Contract_Transfer_Status", "Contract Transfer Status", DataConnector.FieldTypeIdEnum); 
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });
            
            var renewingContractType = svc00600.AddField("RENCNTTYP", "Renewing Contract Type", DataConnector.FieldTypeIdEnum);
            renewingContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            
            var billingStatus = svc00600.AddField("BILSTAT", "Billing Status", DataConnector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });
            
            var contractPeriod = svc00600.AddField("Contract_Period", "Contract Period", DataConnector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            
            var sourceContractType = svc00600.AddField("Source_Contract_Type", "Source Contract Type", DataConnector.FieldTypeIdEnum);
            sourceContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });

        }

        public DataConnectorEntity GetContractLineEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListContractLines), "Contract lines", ParentConnector);

            var svc00601 = entity.AddTable("SVC00601");

            var rm00101 = entity.AddTable("RM00101", "SVC00601");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddContractLineEntityFields(svc00601, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddContractLineEntityFields(DataConnectorTable svc00601, DataConnectorTable rm00101)
        {
            var contractRecordType = svc00601.AddField("CONSTS", "Contract Record Type", DataConnector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc00601.AddField("CONTNBR", "Contract Number", DataConnector.FieldTypeIdString, true);
            svc00601.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger, true);
            svc00601.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc00601.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity, true);
            svc00601.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svc00601.AddField("EQUIPID", "Equipment ID", DataConnector.FieldTypeIdInteger, true);
            svc00601.AddField("SERLNMBR", "Serial Number", DataConnector.FieldTypeIdString, true);
            svc00601.AddField("BILSTRT", "Bill Start", DataConnector.FieldTypeIdDate, true);
            svc00601.AddField("BILEND", "Bill End", DataConnector.FieldTypeIdDate, true);
            svc00601.AddField("BILLNGTH", "Bill Length", DataConnector.FieldTypeIdInteger, true);
            var billPeriod = svc00601.AddField("BILPRD", "Bill Period", DataConnector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc00601.AddField("TOTAL", "Total", DataConnector.FieldTypeIdCurrency, true);
            svc00601.AddField("CNTTYPE", "Contract Type", DataConnector.FieldTypeIdString, true);
            svc00601.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc00601.AddField("SRVTYPE", "Service Type", DataConnector.FieldTypeIdString, true);
            svc00601.AddField("TOTBIL", "Total Billed", DataConnector.FieldTypeIdCurrency, true);
            svc00601.AddField("PREPAID", "Pre Paid", DataConnector.FieldTypeIdYesNo, true);
            svc00601.AddField("BILONDY", "Bill On Day", DataConnector.FieldTypeIdInteger, true);
            var billingCycle = svc00601.AddField("BILCYC", "Billing Cycle", DataConnector.FieldTypeIdEnum, true);
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc00601.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate, true);
            svc00601.AddField("ENDDATE", "End Date", DataConnector.FieldTypeIdDate, true);
            svc00601.AddField("Unit_Cost_Total", "Unit Cost Total", DataConnector.FieldTypeIdCurrency, true);
            var liabilityType = svc00601.AddField("SVC_Liability_Type", "Liability Type", DataConnector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block Time", "Retainage", "Based on Calls", "Metered" });

            svc00601.AddField("Contract_Transfer_Date", "Contract Transfer Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("CNTPRCOVR", "Contract Price Overridden", DataConnector.FieldTypeIdYesNo);
            svc00601.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("CONFGREF", "Config Reference", DataConnector.FieldTypeIdString);
            svc00601.AddField("FRZEND", "Frozen End", DataConnector.FieldTypeIdDate);
            svc00601.AddField("FRXSTRT", "Frozen Start", DataConnector.FieldTypeIdDate);
            svc00601.AddField("BLKTIM", "Blocked Time", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("VALTIM", "Value of Time", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("COMDLRAM", "Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc00601.AddField("PRCSTAT", "Status of Price", DataConnector.FieldTypeIdString);
            svc00601.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc00601.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            svc00601.AddField("PARTPCT", "Part Pct", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("LABPCT", "Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("MISCPCT", "Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("PMMSCPCT", "PM Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("PMPRTPCT", "PM Part Pct", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("PMLABPCT", "PM Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString);
            svc00601.AddField("RETNAGAM", "Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("RTNBILLD", "Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svc00601.AddField("COMMCODE", "Commission Code", DataConnector.FieldTypeIdString);
            svc00601.AddField("COMPRCNT", "Commission Percent", DataConnector.FieldTypeIdPercentage);
            svc00601.AddField("FRSTBLDTE", "First Bill Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("Last_Amount_Billed", "Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("LSTBLDTE", "Last Bill Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("NBRCAL", "Max Number of Calls", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("ACTCAL", "Actual Number of Calls", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("TOTVALCAL", "Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc00601.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc00601.AddField("NXTBLDTE", "Next Bill Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("PRICSHED", "Price Schedule", DataConnector.FieldTypeIdString);
            svc00601.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("MINBIL", "Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("MAXBIL", "Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("MAXBILL", "Max Billable", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("AUTOREN", "Auto Renewing", DataConnector.FieldTypeIdYesNo);
            svc00601.AddField("priorityLevel", "priorityLevel", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("MSTCNTRCT", "Master Contract Number", DataConnector.FieldTypeIdString);
            svc00601.AddField("BILFRRET", "Bill For Retainer", DataConnector.FieldTypeIdYesNo);
            svc00601.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svc00601.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc00601.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svc00601.AddField("CNFGLVL", "Config Level", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("CNFGSEQ", "Config Sequence", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("Contract_Length", "Contract Length", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("Invoiced_Amount", "Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Amount_To_Invoice", "Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Liabiltiy_Reduction", "Liability Reduction", DataConnector.FieldTypeIdYesNo);
            svc00601.AddField("Liability_Amount", "Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Total_Liability_Amount", "Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("NUMOFINV", "Number of Invoices", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("New_Invoice_Amount", "New Invoice Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Contract_Line_Status", "Contract Line Status", DataConnector.FieldTypeIdString);
            svc00601.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", DataConnector.FieldTypeIdString);
            svc00601.AddField("Contract_Response_Time", "Contract Response Time", DataConnector.FieldTypeIdString);
            svc00601.AddField("Liability_Months", "Liability Months", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("Next_Liability_Date", "Next Liability Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("Last_Liability_Date", "Last Liability Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("Total_Liability_Billed", "Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svc00601.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svc00601.AddField("Total_Unit", "Total Unit", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", DataConnector.FieldTypeIdDate);
            svc00601.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString);
            svc00601.AddField("SVC_Monthly_Price", "Monthly_Price", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Location_Segment", "Location Segment", DataConnector.FieldTypeIdString);
            svc00601.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc00601.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc00601.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Invoiced_Discount", "Invoiced Discount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Discount_Recognized", "Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Discount_Remaining", "Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Discount_Next", "Discount Next", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_PM_Day", "PM Day", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc00601.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc00601.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc00601.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            
            svc00601.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc00601.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc00601.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc00601.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc00601.AddField("ORIGVALTIM", "Originating Value of Time", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORCOMAMT", "Originating Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGRETNAGAM", "Originating Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGRTNBILLD", "Originating Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGTOTAL", "Originating Service Total", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGMINBIL", "Originating Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGMAXBIL", "Originating Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Originating_Max_Billable", "Originating Max Billable", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGTOTBIL", "Originating Total Billed", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Orig_Invoiced_Amount", "Originating Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Orig_Liability_Amount", "Originating Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("OrigTotLiabilityAmount", "Originating Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("Originating_Total_Unit", "Originating Total Unit", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORUNTCST", "Originating Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("OrigDiscountReceived", "Originating Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("OrigDiscountRemaining", "Originating Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("OrigDiscountNext", "Originating Discount Next", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGUNITCOSTTOTAL", "Originating Unit Cost Total", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGMONTHPRICE", "Originating Monthly Price", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGINVOICEDDISC", "Originating Invoiced Discount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("OrigNewInvoiceAmount", "Originating New Invoice Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("RM00101.CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var contractTransferStatus = svc00601.AddField("Contract_Transfer_Status", "Contract Transfer Status", DataConnector.FieldTypeIdEnum);
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });
            
            var billingStatus = svc00601.AddField("BILSTAT", "Billing Status", DataConnector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });
            
            var contractPeriod = svc00601.AddField("Contract_Period", "Contract Period", DataConnector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            
            var pmMonth = svc00601.AddField("SVC_PM_Date", "PM Month", DataConnector.FieldTypeIdEnum);
            pmMonth.AddListItems(1, new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });

        }
            
        public DataConnectorEntity GetRmaEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListRma), "RMAs", ParentConnector);

            var svc05000 = entity.AddTable("SVC05000");
            AddRmaEntityFields(svc05000);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRmaEntityFields(DataConnectorTable svc05000)
        {
            svc05000.AddField("RETDOCID", "Return Document ID", DataConnector.FieldTypeIdString, true);
            var returnRecordType = svc05000.AddField("Return_Record_Type", "Return Record Type", DataConnector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc05000.AddField("RMA_Status", "RMA Status", DataConnector.FieldTypeIdInteger, true);
            svc05000.AddField("Received", "Received", DataConnector.FieldTypeIdYesNo, true);
            var returnOrigin = svc05000.AddField("RETORIG", "Return Origin", DataConnector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc05000.AddField("RETREF", "Return Reference", DataConnector.FieldTypeIdString, true);
            svc05000.AddField("RETSTAT", "Return Status", DataConnector.FieldTypeIdString, true);
            svc05000.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString, true);
            svc05000.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc05000.AddField("ETADTE", "ETA Date", DataConnector.FieldTypeIdDate, true);
            svc05000.AddField("RETUDATE", "Return Date", DataConnector.FieldTypeIdDate, true);
            svc05000.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);
            svc05000.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString, true);
            svc05000.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc05000.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc05000.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc05000.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc05000.AddField("ETATME", "ETA Time", DataConnector.FieldTypeIdTime);
            svc05000.AddField("Return_Time", "Return Time", DataConnector.FieldTypeIdTime);
            svc05000.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc05000.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            svc05000.AddField("RTRNNAME", "Return Address Name", DataConnector.FieldTypeIdString);
            svc05000.AddField("RETADDR1", "Return Address 1", DataConnector.FieldTypeIdString);
            svc05000.AddField("RETADDR2", "Return Address 2", DataConnector.FieldTypeIdString);
            svc05000.AddField("RETADDR3", "Return Address 3", DataConnector.FieldTypeIdString);
            svc05000.AddField("RTRNCITY", "Return Address City", DataConnector.FieldTypeIdString);
            svc05000.AddField("SVC_Return_State", "Return State", DataConnector.FieldTypeIdString);
            svc05000.AddField("RTRNZIP", "Return Address Zip Code", DataConnector.FieldTypeIdString);
            svc05000.AddField("Return_Country", "Return Country", DataConnector.FieldTypeIdString);
            svc05000.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            svc05000.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc05000.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc05000.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc05000.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc05000.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc05000.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc05000.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc05000.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc05000.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString);
            svc05000.AddField("EQPLINE", "Equipment Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("Bill_of_Lading", "Bill of Lading", DataConnector.FieldTypeIdString);
            svc05000.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc05000.AddField("SOPNUMBE", "SOP Number", DataConnector.FieldTypeIdString);
            svc05000.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("CMPNTSEQ", "Component Sequence", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc05000.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc05000.AddField("Commit_Date", "Commit Date", DataConnector.FieldTypeIdDate);
            svc05000.AddField("Commit_Time", "Commit Time", DataConnector.FieldTypeIdTime);
            svc05000.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc05000.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc05000.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc05000.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc05000.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc05000.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc05000.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc05000.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc05000.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc05000.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc05000.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc05000.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("ISMCTRX", "Is MC Trx", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc05000.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc05000.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc05000.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString);
            svc05000.AddField("SVC_RMA_Reason_Code", "RMA Reason Code", DataConnector.FieldTypeIdString);
            svc05000.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code Desc", DataConnector.FieldTypeIdString);
            svc05000.AddField("SVC_RMA_From_Service", "RMA From Service", DataConnector.FieldTypeIdString);
            svc05000.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdYesNo);

            var serviceRecordType = svc05000.AddField("SRVRECTYPE", "Service Record Type", DataConnector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
            
            var sopType = svc05000.AddField("SOPTYPE", "SOP Type", DataConnector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
        }

        public DataConnectorEntity GetRtvEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListRtv), "RTVs", ParentConnector);

            var svc05600 = entity.AddTable("SVC05600");
            AddRtvEntityFields(svc05600);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRtvEntityFields(DataConnectorTable svc05600)
        {
            svc05600.AddField("RTV_Number", "RTV Number", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("RTV_Type", "RTV Type", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("RTV_Return_Status", "RTV Return Status", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("VRMA_Document_ID", "VRMA Document ID", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("RETDOCID", "RMA Number", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("VENDORID", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc05600.AddField("Shipped_Date", "Shipped Date", DataConnector.FieldTypeIdDate, true);
            svc05600.AddField("receiptdate", "Receipt Date", DataConnector.FieldTypeIdDate, true);
            svc05600.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);
            svc05600.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("Travel_Price", "Travel Price", DataConnector.FieldTypeIdCurrency, true);
            svc05600.AddField("Bill_of_Lading_Out", "Bill of Landing (Out)", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("Shipping_Method_Out", "Shipping Method (Out)", DataConnector.FieldTypeIdString, true);
            svc05600.AddField("VOIDSTTS", "Void Status", DataConnector.FieldTypeIdInteger, true);
            svc05600.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc05600.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_Name", "Ship Address Name", DataConnector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_1", "Ship Address 1", DataConnector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_2", "Ship Address 2", DataConnector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_3", "Ship Address 3", DataConnector.FieldTypeIdString);
            svc05600.AddField("Ship_City", "Ship City", DataConnector.FieldTypeIdString);
            svc05600.AddField("Ship_State", "Ship State", DataConnector.FieldTypeIdString);
            svc05600.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc05600.AddField("Ship_Country", "Ship Country", DataConnector.FieldTypeIdString);
            svc05600.AddField("ADRSCODE", "Entry Time", DataConnector.FieldTypeIdTime);
            svc05600.AddField("Ship_Address_Name", "Promised Date", DataConnector.FieldTypeIdDate);
            svc05600.AddField("Ship_Address_1", "Promised Time", DataConnector.FieldTypeIdTime);
            svc05600.AddField("Shipped_Time", "Shipped Time", DataConnector.FieldTypeIdTime);
            svc05600.AddField("Receipt_Time", "Receipt Time", DataConnector.FieldTypeIdTime);
            svc05600.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc05600.AddField("LOCCODEB", "Location Code Bad", DataConnector.FieldTypeIdString);
            svc05600.AddField("Part_Price", "Part Price", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Part_Cost", "Part Cost", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Labor_Price", "Labor Price", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Labor_Cost", "Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Expense_Price", "Expense Price", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Expense_Cost", "Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Travel_Cost", "Travel Cost", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Bill_of_Lading", "Bill of Landing", DataConnector.FieldTypeIdString);
            svc05600.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc05600.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString);
            svc05600.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc05600.AddField("VCHNUMWK", "Voucher Number (Work)", DataConnector.FieldTypeIdString);
            svc05600.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", DataConnector.FieldTypeIdString);
            svc05600.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", DataConnector.FieldTypeIdString);
            svc05600.AddField("CUSTOWN", "Customer Owned", DataConnector.FieldTypeIdYesNo);
            svc05600.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            svc05600.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc05600.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc05600.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc05600.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc05600.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc05600.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc05600.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc05600.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc05600.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc05600.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc05600.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc05600.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc05600.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc05600.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc05600.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc05600.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc05600.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc05600.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc05600.AddField("Originating_Part_Price", "Originating Part Price", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Part_Cost", "Originating Part Cost", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Labor_Price", "Originating Labor Price", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Labor_Cost", "Originating Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_ExpensePrice", "Originating Expense Price", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Expense_Cost", "Originating Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Travel_Price", "Originating Travel Price", DataConnector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Travel_Cost", "Originating Travel Cost", DataConnector.FieldTypeIdCurrency);

            var rtvStatus = svc05600.AddField("RTV_Status", "RTV Status", DataConnector.FieldTypeIdEnum);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
        }

        public DataConnectorEntity GetWorkOrderEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListWorkOrders), "Work orders", ParentConnector);

            var svc06100 = entity.AddTable("SVC06100");

            var rm00101 = entity.AddTable("RM00101", "SVC06100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddWorkOrderEntityFields(svc06100, rm00101);

            return entity;
        }
        public void AddWorkOrderEntityFields(DataConnectorTable svc06100, DataConnectorTable rm00101)
        {
            var workOrderRecordType = svc06100.AddField("WORECTYPE", "Work Order Record Type", DataConnector.FieldTypeIdEnum, true);
            workOrderRecordType.AddListItems(1, new List<string> { "Quote", "Open", "History", "Template" });
            svc06100.AddField("WORKORDNUM", "Work Order Number", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("WOTYPE", "Work Order Type", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("SVC_Depot_Priority", "SVC_Depot_Priority", DataConnector.FieldTypeIdInteger, true);
            svc06100.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc06100.AddField("ECOMPDT", "EComp Date", DataConnector.FieldTypeIdDate, true);
            svc06100.AddField("EComp_Time", "EComp Time", DataConnector.FieldTypeIdTime, true);
            svc06100.AddField("RETUDATE", "Return Date", DataConnector.FieldTypeIdDate, true);
            svc06100.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);
            svc06100.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("IBITEMNUM", "Inbound Item Number", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("IBSERIAL", "Inbound Serial Number", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("IBEQUIPID", "Inbound Equipment ID", DataConnector.FieldTypeIdInteger, true);
            svc06100.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity, true);
            svc06100.AddField("STATIONID", "Depot Station ID", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc06100.AddField("LABSTOTCST", "Labor Sub Total Cost", DataConnector.FieldTypeIdCurrency, true);
            svc06100.AddField("PARSTOTCST", "Part Sub Total Cost", DataConnector.FieldTypeIdCurrency, true);
            svc06100.AddField("TOTLABHRS", "Total Labor Hours", DataConnector.FieldTypeIdCurrency, true);
            svc06100.AddField("Commit_Date", "Commit Date", DataConnector.FieldTypeIdDate, true);

            svc06100.AddField("WOSTAT", "Work Order Status", DataConnector.FieldTypeIdString);
            svc06100.AddField("PARWONUM", "Parent Work Order Number", DataConnector.FieldTypeIdString);
            svc06100.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svc06100.AddField("TIMEZONE", "TimeZone", DataConnector.FieldTypeIdString);
            svc06100.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc06100.AddField("ETADTE", "ETA Date", DataConnector.FieldTypeIdDate);
            svc06100.AddField("ETATME", "ETA Time", DataConnector.FieldTypeIdTime);
            svc06100.AddField("Return_Time", "Return Time", DataConnector.FieldTypeIdTime);
            svc06100.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc06100.AddField("BIN", "Bin", DataConnector.FieldTypeIdString);
            svc06100.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            svc06100.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc06100.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc06100.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc06100.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc06100.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc06100.AddField("RETDOCID", "Return Document ID", DataConnector.FieldTypeIdString);
            svc06100.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc06100.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString);
            svc06100.AddField("SERVLITEMSEQ", "Service Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc06100.AddField("OBITEMNUM", "Outbound Item Number", DataConnector.FieldTypeIdString);
            svc06100.AddField("OBSERIAL", "Outbound Serial Number", DataConnector.FieldTypeIdString);
            svc06100.AddField("OBEQUIPID", "Outbound Equipment ID", DataConnector.FieldTypeIdInteger);
            svc06100.AddField("ROUTEID", "Route ID", DataConnector.FieldTypeIdString);
            svc06100.AddField("SEQUENCE1", "Sequence", DataConnector.FieldTypeIdInteger);
            svc06100.AddField("IBANALCODE", "Inbound Analysis Code", DataConnector.FieldTypeIdString);
            svc06100.AddField("OBANALCODE", "Outbound Analysis Code", DataConnector.FieldTypeIdString);
            svc06100.AddField("CUSTOWN", "Customer Owned", DataConnector.FieldTypeIdYesNo);
            svc06100.AddField("ORDDOCID", "Order Document ID", DataConnector.FieldTypeIdString);
            svc06100.AddField("TRANSLINESEQ", "Transfer Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc06100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc06100.AddField("FACTSEAL", "Factory Sealed", DataConnector.FieldTypeIdYesNo);
            svc06100.AddField("PRICELVL", "Price Level", DataConnector.FieldTypeIdString);
            svc06100.AddField("NUMRESCHED", "Number of Reschedules", DataConnector.FieldTypeIdInteger);
            svc06100.AddField("Commit_Time", "Commit Time", DataConnector.FieldTypeIdTime);
            svc06100.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc06100.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc06100.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc06100.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc06100.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc06100.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc06100.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc06100.AddField("SVC_RMA_Reason_Code", "RMA Reason Code", DataConnector.FieldTypeIdString);
            svc06100.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code Description", DataConnector.FieldTypeIdString);
            svc06100.AddField("SVC_Process_SEQ_Number", "Process Sequence Number", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var parentWorkOrderRecordType = svc06100.AddField("PARWORECTYPE", "Parent Work Order Record Type", DataConnector.FieldTypeIdEnum);
            parentWorkOrderRecordType.AddListItems(1, new List<string> { "Quote", "Open", "History", "Template" });

            var origin = svc06100.AddField("ORIGIN", "Origin", DataConnector.FieldTypeIdEnum);
            origin.AddListItems(1, new List<string> { "Quote", "Open", "History", "Template" });

            var serviceRecordType = svc06100.AddField("SRVRECTYPE", "Service Record Type", DataConnector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
        }

        public DataConnectorEntity GetHistoricalServiceCallEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListHistFieldServiceCalls), "Historical service calls", ParentConnector);

            var svc30200 = entity.AddTable("SVC30200");

            var svc30202 = entity.AddScript("(select SRVRECTYPE, CALLNBR, EQUIPID from {0}..SVC30202 with (NOLOCK) where EQPLINE = 1)", "SVC30202", "SVC30200");
            svc30202.AddJoinFields("SRVRECTYPE", "SRVRECTYPE");
            svc30202.AddJoinFields("CALLNBR", "CALLNBR");

            var svc00300 = entity.AddTable("SVC00300", "SVC30202");
            svc00300.AddJoinFields("EQUIPID", "EQUIPID");

            AddHistoricalServiceCallEntityFields(svc30200, svc00300);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalServiceCallEntityFields(DataConnectorTable svc30200, DataConnectorTable svc00300)
        {
            svc30200.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString, true);
            var serviceRecordType = svc30200.AddField("SRVRECTYPE", "Service Record Type", DataConnector.FieldTypeIdEnum, true);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
            svc30200.AddField("SRVSTAT", "Service Call Status", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("SRVTYPE", "Service Type", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("SVCDESCR", "Service Description", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("priorityLevel", "priorityLevel", DataConnector.FieldTypeIdInteger, true);
            svc30200.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("Customer_Reference", "Customer Reference", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("CUSTNAME", "Name", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("SVCAREA", "Service Area", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("TECHID", "Tech ID", DataConnector.FieldTypeIdString, true);
            svc30200.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc30200.AddField("TOTAL", "Total", DataConnector.FieldTypeIdCurrency, true);
            svc30200.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc30200.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc30200.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc30200.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc30200.AddField("ZIP", "Zip", DataConnector.FieldTypeIdString);
            svc30200.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            svc30200.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            svc30200.AddField("TIMEZONE", "TimeZone", DataConnector.FieldTypeIdString);
            svc30200.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("Notify_Date", "Notify Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("Notify_Time", "Notify Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("ETADTE", "ETA Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("ETATME", "ETA Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("DISPDTE", "Dispatch Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("DISPTME", "Dispatch Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("ARRIVDTE", "Arrival Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("ARRIVTME", "Arrival Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("Response_Date", "Response Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("Response_Time", "Response Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("PRICELVL", "Price Level", DataConnector.FieldTypeIdString);
            svc30200.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("LABSTOTPRC", "Labor Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("LABPCT", "Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc30200.AddField("LABSTOTCST", "Labor Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("PARSTOTPRC", "Part Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("PARTPCT", "Part Pct", DataConnector.FieldTypeIdPercentage);
            svc30200.AddField("PARSTOTCST", "Part Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("MSCSTOTPRC", "Misc Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("MISCPCT", "Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc30200.AddField("MISSTOTCST", "Misc Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svc30200.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svc30200.AddField("PRETAXTOT", "PreTax Total", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("Invoiced_Amount", "Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("METER1", "Meter 1", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("METER2", "Meter 2", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("METER3", "Meter 3", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc30200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("NOTFYFLAG", "Notify Flag", DataConnector.FieldTypeIdYesNo);
            svc30200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc30200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc30200.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc30200.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc30200.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc30200.AddField("MSTRCALLNBR", "Master Service Call Number", DataConnector.FieldTypeIdString);
            svc30200.AddField("ESCDATE", "Escalation Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("ESCTIME", "Escalation Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("Escalation_Level", "Escalation Level", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("SPLTTERMS", "Split Terms Code", DataConnector.FieldTypeIdString);
            svc30200.AddField("Callback", "Callback", DataConnector.FieldTypeIdYesNo);
            svc30200.AddField("PROJCTID", "Project ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("ProjectRef1_1", "ProjectRef1", DataConnector.FieldTypeIdString);
            svc30200.AddField("CONTNBR", "Contract Number", DataConnector.FieldTypeIdString);
            svc30200.AddField("SVC_Contract_Line_SEQ", "Contract Line Sequence Number", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ETTR", "ETTR", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("SVC_On_Hold", "On Hold", DataConnector.FieldTypeIdYesNo);
            svc30200.AddField("Print_to_Web", "Print to Web", DataConnector.FieldTypeIdYesNo);
            svc30200.AddField("Meters_1", "Meters", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc30200.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc30200.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc30200.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc30200.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc30200.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc30200.AddField("ORIGMISSTOTCST", "Originating Misc Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGMSCSTOTPRC", "Originating Misc Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGLABSUBTOTCOST", "Originating Labor Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGLABSTOTPRC", "Originating Labor Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGPARSTOTCST", "Originating Part Sub Total Cost", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGPARSTOTPRC", "Originating Part Sub Total Price", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGPRETAXTOT", "Originating PreTax Total", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGTOTAL", "Originating Service Total", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("Orig_Invoiced_Amount", "Originating Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc30200.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc30200.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc30200.AddField("SVC_Pre600", "SVC_Pre600", DataConnector.FieldTypeIdYesNo);
            svc30200.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            svc30200.AddField("Replaces_1", "Replaces", DataConnector.FieldTypeIdString);
            svc00300.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString);
            svc00300.AddField("SERLNMBR", "Equipment/Serial Number", DataConnector.FieldTypeIdString);

            var masterServiceRecordType = svc30200.AddField("MSTRRECTYPE", "Master Service Record Type", DataConnector.FieldTypeIdEnum);
            masterServiceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var contractRecordType = svc30200.AddField("CONSTS", "Contract Record Type", DataConnector.FieldTypeIdEnum);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
        }

        public DataConnectorEntity GetHistoricalContractEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListHistContracts), "Historical contracts", ParentConnector);

            var svc30600 = entity.AddTable("SVC30600");

            var rm00101 = entity.AddTable("RM00101", "SVC30600");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddHistoricalContractEntityFields(svc30600, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalContractEntityFields(DataConnectorTable svc30600, DataConnectorTable rm00101)
        {
            var contractRecordType = svc30600.AddField("CONSTS", "Contract Record Type", DataConnector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc30600.AddField("CONTNBR", "Contract Number", DataConnector.FieldTypeIdString, true);
            svc30600.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc30600.AddField("BILSTRT", "Bill Start", DataConnector.FieldTypeIdDate, true);
            svc30600.AddField("BILEND", "Bill End", DataConnector.FieldTypeIdDate, true);
            svc30600.AddField("BILLNGTH", "Bill Length", DataConnector.FieldTypeIdInteger, true);
            var billPeriod = svc30600.AddField("BILPRD", "Bill Period", DataConnector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc30600.AddField("TOTAL", "Total", DataConnector.FieldTypeIdCurrency, true);
            svc30600.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc30600.AddField("BILONDY", "Bill On Day", DataConnector.FieldTypeIdInteger, true);
            var billingCycle = svc30600.AddField("BILCYC", "Billing Cycle", DataConnector.FieldTypeIdEnum, true);
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc30600.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate, true);
            svc30600.AddField("ENDDATE", "End Date", DataConnector.FieldTypeIdDate, true);
            var liabilityType = svc30600.AddField("SVC_Liability_Type", "Liability Type", DataConnector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block Time", "Retainage", "Based on Calls", "Metered" });
            svc30600.AddField("Contract_Transfer_Date", "Contract Transfer Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("priorityLevel", "priorityLevel", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("TIMEZONE", "TimeZone", DataConnector.FieldTypeIdString);
            svc30600.AddField("CONTPRC", "Contract Price", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("RENPRCSCHD", "Renewing Price Schedule", DataConnector.FieldTypeIdString);
            svc30600.AddField("PCTCRYFWD", "Percentage Carried Forward", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("FRZEND", "Frozen End", DataConnector.FieldTypeIdDate);
            svc30600.AddField("FRXSTRT", "Frozen Start", DataConnector.FieldTypeIdDate);
            svc30600.AddField("MXINCPCT", "Max Increase Percentage", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("BLKTIM", "Blocked Time", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("VALTIM", "Value of Time", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("COMDLRAM", "Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc30600.AddField("PRCSTAT", "Status of Price", DataConnector.FieldTypeIdString);
            svc30600.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc30600.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            svc30600.AddField("PARTPCT", "Part Pct", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("LABPCT", "Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("MISCPCT", "Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("PMMSCPCT", "PM Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("PMPRTPCT", "PM Part Pct", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("PMLABPCT", "PM Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("RETNAGAM", "Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("RTNBILLD", "Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("COMMCODE", "Commission Code", DataConnector.FieldTypeIdString);
            svc30600.AddField("COMPRCNT", "Commission Percent", DataConnector.FieldTypeIdPercentage);
            svc30600.AddField("FRSTBLDTE", "First Bill Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("Last_Amount_Billed", "Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("LSTBLDTE", "Last Bill Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("NBRCAL", "Max Number of Calls", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("ACTCAL", "Actual Number of Calls", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("TOTVALCAL", "Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc30600.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("NXTBLDTE", "Next Bill Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("CNTTYPE", "Contract Type", DataConnector.FieldTypeIdString);
            svc30600.AddField("PRICSHED", "Price Schedule", DataConnector.FieldTypeIdString);
            svc30600.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("MINBIL", "Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("MAXBIL", "Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("MAXBILL", "Max Billable", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("AUTOREN", "Auto Renewing", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("MSTCNTRCT", "Master Contract Number", DataConnector.FieldTypeIdString);
            svc30600.AddField("SRVTYPE", "Service Type", DataConnector.FieldTypeIdString);
            svc30600.AddField("BILFRRET", "Bill For Retainer", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("TOTBIL", "Total Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("PREPAID", "Pre Paid", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svc30600.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc30600.AddField("Contract_Length", "Contract Length", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("Invoiced_Amount", "Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Liabiltiy_Reduction", "Liability Reduction", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("Amount_To_Invoice", "Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Liability_Amount", "Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Total_Liability_Amount", "Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("NUMOFINV", "Number of Invoices", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("Quote_Status", "Quote Status", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("QUOEXPDA", "Quote Expiration Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("Credit_Hold", "Credit Hold", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svc30600.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svc30600.AddField("New_PO_Required", "New PO Required", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", DataConnector.FieldTypeIdString);
            svc30600.AddField("Source_Contract_Number", "Source Contract Number", DataConnector.FieldTypeIdString);
            svc30600.AddField("Contract_Response_Time", "Contract Response Time", DataConnector.FieldTypeIdString);
            svc30600.AddField("Liability_Months", "Liability Months", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("Next_Liability_Date", "Next Liability Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("Last_Liability_Date", "Last Liability Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("Total_Liability_Billed", "Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Total_Unit", "Total Unit", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Created_User_ID", "SVC_Created User ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("Source_User_ID", "Source User ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", DataConnector.FieldTypeIdDate);
            svc30600.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString);
            svc30600.AddField("Location_Segment", "Location Segment", DataConnector.FieldTypeIdString);
            svc30600.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("SVC_Invoice_Detail", "Invoice Detail", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc30600.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc30600.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc30600.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc30600.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc30600.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc30600.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("SVC_Paid_Contract", "Paid Contract", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_Discount_Recognized", "Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("SVC_Discount_Remaining", "Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("SVC_Discount_Next", "Discount Next", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc30600.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc30600.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc30600.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc30600.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("ORIGVALTIM", "Originating Value of Time", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORCOMAMT", "Originating Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGRETNAGAM", "Originating Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGRTNBILLD", "Originating Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGTOTAL", "Originating Service Total", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGMINBIL", "Originating Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGMAXBIL", "Originating Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Originating_Max_Billable", "Originating Max Billable", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGTOTBIL", "Originating Total Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_Invoiced_Amount", "Originating Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_Liability_Amount", "Originating Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("OrigTotLiabilityAmount", "Originating Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Originating_Total_Unit", "Originating Total Unit", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("OrigDiscountReceived", "Originating Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("OrigDiscountRemaining", "Originating Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("OrigDiscountNext", "Originating Discount Next", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("SmoothInvoiceCalc", "Smooth Invoice Calculation", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("SmoothRevenueCalc", "Smooth Revenue Calculation", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_Use_Same_Number", "Use Same Number", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            svc30600.AddField("SVC_Invoiced_Cost", "Invoiced Cost", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_SVC_Invoiced_Cost", "Originating SVC Invoiced Cost", DataConnector.FieldTypeIdCurrency);
            svc30600.AddField("AUTOREN", "Auto Renewing", DataConnector.FieldTypeIdString);
            svc30600.AddField("SVC_New_Contract_Number", "New Contract Number", DataConnector.FieldTypeIdString);
            svc30600.AddField("SVC_Evergreen_Contract", "Evergreen Contract", DataConnector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_Evergreen_RenewLimit", "Evergreen Renew Limit", DataConnector.FieldTypeIdInteger);
            svc30600.AddField("SVC_Evergreen_Renewals", "Evergreen Renewals", DataConnector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var contractTransferStatus = svc30600.AddField("Contract_Transfer_Status", "Contract Transfer Status", DataConnector.FieldTypeIdEnum);
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });

            var renewingContractType = svc30600.AddField("RENCNTTYP", "Renewing Contract Type", DataConnector.FieldTypeIdEnum);
            renewingContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });

            var billingStatus = svc30600.AddField("BILSTAT", "Billing Status", DataConnector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });

            var contractPeriod = svc30600.AddField("Contract_Period", "Contract Period", DataConnector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });

            var sourceContractType = svc30600.AddField("Source_Contract_Type", "Source Contract Type", DataConnector.FieldTypeIdEnum);
            sourceContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
        }

        public DataConnectorEntity GetHistoricalContractLineEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListHistContractLines), "Historical contract lines", ParentConnector);

            var svc30601 = entity.AddTable("SVC30601");

            var rm00101 = entity.AddTable("RM00101", "SVC30601");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddHistoricalContractLineEntityFields(svc30601, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalContractLineEntityFields(DataConnectorTable svc30601, DataConnectorTable rm00101)
        {
            var contractRecordType = svc30601.AddField("CONSTS", "Contract Record Type", DataConnector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc30601.AddField("CONTNBR", "Contract Number", DataConnector.FieldTypeIdString, true);
            svc30601.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger, true);
            svc30601.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc30601.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity, true);
            svc30601.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svc30601.AddField("EQUIPID", "Equipment ID", DataConnector.FieldTypeIdInteger, true);
            svc30601.AddField("SERLNMBR", "Serial Number", DataConnector.FieldTypeIdString, true);
            svc30601.AddField("BILSTRT", "Bill Start", DataConnector.FieldTypeIdDate, true);
            svc30601.AddField("BILEND", "Bill End", DataConnector.FieldTypeIdDate, true);
            svc30601.AddField("BILLNGTH", "Bill Length", DataConnector.FieldTypeIdInteger, true);
            var billPeriod = svc30601.AddField("BILPRD", "Bill Period", DataConnector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc30601.AddField("TOTAL", "Total", DataConnector.FieldTypeIdCurrency, true);
            svc30601.AddField("CNTTYPE", "Contract Type", DataConnector.FieldTypeIdString, true);
            svc30601.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc30601.AddField("SRVTYPE", "Service Type", DataConnector.FieldTypeIdString, true);
            svc30601.AddField("TOTBIL", "Total Billed", DataConnector.FieldTypeIdCurrency, true);
            svc30601.AddField("PREPAID", "Pre Paid", DataConnector.FieldTypeIdYesNo, true);
            svc30601.AddField("BILONDY", "Bill On Day", DataConnector.FieldTypeIdInteger, true);
            var billingCycle = svc30601.AddField("BILCYC", "Billing Cycle", DataConnector.FieldTypeIdEnum, true);
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc30601.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate, true);
            svc30601.AddField("ENDDATE", "End Date", DataConnector.FieldTypeIdDate, true);
            svc30601.AddField("Unit_Cost_Total", "Unit Cost Total", DataConnector.FieldTypeIdCurrency, true);
            var liabilityType = svc30601.AddField("SVC_Liability_Type", "Liability Type", DataConnector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block Time", "Retainage", "Based on Calls", "Metered" });

            svc30601.AddField("Contract_Transfer_Date", "Contract Transfer Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("CNTPRCOVR", "Contract Price Overridden", DataConnector.FieldTypeIdYesNo);
            svc30601.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("CONFGREF", "Config Reference", DataConnector.FieldTypeIdString);
            svc30601.AddField("FRZEND", "Frozen End", DataConnector.FieldTypeIdDate);
            svc30601.AddField("FRXSTRT", "Frozen Start", DataConnector.FieldTypeIdDate);
            svc30601.AddField("BLKTIM", "Blocked Time", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("VALTIM", "Value of Time", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("DSCPCTAM", "Discount Percent Amount", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("COMDLRAM", "Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc30601.AddField("PRCSTAT", "Status of Price", DataConnector.FieldTypeIdString);
            svc30601.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc30601.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            svc30601.AddField("PARTPCT", "Part Pct", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("LABPCT", "Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("MISCPCT", "Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("PMMSCPCT", "PM Misc Pct", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("PMPRTPCT", "PM Part Pct", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("PMLABPCT", "PM Labor Pct", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString);
            svc30601.AddField("RETNAGAM", "Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("RTNBILLD", "Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            svc30601.AddField("COMMCODE", "Commission Code", DataConnector.FieldTypeIdString);
            svc30601.AddField("COMPRCNT", "Commission Percent", DataConnector.FieldTypeIdPercentage);
            svc30601.AddField("FRSTBLDTE", "First Bill Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("Last_Amount_Billed", "Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("LSTBLDTE", "Last Bill Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("NBRCAL", "Max Number of Calls", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("ACTCAL", "Actual Number of Calls", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("TOTVALCAL", "Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc30601.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc30601.AddField("NXTBLDTE", "Next Bill Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("PRICSHED", "Price Schedule", DataConnector.FieldTypeIdString);
            svc30601.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("MINBIL", "Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("MAXBIL", "Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("MAXBILL", "Max Billable", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("AUTOREN", "Auto Renewing", DataConnector.FieldTypeIdYesNo);
            svc30601.AddField("priorityLevel", "priorityLevel", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("MSTCNTRCT", "Master Contract Number", DataConnector.FieldTypeIdString);
            svc30601.AddField("BILFRRET", "Bill For Retainer", DataConnector.FieldTypeIdYesNo);
            svc30601.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svc30601.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc30601.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            svc30601.AddField("CNFGLVL", "Config Level", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("CNFGSEQ", "Config Sequence", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("Contract_Length", "Contract Length", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("Invoiced_Amount", "Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Amount_To_Invoice", "Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Liabiltiy_Reduction", "Liability Reduction", DataConnector.FieldTypeIdYesNo);
            svc30601.AddField("Liability_Amount", "Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Total_Liability_Amount", "Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("NUMOFINV", "Number of Invoices", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("New_Invoice_Amount", "New Invoice Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Contract_Line_Status", "Contract Line Status", DataConnector.FieldTypeIdString);
            svc30601.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", DataConnector.FieldTypeIdString);
            svc30601.AddField("Contract_Response_Time", "Contract Response Time", DataConnector.FieldTypeIdString);
            svc30601.AddField("Liability_Months", "Liability Months", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("Next_Liability_Date", "Next Liability Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("Last_Liability_Date", "Last Liability Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("Total_Liability_Billed", "Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("TAXEXMT1", "Tax Exempt 1", DataConnector.FieldTypeIdString);
            svc30601.AddField("TAXEXMT2", "Tax Exempt 2", DataConnector.FieldTypeIdString);
            svc30601.AddField("Total_Unit", "Total Unit", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", DataConnector.FieldTypeIdDate);
            svc30601.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString);
            svc30601.AddField("SVC_Monthly_Price", "Monthly_Price", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Location_Segment", "Location Segment", DataConnector.FieldTypeIdString);
            svc30601.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc30601.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc30601.AddField("DSCDLRAM", "Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Invoiced_Discount", "Invoiced Discount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Discount_Recognized", "Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Discount_Remaining", "Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Discount_Next", "Discount Next", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_PM_Day", "PM Day", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc30601.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc30601.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc30601.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);

            svc30601.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc30601.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc30601.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc30601.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc30601.AddField("ORIGVALTIM", "Originating Value of Time", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORCOMAMT", "Originating Commission Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGRETNAGAM", "Originating Retainage Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGRTNBILLD", "Originating Retainage Billed", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGTOTAL", "Originating Service Total", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGMINBIL", "Originating Min Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGMAXBIL", "Originating Max Billable Call", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Originating_Max_Billable", "Originating Max Billable", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGTOTBIL", "Originating Total Billed", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Orig_Invoiced_Amount", "Originating Invoiced Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Orig_Liability_Amount", "Originating Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("OrigTotLiabilityAmount", "Originating Total Liability Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("Originating_Total_Unit", "Originating Total Unit", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORUNTCST", "Originating Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORDDLRAT", "Originating Discount Dollar Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("OrigDiscountReceived", "Originating Discount Recognized", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("OrigDiscountRemaining", "Originating Discount Remaining", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("OrigDiscountNext", "Originating Discount Next", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGUNITCOSTTOTAL", "Originating Unit Cost Total", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGMONTHPRICE", "Originating Monthly Price", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGINVOICEDDISC", "Originating Invoiced Discount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("OrigNewInvoiceAmount", "Originating New Invoice Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            rm00101.AddField("RM00101.CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var contractTransferStatus = svc30601.AddField("Contract_Transfer_Status", "Contract Transfer Status", DataConnector.FieldTypeIdEnum);
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });

            var billingStatus = svc30601.AddField("BILSTAT", "Billing Status", DataConnector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });

            var contractPeriod = svc30601.AddField("Contract_Period", "Contract Period", DataConnector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });

            var pmMonth = svc30601.AddField("SVC_PM_Date", "PM Month", DataConnector.FieldTypeIdEnum);
            pmMonth.AddListItems(1, new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
        }

        public DataConnectorEntity GetRmaLineEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListRmaLines), "RMA lines", ParentConnector);

            var svc05200 = entity.AddTable("SVC05200");
            AddRmaLineEntityFields(svc05200);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYS", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("ODECPLCU - 1", "Originating Decimal Places Currency", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRmaLineEntityFields(DataConnectorTable svc05200)
        {
            svc05200.AddField("RETDOCID", "Return Document ID", DataConnector.FieldTypeIdString, true);
            var returnRecordType = svc05200.AddField("Return_Record_Type", "Return Record Type", DataConnector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc05200.AddField("LNSEQNBR", "Line SEQ Number", DataConnector.FieldTypeIdInteger, true);
            svc05200.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString, true);
            svc05200.AddField("RMA_Status", "RMA Status", DataConnector.FieldTypeIdInteger, true);
            svc05200.AddField("RETSTAT", "Return Status", DataConnector.FieldTypeIdString, true);
            svc05200.AddField("Received", "Received", DataConnector.FieldTypeIdYesNo, true);
            svc05200.AddField("SVC_Ready_To_Close", "Ready To Close", DataConnector.FieldTypeIdYesNo, true);
            svc05200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger, true);
            var returnOrigin = svc05200.AddField("RETORIG", "Return Origin", DataConnector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc05200.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc05200.AddField("ETADTE", "ETA Date", DataConnector.FieldTypeIdDate, true);
            svc05200.AddField("RETUDATE", "Return Date", DataConnector.FieldTypeIdDate, true);
            svc05200.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);
            svc05200.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString, true);
            svc05200.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc05200.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc05200.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svc05200.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency, true);
            svc05200.AddField("XTNDPRCE", "Extended Price", DataConnector.FieldTypeIdCurrency, true);

            svc05200.AddField("SVC_Next_Line_SEQ_Number", "Next Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("SVC_Prev_Line_SEQ_Number", "Prev Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("Traveler_Printed", "Traveler Printed", DataConnector.FieldTypeIdYesNo);
            svc05200.AddField("SVC_RMA_Reason_Code", "RMA Reason Code", DataConnector.FieldTypeIdString);
            svc05200.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code Description", DataConnector.FieldTypeIdString);
            svc05200.AddField("RETREF", "Return Reference", DataConnector.FieldTypeIdString);
            svc05200.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString);
            svc05200.AddField("EQPLINE", "Equipment Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("SVC_RMA_From_Service", "RMA From Service", DataConnector.FieldTypeIdYesNo);
            svc05200.AddField("SOPNUMBE", "SOP Number", DataConnector.FieldTypeIdString);
            svc05200.AddField("CMPNTSEQ", "Component Sequence", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc05200.AddField("ETATME", "ETA Time", DataConnector.FieldTypeIdTime);
            svc05200.AddField("Commit_Date", "Commit Date", DataConnector.FieldTypeIdDate);
            svc05200.AddField("Commit_Time", "Commit Time", DataConnector.FieldTypeIdTime);
            svc05200.AddField("Return_Time", "Return Time", DataConnector.FieldTypeIdTime);
            svc05200.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc05200.AddField("PRMDATE", "Promised Date", DataConnector.FieldTypeIdDate);
            svc05200.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svc05200.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            svc05200.AddField("RTRNNAME", "Return Address Name", DataConnector.FieldTypeIdString);
            svc05200.AddField("RETADDR1", "Return Address 1", DataConnector.FieldTypeIdString);
            svc05200.AddField("RETADDR2", "Return Address 2", DataConnector.FieldTypeIdString);
            svc05200.AddField("RETADDR3", "Return Address 3", DataConnector.FieldTypeIdString);
            svc05200.AddField("RTRNCITY", "Return Address City", DataConnector.FieldTypeIdString);
            svc05200.AddField("SVC_Return_State", "Return State", DataConnector.FieldTypeIdString);
            svc05200.AddField("RTRNZIP", "Return Address Zip Code", DataConnector.FieldTypeIdString);
            svc05200.AddField("Return_Country", "Return Country", DataConnector.FieldTypeIdString);
            svc05200.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString);
            svc05200.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            svc05200.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc05200.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc05200.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc05200.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc05200.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc05200.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc05200.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc05200.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc05200.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc05200.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc05200.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString);
            svc05200.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity);
            svc05200.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svc05200.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString);
            svc05200.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("ORUNTCST", "Originating Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("UNITPRCE", "Unit Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("ORUNTPRC", "Originating Unit Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("OXTNDPRC", "Originating Extended Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("CUSTOWN", "Customer Owned", DataConnector.FieldTypeIdYesNo);
            svc05200.AddField("FACTSEAL", "Factory Sealed", DataConnector.FieldTypeIdYesNo);
            svc05200.AddField("ORDDOCID", "Order Document ID", DataConnector.FieldTypeIdString);
            svc05200.AddField("TRANSLINESEQ", "Transfer Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("STATUS", "Transfer Status", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("Flat_Rate_Repair_Price", "Flat Rate Repair Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Orig_Flat_RepairPrice", "Originating Flat Rate Repair Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Repair_Price", "Repair Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Repair_Price", "Originating Repair Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("NTE_Price", "NTE Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_NTE_Price", "Originating NTE Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Repair_Cost", "Repair Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Repair_Cost", "Originating Repair Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Bill_of_Lading", "Bill of Lading", DataConnector.FieldTypeIdString);
            svc05200.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc05200.AddField("Credit_SOP_Number", "Credit SOP Number", DataConnector.FieldTypeIdString);
            svc05200.AddField("Credit_SOP_Line_Item_Seq", "Credit SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("Replace_SOP_Number", "Replace SOP Number", DataConnector.FieldTypeIdString);
            svc05200.AddField("Replace_SOP_Line_Item_Se", "Replace SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("Location_Code_Replacemen", "Location Code Replacement", DataConnector.FieldTypeIdString);
            svc05200.AddField("Replace_Item_Number", "Replace Item Number", DataConnector.FieldTypeIdString);
            svc05200.AddField("Replace_U_Of_M", "Replace U Of M", DataConnector.FieldTypeIdString);
            svc05200.AddField("Replace_Price_Level", "Replace Price Level", DataConnector.FieldTypeIdString);
            svc05200.AddField("Replace_QTY", "Replace QTY", DataConnector.FieldTypeIdQuantity);
            svc05200.AddField("Replace_Cost", "Replace Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Replace_Cost", "Originating Replace Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Replace_Price", "Replace Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Replace_Cost", "Originating Replace Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("SOP_Number_Invoice", "SOP Number Invoice", DataConnector.FieldTypeIdString);
            svc05200.AddField("Item_Number_Invoice", "Item Number Invoice", DataConnector.FieldTypeIdString);
            svc05200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc05200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc05200.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc05200.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc05200.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc05200.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc05200.AddField("Return_Item_Number", "Return Item Number", DataConnector.FieldTypeIdString);
            svc05200.AddField("Return_Item_Description", "Return Item Description", DataConnector.FieldTypeIdString);
            svc05200.AddField("Return_Location_Code", "Return Location Code", DataConnector.FieldTypeIdString);
            svc05200.AddField("Return_QTY", "Return QTY", DataConnector.FieldTypeIdQuantity);
            svc05200.AddField("Return_U_Of_M", "Return U Of M", DataConnector.FieldTypeIdString);
            svc05200.AddField("RETCOST", "Return Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Return_Cost", "Originating Return Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Extended_Return_Cost", "Extended Return Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Orig_Ext_Return_Cost", "Originating Extended Return Cost", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Return_Price_Level", "Return Price Level", DataConnector.FieldTypeIdString);
            svc05200.AddField("SVC_Return_Price", "Return Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Return_Price", "Originating Return Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Extended_Return_Pric", "Extended Return Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Orig_Ext_Return_Pric", "Originating Extended Return Price", DataConnector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            svc05200.AddField("SVC_SCM_Complete", "SCM Complete", DataConnector.FieldTypeIdInteger);

            var serviceRecordType = svc05200.AddField("SRVRECTYPE", "Service Record Type", DataConnector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var sopType = svc05200.AddField("SOPTYPE", "SOP Type", DataConnector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });

            var creditSopType = svc05200.AddField("Credit_SOP_Type", "Credit SOP Type", DataConnector.FieldTypeIdEnum);
            creditSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });

            var replaceSopType = svc05200.AddField("Replace_SOP_Type", "Replace SOP Type", DataConnector.FieldTypeIdEnum);
            replaceSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
        }

        public DataConnectorEntity GetHistoricalRmaEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListHistRma), "Historical RMAs", ParentConnector);

            var svc35000 = entity.AddTable("SVC35000");
            AddHistoricalRmaEntityFields(svc35000);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalRmaEntityFields(DataConnectorTable svc35000)
        {
            svc35000.AddField("RETDOCID", "Return Document ID", DataConnector.FieldTypeIdString, true);
            var returnRecordType = svc35000.AddField("Return_Record_Type", "Return Record Type", DataConnector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc35000.AddField("RMA_Status", "RMA Status", DataConnector.FieldTypeIdInteger, true);
            svc35000.AddField("Received", "Received", DataConnector.FieldTypeIdYesNo, true);
            var returnOrigin = svc35000.AddField("RETORIG", "Return Origin", DataConnector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc35000.AddField("RETREF", "Return Reference", DataConnector.FieldTypeIdString, true);
            svc35000.AddField("RETSTAT", "Return Status", DataConnector.FieldTypeIdString, true);
            svc35000.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString, true);
            svc35000.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc35000.AddField("ETADTE", "ETA Date", DataConnector.FieldTypeIdDate, true);
            svc35000.AddField("RETUDATE", "Return Date", DataConnector.FieldTypeIdDate, true);
            svc35000.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);
            svc35000.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString, true);
            svc35000.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc35000.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString, true);
            svc35000.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc35000.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc35000.AddField("ETATME", "ETA Time", DataConnector.FieldTypeIdTime);
            svc35000.AddField("Return_Time", "Return Time", DataConnector.FieldTypeIdTime);
            svc35000.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc35000.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            svc35000.AddField("RTRNNAME", "Return Address Name", DataConnector.FieldTypeIdString);
            svc35000.AddField("RETADDR1", "Return Address 1", DataConnector.FieldTypeIdString);
            svc35000.AddField("RETADDR2", "Return Address 2", DataConnector.FieldTypeIdString);
            svc35000.AddField("RETADDR3", "Return Address 3", DataConnector.FieldTypeIdString);
            svc35000.AddField("RTRNCITY", "Return Address City", DataConnector.FieldTypeIdString);
            svc35000.AddField("SVC_Return_State", "Return State", DataConnector.FieldTypeIdString);
            svc35000.AddField("RTRNZIP", "Return Address Zip Code", DataConnector.FieldTypeIdString);
            svc35000.AddField("Return_Country", "Return Country", DataConnector.FieldTypeIdString);
            svc35000.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            svc35000.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc35000.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc35000.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc35000.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc35000.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc35000.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc35000.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc35000.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc35000.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString);
            svc35000.AddField("EQPLINE", "Equipment Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("Bill_of_Lading", "Bill of Lading", DataConnector.FieldTypeIdString);
            svc35000.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc35000.AddField("SOPNUMBE", "SOP Number", DataConnector.FieldTypeIdString);
            svc35000.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("CMPNTSEQ", "Component Sequence", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc35000.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc35000.AddField("Commit_Date", "Commit Date", DataConnector.FieldTypeIdDate);
            svc35000.AddField("Commit_Time", "Commit Time", DataConnector.FieldTypeIdTime);
            svc35000.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc35000.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc35000.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc35000.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc35000.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc35000.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc35000.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc35000.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc35000.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc35000.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc35000.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc35000.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("ISMCTRX", "Is MC Trx", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc35000.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc35000.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc35000.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString);
            svc35000.AddField("SVC_RMA_Reason_Code", "RMA Reason Code", DataConnector.FieldTypeIdString);
            svc35000.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code Desc", DataConnector.FieldTypeIdString);
            svc35000.AddField("SVC_RMA_From_Service", "RMA From Service", DataConnector.FieldTypeIdString);
            svc35000.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdYesNo);

            var serviceRecordType = svc35000.AddField("SRVRECTYPE", "Service Record Type", DataConnector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var sopType = svc35000.AddField("SOPTYPE", "SOP Type", DataConnector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
        }

        public DataConnectorEntity GetHistoricalRmaLineEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListHistRmaLines), "Historical RMA lines", ParentConnector);

            var svc35200 = entity.AddTable("SVC35200");
            AddHistoricalRmaLineEntityFields(svc35200);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYS", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("ODECPLCU - 1", "Originating Decimal Places Currency", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalRmaLineEntityFields(DataConnectorTable svc35200)
        {
            svc35200.AddField("RETDOCID", "Return Document ID", DataConnector.FieldTypeIdString, true);
            var returnRecordType = svc35200.AddField("Return_Record_Type", "Return Record Type", DataConnector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc35200.AddField("LNSEQNBR", "Line SEQ Number", DataConnector.FieldTypeIdInteger, true);
            svc35200.AddField("RETTYPE", "Return Type", DataConnector.FieldTypeIdString, true);
            svc35200.AddField("RMA_Status", "RMA Status", DataConnector.FieldTypeIdInteger, true);
            svc35200.AddField("RETSTAT", "Return Status", DataConnector.FieldTypeIdString, true);
            svc35200.AddField("Received", "Received", DataConnector.FieldTypeIdYesNo, true);
            svc35200.AddField("SVC_Ready_To_Close", "Ready To Close", DataConnector.FieldTypeIdYesNo, true);
            svc35200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger, true);
            var returnOrigin = svc35200.AddField("RETORIG", "Return Origin", DataConnector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc35200.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc35200.AddField("ETADTE", "ETA Date", DataConnector.FieldTypeIdDate, true);
            svc35200.AddField("RETUDATE", "Return Date", DataConnector.FieldTypeIdDate, true);
            svc35200.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);
            svc35200.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString, true);
            svc35200.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc35200.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc35200.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svc35200.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency, true);
            svc35200.AddField("XTNDPRCE", "Extended Price", DataConnector.FieldTypeIdCurrency, true);

            svc35200.AddField("SVC_Next_Line_SEQ_Number", "Next Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("SVC_Prev_Line_SEQ_Number", "Prev Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("Traveler_Printed", "Traveler Printed", DataConnector.FieldTypeIdYesNo);
            svc35200.AddField("SVC_RMA_Reason_Code", "RMA Reason Code", DataConnector.FieldTypeIdString);
            svc35200.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code Description", DataConnector.FieldTypeIdString);
            svc35200.AddField("RETREF", "Return Reference", DataConnector.FieldTypeIdString);
            svc35200.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString);
            svc35200.AddField("EQPLINE", "Equipment Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("SVC_RMA_From_Service", "RMA From Service", DataConnector.FieldTypeIdYesNo);
            svc35200.AddField("SOPNUMBE", "SOP Number", DataConnector.FieldTypeIdString);
            svc35200.AddField("CMPNTSEQ", "Component Sequence", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc35200.AddField("ETATME", "ETA Time", DataConnector.FieldTypeIdTime);
            svc35200.AddField("Commit_Date", "Commit Date", DataConnector.FieldTypeIdDate);
            svc35200.AddField("Commit_Time", "Commit Time", DataConnector.FieldTypeIdTime);
            svc35200.AddField("Return_Time", "Return Time", DataConnector.FieldTypeIdTime);
            svc35200.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc35200.AddField("PRMDATE", "Promised Date", DataConnector.FieldTypeIdDate);
            svc35200.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svc35200.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            svc35200.AddField("RTRNNAME", "Return Address Name", DataConnector.FieldTypeIdString);
            svc35200.AddField("RETADDR1", "Return Address 1", DataConnector.FieldTypeIdString);
            svc35200.AddField("RETADDR2", "Return Address 2", DataConnector.FieldTypeIdString);
            svc35200.AddField("RETADDR3", "Return Address 3", DataConnector.FieldTypeIdString);
            svc35200.AddField("RTRNCITY", "Return Address City", DataConnector.FieldTypeIdString);
            svc35200.AddField("SVC_Return_State", "Return State", DataConnector.FieldTypeIdString);
            svc35200.AddField("RTRNZIP", "Return Address Zip Code", DataConnector.FieldTypeIdString);
            svc35200.AddField("Return_Country", "Return Country", DataConnector.FieldTypeIdString);
            svc35200.AddField("CUSTNMBR", "Customer ID", DataConnector.FieldTypeIdString);
            svc35200.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            svc35200.AddField("CONTACT", "Contact", DataConnector.FieldTypeIdString);
            svc35200.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            svc35200.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            svc35200.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            svc35200.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            svc35200.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            svc35200.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc35200.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            svc35200.AddField("Bill_To_Customer", "Bill To Customer ID", DataConnector.FieldTypeIdString);
            svc35200.AddField("SVC_Bill_To_Address_Code", "Bill To Address Code", DataConnector.FieldTypeIdString);
            svc35200.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString);
            svc35200.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity);
            svc35200.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString);
            svc35200.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString);
            svc35200.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("ORUNTCST", "Originating Unit Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("UNITPRCE", "Unit Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("ORUNTPRC", "Originating Unit Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("OXTNDPRC", "Originating Extended Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("CUSTOWN", "Customer Owned", DataConnector.FieldTypeIdYesNo);
            svc35200.AddField("FACTSEAL", "Factory Sealed", DataConnector.FieldTypeIdYesNo);
            svc35200.AddField("ORDDOCID", "Order Document ID", DataConnector.FieldTypeIdString);
            svc35200.AddField("TRANSLINESEQ", "Transfer Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("STATUS", "Transfer Status", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("Flat_Rate_Repair_Price", "Flat Rate Repair Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Orig_Flat_RepairPrice", "Originating Flat Rate Repair Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Repair_Price", "Repair Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Repair_Price", "Originating Repair Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("NTE_Price", "NTE Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_NTE_Price", "Originating NTE Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Repair_Cost", "Repair Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Repair_Cost", "Originating Repair Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Bill_of_Lading", "Bill of Lading", DataConnector.FieldTypeIdString);
            svc35200.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc35200.AddField("Credit_SOP_Number", "Credit SOP Number", DataConnector.FieldTypeIdString);
            svc35200.AddField("Credit_SOP_Line_Item_Seq", "Credit SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("Replace_SOP_Number", "Replace SOP Number", DataConnector.FieldTypeIdString);
            svc35200.AddField("Replace_SOP_Line_Item_Se", "Replace SOP Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("Location_Code_Replacemen", "Location Code Replacement", DataConnector.FieldTypeIdString);
            svc35200.AddField("Replace_Item_Number", "Replace Item Number", DataConnector.FieldTypeIdString);
            svc35200.AddField("Replace_U_Of_M", "Replace U Of M", DataConnector.FieldTypeIdString);
            svc35200.AddField("Replace_Price_Level", "Replace Price Level", DataConnector.FieldTypeIdString);
            svc35200.AddField("Replace_QTY", "Replace QTY", DataConnector.FieldTypeIdQuantity);
            svc35200.AddField("Replace_Cost", "Replace Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Replace_Cost", "Originating Replace Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Replace_Price", "Replace Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Replace_Cost", "Originating Replace Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("SOP_Number_Invoice", "SOP Number Invoice", DataConnector.FieldTypeIdString);
            svc35200.AddField("Item_Number_Invoice", "Item Number Invoice", DataConnector.FieldTypeIdString);
            svc35200.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc35200.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc35200.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc35200.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc35200.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc35200.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc35200.AddField("Return_Item_Number", "Return Item Number", DataConnector.FieldTypeIdString);
            svc35200.AddField("Return_Item_Description", "Return Item Description", DataConnector.FieldTypeIdString);
            svc35200.AddField("Return_Location_Code", "Return Location Code", DataConnector.FieldTypeIdString);
            svc35200.AddField("Return_QTY", "Return QTY", DataConnector.FieldTypeIdQuantity);
            svc35200.AddField("Return_U_Of_M", "Return U Of M", DataConnector.FieldTypeIdString);
            svc35200.AddField("RETCOST", "Return Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Return_Cost", "Originating Return Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Extended_Return_Cost", "Extended Return Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Orig_Ext_Return_Cost", "Originating Extended Return Cost", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Return_Price_Level", "Return Price Level", DataConnector.FieldTypeIdString);
            svc35200.AddField("SVC_Return_Price", "Return Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Return_Price", "Originating Return Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Extended_Return_Pric", "Extended Return Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Orig_Ext_Return_Pric", "Originating Extended Return Price", DataConnector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_FO_ID", "FO ID", DataConnector.FieldTypeIdString);
            svc35200.AddField("SVC_SCM_Complete", "SCM Complete", DataConnector.FieldTypeIdInteger);

            var serviceRecordType = svc35200.AddField("SRVRECTYPE", "Service Record Type", DataConnector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var sopType = svc35200.AddField("SOPTYPE", "SOP Type", DataConnector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });

            var creditSopType = svc35200.AddField("Credit_SOP_Type", "Credit SOP Type", DataConnector.FieldTypeIdEnum);
            creditSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });

            var replaceSopType = svc35200.AddField("Replace_SOP_Type", "Replace SOP Type", DataConnector.FieldTypeIdEnum);
            replaceSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
        }

        public DataConnectorEntity GetHistoricalRtvEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListHistRtv), "Historical RTVs", ParentConnector);

            var svc35600 = entity.AddTable("SVC35600");
            AddHistoricalRtvEntityFields(svc35600);

            return entity;
        }
        public void AddHistoricalRtvEntityFields(DataConnectorTable svc35600)
        {
            svc35600.AddField("RTV_Number", "RTV Number", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("RTV_Type", "RTV Type", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("RTV_Return_Status", "RTV Return Status", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("VRMA_Document_ID", "VRMA Document ID", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("RETDOCID", "RMA Number", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("VENDORID", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc35600.AddField("Shipped_Date", "Shipped Date", DataConnector.FieldTypeIdDate, true);
            svc35600.AddField("receiptdate", "Receipt Date", DataConnector.FieldTypeIdDate, true);
            svc35600.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);
            svc35600.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("Travel_Price", "Travel Price", DataConnector.FieldTypeIdCurrency, true);
            svc35600.AddField("Bill_of_Lading_Out", "Bill of Landing (Out)", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("Shipping_Method_Out", "Shipping Method (Out)", DataConnector.FieldTypeIdString, true);
            svc35600.AddField("VOIDSTTS", "Void Status", DataConnector.FieldTypeIdInteger, true);
            svc35600.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc35600.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_Name", "Ship Address Name", DataConnector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_1", "Ship Address 1", DataConnector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_2", "Ship Address 2", DataConnector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_3", "Ship Address 3", DataConnector.FieldTypeIdString);
            svc35600.AddField("Ship_City", "Ship City", DataConnector.FieldTypeIdString);
            svc35600.AddField("Ship_State", "Ship State", DataConnector.FieldTypeIdString);
            svc35600.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc35600.AddField("Ship_Country", "Ship Country", DataConnector.FieldTypeIdString);
            svc35600.AddField("ADRSCODE", "Entry Time", DataConnector.FieldTypeIdTime);
            svc35600.AddField("Ship_Address_Name", "Promised Date", DataConnector.FieldTypeIdDate);
            svc35600.AddField("Ship_Address_1", "Promised Time", DataConnector.FieldTypeIdTime);
            svc35600.AddField("Shipped_Time", "Shipped Time", DataConnector.FieldTypeIdTime);
            svc35600.AddField("Receipt_Time", "Receipt Time", DataConnector.FieldTypeIdTime);
            svc35600.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc35600.AddField("LOCCODEB", "Location Code Bad", DataConnector.FieldTypeIdString);
            svc35600.AddField("Part_Price", "Part Price", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Part_Cost", "Part Cost", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Labor_Price", "Labor Price", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Labor_Cost", "Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Expense_Price", "Expense Price", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Expense_Cost", "Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Travel_Cost", "Travel Cost", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Bill_of_Lading", "Bill of Landing", DataConnector.FieldTypeIdString);
            svc35600.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc35600.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString);
            svc35600.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc35600.AddField("VCHNUMWK", "Voucher Number (Work)", DataConnector.FieldTypeIdString);
            svc35600.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", DataConnector.FieldTypeIdString);
            svc35600.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", DataConnector.FieldTypeIdString);
            svc35600.AddField("CUSTOWN", "Customer Owned", DataConnector.FieldTypeIdYesNo);
            svc35600.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            svc35600.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc35600.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc35600.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc35600.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc35600.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc35600.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svc35600.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svc35600.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svc35600.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svc35600.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc35600.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svc35600.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svc35600.AddField("RATECALC", "Rate Calc Method", DataConnector.FieldTypeIdInteger);
            svc35600.AddField("VIEWMODE", "View Mode", DataConnector.FieldTypeIdInteger);
            svc35600.AddField("ISMCTRX", "IS MC Trx", DataConnector.FieldTypeIdInteger);
            svc35600.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svc35600.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svc35600.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            svc35600.AddField("Originating_Part_Price", "Originating Part Price", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Part_Cost", "Originating Part Cost", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Labor_Price", "Originating Labor Price", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Labor_Cost", "Originating Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_ExpensePrice", "Originating Expense Price", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Expense_Cost", "Originating Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Travel_Price", "Originating Travel Price", DataConnector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Travel_Cost", "Originating Travel Cost", DataConnector.FieldTypeIdCurrency);

            var rtvStatus = svc35600.AddField("RTV_Status", "RTV Status", DataConnector.FieldTypeIdEnum);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
        }

        public DataConnectorEntity GetRtvLineEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListRtvLines), "RTV lines", ParentConnector);

            var svc05601 = entity.AddTable("SVC05601");
            AddRtvLineEntityFields(svc05601);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYs", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRtvLineEntityFields(DataConnectorTable svc05601)
        {
            svc05601.AddField("RTV_Number", "RTV Number", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("RTV_Line", "RTV Line", DataConnector.FieldTypeIdInteger, true);
            svc05601.AddField("RTV_Type", "RTV Type", DataConnector.FieldTypeIdString, true);
            var rtvStatus = svc05601.AddField("RTV_Status", "RTV Status", DataConnector.FieldTypeIdEnum, true);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            svc05601.AddField("RTV_Return_Status", "RTV Return Status", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity, true);
            svc05601.AddField("QTYSHPPD", "QTY Shipped", DataConnector.FieldTypeIdQuantity, true);
            svc05601.AddField("QTYRECVD", "QTY Received", DataConnector.FieldTypeIdQuantity, true);
            svc05601.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("VNDITNUM", "Vendor Item number", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("Return_Item_Number", "Return Item Number", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("LOCCODEB", "Location Code Bad", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc05601.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc05601.AddField("Shipped_Date", "Shipped Date", DataConnector.FieldTypeIdDate, true);
            svc05601.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);

            svc05601.AddField("VRMA_Document_ID", "VRMA Document ID", DataConnector.FieldTypeIdString);
            svc05601.AddField("RETDOCID", "RMA Number", DataConnector.FieldTypeIdString);
            svc05601.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("SVC_Process_SEQ_Number", "Process Sequence Number", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svc05601.AddField("Reference2", "Reference 2", DataConnector.FieldTypeIdString);
            svc05601.AddField("QTY_To_Receive", "QTY To Receive", DataConnector.FieldTypeIdQuantity);
            svc05601.AddField("QTYCANCE", "QTY Canceled", DataConnector.FieldTypeIdQuantity);
            svc05601.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_Name", "Ship Address Name", DataConnector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_1", "Ship Address 1", DataConnector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_2", "Ship Address 2", DataConnector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_3", "Ship Address 3", DataConnector.FieldTypeIdString);
            svc05601.AddField("Ship_City", "Ship City", DataConnector.FieldTypeIdString);
            svc05601.AddField("Ship_State", "Ship State", DataConnector.FieldTypeIdString);
            svc05601.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc05601.AddField("Ship_Country", "Ship Country", DataConnector.FieldTypeIdString);
            svc05601.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc05601.AddField("PRMDATE", "Promised Date", DataConnector.FieldTypeIdDate);
            svc05601.AddField("Promised_Time", "Promised Time", DataConnector.FieldTypeIdTime);
            svc05601.AddField("Shipped_Time", "Shipped Time", DataConnector.FieldTypeIdTime);
            svc05601.AddField("receiptdate", "Receipt Date", DataConnector.FieldTypeIdDate);
            svc05601.AddField("Receipt_Time", "Receipt Time", DataConnector.FieldTypeIdTime);
            svc05601.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc05601.AddField("PONMBRSTR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc05601.AddField("POLNSEQ", "Purchase Order Line SEQ", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("POPRCTNM", "POP Receipt Number", DataConnector.FieldTypeIdString);
            svc05601.AddField("RCPTLNNM", "Receipt Line Number", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("Transfer_Reference", "Transfer Reference", DataConnector.FieldTypeIdString);
            svc05601.AddField("TRANSLINESEQ", "Transfer Line Sequence", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString);
            svc05601.AddField("EQPLINE", "Equipment Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("LINITMTYP", "Line Item Type", DataConnector.FieldTypeIdString);
            svc05601.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("Bill_of_Lading_Out", "Bill of Lading (Out)", DataConnector.FieldTypeIdString);
            svc05601.AddField("Shipping_Method_Out", "Shipping Method (Out)", DataConnector.FieldTypeIdString);
            svc05601.AddField("Bill_of_Lading", "Bill of Lading", DataConnector.FieldTypeIdString);
            svc05601.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc05601.AddField("Tracking_Number", "Tracking Number", DataConnector.FieldTypeIdString);
            svc05601.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc05601.AddField("VCHNUMWK", "Voucher Number (Work)", DataConnector.FieldTypeIdString);
            svc05601.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", DataConnector.FieldTypeIdString);
            svc05601.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", DataConnector.FieldTypeIdString);
            svc05601.AddField("CUSTOWN", "Customer Owned", DataConnector.FieldTypeIdYesNo);
            svc05601.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc05601.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc05601.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc05601.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc05601.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc05601.AddField("Part_Price", "Part Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Part_Cost", "Part Cost", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Labor_Price", "Labor Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Labor_Cost", "Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Expense_Price", "Expense Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Expense_Cost", "Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Travel_Price", "Travel Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Travel_Cost", "Travel Cost", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Part_Price", "Originating Part Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Part_Cost", "Originating Part Cost", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Labor_Price", "Originating Labor Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Labor_Cost", "Originating Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_ExpensePrice", "Originating Expense Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Expense_Cost", "Originating Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Travel_Price", "Originating Travel Price", DataConnector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Travel_Cost", "Originating Travel Cost", DataConnector.FieldTypeIdCurrency);

            var itemTrackingOption = svc05601.AddField(" SVC05601.ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
        }

        public DataConnectorEntity GetHistoricalRtvLineEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FieldServiceSmartListHistRtvLines), "Historical RTV lines", ParentConnector);

            var svc35601 = entity.AddTable("SVC35601");
            AddHistoricalRtvLineEntityFields(svc35601);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYs", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalRtvLineEntityFields(DataConnectorTable svc35601)
        {
            svc35601.AddField("RTV_Number", "RTV Number", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("RTV_Line", "RTV Line", DataConnector.FieldTypeIdInteger, true);
            svc35601.AddField("RTV_Type", "RTV Type", DataConnector.FieldTypeIdString, true);
            var rtvStatus = svc35601.AddField("RTV_Status", "RTV Status", DataConnector.FieldTypeIdEnum, true);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            svc35601.AddField("RTV_Return_Status", "RTV Return Status", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity, true);
            svc35601.AddField("QTYSHPPD", "QTY Shipped", DataConnector.FieldTypeIdQuantity, true);
            svc35601.AddField("QTYRECVD", "QTY Received", DataConnector.FieldTypeIdQuantity, true);
            svc35601.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("VNDITNUM", "Vendor Item number", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("Return_Item_Number", "Return Item Number", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("LOCCODEB", "Location Code Bad", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString, true);
            svc35601.AddField("ENTDTE", "Entry Date", DataConnector.FieldTypeIdDate, true);
            svc35601.AddField("Shipped_Date", "Shipped Date", DataConnector.FieldTypeIdDate, true);
            svc35601.AddField("COMPDTE", "Complete Date", DataConnector.FieldTypeIdDate, true);

            svc35601.AddField("VRMA_Document_ID", "VRMA Document ID", DataConnector.FieldTypeIdString);
            svc35601.AddField("RETDOCID", "RMA Number", DataConnector.FieldTypeIdString);
            svc35601.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("SVC_Process_SEQ_Number", "Process Sequence Number", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svc35601.AddField("Reference2", "Reference 2", DataConnector.FieldTypeIdString);
            svc35601.AddField("QTY_To_Receive", "QTY To Receive", DataConnector.FieldTypeIdQuantity);
            svc35601.AddField("QTYCANCE", "QTY Canceled", DataConnector.FieldTypeIdQuantity);
            svc35601.AddField("OFFID", "Office ID", DataConnector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_Name", "Ship Address Name", DataConnector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_1", "Ship Address 1", DataConnector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_2", "Ship Address 2", DataConnector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_3", "Ship Address 3", DataConnector.FieldTypeIdString);
            svc35601.AddField("Ship_City", "Ship City", DataConnector.FieldTypeIdString);
            svc35601.AddField("Ship_State", "Ship State", DataConnector.FieldTypeIdString);
            svc35601.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            svc35601.AddField("Ship_Country", "Ship Country", DataConnector.FieldTypeIdString);
            svc35601.AddField("ENTTME", "Entry Time", DataConnector.FieldTypeIdTime);
            svc35601.AddField("PRMDATE", "Promised Date", DataConnector.FieldTypeIdDate);
            svc35601.AddField("Promised_Time", "Promised Time", DataConnector.FieldTypeIdTime);
            svc35601.AddField("Shipped_Time", "Shipped Time", DataConnector.FieldTypeIdTime);
            svc35601.AddField("receiptdate", "Receipt Date", DataConnector.FieldTypeIdDate);
            svc35601.AddField("Receipt_Time", "Receipt Time", DataConnector.FieldTypeIdTime);
            svc35601.AddField("COMPTME", "Complete Time", DataConnector.FieldTypeIdTime);
            svc35601.AddField("PONMBRSTR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            svc35601.AddField("POLNSEQ", "Purchase Order Line SEQ", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("POPRCTNM", "POP Receipt Number", DataConnector.FieldTypeIdString);
            svc35601.AddField("RCPTLNNM", "Receipt Line Number", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("Transfer_Reference", "Transfer Reference", DataConnector.FieldTypeIdString);
            svc35601.AddField("TRANSLINESEQ", "Transfer Line Sequence", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("CALLNBR", "Service Call Number", DataConnector.FieldTypeIdString);
            svc35601.AddField("EQPLINE", "Equipment Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("LINITMTYP", "Line Item Type", DataConnector.FieldTypeIdString);
            svc35601.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("Bill_of_Lading_Out", "Bill of Lading (Out)", DataConnector.FieldTypeIdString);
            svc35601.AddField("Shipping_Method_Out", "Shipping Method (Out)", DataConnector.FieldTypeIdString);
            svc35601.AddField("Bill_of_Lading", "Bill of Lading", DataConnector.FieldTypeIdString);
            svc35601.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            svc35601.AddField("Tracking_Number", "Tracking Number", DataConnector.FieldTypeIdString);
            svc35601.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svc35601.AddField("VCHNUMWK", "Voucher Number (Work)", DataConnector.FieldTypeIdString);
            svc35601.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", DataConnector.FieldTypeIdString);
            svc35601.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", DataConnector.FieldTypeIdString);
            svc35601.AddField("CUSTOWN", "Customer Owned", DataConnector.FieldTypeIdYesNo);
            svc35601.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svc35601.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svc35601.AddField("USRDEF03", "User Defined 3", DataConnector.FieldTypeIdString);
            svc35601.AddField("USRDEF04", "User Defined 4", DataConnector.FieldTypeIdString);
            svc35601.AddField("USRDEF05", "User Defined 5", DataConnector.FieldTypeIdString);
            svc35601.AddField("Part_Price", "Part Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Part_Cost", "Part Cost", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Labor_Price", "Labor Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Labor_Cost", "Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Expense_Price", "Expense Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Expense_Cost", "Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Travel_Price", "Travel Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Travel_Cost", "Travel Cost", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Part_Price", "Originating Part Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Part_Cost", "Originating Part Cost", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Labor_Price", "Originating Labor Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Labor_Cost", "Originating Labor Cost", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_ExpensePrice", "Originating Expense Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Expense_Cost", "Originating Expense Cost", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Travel_Price", "Originating Travel Price", DataConnector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Travel_Cost", "Originating Travel Cost", DataConnector.FieldTypeIdCurrency);

            var itemTrackingOption = svc35601.AddField(" svc35601.ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
        }

    }
}
