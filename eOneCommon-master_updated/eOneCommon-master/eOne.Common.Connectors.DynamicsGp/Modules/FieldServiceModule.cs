using System.Collections.Generic;

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

        public ConnectorEntity GetServiceCallEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListFieldServiceCalls), "Service calls", ParentConnector);

            var svc00200 = entity.AddTable("SVC00200");

            var svc00202 = entity.AddScript("(select SRVRECTYPE, CALLNBR, EQUIPID from {0}..SVC00202 with (NOLOCK) where EQPLINE = 1)", "SVC00202", "SVC00200");
            svc00202.AddJoinFields("SRVRECTYPE", "SRVRECTYPE");
            svc00202.AddJoinFields("CALLNBR", "CALLNBR");

            var svc00300 = entity.AddTable("SVC00300", "SVC00202");
            svc00300.AddJoinFields("EQUIPID", "EQUIPID");

            AddServiceCallEntityFields(svc00200, svc00300);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddServiceCallEntityFields(ConnectorTable svc00200, ConnectorTable svc00300)
        {
            svc00200.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString, true);
            var serviceRecordType = svc00200.AddField("SRVRECTYPE", "Service Record type", Connector.FieldTypeIdEnum, true);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
            svc00200.AddField("SRVSTAT", "Service Call status", Connector.FieldTypeIdString, true);
            svc00200.AddField("SRVTYPE", "Service type", Connector.FieldTypeIdString, true);
            svc00200.AddField("SVCDESCR", "Service description", Connector.FieldTypeIdString, true);
            svc00200.AddField("priorityLevel", "priorityLevel", Connector.FieldTypeIdInteger, true);
            svc00200.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc00200.AddField("Customer_Reference", "Customer Reference", Connector.FieldTypeIdString, true);
            svc00200.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc00200.AddField("CUSTNAME", "Name", Connector.FieldTypeIdString, true);
            svc00200.AddField("OFFID", "Office ID", Connector.FieldTypeIdString, true);
            svc00200.AddField("SVCAREA", "Service Area", Connector.FieldTypeIdString, true);
            svc00200.AddField("TECHID", "Tech ID", Connector.FieldTypeIdString, true);
            svc00200.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc00200.AddField("TOTAL", "Total", Connector.FieldTypeIdCurrency, true);
            svc00200.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc00200.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc00200.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc00200.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc00200.AddField("ZIP", "Zip", Connector.FieldTypeIdString);
            svc00200.AddField("CNTCPRSN", "Contact Person", Connector.FieldTypeIdString);
            svc00200.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            svc00200.AddField("TIMEZONE", "TimeZone", Connector.FieldTypeIdString);
            svc00200.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc00200.AddField("Notify_date", "Notify date", Connector.FieldTypeIdDate);
            svc00200.AddField("Notify_Time", "Notify time", Connector.FieldTypeIdTime);
            svc00200.AddField("ETADTE", "ETA date", Connector.FieldTypeIdDate);
            svc00200.AddField("ETATME", "ETA time", Connector.FieldTypeIdTime);
            svc00200.AddField("DISPDTE", "Dispatch date", Connector.FieldTypeIdDate);
            svc00200.AddField("DISPTME", "Dispatch time", Connector.FieldTypeIdTime);
            svc00200.AddField("ARRIVDTE", "Arrival date", Connector.FieldTypeIdDate);
            svc00200.AddField("ARRIVTME", "Arrival time", Connector.FieldTypeIdTime);
            svc00200.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate);
            svc00200.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc00200.AddField("Response_date", "Response date", Connector.FieldTypeIdDate);
            svc00200.AddField("Response_Time", "Response time", Connector.FieldTypeIdTime);
            svc00200.AddField("PRICELVL", "Price Level", Connector.FieldTypeIdString);
            svc00200.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            svc00200.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svc00200.AddField("LABSTOTPRC", "Labor Sub Total price", Connector.FieldTypeIdCurrency);
            svc00200.AddField("LABPCT", "Labor Pct", Connector.FieldTypeIdPercentage);
            svc00200.AddField("LABSTOTCST", "Labor Sub Total cost", Connector.FieldTypeIdCurrency);
            svc00200.AddField("PARSTOTPRC", "Part Sub Total price", Connector.FieldTypeIdCurrency);
            svc00200.AddField("PARTPCT", "Part Pct", Connector.FieldTypeIdPercentage);
            svc00200.AddField("PARSTOTCST", "Part Sub Total cost", Connector.FieldTypeIdCurrency);
            svc00200.AddField("MSCSTOTPRC", "Misc Sub Total price", Connector.FieldTypeIdCurrency);
            svc00200.AddField("MISCPCT", "Misc Pct", Connector.FieldTypeIdPercentage);
            svc00200.AddField("MISSTOTCST", "Misc Sub Total cost", Connector.FieldTypeIdCurrency);
            svc00200.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svc00200.AddField("TAXEXMT1", "Tax Exempt 1", Connector.FieldTypeIdString);
            svc00200.AddField("TAXEXMT2", "Tax Exempt 2", Connector.FieldTypeIdString);
            svc00200.AddField("PRETAXTOT", "PreTax total", Connector.FieldTypeIdCurrency);
            svc00200.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svc00200.AddField("Invoiced_Amount", "Invoiced amount", Connector.FieldTypeIdCurrency);
            svc00200.AddField("METER1", "Meter 1", Connector.FieldTypeIdInteger);
            svc00200.AddField("METER2", "Meter 2", Connector.FieldTypeIdInteger);
            svc00200.AddField("METER3", "Meter 3", Connector.FieldTypeIdInteger);
            svc00200.AddField("PORDNMBR", "Purchase Order number", Connector.FieldTypeIdString);
            svc00200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc00200.AddField("NOTFYFLAG", "Notify Flag", Connector.FieldTypeIdYesNo);
            svc00200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc00200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc00200.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc00200.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc00200.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc00200.AddField("MSTRCALLNBR", "Master Service Call number", Connector.FieldTypeIdString);
            svc00200.AddField("ESCdate", "Escalation date", Connector.FieldTypeIdDate);
            svc00200.AddField("ESCTIME", "Escalation time", Connector.FieldTypeIdTime);
            svc00200.AddField("Escalation_Level", "Escalation Level", Connector.FieldTypeIdInteger);
            svc00200.AddField("SPLTTERMS", "Split Terms code", Connector.FieldTypeIdString);
            svc00200.AddField("Callback", "Callback", Connector.FieldTypeIdYesNo);
            svc00200.AddField("PROJCTID", "Project ID", Connector.FieldTypeIdString);
            svc00200.AddField("ProjectRef1_1", "ProjectRef1", Connector.FieldTypeIdString);
            svc00200.AddField("CONTNBR", "Contract number", Connector.FieldTypeIdString);
            svc00200.AddField("SVC_Contract_Line_SEQ", "Contract Line Sequence number", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ETTR", "ETTR", Connector.FieldTypeIdCurrency);
            svc00200.AddField("SVC_On_Hold", "On Hold", Connector.FieldTypeIdYesNo);
            svc00200.AddField("Print_to_Web", "Print to Web", Connector.FieldTypeIdYesNo);
            svc00200.AddField("Meters_1", "Meters", Connector.FieldTypeIdInteger);
            svc00200.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc00200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc00200.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc00200.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc00200.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc00200.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc00200.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc00200.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc00200.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc00200.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc00200.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc00200.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc00200.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc00200.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc00200.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc00200.AddField("ORIGMISSTOTCST", "Originating Misc Sub Total cost", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGMSCSTOTPRC", "Originating Misc Sub Total price", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGLABSUBTOTCOST", "Originating Labor Sub Total cost", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGLABSTOTPRC", "Originating Labor Sub Total price", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGPARSTOTCST", "Originating Part Sub Total cost", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGPARSTOTPRC", "Originating Part Sub Total price", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGPRETAXTOT", "Originating PreTax total", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ORIGTOTAL", "Originating Service total", Connector.FieldTypeIdCurrency);
            svc00200.AddField("Orig_Invoiced_Amount", "Originating Invoiced amount", Connector.FieldTypeIdCurrency);
            svc00200.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc00200.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc00200.AddField("SVC_Pre600", "SVC_Pre600", Connector.FieldTypeIdYesNo);
            svc00200.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            svc00200.AddField("Replaces_1", "Replaces", Connector.FieldTypeIdString);
            svc00300.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString);
            svc00300.AddField("SERLNMBR", "Equipment/Serial number", Connector.FieldTypeIdString);

            var masterServiceRecordType = svc00200.AddField("MSTRRECTYPE", "Master Service Record type", Connector.FieldTypeIdEnum);
            masterServiceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var contractRecordType = svc00200.AddField("CONSTS", "Contract Record type", Connector.FieldTypeIdEnum);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
        }

        public ConnectorEntity GetEquipmentEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListEquipment), "Equipment", ParentConnector);

            var svc00300 = entity.AddTable("SVC00300");

            var rm00101 = entity.AddTable("RM00101", "SVC00300");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddEquipmentEntityFields(svc00300, rm00101);

            return entity;
        }
        public void AddEquipmentEntityFields(ConnectorTable svc00300, ConnectorTable rm00101)
        {
            svc00300.AddField("EQUIPID", "Equipment ID", Connector.FieldTypeIdInteger, true);
            svc00300.AddField("SERLNMBR", "Serial number", Connector.FieldTypeIdString, true);
            svc00300.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svc00300.AddField("SRLSTAT", "Equipment status", Connector.FieldTypeIdString, true);
            svc00300.AddField("INSTDTE", "Install date", Connector.FieldTypeIdDate, true);
            svc00300.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc00300.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc00300.AddField("SVC_Serial_ID", "Serial ID", Connector.FieldTypeIdString, true);
            svc00300.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            svc00300.AddField("SVC_Asset_Tag", "Asset Tag", Connector.FieldTypeIdString,true);
            svc00300.AddField("Shipped_date", "Shipped date", Connector.FieldTypeIdDate, true);
            svc00300.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svc00300.AddField("Version", "Version", Connector.FieldTypeIdString);
            svc00300.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc00300.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc00300.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc00300.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc00300.AddField("ZIP", "Zip", Connector.FieldTypeIdString);
            svc00300.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc00300.AddField("CNTCPRSN", "Contact Person", Connector.FieldTypeIdString);
            svc00300.AddField("LSTPMDTE", "Last PM date", Connector.FieldTypeIdDate);
            svc00300.AddField("LSTSRVDTE", "Last Service date", Connector.FieldTypeIdDate);
            svc00300.AddField("TECHID", "Tech ID", Connector.FieldTypeIdString);
            svc00300.AddField("TECHID2", "Tech ID 2", Connector.FieldTypeIdString);
            svc00300.AddField("OFFID", "Office ID", Connector.FieldTypeIdString);
            svc00300.AddField("SVCAREA", "Service Area", Connector.FieldTypeIdString);
            svc00300.AddField("TIMEZONE", "TimeZone", Connector.FieldTypeIdString);
            svc00300.AddField("WARRCDE", "Warranty code", Connector.FieldTypeIdString);
            svc00300.AddField("WARREND", "Warranty End", Connector.FieldTypeIdDate);
            svc00300.AddField("WARRSTART", "Warranty Start", Connector.FieldTypeIdDate);
            svc00300.AddField("SLRWARR", "Seller Warranty code", Connector.FieldTypeIdString);
            svc00300.AddField("SLRWEND", "Seller Warranty End", Connector.FieldTypeIdDate);
            svc00300.AddField("SLRWSTART", "Seller Warranty Start", Connector.FieldTypeIdDate);
            svc00300.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc00300.AddField("MTTR", "MTTR", Connector.FieldTypeIdInteger);
            svc00300.AddField("MTBF", "MTBF", Connector.FieldTypeIdInteger);
            svc00300.AddField("MTBI", "MTBI", Connector.FieldTypeIdInteger);
            svc00300.AddField("Last_Calc_date", "Last Calc date", Connector.FieldTypeIdDate);
            svc00300.AddField("Meters_1", "Meters", Connector.FieldTypeIdInteger);
            svc00300.AddField("Dailys_1", "Dailys", Connector.FieldTypeIdInteger);
            svc00300.AddField("MTBFs_1", "MTBFs", Connector.FieldTypeIdInteger);
            svc00300.AddField("Meter_Deltas_1", "Meter Deltas", Connector.FieldTypeIdInteger);
            var pmMonth = svc00300.AddField("SVC_PM_date", "PM Month", Connector.FieldTypeIdEnum);
            pmMonth.AddListItems(1, new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            svc00300.AddField("SVC_PM_Day", "PM Day", Connector.FieldTypeIdInteger);
            svc00300.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString);
            svc00300.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc00300.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc00300.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc00300.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc00300.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc00300.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc00300.AddField("SVC_Register_date", "Register date", Connector.FieldTypeIdDate);
            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
        }

        public ConnectorEntity GetContractEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListContracts), "Contracts", ParentConnector);

            var svc00600 = entity.AddTable("SVC00600");

            var rm00101 = entity.AddTable("RM00101", "SVC00600");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddContractEntityFields(svc00600, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddContractEntityFields(ConnectorTable svc00600, ConnectorTable rm00101)
        {
            var contractRecordType = svc00600.AddField("CONSTS", "Contract Record type", Connector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc00600.AddField("CONTNBR", "Contract number", Connector.FieldTypeIdString, true);
            svc00600.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc00600.AddField("BILSTRT", "Bill Start", Connector.FieldTypeIdDate, true);
            svc00600.AddField("BILEND", "Bill End", Connector.FieldTypeIdDate, true);
            svc00600.AddField("BILLNGTH", "Bill Length", Connector.FieldTypeIdInteger, true);
            var billPeriod = svc00600.AddField("BILPRD", "Bill period", Connector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc00600.AddField("TOTAL", "Total", Connector.FieldTypeIdCurrency, true);
            svc00600.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc00600.AddField("BILONDY", "Bill On Day", Connector.FieldTypeIdInteger, true);
            var billingCycle = svc00600.AddField("BILCYC", "Billing Cycle", Connector.FieldTypeIdEnum, true); 
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc00600.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate, true);
            svc00600.AddField("ENDdate", "End date", Connector.FieldTypeIdDate, true);
            var liabilityType = svc00600.AddField("SVC_Liability_Type", "Liability type", Connector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block time", "Retainage", "Based on Calls", "Metered" });
            svc00600.AddField("Contract_Transfer_date", "Contract Transfer date", Connector.FieldTypeIdDate);
            svc00600.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svc00600.AddField("priorityLevel", "priorityLevel", Connector.FieldTypeIdInteger);
            svc00600.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc00600.AddField("TIMEZONE", "TimeZone", Connector.FieldTypeIdString);
            svc00600.AddField("CONTPRC", "Contract price", Connector.FieldTypeIdCurrency);
            svc00600.AddField("RENPRCSCHD", "Renewing Price Schedule", Connector.FieldTypeIdString);
            svc00600.AddField("PCTCRYFWD", "Percentage Carried Forward", Connector.FieldTypeIdPercentage);
            svc00600.AddField("FRZEND", "Frozen End", Connector.FieldTypeIdDate);
            svc00600.AddField("FRXSTRT", "Frozen Start", Connector.FieldTypeIdDate);
            svc00600.AddField("MXINCPCT", "Max Increase Percentage", Connector.FieldTypeIdPercentage);
            svc00600.AddField("BLKTIM", "Blocked time", Connector.FieldTypeIdCurrency);
            svc00600.AddField("VALTIM", "Value of time", Connector.FieldTypeIdCurrency);
            svc00600.AddField("DSCPCTAM", "Discount Percent amount", Connector.FieldTypeIdPercentage);
            svc00600.AddField("COMDLRAM", "Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc00600.AddField("PRCSTAT", "Status of price", Connector.FieldTypeIdString);
            svc00600.AddField("PORDNMBR", "Purchase Order number", Connector.FieldTypeIdString);
            svc00600.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            svc00600.AddField("PARTPCT", "Part Pct", Connector.FieldTypeIdPercentage);
            svc00600.AddField("LABPCT", "Labor Pct", Connector.FieldTypeIdPercentage);
            svc00600.AddField("MISCPCT", "Misc Pct", Connector.FieldTypeIdPercentage);
            svc00600.AddField("PMMSCPCT", "PM Misc Pct", Connector.FieldTypeIdPercentage);
            svc00600.AddField("PMPRTPCT", "PM Part Pct", Connector.FieldTypeIdPercentage);
            svc00600.AddField("PMLABPCT", "PM Labor Pct", Connector.FieldTypeIdPercentage);
            svc00600.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString);
            svc00600.AddField("RETNAGAM", "Retainage amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("RTNBILLD", "Retainage Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svc00600.AddField("COMMCODE", "Commission code", Connector.FieldTypeIdString);
            svc00600.AddField("COMPRCNT", "Commission Percent", Connector.FieldTypeIdPercentage);
            svc00600.AddField("FRSTBLDTE", "First Bill date", Connector.FieldTypeIdDate);
            svc00600.AddField("Last_Amount_Billed", "Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("LSTBLDTE", "Last Bill date", Connector.FieldTypeIdDate);
            svc00600.AddField("NBRCAL", "Max Number of Calls", Connector.FieldTypeIdInteger);
            svc00600.AddField("ACTCAL", "Actual Number of Calls", Connector.FieldTypeIdInteger);
            svc00600.AddField("TOTVALCAL", "Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc00600.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc00600.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc00600.AddField("NXTBLDTE", "Next Bill date", Connector.FieldTypeIdDate);
            svc00600.AddField("CNTTYPE", "Contract type", Connector.FieldTypeIdString);
            svc00600.AddField("PRICSHED", "Price Schedule", Connector.FieldTypeIdString);
            svc00600.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate);
            svc00600.AddField("MINBIL", "Min Billable Call", Connector.FieldTypeIdCurrency);
            svc00600.AddField("MAXBIL", "Max Billable Call", Connector.FieldTypeIdCurrency);
            svc00600.AddField("MAXBILL", "Max Billable", Connector.FieldTypeIdCurrency);
            svc00600.AddField("AUTOREN", "Auto Renewing", Connector.FieldTypeIdYesNo);
            svc00600.AddField("MSTCNTRCT", "Master Contract number", Connector.FieldTypeIdString);
            svc00600.AddField("SRVTYPE", "Service type", Connector.FieldTypeIdString);
            svc00600.AddField("BILFRRET", "Bill For Retainer", Connector.FieldTypeIdYesNo);
            svc00600.AddField("TOTBIL", "Total Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("PREPAID", "Pre Paid", Connector.FieldTypeIdYesNo);
            svc00600.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svc00600.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc00600.AddField("Contract_Length", "Contract Length", Connector.FieldTypeIdInteger);
            svc00600.AddField("Invoiced_Amount", "Invoiced amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Liabiltiy_Reduction", "Liability Reduction", Connector.FieldTypeIdYesNo);
            svc00600.AddField("Amount_To_Invoice", "Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Liability_Amount", "Liability amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Total_Liability_Amount", "Total Liability amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("NUMOFINV", "Number of Invoices", Connector.FieldTypeIdInteger);
            svc00600.AddField("Quote_Status", "Quote status", Connector.FieldTypeIdInteger);
            svc00600.AddField("QUOEXPDA", "Quote Expiration date", Connector.FieldTypeIdDate);
            svc00600.AddField("Credit_Hold", "Credit Hold", Connector.FieldTypeIdYesNo);
            svc00600.AddField("TAXEXMT1", "Tax Exempt 1", Connector.FieldTypeIdString);
            svc00600.AddField("TAXEXMT2", "Tax Exempt 2", Connector.FieldTypeIdString);
            svc00600.AddField("New_PO_Required", "New PO Required", Connector.FieldTypeIdYesNo);
            svc00600.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", Connector.FieldTypeIdString);
            svc00600.AddField("Source_Contract_Number", "Source Contract number", Connector.FieldTypeIdString);
            svc00600.AddField("Contract_Response_Time", "Contract Response time", Connector.FieldTypeIdString);
            svc00600.AddField("Liability_Months", "Liability Months", Connector.FieldTypeIdInteger);
            svc00600.AddField("Next_Liability_date", "Next Liability date", Connector.FieldTypeIdDate);
            svc00600.AddField("Last_Liability_date", "Last Liability date", Connector.FieldTypeIdDate);
            svc00600.AddField("Total_Liability_Billed", "Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Total_Unit", "Total Unit", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Created_User_ID", "SVC_Created User ID", Connector.FieldTypeIdString);
            svc00600.AddField("Source_User_ID", "Source User ID", Connector.FieldTypeIdString);
            svc00600.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", Connector.FieldTypeIdDate);
            svc00600.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString);
            svc00600.AddField("Location_Segment", "Location Segment", Connector.FieldTypeIdString);
            svc00600.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc00600.AddField("SVC_Invoice_Detail", "Invoice Detail", Connector.FieldTypeIdYesNo);
            svc00600.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc00600.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc00600.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc00600.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc00600.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc00600.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc00600.AddField("DSCDLRAM", "Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("SVC_Paid_Contract", "Paid Contract", Connector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_Discount_Recognized", "Discount Recognized", Connector.FieldTypeIdCurrency);
            svc00600.AddField("SVC_Discount_Remaining", "Discount Remaining", Connector.FieldTypeIdCurrency);
            svc00600.AddField("SVC_Discount_Next", "Discount Next", Connector.FieldTypeIdCurrency);
            svc00600.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc00600.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc00600.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc00600.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc00600.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc00600.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc00600.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc00600.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc00600.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc00600.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc00600.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc00600.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc00600.AddField("ORIGVALTIM", "Originating Value of time", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORCOMAMT", "Originating Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGRETNAGAM", "Originating Retainage amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGRTNBILLD", "Originating Retainage Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGTOTAL", "Originating Service total", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGMINBIL", "Originating Min Billable Call", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGMAXBIL", "Originating Max Billable Call", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Originating_Max_Billable", "Originating Max Billable", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORIGTOTBIL", "Originating Total Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_Invoiced_Amount", "Originating Invoiced amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_Liability_Amount", "Originating Liability amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("OrigTotLiabilityAmount", "Originating Total Liability amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Originating_Total_Unit", "Originating Total Unit", Connector.FieldTypeIdCurrency);
            svc00600.AddField("ORDDLRAT", "Originating Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc00600.AddField("OrigDiscountReceived", "Originating Discount Recognized", Connector.FieldTypeIdCurrency);
            svc00600.AddField("OrigDiscountRemaining", "Originating Discount Remaining", Connector.FieldTypeIdCurrency);
            svc00600.AddField("OrigDiscountNext", "Originating Discount Next", Connector.FieldTypeIdCurrency);
            svc00600.AddField("SmoothInvoiceCalc", "Smooth Invoice Calculation", Connector.FieldTypeIdYesNo);
            svc00600.AddField("SmoothRevenueCalc", "Smooth Revenue Calculation", Connector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_Use_Same_Number", "Use Same number", Connector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            svc00600.AddField("SVC_Invoiced_Cost", "Invoiced cost", Connector.FieldTypeIdCurrency);
            svc00600.AddField("Orig_SVC_Invoiced_Cost", "Originating SVC Invoiced cost", Connector.FieldTypeIdCurrency);
            svc00600.AddField("AUTOREN", "Auto Renewing", Connector.FieldTypeIdString);
            svc00600.AddField("SVC_New_Contract_Number", "New Contract number", Connector.FieldTypeIdString);
            svc00600.AddField("SVC_Evergreen_Contract", "Evergreen Contract", Connector.FieldTypeIdYesNo);
            svc00600.AddField("SVC_Evergreen_RenewLimit", "Evergreen Renew Limit", Connector.FieldTypeIdInteger);
            svc00600.AddField("SVC_Evergreen_Renewals", "Evergreen Renewals", Connector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var contractTransferStatus = svc00600.AddField("Contract_Transfer_Status", "Contract Transfer status", Connector.FieldTypeIdEnum); 
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });
            
            var renewingContractType = svc00600.AddField("RENCNTTYP", "Renewing Contract type", Connector.FieldTypeIdEnum);
            renewingContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            
            var billingStatus = svc00600.AddField("BILSTAT", "Billing status", Connector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });
            
            var contractPeriod = svc00600.AddField("Contract_Period", "Contract period", Connector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            
            var sourceContractType = svc00600.AddField("Source_Contract_Type", "Source Contract type", Connector.FieldTypeIdEnum);
            sourceContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });

        }

        public ConnectorEntity GetContractLineEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListContractLines), "Contract lines", ParentConnector);

            var svc00601 = entity.AddTable("SVC00601");

            var rm00101 = entity.AddTable("RM00101", "SVC00601");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddContractLineEntityFields(svc00601, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddContractLineEntityFields(ConnectorTable svc00601, ConnectorTable rm00101)
        {
            var contractRecordType = svc00601.AddField("CONSTS", "Contract Record type", Connector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc00601.AddField("CONTNBR", "Contract number", Connector.FieldTypeIdString, true);
            svc00601.AddField("LNSEQNBR", "Line Sequence number", Connector.FieldTypeIdInteger, true);
            svc00601.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc00601.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            svc00601.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svc00601.AddField("EQUIPID", "Equipment ID", Connector.FieldTypeIdInteger, true);
            svc00601.AddField("SERLNMBR", "Serial number", Connector.FieldTypeIdString, true);
            svc00601.AddField("BILSTRT", "Bill Start", Connector.FieldTypeIdDate, true);
            svc00601.AddField("BILEND", "Bill End", Connector.FieldTypeIdDate, true);
            svc00601.AddField("BILLNGTH", "Bill Length", Connector.FieldTypeIdInteger, true);
            var billPeriod = svc00601.AddField("BILPRD", "Bill period", Connector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc00601.AddField("TOTAL", "Total", Connector.FieldTypeIdCurrency, true);
            svc00601.AddField("CNTTYPE", "Contract type", Connector.FieldTypeIdString, true);
            svc00601.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc00601.AddField("SRVTYPE", "Service type", Connector.FieldTypeIdString, true);
            svc00601.AddField("TOTBIL", "Total Billed", Connector.FieldTypeIdCurrency, true);
            svc00601.AddField("PREPAID", "Pre Paid", Connector.FieldTypeIdYesNo, true);
            svc00601.AddField("BILONDY", "Bill On Day", Connector.FieldTypeIdInteger, true);
            var billingCycle = svc00601.AddField("BILCYC", "Billing Cycle", Connector.FieldTypeIdEnum, true);
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc00601.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate, true);
            svc00601.AddField("ENDdate", "End date", Connector.FieldTypeIdDate, true);
            svc00601.AddField("Unit_Cost_Total", "Unit Cost total", Connector.FieldTypeIdCurrency, true);
            var liabilityType = svc00601.AddField("SVC_Liability_Type", "Liability type", Connector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block time", "Retainage", "Based on Calls", "Metered" });

            svc00601.AddField("Contract_Transfer_date", "Contract Transfer date", Connector.FieldTypeIdDate);
            svc00601.AddField("CNTPRCOVR", "Contract Price Overridden", Connector.FieldTypeIdYesNo);
            svc00601.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc00601.AddField("CONFGREF", "Config Reference", Connector.FieldTypeIdString);
            svc00601.AddField("FRZEND", "Frozen End", Connector.FieldTypeIdDate);
            svc00601.AddField("FRXSTRT", "Frozen Start", Connector.FieldTypeIdDate);
            svc00601.AddField("BLKTIM", "Blocked time", Connector.FieldTypeIdCurrency);
            svc00601.AddField("VALTIM", "Value of time", Connector.FieldTypeIdCurrency);
            svc00601.AddField("DSCPCTAM", "Discount Percent amount", Connector.FieldTypeIdPercentage);
            svc00601.AddField("COMDLRAM", "Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc00601.AddField("PRCSTAT", "Status of price", Connector.FieldTypeIdString);
            svc00601.AddField("PORDNMBR", "Purchase Order number", Connector.FieldTypeIdString);
            svc00601.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            svc00601.AddField("PARTPCT", "Part Pct", Connector.FieldTypeIdPercentage);
            svc00601.AddField("LABPCT", "Labor Pct", Connector.FieldTypeIdPercentage);
            svc00601.AddField("MISCPCT", "Misc Pct", Connector.FieldTypeIdPercentage);
            svc00601.AddField("PMMSCPCT", "PM Misc Pct", Connector.FieldTypeIdPercentage);
            svc00601.AddField("PMPRTPCT", "PM Part Pct", Connector.FieldTypeIdPercentage);
            svc00601.AddField("PMLABPCT", "PM Labor Pct", Connector.FieldTypeIdPercentage);
            svc00601.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString);
            svc00601.AddField("RETNAGAM", "Retainage amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("RTNBILLD", "Retainage Billed", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svc00601.AddField("COMMCODE", "Commission code", Connector.FieldTypeIdString);
            svc00601.AddField("COMPRCNT", "Commission Percent", Connector.FieldTypeIdPercentage);
            svc00601.AddField("FRSTBLDTE", "First Bill date", Connector.FieldTypeIdDate);
            svc00601.AddField("Last_Amount_Billed", "Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc00601.AddField("LSTBLDTE", "Last Bill date", Connector.FieldTypeIdDate);
            svc00601.AddField("NBRCAL", "Max Number of Calls", Connector.FieldTypeIdInteger);
            svc00601.AddField("ACTCAL", "Actual Number of Calls", Connector.FieldTypeIdInteger);
            svc00601.AddField("TOTVALCAL", "Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc00601.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc00601.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc00601.AddField("NXTBLDTE", "Next Bill date", Connector.FieldTypeIdDate);
            svc00601.AddField("PRICSHED", "Price Schedule", Connector.FieldTypeIdString);
            svc00601.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate);
            svc00601.AddField("MINBIL", "Min Billable Call", Connector.FieldTypeIdCurrency);
            svc00601.AddField("MAXBIL", "Max Billable Call", Connector.FieldTypeIdCurrency);
            svc00601.AddField("MAXBILL", "Max Billable", Connector.FieldTypeIdCurrency);
            svc00601.AddField("AUTOREN", "Auto Renewing", Connector.FieldTypeIdYesNo);
            svc00601.AddField("priorityLevel", "priorityLevel", Connector.FieldTypeIdInteger);
            svc00601.AddField("MSTCNTRCT", "Master Contract number", Connector.FieldTypeIdString);
            svc00601.AddField("BILFRRET", "Bill For Retainer", Connector.FieldTypeIdYesNo);
            svc00601.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svc00601.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc00601.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svc00601.AddField("CNFGLVL", "Config Level", Connector.FieldTypeIdInteger);
            svc00601.AddField("CNFGSEQ", "Config Sequence", Connector.FieldTypeIdInteger);
            svc00601.AddField("Contract_Length", "Contract Length", Connector.FieldTypeIdInteger);
            svc00601.AddField("Invoiced_Amount", "Invoiced amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Amount_To_Invoice", "Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Liabiltiy_Reduction", "Liability Reduction", Connector.FieldTypeIdYesNo);
            svc00601.AddField("Liability_Amount", "Liability amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Total_Liability_Amount", "Total Liability amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("NUMOFINV", "Number of Invoices", Connector.FieldTypeIdInteger);
            svc00601.AddField("New_Invoice_Amount", "New Invoice amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Contract_Line_Status", "Contract Line status", Connector.FieldTypeIdString);
            svc00601.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", Connector.FieldTypeIdString);
            svc00601.AddField("Contract_Response_Time", "Contract Response time", Connector.FieldTypeIdString);
            svc00601.AddField("Liability_Months", "Liability Months", Connector.FieldTypeIdInteger);
            svc00601.AddField("Next_Liability_date", "Next Liability date", Connector.FieldTypeIdDate);
            svc00601.AddField("Last_Liability_date", "Last Liability date", Connector.FieldTypeIdDate);
            svc00601.AddField("Total_Liability_Billed", "Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc00601.AddField("TAXEXMT1", "Tax Exempt 1", Connector.FieldTypeIdString);
            svc00601.AddField("TAXEXMT2", "Tax Exempt 2", Connector.FieldTypeIdString);
            svc00601.AddField("Total_Unit", "Total Unit", Connector.FieldTypeIdCurrency);
            svc00601.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", Connector.FieldTypeIdDate);
            svc00601.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString);
            svc00601.AddField("SVC_Monthly_Price", "Monthly_Price", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Location_Segment", "Location Segment", Connector.FieldTypeIdString);
            svc00601.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc00601.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc00601.AddField("DSCDLRAM", "Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Invoiced_Discount", "Invoiced Discount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Discount_Recognized", "Discount Recognized", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Discount_Remaining", "Discount Remaining", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_Discount_Next", "Discount Next", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_PM_Day", "PM Day", Connector.FieldTypeIdInteger);
            svc00601.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc00601.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc00601.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc00601.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc00601.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            
            svc00601.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc00601.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc00601.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc00601.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc00601.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc00601.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc00601.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc00601.AddField("ORIGVALTIM", "Originating Value of time", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORCOMAMT", "Originating Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGRETNAGAM", "Originating Retainage amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGRTNBILLD", "Originating Retainage Billed", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGTOTAL", "Originating Service total", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGMINBIL", "Originating Min Billable Call", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGMAXBIL", "Originating Max Billable Call", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Originating_Max_Billable", "Originating Max Billable", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGTOTBIL", "Originating Total Billed", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Orig_Invoiced_Amount", "Originating Invoiced amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Orig_Liability_Amount", "Originating Liability amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("OrigTotLiabilityAmount", "Originating Total Liability amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc00601.AddField("Originating_Total_Unit", "Originating Total Unit", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORUNTCST", "Originating Unit cost", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORDDLRAT", "Originating Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("OrigDiscountReceived", "Originating Discount Recognized", Connector.FieldTypeIdCurrency);
            svc00601.AddField("OrigDiscountRemaining", "Originating Discount Remaining", Connector.FieldTypeIdCurrency);
            svc00601.AddField("OrigDiscountNext", "Originating Discount Next", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGUNITCOSTTOTAL", "Originating Unit Cost total", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGMONTHPRICE", "Originating Monthly price", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORIGINVOICEDDISC", "Originating Invoiced Discount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("OrigNewInvoiceAmount", "Originating New Invoice amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svc00601.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            rm00101.AddField("RM00101.CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var contractTransferStatus = svc00601.AddField("Contract_Transfer_Status", "Contract Transfer status", Connector.FieldTypeIdEnum);
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });
            
            var billingStatus = svc00601.AddField("BILSTAT", "Billing status", Connector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });
            
            var contractPeriod = svc00601.AddField("Contract_Period", "Contract period", Connector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            
            var pmMonth = svc00601.AddField("SVC_PM_date", "PM Month", Connector.FieldTypeIdEnum);
            pmMonth.AddListItems(1, new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });

        }
            
        public ConnectorEntity GetRmaEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListRma), "RMAs", ParentConnector);

            var svc05000 = entity.AddTable("SVC05000");
            AddRmaEntityFields(svc05000);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRmaEntityFields(ConnectorTable svc05000)
        {
            svc05000.AddField("RETDOCID", "Return Document ID", Connector.FieldTypeIdString, true);
            var returnRecordType = svc05000.AddField("Return_Record_Type", "Return Record type", Connector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc05000.AddField("RMA_Status", "RMA status", Connector.FieldTypeIdInteger, true);
            svc05000.AddField("Received", "Received", Connector.FieldTypeIdYesNo, true);
            var returnOrigin = svc05000.AddField("RETORIG", "Return Origin", Connector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc05000.AddField("RETREF", "Return Reference", Connector.FieldTypeIdString, true);
            svc05000.AddField("RETSTAT", "Return status", Connector.FieldTypeIdString, true);
            svc05000.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString, true);
            svc05000.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc05000.AddField("ETADTE", "ETA date", Connector.FieldTypeIdDate, true);
            svc05000.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate, true);
            svc05000.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);
            svc05000.AddField("OFFID", "Office ID", Connector.FieldTypeIdString, true);
            svc05000.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc05000.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc05000.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc05000.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc05000.AddField("ETATME", "ETA time", Connector.FieldTypeIdTime);
            svc05000.AddField("Return_Time", "Return time", Connector.FieldTypeIdTime);
            svc05000.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc05000.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            svc05000.AddField("RTRNNAME", "Return Address name", Connector.FieldTypeIdString);
            svc05000.AddField("RETADDR1", "Return Address 1", Connector.FieldTypeIdString);
            svc05000.AddField("RETADDR2", "Return Address 2", Connector.FieldTypeIdString);
            svc05000.AddField("RETADDR3", "Return Address 3", Connector.FieldTypeIdString);
            svc05000.AddField("RTRNCITY", "Return Address City", Connector.FieldTypeIdString);
            svc05000.AddField("SVC_Return_State", "Return State", Connector.FieldTypeIdString);
            svc05000.AddField("RTRNZIP", "Return Address Zip code", Connector.FieldTypeIdString);
            svc05000.AddField("Return_Country", "Return Country", Connector.FieldTypeIdString);
            svc05000.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            svc05000.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc05000.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc05000.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc05000.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc05000.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc05000.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc05000.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc05000.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc05000.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc05000.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString);
            svc05000.AddField("EQPLINE", "Equipment Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05000.AddField("LNITMSEQ", "Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05000.AddField("Bill_of_Lading", "Bill of Lading", Connector.FieldTypeIdString);
            svc05000.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc05000.AddField("SOPNUMBE", "SOP number", Connector.FieldTypeIdString);
            svc05000.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05000.AddField("CMPNTSEQ", "Component Sequence", Connector.FieldTypeIdInteger);
            svc05000.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc05000.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc05000.AddField("Commit_date", "Commit date", Connector.FieldTypeIdDate);
            svc05000.AddField("Commit_Time", "Commit time", Connector.FieldTypeIdTime);
            svc05000.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc05000.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc05000.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc05000.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc05000.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc05000.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc05000.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc05000.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc05000.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc05000.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc05000.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc05000.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc05000.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc05000.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc05000.AddField("ISMCTRX", "Is MC Trx", Connector.FieldTypeIdInteger);
            svc05000.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc05000.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc05000.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc05000.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString);
            svc05000.AddField("SVC_RMA_Reason_Code", "RMA Reason code", Connector.FieldTypeIdString);
            svc05000.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code Desc", Connector.FieldTypeIdString);
            svc05000.AddField("SVC_RMA_From_Service", "RMA From Service", Connector.FieldTypeIdString);
            svc05000.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdYesNo);

            var serviceRecordType = svc05000.AddField("SRVRECTYPE", "Service Record type", Connector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
            
            var sopType = svc05000.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
        }

        public ConnectorEntity GetRtvEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListRtv), "RTVs", ParentConnector);

            var svc05600 = entity.AddTable("SVC05600");
            AddRtvEntityFields(svc05600);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRtvEntityFields(ConnectorTable svc05600)
        {
            svc05600.AddField("RTV_Number", "RTV number", Connector.FieldTypeIdString, true);
            svc05600.AddField("RTV_Type", "RTV type", Connector.FieldTypeIdString, true);
            svc05600.AddField("RTV_Return_Status", "RTV Return status", Connector.FieldTypeIdString, true);
            svc05600.AddField("VRMA_Document_ID", "VRMA Document ID", Connector.FieldTypeIdString, true);
            svc05600.AddField("RETDOCID", "RMA number", Connector.FieldTypeIdString, true);
            svc05600.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svc05600.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString, true);
            svc05600.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc05600.AddField("VENDORID", "Entry date", Connector.FieldTypeIdDate, true);
            svc05600.AddField("Shipped_date", "Shipped date", Connector.FieldTypeIdDate, true);
            svc05600.AddField("receiptdate", "Receipt date", Connector.FieldTypeIdDate, true);
            svc05600.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);
            svc05600.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc05600.AddField("Travel_Price", "Travel price", Connector.FieldTypeIdCurrency, true);
            svc05600.AddField("Bill_of_Lading_Out", "Bill of Landing (Out)", Connector.FieldTypeIdString, true);
            svc05600.AddField("Shipping_Method_Out", "Shipping Method (Out)", Connector.FieldTypeIdString, true);
            svc05600.AddField("VOIDSTTS", "Void status", Connector.FieldTypeIdInteger, true);
            svc05600.AddField("LNSEQNBR", "Line Sequence number", Connector.FieldTypeIdInteger);
            svc05600.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_Name", "Ship Address name", Connector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_1", "Ship Address 1", Connector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_2", "Ship Address 2", Connector.FieldTypeIdString);
            svc05600.AddField("Ship_Address_3", "Ship Address 3", Connector.FieldTypeIdString);
            svc05600.AddField("Ship_City", "Ship City", Connector.FieldTypeIdString);
            svc05600.AddField("Ship_State", "Ship State", Connector.FieldTypeIdString);
            svc05600.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc05600.AddField("Ship_Country", "Ship Country", Connector.FieldTypeIdString);
            svc05600.AddField("ADRSCODE", "Entry time", Connector.FieldTypeIdTime);
            svc05600.AddField("Ship_Address_Name", "Promised date", Connector.FieldTypeIdDate);
            svc05600.AddField("Ship_Address_1", "Promised time", Connector.FieldTypeIdTime);
            svc05600.AddField("Shipped_Time", "Shipped time", Connector.FieldTypeIdTime);
            svc05600.AddField("Receipt_Time", "Receipt time", Connector.FieldTypeIdTime);
            svc05600.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc05600.AddField("LOCCODEB", "Location Code Bad", Connector.FieldTypeIdString);
            svc05600.AddField("Part_Price", "Part price", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Part_Cost", "Part cost", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Labor_Price", "Labor price", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Labor_Cost", "Labor cost", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Expense_Price", "Expense price", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Expense_Cost", "Expense cost", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Travel_Cost", "Travel cost", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Bill_of_Lading", "Bill of Landing", Connector.FieldTypeIdString);
            svc05600.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc05600.AddField("OFFID", "Office ID", Connector.FieldTypeIdString);
            svc05600.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc05600.AddField("VCHNUMWK", "Voucher Number (Work)", Connector.FieldTypeIdString);
            svc05600.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", Connector.FieldTypeIdString);
            svc05600.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", Connector.FieldTypeIdString);
            svc05600.AddField("CUSTOWN", "Customer Owned", Connector.FieldTypeIdYesNo);
            svc05600.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            svc05600.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc05600.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc05600.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc05600.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc05600.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc05600.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc05600.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc05600.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc05600.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc05600.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc05600.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc05600.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc05600.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc05600.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc05600.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc05600.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc05600.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc05600.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc05600.AddField("Originating_Part_Price", "Originating Part price", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Part_Cost", "Originating Part cost", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Labor_Price", "Originating Labor price", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Labor_Cost", "Originating Labor cost", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_ExpensePrice", "Originating Expense price", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Expense_Cost", "Originating Expense cost", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Travel_Price", "Originating Travel price", Connector.FieldTypeIdCurrency);
            svc05600.AddField("Originating_Travel_Cost", "Originating Travel cost", Connector.FieldTypeIdCurrency);

            var rtvStatus = svc05600.AddField("RTV_Status", "RTV status", Connector.FieldTypeIdEnum);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
        }

        public ConnectorEntity GetWorkOrderEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListWorkOrders), "Work orders", ParentConnector);

            var svc06100 = entity.AddTable("SVC06100");

            var rm00101 = entity.AddTable("RM00101", "SVC06100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddWorkOrderEntityFields(svc06100, rm00101);

            return entity;
        }
        public void AddWorkOrderEntityFields(ConnectorTable svc06100, ConnectorTable rm00101)
        {
            var workOrderRecordType = svc06100.AddField("WORECTYPE", "Work Order Record type", Connector.FieldTypeIdEnum, true);
            workOrderRecordType.AddListItems(1, new List<string> { "Quote", "Open", "History", "Template" });
            svc06100.AddField("WORKORDNUM", "Work Order number", Connector.FieldTypeIdString, true);
            svc06100.AddField("WOTYPE", "Work Order type", Connector.FieldTypeIdString, true);
            svc06100.AddField("SVC_Depot_Priority", "SVC_Depot_Priority", Connector.FieldTypeIdInteger, true);
            svc06100.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc06100.AddField("ECOMPDT", "EComp date", Connector.FieldTypeIdDate, true);
            svc06100.AddField("EComp_Time", "EComp time", Connector.FieldTypeIdTime, true);
            svc06100.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate, true);
            svc06100.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);
            svc06100.AddField("OFFID", "Office ID", Connector.FieldTypeIdString, true);
            svc06100.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc06100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc06100.AddField("IBITEMNUM", "Inbound Item number", Connector.FieldTypeIdString, true);
            svc06100.AddField("IBSERIAL", "Inbound Serial number", Connector.FieldTypeIdString, true);
            svc06100.AddField("IBEQUIPID", "Inbound Equipment ID", Connector.FieldTypeIdInteger, true);
            svc06100.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            svc06100.AddField("STATIONID", "Depot Station ID", Connector.FieldTypeIdString, true);
            svc06100.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc06100.AddField("LABSTOTCST", "Labor Sub Total cost", Connector.FieldTypeIdCurrency, true);
            svc06100.AddField("PARSTOTCST", "Part Sub Total cost", Connector.FieldTypeIdCurrency, true);
            svc06100.AddField("TOTLABHRS", "Total Labor Hours", Connector.FieldTypeIdCurrency, true);
            svc06100.AddField("Commit_date", "Commit date", Connector.FieldTypeIdDate, true);

            svc06100.AddField("WOSTAT", "Work Order status", Connector.FieldTypeIdString);
            svc06100.AddField("PARWONUM", "Parent Work Order number", Connector.FieldTypeIdString);
            svc06100.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svc06100.AddField("TIMEZONE", "TimeZone", Connector.FieldTypeIdString);
            svc06100.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc06100.AddField("ETADTE", "ETA date", Connector.FieldTypeIdDate);
            svc06100.AddField("ETATME", "ETA time", Connector.FieldTypeIdTime);
            svc06100.AddField("Return_Time", "Return time", Connector.FieldTypeIdTime);
            svc06100.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc06100.AddField("BIN", "Bin", Connector.FieldTypeIdString);
            svc06100.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            svc06100.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc06100.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc06100.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc06100.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc06100.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc06100.AddField("RETDOCID", "Return Document ID", Connector.FieldTypeIdString);
            svc06100.AddField("LNSEQNBR", "Line Sequence number", Connector.FieldTypeIdInteger);
            svc06100.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString);
            svc06100.AddField("SERVLITEMSEQ", "Service Line Item Sequence", Connector.FieldTypeIdInteger);
            svc06100.AddField("OBITEMNUM", "Outbound Item number", Connector.FieldTypeIdString);
            svc06100.AddField("OBSERIAL", "Outbound Serial number", Connector.FieldTypeIdString);
            svc06100.AddField("OBEQUIPID", "Outbound Equipment ID", Connector.FieldTypeIdInteger);
            svc06100.AddField("ROUTEID", "Route ID", Connector.FieldTypeIdString);
            svc06100.AddField("SEQUENCE1", "Sequence", Connector.FieldTypeIdInteger);
            svc06100.AddField("IBANALCODE", "Inbound Analysis code", Connector.FieldTypeIdString);
            svc06100.AddField("OBANALCODE", "Outbound Analysis code", Connector.FieldTypeIdString);
            svc06100.AddField("CUSTOWN", "Customer Owned", Connector.FieldTypeIdYesNo);
            svc06100.AddField("ORDDOCID", "Order Document ID", Connector.FieldTypeIdString);
            svc06100.AddField("TRANSLINESEQ", "Transfer Line Item Sequence", Connector.FieldTypeIdInteger);
            svc06100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc06100.AddField("FACTSEAL", "Factory Sealed", Connector.FieldTypeIdYesNo);
            svc06100.AddField("PRICELVL", "Price Level", Connector.FieldTypeIdString);
            svc06100.AddField("NUMRESCHED", "Number of Reschedules", Connector.FieldTypeIdInteger);
            svc06100.AddField("Commit_Time", "Commit time", Connector.FieldTypeIdTime);
            svc06100.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc06100.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc06100.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc06100.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc06100.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc06100.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc06100.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc06100.AddField("SVC_RMA_Reason_Code", "RMA Reason code", Connector.FieldTypeIdString);
            svc06100.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code description", Connector.FieldTypeIdString);
            svc06100.AddField("SVC_Process_SEQ_Number", "Process Sequence number", Connector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var parentWorkOrderRecordType = svc06100.AddField("PARWORECTYPE", "Parent Work Order Record type", Connector.FieldTypeIdEnum);
            parentWorkOrderRecordType.AddListItems(1, new List<string> { "Quote", "Open", "History", "Template" });

            var origin = svc06100.AddField("ORIGIN", "Origin", Connector.FieldTypeIdEnum);
            origin.AddListItems(1, new List<string> { "Quote", "Open", "History", "Template" });

            var serviceRecordType = svc06100.AddField("SRVRECTYPE", "Service Record type", Connector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
        }

        public ConnectorEntity GetHistoricalServiceCallEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListHistFieldServiceCalls), "Historical service calls", ParentConnector);

            var svc30200 = entity.AddTable("SVC30200");

            var svc30202 = entity.AddScript("(select SRVRECTYPE, CALLNBR, EQUIPID from {0}..SVC30202 with (NOLOCK) where EQPLINE = 1)", "SVC30202", "SVC30200");
            svc30202.AddJoinFields("SRVRECTYPE", "SRVRECTYPE");
            svc30202.AddJoinFields("CALLNBR", "CALLNBR");

            var svc00300 = entity.AddTable("SVC00300", "SVC30202");
            svc00300.AddJoinFields("EQUIPID", "EQUIPID");

            AddHistoricalServiceCallEntityFields(svc30200, svc00300);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalServiceCallEntityFields(ConnectorTable svc30200, ConnectorTable svc00300)
        {
            svc30200.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString, true);
            var serviceRecordType = svc30200.AddField("SRVRECTYPE", "Service Record type", Connector.FieldTypeIdEnum, true);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });
            svc30200.AddField("SRVSTAT", "Service Call status", Connector.FieldTypeIdString, true);
            svc30200.AddField("SRVTYPE", "Service type", Connector.FieldTypeIdString, true);
            svc30200.AddField("SVCDESCR", "Service description", Connector.FieldTypeIdString, true);
            svc30200.AddField("priorityLevel", "priorityLevel", Connector.FieldTypeIdInteger, true);
            svc30200.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc30200.AddField("Customer_Reference", "Customer Reference", Connector.FieldTypeIdString, true);
            svc30200.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc30200.AddField("CUSTNAME", "Name", Connector.FieldTypeIdString, true);
            svc30200.AddField("OFFID", "Office ID", Connector.FieldTypeIdString, true);
            svc30200.AddField("SVCAREA", "Service Area", Connector.FieldTypeIdString, true);
            svc30200.AddField("TECHID", "Tech ID", Connector.FieldTypeIdString, true);
            svc30200.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc30200.AddField("TOTAL", "Total", Connector.FieldTypeIdCurrency, true);
            svc30200.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc30200.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc30200.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc30200.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc30200.AddField("ZIP", "Zip", Connector.FieldTypeIdString);
            svc30200.AddField("CNTCPRSN", "Contact Person", Connector.FieldTypeIdString);
            svc30200.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            svc30200.AddField("TIMEZONE", "TimeZone", Connector.FieldTypeIdString);
            svc30200.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc30200.AddField("Notify_date", "Notify date", Connector.FieldTypeIdDate);
            svc30200.AddField("Notify_Time", "Notify time", Connector.FieldTypeIdTime);
            svc30200.AddField("ETADTE", "ETA date", Connector.FieldTypeIdDate);
            svc30200.AddField("ETATME", "ETA time", Connector.FieldTypeIdTime);
            svc30200.AddField("DISPDTE", "Dispatch date", Connector.FieldTypeIdDate);
            svc30200.AddField("DISPTME", "Dispatch time", Connector.FieldTypeIdTime);
            svc30200.AddField("ARRIVDTE", "Arrival date", Connector.FieldTypeIdDate);
            svc30200.AddField("ARRIVTME", "Arrival time", Connector.FieldTypeIdTime);
            svc30200.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate);
            svc30200.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc30200.AddField("Response_date", "Response date", Connector.FieldTypeIdDate);
            svc30200.AddField("Response_Time", "Response time", Connector.FieldTypeIdTime);
            svc30200.AddField("PRICELVL", "Price Level", Connector.FieldTypeIdString);
            svc30200.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            svc30200.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svc30200.AddField("LABSTOTPRC", "Labor Sub Total price", Connector.FieldTypeIdCurrency);
            svc30200.AddField("LABPCT", "Labor Pct", Connector.FieldTypeIdPercentage);
            svc30200.AddField("LABSTOTCST", "Labor Sub Total cost", Connector.FieldTypeIdCurrency);
            svc30200.AddField("PARSTOTPRC", "Part Sub Total price", Connector.FieldTypeIdCurrency);
            svc30200.AddField("PARTPCT", "Part Pct", Connector.FieldTypeIdPercentage);
            svc30200.AddField("PARSTOTCST", "Part Sub Total cost", Connector.FieldTypeIdCurrency);
            svc30200.AddField("MSCSTOTPRC", "Misc Sub Total price", Connector.FieldTypeIdCurrency);
            svc30200.AddField("MISCPCT", "Misc Pct", Connector.FieldTypeIdPercentage);
            svc30200.AddField("MISSTOTCST", "Misc Sub Total cost", Connector.FieldTypeIdCurrency);
            svc30200.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svc30200.AddField("TAXEXMT1", "Tax Exempt 1", Connector.FieldTypeIdString);
            svc30200.AddField("TAXEXMT2", "Tax Exempt 2", Connector.FieldTypeIdString);
            svc30200.AddField("PRETAXTOT", "PreTax total", Connector.FieldTypeIdCurrency);
            svc30200.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svc30200.AddField("Invoiced_Amount", "Invoiced amount", Connector.FieldTypeIdCurrency);
            svc30200.AddField("METER1", "Meter 1", Connector.FieldTypeIdInteger);
            svc30200.AddField("METER2", "Meter 2", Connector.FieldTypeIdInteger);
            svc30200.AddField("METER3", "Meter 3", Connector.FieldTypeIdInteger);
            svc30200.AddField("PORDNMBR", "Purchase Order number", Connector.FieldTypeIdString);
            svc30200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc30200.AddField("NOTFYFLAG", "Notify Flag", Connector.FieldTypeIdYesNo);
            svc30200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc30200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc30200.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc30200.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc30200.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc30200.AddField("MSTRCALLNBR", "Master Service Call number", Connector.FieldTypeIdString);
            svc30200.AddField("ESCdate", "Escalation date", Connector.FieldTypeIdDate);
            svc30200.AddField("ESCTIME", "Escalation time", Connector.FieldTypeIdTime);
            svc30200.AddField("Escalation_Level", "Escalation Level", Connector.FieldTypeIdInteger);
            svc30200.AddField("SPLTTERMS", "Split Terms code", Connector.FieldTypeIdString);
            svc30200.AddField("Callback", "Callback", Connector.FieldTypeIdYesNo);
            svc30200.AddField("PROJCTID", "Project ID", Connector.FieldTypeIdString);
            svc30200.AddField("ProjectRef1_1", "ProjectRef1", Connector.FieldTypeIdString);
            svc30200.AddField("CONTNBR", "Contract number", Connector.FieldTypeIdString);
            svc30200.AddField("SVC_Contract_Line_SEQ", "Contract Line Sequence number", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ETTR", "ETTR", Connector.FieldTypeIdCurrency);
            svc30200.AddField("SVC_On_Hold", "On Hold", Connector.FieldTypeIdYesNo);
            svc30200.AddField("Print_to_Web", "Print to Web", Connector.FieldTypeIdYesNo);
            svc30200.AddField("Meters_1", "Meters", Connector.FieldTypeIdInteger);
            svc30200.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc30200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc30200.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc30200.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc30200.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc30200.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc30200.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc30200.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc30200.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc30200.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc30200.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc30200.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc30200.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc30200.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc30200.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc30200.AddField("ORIGMISSTOTCST", "Originating Misc Sub Total cost", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGMSCSTOTPRC", "Originating Misc Sub Total price", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGLABSUBTOTCOST", "Originating Labor Sub Total cost", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGLABSTOTPRC", "Originating Labor Sub Total price", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGPARSTOTCST", "Originating Part Sub Total cost", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGPARSTOTPRC", "Originating Part Sub Total price", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGPRETAXTOT", "Originating PreTax total", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ORIGTOTAL", "Originating Service total", Connector.FieldTypeIdCurrency);
            svc30200.AddField("Orig_Invoiced_Amount", "Originating Invoiced amount", Connector.FieldTypeIdCurrency);
            svc30200.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc30200.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc30200.AddField("SVC_Pre600", "SVC_Pre600", Connector.FieldTypeIdYesNo);
            svc30200.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            svc30200.AddField("Replaces_1", "Replaces", Connector.FieldTypeIdString);
            svc00300.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString);
            svc00300.AddField("SERLNMBR", "Equipment/Serial number", Connector.FieldTypeIdString);

            var masterServiceRecordType = svc30200.AddField("MSTRRECTYPE", "Master Service Record type", Connector.FieldTypeIdEnum);
            masterServiceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var contractRecordType = svc30200.AddField("CONSTS", "Contract Record type", Connector.FieldTypeIdEnum);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
        }

        public ConnectorEntity GetHistoricalContractEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListHistContracts), "Historical contracts", ParentConnector);

            var svc30600 = entity.AddTable("SVC30600");

            var rm00101 = entity.AddTable("RM00101", "SVC30600");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddHistoricalContractEntityFields(svc30600, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalContractEntityFields(ConnectorTable svc30600, ConnectorTable rm00101)
        {
            var contractRecordType = svc30600.AddField("CONSTS", "Contract Record type", Connector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc30600.AddField("CONTNBR", "Contract number", Connector.FieldTypeIdString, true);
            svc30600.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc30600.AddField("BILSTRT", "Bill Start", Connector.FieldTypeIdDate, true);
            svc30600.AddField("BILEND", "Bill End", Connector.FieldTypeIdDate, true);
            svc30600.AddField("BILLNGTH", "Bill Length", Connector.FieldTypeIdInteger, true);
            var billPeriod = svc30600.AddField("BILPRD", "Bill period", Connector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc30600.AddField("TOTAL", "Total", Connector.FieldTypeIdCurrency, true);
            svc30600.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc30600.AddField("BILONDY", "Bill On Day", Connector.FieldTypeIdInteger, true);
            var billingCycle = svc30600.AddField("BILCYC", "Billing Cycle", Connector.FieldTypeIdEnum, true);
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc30600.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate, true);
            svc30600.AddField("ENDdate", "End date", Connector.FieldTypeIdDate, true);
            var liabilityType = svc30600.AddField("SVC_Liability_Type", "Liability type", Connector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block time", "Retainage", "Based on Calls", "Metered" });
            svc30600.AddField("Contract_Transfer_date", "Contract Transfer date", Connector.FieldTypeIdDate);
            svc30600.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svc30600.AddField("priorityLevel", "priorityLevel", Connector.FieldTypeIdInteger);
            svc30600.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc30600.AddField("TIMEZONE", "TimeZone", Connector.FieldTypeIdString);
            svc30600.AddField("CONTPRC", "Contract price", Connector.FieldTypeIdCurrency);
            svc30600.AddField("RENPRCSCHD", "Renewing Price Schedule", Connector.FieldTypeIdString);
            svc30600.AddField("PCTCRYFWD", "Percentage Carried Forward", Connector.FieldTypeIdPercentage);
            svc30600.AddField("FRZEND", "Frozen End", Connector.FieldTypeIdDate);
            svc30600.AddField("FRXSTRT", "Frozen Start", Connector.FieldTypeIdDate);
            svc30600.AddField("MXINCPCT", "Max Increase Percentage", Connector.FieldTypeIdPercentage);
            svc30600.AddField("BLKTIM", "Blocked time", Connector.FieldTypeIdCurrency);
            svc30600.AddField("VALTIM", "Value of time", Connector.FieldTypeIdCurrency);
            svc30600.AddField("DSCPCTAM", "Discount Percent amount", Connector.FieldTypeIdPercentage);
            svc30600.AddField("COMDLRAM", "Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc30600.AddField("PRCSTAT", "Status of price", Connector.FieldTypeIdString);
            svc30600.AddField("PORDNMBR", "Purchase Order number", Connector.FieldTypeIdString);
            svc30600.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            svc30600.AddField("PARTPCT", "Part Pct", Connector.FieldTypeIdPercentage);
            svc30600.AddField("LABPCT", "Labor Pct", Connector.FieldTypeIdPercentage);
            svc30600.AddField("MISCPCT", "Misc Pct", Connector.FieldTypeIdPercentage);
            svc30600.AddField("PMMSCPCT", "PM Misc Pct", Connector.FieldTypeIdPercentage);
            svc30600.AddField("PMPRTPCT", "PM Part Pct", Connector.FieldTypeIdPercentage);
            svc30600.AddField("PMLABPCT", "PM Labor Pct", Connector.FieldTypeIdPercentage);
            svc30600.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString);
            svc30600.AddField("RETNAGAM", "Retainage amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("RTNBILLD", "Retainage Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svc30600.AddField("COMMCODE", "Commission code", Connector.FieldTypeIdString);
            svc30600.AddField("COMPRCNT", "Commission Percent", Connector.FieldTypeIdPercentage);
            svc30600.AddField("FRSTBLDTE", "First Bill date", Connector.FieldTypeIdDate);
            svc30600.AddField("Last_Amount_Billed", "Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("LSTBLDTE", "Last Bill date", Connector.FieldTypeIdDate);
            svc30600.AddField("NBRCAL", "Max Number of Calls", Connector.FieldTypeIdInteger);
            svc30600.AddField("ACTCAL", "Actual Number of Calls", Connector.FieldTypeIdInteger);
            svc30600.AddField("TOTVALCAL", "Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc30600.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc30600.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc30600.AddField("NXTBLDTE", "Next Bill date", Connector.FieldTypeIdDate);
            svc30600.AddField("CNTTYPE", "Contract type", Connector.FieldTypeIdString);
            svc30600.AddField("PRICSHED", "Price Schedule", Connector.FieldTypeIdString);
            svc30600.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate);
            svc30600.AddField("MINBIL", "Min Billable Call", Connector.FieldTypeIdCurrency);
            svc30600.AddField("MAXBIL", "Max Billable Call", Connector.FieldTypeIdCurrency);
            svc30600.AddField("MAXBILL", "Max Billable", Connector.FieldTypeIdCurrency);
            svc30600.AddField("AUTOREN", "Auto Renewing", Connector.FieldTypeIdYesNo);
            svc30600.AddField("MSTCNTRCT", "Master Contract number", Connector.FieldTypeIdString);
            svc30600.AddField("SRVTYPE", "Service type", Connector.FieldTypeIdString);
            svc30600.AddField("BILFRRET", "Bill For Retainer", Connector.FieldTypeIdYesNo);
            svc30600.AddField("TOTBIL", "Total Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("PREPAID", "Pre Paid", Connector.FieldTypeIdYesNo);
            svc30600.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svc30600.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc30600.AddField("Contract_Length", "Contract Length", Connector.FieldTypeIdInteger);
            svc30600.AddField("Invoiced_Amount", "Invoiced amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Liabiltiy_Reduction", "Liability Reduction", Connector.FieldTypeIdYesNo);
            svc30600.AddField("Amount_To_Invoice", "Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Liability_Amount", "Liability amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Total_Liability_Amount", "Total Liability amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("NUMOFINV", "Number of Invoices", Connector.FieldTypeIdInteger);
            svc30600.AddField("Quote_Status", "Quote status", Connector.FieldTypeIdInteger);
            svc30600.AddField("QUOEXPDA", "Quote Expiration date", Connector.FieldTypeIdDate);
            svc30600.AddField("Credit_Hold", "Credit Hold", Connector.FieldTypeIdYesNo);
            svc30600.AddField("TAXEXMT1", "Tax Exempt 1", Connector.FieldTypeIdString);
            svc30600.AddField("TAXEXMT2", "Tax Exempt 2", Connector.FieldTypeIdString);
            svc30600.AddField("New_PO_Required", "New PO Required", Connector.FieldTypeIdYesNo);
            svc30600.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", Connector.FieldTypeIdString);
            svc30600.AddField("Source_Contract_Number", "Source Contract number", Connector.FieldTypeIdString);
            svc30600.AddField("Contract_Response_Time", "Contract Response time", Connector.FieldTypeIdString);
            svc30600.AddField("Liability_Months", "Liability Months", Connector.FieldTypeIdInteger);
            svc30600.AddField("Next_Liability_date", "Next Liability date", Connector.FieldTypeIdDate);
            svc30600.AddField("Last_Liability_date", "Last Liability date", Connector.FieldTypeIdDate);
            svc30600.AddField("Total_Liability_Billed", "Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Total_Unit", "Total Unit", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Created_User_ID", "SVC_Created User ID", Connector.FieldTypeIdString);
            svc30600.AddField("Source_User_ID", "Source User ID", Connector.FieldTypeIdString);
            svc30600.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", Connector.FieldTypeIdDate);
            svc30600.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString);
            svc30600.AddField("Location_Segment", "Location Segment", Connector.FieldTypeIdString);
            svc30600.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc30600.AddField("SVC_Invoice_Detail", "Invoice Detail", Connector.FieldTypeIdYesNo);
            svc30600.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc30600.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc30600.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc30600.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc30600.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc30600.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc30600.AddField("DSCDLRAM", "Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("SVC_Paid_Contract", "Paid Contract", Connector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_Discount_Recognized", "Discount Recognized", Connector.FieldTypeIdCurrency);
            svc30600.AddField("SVC_Discount_Remaining", "Discount Remaining", Connector.FieldTypeIdCurrency);
            svc30600.AddField("SVC_Discount_Next", "Discount Next", Connector.FieldTypeIdCurrency);
            svc30600.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc30600.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc30600.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc30600.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc30600.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc30600.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc30600.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc30600.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc30600.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc30600.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc30600.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc30600.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc30600.AddField("ORIGVALTIM", "Originating Value of time", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORCOMAMT", "Originating Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGRETNAGAM", "Originating Retainage amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGRTNBILLD", "Originating Retainage Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGTOTAL", "Originating Service total", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGMINBIL", "Originating Min Billable Call", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGMAXBIL", "Originating Max Billable Call", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Originating_Max_Billable", "Originating Max Billable", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORIGTOTBIL", "Originating Total Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_Invoiced_Amount", "Originating Invoiced amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_Liability_Amount", "Originating Liability amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("OrigTotLiabilityAmount", "Originating Total Liability amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Originating_Total_Unit", "Originating Total Unit", Connector.FieldTypeIdCurrency);
            svc30600.AddField("ORDDLRAT", "Originating Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc30600.AddField("OrigDiscountReceived", "Originating Discount Recognized", Connector.FieldTypeIdCurrency);
            svc30600.AddField("OrigDiscountRemaining", "Originating Discount Remaining", Connector.FieldTypeIdCurrency);
            svc30600.AddField("OrigDiscountNext", "Originating Discount Next", Connector.FieldTypeIdCurrency);
            svc30600.AddField("SmoothInvoiceCalc", "Smooth Invoice Calculation", Connector.FieldTypeIdYesNo);
            svc30600.AddField("SmoothRevenueCalc", "Smooth Revenue Calculation", Connector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_Use_Same_Number", "Use Same number", Connector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            svc30600.AddField("SVC_Invoiced_Cost", "Invoiced cost", Connector.FieldTypeIdCurrency);
            svc30600.AddField("Orig_SVC_Invoiced_Cost", "Originating SVC Invoiced cost", Connector.FieldTypeIdCurrency);
            svc30600.AddField("AUTOREN", "Auto Renewing", Connector.FieldTypeIdString);
            svc30600.AddField("SVC_New_Contract_Number", "New Contract number", Connector.FieldTypeIdString);
            svc30600.AddField("SVC_Evergreen_Contract", "Evergreen Contract", Connector.FieldTypeIdYesNo);
            svc30600.AddField("SVC_Evergreen_RenewLimit", "Evergreen Renew Limit", Connector.FieldTypeIdInteger);
            svc30600.AddField("SVC_Evergreen_Renewals", "Evergreen Renewals", Connector.FieldTypeIdInteger);
            rm00101.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var contractTransferStatus = svc30600.AddField("Contract_Transfer_Status", "Contract Transfer status", Connector.FieldTypeIdEnum);
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });

            var renewingContractType = svc30600.AddField("RENCNTTYP", "Renewing Contract type", Connector.FieldTypeIdEnum);
            renewingContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });

            var billingStatus = svc30600.AddField("BILSTAT", "Billing status", Connector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });

            var contractPeriod = svc30600.AddField("Contract_Period", "Contract period", Connector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });

            var sourceContractType = svc30600.AddField("Source_Contract_Type", "Source Contract type", Connector.FieldTypeIdEnum);
            sourceContractType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
        }

        public ConnectorEntity GetHistoricalContractLineEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListHistContractLines), "Historical contract lines", ParentConnector);

            var svc30601 = entity.AddTable("SVC30601");

            var rm00101 = entity.AddTable("RM00101", "SVC30601");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");

            AddHistoricalContractLineEntityFields(svc30601, rm00101);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalContractLineEntityFields(ConnectorTable svc30601, ConnectorTable rm00101)
        {
            var contractRecordType = svc30601.AddField("CONSTS", "Contract Record type", Connector.FieldTypeIdEnum, true);
            contractRecordType.AddListItems(1, new List<string> { "Quote", "Contract", "Template", "History" });
            svc30601.AddField("CONTNBR", "Contract number", Connector.FieldTypeIdString, true);
            svc30601.AddField("LNSEQNBR", "Line Sequence number", Connector.FieldTypeIdInteger, true);
            svc30601.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc30601.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            svc30601.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svc30601.AddField("EQUIPID", "Equipment ID", Connector.FieldTypeIdInteger, true);
            svc30601.AddField("SERLNMBR", "Serial number", Connector.FieldTypeIdString, true);
            svc30601.AddField("BILSTRT", "Bill Start", Connector.FieldTypeIdDate, true);
            svc30601.AddField("BILEND", "Bill End", Connector.FieldTypeIdDate, true);
            svc30601.AddField("BILLNGTH", "Bill Length", Connector.FieldTypeIdInteger, true);
            var billPeriod = svc30601.AddField("BILPRD", "Bill period", Connector.FieldTypeIdEnum, true);
            billPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });
            svc30601.AddField("TOTAL", "Total", Connector.FieldTypeIdCurrency, true);
            svc30601.AddField("CNTTYPE", "Contract type", Connector.FieldTypeIdString, true);
            svc30601.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc30601.AddField("SRVTYPE", "Service type", Connector.FieldTypeIdString, true);
            svc30601.AddField("TOTBIL", "Total Billed", Connector.FieldTypeIdCurrency, true);
            svc30601.AddField("PREPAID", "Pre Paid", Connector.FieldTypeIdYesNo, true);
            svc30601.AddField("BILONDY", "Bill On Day", Connector.FieldTypeIdInteger, true);
            var billingCycle = svc30601.AddField("BILCYC", "Billing Cycle", Connector.FieldTypeIdEnum, true);
            billingCycle.AddListItems(1, new List<string> { "Days", "Weeks", "Months", "Years" });
            svc30601.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate, true);
            svc30601.AddField("ENDdate", "End date", Connector.FieldTypeIdDate, true);
            svc30601.AddField("Unit_Cost_Total", "Unit Cost total", Connector.FieldTypeIdCurrency, true);
            var liabilityType = svc30601.AddField("SVC_Liability_Type", "Liability type", Connector.FieldTypeIdEnum, true);
            liabilityType.AddListItems(1, new List<string> { "Straight-Line", "Block time", "Retainage", "Based on Calls", "Metered" });

            svc30601.AddField("Contract_Transfer_date", "Contract Transfer date", Connector.FieldTypeIdDate);
            svc30601.AddField("CNTPRCOVR", "Contract Price Overridden", Connector.FieldTypeIdYesNo);
            svc30601.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc30601.AddField("CONFGREF", "Config Reference", Connector.FieldTypeIdString);
            svc30601.AddField("FRZEND", "Frozen End", Connector.FieldTypeIdDate);
            svc30601.AddField("FRXSTRT", "Frozen Start", Connector.FieldTypeIdDate);
            svc30601.AddField("BLKTIM", "Blocked time", Connector.FieldTypeIdCurrency);
            svc30601.AddField("VALTIM", "Value of time", Connector.FieldTypeIdCurrency);
            svc30601.AddField("DSCPCTAM", "Discount Percent amount", Connector.FieldTypeIdPercentage);
            svc30601.AddField("COMDLRAM", "Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc30601.AddField("PRCSTAT", "Status of price", Connector.FieldTypeIdString);
            svc30601.AddField("PORDNMBR", "Purchase Order number", Connector.FieldTypeIdString);
            svc30601.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            svc30601.AddField("PARTPCT", "Part Pct", Connector.FieldTypeIdPercentage);
            svc30601.AddField("LABPCT", "Labor Pct", Connector.FieldTypeIdPercentage);
            svc30601.AddField("MISCPCT", "Misc Pct", Connector.FieldTypeIdPercentage);
            svc30601.AddField("PMMSCPCT", "PM Misc Pct", Connector.FieldTypeIdPercentage);
            svc30601.AddField("PMPRTPCT", "PM Part Pct", Connector.FieldTypeIdPercentage);
            svc30601.AddField("PMLABPCT", "PM Labor Pct", Connector.FieldTypeIdPercentage);
            svc30601.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString);
            svc30601.AddField("RETNAGAM", "Retainage amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("RTNBILLD", "Retainage Billed", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            svc30601.AddField("COMMCODE", "Commission code", Connector.FieldTypeIdString);
            svc30601.AddField("COMPRCNT", "Commission Percent", Connector.FieldTypeIdPercentage);
            svc30601.AddField("FRSTBLDTE", "First Bill date", Connector.FieldTypeIdDate);
            svc30601.AddField("Last_Amount_Billed", "Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc30601.AddField("LSTBLDTE", "Last Bill date", Connector.FieldTypeIdDate);
            svc30601.AddField("NBRCAL", "Max Number of Calls", Connector.FieldTypeIdInteger);
            svc30601.AddField("ACTCAL", "Actual Number of Calls", Connector.FieldTypeIdInteger);
            svc30601.AddField("TOTVALCAL", "Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc30601.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc30601.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc30601.AddField("NXTBLDTE", "Next Bill date", Connector.FieldTypeIdDate);
            svc30601.AddField("PRICSHED", "Price Schedule", Connector.FieldTypeIdString);
            svc30601.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate);
            svc30601.AddField("MINBIL", "Min Billable Call", Connector.FieldTypeIdCurrency);
            svc30601.AddField("MAXBIL", "Max Billable Call", Connector.FieldTypeIdCurrency);
            svc30601.AddField("MAXBILL", "Max Billable", Connector.FieldTypeIdCurrency);
            svc30601.AddField("AUTOREN", "Auto Renewing", Connector.FieldTypeIdYesNo);
            svc30601.AddField("priorityLevel", "priorityLevel", Connector.FieldTypeIdInteger);
            svc30601.AddField("MSTCNTRCT", "Master Contract number", Connector.FieldTypeIdString);
            svc30601.AddField("BILFRRET", "Bill For Retainer", Connector.FieldTypeIdYesNo);
            svc30601.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svc30601.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc30601.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            svc30601.AddField("CNFGLVL", "Config Level", Connector.FieldTypeIdInteger);
            svc30601.AddField("CNFGSEQ", "Config Sequence", Connector.FieldTypeIdInteger);
            svc30601.AddField("Contract_Length", "Contract Length", Connector.FieldTypeIdInteger);
            svc30601.AddField("Invoiced_Amount", "Invoiced amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Amount_To_Invoice", "Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Liabiltiy_Reduction", "Liability Reduction", Connector.FieldTypeIdYesNo);
            svc30601.AddField("Liability_Amount", "Liability amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Total_Liability_Amount", "Total Liability amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("NUMOFINV", "Number of Invoices", Connector.FieldTypeIdInteger);
            svc30601.AddField("New_Invoice_Amount", "New Invoice amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Contract_Line_Status", "Contract Line status", Connector.FieldTypeIdString);
            svc30601.AddField("Contract_Renewal_Contact", "Contract Renewal Contact", Connector.FieldTypeIdString);
            svc30601.AddField("Contract_Response_Time", "Contract Response time", Connector.FieldTypeIdString);
            svc30601.AddField("Liability_Months", "Liability Months", Connector.FieldTypeIdInteger);
            svc30601.AddField("Next_Liability_date", "Next Liability date", Connector.FieldTypeIdDate);
            svc30601.AddField("Last_Liability_date", "Last Liability date", Connector.FieldTypeIdDate);
            svc30601.AddField("Total_Liability_Billed", "Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc30601.AddField("TAXEXMT1", "Tax Exempt 1", Connector.FieldTypeIdString);
            svc30601.AddField("TAXEXMT2", "Tax Exempt 2", Connector.FieldTypeIdString);
            svc30601.AddField("Total_Unit", "Total Unit", Connector.FieldTypeIdCurrency);
            svc30601.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Contract_Signed", "SVC_Contract_Signed", Connector.FieldTypeIdDate);
            svc30601.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString);
            svc30601.AddField("SVC_Monthly_Price", "Monthly_Price", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Location_Segment", "Location Segment", Connector.FieldTypeIdString);
            svc30601.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc30601.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc30601.AddField("DSCDLRAM", "Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Invoiced_Discount", "Invoiced Discount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Discount_Recognized", "Discount Recognized", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Discount_Remaining", "Discount Remaining", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_Discount_Next", "Discount Next", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_PM_Day", "PM Day", Connector.FieldTypeIdInteger);
            svc30601.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc30601.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc30601.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc30601.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc30601.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);

            svc30601.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc30601.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc30601.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc30601.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc30601.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc30601.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc30601.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc30601.AddField("ORIGVALTIM", "Originating Value of time", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORCOMAMT", "Originating Commission Dollar amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGRETNAGAM", "Originating Retainage amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGRTNBILLD", "Originating Retainage Billed", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGLASTAmountBilled", "Originating Last Amount Billed", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGTOTAL", "Originating Service total", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGTOTVALCAL", "Originating Total Value of Calls", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGMINBIL", "Originating Min Billable Call", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGMAXBIL", "Originating Max Billable Call", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Originating_Max_Billable", "Originating Max Billable", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGTOTBIL", "Originating Total Billed", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Orig_Invoiced_Amount", "Originating Invoiced amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Orig_Amount_To_Invoice", "Originating Amount To Invoice", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Orig_Liability_Amount", "Originating Liability amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("OrigTotLiabilityAmount", "Originating Total Liability amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("OrigTotLiabBilled", "Originating Total Liability Billed", Connector.FieldTypeIdCurrency);
            svc30601.AddField("Originating_Total_Unit", "Originating Total Unit", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORUNTCST", "Originating Unit cost", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORDDLRAT", "Originating Discount Dollar amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("OrigDiscountReceived", "Originating Discount Recognized", Connector.FieldTypeIdCurrency);
            svc30601.AddField("OrigDiscountRemaining", "Originating Discount Remaining", Connector.FieldTypeIdCurrency);
            svc30601.AddField("OrigDiscountNext", "Originating Discount Next", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGUNITCOSTTOTAL", "Originating Unit Cost total", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGMONTHPRICE", "Originating Monthly price", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORIGINVOICEDDISC", "Originating Invoiced Discount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("OrigNewInvoiceAmount", "Originating New Invoice amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            svc30601.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            rm00101.AddField("RM00101.CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var contractTransferStatus = svc30601.AddField("Contract_Transfer_Status", "Contract Transfer status", Connector.FieldTypeIdEnum);
            contractTransferStatus.AddListItems(1, new List<string> { "Not Transferred", "Transferred", "Auto-Renewed" });

            var billingStatus = svc30601.AddField("BILSTAT", "Billing status", Connector.FieldTypeIdEnum);
            billingStatus.AddListItems(1, new List<string> { "Invoiced", "Cancelled", "On Hold", "Pending Cancellation", "Evergreen Terminated" });

            var contractPeriod = svc30601.AddField("Contract_Period", "Contract period", Connector.FieldTypeIdEnum);
            contractPeriod.AddListItems(1, new List<string> { "Days", "Months", "Years" });

            var pmMonth = svc30601.AddField("SVC_PM_date", "PM Month", Connector.FieldTypeIdEnum);
            pmMonth.AddListItems(1, new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
        }

        public ConnectorEntity GetRmaLineEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListRmaLines), "RMA lines", ParentConnector);

            var svc05200 = entity.AddTable("SVC05200");
            AddRmaLineEntityFields(svc05200);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYS", Connector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Decimal Places Currency", Connector.FieldTypeIdInteger);
            entity.AddCalculation("ODECPLCU - 1", "Originating Decimal Places Currency", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRmaLineEntityFields(ConnectorTable svc05200)
        {
            svc05200.AddField("RETDOCID", "Return Document ID", Connector.FieldTypeIdString, true);
            var returnRecordType = svc05200.AddField("Return_Record_Type", "Return Record type", Connector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc05200.AddField("LNSEQNBR", "Line SEQ number", Connector.FieldTypeIdInteger, true);
            svc05200.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString, true);
            svc05200.AddField("RMA_Status", "RMA status", Connector.FieldTypeIdInteger, true);
            svc05200.AddField("RETSTAT", "Return status", Connector.FieldTypeIdString, true);
            svc05200.AddField("Received", "Received", Connector.FieldTypeIdYesNo, true);
            svc05200.AddField("SVC_Ready_To_Close", "Ready To Close", Connector.FieldTypeIdYesNo, true);
            svc05200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger, true);
            var returnOrigin = svc05200.AddField("RETORIG", "Return Origin", Connector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc05200.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc05200.AddField("ETADTE", "ETA date", Connector.FieldTypeIdDate, true);
            svc05200.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate, true);
            svc05200.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);
            svc05200.AddField("OFFID", "Office ID", Connector.FieldTypeIdString, true);
            svc05200.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc05200.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc05200.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svc05200.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency, true);
            svc05200.AddField("XTNDPRCE", "Extended price", Connector.FieldTypeIdCurrency, true);

            svc05200.AddField("SVC_Next_Line_SEQ_Number", "Next Line Sequence number", Connector.FieldTypeIdInteger);
            svc05200.AddField("SVC_Prev_Line_SEQ_Number", "Prev Line Sequence number", Connector.FieldTypeIdInteger);
            svc05200.AddField("Traveler_Printed", "Traveler Printed", Connector.FieldTypeIdYesNo);
            svc05200.AddField("SVC_RMA_Reason_Code", "RMA Reason code", Connector.FieldTypeIdString);
            svc05200.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code description", Connector.FieldTypeIdString);
            svc05200.AddField("RETREF", "Return Reference", Connector.FieldTypeIdString);
            svc05200.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString);
            svc05200.AddField("EQPLINE", "Equipment Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05200.AddField("LNITMSEQ", "Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05200.AddField("SVC_RMA_From_Service", "RMA From Service", Connector.FieldTypeIdYesNo);
            svc05200.AddField("SOPNUMBE", "SOP number", Connector.FieldTypeIdString);
            svc05200.AddField("CMPNTSEQ", "Component Sequence", Connector.FieldTypeIdInteger);
            svc05200.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05200.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc05200.AddField("ETATME", "ETA time", Connector.FieldTypeIdTime);
            svc05200.AddField("Commit_date", "Commit date", Connector.FieldTypeIdDate);
            svc05200.AddField("Commit_Time", "Commit time", Connector.FieldTypeIdTime);
            svc05200.AddField("Return_Time", "Return time", Connector.FieldTypeIdTime);
            svc05200.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc05200.AddField("PRMdate", "Promised date", Connector.FieldTypeIdDate);
            svc05200.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svc05200.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            svc05200.AddField("RTRNNAME", "Return Address name", Connector.FieldTypeIdString);
            svc05200.AddField("RETADDR1", "Return Address 1", Connector.FieldTypeIdString);
            svc05200.AddField("RETADDR2", "Return Address 2", Connector.FieldTypeIdString);
            svc05200.AddField("RETADDR3", "Return Address 3", Connector.FieldTypeIdString);
            svc05200.AddField("RTRNCITY", "Return Address City", Connector.FieldTypeIdString);
            svc05200.AddField("SVC_Return_State", "Return State", Connector.FieldTypeIdString);
            svc05200.AddField("RTRNZIP", "Return Address Zip code", Connector.FieldTypeIdString);
            svc05200.AddField("Return_Country", "Return Country", Connector.FieldTypeIdString);
            svc05200.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString);
            svc05200.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            svc05200.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc05200.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc05200.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc05200.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc05200.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc05200.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc05200.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc05200.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc05200.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc05200.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc05200.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString);
            svc05200.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity);
            svc05200.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svc05200.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString);
            svc05200.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("ORUNTCST", "Originating Unit cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("OREXTCST", "Originating Extended cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("UNITPRCE", "Unit price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("ORUNTPRC", "Originating Unit price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("OXTNDPRC", "Originating Extended price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("CUSTOWN", "Customer Owned", Connector.FieldTypeIdYesNo);
            svc05200.AddField("FACTSEAL", "Factory Sealed", Connector.FieldTypeIdYesNo);
            svc05200.AddField("ORDDOCID", "Order Document ID", Connector.FieldTypeIdString);
            svc05200.AddField("TRANSLINESEQ", "Transfer Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05200.AddField("STATUS", "Transfer status", Connector.FieldTypeIdInteger);
            svc05200.AddField("Flat_Rate_Repair_Price", "Flat Rate Repair price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Orig_Flat_RepairPrice", "Originating Flat Rate Repair price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Repair_Price", "Repair price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Repair_Price", "Originating Repair price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("NTE_Price", "NTE price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_NTE_Price", "Originating NTE price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Repair_Cost", "Repair cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Repair_Cost", "Originating Repair cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Bill_of_Lading", "Bill of Lading", Connector.FieldTypeIdString);
            svc05200.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc05200.AddField("Credit_SOP_Number", "Credit SOP number", Connector.FieldTypeIdString);
            svc05200.AddField("Credit_SOP_Line_Item_Seq", "Credit SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05200.AddField("Replace_SOP_Number", "Replace SOP number", Connector.FieldTypeIdString);
            svc05200.AddField("Replace_SOP_Line_Item_Se", "Replace SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05200.AddField("Location_Code_Replacemen", "Location Code Replacement", Connector.FieldTypeIdString);
            svc05200.AddField("Replace_Item_Number", "Replace Item number", Connector.FieldTypeIdString);
            svc05200.AddField("Replace_U_Of_M", "Replace U of M", Connector.FieldTypeIdString);
            svc05200.AddField("Replace_Price_Level", "Replace Price Level", Connector.FieldTypeIdString);
            svc05200.AddField("Replace_QTY", "Replace quantity", Connector.FieldTypeIdQuantity);
            svc05200.AddField("Replace_Cost", "Replace cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Replace_Cost", "Originating Replace cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Replace_Price", "Replace price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Replace_Cost", "Originating Replace price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("SOP_Number_Invoice", "SOP Number Invoice", Connector.FieldTypeIdString);
            svc05200.AddField("Item_Number_Invoice", "Item Number Invoice", Connector.FieldTypeIdString);
            svc05200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc05200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc05200.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc05200.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc05200.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc05200.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc05200.AddField("Return_Item_Number", "Return Item number", Connector.FieldTypeIdString);
            svc05200.AddField("Return_Item_Description", "Return Item description", Connector.FieldTypeIdString);
            svc05200.AddField("Return_Location_Code", "Return Location code", Connector.FieldTypeIdString);
            svc05200.AddField("Return_QTY", "Return quantity", Connector.FieldTypeIdQuantity);
            svc05200.AddField("Return_U_Of_M", "Return U of M", Connector.FieldTypeIdString);
            svc05200.AddField("RETCOST", "Return cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Return_Cost", "Originating Return cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Extended_Return_Cost", "Extended Return cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Orig_Ext_Return_Cost", "Originating Extended Return cost", Connector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Return_Price_Level", "Return Price Level", Connector.FieldTypeIdString);
            svc05200.AddField("SVC_Return_Price", "Return price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("Originating_Return_Price", "Originating Return price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Extended_Return_Pric", "Extended Return price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_Orig_Ext_Return_Pric", "Originating Extended Return price", Connector.FieldTypeIdCurrency);
            svc05200.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            svc05200.AddField("SVC_SCM_Complete", "SCM Complete", Connector.FieldTypeIdInteger);

            var serviceRecordType = svc05200.AddField("SRVRECTYPE", "Service Record type", Connector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var sopType = svc05200.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });

            var creditSopType = svc05200.AddField("Credit_SOP_Type", "Credit SOP type", Connector.FieldTypeIdEnum);
            creditSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });

            var replaceSopType = svc05200.AddField("Replace_SOP_Type", "Replace SOP type", Connector.FieldTypeIdEnum);
            replaceSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
        }

        public ConnectorEntity GetHistoricalRmaEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListHistRma), "Historical RMAs", ParentConnector);

            var svc35000 = entity.AddTable("SVC35000");
            AddHistoricalRmaEntityFields(svc35000);

            entity.AddCalculation("DECPLACS - 1", "Decimal Places", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalRmaEntityFields(ConnectorTable svc35000)
        {
            svc35000.AddField("RETDOCID", "Return Document ID", Connector.FieldTypeIdString, true);
            var returnRecordType = svc35000.AddField("Return_Record_Type", "Return Record type", Connector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc35000.AddField("RMA_Status", "RMA status", Connector.FieldTypeIdInteger, true);
            svc35000.AddField("Received", "Received", Connector.FieldTypeIdYesNo, true);
            var returnOrigin = svc35000.AddField("RETORIG", "Return Origin", Connector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc35000.AddField("RETREF", "Return Reference", Connector.FieldTypeIdString, true);
            svc35000.AddField("RETSTAT", "Return status", Connector.FieldTypeIdString, true);
            svc35000.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString, true);
            svc35000.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc35000.AddField("ETADTE", "ETA date", Connector.FieldTypeIdDate, true);
            svc35000.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate, true);
            svc35000.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);
            svc35000.AddField("OFFID", "Office ID", Connector.FieldTypeIdString, true);
            svc35000.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc35000.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString, true);
            svc35000.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc35000.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc35000.AddField("ETATME", "ETA time", Connector.FieldTypeIdTime);
            svc35000.AddField("Return_Time", "Return time", Connector.FieldTypeIdTime);
            svc35000.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc35000.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            svc35000.AddField("RTRNNAME", "Return Address name", Connector.FieldTypeIdString);
            svc35000.AddField("RETADDR1", "Return Address 1", Connector.FieldTypeIdString);
            svc35000.AddField("RETADDR2", "Return Address 2", Connector.FieldTypeIdString);
            svc35000.AddField("RETADDR3", "Return Address 3", Connector.FieldTypeIdString);
            svc35000.AddField("RTRNCITY", "Return Address City", Connector.FieldTypeIdString);
            svc35000.AddField("SVC_Return_State", "Return State", Connector.FieldTypeIdString);
            svc35000.AddField("RTRNZIP", "Return Address Zip code", Connector.FieldTypeIdString);
            svc35000.AddField("Return_Country", "Return Country", Connector.FieldTypeIdString);
            svc35000.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            svc35000.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc35000.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc35000.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc35000.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc35000.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc35000.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc35000.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc35000.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc35000.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc35000.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString);
            svc35000.AddField("EQPLINE", "Equipment Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35000.AddField("LNITMSEQ", "Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35000.AddField("Bill_of_Lading", "Bill of Lading", Connector.FieldTypeIdString);
            svc35000.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc35000.AddField("SOPNUMBE", "SOP number", Connector.FieldTypeIdString);
            svc35000.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35000.AddField("CMPNTSEQ", "Component Sequence", Connector.FieldTypeIdInteger);
            svc35000.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc35000.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc35000.AddField("Commit_date", "Commit date", Connector.FieldTypeIdDate);
            svc35000.AddField("Commit_Time", "Commit time", Connector.FieldTypeIdTime);
            svc35000.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc35000.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc35000.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc35000.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc35000.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc35000.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc35000.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc35000.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc35000.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc35000.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc35000.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc35000.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc35000.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc35000.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc35000.AddField("ISMCTRX", "Is MC Trx", Connector.FieldTypeIdInteger);
            svc35000.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc35000.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc35000.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc35000.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString);
            svc35000.AddField("SVC_RMA_Reason_Code", "RMA Reason code", Connector.FieldTypeIdString);
            svc35000.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code Desc", Connector.FieldTypeIdString);
            svc35000.AddField("SVC_RMA_From_Service", "RMA From Service", Connector.FieldTypeIdString);
            svc35000.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdYesNo);

            var serviceRecordType = svc35000.AddField("SRVRECTYPE", "Service Record type", Connector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var sopType = svc35000.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
        }

        public ConnectorEntity GetHistoricalRmaLineEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListHistRmaLines), "Historical RMA lines", ParentConnector);

            var svc35200 = entity.AddTable("SVC35200");
            AddHistoricalRmaLineEntityFields(svc35200);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYS", Connector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Decimal Places Currency", Connector.FieldTypeIdInteger);
            entity.AddCalculation("ODECPLCU - 1", "Originating Decimal Places Currency", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalRmaLineEntityFields(ConnectorTable svc35200)
        {
            svc35200.AddField("RETDOCID", "Return Document ID", Connector.FieldTypeIdString, true);
            var returnRecordType = svc35200.AddField("Return_Record_Type", "Return Record type", Connector.FieldTypeIdEnum, true);
            returnRecordType.AddListItems(1, new List<string> { "Open", "Closed" });
            svc35200.AddField("LNSEQNBR", "Line SEQ number", Connector.FieldTypeIdInteger, true);
            svc35200.AddField("RETTYPE", "Return type", Connector.FieldTypeIdString, true);
            svc35200.AddField("RMA_Status", "RMA status", Connector.FieldTypeIdInteger, true);
            svc35200.AddField("RETSTAT", "Return status", Connector.FieldTypeIdString, true);
            svc35200.AddField("Received", "Received", Connector.FieldTypeIdYesNo, true);
            svc35200.AddField("SVC_Ready_To_Close", "Ready To Close", Connector.FieldTypeIdYesNo, true);
            svc35200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger, true);
            var returnOrigin = svc35200.AddField("RETORIG", "Return Origin", Connector.FieldTypeIdEnum, true);
            returnOrigin.AddListItems(1, new List<string> { "None", "Service Call", "Sales Invoice" });
            svc35200.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc35200.AddField("ETADTE", "ETA date", Connector.FieldTypeIdDate, true);
            svc35200.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate, true);
            svc35200.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);
            svc35200.AddField("OFFID", "Office ID", Connector.FieldTypeIdString, true);
            svc35200.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc35200.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc35200.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svc35200.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency, true);
            svc35200.AddField("XTNDPRCE", "Extended price", Connector.FieldTypeIdCurrency, true);

            svc35200.AddField("SVC_Next_Line_SEQ_Number", "Next Line Sequence number", Connector.FieldTypeIdInteger);
            svc35200.AddField("SVC_Prev_Line_SEQ_Number", "Prev Line Sequence number", Connector.FieldTypeIdInteger);
            svc35200.AddField("Traveler_Printed", "Traveler Printed", Connector.FieldTypeIdYesNo);
            svc35200.AddField("SVC_RMA_Reason_Code", "RMA Reason code", Connector.FieldTypeIdString);
            svc35200.AddField("SVC_RMA_Reason_Code_Desc", "RMA Reason Code description", Connector.FieldTypeIdString);
            svc35200.AddField("RETREF", "Return Reference", Connector.FieldTypeIdString);
            svc35200.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString);
            svc35200.AddField("EQPLINE", "Equipment Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35200.AddField("LNITMSEQ", "Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35200.AddField("SVC_RMA_From_Service", "RMA From Service", Connector.FieldTypeIdYesNo);
            svc35200.AddField("SOPNUMBE", "SOP number", Connector.FieldTypeIdString);
            svc35200.AddField("CMPNTSEQ", "Component Sequence", Connector.FieldTypeIdInteger);
            svc35200.AddField("SOP_Line_Item_Sequence", "SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35200.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc35200.AddField("ETATME", "ETA time", Connector.FieldTypeIdTime);
            svc35200.AddField("Commit_date", "Commit date", Connector.FieldTypeIdDate);
            svc35200.AddField("Commit_Time", "Commit time", Connector.FieldTypeIdTime);
            svc35200.AddField("Return_Time", "Return time", Connector.FieldTypeIdTime);
            svc35200.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc35200.AddField("PRMdate", "Promised date", Connector.FieldTypeIdDate);
            svc35200.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svc35200.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            svc35200.AddField("RTRNNAME", "Return Address name", Connector.FieldTypeIdString);
            svc35200.AddField("RETADDR1", "Return Address 1", Connector.FieldTypeIdString);
            svc35200.AddField("RETADDR2", "Return Address 2", Connector.FieldTypeIdString);
            svc35200.AddField("RETADDR3", "Return Address 3", Connector.FieldTypeIdString);
            svc35200.AddField("RTRNCITY", "Return Address City", Connector.FieldTypeIdString);
            svc35200.AddField("SVC_Return_State", "Return State", Connector.FieldTypeIdString);
            svc35200.AddField("RTRNZIP", "Return Address Zip code", Connector.FieldTypeIdString);
            svc35200.AddField("Return_Country", "Return Country", Connector.FieldTypeIdString);
            svc35200.AddField("CUSTNMBR", "Customer ID", Connector.FieldTypeIdString);
            svc35200.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            svc35200.AddField("CONTACT", "Contact", Connector.FieldTypeIdString);
            svc35200.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            svc35200.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            svc35200.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            svc35200.AddField("CITY", "City", Connector.FieldTypeIdString);
            svc35200.AddField("STATE", "State", Connector.FieldTypeIdString);
            svc35200.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc35200.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            svc35200.AddField("Bill_To_Customer", "Bill To Customer ID", Connector.FieldTypeIdString);
            svc35200.AddField("SVC_Bill_To_Address_Code", "Bill To Address code", Connector.FieldTypeIdString);
            svc35200.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString);
            svc35200.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity);
            svc35200.AddField("UOFM", "U of M", Connector.FieldTypeIdString);
            svc35200.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString);
            svc35200.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("ORUNTCST", "Originating Unit cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("OREXTCST", "Originating Extended cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("UNITPRCE", "Unit price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("ORUNTPRC", "Originating Unit price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("OXTNDPRC", "Originating Extended price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("CUSTOWN", "Customer Owned", Connector.FieldTypeIdYesNo);
            svc35200.AddField("FACTSEAL", "Factory Sealed", Connector.FieldTypeIdYesNo);
            svc35200.AddField("ORDDOCID", "Order Document ID", Connector.FieldTypeIdString);
            svc35200.AddField("TRANSLINESEQ", "Transfer Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35200.AddField("STATUS", "Transfer status", Connector.FieldTypeIdInteger);
            svc35200.AddField("Flat_Rate_Repair_Price", "Flat Rate Repair price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Orig_Flat_RepairPrice", "Originating Flat Rate Repair price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Repair_Price", "Repair price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Repair_Price", "Originating Repair price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("NTE_Price", "NTE price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_NTE_Price", "Originating NTE price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Repair_Cost", "Repair cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Repair_Cost", "Originating Repair cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Bill_of_Lading", "Bill of Lading", Connector.FieldTypeIdString);
            svc35200.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc35200.AddField("Credit_SOP_Number", "Credit SOP number", Connector.FieldTypeIdString);
            svc35200.AddField("Credit_SOP_Line_Item_Seq", "Credit SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35200.AddField("Replace_SOP_Number", "Replace SOP number", Connector.FieldTypeIdString);
            svc35200.AddField("Replace_SOP_Line_Item_Se", "Replace SOP Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35200.AddField("Location_Code_Replacemen", "Location Code Replacement", Connector.FieldTypeIdString);
            svc35200.AddField("Replace_Item_Number", "Replace Item number", Connector.FieldTypeIdString);
            svc35200.AddField("Replace_U_Of_M", "Replace U of M", Connector.FieldTypeIdString);
            svc35200.AddField("Replace_Price_Level", "Replace Price Level", Connector.FieldTypeIdString);
            svc35200.AddField("Replace_QTY", "Replace quantity", Connector.FieldTypeIdQuantity);
            svc35200.AddField("Replace_Cost", "Replace cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Replace_Cost", "Originating Replace cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Replace_Price", "Replace price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Replace_Cost", "Originating Replace price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("SOP_Number_Invoice", "SOP Number Invoice", Connector.FieldTypeIdString);
            svc35200.AddField("Item_Number_Invoice", "Item Number Invoice", Connector.FieldTypeIdString);
            svc35200.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc35200.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc35200.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc35200.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc35200.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc35200.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc35200.AddField("Return_Item_Number", "Return Item number", Connector.FieldTypeIdString);
            svc35200.AddField("Return_Item_Description", "Return Item description", Connector.FieldTypeIdString);
            svc35200.AddField("Return_Location_Code", "Return Location code", Connector.FieldTypeIdString);
            svc35200.AddField("Return_QTY", "Return quantity", Connector.FieldTypeIdQuantity);
            svc35200.AddField("Return_U_Of_M", "Return U of M", Connector.FieldTypeIdString);
            svc35200.AddField("RETCOST", "Return cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Return_Cost", "Originating Return cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Extended_Return_Cost", "Extended Return cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Orig_Ext_Return_Cost", "Originating Extended Return cost", Connector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Return_Price_Level", "Return Price Level", Connector.FieldTypeIdString);
            svc35200.AddField("SVC_Return_Price", "Return price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("Originating_Return_Price", "Originating Return price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Extended_Return_Pric", "Extended Return price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_Orig_Ext_Return_Pric", "Originating Extended Return price", Connector.FieldTypeIdCurrency);
            svc35200.AddField("SVC_FO_ID", "FO ID", Connector.FieldTypeIdString);
            svc35200.AddField("SVC_SCM_Complete", "SCM Complete", Connector.FieldTypeIdInteger);

            var serviceRecordType = svc35200.AddField("SRVRECTYPE", "Service Record type", Connector.FieldTypeIdEnum);
            serviceRecordType.AddListItems(1, new List<string> { "Quote", "Open", "Invoiced" });

            var sopType = svc35200.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });

            var creditSopType = svc35200.AddField("Credit_SOP_Type", "Credit SOP type", Connector.FieldTypeIdEnum);
            creditSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });

            var replaceSopType = svc35200.AddField("Replace_SOP_Type", "Replace SOP type", Connector.FieldTypeIdEnum);
            replaceSopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
        }

        public ConnectorEntity GetHistoricalRtvEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListHistRtv), "Historical RTVs", ParentConnector);

            var svc35600 = entity.AddTable("SVC35600");
            AddHistoricalRtvEntityFields(svc35600);

            return entity;
        }
        public void AddHistoricalRtvEntityFields(ConnectorTable svc35600)
        {
            svc35600.AddField("RTV_Number", "RTV number", Connector.FieldTypeIdString, true);
            svc35600.AddField("RTV_Type", "RTV type", Connector.FieldTypeIdString, true);
            svc35600.AddField("RTV_Return_Status", "RTV Return status", Connector.FieldTypeIdString, true);
            svc35600.AddField("VRMA_Document_ID", "VRMA Document ID", Connector.FieldTypeIdString, true);
            svc35600.AddField("RETDOCID", "RMA number", Connector.FieldTypeIdString, true);
            svc35600.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svc35600.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString, true);
            svc35600.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc35600.AddField("VENDORID", "Entry date", Connector.FieldTypeIdDate, true);
            svc35600.AddField("Shipped_date", "Shipped date", Connector.FieldTypeIdDate, true);
            svc35600.AddField("receiptdate", "Receipt date", Connector.FieldTypeIdDate, true);
            svc35600.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);
            svc35600.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc35600.AddField("Travel_Price", "Travel price", Connector.FieldTypeIdCurrency, true);
            svc35600.AddField("Bill_of_Lading_Out", "Bill of Landing (Out)", Connector.FieldTypeIdString, true);
            svc35600.AddField("Shipping_Method_Out", "Shipping Method (Out)", Connector.FieldTypeIdString, true);
            svc35600.AddField("VOIDSTTS", "Void status", Connector.FieldTypeIdInteger, true);
            svc35600.AddField("LNSEQNBR", "Line Sequence number", Connector.FieldTypeIdInteger);
            svc35600.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_Name", "Ship Address name", Connector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_1", "Ship Address 1", Connector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_2", "Ship Address 2", Connector.FieldTypeIdString);
            svc35600.AddField("Ship_Address_3", "Ship Address 3", Connector.FieldTypeIdString);
            svc35600.AddField("Ship_City", "Ship City", Connector.FieldTypeIdString);
            svc35600.AddField("Ship_State", "Ship State", Connector.FieldTypeIdString);
            svc35600.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc35600.AddField("Ship_Country", "Ship Country", Connector.FieldTypeIdString);
            svc35600.AddField("ADRSCODE", "Entry time", Connector.FieldTypeIdTime);
            svc35600.AddField("Ship_Address_Name", "Promised date", Connector.FieldTypeIdDate);
            svc35600.AddField("Ship_Address_1", "Promised time", Connector.FieldTypeIdTime);
            svc35600.AddField("Shipped_Time", "Shipped time", Connector.FieldTypeIdTime);
            svc35600.AddField("Receipt_Time", "Receipt time", Connector.FieldTypeIdTime);
            svc35600.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc35600.AddField("LOCCODEB", "Location Code Bad", Connector.FieldTypeIdString);
            svc35600.AddField("Part_Price", "Part price", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Part_Cost", "Part cost", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Labor_Price", "Labor price", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Labor_Cost", "Labor cost", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Expense_Price", "Expense price", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Expense_Cost", "Expense cost", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Travel_Cost", "Travel cost", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Bill_of_Lading", "Bill of Landing", Connector.FieldTypeIdString);
            svc35600.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc35600.AddField("OFFID", "Office ID", Connector.FieldTypeIdString);
            svc35600.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc35600.AddField("VCHNUMWK", "Voucher Number (Work)", Connector.FieldTypeIdString);
            svc35600.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", Connector.FieldTypeIdString);
            svc35600.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", Connector.FieldTypeIdString);
            svc35600.AddField("CUSTOWN", "Customer Owned", Connector.FieldTypeIdYesNo);
            svc35600.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            svc35600.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc35600.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc35600.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc35600.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc35600.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc35600.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svc35600.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svc35600.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svc35600.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svc35600.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svc35600.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svc35600.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svc35600.AddField("RATECALC", "Rate Calc method", Connector.FieldTypeIdInteger);
            svc35600.AddField("VIEWMODE", "View Mode", Connector.FieldTypeIdInteger);
            svc35600.AddField("ISMCTRX", "IS MC Trx", Connector.FieldTypeIdInteger);
            svc35600.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svc35600.AddField("DENXRATE", "Denomination Exchange rate", Connector.FieldTypeIdQuantity);
            svc35600.AddField("MCTRXSTT", "MC Transaction State", Connector.FieldTypeIdInteger);
            svc35600.AddField("Originating_Part_Price", "Originating Part price", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Part_Cost", "Originating Part cost", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Labor_Price", "Originating Labor price", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Labor_Cost", "Originating Labor cost", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_ExpensePrice", "Originating Expense price", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Expense_Cost", "Originating Expense cost", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Travel_Price", "Originating Travel price", Connector.FieldTypeIdCurrency);
            svc35600.AddField("Originating_Travel_Cost", "Originating Travel cost", Connector.FieldTypeIdCurrency);

            var rtvStatus = svc35600.AddField("RTV_Status", "RTV status", Connector.FieldTypeIdEnum);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
        }

        public ConnectorEntity GetRtvLineEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListRtvLines), "RTV lines", ParentConnector);

            var svc05601 = entity.AddTable("SVC05601");
            AddRtvLineEntityFields(svc05601);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYs", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddRtvLineEntityFields(ConnectorTable svc05601)
        {
            svc05601.AddField("RTV_Number", "RTV number", Connector.FieldTypeIdString, true);
            svc05601.AddField("RTV_Line", "RTV Line", Connector.FieldTypeIdInteger, true);
            svc05601.AddField("RTV_Type", "RTV type", Connector.FieldTypeIdString, true);
            var rtvStatus = svc05601.AddField("RTV_Status", "RTV status", Connector.FieldTypeIdEnum, true);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            svc05601.AddField("RTV_Return_Status", "RTV Return status", Connector.FieldTypeIdString, true);
            svc05601.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString, true);
            svc05601.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svc05601.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            svc05601.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            svc05601.AddField("QTYSHPPD", "Quantity Shipped", Connector.FieldTypeIdQuantity, true);
            svc05601.AddField("QTYRECVD", "Quantity Received", Connector.FieldTypeIdQuantity, true);
            svc05601.AddField("UOFM", "U of M", Connector.FieldTypeIdString, true);
            svc05601.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svc05601.AddField("VNDITNUM", "Vendor Item number", Connector.FieldTypeIdString, true);
            svc05601.AddField("Return_Item_Number", "Return Item number", Connector.FieldTypeIdString, true);
            svc05601.AddField("LOCCODEB", "Location Code Bad", Connector.FieldTypeIdString, true);
            svc05601.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc05601.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc05601.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc05601.AddField("Shipped_date", "Shipped date", Connector.FieldTypeIdDate, true);
            svc05601.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);

            svc05601.AddField("VRMA_Document_ID", "VRMA Document ID", Connector.FieldTypeIdString);
            svc05601.AddField("RETDOCID", "RMA number", Connector.FieldTypeIdString);
            svc05601.AddField("LNSEQNBR", "Line Sequence number", Connector.FieldTypeIdInteger);
            svc05601.AddField("SVC_Process_SEQ_Number", "Process Sequence number", Connector.FieldTypeIdInteger);
            svc05601.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svc05601.AddField("Reference2", "Reference 2", Connector.FieldTypeIdString);
            svc05601.AddField("QTY_To_Receive", "Quantity To Receive", Connector.FieldTypeIdQuantity);
            svc05601.AddField("QTYCANCE", "Quantity Canceled", Connector.FieldTypeIdQuantity);
            svc05601.AddField("OFFID", "Office ID", Connector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_Name", "Ship Address name", Connector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_1", "Ship Address 1", Connector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_2", "Ship Address 2", Connector.FieldTypeIdString);
            svc05601.AddField("Ship_Address_3", "Ship Address 3", Connector.FieldTypeIdString);
            svc05601.AddField("Ship_City", "Ship City", Connector.FieldTypeIdString);
            svc05601.AddField("Ship_State", "Ship State", Connector.FieldTypeIdString);
            svc05601.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc05601.AddField("Ship_Country", "Ship Country", Connector.FieldTypeIdString);
            svc05601.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc05601.AddField("PRMdate", "Promised date", Connector.FieldTypeIdDate);
            svc05601.AddField("Promised_Time", "Promised time", Connector.FieldTypeIdTime);
            svc05601.AddField("Shipped_Time", "Shipped time", Connector.FieldTypeIdTime);
            svc05601.AddField("receiptdate", "Receipt date", Connector.FieldTypeIdDate);
            svc05601.AddField("Receipt_Time", "Receipt time", Connector.FieldTypeIdTime);
            svc05601.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc05601.AddField("PONMBRSTR", "Purchase Order number", Connector.FieldTypeIdString);
            svc05601.AddField("POLNSEQ", "Purchase Order Line SEQ", Connector.FieldTypeIdInteger);
            svc05601.AddField("POPRCTNM", "POP Receipt number", Connector.FieldTypeIdString);
            svc05601.AddField("RCPTLNNM", "Receipt Line number", Connector.FieldTypeIdInteger);
            svc05601.AddField("Transfer_Reference", "Transfer Reference", Connector.FieldTypeIdString);
            svc05601.AddField("TRANSLINESEQ", "Transfer Line Sequence", Connector.FieldTypeIdInteger);
            svc05601.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString);
            svc05601.AddField("EQPLINE", "Equipment Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05601.AddField("LINITMTYP", "Line Item type", Connector.FieldTypeIdString);
            svc05601.AddField("LNITMSEQ", "Line Item Sequence", Connector.FieldTypeIdInteger);
            svc05601.AddField("Bill_of_Lading_Out", "Bill of Lading (Out)", Connector.FieldTypeIdString);
            svc05601.AddField("Shipping_Method_Out", "Shipping Method (Out)", Connector.FieldTypeIdString);
            svc05601.AddField("Bill_of_Lading", "Bill of Lading", Connector.FieldTypeIdString);
            svc05601.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc05601.AddField("Tracking_Number", "Tracking number", Connector.FieldTypeIdString);
            svc05601.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc05601.AddField("VCHNUMWK", "Voucher Number (Work)", Connector.FieldTypeIdString);
            svc05601.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", Connector.FieldTypeIdString);
            svc05601.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", Connector.FieldTypeIdString);
            svc05601.AddField("CUSTOWN", "Customer Owned", Connector.FieldTypeIdYesNo);
            svc05601.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc05601.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc05601.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc05601.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc05601.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc05601.AddField("Part_Price", "Part price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Part_Cost", "Part cost", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Labor_Price", "Labor price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Labor_Cost", "Labor cost", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Expense_Price", "Expense price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Expense_Cost", "Expense cost", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Travel_Price", "Travel price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Travel_Cost", "Travel cost", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Part_Price", "Originating Part price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Part_Cost", "Originating Part cost", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Labor_Price", "Originating Labor price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Labor_Cost", "Originating Labor cost", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_ExpensePrice", "Originating Expense price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Expense_Cost", "Originating Expense cost", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Travel_Price", "Originating Travel price", Connector.FieldTypeIdCurrency);
            svc05601.AddField("Originating_Travel_Cost", "Originating Travel cost", Connector.FieldTypeIdCurrency);

            var itemTrackingOption = svc05601.AddField(" SVC05601.ITMTRKOP", "Item Tracking Option", Connector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
        }

        public ConnectorEntity GetHistoricalRtvLineEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FieldServiceSmartListHistRtvLines), "Historical RTV lines", ParentConnector);

            var svc35601 = entity.AddTable("SVC35601");
            AddHistoricalRtvLineEntityFields(svc35601);

            entity.AddCalculation("DECPLQTY - 1", "Decimal Places QTYs", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddHistoricalRtvLineEntityFields(ConnectorTable svc35601)
        {
            svc35601.AddField("RTV_Number", "RTV number", Connector.FieldTypeIdString, true);
            svc35601.AddField("RTV_Line", "RTV Line", Connector.FieldTypeIdInteger, true);
            svc35601.AddField("RTV_Type", "RTV type", Connector.FieldTypeIdString, true);
            var rtvStatus = svc35601.AddField("RTV_Status", "RTV status", Connector.FieldTypeIdEnum, true);
            rtvStatus.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            svc35601.AddField("RTV_Return_Status", "RTV Return status", Connector.FieldTypeIdString, true);
            svc35601.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString, true);
            svc35601.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svc35601.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            svc35601.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            svc35601.AddField("QTYSHPPD", "Quantity Shipped", Connector.FieldTypeIdQuantity, true);
            svc35601.AddField("QTYRECVD", "Quantity Received", Connector.FieldTypeIdQuantity, true);
            svc35601.AddField("UOFM", "U of M", Connector.FieldTypeIdString, true);
            svc35601.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            svc35601.AddField("VNDITNUM", "Vendor Item number", Connector.FieldTypeIdString, true);
            svc35601.AddField("Return_Item_Number", "Return Item number", Connector.FieldTypeIdString, true);
            svc35601.AddField("LOCCODEB", "Location Code Bad", Connector.FieldTypeIdString, true);
            svc35601.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            svc35601.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString, true);
            svc35601.AddField("ENTDTE", "Entry date", Connector.FieldTypeIdDate, true);
            svc35601.AddField("Shipped_date", "Shipped date", Connector.FieldTypeIdDate, true);
            svc35601.AddField("COMPDTE", "Complete date", Connector.FieldTypeIdDate, true);

            svc35601.AddField("VRMA_Document_ID", "VRMA Document ID", Connector.FieldTypeIdString);
            svc35601.AddField("RETDOCID", "RMA number", Connector.FieldTypeIdString);
            svc35601.AddField("LNSEQNBR", "Line Sequence number", Connector.FieldTypeIdInteger);
            svc35601.AddField("SVC_Process_SEQ_Number", "Process Sequence number", Connector.FieldTypeIdInteger);
            svc35601.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svc35601.AddField("Reference2", "Reference 2", Connector.FieldTypeIdString);
            svc35601.AddField("QTY_To_Receive", "Quantity To Receive", Connector.FieldTypeIdQuantity);
            svc35601.AddField("QTYCANCE", "Quantity Canceled", Connector.FieldTypeIdQuantity);
            svc35601.AddField("OFFID", "Office ID", Connector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_Name", "Ship Address name", Connector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_1", "Ship Address 1", Connector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_2", "Ship Address 2", Connector.FieldTypeIdString);
            svc35601.AddField("Ship_Address_3", "Ship Address 3", Connector.FieldTypeIdString);
            svc35601.AddField("Ship_City", "Ship City", Connector.FieldTypeIdString);
            svc35601.AddField("Ship_State", "Ship State", Connector.FieldTypeIdString);
            svc35601.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            svc35601.AddField("Ship_Country", "Ship Country", Connector.FieldTypeIdString);
            svc35601.AddField("ENTTME", "Entry time", Connector.FieldTypeIdTime);
            svc35601.AddField("PRMdate", "Promised date", Connector.FieldTypeIdDate);
            svc35601.AddField("Promised_Time", "Promised time", Connector.FieldTypeIdTime);
            svc35601.AddField("Shipped_Time", "Shipped time", Connector.FieldTypeIdTime);
            svc35601.AddField("receiptdate", "Receipt date", Connector.FieldTypeIdDate);
            svc35601.AddField("Receipt_Time", "Receipt time", Connector.FieldTypeIdTime);
            svc35601.AddField("COMPTME", "Complete time", Connector.FieldTypeIdTime);
            svc35601.AddField("PONMBRSTR", "Purchase Order number", Connector.FieldTypeIdString);
            svc35601.AddField("POLNSEQ", "Purchase Order Line SEQ", Connector.FieldTypeIdInteger);
            svc35601.AddField("POPRCTNM", "POP Receipt number", Connector.FieldTypeIdString);
            svc35601.AddField("RCPTLNNM", "Receipt Line number", Connector.FieldTypeIdInteger);
            svc35601.AddField("Transfer_Reference", "Transfer Reference", Connector.FieldTypeIdString);
            svc35601.AddField("TRANSLINESEQ", "Transfer Line Sequence", Connector.FieldTypeIdInteger);
            svc35601.AddField("CALLNBR", "Service Call number", Connector.FieldTypeIdString);
            svc35601.AddField("EQPLINE", "Equipment Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35601.AddField("LINITMTYP", "Line Item type", Connector.FieldTypeIdString);
            svc35601.AddField("LNITMSEQ", "Line Item Sequence", Connector.FieldTypeIdInteger);
            svc35601.AddField("Bill_of_Lading_Out", "Bill of Lading (Out)", Connector.FieldTypeIdString);
            svc35601.AddField("Shipping_Method_Out", "Shipping Method (Out)", Connector.FieldTypeIdString);
            svc35601.AddField("Bill_of_Lading", "Bill of Lading", Connector.FieldTypeIdString);
            svc35601.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            svc35601.AddField("Tracking_Number", "Tracking number", Connector.FieldTypeIdString);
            svc35601.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svc35601.AddField("VCHNUMWK", "Voucher Number (Work)", Connector.FieldTypeIdString);
            svc35601.AddField("Voucher_Number_Invoice", "Voucher Number Invoice", Connector.FieldTypeIdString);
            svc35601.AddField("Voucher_Number_Reimburse", "Voucher Number Reimbursement", Connector.FieldTypeIdString);
            svc35601.AddField("CUSTOWN", "Customer Owned", Connector.FieldTypeIdYesNo);
            svc35601.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svc35601.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svc35601.AddField("USRDEF03", "User defined 3", Connector.FieldTypeIdString);
            svc35601.AddField("USRDEF04", "User defined 4", Connector.FieldTypeIdString);
            svc35601.AddField("USRDEF05", "User defined 5", Connector.FieldTypeIdString);
            svc35601.AddField("Part_Price", "Part price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Part_Cost", "Part cost", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Labor_Price", "Labor price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Labor_Cost", "Labor cost", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Expense_Price", "Expense price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Expense_Cost", "Expense cost", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Travel_Price", "Travel price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Travel_Cost", "Travel cost", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Part_Price", "Originating Part price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Part_Cost", "Originating Part cost", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Labor_Price", "Originating Labor price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Labor_Cost", "Originating Labor cost", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_ExpensePrice", "Originating Expense price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Expense_Cost", "Originating Expense cost", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Travel_Price", "Originating Travel price", Connector.FieldTypeIdCurrency);
            svc35601.AddField("Originating_Travel_Cost", "Originating Travel cost", Connector.FieldTypeIdCurrency);

            var itemTrackingOption = svc35601.AddField(" svc35601.ITMTRKOP", "Item Tracking Option", Connector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
        }

    }
}
