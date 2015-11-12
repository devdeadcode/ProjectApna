using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class DynamicsGpInventoryModule : DynamicsGpModule
    {

        private const short GpSmartListItems = 4;
        private const short GpSmartListItemQuantities = 11;
        private const short GpSmartListInventoryTrx = 19;
        private const short GpSmartListVendorItems = 23;
        private const short GpSmartListCustomerItems = 24;
        private const short GpSmartListLandedCosts = 30;
        private const short GpSmartListLandedCostGroups = 31;
        private const short GpSmartListInventoryPurchaseReceipts = 36;
        
        public DynamicsGpInventoryModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 4;
            Name = "Inventory";
            Installed = true;
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetItemEntity());
            Entities.Add(GetItemQuantityEntity());
            Entities.Add(GetInventoryTransactionEntity());
            Entities.Add(GetLandedCostEntity());
            Entities.Add(GetLandedCostGroupEntity());
        }

        public ConnectorEntity GetItemEntity()
        {
            var entity = new ConnectorEntity(GpSmartListItems, "Items", ParentConnector);

            var iv00101 = entity.AddTable("IV00101");

            var iv00115 = entity.AddTable("IV00115", "IV00101");
            iv00115.AddJoinFields("ITEMNMBR", "ITEMNMBR");

            AddItemEntityFields(iv00101, iv00115);

            entity.AddCalculation("convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item shipping weight", Connector.FieldTypeIdQuantity);
            entity.AddCalculation("IV00101.DECPLQTY - 1", "Decimal places quantities", Connector.FieldTypeIdInteger);
            entity.AddCalculation("IV00101.DECPLCUR - 1", "Decimal places currency", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddItemEntityFields(ConnectorTable iv00101, ConnectorTable iv00115)
        {
            var itemNumber = iv00101.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            itemNumber.KeyNumber = 1;

            iv00101.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            var itemType = iv00101.AddField("ITEMTYPE", "Item type", Connector.FieldTypeIdEnum, true);
            itemType.AddListItems(1, new List<string> { "Sales inventory", "Discontinued", "Kit", "Misc charges", "Services", "Flat fee" });
            iv00101.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            iv00101.AddField("ITMSHNAM", "Item short name", Connector.FieldTypeIdString);
            iv00101.AddField("ITMGEDSC", "Item generic description", Connector.FieldTypeIdString);
            iv00101.AddField("STNDCOST", "Standard cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("CURRCOST", "Current cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("ITMTSHID", "Item tax schedule ID", Connector.FieldTypeIdString);
            
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

            var modifiedDate = iv00101.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            modifiedDate.ModifyDate = true;

            var createDate = iv00101.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            createDate.CreateDate = true;

            iv00101.AddField("WRNTYDYS", "Warranty days", Connector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            iv00101.AddField("CGSINFLX", "COGS inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("CGSMCIDX", "COGS monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINFIDX", "Inventory inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("INVMCIDX", "Inventory monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITEMCODE", "Item code", Connector.FieldTypeIdString);
            iv00101.AddField("LASTGENSN", "Last generated serial number", Connector.FieldTypeIdString);
            iv00101.AddField("PriceGroup", "Price group", Connector.FieldTypeIdString);
            iv00101.AddField("PINFLIDX", "Purch inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURMCIDX", "Purch monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PRCHSUOM", "Purchasing U of M", Connector.FieldTypeIdString);
            iv00101.AddField("SELNGUOM", "Selling U of M", Connector.FieldTypeIdString);
            iv00101.AddField("TCC", "Tax commodity code", Connector.FieldTypeIdString);
            iv00101.AddField("UPPVIDX", "Unrealized purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString);
            iv00101.AddField("Revalue_Inventory", "Revalue inventory", Connector.FieldTypeIdYesNo);
            iv00101.AddField("Tolerance_Percentage", "Tolerance percentage", Connector.FieldTypeIdPercentage);
            iv00101.AddField("Purchase_Item_Tax_Schedu", "Purchase item tax schedule ID", Connector.FieldTypeIdString);
            iv00101.AddField("CNTRYORGN", "Country origin", Connector.FieldTypeIdString);
            iv00115.AddField("MANUFACTURER", "Manufacturer", Connector.FieldTypeIdString);
            iv00115.AddField("MNFCTRITMNMBR", "Manufacturer's item number", Connector.FieldTypeIdString);
            iv00115.AddField("ITEMDESC", "Manufacturer's item description", Connector.FieldTypeIdString);
            iv00115.AddField("PRIMARYITEM", "Primary manfacturing number", Connector.FieldTypeIdString);

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

            var purchaseTaxOptions = iv00101.AddField("Purchase_Tax_Options", "Purchase tax options", Connector.FieldTypeIdEnum);
            purchaseTaxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
        }

        public ConnectorEntity GetItemQuantityEntity()
        {
            var entity = new ConnectorEntity(GpSmartListItemQuantities, "Item quantities", ParentConnector);

            var iv00102 = entity.AddTable("IV00102");

            var iv00101 = entity.AddTable("IV00101", "IV00102", ConnectorTable.ConnectorTableJoinType.Inner);
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");

            var iv00103 = entity.AddTable("IV00103", "IV00102", ConnectorTable.ConnectorTableJoinType.Inner);
            iv00103.AddJoinFields("ITEMNMBR", "ITEMNMBR");
            iv00103.AddJoinFields("VENDORID", "PRIMVNDR");

            var pm00200 = entity.AddTable("PM00200", "IV00103", ConnectorTable.ConnectorTableJoinType.Inner);
            pm00200.AddJoinFields("VENDORID", "VENDORID");

            var iv40201 = entity.AddTable("IV40201", "IV00101", ConnectorTable.ConnectorTableJoinType.Inner);
            iv40201.AddJoinFields("UOMSCHDL", "UOMSCHDL");

            AddItemQuantityEntityFields(iv00102, iv00101, iv00103, pm00200, iv40201);

            entity.AddCalculation("case IV00102.RCRDTYPE when 1 then 'All' else IV00102.LOCNCODE end", "Location code", Connector.FieldTypeIdString, true);
            entity.AddCalculation("QTYONHND - IV00102.ATYALLOC", "Quantity available", Connector.FieldTypeIdQuantity, true);
            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item shipping weight", Connector.FieldTypeIdQuantity);
            entity.AddCalculation("IV40201.UMDPQTYS - 1", "U of M decimal places QTYS", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddItemQuantityEntityFields(ConnectorTable iv00102, ConnectorTable iv00101, ConnectorTable iv00103, ConnectorTable pm00200, ConnectorTable iv40201)
        {
            iv00102.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            iv00101.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            var recordType = iv00102.AddField("RCRDTYPE", "Record type", Connector.FieldTypeIdEnum, true);
            recordType.AddListItems(1, new List<string> { "Overall", "Site" });
            iv00102.AddField("QTYONORD", "Quantity on order", Connector.FieldTypeIdQuantity, true);
            iv00102.AddField("QTYONHND", "Quantity on hand", Connector.FieldTypeIdQuantity, true);
            iv00102.AddField("ATYALLOC", "Quantity allocated", Connector.FieldTypeIdQuantity, true);
            iv00102.AddField("BINNMBR", "Bin number", Connector.FieldTypeIdString);
            iv00102.AddField("PRIMVNDR", "Primary vendor", Connector.FieldTypeIdString);
            iv00102.AddField("ITMFRFLG", "Item freeze flag", Connector.FieldTypeIdYesNo);
            iv00102.AddField("BGNGQTY", "Beginning quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("LSORDQTY", "Last order quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("LRCPTQTY", "Last receipt quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("LSTORDDT", "Last order date", Connector.FieldTypeIdDate);
            iv00102.AddField("LSORDVND", "Last order vendor", Connector.FieldTypeIdString);
            iv00102.AddField("LSRCPTDT", "Last receipt date", Connector.FieldTypeIdDate);
            iv00102.AddField("QTYRQSTN", "Quantity requisitioned", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTYBKORD", "Quantity back ordered", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTY_Drop_Shipped", "Quantity drop shipped", Connector.FieldTypeIdQuantity);
            iv00102.AddField("ORDRPNTQTY", "Order point quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("REORDERVARIANCE", "Reorder variance", Connector.FieldTypeIdQuantity);
            iv00102.AddField("ORDRUPTOLVL", "Order up to level", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTYINUSE", "Quantity in use", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTYINSVC", "Quantity in service", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTYRTRND", "Quantity returned", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTYDMGED", "Quantity damaged", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTYCOMTD", "Quantity committed", Connector.FieldTypeIdQuantity);
            iv00102.AddField("QTYSOLD", "Quantity sold", Connector.FieldTypeIdQuantity);
            iv00101.AddField("ITMGEDSC", "Item generic description", Connector.FieldTypeIdString);
            iv00101.AddField("ITMSHNAM", "Item short name", Connector.FieldTypeIdString);
            pm00200.AddField("VENDNAME", "Vendor name", Connector.FieldTypeIdString);
            iv00103.AddField("VNDITNUM", "Vendor item number", Connector.FieldTypeIdString);
            iv00103.AddField("VNDITDSC", "Vendor item description", Connector.FieldTypeIdString);
            iv00101.AddField("CURRCOST", "Current cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("STNDCOST", "Standard cost", Connector.FieldTypeIdCurrency);
            iv40201.AddField("IV40201.BASEUOFM", "Base U of M", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_1", "User category value 1", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_2", "User category value 2", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_3", "User category value 3", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_4", "User category value 4", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_5", "User category value 5", Connector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_6", "User category value 6", Connector.FieldTypeIdString);
            iv00101.AddField("ALTITEM1", "Alternate item 1", Connector.FieldTypeIdString);
            iv00101.AddField("ALTITEM2", "Alternate item 2", Connector.FieldTypeIdString);
            iv00101.AddField("ITMTSHID", "Item tax schedule ID", Connector.FieldTypeIdString);
            iv00101.AddField("KPERHIST", "Keep period history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPTRXHST", "Keep transaction history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPCALHST", "Keep calendar history", Connector.FieldTypeIdYesNo);
            iv00101.AddField("KPDSTHST", "Keep distribution history", Connector.FieldTypeIdYesNo);
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
            iv00101.AddField("ALWBKORD", "Allow back orders", Connector.FieldTypeIdYesNo);
            iv00101.AddField("UOMSCHDL", "U of M schedule", Connector.FieldTypeIdString);
            iv00101.AddField("MSTRCDTY", "Master record type", Connector.FieldTypeIdInteger);
            iv00101.AddField("WRNTYDYS", "Warranty days", Connector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            iv00102.AddField("LSTCNTDT", "Last count date", Connector.FieldTypeIdDate);
            iv00102.AddField("LSTCNTTM", "Last count time", Connector.FieldTypeIdTime);
            iv00102.AddField("NXTCNTDT", "Next count date", Connector.FieldTypeIdDate);
            iv00102.AddField("NXTCNTTM", "Next count time", Connector.FieldTypeIdTime);
            iv00102.AddField("STCKCNTINTRVL", "Stock count interval", Connector.FieldTypeIdInteger);
            iv40201.AddField("IV40201.UMSCHDSC", "U of M schedule description", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor check name", Connector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor short name", Connector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor address code - primary", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor address code - purchase address", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor address code - ship from", Connector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor address code - remit to", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCLSID", "Vendor class ID", Connector.FieldTypeIdString);
            pm00200.AddField("VNDCNTCT", "Vendor contact", Connector.FieldTypeIdString);
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
            pm00200.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account number with vendor", Connector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID number", Connector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax registration number", Connector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent vendor ID", Connector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade discount", Connector.FieldTypeIdPercentage);
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
            pm00200.AddField("KPCALHST", "Keep calendar history from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL distribution history from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep period history from vendor master", Connector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep transaction history from vendor master", Connector.FieldTypeIdYesNo);
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
            pm00200.AddField("PURPVIDX", "Purchase price variance account number from vendor master", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pm00200.AddField("Revalue_Vendor", "Revalue vendor", Connector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document format ID", Connector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due date grace period", Connector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental corporate ID", Connector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental individual ID", Connector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax invoice received", Connector.FieldTypeIdYesNo);
            iv00103.AddField("Last_Currency_ID", "Last currency ID", Connector.FieldTypeIdString);
            iv00103.AddField("LSTORDDT", "Last order date from vendor item", Connector.FieldTypeIdDate);
            iv00103.AddField("LSORDQTY", "Last order quantity from vendor item", Connector.FieldTypeIdQuantity);
            iv00103.AddField("Last_Originating_Cost", "Last originating cost", Connector.FieldTypeIdCurrency);
            iv00103.AddField("LRCPTCST", "Last receipt cost", Connector.FieldTypeIdCurrency);
            iv00103.AddField("LRCPTQTY", "Last receipt quantity from vendor item", Connector.FieldTypeIdQuantity);
            iv00103.AddField("MAXORDQTY", "Maximum order quantity", Connector.FieldTypeIdQuantity);
            iv00103.AddField("MINORQTY", "Minimum order quantity", Connector.FieldTypeIdQuantity);
            iv00103.AddField("NORCTITM", "Number of receipts for item", Connector.FieldTypeIdInteger);
            iv00103.AddField("PLANNINGLEADTIME", "Planning lead time", Connector.FieldTypeIdInteger);
            iv00103.AddField("QTY_Drop_Shipped", "Quantity drop shipped from vendor item", Connector.FieldTypeIdQuantity);
            iv00103.AddField("QTYONORD", "Quantity on order from vendor item", Connector.FieldTypeIdQuantity);
            iv00103.AddField("QTYRQSTN", "Quantity requisitioned from vendor item", Connector.FieldTypeIdQuantity);
            iv00103.AddField("LSRCPTDT", "Last receipt date from vendor item", Connector.FieldTypeIdDate);
            iv00103.AddField("AVRGLDTM", "Average lead time", Connector.FieldTypeIdInteger);
            iv00103.AddField("PRCHSUOM", "Purchasing U of M from vendor item", Connector.FieldTypeIdString);
            iv00103.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            iv00103.AddField("ECORDQTY", "Economic order quantity", Connector.FieldTypeIdQuantity);
            iv00103.AddField("ITMVNDTY", "Item vendor type", Connector.FieldTypeIdInteger);
            iv00101.AddField("CGSINFLX", "COGS inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("CGSMCIDX", "COGS monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINFIDX", "Inventory inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("INVMCIDX", "Inventory monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITEMCODE", "Item code", Connector.FieldTypeIdString);
            iv00101.AddField("LASTGENSN", "Last generated serial number", Connector.FieldTypeIdString);
            iv00101.AddField("PriceGroup", "Price group", Connector.FieldTypeIdString);
            iv00101.AddField("PINFLIDX", "Purch inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURMCIDX", "Purch monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PRCHSUOM", "Purchasing U of M", Connector.FieldTypeIdString);
            iv00101.AddField("SELNGUOM", "Selling U of M", Connector.FieldTypeIdString);
            iv00101.AddField("TCC", "Tax commodity code", Connector.FieldTypeIdString);
            iv00101.AddField("UPPVIDX", "Unrealized purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("LOCNCODE", "Location code from item master", Connector.FieldTypeIdString);
            iv00102.AddField("Landed_Cost_Group_ID", "Landed cost group ID", Connector.FieldTypeIdString);
            iv00102.AddField("PORETRNBIN", "Purchase returns bin", Connector.FieldTypeIdString);
            iv00102.AddField("SOFULFILLMENTBIN", "Sales order fulfillment bin", Connector.FieldTypeIdString);
            iv00102.AddField("SORETURNBIN", "Sales order return bin", Connector.FieldTypeIdString);
            iv00102.AddField("BOMRCPTBIN", "Bill of materials receipt bin", Connector.FieldTypeIdString);
            iv00102.AddField("MATERIALISSUEBIN", "Material issues bin", Connector.FieldTypeIdString);
            iv00102.AddField("MORECEIPTBIN", "Manufaturing order receipt bin", Connector.FieldTypeIdString);
            iv00102.AddField("REPAIRISSUESBIN", "Repair issues bin", Connector.FieldTypeIdString);
            iv00102.AddField("BUYERID", "Buyer ID", Connector.FieldTypeIdString);
            iv00102.AddField("PLANNERID", "Planner ID", Connector.FieldTypeIdString);
            iv00102.AddField("FXDORDRQTY", "Fixed order quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("NMBROFDYS", "Number of days", Connector.FieldTypeIdInteger);
            iv00102.AddField("MNMMORDRQTY", "Minimum order quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("MXMMORDRQTY", "Maximum order quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("ORDERMULTIPLE", "Order multiple", Connector.FieldTypeIdQuantity);
            iv00102.AddField("SHRINKAGEFACTOR", "Shrinkage factor", Connector.FieldTypeIdQuantity);
            iv00102.AddField("PRCHSNGLDTM", "Purchasing lead time", Connector.FieldTypeIdQuantity);
            iv00102.AddField("MNFCTRNGFXDLDTM", "Manufacturing fixed lead time", Connector.FieldTypeIdQuantity);
            iv00102.AddField("MNFCTRNGVRBLLDTM", "Manufacturing variable lead time", Connector.FieldTypeIdQuantity);
            iv00102.AddField("STAGINGLDTME", "Staging lead time", Connector.FieldTypeIdQuantity);
            iv00102.AddField("PLNNNGTMFNCDYS", "Planning time fence days", Connector.FieldTypeIdInteger);
            iv00102.AddField("DMNDTMFNCPRDS", "Demand time fence periods", Connector.FieldTypeIdInteger);
            iv00102.AddField("INCLDDINPLNNNG", "Included in planning", Connector.FieldTypeIdYesNo);
            iv00102.AddField("SFTYSTCKQTY", "Safety stock quantity", Connector.FieldTypeIdQuantity);
            iv00102.AddField("PORECEIPTBIN", "Purchase receipt bin", Connector.FieldTypeIdString);
            iv00102.AddField("MasterLocationCode", "Master location code", Connector.FieldTypeIdString);
            iv00102.AddField("PurchasePrice", "Specified cost", Connector.FieldTypeIdCurrency);
            iv00102.AddField("IncludeAllocations", "Include allocations", Connector.FieldTypeIdYesNo);
            iv00102.AddField("IncludeBackorders", "Include backorders", Connector.FieldTypeIdYesNo);
            iv00102.AddField("IncludeRequisitions", "Include requisitions", Connector.FieldTypeIdYesNo);

            var valuationMethod = iv00101.AddField("VCTNMTHD", "Valuation method", Connector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO perpetual", "LIFO perpetual", "Average perpetual", "FIFO periodic", "LIFO periodic" });

            var itemType = iv00101.AddField("ITEMTYPE", "Item type", Connector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales inventory", "Discontinued", "Kit", "Misc charges", "Services", "Flat fee" });

            var itemTrackingOption = iv00101.AddField("ITMTRKOP", "Item tracking option", Connector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });

            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax options", Connector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor status", Connector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });

            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 type", Connector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 vendor", "Dividend", "Interest", "Miscellaneous" });

            var minPaymentType = pm00200.AddField("MINPYTYP", "Minimum payment type", Connector.FieldTypeIdEnum);
            minPaymentType.AddListItems(0, new List<string> { "No minimum", "Percent", "Amount" });

            var maximumInvoiceAmountForVendors = pm00200.AddField("MXIAFVND", "Maximum invoice amount for vendors", Connector.FieldTypeIdEnum);
            maximumInvoiceAmountForVendors.AddListItems(0, new List<string> { "No maximum", "Amount" });

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

            var freeOnBoardFromVendorItem = iv00103.AddField("FREEONBOARD", "Free on board from vendor item", Connector.FieldTypeIdEnum);
            freeOnBoardFromVendorItem.AddListItems(1, new List<string> { "None", "Origin", "Destination" });

            var abcCode = iv00101.AddField("ABCCODE", "ABC code", Connector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });

            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS account source", Connector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From component item", "From kit item" });

            var priceMethod = iv00101.AddField("PRICMTHD", "Price method", Connector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency amount", "Percentage of list price", "Percentage markup - current cost", "Percentage markup - standard cost", "Percentage margin - current cost", "Percentage margin - standard cost" });

            var orderPolicy = iv00102.AddField("ORDERPOLICY", "Order policy", Connector.FieldTypeIdEnum);
            orderPolicy.AddListItems(1, new List<string> { "Not planned", "Lot for lot", "Fixed order quantity", "Period order quantity", "Order point", "Manually planned" });

            var replenishmentMethod = iv00102.AddField("REPLENISHMENTMETHOD", "Replenishment method", Connector.FieldTypeIdEnum);
            replenishmentMethod.AddListItems(1, new List<string> { "Make", "Buy" });

            var forecastConsumptionPeriod = iv00102.AddField("FRCSTCNSMPTNPRD", "Forecast consumption period", Connector.FieldTypeIdEnum);
            forecastConsumptionPeriod.AddListItems(1, new List<string> { "Days", "Weeks", "Months" });

            var replenishmentLevel = iv00102.AddField("ReplenishmentLevel", "Replenishment level", Connector.FieldTypeIdEnum);
            replenishmentLevel.AddListItems(1, new List<string> { "Order point quantity", "Order up to level", "Vendor EOQ" });

            var popOrderMethod = iv00102.AddField("POPOrderMethod", "POP order method", Connector.FieldTypeIdEnum);
            popOrderMethod.AddListItems(1, new List<string> { "Order to independent site", "Order to master site" });

            var popVendorSelection = iv00102.AddField("POPVendorSelection", "POP vendor selection", Connector.FieldTypeIdEnum);
            popVendorSelection.AddListItems(1, new List<string> { "Site primary vendor", "Vendor with lowest cost", "Vendor with shortest lead time" });

            var costSelection = iv00102.AddField("POPPricingSelection", "Cost selection", Connector.FieldTypeIdEnum);
            costSelection.AddListItems(1, new List<string> { "Vendor last originating invoice cost", "Item current cost", "Item standard cost", "Specified cost (in functional currency)" });
            
        }

        public ConnectorEntity GetInventoryTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListInventoryTrx, "Inventory transactions", ParentConnector);

            var svIvTrx = entity.AddTable("svIVTrx");

            var iv00101 = entity.AddTable("IV00101", "svIVTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");

            var iv30300 = entity.AddTable("IV30300", "svIVTrx");
            iv30300.AddJoinFields("DOCTYPE", "IVDOCTYP");
            iv30300.AddJoinFields("DOCNUMBR", "DOCNUMBR");
            iv30300.AddJoinFields("LNSEQNBR", "LNSEQNBR");

            var iv10001 = entity.AddTable("IV10001", "svIVTrx");
            iv10001.AddJoinFields("IVDOCTYP", "IVDOCTYP");
            iv10001.AddJoinFields("DOCNUMBR", "DOCNUMBR");
            iv10001.AddJoinFields("LNSEQNBR", "LNSEQNBR");

            var iv30200 = entity.AddTable("IV30200", "svIVTrx");
            iv30200.AddJoinFields("IVDOCTYP", "IVDOCTYP");
            iv30200.AddJoinFields("DOCNUMBR", "DOCNUMBR");

            var iv10000 = entity.AddTable("IV10000", "svIVTrx");
            iv10000.AddJoinFields("IVDOCTYP", "IVDOCTYP");
            iv10000.AddJoinFields("IVDOCNBR", "DOCNUMBR");

            AddInventoryTransactionEntityFields(svIvTrx, iv00101, iv30300, iv10001, iv30200, iv10000);

            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item shipping weight", Connector.FieldTypeIdQuantity);
            entity.AddCalculation("IV00101.DECPLQTY - 1", "Decimal places - quantites", Connector.FieldTypeIdInteger);
            entity.AddCalculation("IV00101.DECPLCUR - 1", "Decimal places - currency", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddInventoryTransactionEntityFields(ConnectorTable svIvTrx, ConnectorTable iv00101, ConnectorTable iv30300, ConnectorTable iv10001, ConnectorTable iv30200, ConnectorTable iv10000)
        {
            svIvTrx.AddField("DOCNUMBR", "Document number", Connector.FieldTypeIdString, true);
            var documentType = svIvTrx.AddField("IVDOCTYP", "Document type", Connector.FieldTypeIdEnum);
            documentType.AddListItems(1, new List<string> { "Adjustment", "Variance", "Transfer", "Receipt", "Return", "Sale", "Bill of materials" });
            svIvTrx.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            svIvTrx.AddField("UOFM", "U of M", Connector.FieldTypeIdString, true);
            svIvTrx.AddField("TRXQTY", "Transaction quantity", Connector.FieldTypeIdQuantity, true);
            svIvTrx.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency, true);
            svIvTrx.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency, true);
            svIvTrx.AddField("TRXLOCTN", "Transaction location", Connector.FieldTypeIdString, true); 
            svIvTrx.AddField("DOCdate", "Document date", Connector.FieldTypeIdDate, true);
            var documentStatus = svIvTrx.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });

            svIvTrx.AddField("LNSEQNBR", "Line sequence number", Connector.FieldTypeIdInteger);
            iv10001.AddField("QTYBSUOM", "Quantity in base U of M", Connector.FieldTypeIdQuantity);
            svIvTrx.AddField("TRNSTLOC", "Transfer to location", Connector.FieldTypeIdString);
            svIvTrx.AddField("IVIVINDX", "Inventory account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svIvTrx.AddField("IVIVOFIX", "Inventory offset account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv30300.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            iv30300.AddField("HSTMODUL", "History module", Connector.FieldTypeIdString);
            iv30300.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString);
            iv10000.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svIvTrx.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            iv10000.AddField("RCDOCNUM", "Recurring document number", Connector.FieldTypeIdString);
            iv10000.AddField("MDFUSRID", "Modified user ID", Connector.FieldTypeIdString);
            iv10000.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            iv10000.AddField("PTDUSRID", "Posted user ID", Connector.FieldTypeIdString);
            svIvTrx.AddField("GLPOSTDT", "GL posting date", Connector.FieldTypeIdDate);
            iv10000.AddField("PSTGSTUS", "Posting status", Connector.FieldTypeIdInteger);
            iv10000.AddField("TRXQTYTL", "Transaction quantity total", Connector.FieldTypeIdQuantity);
            iv10000.AddField("IVWHRMSG", "IV work header messages", Connector.FieldTypeIdString);
            svIvTrx.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            iv00101.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString);
            iv00101.AddField("ITMSHNAM", "Item short name", Connector.FieldTypeIdString);
            iv00101.AddField("ITMGEDSC", "Item generic description", Connector.FieldTypeIdString);
            iv00101.AddField("STNDCOST", "Standard cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("CURRCOST", "Current cost", Connector.FieldTypeIdCurrency);
            iv00101.AddField("ITMTSHID", "Item tax schedule ID", Connector.FieldTypeIdString);
            iv00101.AddField("IVIVINDX", "Inventory account number from item master", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVIVOFIX", "Inventory offset account number from item master", DynamicsGpConnector.FieldTypeIdAccountIndex);
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
            iv00101.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            iv00101.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            iv00101.AddField("WRNTYDYS", "Warranty days", Connector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "Price level", Connector.FieldTypeIdString);
            iv00101.AddField("CGSINFLX", "COGS inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("CGSMCIDX", "COGS monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVINFIDX", "Inventory inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("INVMCIDX", "Inventory monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("ITEMCODE", "Item code", Connector.FieldTypeIdString);
            iv00101.AddField("LASTGENSN", "Last generated serial number", Connector.FieldTypeIdString);
            iv00101.AddField("PriceGroup", "Price group", Connector.FieldTypeIdString);
            iv00101.AddField("PINFLIDX", "Purchasing inflation account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PURMCIDX", "Purchasing monetary correction account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("PRCHSUOM", "Purchasing U of M", Connector.FieldTypeIdString);
            iv00101.AddField("SELNGUOM", "Selling U of M", Connector.FieldTypeIdString);
            iv00101.AddField("TCC", "Tax commodity code", Connector.FieldTypeIdString);
            iv00101.AddField("UPPVIDX", "Unrealized purchase price variance account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString);
            iv10001.AddField("USAGETYPE", "Usage type", Connector.FieldTypeIdInteger);
            svIvTrx.AddField("SRCRFRNCNMBR", "Source reference number", Connector.FieldTypeIdString);

            var transferFromQuantityType = svIvTrx.AddField("TRFQTYTY", "Transfer from quantity type", Connector.FieldTypeIdEnum);
            transferFromQuantityType.AddListItems(1, new List<string> { "On hand", "Returned", "In use", "In service", "Damaged" });
            
            var transferToQuantityType = svIvTrx.AddField("TRTQTYTY", "Transfer to quantity type", Connector.FieldTypeIdEnum);
            transferToQuantityType.AddListItems(1, new List<string> { "On hand", "Returned", "In use", "In Ssrvice", "Damaged" });
            
            var itemType = iv00101.AddField("ITEMTYPE", "Item type", Connector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales inventory", "Discontinued", "Kit", "Misc charges", "Services", "Flat Fee" });
            
            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax options", Connector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var itemTrackingOptions = iv00101.AddField("ITMTRKOP", "Item tracking option", Connector.FieldTypeIdEnum);
            itemTrackingOptions.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
            
            var valuationMethod = iv00101.AddField("VCTNMTHD", "Valuation method", Connector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO perpetual", "LIFO perpetual", "Average perpetual", "FIFO periodic", "LIFO periodic" });
            
            var abcCode = iv00101.AddField("ABCCODE", "ABC code", Connector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });
            
            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS account source", Connector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From component item", "From kit item" });
            
            var priceMethod = iv00101.AddField("PRICMTHD", "Price method", Connector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency amount", "Percentage of list price", "Percentage markup - current cost", "Percentage markup - standard cost", "Percentage margin - current cost", "Percentage margin - standard cost" });
            
            var sourceIndicator = svIvTrx.AddField("SOURCEINDICATOR", "Source indicator", Connector.FieldTypeIdEnum);
            sourceIndicator.AddListItems(1, new List<string> { "None", "Issue", "Reverse issue", "Finished good post", "Reverse finished good post" });

        }

        public ConnectorEntity GetLandedCostEntity()
        {
            var entity = new ConnectorEntity(GpSmartListLandedCosts, "Landed costs", ParentConnector);

            var iv41100 = entity.AddTable("IV41100");
            AddLandedCostEntityFields(iv41100);

            entity.AddCalculation("ODECPLCU - 1", "Currency decimals", Connector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Functional currency decimals", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddLandedCostEntityFields(ConnectorTable iv41100)
        {
            iv41100.AddField("Landed_Cost_ID", "Landed cost ID", Connector.FieldTypeIdString, true);
            iv41100.AddField("Long_Description", "Description", Connector.FieldTypeIdString, true);
            iv41100.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            var costCalculationMethod = iv41100.AddField("CALCMTHD", "Cost calculation method", Connector.FieldTypeIdEnum, true);
            costCalculationMethod.AddListItems(1, new List<string> { "Percent of extended cost", "Flat amount", "Flat amount per unit" });
            iv41100.AddField("Invoice_Match", "Invoice match", Connector.FieldTypeIdYesNo, true);
            iv41100.AddField("ACPURIDX", "Accrued purchases account", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            iv41100.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            iv41100.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            iv41100.AddField("Revalue_Inventory", "Revalue inventory for cost variance", Connector.FieldTypeIdYesNo);
            iv41100.AddField("Tolerance_Percentage", "Tolerance percentage", Connector.FieldTypeIdPercentage);
            iv41100.AddField("PURPVIDX", "Purchase price variance account", DynamicsGpConnector.FieldTypeIdAccountIndex);
        }

        public ConnectorEntity GetLandedCostGroupEntity()
        {
            var entity = new ConnectorEntity(GpSmartListLandedCostGroups, "Landed cost groups", ParentConnector);

            var iv41102 = entity.AddTable("IV41102");

            var iv41101 = entity.AddTable("IV41101", "IV41102", ConnectorTable.ConnectorTableJoinType.Inner);
            iv41101.AddJoinFields("Landed_Cost_Group_ID", "Landed_Cost_Group_ID");

            var iv41100 = entity.AddTable("IV41100", "IV41102", ConnectorTable.ConnectorTableJoinType.Inner);
            iv41100.AddJoinFields("Landed_Cost_ID", "Landed_Cost_ID");

            AddLandedCostGroupEntityFields(iv41102, iv41101, iv41100);

            entity.AddCalculation("IV41100.ODECPLCU - 1", "Currency decimals", Connector.FieldTypeIdInteger);
            entity.AddCalculation("IV41100.DECPLCUR - 1", "Functional currency decimals", Connector.FieldTypeIdInteger);

            return entity;
        }
        public void AddLandedCostGroupEntityFields(ConnectorTable iv41102, ConnectorTable iv41101, ConnectorTable iv41100)
        {
            iv41101.AddField("Landed_Cost_Group_ID", "Landed cost group ID", Connector.FieldTypeIdString, true);
            iv41101.AddField("Long_Description", "Description", Connector.FieldTypeIdString, true);
            iv41102.AddField("Landed_Cost_ID", "Landed cost ID", Connector.FieldTypeIdString, true);
            iv41100.AddField("Long_Description", "Landed cost description", Connector.FieldTypeIdString);
            iv41100.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString);
            iv41100.AddField("CALCMTHD", "Cost calculation method", Connector.FieldTypeIdEnum);
            iv41100.AddField("Invoice_Match", "Invoice match", Connector.FieldTypeIdYesNo);
            iv41100.AddField("ACPURIDX", "Accrued purchases account", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv41100.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            iv41100.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            iv41100.AddField("Revalue_Inventory", "Revalue inventory for cost variance", Connector.FieldTypeIdYesNo);
            iv41100.AddField("Tolerance_Percentage", "Tolerance percentage", Connector.FieldTypeIdPercentage);
            iv41100.AddField("PURPVIDX", "Purchase price variance account", DynamicsGpConnector.FieldTypeIdAccountIndex);
        }

        public ConnectorEntity GetInventoryPurchaseReceiptEntity()
        {
            var entity = new ConnectorEntity(GpSmartListInventoryPurchaseReceipts, "Inventory purchase receipts", ParentConnector);

            var iv10200 = entity.AddTable("IV10200");

            AddInventoryPurchaseReceiptEntityFields(iv10200);

            return entity;
        }
        public void AddInventoryPurchaseReceiptEntityFields(ConnectorTable iv10200)
        {
            iv10200.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            iv10200.AddField("DATERECD", "Date received", Connector.FieldTypeIdDate, true);
            iv10200.AddField("QTYRECVD", "Quantity received", Connector.FieldTypeIdQuantity, true);
            iv10200.AddField("QTYSOLD", "Quantity sold", Connector.FieldTypeIdQuantity, true);
            iv10200.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency, true);
            var quantityType = iv10200.AddField("QTYTYPE", "Quantity type", Connector.FieldTypeIdEnum, true);
            quantityType.AddListItems(1, new List<string> { "On hand", "Returned", "In use", "In service", "Damaged" });
            iv10200.AddField("TRXLOCTN", "Transaction location", Connector.FieldTypeIdString, true);
            var purchaseReceiptType = iv10200.AddField("PCHSRCTY", "Purchase receipt type", Connector.FieldTypeIdEnum, true);
            purchaseReceiptType.AddListItems(1, new List<string> { "Adjustment", "Variance", "Transfer", "Override", "Receipt", "Return", "Assembly", "In-transit" });
            iv10200.AddField("RCPTNMBR", "Receipt number", Connector.FieldTypeIdString, true);
            iv10200.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            iv10200.AddField("PORDNMBR", "Purchase order number", Connector.FieldTypeIdString, true);

            iv10200.AddField("RCTSEQNM", "Receipt sequence number", Connector.FieldTypeIdInteger);
            iv10200.AddField("RCPTSOLD", "Receipt sold", Connector.FieldTypeIdYesNo);
            iv10200.AddField("QTYRESERVED", "Quantity reserved", Connector.FieldTypeIdQuantity);
            iv10200.AddField("Landed_Cost", "Landed cost", Connector.FieldTypeIdYesNo);
        }

        public ConnectorEntity GetVendorItemsEntity()
        {
            var entity = new ConnectorEntity(GpSmartListVendorItems, "Vendor items", ParentConnector);
            // todo - add tables
            return entity;
        }
        public void AddVendorItemEntityFields()
        {
            // todo - add fields
        }

        public ConnectorEntity GetCustomerItemsEntity()
        {
            var entity = new ConnectorEntity(GpSmartListCustomerItems, "Customer items", ParentConnector);
            // todo - add tables
            return entity;
        }
        public void AddCustomerItemEntityFields()
        {
            // todo - add fields
        }

    }
}
