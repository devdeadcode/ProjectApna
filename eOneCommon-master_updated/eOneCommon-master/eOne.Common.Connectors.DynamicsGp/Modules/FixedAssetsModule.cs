using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class FixedAssetsModule : DynamicsGpModule
    {

        private const short FixedAssetsSmartListAssets = 1;
        private const short FixedAssetsSmartListAssetBooks = 2;
        private const short FixedAssetsSmartListAssetPurchase = 3;
        
        public FixedAssetsModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 309;
            Name = "Fixed Assets";
            ParentConnector = connector;
            Installed = connector.ObjectExists("FA00100", "U");
        }

        public override void AddEntities()
        {
            Entities.Add(GetFixedAssetEntity());
            Entities.Add(GetFixedAssetBookEntity());
            Entities.Add(GetFixedAssetPurchaseEntity());
        }

        private ConnectorEntity GetFixedAssetEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FixedAssetsSmartListAssets), "Fixed assets", ParentConnector);

            var fa00100 = entity.AddTable("FA00100");

            var fa00400 = entity.AddTable("FA00400", "FA00100");
            fa00400.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            var fa00600 = entity.AddTable("FA00600", "FA00100");
            fa00600.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            var fa00500 = entity.AddTable("FA00500", "FA00100");
            fa00500.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            var fa41600 = entity.AddTable("FA41600", "FA00500");
            fa41600.AddJoinFields("LEASECOMPINDX", "LEASECOMPINDX");

            var fa19900 = entity.AddTable("FA19900", "FA00100");
            fa19900.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            var fa41200 = entity.AddTable("FA41200", "FA00600");
            fa41200.AddJoinFields("INSCLASSINDX", "INSCLASSINDX");

            AddFixedAssetFields(fa00100, fa00400, fa00600, fa00500, fa41600, fa19900, fa41200);

            return entity;
        }
        private static void AddFixedAssetFields(ConnectorTable fa00100, ConnectorTable fa00400, ConnectorTable fa00600, ConnectorTable fa00500, ConnectorTable fa41600, ConnectorTable fa19900, ConnectorTable fa41200)
        {
            fa00100.AddField("ASSETID", "Asset ID", Connector.FieldTypeIdString, true);
            fa00100.AddField("ASSETIDSUF", "Suffix", Connector.FieldTypeIdInteger, true);
            fa00100.AddField("ASSETDESC", "Asset description", Connector.FieldTypeIdString, true);
            fa00100.AddField("ASSETCLASSID", "Asset class ID", Connector.FieldTypeIdString, true);
            fa00100.AddField("ACQdate", "Acquisition date", Connector.FieldTypeIdDate, true);
            fa00100.AddField("Acquisition_Cost", "Acquisition cost", Connector.FieldTypeIdCurrency, true);
            fa00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString, true);
            fa00100.AddField("ASSETQTY", "Asset quantity", Connector.FieldTypeIdInteger);
            fa00100.AddField("ASSETBEGQTY", "Asset begin quantity", Connector.FieldTypeIdInteger);
            fa00100.AddField("EXTASSETDESC", "Extended description", Connector.FieldTypeIdString);
            fa00100.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            fa00100.AddField("ASSESSEDVALUE", "Assessed value", Connector.FieldTypeIdCurrency);
            fa00100.AddField("Master_Asset_ID", "Master asset ID", Connector.FieldTypeIdString);
            fa00100.AddField("Physical_Location_ID", "Physical location ID", Connector.FieldTypeIdString);
            fa00100.AddField("Asset_Label", "Asset label", Connector.FieldTypeIdString);
            fa00100.AddField("PIN", "PIN", Connector.FieldTypeIdString);
            fa00100.AddField("Verified_date", "Verified date", Connector.FieldTypeIdDate);
            fa00100.AddField("STRUCTUREID", "Structure ID", Connector.FieldTypeIdString);
            fa00100.AddField("CUSTODIAN", "Custodian", Connector.FieldTypeIdString);
            fa00100.AddField("MFGRNAME", "Manufacturer name", Connector.FieldTypeIdString);
            fa00100.AddField("SERLNMBR", "Serial number", Connector.FieldTypeIdString);
            fa00100.AddField("MODELNUMBER", "Model number", Connector.FieldTypeIdString);
            fa00100.AddField("WARRENTYdate", "Warranty date", Connector.FieldTypeIdDate);
            fa00100.AddField("LASTMAINTdate", "Last maintenance date", Connector.FieldTypeIdDate);
            fa00100.AddField("ASSETCURRMAINT", "Current maintenance amount", Connector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETYTDMAINT", "Year to date maintenance amount", Connector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETLTDMAINT", "Life to date maintenance amount", Connector.FieldTypeIdCurrency);
            fa00100.AddField("DATEADDED", "Date added", Connector.FieldTypeIdDate);
            fa00100.AddField("DELETEdate", "Delete date", Connector.FieldTypeIdDate);
            fa00100.AddField("LASTPURCHLINESEQ", "Last purchase line sequence", Connector.FieldTypeIdInteger);
            fa00100.AddField("LASTMNTDdate", "Last maintained date", Connector.FieldTypeIdDate);
            fa00100.AddField("LASTMNTDTIME", "Last maintained time", Connector.FieldTypeIdTime);
            fa00100.AddField("LASTMNTDUSERID", "Last maintained user ID", Connector.FieldTypeIdString);
            fa00100.AddField("ASSETindex", "Asset index", Connector.FieldTypeIdInteger);
            fa00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            fa00400.AddField("ASSETindex", "Asset index (account)", Connector.FieldTypeIdInteger);
            fa00400.AddField("DEPREXPACCTINDX", "Depreciation expense account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("DEPRRESVACCTINDX", "Depreciation reserve account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("PRIORYRDEPRACCTINDX", "Prior year depreciation account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("ASSETCOSTACCTINDX", "Asset cost account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("PROCEEDSACCTINDX", "Proceeds account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("RECGAINLOSSACCTINDX", "Rec G/L account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("NONRECGAINLOSSACCTINDX", "Non rec G/L account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("CLEARINGACCTINDX", "Clearing account number (account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("NOTEINDX", "Note index (account)", Connector.FieldTypeIdInteger);
            fa00400.AddField("LASTMNTDdate", "Last maintained Date (account)", Connector.FieldTypeIdDate);
            fa00400.AddField("LASTMNTDTIME", "Last maintained Time (account)", Connector.FieldTypeIdTime);
            fa00400.AddField("LASTMNTDUSERID", "Last maintained User ID (account)", Connector.FieldTypeIdString);
            fa00600.AddField("ASSETindex", "Asset index (insurance)", Connector.FieldTypeIdInteger);
            fa41200.AddField("INSCLASSID", "Insurance class ID (insurance)", Connector.FieldTypeIdString);
            fa00600.AddField("INSCLASSINDX", "Insurance class index (insurance)", Connector.FieldTypeIdInteger);
            fa00600.AddField("INSURANCEYEAR", "Insurance year (insurance)", Connector.FieldTypeIdString);
            fa00600.AddField("INSURANCEVALUE", "Insurance value (insurance)", Connector.FieldTypeIdCurrency);
            fa00600.AddField("REPLACEMENTCOST", "Replacement cost (insurance)", Connector.FieldTypeIdCurrency);
            fa00600.AddField("REPROCOST", "Reproduction cost (insurance)", Connector.FieldTypeIdCurrency);
            fa00600.AddField("DEPRREPROCOST", "Depreciated reproduction cost (insurance)", Connector.FieldTypeIdCurrency);
            fa00600.AddField("EXCLUSIONAMT", "Exclusion amount (insurance)", Connector.FieldTypeIdCurrency);
            fa00600.AddField("EXCLUSIONTYPE", "Exclusion type (insurance)", Connector.FieldTypeIdString);
            fa00600.AddField("NOTEINDX", "Note index (insurance)", Connector.FieldTypeIdInteger);
            fa00600.AddField("LASTMNTDdate", "Last maintained date (insurance)", Connector.FieldTypeIdDate);
            fa00600.AddField("LASTMNTDTIME", "Last maintained time (insurance)", Connector.FieldTypeIdTime);
            fa00600.AddField("LASTMNTDUSERID", "Last maintained user ID (insurance)", Connector.FieldTypeIdString);
            fa00500.AddField("ASSETindex", "Asset index (lease)", Connector.FieldTypeIdInteger);
            fa41600.AddField("LEASECOMPID", "Lease company ID (lease)", Connector.FieldTypeIdString);
            fa00500.AddField("LEASECOMPINDX", "Lease company index (lease)", Connector.FieldTypeIdInteger);
            fa00500.AddField("LEASECONTRACTID", "Lease contract ID (lease)", Connector.FieldTypeIdString);
            fa00500.AddField("LEASEPAYMENT", "Lease payment (lease)", Connector.FieldTypeIdCurrency);
            fa00500.AddField("LEASEINTRATE", "Lease interest rate (lease)", Connector.FieldTypeIdInteger);
            fa00500.AddField("LEASEENDdate", "Lease end date (lease)", Connector.FieldTypeIdDate);
            fa00500.AddField("NOTEINDX", "Note index (lease)", Connector.FieldTypeIdInteger);
            fa00500.AddField("LASTMNTDdate", "Last maintained date (lease)", Connector.FieldTypeIdDate);
            fa00500.AddField("LASTMNTDTIME", "Last maintained time (lease)", Connector.FieldTypeIdTime);
            fa00500.AddField("LASTMNTDUSERID", "Last maintained user ID (lease)", Connector.FieldTypeIdString);
            fa19900.AddField("ASSETindex", "Asset index (user data)", Connector.FieldTypeIdInteger);
            fa19900.AddField("USRFIELD1", "User field 1 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD2", "User field 2 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD3", "User field 3 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD4", "User field 4 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD5", "User field 5 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD6", "User field 6 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD7", "User field 7 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD8", "User field 8 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD9", "User field 9 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD10", "User field 10 (user data)", Connector.FieldTypeIdString);
            fa19900.AddField("USRFIELD11", "User field 11 (user data)", Connector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD12", "User field 12 (user data)", Connector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD13", "User field 13 (user data)", Connector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD14", "User field 14 (user data)", Connector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD15", "User field 15 (user data)", Connector.FieldTypeIdCurrency);
            fa19900.AddField("NOTEINDX", "Note index (user data)", Connector.FieldTypeIdInteger);
            fa19900.AddField("LASTMNTDdate", "Last maintained date (user data)", Connector.FieldTypeIdDate);
            fa19900.AddField("LASTMNTDTIME", "Last maintained time (user data)", Connector.FieldTypeIdTime);
            fa19900.AddField("LASTMNTDUSERID", "Last maintained user ID (user data)", Connector.FieldTypeIdString);

            var assetType = fa00100.AddField("ASSETTYPE", "Asset type", Connector.FieldTypeIdEnum);
            assetType.AddListItems(1, new List<string> { "New", "Used", "Leased" });

            var assetStatus = fa00100.AddField("ASSETSTATUS", "Asset status", Connector.FieldTypeIdEnum);
            assetStatus.AddListItems(1, new List<string> { "Active", "Deleted", "Partial open", "Retired" });

            var propertyType = fa00100.AddField("PROPTYPE", "Property type", Connector.FieldTypeIdEnum);
            propertyType.AddListItems(1, new List<string> { "Personal", "Personal, listed", "Real", "Real, listed", "Real, conservation", "Real, energy", "Real, farms", "Real, low income housing", "Amortizable" });
            
            var leaseType = fa00500.AddField("LEASETYPE", "Lease type (lease)", Connector.FieldTypeIdEnum);
            leaseType.AddListItems(1, new List<string> { "Capital", "Operating" });

        }

        private ConnectorEntity GetFixedAssetBookEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FixedAssetsSmartListAssetBooks), "Fixed asset books", ParentConnector);

            var fa00200 = entity.AddTable("FA00200");

            var fa00100 = entity.AddTable("FA00100", "FA00200", ConnectorTable.ConnectorTableJoinType.Inner);
            fa00100.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            var fa40200 = entity.AddTable("FA40200", "FA00200", ConnectorTable.ConnectorTableJoinType.Inner);
            fa40200.AddJoinFields("BOOKINDX", "BOOKINDX");

            AddFixedAssetBookFields(fa00200, fa00100, fa40200);

            return entity;
        }
        private static void AddFixedAssetBookFields(ConnectorTable fa00200, ConnectorTable fa00100, ConnectorTable fa40200)
        {
            fa00100.AddField("ASSETID", "Asset ID", Connector.FieldTypeIdString, true);
            fa00100.AddField("ASSETIDSUF", "Suffix", Connector.FieldTypeIdInteger, true);
            fa00100.AddField("ASSETDESC", "Asset description", Connector.FieldTypeIdString, true);
            fa40200.AddField("BOOKID", "Book ID", Connector.FieldTypeIdString, true);
            fa00200.AddField("PLINSERVdate", "Place in service date", Connector.FieldTypeIdDate, true);
            fa00100.AddField("ASSETindex", "Asset index", Connector.FieldTypeIdInteger);
            fa00100.AddField("ASSETCLASSID", "Asset Class ID", Connector.FieldTypeIdString);
            fa00100.AddField("STRUCTUREID", "Structure ID", Connector.FieldTypeIdString);
            fa00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            fa00100.AddField("Physical_Location_ID", "Physical Location ID", Connector.FieldTypeIdString);
            fa00100.AddField("Master_Asset_ID", "Master Asset ID", Connector.FieldTypeIdString);
            fa00200.AddField("BOOKINDX", "Book index", Connector.FieldTypeIdInteger);
            fa00200.AddField("DELETEdate", "Delete date", Connector.FieldTypeIdDate);
            fa00200.AddField("DEPRBEGdate", "Depreciation begin date", Connector.FieldTypeIdDate);
            fa00200.AddField("FULLYDEPRFLAG", "Fully depreciated flag", Connector.FieldTypeIdString);
            fa00200.AddField("FULLYDEPRdate", "Fully depreciated date", Connector.FieldTypeIdDate);
            fa00200.AddField("ORIGINALLIFEYEARS", "Original life years", Connector.FieldTypeIdInteger);
            fa00200.AddField("ORIGINALLIFEDAYS", "Original life days", Connector.FieldTypeIdInteger);
            fa00200.AddField("REMAININGLIFEYEARS", "Remaining life years", Connector.FieldTypeIdInteger);
            fa00200.AddField("REMAININGLIFEDAYS", "Remaining life days", Connector.FieldTypeIdInteger);
            fa00200.AddField("DEPRTOdate", "Depreciated to date", Connector.FieldTypeIdDate);
            fa00200.AddField("LASTRECALCdate", "Last recalc date", Connector.FieldTypeIdDate);
            fa00200.AddField("LASTRECALCDATEFISYR", "Last recalc date fiscal year", Connector.FieldTypeIdInteger);
            fa00200.AddField("BEGINYEARCOST", "Begin year cost", Connector.FieldTypeIdCurrency);
            fa00200.AddField("BAGINSALVAGE", "Begin salvage", Connector.FieldTypeIdCurrency);
            fa00200.AddField("BEGINRESERVE", "Begin reserve", Connector.FieldTypeIdCurrency);
            fa00200.AddField("COSTBASIS", "Cost basis", Connector.FieldTypeIdCurrency);
            fa00200.AddField("SALVAGEVALUE", "Salvage value", Connector.FieldTypeIdCurrency);
            fa00200.AddField("SWITCHFM1AMOUNT", "Switch from 1 amount", Connector.FieldTypeIdCurrency);
            fa00200.AddField("SWITCHFM1date", "Switch from 1 date", Connector.FieldTypeIdDate);
            fa00200.AddField("DLYDEPRRATE", "Daily depreciation rate", Connector.FieldTypeIdCurrency);
            fa00200.AddField("PERDEPRRATE", "Periodic depreciation rate", Connector.FieldTypeIdCurrency);
            fa00200.AddField("YRLYDEPRRATE", "Yearly depreciation rate", Connector.FieldTypeIdCurrency);
            fa00200.AddField("SAVEDLYDEPRRATE", "Save daily depreciation rate", Connector.FieldTypeIdCurrency);
            fa00200.AddField("SAVEPERDEPRRATE", "Save periodic depreciation rate", Connector.FieldTypeIdCurrency);
            fa00200.AddField("SAVEYRLYDEPRRATE", "Save yearly depreciation rate", Connector.FieldTypeIdCurrency);
            fa00200.AddField("AMORTIZATIONAMOUNT", "Amortization amount", Connector.FieldTypeIdCurrency);
            fa00200.AddField("CURRUNDEPRAMT", "Current run depreciation amount", Connector.FieldTypeIdCurrency);
            fa00200.AddField("PREVRUNDEPRAMT", "Previous run depreciation amount", Connector.FieldTypeIdCurrency);
            fa00200.AddField("YTDDEPRAMT", "Year to date depreciation amount", Connector.FieldTypeIdCurrency);
            fa00200.AddField("LTDDEPRAMT", "Life depreciation amount", Connector.FieldTypeIdCurrency);
            fa00200.AddField("NETBOOKVALUE", "Net book value", Connector.FieldTypeIdCurrency, true);
            fa00200.AddField("LUXAUTOIND - 1", "Luxury automobile indicator", Connector.FieldTypeIdYesNo);
            fa00200.AddField("PRORATEDRETdate", "Prorated retirement date", Connector.FieldTypeIdDate);
            fa00200.AddField("DEPRTODATEBFRET", "Depreciated to date before retire", Connector.FieldTypeIdDate);
            fa00200.AddField("RECGAINLOSS", "Recognized gain loss", Connector.FieldTypeIdCurrency);
            fa00200.AddField("NONRECGAINLOSS", "Non recognized gain loss", Connector.FieldTypeIdCurrency);
            fa00200.AddField("STLINEDEPRATRET", "Straight line depreciation at retirement", Connector.FieldTypeIdCurrency);
            fa00200.AddField("COSTBFRETORDEL", "Cost before retire or delete", Connector.FieldTypeIdCurrency);
            fa00200.AddField("Initial_Allowance_Perc", "Initial allowance percentage", Connector.FieldTypeIdPercentage);
            fa00200.AddField("Initial_Allowance_Amount", "Initial allowance amount", Connector.FieldTypeIdCurrency);
            fa00200.AddField("DATEADDED", "Date added", Connector.FieldTypeIdDate);
            fa00200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            fa00200.AddField("LASTMNTDdate", "Last maintained date", Connector.FieldTypeIdDate);
            fa00200.AddField("LASTMNTDTIME", "Last maintained time", Connector.FieldTypeIdTime);
            fa00200.AddField("LASTMNTDUSERID", "Last maintained user ID", Connector.FieldTypeIdString);
            fa00200.AddField("SPECDEPRALLOW", "Special depreciation allowance", Connector.FieldTypeIdYesNo);
            fa00200.AddField("SPECDEPRALLOWPCT", "Special depreciation allowance percentage", Connector.FieldTypeIdPercentage);
            fa00200.AddField("SPECDEPRALLOWAMT", "Special depreciation allowance amount", Connector.FieldTypeIdCurrency);

            var assetType = fa00100.AddField("ASSETTYPE", "Asset type", Connector.FieldTypeIdEnum); 
            assetType.AddListItems(1, new List<string> { "New", "Used", "Leased" });

            var depreciationMethod = fa00200.AddField("DEPRECIATIONMETHOD", "Depreciation method", Connector.FieldTypeIdEnum);
            depreciationMethod.AddListItems(1, new List<string> { "Straight-line orig life", "Straight-line rem life", "125% DB", "150% DB", "175% DB", "200% DB", "SOY Digits", "Remaining life", "Amortization", "ACRS personal", "ACRS real", "ACRS real MSL", "ACRS LIH", "ACRS foreign real", "No depreciation", "Declining balance" });
            
            var averagingConvention = fa00200.AddField("AVERAGINGCONV", "Averaging convention", Connector.FieldTypeIdEnum);
            averagingConvention.AddListItems(1, new List<string> { "Half-year", "Modified half-year", "Mid-month (1st)", "Mid-month (15th)", "Mid-quarter", "Next month", "Full month", "Next year", "Full year", "Full year all year", "None", "Next period", "Full period" });
            
            fa00200.AddField("SWITCHOVER", "Switchover", Connector.FieldTypeIdEnum); 
            assetType.AddListItems(1, new List<string> { "No Switch", "Straight-line" });
            
            fa00200.AddField("SWITCHFM1METHOD", "Switch from 1 method", Connector.FieldTypeIdEnum); 
            assetType.AddListItems(1, new List<string> { "Straight-line orig life", "Straight-line rem life", "125% DB", "150% DB", "175% DB", "200% DB", "SOY digits", "Remaining life", "Amortization", "ACRS personal", "ACRS real", "ACRS real MSL", "ACRS LIH", "ACRS foreign real", "No depreciation", "Declining balance" });
            
            fa00200.AddField("AMORTIZATIONCODE", "Amortization code", Connector.FieldTypeIdEnum);
            assetType.AddListItems(1, new List<string> { "Daily", "Weekly", "Monthly", "Quarterly", "Yearly", "Percentage", "Rate" });

        }

        private ConnectorEntity GetFixedAssetPurchaseEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(FixedAssetsSmartListAssetPurchase), "Fixed asset purchases", ParentConnector);

            var fa01400 = entity.AddTable("FA01400");

            var fa00100 = entity.AddTable("FA00100", "FA01400", ConnectorTable.ConnectorTableJoinType.Inner);
            fa00100.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            AddFixedAssetPurchaseFields(fa01400, fa00100);

            return entity;
        }
        private static void AddFixedAssetPurchaseFields(ConnectorTable fa01400, ConnectorTable fa00100)
        {
            fa00100.AddField("ASSETID", "Asset ID", Connector.FieldTypeIdString, true);
            fa00100.AddField("ASSETIDSUF", "Suffix", Connector.FieldTypeIdInteger, true);
            fa01400.AddField("PURCHDESC", "Description", Connector.FieldTypeIdString, true);
            fa01400.AddField("DOCdate", "Date", Connector.FieldTypeIdDate, true);
            fa01400.AddField("Acquisition_Cost", "Acquisition cost", Connector.FieldTypeIdCurrency, true);
            fa01400.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            fa00100.AddField("ASSETDESC", "Asset description", Connector.FieldTypeIdString);
            fa00100.AddField("ASSETCLASSID", "Asset class ID", Connector.FieldTypeIdString);
            fa00100.AddField("STRUCTUREID", "Structure ID", Connector.FieldTypeIdString);
            fa00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            fa00100.AddField("Physical_Location_ID", "Physical location ID", Connector.FieldTypeIdString);
            fa00100.AddField("Master_Asset_ID", "Master asset ID", Connector.FieldTypeIdString);
            fa00100.AddField("ACQdate", "Acquisition date", Connector.FieldTypeIdDate);
            fa00100.AddField("Acquisition_Cost", "Acquisition cost (general)", Connector.FieldTypeIdCurrency);
            fa00100.AddField("Asset_Label", "Asset label", Connector.FieldTypeIdString);
            fa00100.AddField("ASSETQTY", "Asset quantity", Connector.FieldTypeIdInteger);
            fa00100.AddField("EXTASSETDESC", "Extended description", Connector.FieldTypeIdString);
            fa00100.AddField("SHRTNAME", "Short name", Connector.FieldTypeIdString);
            fa00100.AddField("PIN", "PIN", Connector.FieldTypeIdString);
            fa00100.AddField("Verified_date", "Verified date", Connector.FieldTypeIdDate);
            fa00100.AddField("CUSTODIAN", "Custodian", Connector.FieldTypeIdString);
            fa00100.AddField("MFGRNAME", "Manufacturer name", Connector.FieldTypeIdString);
            fa00100.AddField("SERLNMBR", "Serial number", Connector.FieldTypeIdString);
            fa00100.AddField("MODELNUMBER", "Model number", Connector.FieldTypeIdString);
            fa00100.AddField("WARRENTYdate", "Warranty date", Connector.FieldTypeIdDate);
            fa00100.AddField("LASTMAINTdate", "Last maintenance date", Connector.FieldTypeIdDate);
            fa00100.AddField("ASSETCURRMAINT", "Current maintenance amount", Connector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETYTDMAINT", "Year to date maintenance amount", Connector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETLTDMAINT", "Life maintenance amount", Connector.FieldTypeIdCurrency);
            fa00100.AddField("DATEADDED", "Date added", Connector.FieldTypeIdDate);
            fa00100.AddField("LASTPURCHLINESEQ", "Last purchase line sequence", Connector.FieldTypeIdInteger);
            fa00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            fa01400.AddField("Orig_Acquisition_Cost", "Originating acquisition cost", Connector.FieldTypeIdCurrency);
            fa01400.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            fa01400.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            fa01400.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdCurrency);
            fa01400.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            fa01400.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            fa01400.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            fa01400.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            fa01400.AddField("RTCLCMTD", "Rate calculation method", Connector.FieldTypeIdString);
            fa01400.AddField("DENXRATE", "Denomination exchange rate", Connector.FieldTypeIdCurrency);
            fa01400.AddField("MCTRXSTT", "Multicurrency transaction State", Connector.FieldTypeIdInteger);
            fa01400.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            fa01400.AddField("DOCNUMBR", "Document number", Connector.FieldTypeIdString);
            fa01400.AddField("ORCTRNUM", "Voucher/receipt number", Connector.FieldTypeIdString);
            fa01400.AddField("PORDNMBR", "Purchase order number", Connector.FieldTypeIdString);
            fa01400.AddField("LASTMNTDdate", "Last maintained date", Connector.FieldTypeIdDate);
            fa01400.AddField("LASTMNTDTIME", "Last maintained time", Connector.FieldTypeIdTime);
            fa01400.AddField("LASTMNTDUSERID", "Last maintained user ID", Connector.FieldTypeIdString);
            fa01400.AddField("PURCHLINESEQ", "Purchase line sequence", Connector.FieldTypeIdInteger);
            fa01400.AddField("ASSETindex", "Asset index", Connector.FieldTypeIdInteger);
            fa01400.AddField("FA_AP_Post_index", "AP post index", Connector.FieldTypeIdInteger);

            var assetType = fa00100.AddField("ASSETTYPE", "Asset type", Connector.FieldTypeIdEnum);
            assetType.AddListItems(1, new List<string> { "New", "Used", "Leased" });

            var assetStatus = fa00100.AddField("ASSETSTATUS", "Asset status", Connector.FieldTypeIdEnum);
            assetStatus.AddListItems(1, new List<string> { "Active", "Deleted", "Partial open", "Retired" });

            var propertyType = fa00100.AddField("PROPTYPE", "Property type", Connector.FieldTypeIdEnum);
            propertyType.AddListItems(1, new List<string> { "Personal", "Personal, listed", "Real", "Real, listed", "Real, conservation", "Real, energy", "Real, farms", "Real, low income housing", "Amortizable" });

        }

    }
}
