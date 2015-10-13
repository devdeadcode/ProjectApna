using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class ProjectAccountingModule : DynamicsGpModule
    {

        private const short ProjectSmartListContracts = 1;
        private const short ProjectSmartListProjects = 2;
        private const short ProjectSmartListCostCategories = 3;
        private const short ProjectSmartListEquipment = 4;
        private const short ProjectSmartListMiscellaneous = 5;
        private const short ProjectSmartListBillingCycle = 6;
        private const short ProjectSmartListEmployeeExpenseTrx = 7;
        private const short ProjectSmartListEquipmentLogTrx = 8;
        private const short ProjectSmartListIvTransferTrx = 9;
        private const short ProjectSmartListMiscLogTrx = 10;
        private const short ProjectSmartListTimesheetTrx = 11;
        private const short ProjectSmartListMiscLogHistTrx = 12;
        private const short ProjectSmartListEquipmentLogHistTrx = 13;
        private const short ProjectSmartListTimesheetHistTrx = 14;
        private const short ProjectSmartListIvTransferHistTrx = 15;
        private const short ProjectSmartListEmployeeExpenseHistTrx = 16;
        private const short ProjectSmartListBillingHistTrx = 17;
        private const short ProjectSmartListBillingWorkTrx = 18;
        private const short ProjectSmartListBillingOpenTrx = 23;

        public ProjectAccountingModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 258;
            Name = "Project Accounting";
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetContractEntity());
            Entities.Add(GetProjectEntity());
            Entities.Add(GetCostCategoryEntity());
            Entities.Add(GetEquipmentEntity());
            Entities.Add(GetMiscellaneousEntity());
            Entities.Add(GetBillingCycleEntity());
            Entities.Add(GetEmployeeExpenseTransactionEntity());
            Entities.Add(GetEquipmentLogTransactionEntity());
            Entities.Add(GetIvTransferTransactionEntity());
            Entities.Add(GetMiscLogTransactionEntity());
            Entities.Add(GetTimesheetTransactionEntity());
            Entities.Add(GetMiscLogHistTransactionEntity());
            Entities.Add(GetEquipmentLogHistTransactionEntity());
            Entities.Add(GetTimesheetHistTransactionEntity());
            Entities.Add(GetIvTransferHistTransactionEntity());
            Entities.Add(GetEmployeeExpenseHistTransactionEntity());
            Entities.Add(GetBillingHistTransactionEntity());
            Entities.Add(GetBillingWorkTransactionEntity());
            Entities.Add(GetBillingOpenTransactionEntity());
        }

        public DataConnectorEntity GetContractEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListContracts), "Contracts", ParentConnector);
            
            var pa01101 = entity.AddTable("PA01101");
            
            AddContractEntityFields(pa01101);
            
            return entity;
        }
        public void AddContractEntityFields(DataConnectorTable pa01101)
        {
            pa01101.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            pa01101.AddField("PAcontid", "Contract ID", DataConnector.FieldTypeIdString, true);
            pa01101.AddField("PAcontname", "PA Contract Name", DataConnector.FieldTypeIdString, true);
            pa01101.AddField("PACONTNUMBER", "PA Contract Number", DataConnector.FieldTypeIdString, true);
            pa01101.AddField("PAcontclassid", "PA Contract Class ID", DataConnector.FieldTypeIdString);
            pa01101.AddField("PApurordnum", "PA Purchase Order No.", DataConnector.FieldTypeIdString);
            pa01101.AddField("PABBeginDate", "PA Baseline Begin Date", DataConnector.FieldTypeIdDate);
            pa01101.AddField("PABEndDate", "PA Baseline End Date", DataConnector.FieldTypeIdDate);
            pa01101.AddField("PAFBeginDate", "PA Forecast Begin Date", DataConnector.FieldTypeIdDate);
            pa01101.AddField("PAFEndDate", "PA Forecast End Date", DataConnector.FieldTypeIdDate);
            pa01101.AddField("PAACTUALBEGDATE", "PA Actual Begin Date", DataConnector.FieldTypeIdDate);
            pa01101.AddField("PA_Actual_End_Date", "PA Actual End Date", DataConnector.FieldTypeIdDate);
            pa01101.AddField("PAcloseProjcosts", "PA Close to Project Costs", DataConnector.FieldTypeIdYesNo);
            pa01101.AddField("PAclosetobillings", "PA Close to Billings", DataConnector.FieldTypeIdYesNo);
            pa01101.AddField("PAContMgrID", "PA Contract Manager ID", DataConnector.FieldTypeIdString);
            pa01101.AddField("PABusMgrID", "PA Business Manager ID", DataConnector.FieldTypeIdString);
            pa01101.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            pa01101.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            pa01101.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            pa01101.AddField("COMPRCNT", "Commission Percent", DataConnector.FieldTypeIdPercentage);
            pa01101.AddField("PABILLFORMAT", "PA Bill Format", DataConnector.FieldTypeIdString);
            pa01101.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            pa01101.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            pa01101.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            pa01101.AddField("PAUD1_Cont", "User Defined 1", DataConnector.FieldTypeIdString);
            pa01101.AddField("PAUD2_Cont", "User Defined 2", DataConnector.FieldTypeIdString);
            pa01101.AddField("PABQuantity", "Baseline Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PABTotalCost", "Baseline Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABBillings", "Baseline Billings", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABProfit", "Baseline Profit", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABTaxPaidAmt", "Baseline Tax Paid Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABTaxChargedAmt", "Baseline Tax Charged Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABaselineOvhdCost", "Baseline Overhead Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAFQuantity", "Forecast Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAFTotalCost", "Forecast Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAFBillings", "Forecast Billings", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAFProfit", "Forecast Profit", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAFTaxPaidAmt", "Forecast Tax Paid Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAFTaxChargedAmt", "Forecast Tax Charged Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAForecastOvhdCost", "Forecast Overhead Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAProject_Amount", "Contract Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedQty", "Unposted Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAUnpostedTotalCostN", "Unposted Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Overhead", "Unposted Overhead", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedProfitN", "Unposted Profit", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Tax_Amount", "Unposted Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostAccrRevN", "Unposted Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedCommitedQty", "Unposted Committed Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAUnpostedCommitedCost", "Unposted Committed Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedCommitedTaxAmt", "Unposted Committed Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedRecogRevN", "Unposted Recognized Revenue", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Project_Fee", "Unposted Project Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Retainer_Fee", "Unposted Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Service_Fee", "Unposted Service Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUNPOSTRETAMT", "Unposted Retention Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUNPOSTBIEEAMOUNT", "Unposted BIEE Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUNPEIEBAMOUNT", "Unposted EIEB Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Unposted_Billed_Reten", "Unposted Billed Retention", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedQty", "Actual Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAPostedTotalCostN", "Actual Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Overhead", "Actual Overhead", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedProfitN", "Actual Profit", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Tax_Amount", "Actual Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Accr_RevN", "Actual Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedCommitedQty", "Actual Committed Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAPostedCommitedCost", "Actual Committed Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedCommitedTaxAmt", "Actual Committed Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostRecogRevN", "Actual Recognized Revenue", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Project_Fee", "Actual Project Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Retainer_Fee", "Actual Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Service_Fee", "Actual Service Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOSTRETAMT", "Actual Retention Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOSBIEEAMOUNT", "Actual BIEE Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOSEIEBAMOUNT", "Actual EIEB Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Actual_Billed_Retenti", "Actual Billed Retention", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAWrite_UpDown_Amount", "Write Up/Down Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABilled_QtyN", "Billed Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PABilled_Cost", "Billed Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABilled_Accrued_Revenu", "Billed Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PACostPcntCompleted", "Cost Percent Completed", DataConnector.FieldTypeIdPercentage);
            pa01101.AddField("PAQuantityPcntCompleted", "Quantity Percent Completed", DataConnector.FieldTypeIdPercentage);
            pa01101.AddField("PA_Receipts_Amount", "Receipts Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Actual_Receipts_Amoun", "Actual Receipts Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Earnings", "Earnings", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Cost_of_Earnings", "Cost of Earnings", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostBillN", "Unposted Billings", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostDiscDolAmtN", "Unposted Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Sales_Tax_Am", "Unposted Sales Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedBillingsN", "Actual Billings", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedDiscDolAmtN", "Actual Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Sales_Tax_Amou", "Actual Sales Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAService_Fee_Amount", "Service Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PARetainer_Fee_Amount", "Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAProject_Fee_Amount", "Project Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PARetentionFeeAmount", "Retention Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("DSCPCTAM", "Discount Percent", DataConnector.FieldTypeIdPercentage);
            pa01101.AddField("PABCWPAMT", "BCWP Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PABCWSAMT", "BCWS Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAACWPAMT", "ACWP Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAApproved_Accrued_Reve", "Approved Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAApproved_Cost", "Approved Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAApproved_Quantity", "Approved Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("WROFAMNT", "Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("ActualWriteOffAmount", "Actual Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("ActualDiscTakenAmount", "Actual Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PACommitted_Costs", "Committed Costs", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PACommitted_Qty", "Committed Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAPOCost", "PO Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOQty", "PO Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAPOPostedCost", "PO Actual Cost", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedQty", "PO Actual Quantity", DataConnector.FieldTypeIdQuantity);
            pa01101.AddField("PAtaxpaidamt", "Tax Paid Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PApretainage", "Actual Retainage", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAunpretainage", "Unposted Retainage", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Write_Off_Tax_Amount", "Write Off Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualWOTaxAmt", "Actual Write Off Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Terms_Taken_Tax_Amt", "Terms Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualTermsTakenTax", "Actual Terms Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedLossAmount", "Unposted Loss Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualLossAmount", "Actual Loss Amount", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Earnings", "Actual Earnings", DataConnector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualCostofEarnings", "Actual Cost of Earnings", DataConnector.FieldTypeIdCurrency);

            var projectType = pa01101.AddField("PAProjectType", "Project Type", DataConnector.FieldTypeIdEnum);
            projectType.AddListItems(1, new List<string> { "Time and Materials", "Cost Plus", "Fixed Price" });

            var accountingMethod = pa01101.AddField("PAAcctgMethod", "Accounting Method", DataConnector.FieldTypeIdEnum);
            accountingMethod.AddListItems(1, new List<string> { "When Performed", "When Billed", "Cost-to-Cost", "Effort-Expended", "Completed", "Effort Expended - Labor Only" });

            var status = pa01101.AddField("PASTAT", "Status", DataConnector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Open", "On Hold", "Closed", "Estimate", "Completed" });
        }

        public DataConnectorEntity GetProjectEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListProjects), "Projects", ParentConnector);
            
            var pa01201 = entity.AddTable("PA01201");
            
            AddProjectEntityFields(pa01201);
            
            return entity;
        }
        public void AddProjectEntityFields(DataConnectorTable pa01201)
        {
            pa01201.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            pa01201.AddField("PACONTNUMBER", "Contract Number", DataConnector.FieldTypeIdString, true);
            pa01201.AddField("PAcontid", "Contract ID", DataConnector.FieldTypeIdString, true);
            pa01201.AddField("PAprojid", "Project ID", DataConnector.FieldTypeIdString, true);
            pa01201.AddField("PAprojname", "Project Name", DataConnector.FieldTypeIdString, true);
            pa01201.AddField("PAPROJNUMBER", "Project Number", DataConnector.FieldTypeIdString, true);
            pa01201.AddField("PAprjclsid", "Project Class ID", DataConnector.FieldTypeIdString);
            var projectType = pa01201.AddField("PAProjectType", "Project Type", DataConnector.FieldTypeIdEnum, true);
            projectType.AddListItems(1, new List<string> { "Time and Materials", "Cost Plus", "Fixed Price" });
            var status = pa01201.AddField("PASTAT", "Status", DataConnector.FieldTypeIdEnum, true);
            status.AddListItems(1, new List<string> { "Open", "On Hold", "Closed", "Estimate", "Completed" });
            pa01201.AddField("PApurordnum", "Purchase Order No.", DataConnector.FieldTypeIdString);
            pa01201.AddField("PABBeginDate", "Baseline Begin Date", DataConnector.FieldTypeIdDate);
            pa01201.AddField("PABEndDate", "Baseline End Date", DataConnector.FieldTypeIdDate);
            pa01201.AddField("PABQuantity", "Baseline Quantity", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PABTotalCost", "Baseline Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABProfit", "Baseline Profit", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABBillings", "Baseline Billings", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABTaxPaidAmt", "Baseline Tax Paid Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABTaxChargedAmt", "Baseline Tax Charged Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABaselineOvhdCost", "Baseline Overhead Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAACTUALBEGDATE", "Actual Begin Date", DataConnector.FieldTypeIdDate);
            pa01201.AddField("PA_Actual_End_Date", "Actual End Date", DataConnector.FieldTypeIdDate);
            pa01201.AddField("PAFBeginDate", "Forecast Begin Date", DataConnector.FieldTypeIdDate);
            pa01201.AddField("PAFEndDate", "Forecast End Date", DataConnector.FieldTypeIdDate);
            pa01201.AddField("PAFQuantity", "Forecast Quantity", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAFTotalCost", "Forecast Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAFProfit", "Forecast Profit", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAFBillings", "Forecast Billings", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAFTaxPaidAmt", "Forecast Tax Paid Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAFTaxChargedAmt", "Forecast Tax Charged Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAForecastOvhdCost", "Forecast Overhead Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAcloseProjcosts", "Close to Project Costs", DataConnector.FieldTypeIdYesNo);
            pa01201.AddField("PAclosetobillings", "Close to Billings", DataConnector.FieldTypeIdYesNo);
            pa01201.AddField("PADepartment", "Department", DataConnector.FieldTypeIdString);
            pa01201.AddField("PAEstimatorID", "Estimator ID", DataConnector.FieldTypeIdString);
            pa01201.AddField("PAprojmngrid", "Project Manager ID", DataConnector.FieldTypeIdString);
            pa01201.AddField("PABusMgrID", "Business Manager ID", DataConnector.FieldTypeIdString);
            pa01201.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            pa01201.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            pa01201.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            pa01201.AddField("COMPRCNT", "Commission Percent", DataConnector.FieldTypeIdPercentage);
            pa01201.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            pa01201.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            pa01201.AddField("PALabor_Rate_Table_ID", "Labor Rate Table ID", DataConnector.FieldTypeIdString);
            pa01201.AddField("PALabor_Rate_Table_Acc", "Labor Rate Table Accept", DataConnector.FieldTypeIdYesNo);
            pa01201.AddField("PAEquip_Rate_Table_ID", "Equip Rate Table ID", DataConnector.FieldTypeIdString);
            pa01201.AddField("PAEquip_Rate_Table_Acc", "Equip Rate Table Accept", DataConnector.FieldTypeIdYesNo);
            pa01201.AddField("PAService_Fee_Amount", "Service Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAProject_Fee_Amount", "Project Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PARetainer_Fee_Amount", "Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PARetentionFeeAmount", "Retention Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAProject_Amount", "Project Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("ACCTAMNT", "Account Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABILLFORMAT", "Bill Format", DataConnector.FieldTypeIdString);
            pa01201.AddField("DSCPCTAM", "Discount Percent", DataConnector.FieldTypeIdPercentage);
            pa01201.AddField("PA_Retention_Percent", "Retention Percent", DataConnector.FieldTypeIdPercentage);
            pa01201.AddField("PAUD1Proj", "User Defined 1", DataConnector.FieldTypeIdString);
            pa01201.AddField("PAUD2_Proj", "User Defined 2", DataConnector.FieldTypeIdString);
            pa01201.AddField("PAUnpostedQty", "Unposted Quantity", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAUnpostedTotalCostN", "Unposted Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Overhead", "Unposted Overhead", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedProfitN", "Unposted Profit", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Tax_Amount", "Unposted Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostAccrRevN", "Unposted Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedRecogRevN", "Unposted Recognized Revenue", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedCommitedQty", "Unposted Committed Qty", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAUnpostedCommitedCost", "Unposted Committed Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedCommitedTaxAmt", "Unposted Committed Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Project_Fee", "Unposted Project Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Retainer_Fee", "Unposted Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Service_Fee", "Unposted Service Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUNPOSTRETAMT", "Unposted Retention Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUNPOSTBIEEAMOUNT", "Unposted BIEE Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUNPEIEBAMOUNT", "Unposted EIEB Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Unposted_Billed_Reten", "Unposted Billed Retention", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedQty", "Actual Qty", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAPostedTotalCostN", "Actual Total Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedProfitN", "Actual Profit", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Tax_Amount", "Actual Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Accr_RevN", "Actual Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostRecogRevN", "Actual Recognized Revenue", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedCommitedQty", "Actual Committed Qty", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAPostedCommitedCost", "Actual Committed Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedCommitedTaxAmt", "Actual Committed Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Project_Fee", "Actual Project Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Retainer_Fee", "Actual Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Service_Fee", "Actual Service Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOSTRETAMT", "Actual Retention Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOSBIEEAMOUNT", "Actual BIEE Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOSEIEBAMOUNT", "Actual EIEB Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Actual_Billed_Retenti", "Actual Billed Retention", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAWrite_UpDown_Amount", "Write Up/Down Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABilled_QtyN", "Billed Qty", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PABilled_Cost", "Billed Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABilled_Accrued_Revenu", "Billed Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PACostPcntCompleted", "Cost Percent Completed", DataConnector.FieldTypeIdPercentage);
            pa01201.AddField("PAQuantityPcntCompleted", "Quantity Percent Completed", DataConnector.FieldTypeIdPercentage);
            pa01201.AddField("PA_Receipts_Amount", "Receipts Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Actual_Receipts_Amoun", "Actual Receipts Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Earnings", "Earnings", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Cost_of_Earnings", "Cost of Earnings", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostBillN", "Unposted Billings", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostDiscDolAmtN", "Unposted Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Sales_Tax_Am", "Unposted Sales Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedBillingsN", "Actual Billings", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedDiscDolAmtN", "Actual Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Sales_Tax_Amou", "Actual Sales Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABCWPAMT", "BCWP Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PABCWSAMT", "BCWS Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAACWPAMT", "ACWP Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            pa01201.AddField("PAPosted_Overhead", "Actual Overhead", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAApproved_Accrued_Reve", "Approved Accrued revenues", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAApproved_Cost", "Approved Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAApproved_Quantity", "Approved Quantity", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("WROFAMNT", "Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("ActualWriteOffAmount", "Actual Write Off Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("ActualDiscTakenAmount", "Actual Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PACommitted_Costs", "Committed Costs", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PACommitted_Qty", "Committed Quantity", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAPOCost", "PO Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOQty", "PO Quantity", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAPOPostedCost", "PO Actual Cost", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOPostedQty", "PO Actual Qty", DataConnector.FieldTypeIdQuantity);
            pa01201.AddField("PAtaxpaidamt", "Tax Paid Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedTaxPaidN", "Actual Tax Paid", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PApretainage", "Actual Retainage", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAunpretainage", "Unposted Retainage", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Write_Off_Tax_Amount", "Write Off Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualWOTaxAmt", "Actual Write Off Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Terms_Taken_Tax_Amt", "Terms Taken Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualTermsTakenTax", "Actual Terms Taken Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("WRKRCOMP", "Workers Comp", DataConnector.FieldTypeIdString);
            pa01201.AddField("PAUnpostedLossAmount", "Unposted Loss Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualLossAmount", "Actual Loss Amount", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Earnings", "Actual Earnings", DataConnector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualCostofEarnings", "Actual Cost of Earnings", DataConnector.FieldTypeIdCurrency);

            var accountingMethod = pa01201.AddField("PAAcctgMethod", "Accounting Method", DataConnector.FieldTypeIdEnum);
            accountingMethod.AddListItems(1, new List<string> { "When Performed", "When Billed", "Cost-to-Cost", "Effort-Expended", "Completed", "Effort Expended - Labor Only" });

            var laborRateTableType = pa01201.AddField("PALabor_RateTable_Type", "Labor Rate Table Type", DataConnector.FieldTypeIdEnum);
            laborRateTableType.AddListItems(1, new List<string> { "Employee", "Position" });

            var billingType = pa01201.AddField("PAbllngtype", "Billing Type", DataConnector.FieldTypeIdEnum);
            billingType.AddListItems(1, new List<string> { "STD", "N/C", "N/B" });
        }
            
        public DataConnectorEntity GetCostCategoryEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListCostCategories), "Cost categories", ParentConnector);
            
            var pa01001 = entity.AddTable("PA01001");

            var pa41001 = entity.AddTable("PA41001", "PA01001");
            pa41001.AddJoinFields("PACOSTCATCLASID", "PACOSTCATCLASID");
            
            AddCostCategoryEntityFields(pa01001, pa41001);
            
            entity.AddCalculation("DECPLQTY - 1", "Decimal Places Qty", DataConnector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            
            return entity;
        }
        public void AddCostCategoryEntityFields(DataConnectorTable pa01001, DataConnectorTable pa41001)
        {
            pa01001.AddField("PACOSTCATID", "Cost Category ID", DataConnector.FieldTypeIdString, true);
            pa01001.AddField("PACOSTCATNME", "Name", DataConnector.FieldTypeIdString, true);
            pa01001.AddField("PAinactive", "Inactive", DataConnector.FieldTypeIdYesNo);
            pa01001.AddField("PAIV_Item_Checkbox", "Inventory Item", DataConnector.FieldTypeIdYesNo);
            pa01001.AddField("PACOSTCATCLASID", "Cost Category Class ID", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAPay_Code_Hourly", "Paycode - Hourly", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAPay_Code_Salary", "Paycode - Salary", DataConnector.FieldTypeIdString);
            pa01001.AddField("UOMSCHDL", "U of M Schedule", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAUnit_of_Measure", "Unit of Measure", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAUNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            pa01001.AddField("PAcostaxscheduleid", "Purchase Tax Schedule ID", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAbilltaxscheduleid", "Sales Tax Schedule ID", DataConnector.FieldTypeIdString);
            pa41001.AddField("PA41001.PACOSTCATCLASNME", "Class ID Description", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAProfitPercentCP", "Profit Percent - CP", DataConnector.FieldTypeIdPercentage);
            pa01001.AddField("PAFFProfitPercent", "Profit Percent - FP", DataConnector.FieldTypeIdPercentage);
            pa01001.AddField("PATMProfitPercent", "Profit Percent - TM", DataConnector.FieldTypeIdPercentage);
            pa01001.AddField("PAProfitAmountCP", "Profit Amount - CP", DataConnector.FieldTypeIdCurrency);
            pa01001.AddField("PAFFProfitAmount", "Profit Amount - FP", DataConnector.FieldTypeIdCurrency);
            pa01001.AddField("PATMProfitAmount", "Profit Amount - TM", DataConnector.FieldTypeIdCurrency);
            pa01001.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa01001.AddField("PAOvhdAmtPerUnit", "Overhead Amount Per Unit", DataConnector.FieldTypeIdCurrency);
            pa01001.AddField("PAOverheaPercentage", "Overhead Percentage", DataConnector.FieldTypeIdPercentage);
         
            var transactionUsage = pa01001.AddField("PATU", "Transaction Usage", DataConnector.FieldTypeIdEnum);
            transactionUsage.AddListItems(1, new List<string> { "Timesheet", "Equipment Log", "Miscellaneous Log", "Purchases/Material", "Employee Expense" });

            var purchaseTaxOption = pa01001.AddField("PAPurchase_Tax_Options", "Purchase Tax Option", DataConnector.FieldTypeIdEnum);
            purchaseTaxOption.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var salesTaxOption = pa01001.AddField("PASales_Tax_Options", "Sales Tax Option", DataConnector.FieldTypeIdEnum);
            salesTaxOption.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var profitTypeCp = pa01001.AddField("PAProfit_Type__CP", "Profit Type - CP", DataConnector.FieldTypeIdEnum);
            profitTypeCp.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeFp = pa01001.AddField("PAFFProfitType", "Profit Type - FP", DataConnector.FieldTypeIdEnum);
            profitTypeFp.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeTm = pa01001.AddField("PATMProfitType", "Profit Type - TM", DataConnector.FieldTypeIdEnum);
            profitTypeTm.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });
        }
            
        public DataConnectorEntity GetEquipmentEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListEquipment), "Equipment", ParentConnector);

            var pa00701 = entity.AddTable("PA00701");

            var pa40701 = entity.AddTable("PA40701", "PA00701");
            pa40701.AddJoinFields("PAEQUIPTCLSID", "PAEQUIPTCLSID");
            
            AddEquipmentEntityFields(pa00701, pa40701);
            
            return entity;
        }
        public void AddEquipmentEntityFields(DataConnectorTable pa00701, DataConnectorTable pa40701)
        {
            pa00701.AddField("PAEQUIPTID", "Equipment ID", DataConnector.FieldTypeIdString, true);
            pa00701.AddField("PAEQNME", "Name", DataConnector.FieldTypeIdString, true);
            pa00701.AddField("PAinactive", "Inactive", DataConnector.FieldTypeIdYesNo);
            pa00701.AddField("PAEQUIPTCLSID", "Class ID", DataConnector.FieldTypeIdString);
            pa40701.AddField("PAEQPTCLSNME", "Class ID Description", DataConnector.FieldTypeIdString);
            pa00701.AddField("PAUnit_of_Measure", "Unit of Measure", DataConnector.FieldTypeIdString);
            pa00701.AddField("PAUNITCOST", "Unit Cost", DataConnector.FieldTypeIdString);
            pa00701.AddField("PATMProfitAmount", "Billing Rate - TM", DataConnector.FieldTypeIdCurrency);
            pa00701.AddField("PATMProfitPercent", "Markup Percent - TM", DataConnector.FieldTypeIdPercentage);
            pa00701.AddField("PAProfitAmountCP", "Profit Amount - CP", DataConnector.FieldTypeIdCurrency);
            pa00701.AddField("PAFFProfitAmount", "Profit Amount - FP", DataConnector.FieldTypeIdCurrency);
            pa00701.AddField("PATMProfitAmount", "Profit Amount - TM", DataConnector.FieldTypeIdCurrency);
            pa00701.AddField("PAProfitPercentCP", "Profit Percent - CP", DataConnector.FieldTypeIdPercentage);
            pa00701.AddField("PAFFProfitPercent", "Profit Percent - FP", DataConnector.FieldTypeIdPercentage);
            pa00701.AddField("PATMProfitPercent", "Profit Percent - TM", DataConnector.FieldTypeIdPercentage);
            pa00701.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa00701.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);

            var profitTypeTm = pa00701.AddField("PATMProfitType", "Profit Type - TM", DataConnector.FieldTypeIdEnum);
            profitTypeTm.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeCp = pa00701.AddField("PAProfit_Type__CP", "Profit Type - CP", DataConnector.FieldTypeIdEnum);
            profitTypeCp.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeFp = pa00701.AddField("PAFFProfitType", "Profit Type - FP", DataConnector.FieldTypeIdEnum);
            profitTypeFp.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });
        }
            
        public DataConnectorEntity GetMiscellaneousEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListMiscellaneous), "Miscellaneous", ParentConnector);
            
            var pa00801 = entity.AddTable("PA00801");

            var pa40801 = entity.AddTable("PA40801", "PA00801");
            pa40801.AddJoinFields("PAMISCCID", "PAMISCCID");
            
            AddMiscellaneousEntityFields(pa00801, pa40801);
            
            return entity;
        }
        public void AddMiscellaneousEntityFields(DataConnectorTable pa00801, DataConnectorTable pa40801)
        {
            pa00801.AddField("PSMISCID", "Miscellaneous ID", DataConnector.FieldTypeIdString, true);
            pa00801.AddField("PAMISCEN", "Name", DataConnector.FieldTypeIdString, true);
            pa00801.AddField("PAinactive", "Inactive", DataConnector.FieldTypeIdYesNo);
            pa00801.AddField("PAMISCCID", "Miscellaneous Class ID", DataConnector.FieldTypeIdString);
            pa00801.AddField("PAUnit_of_Measure", "Unit of Measure", DataConnector.FieldTypeIdString);
            pa00801.AddField("PAUNITCOST", "Unit Cost", DataConnector.FieldTypeIdCurrency);
            pa40801.AddField("PAMCN", "Class ID Description", DataConnector.FieldTypeIdString);
            pa00801.AddField("PATMProfitAmount", "Billing Rate - TM", DataConnector.FieldTypeIdCurrency);
            pa00801.AddField("PATMProfitPercent", "Markup Percent - TM", DataConnector.FieldTypeIdPercentage);
            pa00801.AddField("PAProfitAmountCP", "Profit Amount - CP", DataConnector.FieldTypeIdCurrency);
            pa00801.AddField("PAFFProfitAmount", "Profit Amount - FP", DataConnector.FieldTypeIdCurrency);
            pa00801.AddField("PATMProfitAmount", "Profit Amount - TM", DataConnector.FieldTypeIdCurrency);
            pa00801.AddField("PAProfitPercentCP", "Profit Percent - CP", DataConnector.FieldTypeIdPercentage); 
            pa00801.AddField("PAFFProfitPercent", "Profit Percent - FP", DataConnector.FieldTypeIdPercentage);
            pa00801.AddField("PATMProfitPercent", "Profit Percent - TM", DataConnector.FieldTypeIdPercentage);
            pa00801.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa00801.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);

            var profitTypeTm = pa00801.AddField("PATMProfitType", "Profit Type - TM", DataConnector.FieldTypeIdEnum);
            profitTypeTm.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeCp = pa00801.AddField("PAProfit_Type__CP", "Profit Type - CP", DataConnector.FieldTypeIdEnum);
            profitTypeCp.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeFp = pa00801.AddField("PAFFProfitType", "Profit Type - FP", DataConnector.FieldTypeIdEnum);
            profitTypeFp.AddListItems(1, new List<string> { "Billing Rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });
        }
            
        public DataConnectorEntity GetBillingCycleEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListBillingCycle), "Billing cycles", ParentConnector);
            
            var pa02000 = entity.AddTable("PA02000");
            
            AddBillingCycleEntityFields(pa02000);
            
            return entity;
        }
        public void AddBillingCycleEntityFields(DataConnectorTable pa02000)
        {
            pa02000.AddField("PABILLCYCLEID", "Billing Cycle ID", DataConnector.FieldTypeIdString, true);
            pa02000.AddField("PABILLCYCLEDESC", "Description", DataConnector.FieldTypeIdString, true);
            pa02000.AddField("PAPrior_Days_Before_Bi", "Days Before Billing", DataConnector.FieldTypeIdInteger, true);
            var frequency = pa02000.AddField("PABilling_Frequency", "Frequency", DataConnector.FieldTypeIdEnum, true);
            frequency.AddListItems(1, new List<string> { "Daily", "Weekly", "Semimonthly", "Monthly", "Quarterly", "Semiannually", "Annually" });
            pa02000.AddField("PA_Include_Project_Fee", "Include Project Fee", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbfee", "Include Service Fee", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PA_Include_Retainer_Fee", "Include Retainer Fee", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbts", "Include Timesheets", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbinv", "Include Purchase Invoice", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbEL", "Include Equipment Logs", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcber", "Include Employee Expense", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbML", "Include Miscellaneous Logs", DataConnector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbvi", "Include Inventory", DataConnector.FieldTypeIdYesNo);
        }
            
        public DataConnectorEntity GetEmployeeExpenseTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListEmployeeExpenseTrx), "Employee expense transactions", ParentConnector);
            
            var pa10500 = entity.AddTable("PA10500");
            
            AddEmployeeExpenseTransactionEntityFields(pa10500);
            
            return entity;
        }
        public void AddEmployeeExpenseTransactionEntityFields(DataConnectorTable pa10500)
        {
            var transactionType = pa10500.AddField("PAertrxtype", "Transaction Type", DataConnector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10500.AddField("PAerdocnumber", "Empl Expense Document No.", DataConnector.FieldTypeIdString, true);
            pa10500.AddField("PDK_EE_No", "PDK EE No.", DataConnector.FieldTypeIdString, true);
            pa10500.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa10500.AddField("USERID", "User ID", DataConnector.FieldTypeIdString, true);
            pa10500.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            pa10500.AddField("PAStartDate", "Start Date", DataConnector.FieldTypeIdDate, true);
            pa10500.AddField("PAEnDate", "End Date", DataConnector.FieldTypeIdDate, true);
            pa10500.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa10500.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa10500.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency, true);
            pa10500.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa10500.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa10500.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa10500.AddField("PAREFNO", "Reference Document No.", DataConnector.FieldTypeIdString);
            pa10500.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pa10500.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa10500.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa10500.AddField("PAVENADDRESSID", "Address ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdString);
            pa10500.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa10500.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            pa10500.AddField("DSCDLRAM", "Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORDDLRAT", "Originating Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            pa10500.AddField("DISAMTAV", "Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ODISAMTAV", "Originating Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PRCTDISC", "Percent Discount", DataConnector.FieldTypeIdPercentage);
            pa10500.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa10500.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            pa10500.AddField("PAEXTCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PAREIMBURSTAXAMT", "Reimbursable Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PAORIGREIMTAXAMT", "Originating Reimbursable Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("BKTPURAM", "Backout Purchases Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("MSCCHAMT", "Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("OMISCAMT", "Originating Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("TEN99AMNT", "1099 Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("OR1099AM", "Originating 1099 Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("UN1099AM", "Unapplied 1099 Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("CASHAMNT", "Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORCASAMT", "Originating Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("CAMCBKID", "Cash Amount Checkbook ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("CDOCNMBR", "Cash Document Number", DataConnector.FieldTypeIdString);
            pa10500.AddField("CAMTDATE", "Cash Amount Date", DataConnector.FieldTypeIdDate);
            pa10500.AddField("CAMPMTNM", "Cash Amount Payment Number", DataConnector.FieldTypeIdString);
            pa10500.AddField("CHRGAMNT", "Charge Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("OCHGAMT", "Originating Charge Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("CHEKAMNT", "Check Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORCHKAMT", "Originating Check Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString);
            pa10500.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            pa10500.AddField("CAMPYNBR", "Check Amount Payment Number", DataConnector.FieldTypeIdString);
            pa10500.AddField("CHAMCBID", "Check Amount Checkbook ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("CARDNAME", "Card Name", DataConnector.FieldTypeIdString);
            pa10500.AddField("CRCRDAMT", "Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORCCDAMT", "Originating Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("CRCARDDT", "Credit Card Date", DataConnector.FieldTypeIdDate);
            pa10500.AddField("CCAMPYNM", "Credit Card Amount Payment Number", DataConnector.FieldTypeIdString);
            pa10500.AddField("CCRCTNUM", "Credit Card Receipt Number", DataConnector.FieldTypeIdString);
            pa10500.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORDISTKN", "Originating Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORDAVFRT", "Originating Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ODAVPUR", "Originating Discount Available Purchases", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORDAVMSC", "Originating Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("BCKTXAMT", "Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("OBTAXAMT", "Originating Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PAReimbursableAmount", "PA Reimbursable Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PAOrigReimbursableAmt", "PA Originating Reimbursable Amount", DataConnector.FieldTypeIdCurrency);
            pa10500.AddField("PAPD", "PA Post Date", DataConnector.FieldTypeIdDate);
            pa10500.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            pa10500.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            pa10500.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);

            var freightTaxablePurchase = pa10500.AddField("PAFreight_Taxable_P", "Freight Taxable - Purchase", DataConnector.FieldTypeIdEnum);
            freightTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var miscTaxablePurchase = pa10500.AddField("PAMisc_Taxable_P", "Misc Taxable - Purchase", DataConnector.FieldTypeIdEnum);
            miscTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
        }

        public DataConnectorEntity GetEquipmentLogTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListEquipmentLogTrx), "Equipment log transactions", ParentConnector);
            
            var pa10100 = entity.AddTable("PA10100");
            
            AddEquipmentLogTransactionEntityFields(pa10100);
            
            return entity;
        }
        public void AddEquipmentLogTransactionEntityFields(DataConnectorTable pa10100)
        {
            var transactionType = pa10100.AddField("PAEQLTRX", "Transaction Type", DataConnector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10100.AddField("PAEQLOGNO", "Equipment Log No.", DataConnector.FieldTypeIdString, true);
            pa10100.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa10100.AddField("PAEQUIPTID", "Equipment ID", DataConnector.FieldTypeIdString, true);
            pa10100.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa10100.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa10100.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa10100.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa10100.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa10100.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa10100.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa10100.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa10100.AddField("PAREFNO", "Reference Document No.", DataConnector.FieldTypeIdString);
            pa10100.AddField("PAREPD", "Reporting Period", DataConnector.FieldTypeIdInteger);
            pa10100.AddField("PAREPDT", "Reporting Date", DataConnector.FieldTypeIdDate);
            pa10100.AddField("PAPeriodEndDate", "Period End Date", DataConnector.FieldTypeIdDate);
            pa10100.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa10100.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa10100.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa10100.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdString);
            pa10100.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa10100.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa10100.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa10100.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);
        }

        public DataConnectorEntity GetIvTransferTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListIvTransferTrx), "Inventory transfer transactions", ParentConnector);
            
            var pa10900 = entity.AddTable("PA10900");
            
            AddIvTransferTransactionEntityFields(pa10900);
            
            return entity;
        }
        public void AddIvTransferTransactionEntityFields(DataConnectorTable pa10900)
        {
            pa10900.AddField("PAIV_Document_No", "PA IV Document Number", DataConnector.FieldTypeIdString, true);
            var ivTransferType = pa10900.AddField("PAIV_Transfer_Type", "IV Transfer Type", DataConnector.FieldTypeIdEnum, true);
            ivTransferType.AddListItems(1, new List<string> { "Standard", "Return", "Return From Project" });
            pa10900.AddField("IVDOCNBR", "IV Document Number", DataConnector.FieldTypeIdString, true);
            pa10900.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa10900.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            pa10900.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa10900.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa10900.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa10900.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa10900.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa10900.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa10900.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa10900.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa10900.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa10900.AddField("REQSTDBY", "Requested By", DataConnector.FieldTypeIdString);
            pa10900.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa10900.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
        }

        public DataConnectorEntity GetMiscLogTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListMiscLogTrx), "Miscellaneous log transactions", ParentConnector);
            
            var pa10200 = entity.AddTable("PA10200");
            
            AddMiscLogTransactionEntityFields(pa10200);
            
            return entity;
        }
        public void AddMiscLogTransactionEntityFields(DataConnectorTable pa10200)
        {
            var transactionType = pa10200.AddField("PSMISCLTRXTYPE", "Transaction Type", DataConnector.FieldTypeIdEnum, true); 
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10200.AddField("PAMISCLDOCNO", "Miscellaneous Log No.", DataConnector.FieldTypeIdString, true);
            pa10200.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa10200.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa10200.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa10200.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa10200.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa10200.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa10200.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa10200.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa10200.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa10200.AddField("PAREFNO", "Reference Document No.", DataConnector.FieldTypeIdString);
            pa10200.AddField("PSMISCID", "Miscellaneous ID", DataConnector.FieldTypeIdString);
            pa10200.AddField("PAREPD", "Reporting Period", DataConnector.FieldTypeIdInteger);
            pa10200.AddField("PAREPDT", "Reporting Date", DataConnector.FieldTypeIdDate);
            pa10200.AddField("PAPeriodEndDate", "Period End Date", DataConnector.FieldTypeIdDate);
            pa10200.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa10200.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa10200.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa10200.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdCurrency);
            pa10200.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa10200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa10200.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa10200.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);
        }
            
        public DataConnectorEntity GetTimesheetTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListTimesheetTrx), "Timesheet transactions", ParentConnector);
            
            var pa10000 = entity.AddTable("PA10000");
            
            AddTimesheetTransactionEntityFields(pa10000);
            
            return entity;
        }
        public void AddTimesheetTransactionEntityFields(DataConnectorTable pa10000)
        {
            var transactionType = pa10000.AddField("PATSTYP", "Transaction Type", DataConnector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10000.AddField("PATSNO", "Timesheet No.", DataConnector.FieldTypeIdString, true);
            pa10000.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate,true);
            pa10000.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            pa10000.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa10000.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa10000.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa10000.AddField("PDK_TS_No", "PDK Timesheet No.", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa10000.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa10000.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa10000.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa10000.AddField("TRXSORCE", "Transaction Source", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAREFNO", "Reference Doc No.", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAREPD", "Reporting Period", DataConnector.FieldTypeIdInteger);
            pa10000.AddField("PAREPDT", "Reporting Date", DataConnector.FieldTypeIdDate);
            pa10000.AddField("PAPeriodEndDate", "Period End Date", DataConnector.FieldTypeIdDate);
            pa10000.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa10000.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa10000.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa10000.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);

            var employedBy = pa10000.AddField("PA_Employed_By", "Employed By", DataConnector.FieldTypeIdEnum);
            employedBy.AddListItems(1, new List<string> { "Company", "Placement Agency", "Subcontractor" });
        }
            
        public DataConnectorEntity GetMiscLogHistTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListMiscLogHistTrx), "Historical miscellaneous log transactions", ParentConnector);
            
            var pa30300 = entity.AddTable("PA30300");
            
            AddMiscLogHistTransactionEntityFields(pa30300);
            
            return entity;
        }
        public void AddMiscLogHistTransactionEntityFields(DataConnectorTable pa30300)
        {
            var transactionType = pa30300.AddField("PSMISCLTRXTYPE", "Transaction Type", DataConnector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30300.AddField("PAMISCLDOCNO", "Miscellaneous Log No.", DataConnector.FieldTypeIdString, true);
            pa30300.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa30300.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa30300.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa30300.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa30300.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa30300.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa30300.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa30300.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa30300.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa30300.AddField("PAREFNO", "Reference Document No.", DataConnector.FieldTypeIdString);
            pa30300.AddField("PSMISCID", "Miscellaneous ID", DataConnector.FieldTypeIdString);
            pa30300.AddField("PAREPD", "Reporting Period", DataConnector.FieldTypeIdInteger);
            pa30300.AddField("PAREPDT", "Reporting Date", DataConnector.FieldTypeIdDate);
            pa30300.AddField("PAPeriodEndDate", "Period End Date", DataConnector.FieldTypeIdDate);
            pa30300.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa30300.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa30300.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa30300.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdCurrency);
            pa30300.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa30300.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa30300.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa30300.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);
        }
            
        public DataConnectorEntity GetEquipmentLogHistTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListEquipmentLogHistTrx), "Historical equipment log transactions", ParentConnector);
            
            var pa30200 = entity.AddTable("PA30200");
            
            AddEquipmentLogHistTransactionEntityFields(pa30200);
            
            return entity;
        }
        public void AddEquipmentLogHistTransactionEntityFields(DataConnectorTable pa30200)
        {
            var transactionType = pa30200.AddField("PAEQLTRX", "Transaction Type", DataConnector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30200.AddField("PAEQLOGNO", "Equipment Log No.", DataConnector.FieldTypeIdString, true);
            pa30200.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa30200.AddField("PAEQUIPTID", "Equipment ID", DataConnector.FieldTypeIdString, true);
            pa30200.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa30200.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa30200.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa30200.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa30200.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa30200.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa30200.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa30200.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa30200.AddField("PAREFNO", "Reference Document No.", DataConnector.FieldTypeIdString);
            pa30200.AddField("PAREPD", "Reporting Period", DataConnector.FieldTypeIdInteger);
            pa30200.AddField("PAREPDT", "Reporting Date", DataConnector.FieldTypeIdDate);
            pa30200.AddField("PAPeriodEndDate", "Period End Date", DataConnector.FieldTypeIdDate);
            pa30200.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa30200.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa30200.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa30200.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdString);
            pa30200.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa30200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa30200.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa30200.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);
        }
            
        public DataConnectorEntity GetTimesheetHistTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListTimesheetHistTrx), "Historical timesheet transactions", ParentConnector);
            
            var pa30100 = entity.AddTable("PA30100");
            
            AddTimesheetHistTransactionEntityFields(pa30100);
            
            return entity;
        }
        public void AddTimesheetHistTransactionEntityFields(DataConnectorTable pa30100)
        {
            var transactionType = pa30100.AddField("PATSTYP", "Transaction Type", DataConnector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30100.AddField("PATSNO", "Timesheet No.", DataConnector.FieldTypeIdString, true);
            pa30100.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa30100.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            pa30100.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa30100.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa30100.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa30100.AddField("PDK_TS_No", "PDK Timesheet No.", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa30100.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa30100.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa30100.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa30100.AddField("TRXSORCE", "Transaction Source", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAREFNO", "Reference Doc No.", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAREPD", "Reporting Period", DataConnector.FieldTypeIdInteger);
            pa30100.AddField("PAREPDT", "Reporting Date", DataConnector.FieldTypeIdDate);
            pa30100.AddField("PAPeriodEndDate", "Period End Date", DataConnector.FieldTypeIdDate);
            pa30100.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa30100.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa30100.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa30100.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);

            var employedBy = pa30100.AddField("PA_Employed_By", "Employed By", DataConnector.FieldTypeIdEnum);
            employedBy.AddListItems(1, new List<string> { "Company", "Placement Agency", "Subcontractor" });
        }
            
        public DataConnectorEntity GetIvTransferHistTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListIvTransferHistTrx), "Historical inventory transfer transactions", ParentConnector);
            
            var pa30900 = entity.AddTable("PA30900");
            
            AddIvTransferHistTransactionEntityFields(pa30900);
            
            return entity;
        }
        public void AddIvTransferHistTransactionEntityFields(DataConnectorTable pa30900)
        {
            pa30900.AddField("PAIV_Document_No", "PA IV Document Number", DataConnector.FieldTypeIdString, true);
            var ivTransferType = pa30900.AddField("PAIV_Transfer_Type", "IV Transfer Type", DataConnector.FieldTypeIdEnum, true);
            ivTransferType.AddListItems(1, new List<string> { "Standard", "Return", "Return From Project" });
            pa30900.AddField("IVDOCNBR", "IV Document Number", DataConnector.FieldTypeIdString, true);
            pa30900.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa30900.AddField("LOCNCODE", "Location Code", DataConnector.FieldTypeIdString, true);
            pa30900.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa30900.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa30900.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency, true);
            pa30900.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa30900.AddField("USERID", "User ID", DataConnector.FieldTypeIdString);
            pa30900.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa30900.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa30900.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa30900.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa30900.AddField("REQSTDBY", "Requested By", DataConnector.FieldTypeIdString);
            pa30900.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa30900.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
        }
            
        public DataConnectorEntity GetEmployeeExpenseHistTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListEmployeeExpenseHistTrx), "Historical employee expense transactions", ParentConnector);
            
            var pa30500 = entity.AddTable("PA30500");
            
            AddEmployeeExpenseHistTransactionEntityFields(pa30500);
            
            return entity;
        }
        public void AddEmployeeExpenseHistTransactionEntityFields(DataConnectorTable pa30500)
        {
            var transactionType = pa30500.AddField("PAertrxtype", "Transaction Type", DataConnector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30500.AddField("PAerdocnumber", "Empl Expense Document No.", DataConnector.FieldTypeIdString, true);
            pa30500.AddField("PDK_EE_No", "PDK EE No.", DataConnector.FieldTypeIdString, true);
            pa30500.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa30500.AddField("USERID", "User ID", DataConnector.FieldTypeIdString, true);
            pa30500.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            pa30500.AddField("PAStartDate", "Start Date", DataConnector.FieldTypeIdDate, true);
            pa30500.AddField("PAEnDate", "End Date", DataConnector.FieldTypeIdDate, true);
            pa30500.AddField("PATQTY", "Total Quantity", DataConnector.FieldTypeIdQuantity, true);
            pa30500.AddField("PAtotcosts", "Total Costs", DataConnector.FieldTypeIdCurrency, true);
            pa30500.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency, true);
            pa30500.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa30500.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa30500.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa30500.AddField("PAREFNO", "Reference Document No.", DataConnector.FieldTypeIdString);
            pa30500.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pa30500.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa30500.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa30500.AddField("PAVENADDRESSID", "Address ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("PAreptsuff", "Reporting Suffix", DataConnector.FieldTypeIdString);
            pa30500.AddField("PAYR", "Year", DataConnector.FieldTypeIdString);
            pa30500.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            pa30500.AddField("DSCDLRAM", "Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORDDLRAT", "Originating Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            pa30500.AddField("DISAMTAV", "Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ODISAMTAV", "Originating Discount Amount Available", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PRCTDISC", "Percent Discount", DataConnector.FieldTypeIdPercentage);
            pa30500.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa30500.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            pa30500.AddField("PAEXTCOST", "Extended Cost", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("OREXTCST", "Originating Extended Cost", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PAORIGTOTCOSTS", "Originating Total Costs", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PAREIMBURSTAXAMT", "Reimbursable Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PAORIGREIMTAXAMT", "Originating Reimbursable Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("BKTPURAM", "Backout Purchases Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PATACRV", "Total Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PAORIACCRREV", "Originating Accrued Revenues", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("FRTAMNT", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORFRTAMT", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("MSCCHAMT", "Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("OMISCAMT", "Originating Misc Charges Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("TEN99AMNT", "1099 Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("OR1099AM", "Originating 1099 Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("UN1099AM", "Unapplied 1099 Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("CASHAMNT", "Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORCASAMT", "Originating Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("CAMCBKID", "Cash Amount Checkbook ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("CDOCNMBR", "Cash Document Number", DataConnector.FieldTypeIdString);
            pa30500.AddField("CAMTDATE", "Cash Amount Date", DataConnector.FieldTypeIdDate);
            pa30500.AddField("CAMPMTNM", "Cash Amount Payment Number", DataConnector.FieldTypeIdString);
            pa30500.AddField("CHRGAMNT", "Charge Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("OCHGAMT", "Originating Charge Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("CHEKAMNT", "Check Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORCHKAMT", "Originating Check Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString);
            pa30500.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            pa30500.AddField("CAMPYNBR", "Check Amount Payment Number", DataConnector.FieldTypeIdString);
            pa30500.AddField("CHAMCBID", "Check Amount Checkbook ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("CARDNAME", "Card Name", DataConnector.FieldTypeIdString);
            pa30500.AddField("CRCRDAMT", "Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORCCDAMT", "Originating Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("CRCARDDT", "Credit Card Date", DataConnector.FieldTypeIdDate);
            pa30500.AddField("CCAMPYNM", "Credit Card Amount Payment Number", DataConnector.FieldTypeIdString);
            pa30500.AddField("CCRCTNUM", "Credit Card Receipt Number", DataConnector.FieldTypeIdString);
            pa30500.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORDISTKN", "Originating Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORDAVFRT", "Originating Discount Available Freight", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ODAVPUR", "Originating Discount Available Purchases", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORDAVMSC", "Originating Discount Available Misc", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("BCKTXAMT", "Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("OBTAXAMT", "Originating Backout Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PAReimbursableAmount", "PA Reimbursable Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PAOrigReimbursableAmt", "PA Originating Reimbursable Amount", DataConnector.FieldTypeIdCurrency);
            pa30500.AddField("PAPD", "PA Post Date", DataConnector.FieldTypeIdDate);
            pa30500.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            pa30500.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            pa30500.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);

            var freightTaxablePurchase = pa30500.AddField("PAFreight_Taxable_P", "Freight Taxable - Purchase", DataConnector.FieldTypeIdEnum);
            freightTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var miscTaxablePurchase = pa30500.AddField("PAMisc_Taxable_P", "Misc Taxable - Purchase", DataConnector.FieldTypeIdEnum);
            miscTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
        }
            
        public DataConnectorEntity GetBillingHistTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListBillingHistTrx), "Billing transactions (history)", ParentConnector);
            
            var pa33100 = entity.AddTable("PA33100");
            
            var rm00101 = entity.AddTable("RM00101", "PA33100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddBillingHistTransactionEntityFields(pa33100, rm00101);
            
            return entity;
        }
        public void AddBillingHistTransactionEntityFields(DataConnectorTable pa33100, DataConnectorTable rm00101)
        {
            var billerTrxType = pa33100.AddField("PABILLTRXT", "Biller Transaction Type", DataConnector.FieldTypeIdEnum, true);
            billerTrxType.AddListItems(1, new List<string> { "Invoice", "TM Return" });
            pa33100.AddField("PADocnumber20", "Document Number", DataConnector.FieldTypeIdString, true);
            pa33100.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa33100.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            pa33100.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString, true);
            pa33100.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency, true);
            pa33100.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa33100.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa33100.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa33100.AddField("PAuserid", "User ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("LSTEDTDT", "Last Edit Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("LSTUSRED", "Last User to Edit", DataConnector.FieldTypeIdString);
            pa33100.AddField("PAREFNO", "Reference Document Number", DataConnector.FieldTypeIdString);
            pa33100.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            pa33100.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("PABILLFORMAT", "Bill Format", DataConnector.FieldTypeIdString);
            pa33100.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa33100.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa33100.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa33100.AddField("CASHAMNT", "Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("CBKIDCSH", "Checkbook ID Cash", DataConnector.FieldTypeIdString);
            pa33100.AddField("CASHDATE", "Cash Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("DCNUMCSH", "Document Number Cash", DataConnector.FieldTypeIdString);
            pa33100.AddField("CHEKAMNT", "Check Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("CBKIDCHK", "Checkbook ID Check", DataConnector.FieldTypeIdString);
            pa33100.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString);
            pa33100.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("DCNUMCHK", "Document Number Check", DataConnector.FieldTypeIdString);
            pa33100.AddField("CRCRDAMT", "Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("CRCRDNAM", "Credit Card Name", DataConnector.FieldTypeIdString);
            pa33100.AddField("RCTNCCRD", "Receipt Number Credit Card", DataConnector.FieldTypeIdString);
            pa33100.AddField("CRCARDDT", "Credit Card Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("DCNUMCRD", "Document Number Credit Card", DataConnector.FieldTypeIdString);
            pa33100.AddField("DSCPCTAM", "Discount Percent", DataConnector.FieldTypeIdPercentage);
            pa33100.AddField("DISCAMNT", "Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("DISAVAMT", "Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("DISCRTND", "Discount Returned", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PABillingAmount", "Billing Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PA_Freight_Amount", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PA_Misc_Amount", "Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PATOTTAX", "Total Tax", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PABACKBILL", "Backout Bill Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("BKTFRTAM", "Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("BKTMSCAM", "Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("APPLDAMT", "Applied Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PALastDateApplied", "Last Date Applied", DataConnector.FieldTypeIdDate);
            pa33100.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            pa33100.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("COMDLRAM", "Commission Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("CUTOFDAT", "Cutoff Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pa33100.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("PARetentionFeeAmount", "Retention Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PARetainer_Fee_Amount", "Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAtotbillings", "Total Billings", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAlastprtby", "Last Printed By", DataConnector.FieldTypeIdString);
            pa33100.AddField("PAlastprtdate", "Last Printed Date", DataConnector.FieldTypeIdDate);
            pa33100.AddField("ORCASAMT", "Originating Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORCHKAMT", "Originating Check Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORCCDAMT", "Originating Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORDDLRAT", "Originating Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORDAVAMT", "Originating Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORBKTFRT", "Originating Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORBKTMSC", "Originating Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORAPPAMT", "Originating Applied Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("ORCOMAMT", "Originating Commission Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigBillAmount", "Originating Billing Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigFreightAmt", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigMiscAmt", "Originating Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigTotalTaxAmt", "Originating Total Tax", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigBackoutBillAmt", "Originating Backout Bill Amt", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigRetentionFeeAmt", "Originating Retention Fee Amt", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigRetainerFeeAmt", "Originating Retainer Fee Amt", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigTotalBilings", "Originating Total Billings", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigDiscTakenAmt", "Originating Discount Taken Amt", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigDiscRetAmt", "Originating Discount Returned Amt", DataConnector.FieldTypeIdCurrency);
            pa33100.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            pa33100.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            pa33100.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("RM00101.CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var miscTaxable = pa33100.AddField("MISCTXBL", "Misc Taxable", DataConnector.FieldTypeIdEnum); 
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var freightTaxable = pa33100.AddField("FRGTTXBL", "Freight Taxable", DataConnector.FieldTypeIdEnum); 
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
        }
            
        public DataConnectorEntity GetBillingWorkTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListBillingWorkTrx), "Billing transactions (work)", ParentConnector);
            
            var pa13100 = entity.AddTable("PA13100");
            
            var rm00101 = entity.AddTable("RM00101", "PA13100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddBillingHistTransactionEntityFields(pa13100, rm00101);
            
            return entity;
        }
        public void AddBillingWorkTransactionEntityFields(DataConnectorTable pa13100, DataConnectorTable rm00101)
        {
            var billerTrxType = pa13100.AddField("PABILLTRXT", "Biller Transaction Type", DataConnector.FieldTypeIdEnum, true);
            billerTrxType.AddListItems(1, new List<string> { "Invoice", "TM Return" });
            pa13100.AddField("PADocnumber20", "Document Number", DataConnector.FieldTypeIdString, true);
            pa13100.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa13100.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            pa13100.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString, true);
            pa13100.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency, true);
            pa13100.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa13100.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa13100.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa13100.AddField("PAuserid", "User ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("LSTEDTDT", "Last Edit Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("LSTUSRED", "Last User to Edit", DataConnector.FieldTypeIdString);
            pa13100.AddField("PAREFNO", "Reference Document Number", DataConnector.FieldTypeIdString);
            pa13100.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            pa13100.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("PABILLFORMAT", "Bill Format", DataConnector.FieldTypeIdString);
            pa13100.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa13100.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa13100.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa13100.AddField("CASHAMNT", "Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("CBKIDCSH", "Checkbook ID Cash", DataConnector.FieldTypeIdString);
            pa13100.AddField("CASHDATE", "Cash Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("DCNUMCSH", "Document Number Cash", DataConnector.FieldTypeIdString);
            pa13100.AddField("CHEKAMNT", "Check Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("CBKIDCHK", "Checkbook ID Check", DataConnector.FieldTypeIdString);
            pa13100.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString);
            pa13100.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("DCNUMCHK", "Document Number Check", DataConnector.FieldTypeIdString);
            pa13100.AddField("CRCRDAMT", "Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("CRCRDNAM", "Credit Card Name", DataConnector.FieldTypeIdString);
            pa13100.AddField("RCTNCCRD", "Receipt Number Credit Card", DataConnector.FieldTypeIdString);
            pa13100.AddField("CRCARDDT", "Credit Card Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("DCNUMCRD", "Document Number Credit Card", DataConnector.FieldTypeIdString);
            pa13100.AddField("DSCPCTAM", "Discount Percent", DataConnector.FieldTypeIdPercentage);
            pa13100.AddField("DISCAMNT", "Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("DISAVAMT", "Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("DISCRTND", "Discount Returned", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PABillingAmount", "Billing Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PA_Freight_Amount", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PA_Misc_Amount", "Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PATOTTAX", "Total Tax", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PABACKBILL", "Backout Bill Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("BKTFRTAM", "Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("BKTMSCAM", "Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("APPLDAMT", "Applied Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PALastDateApplied", "Last Date Applied", DataConnector.FieldTypeIdDate);
            pa13100.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            pa13100.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("COMDLRAM", "Commission Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("CUTOFDAT", "Cutoff Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pa13100.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("PARetentionFeeAmount", "Retention Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PARetainer_Fee_Amount", "Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAtotbillings", "Total Billings", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAlastprtby", "Last Printed By", DataConnector.FieldTypeIdString);
            pa13100.AddField("PAlastprtdate", "Last Printed Date", DataConnector.FieldTypeIdDate);
            pa13100.AddField("ORCASAMT", "Originating Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORCHKAMT", "Originating Check Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORCCDAMT", "Originating Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORDDLRAT", "Originating Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORDAVAMT", "Originating Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORBKTFRT", "Originating Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORBKTMSC", "Originating Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORAPPAMT", "Originating Applied Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("ORCOMAMT", "Originating Commission Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigBillAmount", "Originating Billing Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigFreightAmt", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigMiscAmt", "Originating Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigTotalTaxAmt", "Originating Total Tax", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigBackoutBillAmt", "Originating Backout Bill Amt", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigRetentionFeeAmt", "Originating Retention Fee Amt", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigRetainerFeeAmt", "Originating Retainer Fee Amt", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigTotalBilings", "Originating Total Billings", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigDiscTakenAmt", "Originating Discount Taken Amt", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigDiscRetAmt", "Originating Discount Returned Amt", DataConnector.FieldTypeIdCurrency);
            pa13100.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            pa13100.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            pa13100.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("RM00101.CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var miscTaxable = pa13100.AddField("MISCTXBL", "Misc Taxable", DataConnector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var freightTaxable = pa13100.AddField("FRGTTXBL", "Freight Taxable", DataConnector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
        }
            
        public DataConnectorEntity GetBillingOpenTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(ProjectSmartListBillingOpenTrx), "Billing transactions (open)", ParentConnector);
            
            var pa23100 = entity.AddTable("PA23100");
            
            var rm00101 = entity.AddTable("RM00101", "PA23100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddBillingHistTransactionEntityFields(pa23100, rm00101);
            
            return entity;
        }
        public void AddBillingOpenTransactionEntityFields(DataConnectorTable pa23100, DataConnectorTable rm00101)
        {
            var billerTrxType = pa23100.AddField("PABILLTRXT", "Biller Transaction Type", DataConnector.FieldTypeIdEnum, true);
            billerTrxType.AddListItems(1, new List<string> { "Invoice", "TM Return" });
            pa23100.AddField("PADocnumber20", "Document Number", DataConnector.FieldTypeIdString, true);
            pa23100.AddField("PADOCDT", "Document Date", DataConnector.FieldTypeIdDate, true);
            pa23100.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            pa23100.AddField("CSTPONBR", "Customer PO Number", DataConnector.FieldTypeIdString, true);
            pa23100.AddField("DOCAMNT", "Document Amount", DataConnector.FieldTypeIdCurrency, true);
            pa23100.AddField("PAPD", "Post Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            pa23100.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            pa23100.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            pa23100.AddField("PAuserid", "User ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("LSTEDTDT", "Last Edit Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("LSTUSRED", "Last User to Edit", DataConnector.FieldTypeIdString);
            pa23100.AddField("PAREFNO", "Reference Document Number", DataConnector.FieldTypeIdString);
            pa23100.AddField("PRBTADCD", "Primary Billto Address Code", DataConnector.FieldTypeIdString);
            pa23100.AddField("PYMTRMID", "Payment Terms ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("PABILLFORMAT", "Bill Format", DataConnector.FieldTypeIdString);
            pa23100.AddField("PACOMM", "Comment", DataConnector.FieldTypeIdString);
            pa23100.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("PAUD1", "User Defined 1", DataConnector.FieldTypeIdString);
            pa23100.AddField("PAUD2", "User Defined 2", DataConnector.FieldTypeIdString);
            pa23100.AddField("CASHAMNT", "Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("CBKIDCSH", "Checkbook ID Cash", DataConnector.FieldTypeIdString);
            pa23100.AddField("CASHDATE", "Cash Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("DCNUMCSH", "Document Number Cash", DataConnector.FieldTypeIdString);
            pa23100.AddField("CHEKAMNT", "Check Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("CBKIDCHK", "Checkbook ID Check", DataConnector.FieldTypeIdString);
            pa23100.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString);
            pa23100.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("DCNUMCHK", "Document Number Check", DataConnector.FieldTypeIdString);
            pa23100.AddField("CRCRDAMT", "Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("CRCRDNAM", "Credit Card Name", DataConnector.FieldTypeIdString);
            pa23100.AddField("RCTNCCRD", "Receipt Number Credit Card", DataConnector.FieldTypeIdString);
            pa23100.AddField("CRCARDDT", "Credit Card Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("DCNUMCRD", "Document Number Credit Card", DataConnector.FieldTypeIdString);
            pa23100.AddField("DSCPCTAM", "Discount Percent", DataConnector.FieldTypeIdPercentage);
            pa23100.AddField("DISCAMNT", "Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("DISAVAMT", "Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("DISCDATE", "Discount Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("DISTKNAM", "Discount Taken Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("DISCRTND", "Discount Returned", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PABillingAmount", "Billing Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("TRDISAMT", "Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PA_Freight_Amount", "Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PA_Misc_Amount", "Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("FRTTXAMT", "Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("MSCTXAMT", "Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PATOTTAX", "Total Tax", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PABACKBILL", "Backout Bill Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("BKTFRTAM", "Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("BKTMSCAM", "Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("DUEDATE", "Due Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("APPLDAMT", "Applied Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PALastDateApplied", "Last Date Applied", DataConnector.FieldTypeIdDate);
            pa23100.AddField("SLPRSNID", "Salesperson ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("SALSTERR", "Sales Territory", DataConnector.FieldTypeIdString);
            pa23100.AddField("TAXSCHID", "Tax Schedule ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("COMDLRAM", "Commission Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("CUTOFDAT", "Cutoff Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("SHIPMTHD", "Shipping Method", DataConnector.FieldTypeIdString);
            pa23100.AddField("MSCSCHID", "Misc Schedule ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("FRTSCHID", "Freight Schedule ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("PARetentionFeeAmount", "Retention Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PARetainer_Fee_Amount", "Retainer Fee Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAtotbillings", "Total Billings", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAlastprtby", "Last Printed By", DataConnector.FieldTypeIdString);
            pa23100.AddField("PAlastprtdate", "Last Printed Date", DataConnector.FieldTypeIdDate);
            pa23100.AddField("ORCASAMT", "Originating Cash Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORCHKAMT", "Originating Check Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORCCDAMT", "Originating Credit Card Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORDDLRAT", "Originating Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORDAVAMT", "Originating Discount Available Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORTDISAM", "Originating Trade Discount Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORFRTTAX", "Originating Freight Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORMSCTAX", "Originating Misc Tax Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORBKTFRT", "Originating Backout Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORBKTMSC", "Originating Backout Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORDOCAMT", "Originating Document Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORAPPAMT", "Originating Applied Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("ORCOMAMT", "Originating Commission Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigBillAmount", "Originating Billing Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigFreightAmt", "Originating Freight Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigMiscAmt", "Originating Misc Amount", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigTotalTaxAmt", "Originating Total Tax", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigBackoutBillAmt", "Originating Backout Bill Amt", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigRetentionFeeAmt", "Originating Retention Fee Amt", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigRetainerFeeAmt", "Originating Retainer Fee Amt", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigTotalBilings", "Originating Total Billings", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigDiscTakenAmt", "Originating Discount Taken Amt", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigDiscRetAmt", "Originating Discount Returned Amt", DataConnector.FieldTypeIdCurrency);
            pa23100.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            pa23100.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            pa23100.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            rm00101.AddField("RM00101.CUSTNAME", "Customer Name", DataConnector.FieldTypeIdString);

            var miscTaxable = pa23100.AddField("MISCTXBL", "Misc Taxable", DataConnector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var freightTaxable = pa23100.AddField("FRGTTXBL", "Freight Taxable", DataConnector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
        }

    }
}
