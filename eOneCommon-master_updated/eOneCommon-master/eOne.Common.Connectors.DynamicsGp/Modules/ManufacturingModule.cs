using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class ManufacturingModule : DynamicsGpModule
    {

        private const short ManufacturingSmartListSoMoLink = 1;
        private const short ManufacturingSmartListWorkInProcess = 2;
        private const short ManufacturingSmartListSalesDocumentsNotLinkedToManufacturing = 3;
        private const short ManufacturingSmartListBillOfMaterials = 4;
        private const short ManufacturingSmartListManufacturingOrders = 5;
        private const short ManufacturingSmartListRecordedOutsourcingShipments = 6;
        private const short ManufacturingSmartListSuggestedOutsourcingShipments = 7;
        private const short ManufacturingSmartListSuggestedOutsourcingPurchaseOrders = 8;
        private const short ManufacturingSmartListPicklists = 9;
        
        public ManufacturingModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 346;
            Name = "Manufacturing";
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetSoMoLinkEntity());
            Entities.Add(GetWorkInProcessEntity());
            Entities.Add(GetSalesDocumentsNotLinkedToManufacturingEntity());
            Entities.Add(GetBillOfMaterialEntity());
            Entities.Add(GetManufacturingOrderEntity());
            Entities.Add(GetRecordedOutsourcingShipmentEntity());
            Entities.Add(GetSuggestedOutsourcingShipmentEntity());
            Entities.Add(GetSuggestedOutsourcingPurchaseOrderEntity());
            Entities.Add(GetPicklistEntity());
        }

        public ConnectorEntity GetSoMoLinkEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListSoMoLink), "SO/MO link", ParentConnector);
            
            var is010001 = entity.AddTable("IS010001");
            
            AddSoMoLinkEntityFields(is010001);
            
            return entity;
        }
        public void AddSoMoLinkEntityFields(ConnectorTable is010001)
        {
            is010001.AddField("SOPNUMBE", "Sales order number", Connector.FieldTypeIdString, true);
            is010001.AddField("MANUFACTUREORDER_I", "Manufacturing order number", Connector.FieldTypeIdString, true);
            is010001.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            is010001.AddField("SOITEMDUEDATE_I", "Sales order item due date", Connector.FieldTypeIdDate, true);
            is010001.AddField("SOITEMPROMISEDATE_I", "Sales order item promise date", Connector.FieldTypeIdDate, true);
            var sopType = is010001.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            is010001.AddField("LNITMSEQ", "Line item sequence", Connector.FieldTypeIdInteger);
            is010001.AddField("CMPNTSEQ", "Component sequence", Connector.FieldTypeIdInteger);
            is010001.AddField("CUSTOMERPARTNUMBER_I", "Customer part number", Connector.FieldTypeIdString);
            is010001.AddField("SOCHANGEDATE_I", "Sales order change date", Connector.FieldTypeIdDate);
            is010001.AddField("REVISIONLEVEL_I", "Revision level", Connector.FieldTypeIdString);
            is010001.AddField("MFGNOTEINDEX_I", "Manufacturing note index", Connector.FieldTypeIdInteger);
            is010001.AddField("MRKDNAMT", "Markdown amount", Connector.FieldTypeIdCurrency);
            is010001.AddField("Markdown_Amount_Addition", "Markdown amount additional", Connector.FieldTypeIdCurrency);
            is010001.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            is010001.AddField("USERID", "User ID", Connector.FieldTypeIdString);
        }
        
        public ConnectorEntity GetWorkInProcessEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListWorkInProcess), "Work in process", ParentConnector);
            
            var mop1000 = entity.AddTable("MOP1000");
            
            AddWorkInProcessEntityFields(mop1000);
            
            return entity;
        }
        public void AddWorkInProcessEntityFields(ConnectorTable mop1000)
        {
            mop1000.AddField("MANUFACTUREORDER_I", "Manufacturing order number", Connector.FieldTypeIdString, true);
            mop1000.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            mop1000.AddField("TO_SITE_I", "Issue to site ID", Connector.FieldTypeIdString, true);
            mop1000.AddField("DATERECD", "WIP in date", Connector.FieldTypeIdDate, true);
            mop1000.AddField("QTYRECVD", "WIP quantity in", Connector.FieldTypeIdQuantity, true);
            mop1000.AddField("QTYSOLD", "WIP quantity out", Connector.FieldTypeIdQuantity, true);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_10", "Unit cost", Connector.FieldTypeIdCurrency, true);
            mop1000.AddField("WIPSEQNMBR", "WIP sequence number", Connector.FieldTypeIdCurrency);
            mop1000.AddField("PICKLISTSEQ", "Picklist sequence number", Connector.FieldTypeIdInteger);
            mop1000.AddField("PICKNUMBER", "Picking document number", Connector.FieldTypeIdString);
            mop1000.AddField("PICKDOCLINENUM", "Picking document line number", Connector.FieldTypeIdInteger);
            mop1000.AddField("WIPQTYSOLD", "All WIP used", Connector.FieldTypeIdYesNo);
            mop1000.AddField("IVDOCNBR", "IV document number", Connector.FieldTypeIdString);
            mop1000.AddField("RCTSEQNM", "Receipt sequence number", Connector.FieldTypeIdInteger);
            mop1000.AddField("RTSEQNUM_I", "Routing sequence number", Connector.FieldTypeIdString);
            mop1000.AddField("FROM_SITE_I", "Issue from site ID", Connector.FieldTypeIdString);
            mop1000.AddField("DTRCVDINVNTRY", "Date received in inventory", Connector.FieldTypeIdDate);
            mop1000.AddField("BACKFLUSHITEM_I", "Backflushed", Connector.FieldTypeIdYesNo);
            mop1000.AddField("NUMBERSCRAPPED_I", "Scrapped quantity", Connector.FieldTypeIdQuantity);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_1", "Material unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_2", "Material fixed overhead unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_3", "Material variable overhead unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_4", "Labor unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_5", "Labor fixed overhead unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_6", "Labor variable overhead unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_7", "Machine unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_8", "Machine fixed overhead unit cost", Connector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_9", "Machine variable overhead unit cost", Connector.FieldTypeIdCurrency);

            var itemTrackingOption = mop1000.AddField("ITMTRKOP", "Item tracking option", Connector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial numbers", "Lot numbers" });
        }
        
        public ConnectorEntity GetSalesDocumentsNotLinkedToManufacturingEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListSalesDocumentsNotLinkedToManufacturing), "Sales documents not linked to manufacturing", ParentConnector);
            
            var expv1000 = entity.AddTable("EXPV1000");
            
            AddSalesDocumentsNotLinkedToManufacturingEntityFields(expv1000);
            
            entity.AddCalculation("case EXPV1000.CUSTPRIORITY when 1 then 'None' else cast(EXPV1000.CUSTPRIORITY - 1 as varchar(3)) end", "Customer priority", Connector.FieldTypeIdString);
            
            return entity;
        }
        public void AddSalesDocumentsNotLinkedToManufacturingEntityFields(ConnectorTable expv1000)
        {
            var sopType = expv1000.AddField("SOPTYPE", "SOP type", Connector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back order", "Fulfillment order" });
            expv1000.AddField("SOPNUMBE", "SOP number", Connector.FieldTypeIdString, true);
            expv1000.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            expv1000.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            expv1000.AddField("QUANTITY", "Quantity", Connector.FieldTypeIdQuantity, true);
            expv1000.AddField("QTYBACKO", "Quantity to back order", Connector.FieldTypeIdQuantity, true);
            expv1000.AddField("ReqShipdate", "Requested ship date", Connector.FieldTypeIdDate, true);
            var itemFulfillmentMethod = expv1000.AddField("ITEMFULFILLMETHOD", "Item fulfillment method", Connector.FieldTypeIdEnum, true);
            itemFulfillmentMethod.AddListItems(1, new List<string> { "Make to stock", "Make to order - manual", "Make to order - silent" });
            expv1000.AddField("UOFM", "U of M", Connector.FieldTypeIdString, true);
            expv1000.AddField("CUSTNAME", "Customer name", Connector.FieldTypeIdString);
            expv1000.AddField("AVERAGEORDQTY_I", "Average order quantity", Connector.FieldTypeIdQuantity);
            expv1000.AddField("EFFECTIVEDATE_I", "Effective date", Connector.FieldTypeIdDate);
            expv1000.AddField("LNITMSEQ", "Line item sequence", Connector.FieldTypeIdInteger);
            expv1000.AddField("CMPNTSEQ", "Component sequence", Connector.FieldTypeIdInteger);
            expv1000.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString);
            expv1000.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString);

            var makeBuyCode = expv1000.AddField("MAKEBUYCODE_I", "Make/buy code", Connector.FieldTypeIdEnum);
            makeBuyCode.AddListItems(1, new List<string> { "Make", "Either", "Buy" });

            var itemStatus = expv1000.AddField("ITEMSTATUS_I", "Item status", Connector.FieldTypeIdEnum);
            itemStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Service", "Obsolete", "Prerelease", "Released" });
        }
        
        public ConnectorEntity GetBillOfMaterialEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListBillOfMaterials), "Bill of materials", ParentConnector);
            
            var expv1001 = entity.AddTable("EXPV1001");
            
            AddBillOfMaterialEntityFields(expv1001);
            
            return entity;
        }
        public void AddBillOfMaterialEntityFields(ConnectorTable expv1001)
        {
            expv1001.AddField("PPN_I", "Finished good item number", Connector.FieldTypeIdString, true);
            var bomType = expv1001.AddField("BOMCAT_I", "BOM type", Connector.FieldTypeIdEnum, true);
            bomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });
            var bomCategory = expv1001.AddField("BOMTYPE_I", "BOM category", Connector.FieldTypeIdEnum, true);
            bomCategory.AddListItems(1, new List<string> { "Regular", "Modular", "Phantom", "Option" });
            expv1001.AddField("BOMNAME_I", "BOM name", Connector.FieldTypeIdString, true);
            expv1001.AddField("BOM_REVISION_LEVEL", "Revision level", Connector.FieldTypeIdString, true);
            expv1001.AddField("BOMSEQ_I", "BOM position number", Connector.FieldTypeIdInteger, true);
            expv1001.AddField("CPN_I", "Component", Connector.FieldTypeIdString, true);
            var componentBomType = expv1001.AddField("SUBCAT_I", "Component BOM type", Connector.FieldTypeIdEnum, true);
            componentBomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });
            expv1001.AddField("SUBNAME_I", "Component BOM name", Connector.FieldTypeIdString, true);
            expv1001.AddField("QUANTITY_I", "Quantity", Connector.FieldTypeIdQuantity, true);
            expv1001.AddField("UOFM", "U of M", Connector.FieldTypeIdString, true);
            expv1001.AddField("FIXED_QTY_I", "Fixed quantity", Connector.FieldTypeIdQuantity, true);
            expv1001.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency, true);
            expv1001.AddField("LOCNCODE", "Issue from", Connector.FieldTypeIdString, true);
            expv1001.AddField("WCID_I", "Issue to", Connector.FieldTypeIdString, true);
            expv1001.AddField("FLOORSTOCK_I", "Floor stock", Connector.FieldTypeIdYesNo, true);
            expv1001.AddField("BACKFLUSHITEM_I", "Backflush item", Connector.FieldTypeIdYesNo, true);
            expv1001.AddField("ALTERNATE_I", "Alternate", Connector.FieldTypeIdYesNo, true);
            expv1001.AddField("ALTERNATEPARTFOR_I", "Alternate for", Connector.FieldTypeIdString, true);
            expv1001.AddField("EFFECTIVEDATE_I", "Effective date", Connector.FieldTypeIdDate, true);
            expv1001.AddField("EFFECTIVEINDATE_I", "Effective in date", Connector.FieldTypeIdDate);
            expv1001.AddField("EFFECTIVEOUTDATE_I", "Effective out date", Connector.FieldTypeIdDate);
            expv1001.AddField("LEADTIMEOFFSET_I", "Lead time offset", Connector.FieldTypeIdInteger);
            expv1001.AddField("OPTPERCENT_I", "Option percent", Connector.FieldTypeIdPercentage);
            expv1001.AddField("SCRAPPERCENT_I", "Scrap percentage", Connector.FieldTypeIdPercentage);
            expv1001.AddField("BOMSINGLELOT_I", "Single lot", Connector.FieldTypeIdYesNo);
            expv1001.AddField("BOMENGAPPROVAL_I", "Eng. approval required", Connector.FieldTypeIdYesNo);
            expv1001.AddField("ACTUAL_CONSUMED_CHECK_I", "Actual consumed check", Connector.FieldTypeIdYesNo);
            expv1001.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            expv1001.AddField("CHANGEBY_I", "Changed By", Connector.FieldTypeIdString);
            expv1001.AddField("MODIFDT", "Revision change date", Connector.FieldTypeIdDate);
            expv1001.AddField("USERID", "Revision changed by", Connector.FieldTypeIdString);
            expv1001.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            expv1001.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);

            var offsetFrom = expv1001.AddField("OFFSET_FROM_I", "Offset From", Connector.FieldTypeIdEnum);
            offsetFrom.AddListItems(1, new List<string> { "Manufacturing order start date", "Manufacturing order due date" });
        }
        
        public ConnectorEntity GetManufacturingOrderEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListManufacturingOrders), "Manufacturing orders", ParentConnector);
            var wo010032 = entity.AddTable("WO010032");
            var iv00101 = entity.AddTable("IV00101", "WO010032");
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");
            AddManufacturingOrderEntityFields(wo010032, iv00101);
            return entity;
        }
        public void AddManufacturingOrderEntityFields(ConnectorTable wo010032, ConnectorTable iv00101)
        {
            wo010032.AddField("MANUFACTUREORDER_I", "Manufacturing order", Connector.FieldTypeIdString, true);
            wo010032.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            iv00101.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            var moStatus = wo010032.AddField("MANUFACTUREORDERST_I", "Manufacturing order status", Connector.FieldTypeIdEnum, true);
            moStatus.AddListItems(1, new List<string> { "Quote/estimate", "Open", "Released", "Hold", "Canceled", "Complete", "Partially received", "Closed" });
            wo010032.AddField("STARTQTY_I", "Start quantity", Connector.FieldTypeIdQuantity, true);
            wo010032.AddField("ENDQTY_I", "End quantity", Connector.FieldTypeIdQuantity, true);
            wo010032.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate, true);
            wo010032.AddField("ENDdate", "Due date", Connector.FieldTypeIdDate, true);
            wo010032.AddField("POSTTOSITE_I", "Post to site", Connector.FieldTypeIdString, true);
            wo010032.AddField("ACTUALDEMAND_I", "Actual demand", Connector.FieldTypeIdCurrency);
            wo010032.AddField("BOMNAME_I", "BOM name", Connector.FieldTypeIdString);
            wo010032.AddField("DSCRIPTN", "Manufacturing order description", Connector.FieldTypeIdString);
            wo010032.AddField("DRAWFROMSITE_I", "Draw from site", Connector.FieldTypeIdString);
            wo010032.AddField("LOTNUMBR", "Lot number", Connector.FieldTypeIdString);
            wo010032.AddField("SCHEDULINGPREFEREN_I", "Scheduling preference", Connector.FieldTypeIdString);
            wo010032.AddField("OUTSOURCED_I + 1", "Outsourced", Connector.FieldTypeIdYesNo);
            wo010032.AddField("PROJEMPLOYEEHRSSUM_I", "Projected employee hours", Connector.FieldTypeIdCurrency);
            wo010032.AddField("PROJMACHINEHRSSUM_I", "Projected machine hours", Connector.FieldTypeIdCurrency);
            wo010032.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);

            var bomType = wo010032.AddField("BOMCAT_I", "BOM type", Connector.FieldTypeIdEnum);
            bomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });

            var moPriority = wo010032.AddField("MANUFACTUREORDPRI_I", "Manufacturing order priority", Connector.FieldTypeIdEnum);
            moPriority.AddListItems(1, new List<string> { "High", "Medium", "Low" });

            var schedulingMethod = wo010032.AddField("SCHEDULEMETHOD_I", "Scheduling method", Connector.FieldTypeIdEnum);
            schedulingMethod.AddListItems(1, new List<string> { "Forward infinite", "Backward infinite" });
        }
        
        public ConnectorEntity GetRecordedOutsourcingShipmentEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListRecordedOutsourcingShipments), "Recorded outsourcing shipments", ParentConnector);
            var osrc1300 = entity.AddTable("OSRC1300");
            AddRecordedOutsourcingShipmentFields(osrc1300);
            return entity;
        }
        public void AddRecordedOutsourcingShipmentFields(ConnectorTable osrc1300)
        {
            osrc1300.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            osrc1300.AddField("QTYSHPPD", "Quantity shipped", Connector.FieldTypeIdQuantity, true);
            osrc1300.AddField("UOFM", "U of M", Connector.FieldTypeIdString, true);
            osrc1300.AddField("Ship_date", "Ship date", Connector.FieldTypeIdDate, true);
            osrc1300.AddField("RETUdate", "Return date", Connector.FieldTypeIdDate, true);
            osrc1300.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            osrc1300.AddField("MANUFACTUREORDER_I", "Manufacturing order", Connector.FieldTypeIdString, true);
            osrc1300.AddField("RTSEQNUM_I", "Routing sequence", Connector.FieldTypeIdString, true);
            var itemTrackingOption = osrc1300.AddField("OSRC_Item_Type", "Item tracking option", Connector.FieldTypeIdEnum, true);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Lot tracked", "Serial tracked", "WIP material" });
            osrc1300.AddField("VOIDED", "Voided", Connector.FieldTypeIdYesNo, true);
            osrc1300.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString, true);
            osrc1300.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString);
            osrc1300.AddField("VOIDdate", "Voided date", Connector.FieldTypeIdDate);
            osrc1300.AddField("VOIDEDBY", "Voided by", Connector.FieldTypeIdString);
            osrc1300.AddField("Ship_By_date", "Ship by date", Connector.FieldTypeIdDate);
            osrc1300.AddField("USERID", "Shipped by", Connector.FieldTypeIdString);
            osrc1300.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
        }
        
        public ConnectorEntity GetSuggestedOutsourcingShipmentEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListSuggestedOutsourcingShipments), "Suggested outsourcing shipments", ParentConnector);
            var osrc1200 = entity.AddTable("OSRC1200");
            var iv00101 = entity.AddTable("IV00101", "OSRC1200");
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");
            var iv40201 = entity.AddTable("IV40201", "IV00101");
            iv40201.AddJoinFields("UOMSCHDL", "UOMSCHDL");
            AddSuggestedOutsourcingShipmentEntityFields(osrc1200, iv00101, iv40201);
            return entity;
        }
        public void AddSuggestedOutsourcingShipmentEntityFields(ConnectorTable osrc1200, ConnectorTable iv00101, ConnectorTable iv40201)
        {
            osrc1200.AddField("ITEMNMBR", "Item number", Connector.FieldTypeIdString, true);
            iv00101.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            osrc1200.AddField("Total_Suggested_QTY", "Total suggested quantity", Connector.FieldTypeIdQuantity, true);
            osrc1200.AddField("QTYSHPPD", "Quantity shipped", Connector.FieldTypeIdQuantity, true);
            iv40201.AddField("BASEUOFM", "U of M", Connector.FieldTypeIdString, true);
            osrc1200.AddField("Ship_By_date", "Ship by date", Connector.FieldTypeIdDate, true);
            osrc1200.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            osrc1200.AddField("MANUFACTUREORDER_I", "Manufacturing order", Connector.FieldTypeIdString, true);
            osrc1200.AddField("RTSEQNUM_I", "Routing sequence", Connector.FieldTypeIdString, true);
            var itemTrackingOption = osrc1200.AddField("OSRC_Item_Type", "Item tracking option", Connector.FieldTypeIdEnum, true); 
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Lot tracked", "Serial tracked", "WIP material" });
            osrc1200.AddField("SEQ_I", "Picklist sequence", Connector.FieldTypeIdInteger);
        }
        
        public ConnectorEntity GetSuggestedOutsourcingPurchaseOrderEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListSuggestedOutsourcingPurchaseOrders), "Suggested outsourcing purchase orders", ParentConnector);
            var expv1002 = entity.AddTable("EXPV1002");
            AddSuggestedOutsourcingPurchaseOrderEntityFields(expv1002);
            return entity;
        }
        public void AddSuggestedOutsourcingPurchaseOrderEntityFields(ConnectorTable expv1002)
        {
            expv1002.AddField("MANUFACTUREORDER_I", "Manufacturing order", Connector.FieldTypeIdString, true);
            expv1002.AddField("ITEMNMBR", "Item number to purchase", Connector.FieldTypeIdString, true);
            expv1002.AddField("ITEMDESC", "Item description", Connector.FieldTypeIdString, true);
            expv1002.AddField("VENDORID", "Vendor ID", Connector.FieldTypeIdString, true);
            expv1002.AddField("RELEASEBYdate", "Release by date", Connector.FieldTypeIdDate, true);
            expv1002.AddField("REQdate", "Required date", Connector.FieldTypeIdDate, true);
            expv1002.AddField("QTYRECVD", "Received quantity", Connector.FieldTypeIdQuantity, true);
            expv1002.AddField("Total_Suggested_QTY", "Total suggested quantity", Connector.FieldTypeIdQuantity, true);
            expv1002.AddField("QTYORDER", "Quantity ordered", Connector.FieldTypeIdQuantity, true);
            expv1002.AddField("QTYTORDR", "Quantity remaining to order", Connector.FieldTypeIdQuantity, true);
            expv1002.AddField("UOFM", "U of M", Connector.FieldTypeIdString, true);
            expv1002.AddField("UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            expv1002.AddField("EXTDCOST", "Extended cost", Connector.FieldTypeIdCurrency);
            expv1002.AddField("RTSEQNUM_I", "Routing sequence", Connector.FieldTypeIdString);
            var moStatus = expv1002.AddField("MANUFACTUREORDERST_I", "Manufacturing order status", Connector.FieldTypeIdEnum);
            moStatus.AddListItems(1, new List<string> { "Quote/estimate", "Open", "Released", "Hold", "Canceled", "Complete", "Partially received", "Closed" });
        }
                
        public ConnectorEntity GetPicklistEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ManufacturingSmartListPicklists), "Picklists", ParentConnector);
            var expv1003 = entity.AddTable("EXPV1003");
            AddPicklistEntityFields(expv1003);
            return entity;
        }
        public void AddPicklistEntityFields(ConnectorTable expv1003)
        {
            expv1003.AddField("EXPV1003.MANUFACTUREORDER_I", "Manufacturing order", Connector.FieldTypeIdString, true);
            var moStatus = expv1003.AddField("EXPV1003.MANUFACTUREORDERST_I", "Manufacturing order status", Connector.FieldTypeIdEnum, true);
            moStatus.AddListItems(1, new List<string> { "Quote/estimate", "Open", "Released", "Hold", "Canceled", "Complete", "Partially received", "Closed" });
            expv1003.AddField("EXPV1003.PPN_I", "Parent item number", Connector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.POSITION_NUMBER", "Position number", Connector.FieldTypeIdInteger, true);
            expv1003.AddField("EXPV1003.ITEMNMBR", "Component item number", Connector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.ITEMDESC", "Component item description", Connector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.MRPAMOUNT_I", "Required quantity", Connector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.QTYREMAI", "Quantity remaining to allocate/issue", Connector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.ATYALLOC", "Quantity allocated", Connector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.QTY_ISSUED_I", "Quantity issued", Connector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.QTY_BACKFLUSHED_I", "Quantity backflushed", Connector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.NUMBERSCRAPPED_I", "Quantity scrapped", Connector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.REQdate", "Required date", Connector.FieldTypeIdDate, true);
            expv1003.AddField("EXPV1003.UOFM", "U of M", Connector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.LOCNCODE", "Issue from", Connector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.WCID_I", "Issue to", Connector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.BACKFLUSHITEM_I", "Backflush item", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.ALTERNATE_I", "Alternate", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.ALTERNATEPARTFOR_I", "Alternate for", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ALLOCATED_I", "Allocated", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.ALLOCATEUID_I", "Allocated by", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ALLOCATEDATEI", "Allocated date", Connector.FieldTypeIdDate);
            expv1003.AddField("EXPV1003.ACTUAL_CONSUMED_CHECK_I", "Actual consumed check", Connector.FieldTypeIdYesNo);
            var bomType = expv1003.AddField("EXPV1003.BOMCAT_I", "BOM type", Connector.FieldTypeIdEnum);
            bomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });
            expv1003.AddField("EXPV1003.BOMNAME_I", "BOM name", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ISSUED_I", "Issued", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.BOMSEQ_I", "BOM position number", Connector.FieldTypeIdInteger);
            expv1003.AddField("EXPV1003.MRPISSUEDATE_I", "Issue date", Connector.FieldTypeIdDate);
            expv1003.AddField("EXPV1003.ISSUEDUID_I", "Issued by", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ROUTINGNAME_I", "Routing name", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.RTSEQNUM_I", "Routing sequence", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.BOMSINGLELOT_I", "Single lot", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.SUBASSEMBLY_I", "Subassembly", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.SUBASSEMBLYOF_I", "Subassembly of", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.UNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            expv1003.AddField("EXPV1003.PHANTOM_I", "Phantom", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.SEQ_I", "Picklist sequence", Connector.FieldTypeIdInteger);
            expv1003.AddField("EXPV1003.LINK_TO_SEQUENCE_I", "Link to sequence", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.LEADTIMEOFFSET_I", "Lead time offset", Connector.FieldTypeIdInteger);
            var offsetFrom = expv1003.AddField("EXPV1003.OFFSET_FROM_I", "Offset from", Connector.FieldTypeIdEnum);
            offsetFrom.AddListItems(1, new List<string> { "Manufacturing order start date", "Manufacturing order due date" });
            expv1003.AddField("EXPV1003.FLOORSTOCK_I", "Floor stock", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.FIXED_QTY_I", "Fixed quantity", Connector.FieldTypeIdQuantity);
            expv1003.AddField("EXPV1003.BOMENGAPPROVAL_I", "Eng. approval required", Connector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            expv1003.AddField("EXPV1003.USERID", "Changed by", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.BOMUSERDEF1_I", "User defined 1", Connector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.BOMUSERDEF2_I", "User defined 2", Connector.FieldTypeIdString);
        }

    }
}
