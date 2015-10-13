using System.Collections.Generic;
using eOne.Common.DataConnectors;

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

        public DataConnectorEntity GetSoMoLinkEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListSoMoLink), "SO/MO link", ParentConnector);
            
            var is010001 = entity.AddTable("IS010001");
            
            AddSoMoLinkEntityFields(is010001);
            
            return entity;
        }
        public void AddSoMoLinkEntityFields(DataConnectorTable is010001)
        {
            is010001.AddField("SOPNUMBE", "Sales Order Number", DataConnector.FieldTypeIdString, true);
            is010001.AddField("MANUFACTUREORDER_I", "Manufacturing Order Number", DataConnector.FieldTypeIdString, true);
            is010001.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            is010001.AddField("SOITEMDUEDATE_I", "SO Item Due Date", DataConnector.FieldTypeIdDate, true);
            is010001.AddField("SOITEMPROMISEDATE_I", "SO Item Promise Date", DataConnector.FieldTypeIdDate, true);
            var sopType = is010001.AddField("SOPTYPE", "SOP Type", DataConnector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            is010001.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            is010001.AddField("CMPNTSEQ", "Component Sequence", DataConnector.FieldTypeIdInteger);
            is010001.AddField("CUSTOMERPARTNUMBER_I", "Customer Part Number", DataConnector.FieldTypeIdString);
            is010001.AddField("SOCHANGEDATE_I", "SO Change Date", DataConnector.FieldTypeIdDate);
            is010001.AddField("REVISIONLEVEL_I", "Revision Level", DataConnector.FieldTypeIdString);
            is010001.AddField("MFGNOTEINDEX_I", "MFG Note Index", DataConnector.FieldTypeIdInteger);
            is010001.AddField("MRKDNAMT", "Markdown Amount", DataConnector.FieldTypeIdCurrency);
            is010001.AddField("Markdown_Amount_Addition", "Markdown Amount Additional", DataConnector.FieldTypeIdCurrency);
            is010001.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            is010001.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
        }
        
        public DataConnectorEntity GetWorkInProcessEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListWorkInProcess), "Work in process", ParentConnector);
            
            var mop1000 = entity.AddTable("MOP1000");
            
            AddWorkInProcessEntityFields(mop1000);
            
            return entity;
        }
        public void AddWorkInProcessEntityFields(DataConnectorTable mop1000)
        {
            mop1000.AddField("MANUFACTUREORDER_I", "MO Number", DataConnector.FieldTypeIdString, true);
            mop1000.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            mop1000.AddField("TO_SITE_I", "Issue To Site ID", DataConnector.FieldTypeIdString, true);
            mop1000.AddField("DATERECD", "WIP In Date", DataConnector.FieldTypeIdDate, true);
            mop1000.AddField("QTYRECVD", "WIP Qty In", DataConnector.FieldTypeIdQuantity, true);
            mop1000.AddField("QTYSOLD", "WIP Qty Out", DataConnector.FieldTypeIdQuantity, true);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_10", "Unit Cost", DataConnector.FieldTypeIdCurrency, true);
            mop1000.AddField("WIPSEQNMBR", "WIP Sequence Number", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("PICKLISTSEQ", "Picklist Sequence Number", DataConnector.FieldTypeIdInteger);
            mop1000.AddField("PICKNUMBER", "Picking Document Number", DataConnector.FieldTypeIdString);
            mop1000.AddField("PICKDOCLINENUM", "Picking Document Line Number", DataConnector.FieldTypeIdInteger);
            mop1000.AddField("WIPQTYSOLD", "All WIP Used", DataConnector.FieldTypeIdYesNo);
            mop1000.AddField("IVDOCNBR", "IV Document Number", DataConnector.FieldTypeIdString);
            mop1000.AddField("RCTSEQNM", "Receipt Sequence Number", DataConnector.FieldTypeIdInteger);
            mop1000.AddField("RTSEQNUM_I", "Routing Sequence Number", DataConnector.FieldTypeIdString);
            mop1000.AddField("FROM_SITE_I", "Issue From Site ID", DataConnector.FieldTypeIdString);
            mop1000.AddField("DTRCVDINVNTRY", "Date Received In Inventory", DataConnector.FieldTypeIdDate);
            mop1000.AddField("BACKFLUSHITEM_I", "Backflushed", DataConnector.FieldTypeIdYesNo);
            mop1000.AddField("NUMBERSCRAPPED_I", "Scrapped Qty", DataConnector.FieldTypeIdQuantity);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_1", "Material Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_2", "Material Fixed Overhead Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_3", "Material Variable Overhead Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_4", "Labor Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_5", "Labor Fixed Overhead Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_6", "Labor Variable Overhead Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_7", "Machine Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_8", "Machine Fixed Overhead Unit Cost", DataConnector.FieldTypeIdCurrency);
            mop1000.AddField("ITEM_COSTS_ARRAY_I_9", "Machine Variable Overhead Unit Cost", DataConnector.FieldTypeIdCurrency);

            var itemTrackingOption = mop1000.AddField("ITMTRKOP", "Item Tracking Option", DataConnector.FieldTypeIdEnum);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Serial Numbers", "Lot Numbers" });
        }
        
        public DataConnectorEntity GetSalesDocumentsNotLinkedToManufacturingEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListSalesDocumentsNotLinkedToManufacturing), "Sales documents not linked to manufacturing", ParentConnector);
            
            var expv1000 = entity.AddTable("EXPV1000");
            
            AddSalesDocumentsNotLinkedToManufacturingEntityFields(expv1000);
            
            entity.AddCalculation("case EXPV1000.CUSTPRIORITY when 1 then 'None' else cast(EXPV1000.CUSTPRIORITY - 1 as varchar(3)) end", "Customer Priority", DataConnector.FieldTypeIdString);
            
            return entity;
        }
        public void AddSalesDocumentsNotLinkedToManufacturingEntityFields(DataConnectorTable expv1000)
        {
            var sopType = expv1000.AddField("SOPTYPE", "SOP Type", DataConnector.FieldTypeIdEnum, true);
            sopType.AddListItems(1, new List<string> { "Quote", "Order", "Invoice", "Return", "Back Order", "Fulfillment Order" });
            expv1000.AddField("SOPNUMBE", "SOP Number", DataConnector.FieldTypeIdString, true);
            expv1000.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            expv1000.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            expv1000.AddField("QUANTITY", "QTY", DataConnector.FieldTypeIdQuantity, true);
            expv1000.AddField("QTYBACKO", "Qty to Back Order", DataConnector.FieldTypeIdQuantity, true);
            expv1000.AddField("ReqShipDate", "Requested Ship Date", DataConnector.FieldTypeIdDate, true);
            var itemFulfillmentMethod = expv1000.AddField("ITEMFULFILLMETHOD", "Item Fulfillment Method", DataConnector.FieldTypeIdEnum, true);
            itemFulfillmentMethod.AddListItems(1, new List<string> { "Make to Stock", "Make to Order - Manual", "Make to Order - Silent" });
            expv1000.AddField("UOFM", "U of M", DataConnector.FieldTypeIdString, true);
            expv1000.AddField("CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);
            expv1000.AddField("AVERAGEORDQTY_I", "Average Order QTY", DataConnector.FieldTypeIdQuantity);
            expv1000.AddField("EFFECTIVEDATE_I", "Effective Date", DataConnector.FieldTypeIdDate);
            expv1000.AddField("LNITMSEQ", "Line Item Sequence", DataConnector.FieldTypeIdInteger);
            expv1000.AddField("CMPNTSEQ", "Component Sequence", DataConnector.FieldTypeIdInteger);
            expv1000.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString);
            expv1000.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString);

            var makeBuyCode = expv1000.AddField("MAKEBUYCODE_I", "Make/Buy Code", DataConnector.FieldTypeIdEnum);
            makeBuyCode.AddListItems(1, new List<string> { "Make", "Either", "Buy" });

            var itemStatus = expv1000.AddField("ITEMSTATUS_I", "Item Status", DataConnector.FieldTypeIdEnum);
            itemStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Service", "Obsolete", "Prerelease", "Released" });
        }
        
        public DataConnectorEntity GetBillOfMaterialEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListBillOfMaterials), "Bill of materials", ParentConnector);
            
            var expv1001 = entity.AddTable("EXPV1001");
            
            AddBillOfMaterialEntityFields(expv1001);
            
            return entity;
        }
        public void AddBillOfMaterialEntityFields(DataConnectorTable expv1001)
        {
            expv1001.AddField("PPN_I", "Finished Good Item Number", DataConnector.FieldTypeIdString, true);
            var bomType = expv1001.AddField("BOMCAT_I", "BOM Type", DataConnector.FieldTypeIdEnum, true);
            bomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });
            var bomCategory = expv1001.AddField("BOMTYPE_I", "BOM Category", DataConnector.FieldTypeIdEnum, true);
            bomCategory.AddListItems(1, new List<string> { "Regular", "Modular", "Phantom", "Option" });
            expv1001.AddField("BOMNAME_I", "BOM Name", DataConnector.FieldTypeIdString, true);
            expv1001.AddField("BOM_REVISION_LEVEL", "Revision Level", DataConnector.FieldTypeIdString, true);
            expv1001.AddField("BOMSEQ_I", "BOM Position Number", DataConnector.FieldTypeIdInteger, true);
            expv1001.AddField("CPN_I", "Component", DataConnector.FieldTypeIdString, true);
            var componentBomType = expv1001.AddField("SUBCAT_I", "Component BOM Type", DataConnector.FieldTypeIdEnum, true);
            componentBomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });
            expv1001.AddField("SUBNAME_I", "Component BOM Name", DataConnector.FieldTypeIdString, true);
            expv1001.AddField("QUANTITY_I", "QTY", DataConnector.FieldTypeIdQuantity, true);
            expv1001.AddField("UOFM", "U of M", DataConnector.FieldTypeIdString, true);
            expv1001.AddField("FIXED_QTY_I", "Fixed QTY", DataConnector.FieldTypeIdQuantity, true);
            expv1001.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency, true);
            expv1001.AddField("LOCNCODE", "Issue From", DataConnector.FieldTypeIdString, true);
            expv1001.AddField("WCID_I", "Issue To", DataConnector.FieldTypeIdString, true);
            expv1001.AddField("FLOORSTOCK_I", "Floor Stock", DataConnector.FieldTypeIdYesNo, true);
            expv1001.AddField("BACKFLUSHITEM_I", "Backflush Item", DataConnector.FieldTypeIdYesNo, true);
            expv1001.AddField("ALTERNATE_I", "Alternate", DataConnector.FieldTypeIdYesNo, true);
            expv1001.AddField("ALTERNATEPARTFOR_I", "Alternate For", DataConnector.FieldTypeIdString, true);
            expv1001.AddField("EFFECTIVEDATE_I", "Effective Date", DataConnector.FieldTypeIdDate, true);
            expv1001.AddField("EFFECTIVEINDATE_I", "Effective In Date", DataConnector.FieldTypeIdDate);
            expv1001.AddField("EFFECTIVEOUTDATE_I", "Effective Out Date", DataConnector.FieldTypeIdDate);
            expv1001.AddField("LEADTIMEOFFSET_I", "Lead Time Offset", DataConnector.FieldTypeIdInteger);
            expv1001.AddField("OPTPERCENT_I", "Option Percent", DataConnector.FieldTypeIdPercentage);
            expv1001.AddField("SCRAPPERCENT_I", "Scrap Percentage", DataConnector.FieldTypeIdPercentage);
            expv1001.AddField("BOMSINGLELOT_I", "Single Lot", DataConnector.FieldTypeIdYesNo);
            expv1001.AddField("BOMENGAPPROVAL_I", "Eng. Approval Req'd", DataConnector.FieldTypeIdYesNo);
            expv1001.AddField("ACTUAL_CONSUMED_CHECK_I", "Actual Consumed Check", DataConnector.FieldTypeIdYesNo);
            expv1001.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            expv1001.AddField("CHANGEBY_I", "Changed By", DataConnector.FieldTypeIdString);
            expv1001.AddField("MODIFDT", "Revision Change Date", DataConnector.FieldTypeIdDate);
            expv1001.AddField("USERID", "Revision Changed By", DataConnector.FieldTypeIdString);
            expv1001.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            expv1001.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);

            var offsetFrom = expv1001.AddField("OFFSET_FROM_I", "Offset From", DataConnector.FieldTypeIdEnum);
            offsetFrom.AddListItems(1, new List<string> { "MO Start Date", "MO Due Date" });
        }
        
        public DataConnectorEntity GetManufacturingOrderEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListManufacturingOrders), "Manufacturing orders", ParentConnector);
            var wo010032 = entity.AddTable("WO010032");
            var iv00101 = entity.AddTable("IV00101", "WO010032");
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");
            AddManufacturingOrderEntityFields(wo010032, iv00101);
            return entity;
        }
        public void AddManufacturingOrderEntityFields(DataConnectorTable wo010032, DataConnectorTable iv00101)
        {
            wo010032.AddField("MANUFACTUREORDER_I", "Manufacturing Order", DataConnector.FieldTypeIdString, true);
            wo010032.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            iv00101.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            var moStatus = wo010032.AddField("MANUFACTUREORDERST_I", "MO Status", DataConnector.FieldTypeIdEnum, true);
            moStatus.AddListItems(1, new List<string> { "Quote/Estimate", "Open", "Released", "Hold", "Canceled", "Complete", "Partially Received", "Closed" });
            wo010032.AddField("STARTQTY_I", "Start QTY", DataConnector.FieldTypeIdQuantity, true);
            wo010032.AddField("ENDQTY_I", "End QTY", DataConnector.FieldTypeIdQuantity, true);
            wo010032.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate, true);
            wo010032.AddField("ENDDATE", "Due Date", DataConnector.FieldTypeIdDate, true);
            wo010032.AddField("POSTTOSITE_I", "Post To Site", DataConnector.FieldTypeIdString, true);
            wo010032.AddField("ACTUALDEMAND_I", "Actual Demand", DataConnector.FieldTypeIdCurrency);
            wo010032.AddField("BOMNAME_I", "BOM Name", DataConnector.FieldTypeIdString);
            wo010032.AddField("DSCRIPTN", "MO Description", DataConnector.FieldTypeIdString);
            wo010032.AddField("DRAWFROMSITE_I", "Draw From Site", DataConnector.FieldTypeIdString);
            wo010032.AddField("LOTNUMBR", "Lot Number", DataConnector.FieldTypeIdString);
            wo010032.AddField("SCHEDULINGPREFEREN_I", "Scheduling Preference", DataConnector.FieldTypeIdString);
            wo010032.AddField("OUTSOURCED_I + 1", "Outsourced", DataConnector.FieldTypeIdYesNo);
            wo010032.AddField("PROJEMPLOYEEHRSSUM_I", "Projected Employee Hours", DataConnector.FieldTypeIdCurrency);
            wo010032.AddField("PROJMACHINEHRSSUM_I", "Projected Machine Hours", DataConnector.FieldTypeIdCurrency);
            wo010032.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);

            var bomType = wo010032.AddField("BOMCAT_I", "BOM Type", DataConnector.FieldTypeIdEnum);
            bomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });

            var moPriority = wo010032.AddField("MANUFACTUREORDPRI_I", "MO Priority", DataConnector.FieldTypeIdEnum);
            moPriority.AddListItems(1, new List<string> { "High", "Medium", "Low" });

            var schedulingMethod = wo010032.AddField("SCHEDULEMETHOD_I", "Scheduling Method", DataConnector.FieldTypeIdEnum);
            schedulingMethod.AddListItems(1, new List<string> { "Forward Infinite", "Backward Infinite" });
        }
        
        public DataConnectorEntity GetRecordedOutsourcingShipmentEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListRecordedOutsourcingShipments), "Recorded outsourcing shipments", ParentConnector);
            var osrc1300 = entity.AddTable("OSRC1300");
            AddRecordedOutsourcingShipmentFields(osrc1300);
            return entity;
        }
        public void AddRecordedOutsourcingShipmentFields(DataConnectorTable osrc1300)
        {
            osrc1300.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            osrc1300.AddField("QTYSHPPD", "QTY Shipped", DataConnector.FieldTypeIdQuantity, true);
            osrc1300.AddField("UOFM", "U of M", DataConnector.FieldTypeIdString, true);
            osrc1300.AddField("Ship_Date", "Ship Date", DataConnector.FieldTypeIdDate, true);
            osrc1300.AddField("RETUDATE", "Return Date", DataConnector.FieldTypeIdDate, true);
            osrc1300.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            osrc1300.AddField("MANUFACTUREORDER_I", "Manufacturing Order", DataConnector.FieldTypeIdString, true);
            osrc1300.AddField("RTSEQNUM_I", "Routing Sequence", DataConnector.FieldTypeIdString, true);
            var itemTrackingOption = osrc1300.AddField("OSRC_Item_Type", "Item Tracking Option", DataConnector.FieldTypeIdEnum, true);
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Lot Tracked", "Serial Tracked", "WIP Material" });
            osrc1300.AddField("VOIDED", "Voided", DataConnector.FieldTypeIdYesNo, true);
            osrc1300.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString, true);
            osrc1300.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString);
            osrc1300.AddField("VOIDDATE", "Voided Date", DataConnector.FieldTypeIdDate);
            osrc1300.AddField("VOIDEDBY", "Voided By", DataConnector.FieldTypeIdString);
            osrc1300.AddField("Ship_By_Date", "Ship By Date", DataConnector.FieldTypeIdDate);
            osrc1300.AddField("USERID", "Shipped By", DataConnector.FieldTypeIdString);
            osrc1300.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
        }
        
        public DataConnectorEntity GetSuggestedOutsourcingShipmentEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListSuggestedOutsourcingShipments), "Suggested outsourcing shipments", ParentConnector);
            var osrc1200 = entity.AddTable("OSRC1200");
            var iv00101 = entity.AddTable("IV00101", "OSRC1200");
            iv00101.AddJoinFields("ITEMNMBR", "ITEMNMBR");
            var iv40201 = entity.AddTable("IV40201", "IV00101");
            iv40201.AddJoinFields("UOMSCHDL", "UOMSCHDL");
            AddSuggestedOutsourcingShipmentEntityFields(osrc1200, iv00101, iv40201);
            return entity;
        }
        public void AddSuggestedOutsourcingShipmentEntityFields(DataConnectorTable osrc1200, DataConnectorTable iv00101, DataConnectorTable iv40201)
        {
            osrc1200.AddField("ITEMNMBR", "Item Number", DataConnector.FieldTypeIdString, true);
            iv00101.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            osrc1200.AddField("Total_Suggested_QTY", "Total Suggested QTY", DataConnector.FieldTypeIdQuantity, true);
            osrc1200.AddField("QTYSHPPD", "QTY Shipped", DataConnector.FieldTypeIdQuantity, true);
            iv40201.AddField("BASEUOFM", "U of M", DataConnector.FieldTypeIdString, true);
            osrc1200.AddField("Ship_By_Date", "Ship By Date", DataConnector.FieldTypeIdDate, true);
            osrc1200.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            osrc1200.AddField("MANUFACTUREORDER_I", "Manufacturing Order", DataConnector.FieldTypeIdString, true);
            osrc1200.AddField("RTSEQNUM_I", "Routing Sequence", DataConnector.FieldTypeIdString, true);
            var itemTrackingOption = osrc1200.AddField("OSRC_Item_Type", "Item Tracking Option", DataConnector.FieldTypeIdEnum, true); 
            itemTrackingOption.AddListItems(1, new List<string> { "None", "Lot Tracked", "Serial Tracked", "WIP Material" });
            osrc1200.AddField("SEQ_I", "Picklist Sequence", DataConnector.FieldTypeIdInteger);
        }
        
        public DataConnectorEntity GetSuggestedOutsourcingPurchaseOrderEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListSuggestedOutsourcingPurchaseOrders), "Suggested outsourcing purchase orders", ParentConnector);
            var expv1002 = entity.AddTable("EXPV1002");
            AddSuggestedOutsourcingPurchaseOrderEntityFields(expv1002);
            return entity;
        }
        public void AddSuggestedOutsourcingPurchaseOrderEntityFields(DataConnectorTable expv1002)
        {
            expv1002.AddField("MANUFACTUREORDER_I", "Manufacturing Order", DataConnector.FieldTypeIdString, true);
            expv1002.AddField("ITEMNMBR", "Item Number to Purchase", DataConnector.FieldTypeIdString, true);
            expv1002.AddField("ITEMDESC", "Item Description", DataConnector.FieldTypeIdString, true);
            expv1002.AddField("VENDORID", "Vendor ID", DataConnector.FieldTypeIdString, true);
            expv1002.AddField("RELEASEBYDATE", "Release By Date", DataConnector.FieldTypeIdDate, true);
            expv1002.AddField("REQDATE", "Required Date", DataConnector.FieldTypeIdDate, true);
            expv1002.AddField("QTYRECVD", "Received QTY", DataConnector.FieldTypeIdQuantity, true);
            expv1002.AddField("Total_Suggested_QTY", "Total Suggested QTY", DataConnector.FieldTypeIdQuantity, true);
            expv1002.AddField("QTYORDER", "QTY Ordered", DataConnector.FieldTypeIdQuantity, true);
            expv1002.AddField("QTYTORDR", "QTY Remaining to Order", DataConnector.FieldTypeIdQuantity, true);
            expv1002.AddField("UOFM", "U of M", DataConnector.FieldTypeIdString, true);
            expv1002.AddField("UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            expv1002.AddField("EXTDCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency);
            expv1002.AddField("RTSEQNUM_I", "Routing Sequence", DataConnector.FieldTypeIdString);
            var moStatus = expv1002.AddField("MANUFACTUREORDERST_I", "MO Status", DataConnector.FieldTypeIdEnum);
            moStatus.AddListItems(1, new List<string> { "Quote/Estimate", "Open", "Released", "Hold", "Canceled", "Complete", "Partially Received", "Closed" });
        }
                
        public DataConnectorEntity GetPicklistEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ManufacturingSmartListPicklists), "Picklists", ParentConnector);
            var expv1003 = entity.AddTable("EXPV1003");
            AddPicklistEntityFields(expv1003);
            return entity;
        }
        public void AddPicklistEntityFields(DataConnectorTable expv1003)
        {
            expv1003.AddField("EXPV1003.MANUFACTUREORDER_I", "Manufacturing Order", DataConnector.FieldTypeIdString, true);
            var moStatus = expv1003.AddField("EXPV1003.MANUFACTUREORDERST_I", "MO Status", DataConnector.FieldTypeIdEnum, true);
            moStatus.AddListItems(1, new List<string> { "Quote/Estimate", "Open", "Released", "Hold", "Canceled", "Complete", "Partially Received", "Closed" });
            expv1003.AddField("EXPV1003.PPN_I", "Parent Item Number", DataConnector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.POSITION_NUMBER", "Position Number", DataConnector.FieldTypeIdInteger, true);
            expv1003.AddField("EXPV1003.ITEMNMBR", "Component Item Number", DataConnector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.ITEMDESC", "Component Item Description", DataConnector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.MRPAMOUNT_I", "Required QTY", DataConnector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.QTYREMAI", "QTY Remaining To Allocate/Issue", DataConnector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.ATYALLOC", "QTY Allocated", DataConnector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.QTY_ISSUED_I", "QTY Issued", DataConnector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.QTY_BACKFLUSHED_I", "QTY Backflushed", DataConnector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.NUMBERSCRAPPED_I", "QTY Scrapped", DataConnector.FieldTypeIdQuantity, true);
            expv1003.AddField("EXPV1003.REQDATE", "Required Date", DataConnector.FieldTypeIdDate, true);
            expv1003.AddField("EXPV1003.UOFM", "U of M", DataConnector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.LOCNCODE", "Issue From", DataConnector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.WCID_I", "Issue To", DataConnector.FieldTypeIdString, true);
            expv1003.AddField("EXPV1003.BACKFLUSHITEM_I", "Backflush Item", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.ALTERNATE_I", "Alternate", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.ALTERNATEPARTFOR_I", "Alternate For", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ALLOCATED_I", "Allocated", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.ALLOCATEUID_I", "Allocated By", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ALLOCATEDATEI", "Allocated Date", DataConnector.FieldTypeIdDate);
            expv1003.AddField("EXPV1003.ACTUAL_CONSUMED_CHECK_I", "Actual Consumed Check", DataConnector.FieldTypeIdYesNo);
            var bomType = expv1003.AddField("EXPV1003.BOMCAT_I", "BOM Type", DataConnector.FieldTypeIdEnum);
            bomType.AddListItems(1, new List<string> { "MFG BOM", "ENG. BOM", "ARCH. BOM", "CONFIG. BOM", "SUPER BOM" });
            expv1003.AddField("EXPV1003.BOMNAME_I", "BOM Name", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ISSUED_I", "Issued", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.BOMSEQ_I", "BOM Position Number", DataConnector.FieldTypeIdInteger);
            expv1003.AddField("EXPV1003.MRPISSUEDATE_I", "Issue Date", DataConnector.FieldTypeIdDate);
            expv1003.AddField("EXPV1003.ISSUEDUID_I", "Issued By", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.ROUTINGNAME_I", "Routing Name", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.RTSEQNUM_I", "Routing Sequence", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.BOMSINGLELOT_I", "Single Lot", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.SUBASSEMBLY_I", "Subassembly", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.SUBASSEMBLYOF_I", "Subassembly Of", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.UNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            expv1003.AddField("EXPV1003.PHANTOM_I", "Phantom", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.SEQ_I", "Picklist Sequence", DataConnector.FieldTypeIdInteger);
            expv1003.AddField("EXPV1003.LINK_TO_SEQUENCE_I", "Link To Sequence", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.LEADTIMEOFFSET_I", "Lead Time Offset", DataConnector.FieldTypeIdInteger);
            var offsetFrom = expv1003.AddField("EXPV1003.OFFSET_FROM_I", "Offset From", DataConnector.FieldTypeIdEnum);
            offsetFrom.AddListItems(1, new List<string> { "MO Start Date", "MO Due Date" });
            expv1003.AddField("EXPV1003.FLOORSTOCK_I", "Floor Stock", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.FIXED_QTY_I", "Fixed QTY", DataConnector.FieldTypeIdQuantity);
            expv1003.AddField("EXPV1003.BOMENGAPPROVAL_I", "Eng. Approval Req'd", DataConnector.FieldTypeIdYesNo);
            expv1003.AddField("EXPV1003.CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            expv1003.AddField("EXPV1003.USERID", "Changed By", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.BOMUSERDEF1_I", "User Defined 1", DataConnector.FieldTypeIdString);
            expv1003.AddField("EXPV1003.BOMUSERDEF2_I", "User Defined 2", DataConnector.FieldTypeIdString);
        }

    }
}
