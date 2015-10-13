using System.Collections.Generic;
using eOne.Common.DataConnectors;

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

        private DataConnectorEntity GetFixedAssetEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FixedAssetsSmartListAssets), "Fixed assets", ParentConnector);

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
        private static void AddFixedAssetFields(DataConnectorTable fa00100, DataConnectorTable fa00400, DataConnectorTable fa00600, DataConnectorTable fa00500, DataConnectorTable fa41600, DataConnectorTable fa19900, DataConnectorTable fa41200)
        {
            fa00100.AddField("ASSETID", "Asset ID", DataConnector.FieldTypeIdString, true);
            fa00100.AddField("ASSETIDSUF", "Suf", DataConnector.FieldTypeIdInteger, true);
            fa00100.AddField("ASSETDESC", "Asset Description", DataConnector.FieldTypeIdString, true);
            fa00100.AddField("ASSETCLASSID", "Asset Class ID", DataConnector.FieldTypeIdString, true);
            fa00100.AddField("ACQDATE", "Acquisition Date", DataConnector.FieldTypeIdDate, true);
            fa00100.AddField("Acquisition_Cost", "Acquisition Cost", DataConnector.FieldTypeIdCurrency, true);
            fa00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString, true);
            fa00100.AddField("ASSETQTY", "Asset Quantity", DataConnector.FieldTypeIdInteger);
            fa00100.AddField("ASSETBEGQTY", "Asset Begin Quantity", DataConnector.FieldTypeIdInteger);
            fa00100.AddField("EXTASSETDESC", "Extended Description", DataConnector.FieldTypeIdString);
            fa00100.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            fa00100.AddField("ASSESSEDVALUE", "Assessed Value", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("Master_Asset_ID", "Master Asset ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("Physical_Location_ID", "Physical Location ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("Asset_Label", "Asset Label", DataConnector.FieldTypeIdString);
            fa00100.AddField("PIN", "PIN", DataConnector.FieldTypeIdString);
            fa00100.AddField("Verified_Date", "Verified Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("STRUCTUREID", "Structure ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("CUSTODIAN", "Custodian", DataConnector.FieldTypeIdString);
            fa00100.AddField("MFGRNAME", "Manufacturer Name", DataConnector.FieldTypeIdString);
            fa00100.AddField("SERLNMBR", "Serial Number", DataConnector.FieldTypeIdString);
            fa00100.AddField("MODELNUMBER", "Model Number", DataConnector.FieldTypeIdString);
            fa00100.AddField("WARRENTYDATE", "Warranty Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("LASTMAINTDATE", "Last Maintenance Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("ASSETCURRMAINT", "Current Maintenance Amount", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETYTDMAINT", "YTD Maintenance Amount", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETLTDMAINT", "LTD Maintenance Amount", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("DATEADDED", "Date Added", DataConnector.FieldTypeIdDate);
            fa00100.AddField("DELETEDATE", "Delete Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("LASTPURCHLINESEQ", "Last Purchase Line Sequence", DataConnector.FieldTypeIdInteger);
            fa00100.AddField("LASTMNTDDATE", "Last Maintained Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("LASTMNTDTIME", "Last Maintained Time", DataConnector.FieldTypeIdTime);
            fa00100.AddField("LASTMNTDUSERID", "Last Maintained User ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("ASSETINDEX", "Asset Index", DataConnector.FieldTypeIdInteger);
            fa00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            fa00400.AddField("ASSETINDEX", "Asset Index (Account)", DataConnector.FieldTypeIdInteger);
            fa00400.AddField("DEPREXPACCTINDX", "Depr Expense Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("DEPRRESVACCTINDX", "Depr Reserve Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("PRIORYRDEPRACCTINDX", "Prior Year Depr Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("ASSETCOSTACCTINDX", "Asset Cost Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("PROCEEDSACCTINDX", "Proceeds Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("RECGAINLOSSACCTINDX", "Rec G/L Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("NONRECGAINLOSSACCTINDX", "Non Rec G/L Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("CLEARINGACCTINDX", "Clearing Acct Number (Account)", DynamicsGpConnector.FieldTypeIdAccountIndex);
            fa00400.AddField("NOTEINDX", "Note Index (Account)", DataConnector.FieldTypeIdInteger);
            fa00400.AddField("LASTMNTDDATE", "Last Maintained Date (Account)", DataConnector.FieldTypeIdDate);
            fa00400.AddField("LASTMNTDTIME", "Last Maintained Time (Account)", DataConnector.FieldTypeIdTime);
            fa00400.AddField("LASTMNTDUSERID", "Last Maintained User ID (Account)", DataConnector.FieldTypeIdString);
            fa00600.AddField("ASSETINDEX", "Asset Index (Insurance)", DataConnector.FieldTypeIdInteger);
            fa41200.AddField("INSCLASSID", "Insurance Class ID (Insurance)", DataConnector.FieldTypeIdString);
            fa00600.AddField("INSCLASSINDX", "Insurance Class Index (Insurance)", DataConnector.FieldTypeIdInteger);
            fa00600.AddField("INSURANCEYEAR", "Insurance Year (Insurance)", DataConnector.FieldTypeIdString);
            fa00600.AddField("INSURANCEVALUE", "Insurance Value (Insurance)", DataConnector.FieldTypeIdCurrency);
            fa00600.AddField("REPLACEMENTCOST", "Replacement Cost (Insurance)", DataConnector.FieldTypeIdCurrency);
            fa00600.AddField("REPROCOST", "Reproduction Cost (Insurance)", DataConnector.FieldTypeIdCurrency);
            fa00600.AddField("DEPRREPROCOST", "Depreciated Reproduction Cost (Insurance)", DataConnector.FieldTypeIdCurrency);
            fa00600.AddField("EXCLUSIONAMT", "Exclusion Amount (Insurance)", DataConnector.FieldTypeIdCurrency);
            fa00600.AddField("EXCLUSIONTYPE", "Exclusion Type (Insurance)", DataConnector.FieldTypeIdString);
            fa00600.AddField("NOTEINDX", "Note Index (Insurance)", DataConnector.FieldTypeIdInteger);
            fa00600.AddField("LASTMNTDDATE", "Last Maintained Date (Insurance)", DataConnector.FieldTypeIdDate);
            fa00600.AddField("LASTMNTDTIME", "Last Maintained Time (Insurance)", DataConnector.FieldTypeIdTime);
            fa00600.AddField("LASTMNTDUSERID", "Last Maintained User ID (Insurance)", DataConnector.FieldTypeIdString);
            fa00500.AddField("ASSETINDEX", "Asset Index (Lease)", DataConnector.FieldTypeIdInteger);
            fa41600.AddField("LEASECOMPID", "Lease Company ID (Lease)", DataConnector.FieldTypeIdString);
            fa00500.AddField("LEASECOMPINDX", "Lease Company Index (Lease)", DataConnector.FieldTypeIdInteger);
            fa00500.AddField("LEASECONTRACTID", "Lease Contract ID (Lease)", DataConnector.FieldTypeIdString);
            fa00500.AddField("LEASEPAYMENT", "Lease Payment (Lease)", DataConnector.FieldTypeIdCurrency);
            fa00500.AddField("LEASEINTRATE", "Lease Interest Rate (Lease)", DataConnector.FieldTypeIdInteger);
            fa00500.AddField("LEASEENDDATE", "Lease End Date (Lease)", DataConnector.FieldTypeIdDate);
            fa00500.AddField("NOTEINDX", "Note Index (Lease)", DataConnector.FieldTypeIdInteger);
            fa00500.AddField("LASTMNTDDATE", "Last Maintained Date (Lease)", DataConnector.FieldTypeIdDate);
            fa00500.AddField("LASTMNTDTIME", "Last Maintained Time (Lease)", DataConnector.FieldTypeIdTime);
            fa00500.AddField("LASTMNTDUSERID", "Last Maintained User ID (Lease)", DataConnector.FieldTypeIdString);
            fa19900.AddField("ASSETINDEX", "Asset Index (User Data)", DataConnector.FieldTypeIdInteger);
            fa19900.AddField("USRFIELD1", "User Field 1 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD2", "User Field 2 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD3", "User Field 3 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD4", "User Field 4 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD5", "User Field 5 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD6", "User Field 6 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD7", "User Field 7 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD8", "User Field 8 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD9", "User Field 9 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD10", "User Field 10 (User Data)", DataConnector.FieldTypeIdString);
            fa19900.AddField("USRFIELD11", "User Field 11 (User Data)", DataConnector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD12", "User Field 12 (User Data)", DataConnector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD13", "User Field 13 (User Data)", DataConnector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD14", "User Field 14 (User Data)", DataConnector.FieldTypeIdCurrency);
            fa19900.AddField("USRFIELD15", "User Field 15 (User Data)", DataConnector.FieldTypeIdCurrency);
            fa19900.AddField("NOTEINDX", "Note Index (User Data)", DataConnector.FieldTypeIdInteger);
            fa19900.AddField("LASTMNTDDATE", "Last Maintained Date (User Data)", DataConnector.FieldTypeIdDate);
            fa19900.AddField("LASTMNTDTIME", "Last Maintained Time (User Data)", DataConnector.FieldTypeIdTime);
            fa19900.AddField("LASTMNTDUSERID", "Last Maintained User ID (User Data)", DataConnector.FieldTypeIdString);

            var assetType = fa00100.AddField("ASSETTYPE", "Asset Type", DataConnector.FieldTypeIdEnum);
            assetType.AddListItems(1, new List<string> { "New", "Used", "Leased" });

            var assetStatus = fa00100.AddField("ASSETSTATUS", "Asset Status", DataConnector.FieldTypeIdEnum);
            assetStatus.AddListItems(1, new List<string> { "Active", "Deleted", "Partial Open", "Retired" });

            var propertyType = fa00100.AddField("PROPTYPE", "Property Type", DataConnector.FieldTypeIdEnum);
            propertyType.AddListItems(1, new List<string> { "Personal", "Personal, Listed", "Real", "Real, Listed", "Real, Conservation", "Real, Energy", "Real, Farms", "Real, Low Income Housing", "Amortizable" });
            
            var leaseType = fa00500.AddField("LEASETYPE", "Lease Type (Lease)", DataConnector.FieldTypeIdEnum);
            leaseType.AddListItems(1, new List<string> { "Capital", "Operating" });

        }

        private DataConnectorEntity GetFixedAssetBookEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FixedAssetsSmartListAssetBooks), "Fixed asset books", ParentConnector);

            var fa00200 = entity.AddTable("FA00200");

            var fa00100 = entity.AddTable("FA00100", "FA00200", DataConnectorTable.DataConnectorTableJoinType.Inner);
            fa00100.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            var fa40200 = entity.AddTable("FA40200", "FA00200", DataConnectorTable.DataConnectorTableJoinType.Inner);
            fa40200.AddJoinFields("BOOKINDX", "BOOKINDX");

            AddFixedAssetBookFields(fa00200, fa00100, fa40200);

            return entity;
        }
        private static void AddFixedAssetBookFields(DataConnectorTable fa00200, DataConnectorTable fa00100, DataConnectorTable fa40200)
        {
            fa00100.AddField("ASSETID", "Asset ID", DataConnector.FieldTypeIdString, true);
            fa00100.AddField("ASSETIDSUF", "Suf", DataConnector.FieldTypeIdInteger, true);
            fa00100.AddField("ASSETDESC", "Asset Description", DataConnector.FieldTypeIdString, true);
            fa40200.AddField("BOOKID", "Book ID", DataConnector.FieldTypeIdString, true);
            fa00200.AddField("PLINSERVDATE", "Place in Service Date", DataConnector.FieldTypeIdDate, true);
            fa00100.AddField("ASSETINDEX", "Asset Index", DataConnector.FieldTypeIdInteger);
            fa00100.AddField("ASSETCLASSID", "Asset Class ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("STRUCTUREID", "Structure ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("Physical_Location_ID", "Physical Location ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("Master_Asset_ID", "Master Asset ID", DataConnector.FieldTypeIdString);
            fa00200.AddField("BOOKINDX", "Book Index", DataConnector.FieldTypeIdInteger);
            fa00200.AddField("DELETEDATE", "Delete Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("DEPRBEGDATE", "Depreciation Begin Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("FULLYDEPRFLAG", "Fully Depreciated Flag", DataConnector.FieldTypeIdString);
            fa00200.AddField("FULLYDEPRDATE", "Fully Depreciated Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("ORIGINALLIFEYEARS", "Original Life Years", DataConnector.FieldTypeIdInteger);
            fa00200.AddField("ORIGINALLIFEDAYS", "Original Life Days", DataConnector.FieldTypeIdInteger);
            fa00200.AddField("REMAININGLIFEYEARS", "Remaining Life Years", DataConnector.FieldTypeIdInteger);
            fa00200.AddField("REMAININGLIFEDAYS", "Remaining Life Days", DataConnector.FieldTypeIdInteger);
            fa00200.AddField("DEPRTODATE", "Depreciated to Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("LASTRECALCDATE", "Last Recalc Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("LASTRECALCDATEFISYR", "Last Recalc Date Fiscal Year", DataConnector.FieldTypeIdInteger);
            fa00200.AddField("BEGINYEARCOST", "Begin Year Cost", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("BAGINSALVAGE", "Begin Salvage", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("BEGINRESERVE", "Begin Reserve", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("COSTBASIS", "Cost Basis", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("SALVAGEVALUE", "Salvage Value", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("SWITCHFM1AMOUNT", "Switch From 1 Amount", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("SWITCHFM1DATE", "Switch From 1 Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("DLYDEPRRATE", "Daily Depreciation Rate", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("PERDEPRRATE", "Periodic Depreciation Rate", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("YRLYDEPRRATE", "Yearly Depreciation Rate", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("SAVEDLYDEPRRATE", "Save Daily Depreciation Rate", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("SAVEPERDEPRRATE", "Save Periodic Depreciation Rate", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("SAVEYRLYDEPRRATE", "Save Yearly Depreciation Rate", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("AMORTIZATIONAMOUNT", "Amortization Amount", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("CURRUNDEPRAMT", "Current Run Depreciation Amount", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("PREVRUNDEPRAMT", "Previous Run Depreciation Amount", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("YTDDEPRAMT", "YTD Depreciation Amount", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("LTDDEPRAMT", "LTD Depreciation Amount", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("NETBOOKVALUE", "Net Book Value", DataConnector.FieldTypeIdCurrency, true);
            fa00200.AddField("LUXAUTOIND - 1", "Luxury Automobile Indicator", DataConnector.FieldTypeIdYesNo);
            fa00200.AddField("PRORATEDRETDATE", "Prorated Retirement Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("DEPRTODATEBFRET", "Depreciated to Date Before Retire", DataConnector.FieldTypeIdDate);
            fa00200.AddField("RECGAINLOSS", "Recognized Gain Loss", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("NONRECGAINLOSS", "Non Recognized Gain Loss", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("STLINEDEPRATRET", "Straight Line Depr at Retirement", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("COSTBFRETORDEL", "Cost Before Retire or Delete", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("Initial_Allowance_Perc", "Initial Allowance Percentage", DataConnector.FieldTypeIdPercentage);
            fa00200.AddField("Initial_Allowance_Amount", "Initial Allowance Amount", DataConnector.FieldTypeIdCurrency);
            fa00200.AddField("DATEADDED", "Date Added", DataConnector.FieldTypeIdDate);
            fa00200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            fa00200.AddField("LASTMNTDDATE", "Last Maintained Date", DataConnector.FieldTypeIdDate);
            fa00200.AddField("LASTMNTDTIME", "Last Maintained Time", DataConnector.FieldTypeIdTime);
            fa00200.AddField("LASTMNTDUSERID", "Last Maintained User ID", DataConnector.FieldTypeIdString);
            fa00200.AddField("SPECDEPRALLOW", "Special Depreciation Allowance", DataConnector.FieldTypeIdYesNo);
            fa00200.AddField("SPECDEPRALLOWPCT", "Special Depreciation Allowance Percentage", DataConnector.FieldTypeIdPercentage);
            fa00200.AddField("SPECDEPRALLOWAMT", "Special Depreciation Allowance Amount", DataConnector.FieldTypeIdCurrency);

            var assetType = fa00100.AddField("ASSETTYPE", "Asset Type", DataConnector.FieldTypeIdEnum); 
            assetType.AddListItems(1, new List<string> { "New", "Used", "Leased" });

            var depreciationMethod = fa00200.AddField("DEPRECIATIONMETHOD", "Depreciation Method", DataConnector.FieldTypeIdEnum);
            depreciationMethod.AddListItems(1, new List<string> { "Straight-Line Orig Life", "Straight-Line Rem Life", "125% DB", "150% DB", "175% DB", "200% DB", "SOY Digits", "Remaining Life", "Amortization", "ACRS Personal", "ACRS Real", "ACRS Real MSL", "ACRS LIH", "ACRS Foreign Real", "No Depreciation", "Declining Balance" });
            
            var averagingConvention = fa00200.AddField("AVERAGINGCONV", "Averaging Convention", DataConnector.FieldTypeIdEnum);
            averagingConvention.AddListItems(1, new List<string> { "Half-Year", "Modified Half-Year", "Mid-Month (1st", "Mid-Month (15th", "Mid-Quarter", "Next Month", "Full Month", "Next Year", "Full Year", "Full Year All Year", "None", "Next Period", "Full Period" });
            
            fa00200.AddField("SWITCHOVER", "Switchover", DataConnector.FieldTypeIdEnum); 
            assetType.AddListItems(1, new List<string> { "No Switch", "Straight-Line" });
            
            fa00200.AddField("SWITCHFM1METHOD", "Switch From 1 Method", DataConnector.FieldTypeIdEnum); 
            assetType.AddListItems(1, new List<string> { "Straight-Line Orig Life", "Straight-Line Rem Life", "125% DB", "150% DB", "175% DB", "200% DB", "SOY Digits", "Remaining Life", "Amortization", "ACRS Personal", "ACRS Real", "ACRS Real MSL", "ACRS LIH", "ACRS Foreign Real", "No Depreciation", "Declining Balance" });
            
            fa00200.AddField("AMORTIZATIONCODE", "Amortization Code", DataConnector.FieldTypeIdEnum);
            assetType.AddListItems(1, new List<string> { "Daily", "Weekly", "Monthly", "Quarterly", "Yearly", "Percentage", "Rate" });

        }

        private DataConnectorEntity GetFixedAssetPurchaseEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(FixedAssetsSmartListAssetPurchase), "Fixed asset purchases", ParentConnector);

            var fa01400 = entity.AddTable("FA01400");

            var fa00100 = entity.AddTable("FA00100", "FA01400", DataConnectorTable.DataConnectorTableJoinType.Inner);
            fa00100.AddJoinFields("ASSETINDEX", "ASSETINDEX");

            AddFixedAssetPurchaseFields(fa01400, fa00100);

            return entity;
        }
        private static void AddFixedAssetPurchaseFields(DataConnectorTable fa01400, DataConnectorTable fa00100)
        {
            fa00100.AddField("ASSETID", "Asset ID", DataConnector.FieldTypeIdString, true);
            fa00100.AddField("ASSETIDSUF", "Suf", DataConnector.FieldTypeIdInteger, true);
            fa01400.AddField("PURCHDESC", "Description", DataConnector.FieldTypeIdString, true);
            fa01400.AddField("DOCDATE", "Date", DataConnector.FieldTypeIdDate, true);
            fa01400.AddField("Acquisition_Cost", "Acquisition Cost", DataConnector.FieldTypeIdCurrency, true);
            fa01400.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            fa00100.AddField("ASSETDESC", "Asset Description", DataConnector.FieldTypeIdString);
            fa00100.AddField("ASSETCLASSID", "Asset Class ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("STRUCTUREID", "Structure ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("Physical_Location_ID", "Physical Location ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("Master_Asset_ID", "Master Asset ID", DataConnector.FieldTypeIdString);
            fa00100.AddField("ACQDATE", "Acquisition Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("Acquisition_Cost", "Acquisition Cost (General", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("Asset_Label", "Asset Label", DataConnector.FieldTypeIdString);
            fa00100.AddField("ASSETQTY", "Asset Quantity", DataConnector.FieldTypeIdInteger);
            fa00100.AddField("EXTASSETDESC", "Extended Description", DataConnector.FieldTypeIdString);
            fa00100.AddField("SHRTNAME", "Short Name", DataConnector.FieldTypeIdString);
            fa00100.AddField("PIN", "PIN", DataConnector.FieldTypeIdString);
            fa00100.AddField("Verified_Date", "Verified Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("CUSTODIAN", "Custodian", DataConnector.FieldTypeIdString);
            fa00100.AddField("MFGRNAME", "Manufacturer Name", DataConnector.FieldTypeIdString);
            fa00100.AddField("SERLNMBR", "Serial Number", DataConnector.FieldTypeIdString);
            fa00100.AddField("MODELNUMBER", "Model Number", DataConnector.FieldTypeIdString);
            fa00100.AddField("WARRENTYDATE", "Warranty Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("LASTMAINTDATE", "Last Maintenance Date", DataConnector.FieldTypeIdDate);
            fa00100.AddField("ASSETCURRMAINT", "Current Maintenance Amount", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETYTDMAINT", "YTD Maintenance Amount", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("ASSETLTDMAINT", "LTD Maintenance Amount", DataConnector.FieldTypeIdCurrency);
            fa00100.AddField("DATEADDED", "Date Added", DataConnector.FieldTypeIdDate);
            fa00100.AddField("LASTPURCHLINESEQ", "Last Purchase Line Sequence", DataConnector.FieldTypeIdInteger);
            fa00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            fa01400.AddField("Orig_Acquisition_Cost", "Originating Acquisition Cost", DataConnector.FieldTypeIdCurrency);
            fa01400.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            fa01400.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            fa01400.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdCurrency);
            fa01400.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            fa01400.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            fa01400.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            fa01400.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            fa01400.AddField("RTCLCMTD", "Rate Calculation Method", DataConnector.FieldTypeIdString);
            fa01400.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdCurrency);
            fa01400.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdInteger);
            fa01400.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            fa01400.AddField("DOCNUMBR", "Document Number", DataConnector.FieldTypeIdString);
            fa01400.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            fa01400.AddField("ORCTRNUM", "Voucher / Receipt Number", DataConnector.FieldTypeIdString);
            fa01400.AddField("PORDNMBR", "Purchase Order Number", DataConnector.FieldTypeIdString);
            fa01400.AddField("LASTMNTDDATE", "Last Maintained Date", DataConnector.FieldTypeIdDate);
            fa01400.AddField("LASTMNTDTIME", "Last Maintained Time", DataConnector.FieldTypeIdTime);
            fa01400.AddField("LASTMNTDUSERID", "Last Maintained User ID", DataConnector.FieldTypeIdString);
            fa01400.AddField("PURCHLINESEQ", "Purchase Line Sequence", DataConnector.FieldTypeIdInteger);
            fa01400.AddField("ASSETINDEX", "Asset Index", DataConnector.FieldTypeIdInteger);
            fa01400.AddField("FA_AP_Post_Index", "AP Post Index", DataConnector.FieldTypeIdInteger);

            var assetType = fa00100.AddField("ASSETTYPE", "Asset Type", DataConnector.FieldTypeIdEnum);
            assetType.AddListItems(1, new List<string> { "New", "Used", "Leased" });

            var assetStatus = fa00100.AddField("ASSETSTATUS", "Asset Status", DataConnector.FieldTypeIdEnum);
            assetStatus.AddListItems(1, new List<string> { "Active", "Deleted", "Partial Open", "Retired" });

            var propertyType = fa00100.AddField("PROPTYPE", "Property Type", DataConnector.FieldTypeIdEnum);
            propertyType.AddListItems(1, new List<string> { "Personal", "Personal, Listed", "Real", "Real, Listed", "Real, Conservation", "Real, Energy", "Real, Farms", "Real, Low Income Housing", "Amortizable" });

        }

    }
}
