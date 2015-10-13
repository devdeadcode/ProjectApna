using System.Collections.Generic;
using eOne.Common.DataConnectors;

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

        public DataConnectorEntity GetItemEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListItems, "Items", ParentConnector);

            var iv00101 = entity.AddTable("IV00101");

            var iv00115 = entity.AddTable("IV00115", "IV00101");
            iv00115.AddJoinFields("ITEMNMBR", "ITEMNMBR");

            AddItemEntityFields(iv00101, iv00115);

            entity.AddCalculation("convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item Shipping Weight", DataConnector.FieldTypeIdQuantity);
            entity.AddCalculation("IV00101.DECPLQTY - 1", "Decimal Places QTYS", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("IV00101.DECPLCUR - 1", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddItemEntityFields(DataConnectorTable iv00101, DataConnectorTable iv00115)
        {
            iv00101.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            iv00101.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            var itemType = iv00101.AddField("ITEMTYPE", "Item Type", DataConnector.FieldTypeIdEnum, true);
            itemType.AddListItems(1, new List<string> { "Sales Inventory", "Discontinued", "Kit", "Misc Charges", "Services", "Flat Fee" });
            iv00101.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
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
            iv00101.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            iv00101.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            iv00101.AddField("WRNTYDYS", "Warranty Days", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
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
            iv00101.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString);
            iv00101.AddField("Revalue_Inventory", "Revalue Inventory", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("Tolerance_Percentage", "Tolerance Percentage", DataConnector.FieldTypeIdPercentage);
            iv00101.AddField("Purchase_Item_Tax_Schedu", "Purchase Item Tax Schedule ID", DataConnector.FieldTypeIdString);
            iv00101.AddField("CNTRYORGN", "Country Origin", DataConnector.FieldTypeIdString);
            iv00115.AddField("MANUFACTURER", "Manufacturer", DataConnector.FieldTypeIdString);
            iv00115.AddField("MNFCTRITMNMBR", "Manufacturer's Item Number", DataConnector.FieldTypeIdString);
            iv00115.AddField("ITEMDESC", "Mfg. Item Description", DataConnector.FieldTypeIdString);
            iv00115.AddField("PRIMARYITEM", "Primary Mfg. Number", DataConnector.FieldTypeIdString);

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

            var purchaseTaxOptions = iv00101.AddField("Purchase_Tax_Options", "Purchase Tax Options", DataConnector.FieldTypeIdEnum);
            purchaseTaxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
        }

        public DataConnectorEntity GetItemQuantityEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListItemQuantities, "Item quantities", ParentConnector);

            var iv00102 = entity.AddTable("IV00102");

            var iv00101 = entity.AddTable("IV00101", "IV00102", DataConnectorTable.DataConnectorTableJoinType.Inner);
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");

            var iv00103 = entity.AddTable("IV00103", "IV00102", DataConnectorTable.DataConnectorTableJoinType.Inner);
            iv00103.AddJoinFields("ITEMNMBR", "ITEMNMBR");
            iv00103.AddJoinFields("VENDORID", "PRIMVNDR");

            var pm00200 = entity.AddTable("PM00200", "IV00103", DataConnectorTable.DataConnectorTableJoinType.Inner);
            pm00200.AddJoinFields("VENDORID", "VENDORID");

            var iv40201 = entity.AddTable("IV40201", "IV00101", DataConnectorTable.DataConnectorTableJoinType.Inner);
            iv40201.AddJoinFields("UOMSCHDL", "UOMSCHDL");

            AddItemQuantityEntityFields(iv00102, iv00101, iv00103, pm00200, iv40201);

            entity.AddCalculation("case IV00102.RCRDTYPE when 1 then 'All' else IV00102.LOCNCODE end", "Location Code", DataConnector.FieldTypeIdString, true);
            entity.AddCalculation("QTYONHND - IV00102.ATYALLOC", "QTY Available", DataConnector.FieldTypeIdQuantity, true);
            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item Shipping Weight", DataConnector.FieldTypeIdQuantity);
            entity.AddCalculation("IV40201.UMDPQTYS - 1", "U Of M Decimal Places QTYS", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddItemQuantityEntityFields(DataConnectorTable iv00102, DataConnectorTable iv00101, DataConnectorTable iv00103, DataConnectorTable pm00200, DataConnectorTable iv40201)
        {
            iv00102.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            iv00101.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            var recordType = iv00102.AddField("RCRDTYPE", "Record Type", DataConnector.FieldTypeIdEnum, true);
            recordType.AddListItems(1, new List<string> { "Overall", "Site" });
            iv00102.AddField("QTYONORD", "QTY On Order", DataConnector.FieldTypeIdQuantity, true);
            iv00102.AddField("QTYONHND", "QTY On Hand", DataConnector.FieldTypeIdQuantity, true);
            iv00102.AddField("ATYALLOC", "QTY Allocated", DataConnector.FieldTypeIdQuantity, true);
            iv00102.AddField("BINNMBR", "Bin Number", DataConnector.FieldTypeIdString);
            iv00102.AddField("PRIMVNDR", "Primary Vendor", DataConnector.FieldTypeIdString);
            iv00102.AddField("ITMFRFLG", "Item Freeze Flag", DataConnector.FieldTypeIdYesNo);
            iv00102.AddField("BGNGQTY", "Beginning QTY", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("LSORDQTY", "Last ORD QTY", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("LRCPTQTY", "Last RCPT QTY", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("LSTORDDT", "Last ORD Date", DataConnector.FieldTypeIdDate);
            iv00102.AddField("LSORDVND", "Last ORD Vendor", DataConnector.FieldTypeIdString);
            iv00102.AddField("LSRCPTDT", "Last RCPT Date", DataConnector.FieldTypeIdDate);
            iv00102.AddField("QTYRQSTN", "QTY Requisitioned", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTYBKORD", "QTY Back Ordered", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTY_Drop_Shipped", "QTY Drop Shipped", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("ORDRPNTQTY", "Order Point Qty", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("REORDERVARIANCE", "Reorder Variance", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("ORDRUPTOLVL", "Order Up To Level", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTYINUSE", "QTY In Use", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTYINSVC", "QTY In Service", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTYRTRND", "QTY Returned", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTYDMGED", "QTY Damaged", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTYCOMTD", "QTY Committed", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("QTYSOLD", "QTY Sold", DataConnector.FieldTypeIdQuantity);
            iv00101.AddField("ITMGEDSC", "Item Generic Description", DataConnector.FieldTypeIdString);
            iv00101.AddField("ITMSHNAM", "Item Short Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VENDNAME", "Vendor Name", DataConnector.FieldTypeIdString);
            iv00103.AddField("VNDITNUM", "Vendor Item Number", DataConnector.FieldTypeIdString);
            iv00103.AddField("VNDITDSC", "Vendor Item Description", DataConnector.FieldTypeIdString);
            iv00101.AddField("CURRCOST", "Current Cost", DataConnector.FieldTypeIdCurrency);
            iv00101.AddField("STNDCOST", "Standard Cost", DataConnector.FieldTypeIdCurrency);
            iv40201.AddField("IV40201.BASEUOFM", "Base U Of M", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_1", "User Category Value 1", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_2", "User Category Value 2", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_3", "User Category Value 3", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_4", "User Category Value 4", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_5", "User Category Value 5", DataConnector.FieldTypeIdString);
            iv00101.AddField("USCATVLS_6", "User Category Value 6", DataConnector.FieldTypeIdString);
            iv00101.AddField("ALTITEM1", "Alternate Item 1", DataConnector.FieldTypeIdString);
            iv00101.AddField("ALTITEM2", "Alternate Item 2", DataConnector.FieldTypeIdString);
            iv00101.AddField("ITMTSHID", "Item Tax Schedule ID", DataConnector.FieldTypeIdString);
            iv00101.AddField("KPERHIST", "Keep Period History", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("KPTRXHST", "Keep Trx History", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("KPCALHST", "Keep Calendar History", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("KPDSTHST", "Keep Distribution History", DataConnector.FieldTypeIdYesNo);
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
            iv00101.AddField("ALWBKORD", "Allow Back Orders", DataConnector.FieldTypeIdYesNo);
            iv00101.AddField("UOMSCHDL", "U Of M Schedule", DataConnector.FieldTypeIdString);
            iv00101.AddField("MSTRCDTY", "Master Record Type", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("WRNTYDYS", "Warranty Days", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
            iv00102.AddField("LSTCNTDT", "Last Count Date", DataConnector.FieldTypeIdDate);
            iv00102.AddField("LSTCNTTM", "Last Count Time", DataConnector.FieldTypeIdTime);
            iv00102.AddField("NXTCNTDT", "Next Count Date", DataConnector.FieldTypeIdDate);
            iv00102.AddField("NXTCNTTM", "Next Count Time", DataConnector.FieldTypeIdTime);
            iv00102.AddField("STCKCNTINTRVL", "Stock Count Interval", DataConnector.FieldTypeIdInteger);
            iv40201.AddField("IV40201.UMSCHDSC", "U Of M Schedule Description", DataConnector.FieldTypeIdString);
            pm00200.AddField("VNDCHKNM", "Vendor Check Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VENDSHNM", "Vendor Short Name", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADDCDPR", "Vendor Address Code - Primary", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDPAD", "Vendor Address Code - Purchase Address", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDSFR", "Vendor Address Code - Ship From", DataConnector.FieldTypeIdString);
            pm00200.AddField("VADCDTRO", "Vendor Address Code - Remit To", DataConnector.FieldTypeIdString);
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
            pm00200.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pm00200.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("ACNMVNDR", "Account Number With Vendor", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXIDNMBR", "Tax ID Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TXRGNNUM", "Tax Registration Number", DataConnector.FieldTypeIdString);
            pm00200.AddField("PARVENID", "Parent Vendor ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("TRDDISCT", "Trade Discount", DataConnector.FieldTypeIdPercentage);
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
            pm00200.AddField("KPCALHST", "Keep Calendar History from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KGLDSTHS", "Keep GL Dist History", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPERHIST", "Keep Period History from Vendor Master", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("KPTRXHST", "Keep Trx History from Vendor Master", DataConnector.FieldTypeIdYesNo);
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
            pm00200.AddField("PURPVIDX", "Purchase Price Variance Account Number from Vendor Master", DynamicsGpConnector.FieldTypeIdAccountIndex);
            pm00200.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            pm00200.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("Revalue_Vendor", "Revalue Vendor", DataConnector.FieldTypeIdYesNo);
            pm00200.AddField("DISGRPER", "Discount Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("DOCFMTID", "Document Format ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("DUEGRPER", "Due Date Grace Period", DataConnector.FieldTypeIdInteger);
            pm00200.AddField("GOVCRPID", "Governmental Corporate ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("GOVINDID", "Governmental Individual ID", DataConnector.FieldTypeIdString);
            pm00200.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            pm00200.AddField("TaxInvRecvd", "Tax Invoice Received", DataConnector.FieldTypeIdYesNo);
            iv00103.AddField("Last_Currency_ID", "Last Currency ID", DataConnector.FieldTypeIdString);
            iv00103.AddField("LSTORDDT", "Last ORD Date from Vendor Item", DataConnector.FieldTypeIdDate);
            iv00103.AddField("LSORDQTY", "Last ORD QTY from Vendor Item", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("Last_Originating_Cost", "Last Originating Cost", DataConnector.FieldTypeIdCurrency);
            iv00103.AddField("LRCPTCST", "Last RCPT Cost", DataConnector.FieldTypeIdCurrency);
            iv00103.AddField("LRCPTQTY", "Last RCPT QTY from Vendor Item", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("MAXORDQTY", "Maximum ORD QTY", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("MINORQTY", "Minimum ORD QTY", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("NORCTITM", "Number Of RCPTS For Item", DataConnector.FieldTypeIdInteger);
            iv00103.AddField("PLANNINGLEADTIME", "Planning Lead Time", DataConnector.FieldTypeIdInteger);
            iv00103.AddField("QTY_Drop_Shipped", "QTY Drop Shipped from Vendor Item", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("QTYONORD", "QTY On Order from Vendor Item", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("QTYRQSTN", "QTY Requisitioned from Vendor Item", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("LSRCPTDT", "Last RCPT Date from Vendor Item", DataConnector.FieldTypeIdDate);
            iv00103.AddField("AVRGLDTM", "Average Lead Time", DataConnector.FieldTypeIdInteger);
            iv00103.AddField("PRCHSUOM", "Purchasing U Of M from Vendor Item", DataConnector.FieldTypeIdString);
            iv00103.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            iv00103.AddField("ECORDQTY", "Economic ORD QTY", DataConnector.FieldTypeIdQuantity);
            iv00103.AddField("ITMVNDTY", "Item Vendor Type", DataConnector.FieldTypeIdInteger);
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
            iv00102.AddField("Landed_Cost_Group_ID", "Landed Cost Group ID", DataConnector.FieldTypeIdString);
            iv00102.AddField("PORETRNBIN", "Purchase Returns Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("SOFULFILLMENTBIN", "SO Fulfillment Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("SORETURNBIN", "SO Return Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("BOMRCPTBIN", "BOM Receipt Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("MATERIALISSUEBIN", "Material Issues Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("MORECEIPTBIN", "MO Receipt Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("REPAIRISSUESBIN", "Repair Issues Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("BUYERID", "Buyer ID", DataConnector.FieldTypeIdString);
            iv00102.AddField("PLANNERID", "Planner ID", DataConnector.FieldTypeIdString);
            iv00102.AddField("FXDORDRQTY", "Fixed Order Qty", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("NMBROFDYS", "Number of Days", DataConnector.FieldTypeIdInteger);
            iv00102.AddField("MNMMORDRQTY", "Minimum Order Qty", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("MXMMORDRQTY", "Maximum Order Qty", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("ORDERMULTIPLE", "Order Multiple", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("SHRINKAGEFACTOR", "Shrinkage Factor", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("PRCHSNGLDTM", "Purchasing Lead Time", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("MNFCTRNGFXDLDTM", "Manufacturing Fixed Lead Time", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("MNFCTRNGVRBLLDTM", "Manufacturing Variable Lead Time", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("STAGINGLDTME", "Staging Lead Time", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("PLNNNGTMFNCDYS", "Planning Time Fence Days", DataConnector.FieldTypeIdInteger);
            iv00102.AddField("DMNDTMFNCPRDS", "Demand Time Fence Periods", DataConnector.FieldTypeIdInteger);
            iv00102.AddField("INCLDDINPLNNNG", "Included in Planning", DataConnector.FieldTypeIdYesNo);
            iv00102.AddField("SFTYSTCKQTY", "Safety Stock Qty", DataConnector.FieldTypeIdQuantity);
            iv00102.AddField("PORECEIPTBIN", "Purchase Receipt Bin", DataConnector.FieldTypeIdString);
            iv00102.AddField("MasterLocationCode", "Master Location Code", DataConnector.FieldTypeIdString);
            iv00102.AddField("PurchasePrice", "Specified Cost", DataConnector.FieldTypeIdCurrency);
            iv00102.AddField("IncludeAllocations", "Include Allocations", DataConnector.FieldTypeIdYesNo);
            iv00102.AddField("IncludeBackorders", "Include Backorders", DataConnector.FieldTypeIdYesNo);
            iv00102.AddField("IncludeRequisitions", "Include Requisitions", DataConnector.FieldTypeIdYesNo);

            var valuationMethod = iv00101.AddField("VCTNMTHD", "Valuation Method", DataConnector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO Perpetual", "LIFO Perpetual", "Average Perpetual", "FIFO Periodic", "LIFO Periodic" });

            var itemType = iv00101.AddField("ITEMTYPE", "Item Type", DataConnector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales Inventory", "Discontinued", "Kit", "Misc Charges", "Services", "Flat Fee" });

            var itemTrackingOption = iv00101.AddField("ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });

            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax Options", DataConnector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var vendorStatus = pm00200.AddField("VENDSTTS", "Vendor Status", DataConnector.FieldTypeIdEnum);
            vendorStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Temporary" });

            var ten99Type = pm00200.AddField("TEN99TYPE", "1099 Type", DataConnector.FieldTypeIdEnum);
            ten99Type.AddListItems(1, new List<string> { "Not a 1099 Vendor", "Dividend", "Interest", "Miscellaneous" });

            var minPaymentType = pm00200.AddField("MINPYTYP", "Minimum Payment Type", DataConnector.FieldTypeIdEnum);
            minPaymentType.AddListItems(0, new List<string> { "No Minimum", "Percent", "Amount" });

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

            var freeOnBoardFromVendorItem = iv00103.AddField("FREEONBOARD", "Free On Board from Vendor Item", DataConnector.FieldTypeIdEnum);
            freeOnBoardFromVendorItem.AddListItems(1, new List<string> { "None", "Origin", "Destination" });

            var abcCode = iv00101.AddField("ABCCODE", "ABC Code", DataConnector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });

            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS Account Source", DataConnector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From Component Item", "From Kit Item" });

            var priceMethod = iv00101.AddField("PRICMTHD", "Price Method", DataConnector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency Amount", "% of List Price", "% Markup - Current Cost", "% Markup - Standard Cost", "% Margin - Current Cost", "% Margin - Standard Cost" });

            var orderPolicy = iv00102.AddField("ORDERPOLICY", "Order Policy", DataConnector.FieldTypeIdEnum);
            orderPolicy.AddListItems(1, new List<string> { "Not Planned", "Lot for Lot", "Fixed Order Quantity", "Period Order Quantity", "Order Point", "Manually Planned" });

            var replenishmentMethod = iv00102.AddField("REPLENISHMENTMETHOD", "Replenishment Method", DataConnector.FieldTypeIdEnum);
            replenishmentMethod.AddListItems(1, new List<string> { "Make", "Buy" });

            var forecastConsumptionPeriod = iv00102.AddField("FRCSTCNSMPTNPRD", "Forecast Consumption Period", DataConnector.FieldTypeIdEnum);
            forecastConsumptionPeriod.AddListItems(1, new List<string> { "Days", "Weeks", "Months" });

            var replenishmentLevel = iv00102.AddField("ReplenishmentLevel", "Replenishment Level", DataConnector.FieldTypeIdEnum);
            replenishmentLevel.AddListItems(1, new List<string> { "Order Point Quantity", "Order-Up-To Level", "Vendor EOQ" });

            var popOrderMethod = iv00102.AddField("POPOrderMethod", "POP Order Method", DataConnector.FieldTypeIdEnum);
            popOrderMethod.AddListItems(1, new List<string> { "Order To Independent Site", "Order To Master Site" });

            var popVendorSelection = iv00102.AddField("POPVendorSelection", "POP Vendor Selection", DataConnector.FieldTypeIdEnum);
            popVendorSelection.AddListItems(1, new List<string> { "Site Primary Vendor", "Vendor With Lowest Cost", "Vendor With Shortest Lead Time" });

            var costSelection = iv00102.AddField("POPPricingSelection", "Cost Selection", DataConnector.FieldTypeIdEnum);
            costSelection.AddListItems(1, new List<string> { "Vendor Last Originating Invoice Cost", "Item Current Cost", "Item Standard Cost", "Specified Cost (In Functional Currency)" });
            
        }

        public DataConnectorEntity GetInventoryTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListInventoryTrx, "Inventory transactions", ParentConnector);

            var svIvTrx = entity.AddTable("svIVTrx");

            var iv00101 = entity.AddTable("IV00101", "svIVTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
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

            entity.AddCalculation("(convert(numeric(18,2),IV00101.ITEMSHWT) / 100", "Item Shipping Weight", DataConnector.FieldTypeIdQuantity);
            entity.AddCalculation("IV00101.DECPLQTY - 1", "Decimal Places QTYS", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("IV00101.DECPLCUR - 1", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddInventoryTransactionEntityFields(DataConnectorTable svIvTrx, DataConnectorTable iv00101, DataConnectorTable iv30300, DataConnectorTable iv10001, DataConnectorTable iv30200, DataConnectorTable iv10000)
        {
            svIvTrx.AddField("DOCNUMBR", "Document Number", DataConnector.FieldTypeIdString, true);
            var documentType = svIvTrx.AddField("IVDOCTYP", "Document Type", DataConnector.FieldTypeIdEnum);
            documentType.AddListItems(1, new List<string> { "Adjustment", "Variance", "Transfer", "Receipt", "Return", "Sale", "Bill Of Materials" });
            svIvTrx.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            svIvTrx.AddField("UOFM", "U Of M", DataConnector.FieldTypeIdString, true);
            svIvTrx.AddField("TRXQTY", "TRX QTY", DataConnector.FieldTypeIdQuantity, true);
            svIvTrx.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency, true);
            svIvTrx.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency, true);
            svIvTrx.AddField("TRXLOCTN", "TRX Location", DataConnector.FieldTypeIdString, true);
            svIvTrx.AddField("DOCDATE", "Document Date", DataConnector.FieldTypeIdDate, true);
            var documentStatus = svIvTrx.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Unposted", "", "Posted" });

            svIvTrx.AddField("LNSEQNBR", "Line Sequence Number", DataConnector.FieldTypeIdInteger);
            iv10001.AddField("QTYBSUOM", "Quantity In Base U Of M", DataConnector.FieldTypeIdQuantity);
            svIvTrx.AddField("TRNSTLOC", "Transfer To Location", DataConnector.FieldTypeIdString);
            svIvTrx.AddField("IVIVINDX", "Inventory Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svIvTrx.AddField("IVIVOFIX", "Inventory Offset Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv30300.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            iv30300.AddField("HSTMODUL", "History Module", DataConnector.FieldTypeIdString);
            iv30300.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString);
            iv10000.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svIvTrx.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            iv10000.AddField("RCDOCNUM", "Recurring Document Number", DataConnector.FieldTypeIdString);
            iv10000.AddField("MDFUSRID", "Modified User ID", DataConnector.FieldTypeIdString);
            iv10000.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            iv10000.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svIvTrx.AddField("GLPOSTDT", "GL Posting Date", DataConnector.FieldTypeIdDate);
            iv10000.AddField("PSTGSTUS", "Posting Status", DataConnector.FieldTypeIdInteger);
            iv10000.AddField("TRXQTYTL", "TRX QTY Total", DataConnector.FieldTypeIdQuantity);
            iv10000.AddField("IVWHRMSG", "IV WORK HDR Messages", DataConnector.FieldTypeIdString);
            svIvTrx.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString);
            iv00101.AddField("ITMSHNAM", "Item Short Name", DataConnector.FieldTypeIdString);
            iv00101.AddField("ITMGEDSC", "Item Generic Description", DataConnector.FieldTypeIdString);
            iv00101.AddField("STNDCOST", "Standard Cost", DataConnector.FieldTypeIdCurrency);
            iv00101.AddField("CURRCOST", "Current Cost", DataConnector.FieldTypeIdCurrency);
            iv00101.AddField("ITMTSHID", "Item Tax Schedule ID", DataConnector.FieldTypeIdString);
            iv00101.AddField("IVIVINDX", "Inventory Account Number from Item Master", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv00101.AddField("IVIVOFIX", "Inventory Offset Account Number from Item Master", DynamicsGpConnector.FieldTypeIdAccountIndex);
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
            iv00101.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            iv00101.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            iv00101.AddField("WRNTYDYS", "Warranty Days", DataConnector.FieldTypeIdInteger);
            iv00101.AddField("PRCLEVEL", "PriceLevel", DataConnector.FieldTypeIdString);
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
            iv00101.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString);
            iv10001.AddField("USAGETYPE", "Usage Type", DataConnector.FieldTypeIdInteger);
            svIvTrx.AddField("SRCRFRNCNMBR", "Source Reference Number", DataConnector.FieldTypeIdString);

            var transferFromQuantityType = svIvTrx.AddField("TRFQTYTY", "Transfer from quantity type", DataConnector.FieldTypeIdEnum);
            transferFromQuantityType.AddListItems(1, new List<string> { "On Hand", "Returned", "In Use", "In Service", "Damaged" });
            
            var transferToQuantityType = svIvTrx.AddField("TRTQTYTY", "Transfer to quantity type", DataConnector.FieldTypeIdEnum);
            transferToQuantityType.AddListItems(1, new List<string> { "On Hand", "Returned", "In Use", "In Service", "Damaged" });
            
            var itemType = iv00101.AddField("ITEMTYPE", "Item type", DataConnector.FieldTypeIdEnum);
            itemType.AddListItems(1, new List<string> { "Sales Inventory", "Discontinued", "Kit", "Misc Charges", "Services", "Flat Fee" });
            
            var taxOptions = iv00101.AddField("TAXOPTNS", "Tax Options", DataConnector.FieldTypeIdEnum);
            taxOptions.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var itemTrackingOptions = iv00101.AddField("ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOptions.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
            
            var valuationMethod = iv00101.AddField("VCTNMTHD", "Valuation Method", DataConnector.FieldTypeIdEnum);
            valuationMethod.AddListItems(1, new List<string> { "FIFO Perpetual", "LIFO Perpetual", "Average Perpetual", "FIFO Periodic", "LIFO Periodic" });
            
            var abcCode = iv00101.AddField("ABCCODE", "ABC Code", DataConnector.FieldTypeIdEnum);
            abcCode.AddListItems(1, new List<string> { "None", "A", "B", "C" });
            
            var kitCogsAccountSource = iv00101.AddField("KTACCTSR", "Kit COGS Account Source", DataConnector.FieldTypeIdEnum);
            kitCogsAccountSource.AddListItems(0, new List<string> { "From Component Item", "From Kit Item" });
            
            var priceMethod = iv00101.AddField("PRICMTHD", "Price Method", DataConnector.FieldTypeIdEnum);
            priceMethod.AddListItems(1, new List<string> { "Currency Amount", "% of List Price", "% Markup - Current Cost", "% Markup - Standard Cost", "% Margin - Current Cost", "% Margin - Standard Cost" });
            
            var sourceIndicator = svIvTrx.AddField("SOURCEINDICATOR", "Source Indicator", DataConnector.FieldTypeIdEnum);
            sourceIndicator.AddListItems(1, new List<string> { "None", "Issue", "Reverse Issue", "Finished Good Post", "Reverse Finished Good Post" });

        }

        public DataConnectorEntity GetLandedCostEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListLandedCosts, "Landed costs", ParentConnector);

            var iv41100 = entity.AddTable("IV41100");
            AddLandedCostEntityFields(iv41100);

            entity.AddCalculation("ODECPLCU - 1", "Currency decimals", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Functional currency decimals", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddLandedCostEntityFields(DataConnectorTable iv41100)
        {
            iv41100.AddField("Landed_Cost_ID", "Landed Cost ID", DataConnector.FieldTypeIdString, true);
            iv41100.AddField("Long_Description", "Description", DataConnector.FieldTypeIdString, true);
            iv41100.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            var costCalculationMethod = iv41100.AddField("CALCMTHD", "Cost Calculation Method", DataConnector.FieldTypeIdEnum, true);
            costCalculationMethod.AddListItems(1, new List<string> { "Percent of Extended Cost", "Flat Amount", "Flat Amount Per Unit" });
            iv41100.AddField("Invoice_Match", "Invoice Match", DataConnector.FieldTypeIdYesNo, true);
            iv41100.AddField("ACPURIDX", "Accrued Purchases Account", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            iv41100.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            iv41100.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            iv41100.AddField("Revalue_Inventory", "Revalue Inventory for Cost Variance", DataConnector.FieldTypeIdYesNo);
            iv41100.AddField("Tolerance_Percentage", "Tolerance Percentage", DataConnector.FieldTypeIdPercentage);
            iv41100.AddField("PURPVIDX", "Purchase Price Variance Account", DynamicsGpConnector.FieldTypeIdAccountIndex);
        }

        public DataConnectorEntity GetLandedCostGroupEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListLandedCostGroups, "Landed cost groups", ParentConnector);

            var iv41102 = entity.AddTable("IV41102");

            var iv41101 = entity.AddTable("IV41101", "IV41102", DataConnectorTable.DataConnectorTableJoinType.Inner);
            iv41101.AddJoinFields("Landed_Cost_Group_ID", "Landed_Cost_Group_ID");

            var iv41100 = entity.AddTable("IV41100", "IV41102", DataConnectorTable.DataConnectorTableJoinType.Inner);
            iv41100.AddJoinFields("Landed_Cost_ID", "Landed_Cost_ID");

            AddLandedCostGroupEntityFields(iv41102, iv41101, iv41100);

            entity.AddCalculation("IV41100.ODECPLCU - 1", "Currency Decimals", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("IV41100.DECPLCUR - 1", "Functional Currency Decimals", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        public void AddLandedCostGroupEntityFields(DataConnectorTable iv41102, DataConnectorTable iv41101, DataConnectorTable iv41100)
        {
            iv41101.AddField("Landed_Cost_Group_ID", "Landed Cost Group ID", DataConnector.FieldTypeIdString, true);
            iv41101.AddField("Long_Description", "Description", DataConnector.FieldTypeIdString, true);
            iv41102.AddField("Landed_Cost_ID", "Landed Cost ID", DataConnector.FieldTypeIdString, true);
            iv41100.AddField("Long_Description", "Landed Cost Description", DataConnector.FieldTypeIdString);
            iv41100.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString);
            iv41100.AddField("CALCMTHD", "Cost Calculation Method", DataConnector.FieldTypeIdEnum);
            iv41100.AddField("Invoice_Match", "Invoice Match", DataConnector.FieldTypeIdYesNo);
            iv41100.AddField("ACPURIDX", "Accrued Purchases Account", DynamicsGpConnector.FieldTypeIdAccountIndex);
            iv41100.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            iv41100.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            iv41100.AddField("Revalue_Inventory", "Revalue Inventory for Cost Variance", DataConnector.FieldTypeIdYesNo);
            iv41100.AddField("Tolerance_Percentage", "Tolerance Percentage", DataConnector.FieldTypeIdPercentage);
            iv41100.AddField("PURPVIDX", "Purchase Price Variance Account", DynamicsGpConnector.FieldTypeIdAccountIndex);
        }

        public DataConnectorEntity GetInventoryPurchaseReceiptEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListInventoryPurchaseReceipts, "Inventory purchase receipts", ParentConnector);

            var iv10200 = entity.AddTable("IV10200");

            AddInventoryPurchaseReceiptEntityFields(iv10200);

            return entity;
        }
        public void AddInventoryPurchaseReceiptEntityFields(DataConnectorTable iv10200)
        {
            iv10200.AddField("ITEMNMBR", "Item number", DataConnector.FieldTypeIdString, true);
            iv10200.AddField("DATERECD", "Date received", DataConnector.FieldTypeIdDate, true);
            iv10200.AddField("QTYRECVD", "Quantity received", DataConnector.FieldTypeIdQuantity, true);
            iv10200.AddField("QTYSOLD", "Quantity sold", DataConnector.FieldTypeIdQuantity, true);
            iv10200.AddField("UNITCOST", "Unit cost", DataConnector.FieldTypeIdCurrency, true);
            var quantityType = iv10200.AddField("QTYTYPE", "Quantity type", DataConnector.FieldTypeIdEnum, true);
            quantityType.AddListItems(1, new List<string> { "On hand", "Returned", "In use", "In service", "Damaged" });
            iv10200.AddField("TRXLOCTN", "Transaction location", DataConnector.FieldTypeIdString, true);
            var purchaseReceiptType = iv10200.AddField("PCHSRCTY", "Purchase receipt type", DataConnector.FieldTypeIdEnum, true);
            purchaseReceiptType.AddListItems(1, new List<string> { "Adjustment", "Variance", "Transfer", "Override", "Receipt", "Return", "Assembly", "In-Transit" });
            iv10200.AddField("RCPTNMBR", "Receipt number", DataConnector.FieldTypeIdString, true);
            iv10200.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            iv10200.AddField("PORDNMBR", "Purchase order number", DataConnector.FieldTypeIdString, true);

            iv10200.AddField("RCTSEQNM", "Receipt sequence number", DataConnector.FieldTypeIdInteger);
            iv10200.AddField("RCPTSOLD", "Receipt sold", DataConnector.FieldTypeIdYesNo);
            iv10200.AddField("QTYRESERVED", "Quantity reserved", DataConnector.FieldTypeIdQuantity);
            iv10200.AddField("Landed_Cost", "Landed cost", DataConnector.FieldTypeIdYesNo);
        }
    }
}
