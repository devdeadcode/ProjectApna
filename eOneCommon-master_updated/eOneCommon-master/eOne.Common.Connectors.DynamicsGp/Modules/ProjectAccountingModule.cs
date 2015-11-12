using System.Collections.Generic;

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

        public ConnectorEntity GetContractEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListContracts), "Contracts", ParentConnector);
            
            var pa01101 = entity.AddTable("PA01101");
            
            AddContractEntityFields(pa01101);
            
            return entity;
        }
        public void AddContractEntityFields(ConnectorTable pa01101)
        {
            pa01101.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            pa01101.AddField("PAcontid", "Contract ID", Connector.FieldTypeIdString, true);
            pa01101.AddField("PAcontname", "PA Contract name", Connector.FieldTypeIdString, true);
            pa01101.AddField("PACONTNUMBER", "PA Contract number", Connector.FieldTypeIdString, true);
            pa01101.AddField("PAcontclassid", "PA Contract Class ID", Connector.FieldTypeIdString);
            pa01101.AddField("PApurordnum", "PA Purchase Order No.", Connector.FieldTypeIdString);
            pa01101.AddField("PABBegindate", "PA Baseline Begin date", Connector.FieldTypeIdDate);
            pa01101.AddField("PABEnddate", "PA Baseline End date", Connector.FieldTypeIdDate);
            pa01101.AddField("PAFBegindate", "PA Forecast Begin date", Connector.FieldTypeIdDate);
            pa01101.AddField("PAFEnddate", "PA Forecast End date", Connector.FieldTypeIdDate);
            pa01101.AddField("PAACTUALBEGdate", "PA Actual Begin date", Connector.FieldTypeIdDate);
            pa01101.AddField("PA_Actual_End_date", "PA Actual End date", Connector.FieldTypeIdDate);
            pa01101.AddField("PAcloseProjcosts", "PA Close to Project Costs", Connector.FieldTypeIdYesNo);
            pa01101.AddField("PAclosetobillings", "PA Close to Billings", Connector.FieldTypeIdYesNo);
            pa01101.AddField("PAContMgrID", "PA Contract Manager ID", Connector.FieldTypeIdString);
            pa01101.AddField("PABusMgrID", "PA Business Manager ID", Connector.FieldTypeIdString);
            pa01101.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            pa01101.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            pa01101.AddField("SALSTERR", "Sales Territory", Connector.FieldTypeIdString);
            pa01101.AddField("COMPRCNT", "Commission Percent", Connector.FieldTypeIdPercentage);
            pa01101.AddField("PABILLFORMAT", "PA Bill Format", Connector.FieldTypeIdString);
            pa01101.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            pa01101.AddField("CNTCPRSN", "Contact Person", Connector.FieldTypeIdString);
            pa01101.AddField("PRBTADCD", "Primary Billto Address code", Connector.FieldTypeIdString);
            pa01101.AddField("PAUD1_Cont", "User defined 1", Connector.FieldTypeIdString);
            pa01101.AddField("PAUD2_Cont", "User defined 2", Connector.FieldTypeIdString);
            pa01101.AddField("PABQuantity", "Baseline quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PABTotalCost", "Baseline Total cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABBillings", "Baseline Billings", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABProfit", "Baseline Profit", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABTaxPaidAmt", "Baseline Tax Paid amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABTaxChargedAmt", "Baseline Tax Charged amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABaselineOvhdCost", "Baseline Overhead cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAFQuantity", "Forecast quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAFTotalCost", "Forecast Total cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAFBillings", "Forecast Billings", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAFProfit", "Forecast Profit", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAFTaxPaidAmt", "Forecast Tax Paid amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAFTaxChargedAmt", "Forecast Tax Charged amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAForecastOvhdCost", "Forecast Overhead cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAProject_Amount", "Contract amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedQty", "Unposted quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAUnpostedTotalCostN", "Unposted Total cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Overhead", "Unposted Overhead", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedProfitN", "Unposted Profit", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Tax_Amount", "Unposted tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostAccrRevN", "Unposted Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedCommitedQty", "Unposted Committed quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAUnpostedCommitedCost", "Unposted Committed cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedCommitedTaxAmt", "Unposted Committed tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedRecogRevN", "Unposted Recognized Revenue", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Project_Fee", "Unposted Project Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Retainer_Fee", "Unposted Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Service_Fee", "Unposted Service Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUNPOSTRETAMT", "Unposted Retention amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUNPOSTBIEEAMOUNT", "Unposted BIEE amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUNPEIEBAMOUNT", "Unposted EIEB amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Unposted_Billed_Reten", "Unposted Billed Retention", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedQty", "Actual quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAPostedTotalCostN", "Actual Total cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Overhead", "Actual Overhead", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedProfitN", "Actual Profit", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Tax_Amount", "Actual tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Accr_RevN", "Actual Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedCommitedQty", "Actual Committed quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAPostedCommitedCost", "Actual Committed cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedCommitedTaxAmt", "Actual Committed tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostRecogRevN", "Actual Recognized Revenue", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Project_Fee", "Actual Project Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Retainer_Fee", "Actual Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Service_Fee", "Actual Service Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOSTRETAMT", "Actual Retention amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOSBIEEAMOUNT", "Actual BIEE amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOSEIEBAMOUNT", "Actual EIEB amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Actual_Billed_Retenti", "Actual Billed Retention", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAWrite_UpDown_Amount", "Write Up/Down amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABilled_QtyN", "Billed quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PABilled_Cost", "Billed cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABilled_Accrued_Revenu", "Billed Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PACostPcntCompleted", "Cost Percent Completed", Connector.FieldTypeIdPercentage);
            pa01101.AddField("PAQuantityPcntCompleted", "Quantity Percent Completed", Connector.FieldTypeIdPercentage);
            pa01101.AddField("PA_Receipts_Amount", "Receipts amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Actual_Receipts_Amoun", "Actual Receipts amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Earnings", "Earnings", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Cost_of_Earnings", "Cost of Earnings", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostBillN", "Unposted Billings", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostDiscDolAmtN", "Unposted Discount amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnposted_Sales_Tax_Am", "Unposted Sales tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedBillingsN", "Actual Billings", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedDiscDolAmtN", "Actual Discount amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Sales_Tax_Amou", "Actual Sales tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAService_Fee_Amount", "Service Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PARetainer_Fee_Amount", "Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAProject_Fee_Amount", "Project Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PARetentionFeeAmount", "Retention Fee amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("DSCPCTAM", "Discount Percent", Connector.FieldTypeIdPercentage);
            pa01101.AddField("PABCWPAMT", "BCWP amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PABCWSAMT", "BCWS amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAACWPAMT", "ACWP amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAApproved_Accrued_Reve", "Approved Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAApproved_Cost", "Approved cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAApproved_Quantity", "Approved quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("WROFAMNT", "Write Off amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("ActualWriteOffAmount", "Actual Write Off amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("ActualDiscTakenAmount", "Actual Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PACommitted_Costs", "Committed Costs", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PACommitted_Qty", "Committed quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAPOCost", "PO cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPOQty", "PO quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAPOPostedCost", "PO Actual cost", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPostedQty", "PO Actual quantity", Connector.FieldTypeIdQuantity);
            pa01101.AddField("PAtaxpaidamt", "Tax Paid amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PApretainage", "Actual Retainage", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAunpretainage", "Unposted Retainage", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Write_Off_Tax_Amount", "Write Off tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualWOTaxAmt", "Actual Write Off tax amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PA_Terms_Taken_Tax_Amt", "Terms Taken amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualTermsTakenTax", "Actual Terms Taken amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAUnpostedLossAmount", "Unposted Loss amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualLossAmount", "Actual Loss amount", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAPosted_Earnings", "Actual Earnings", Connector.FieldTypeIdCurrency);
            pa01101.AddField("PAActualCostofEarnings", "Actual Cost of Earnings", Connector.FieldTypeIdCurrency);

            var projectType = pa01101.AddField("PAProjectType", "Project type", Connector.FieldTypeIdEnum);
            projectType.AddListItems(1, new List<string> { "Time and Materials", "Cost Plus", "Fixed price" });

            var accountingMethod = pa01101.AddField("PAAcctgMethod", "Accounting method", Connector.FieldTypeIdEnum);
            accountingMethod.AddListItems(1, new List<string> { "When Performed", "When Billed", "Cost-to-Cost", "Effort-Expended", "Completed", "Effort Expended - Labor Only" });

            var status = pa01101.AddField("PASTAT", "Status", Connector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Open", "On Hold", "Closed", "Estimate", "Completed" });
        }

        public ConnectorEntity GetProjectEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListProjects), "Projects", ParentConnector);
            
            var pa01201 = entity.AddTable("PA01201");
            
            AddProjectEntityFields(pa01201);
            
            return entity;
        }
        public void AddProjectEntityFields(ConnectorTable pa01201)
        {
            pa01201.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            pa01201.AddField("PACONTNUMBER", "Contract number", Connector.FieldTypeIdString, true);
            pa01201.AddField("PAcontid", "Contract ID", Connector.FieldTypeIdString, true);
            pa01201.AddField("PAprojid", "Project ID", Connector.FieldTypeIdString, true);
            pa01201.AddField("PAprojname", "Project name", Connector.FieldTypeIdString, true);
            pa01201.AddField("PAPROJNUMBER", "Project number", Connector.FieldTypeIdString, true);
            pa01201.AddField("PAprjclsid", "Project Class ID", Connector.FieldTypeIdString);
            var projectType = pa01201.AddField("PAProjectType", "Project type", Connector.FieldTypeIdEnum, true);
            projectType.AddListItems(1, new List<string> { "Time and Materials", "Cost Plus", "Fixed price" });
            var status = pa01201.AddField("PASTAT", "Status", Connector.FieldTypeIdEnum, true);
            status.AddListItems(1, new List<string> { "Open", "On Hold", "Closed", "Estimate", "Completed" });
            pa01201.AddField("PApurordnum", "Purchase Order No.", Connector.FieldTypeIdString);
            pa01201.AddField("PABBegindate", "Baseline Begin date", Connector.FieldTypeIdDate);
            pa01201.AddField("PABEnddate", "Baseline End date", Connector.FieldTypeIdDate);
            pa01201.AddField("PABQuantity", "Baseline quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PABTotalCost", "Baseline Total cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABProfit", "Baseline Profit", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABBillings", "Baseline Billings", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABTaxPaidAmt", "Baseline Tax Paid amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABTaxChargedAmt", "Baseline Tax Charged amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABaselineOvhdCost", "Baseline Overhead cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAACTUALBEGdate", "Actual Begin date", Connector.FieldTypeIdDate);
            pa01201.AddField("PA_Actual_End_date", "Actual End date", Connector.FieldTypeIdDate);
            pa01201.AddField("PAFBegindate", "Forecast Begin date", Connector.FieldTypeIdDate);
            pa01201.AddField("PAFEnddate", "Forecast End date", Connector.FieldTypeIdDate);
            pa01201.AddField("PAFQuantity", "Forecast quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAFTotalCost", "Forecast Total cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAFProfit", "Forecast Profit", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAFBillings", "Forecast Billings", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAFTaxPaidAmt", "Forecast Tax Paid amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAFTaxChargedAmt", "Forecast Tax Charged amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAForecastOvhdCost", "Forecast Overhead cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAcloseProjcosts", "Close to Project Costs", Connector.FieldTypeIdYesNo);
            pa01201.AddField("PAclosetobillings", "Close to Billings", Connector.FieldTypeIdYesNo);
            pa01201.AddField("PADepartment", "Department", Connector.FieldTypeIdString);
            pa01201.AddField("PAEstimatorID", "Estimator ID", Connector.FieldTypeIdString);
            pa01201.AddField("PAprojmngrid", "Project Manager ID", Connector.FieldTypeIdString);
            pa01201.AddField("PABusMgrID", "Business Manager ID", Connector.FieldTypeIdString);
            pa01201.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            pa01201.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            pa01201.AddField("SALSTERR", "Sales Territory", Connector.FieldTypeIdString);
            pa01201.AddField("COMPRCNT", "Commission Percent", Connector.FieldTypeIdPercentage);
            pa01201.AddField("CNTCPRSN", "Contact Person", Connector.FieldTypeIdString);
            pa01201.AddField("PRBTADCD", "Primary Billto Address code", Connector.FieldTypeIdString);
            pa01201.AddField("PALabor_Rate_Table_ID", "Labor Rate Table ID", Connector.FieldTypeIdString);
            pa01201.AddField("PALabor_Rate_Table_Acc", "Labor Rate Table Accept", Connector.FieldTypeIdYesNo);
            pa01201.AddField("PAEquip_Rate_Table_ID", "Equip Rate Table ID", Connector.FieldTypeIdString);
            pa01201.AddField("PAEquip_Rate_Table_Acc", "Equip Rate Table Accept", Connector.FieldTypeIdYesNo);
            pa01201.AddField("PAService_Fee_Amount", "Service Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAProject_Fee_Amount", "Project Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PARetainer_Fee_Amount", "Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PARetentionFeeAmount", "Retention Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAProject_Amount", "Project amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("ACCTAMNT", "Account amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABILLFORMAT", "Bill Format", Connector.FieldTypeIdString);
            pa01201.AddField("DSCPCTAM", "Discount Percent", Connector.FieldTypeIdPercentage);
            pa01201.AddField("PA_Retention_Percent", "Retention Percent", Connector.FieldTypeIdPercentage);
            pa01201.AddField("PAUD1Proj", "User defined 1", Connector.FieldTypeIdString);
            pa01201.AddField("PAUD2_Proj", "User defined 2", Connector.FieldTypeIdString);
            pa01201.AddField("PAUnpostedQty", "Unposted quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAUnpostedTotalCostN", "Unposted Total cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Overhead", "Unposted Overhead", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedProfitN", "Unposted Profit", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Tax_Amount", "Unposted tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostAccrRevN", "Unposted Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedRecogRevN", "Unposted Recognized Revenue", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedCommitedQty", "Unposted Committed quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAUnpostedCommitedCost", "Unposted Committed cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostedCommitedTaxAmt", "Unposted Committed tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Project_Fee", "Unposted Project Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Retainer_Fee", "Unposted Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Service_Fee", "Unposted Service Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUNPOSTRETAMT", "Unposted Retention amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUNPOSTBIEEAMOUNT", "Unposted BIEE amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUNPEIEBAMOUNT", "Unposted EIEB amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Unposted_Billed_Reten", "Unposted Billed Retention", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedQty", "Actual quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAPostedTotalCostN", "Actual Total cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedProfitN", "Actual Profit", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Tax_Amount", "Actual tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Accr_RevN", "Actual Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostRecogRevN", "Actual Recognized Revenue", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedCommitedQty", "Actual Committed quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAPostedCommitedCost", "Actual Committed cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedCommitedTaxAmt", "Actual Committed tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Project_Fee", "Actual Project Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Retainer_Fee", "Actual Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Service_Fee", "Actual Service Fee amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOSTRETAMT", "Actual Retention amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOSBIEEAMOUNT", "Actual BIEE amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOSEIEBAMOUNT", "Actual EIEB amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Actual_Billed_Retenti", "Actual Billed Retention", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAWrite_UpDown_Amount", "Write Up/Down amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABilled_QtyN", "Billed quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PABilled_Cost", "Billed cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABilled_Accrued_Revenu", "Billed Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PACostPcntCompleted", "Cost Percent Completed", Connector.FieldTypeIdPercentage);
            pa01201.AddField("PAQuantityPcntCompleted", "Quantity Percent Completed", Connector.FieldTypeIdPercentage);
            pa01201.AddField("PA_Receipts_Amount", "Receipts amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Actual_Receipts_Amoun", "Actual Receipts amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Earnings", "Earnings", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Cost_of_Earnings", "Cost of Earnings", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostBillN", "Unposted Billings", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnpostDiscDolAmtN", "Unposted Discount amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAUnposted_Sales_Tax_Am", "Unposted Sales tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedBillingsN", "Actual Billings", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedDiscDolAmtN", "Actual Discount amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Sales_Tax_Amou", "Actual Sales tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABCWPAMT", "BCWP amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PABCWSAMT", "BCWS amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAACWPAMT", "ACWP amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            pa01201.AddField("PAPosted_Overhead", "Actual Overhead", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAApproved_Accrued_Reve", "Approved Accrued revenues", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAApproved_Cost", "Approved cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAApproved_Quantity", "Approved quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("WROFAMNT", "Write Off amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("ActualWriteOffAmount", "Actual Write Off amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("ActualDiscTakenAmount", "Actual Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PACommitted_Costs", "Committed Costs", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PACommitted_Qty", "Committed quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAPOCost", "PO cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOQty", "PO quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAPOPostedCost", "PO Actual cost", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPOPostedQty", "PO Actual quantity", Connector.FieldTypeIdQuantity);
            pa01201.AddField("PAtaxpaidamt", "Tax Paid amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPostedTaxPaidN", "Actual Tax Paid", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PApretainage", "Actual Retainage", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAunpretainage", "Unposted Retainage", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Write_Off_Tax_Amount", "Write Off tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualWOTaxAmt", "Actual Write Off tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PA_Terms_Taken_Tax_Amt", "Terms Taken tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualTermsTakenTax", "Actual Terms Taken tax amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("WRKRCOMP", "Workers Comp", Connector.FieldTypeIdString);
            pa01201.AddField("PAUnpostedLossAmount", "Unposted Loss amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualLossAmount", "Actual Loss amount", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAPosted_Earnings", "Actual Earnings", Connector.FieldTypeIdCurrency);
            pa01201.AddField("PAActualCostofEarnings", "Actual Cost of Earnings", Connector.FieldTypeIdCurrency);

            var accountingMethod = pa01201.AddField("PAAcctgMethod", "Accounting method", Connector.FieldTypeIdEnum);
            accountingMethod.AddListItems(1, new List<string> { "When Performed", "When Billed", "Cost-to-Cost", "Effort-Expended", "Completed", "Effort Expended - Labor Only" });

            var laborRateTableType = pa01201.AddField("PALabor_RateTable_Type", "Labor Rate Table type", Connector.FieldTypeIdEnum);
            laborRateTableType.AddListItems(1, new List<string> { "Employee", "Position" });

            var billingType = pa01201.AddField("PAbllngtype", "Billing type", Connector.FieldTypeIdEnum);
            billingType.AddListItems(1, new List<string> { "STD", "N/C", "N/B" });
        }
            
        public ConnectorEntity GetCostCategoryEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListCostCategories), "Cost categories", ParentConnector);
            
            var pa01001 = entity.AddTable("PA01001");

            var pa41001 = entity.AddTable("PA41001", "PA01001");
            pa41001.AddJoinFields("PACOSTCATCLASID", "PACOSTCATCLASID");
            
            AddCostCategoryEntityFields(pa01001, pa41001);
            
            entity.AddCalculation("DECPLQTY - 1", "Decimal Places quantity", Connector.FieldTypeIdInteger);
            entity.AddCalculation("DECPLCUR - 1", "Decimal Places Currency", Connector.FieldTypeIdInteger);
            
            return entity;
        }
        public void AddCostCategoryEntityFields(ConnectorTable pa01001, ConnectorTable pa41001)
        {
            pa01001.AddField("PACOSTCATID", "Cost Category ID", Connector.FieldTypeIdString, true);
            pa01001.AddField("PACOSTCATNME", "Name", Connector.FieldTypeIdString, true);
            pa01001.AddField("PAinactive", "Inactive", Connector.FieldTypeIdYesNo);
            pa01001.AddField("PAIV_Item_Checkbox", "Inventory Item", Connector.FieldTypeIdYesNo);
            pa01001.AddField("PACOSTCATCLASID", "Cost Category Class ID", Connector.FieldTypeIdString);
            pa01001.AddField("PAPay_Code_Hourly", "Paycode - Hourly", Connector.FieldTypeIdString);
            pa01001.AddField("PAPay_Code_Salary", "Paycode - Salary", Connector.FieldTypeIdString);
            pa01001.AddField("UOMSCHDL", "U of M Schedule", Connector.FieldTypeIdString);
            pa01001.AddField("PAUnit_of_Measure", "Unit of Measure", Connector.FieldTypeIdString);
            pa01001.AddField("PAUNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            pa01001.AddField("PAcostaxscheduleid", "Purchase Tax Schedule ID", Connector.FieldTypeIdString);
            pa01001.AddField("PAbilltaxscheduleid", "Sales Tax Schedule ID", Connector.FieldTypeIdString);
            pa41001.AddField("PA41001.PACOSTCATCLASNME", "Class ID description", Connector.FieldTypeIdString);
            pa01001.AddField("PAProfitPercentCP", "Profit Percent - CP", Connector.FieldTypeIdPercentage);
            pa01001.AddField("PAFFProfitPercent", "Profit Percent - FP", Connector.FieldTypeIdPercentage);
            pa01001.AddField("PATMProfitPercent", "Profit Percent - TM", Connector.FieldTypeIdPercentage);
            pa01001.AddField("PAProfitAmountCP", "Profit Amount - CP", Connector.FieldTypeIdCurrency);
            pa01001.AddField("PAFFProfitAmount", "Profit Amount - FP", Connector.FieldTypeIdCurrency);
            pa01001.AddField("PATMProfitAmount", "Profit Amount - TM", Connector.FieldTypeIdCurrency);
            pa01001.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa01001.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa01001.AddField("PAOvhdAmtPerUnit", "Overhead Amount Per Unit", Connector.FieldTypeIdCurrency);
            pa01001.AddField("PAOverheaPercentage", "Overhead Percentage", Connector.FieldTypeIdPercentage);
         
            var transactionUsage = pa01001.AddField("PATU", "Transaction Usage", Connector.FieldTypeIdEnum);
            transactionUsage.AddListItems(1, new List<string> { "Timesheet", "Equipment Log", "Miscellaneous Log", "Purchases/Material", "Employee Expense" });

            var purchaseTaxOption = pa01001.AddField("PAPurchase_Tax_Options", "Purchase Tax Option", Connector.FieldTypeIdEnum);
            purchaseTaxOption.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var salesTaxOption = pa01001.AddField("PASales_Tax_Options", "Sales Tax Option", Connector.FieldTypeIdEnum);
            salesTaxOption.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var profitTypeCp = pa01001.AddField("PAProfit_Type__CP", "Profit Type - CP", Connector.FieldTypeIdEnum);
            profitTypeCp.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeFp = pa01001.AddField("PAFFProfitType", "Profit Type - FP", Connector.FieldTypeIdEnum);
            profitTypeFp.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeTm = pa01001.AddField("PATMProfitType", "Profit Type - TM", Connector.FieldTypeIdEnum);
            profitTypeTm.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });
        }
            
        public ConnectorEntity GetEquipmentEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListEquipment), "Equipment", ParentConnector);

            var pa00701 = entity.AddTable("PA00701");

            var pa40701 = entity.AddTable("PA40701", "PA00701");
            pa40701.AddJoinFields("PAEQUIPTCLSID", "PAEQUIPTCLSID");
            
            AddEquipmentEntityFields(pa00701, pa40701);
            
            return entity;
        }
        public void AddEquipmentEntityFields(ConnectorTable pa00701, ConnectorTable pa40701)
        {
            pa00701.AddField("PAEQUIPTID", "Equipment ID", Connector.FieldTypeIdString, true);
            pa00701.AddField("PAEQNME", "Name", Connector.FieldTypeIdString, true);
            pa00701.AddField("PAinactive", "Inactive", Connector.FieldTypeIdYesNo);
            pa00701.AddField("PAEQUIPTCLSID", "Class ID", Connector.FieldTypeIdString);
            pa40701.AddField("PAEQPTCLSNME", "Class ID description", Connector.FieldTypeIdString);
            pa00701.AddField("PAUnit_of_Measure", "Unit of Measure", Connector.FieldTypeIdString);
            pa00701.AddField("PAUNITCOST", "Unit cost", Connector.FieldTypeIdString);
            pa00701.AddField("PATMProfitAmount", "Billing Rate - TM", Connector.FieldTypeIdCurrency);
            pa00701.AddField("PATMProfitPercent", "Markup Percent - TM", Connector.FieldTypeIdPercentage);
            pa00701.AddField("PAProfitAmountCP", "Profit Amount - CP", Connector.FieldTypeIdCurrency);
            pa00701.AddField("PAFFProfitAmount", "Profit Amount - FP", Connector.FieldTypeIdCurrency);
            pa00701.AddField("PATMProfitAmount", "Profit Amount - TM", Connector.FieldTypeIdCurrency);
            pa00701.AddField("PAProfitPercentCP", "Profit Percent - CP", Connector.FieldTypeIdPercentage);
            pa00701.AddField("PAFFProfitPercent", "Profit Percent - FP", Connector.FieldTypeIdPercentage);
            pa00701.AddField("PATMProfitPercent", "Profit Percent - TM", Connector.FieldTypeIdPercentage);
            pa00701.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa00701.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);

            var profitTypeTm = pa00701.AddField("PATMProfitType", "Profit Type - TM", Connector.FieldTypeIdEnum);
            profitTypeTm.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeCp = pa00701.AddField("PAProfit_Type__CP", "Profit Type - CP", Connector.FieldTypeIdEnum);
            profitTypeCp.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeFp = pa00701.AddField("PAFFProfitType", "Profit Type - FP", Connector.FieldTypeIdEnum);
            profitTypeFp.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });
        }
            
        public ConnectorEntity GetMiscellaneousEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListMiscellaneous), "Miscellaneous", ParentConnector);
            
            var pa00801 = entity.AddTable("PA00801");

            var pa40801 = entity.AddTable("PA40801", "PA00801");
            pa40801.AddJoinFields("PAMISCCID", "PAMISCCID");
            
            AddMiscellaneousEntityFields(pa00801, pa40801);
            
            return entity;
        }
        public void AddMiscellaneousEntityFields(ConnectorTable pa00801, ConnectorTable pa40801)
        {
            pa00801.AddField("PSMISCID", "Miscellaneous ID", Connector.FieldTypeIdString, true);
            pa00801.AddField("PAMISCEN", "Name", Connector.FieldTypeIdString, true);
            pa00801.AddField("PAinactive", "Inactive", Connector.FieldTypeIdYesNo);
            pa00801.AddField("PAMISCCID", "Miscellaneous Class ID", Connector.FieldTypeIdString);
            pa00801.AddField("PAUnit_of_Measure", "Unit of Measure", Connector.FieldTypeIdString);
            pa00801.AddField("PAUNITCOST", "Unit cost", Connector.FieldTypeIdCurrency);
            pa40801.AddField("PAMCN", "Class ID description", Connector.FieldTypeIdString);
            pa00801.AddField("PATMProfitAmount", "Billing Rate - TM", Connector.FieldTypeIdCurrency);
            pa00801.AddField("PATMProfitPercent", "Markup Percent - TM", Connector.FieldTypeIdPercentage);
            pa00801.AddField("PAProfitAmountCP", "Profit Amount - CP", Connector.FieldTypeIdCurrency);
            pa00801.AddField("PAFFProfitAmount", "Profit Amount - FP", Connector.FieldTypeIdCurrency);
            pa00801.AddField("PATMProfitAmount", "Profit Amount - TM", Connector.FieldTypeIdCurrency);
            pa00801.AddField("PAProfitPercentCP", "Profit Percent - CP", Connector.FieldTypeIdPercentage); 
            pa00801.AddField("PAFFProfitPercent", "Profit Percent - FP", Connector.FieldTypeIdPercentage);
            pa00801.AddField("PATMProfitPercent", "Profit Percent - TM", Connector.FieldTypeIdPercentage);
            pa00801.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa00801.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);

            var profitTypeTm = pa00801.AddField("PATMProfitType", "Profit Type - TM", Connector.FieldTypeIdEnum);
            profitTypeTm.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeCp = pa00801.AddField("PAProfit_Type__CP", "Profit Type - CP", Connector.FieldTypeIdEnum);
            profitTypeCp.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });

            var profitTypeFp = pa00801.AddField("PAFFProfitType", "Profit Type - FP", Connector.FieldTypeIdEnum);
            profitTypeFp.AddListItems(1, new List<string> { "Billing rate", "Markup %", "Profit/Unit-Fixed", "Profit/Unit-Variable", "Total Profit", "% of Basleine", "% of Actual", "None", "Price Level" });
        }
            
        public ConnectorEntity GetBillingCycleEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListBillingCycle), "Billing cycles", ParentConnector);
            
            var pa02000 = entity.AddTable("PA02000");
            
            AddBillingCycleEntityFields(pa02000);
            
            return entity;
        }
        public void AddBillingCycleEntityFields(ConnectorTable pa02000)
        {
            pa02000.AddField("PABILLCYCLEID", "Billing Cycle ID", Connector.FieldTypeIdString, true);
            pa02000.AddField("PABILLCYCLEDESC", "Description", Connector.FieldTypeIdString, true);
            pa02000.AddField("PAPrior_Days_Before_Bi", "Days Before Billing", Connector.FieldTypeIdInteger, true);
            var frequency = pa02000.AddField("PABilling_Frequency", "Frequency", Connector.FieldTypeIdEnum, true);
            frequency.AddListItems(1, new List<string> { "Daily", "Weekly", "Semimonthly", "Monthly", "Quarterly", "Semiannually", "Annually" });
            pa02000.AddField("PA_Include_Project_Fee", "Include Project Fee", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbfee", "Include Service Fee", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PA_Include_Retainer_Fee", "Include Retainer Fee", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbts", "Include Timesheets", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbinv", "Include Purchase Invoice", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbEL", "Include Equipment Logs", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcber", "Include Employee Expense", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbML", "Include Miscellaneous Logs", Connector.FieldTypeIdYesNo);
            pa02000.AddField("PAtotcbvi", "Include Inventory", Connector.FieldTypeIdYesNo);
        }
            
        public ConnectorEntity GetEmployeeExpenseTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListEmployeeExpenseTrx), "Employee expense transactions", ParentConnector);
            
            var pa10500 = entity.AddTable("PA10500");
            
            AddEmployeeExpenseTransactionEntityFields(pa10500);
            
            return entity;
        }
        public void AddEmployeeExpenseTransactionEntityFields(ConnectorTable pa10500)
        {
            var transactionType = pa10500.AddField("PAertrxtype", "Transaction type", Connector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10500.AddField("PAerdocnumber", "Empl Expense Document No.", Connector.FieldTypeIdString, true);
            pa10500.AddField("PDK_EE_No", "PDK EE No.", Connector.FieldTypeIdString, true);
            pa10500.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa10500.AddField("USERID", "User ID", Connector.FieldTypeIdString, true);
            pa10500.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            pa10500.AddField("PAStartdate", "Start date", Connector.FieldTypeIdDate, true);
            pa10500.AddField("PAEndate", "End date", Connector.FieldTypeIdDate, true);
            pa10500.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa10500.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa10500.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency, true);
            pa10500.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa10500.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa10500.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa10500.AddField("PAREFNO", "Reference Document No.", Connector.FieldTypeIdString);
            pa10500.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pa10500.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa10500.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa10500.AddField("PAVENADDRESSID", "Address ID", Connector.FieldTypeIdString);
            pa10500.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdString);
            pa10500.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa10500.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            pa10500.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            pa10500.AddField("DSCDLRAM", "Discount amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORDDLRAT", "Originating Discount amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            pa10500.AddField("DISAMTAV", "Discount Amount Available", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ODISAMTAV", "Originating Discount Amount Available", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PRCTDISC", "Percent Discount", Connector.FieldTypeIdPercentage);
            pa10500.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa10500.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa10500.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            pa10500.AddField("PAEXTCOST", "Extended cost", Connector.FieldTypeIdCurrency);
            pa10500.AddField("OREXTCST", "Originating Extended cost", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PAREIMBURSTAXAMT", "Reimbursable tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PAORIGREIMTAXAMT", "Originating Reimbursable tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("BKTPURAM", "Backout Purchases amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa10500.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            pa10500.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORFRTTAX", "Originating Freight tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdCurrency);
            pa10500.AddField("MSCCHAMT", "Misc Charges amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("OMISCAMT", "Originating Misc Charges amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORMSCTAX", "Originating Misc tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORDOCAMT", "Originating Document amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("TEN99AMNT", "1099 amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("OR1099AM", "Originating 1099 amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("UN1099AM", "Unapplied 1099 amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("CASHAMNT", "Cash amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORCASAMT", "Originating Cash amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("CAMCBKID", "Cash Amount Checkbook ID", Connector.FieldTypeIdString);
            pa10500.AddField("CDOCNMBR", "Cash Document number", Connector.FieldTypeIdString);
            pa10500.AddField("CAMTdate", "Cash Amount date", Connector.FieldTypeIdDate);
            pa10500.AddField("CAMPMTNM", "Cash Amount Payment number", Connector.FieldTypeIdString);
            pa10500.AddField("CHRGAMNT", "Charge amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("OCHGAMT", "Originating Charge amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("CHEKAMNT", "Check amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORCHKAMT", "Originating Check amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString);
            pa10500.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            pa10500.AddField("CAMPYNBR", "Check Amount Payment number", Connector.FieldTypeIdString);
            pa10500.AddField("CHAMCBID", "Check Amount Checkbook ID", Connector.FieldTypeIdString);
            pa10500.AddField("CARDNAME", "Card name", Connector.FieldTypeIdString);
            pa10500.AddField("CRCRDAMT", "Credit Card amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORCCDAMT", "Originating Credit Card amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("CRCARDDT", "Credit Card date", Connector.FieldTypeIdDate);
            pa10500.AddField("CCAMPYNM", "Credit Card Amount Payment number", Connector.FieldTypeIdString);
            pa10500.AddField("CCRCTNUM", "Credit Card Receipt number", Connector.FieldTypeIdString);
            pa10500.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORDISTKN", "Originating Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORDAVFRT", "Originating Discount Available Freight", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ODAVPUR", "Originating Discount Available Purchases", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORDAVMSC", "Originating Discount Available Misc", Connector.FieldTypeIdCurrency);
            pa10500.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            pa10500.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("BCKTXAMT", "Backout tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("OBTAXAMT", "Originating Backout tax amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PAReimbursableAmount", "PA Reimbursable amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PAOrigReimbursableAmt", "PA Originating Reimbursable amount", Connector.FieldTypeIdCurrency);
            pa10500.AddField("PAPD", "PA Post date", Connector.FieldTypeIdDate);
            pa10500.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pa10500.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            pa10500.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            pa10500.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);

            var freightTaxablePurchase = pa10500.AddField("PAFreight_Taxable_P", "Freight Taxable - Purchase", Connector.FieldTypeIdEnum);
            freightTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var miscTaxablePurchase = pa10500.AddField("PAMisc_Taxable_P", "Misc Taxable - Purchase", Connector.FieldTypeIdEnum);
            miscTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
        }

        public ConnectorEntity GetEquipmentLogTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListEquipmentLogTrx), "Equipment log transactions", ParentConnector);
            
            var pa10100 = entity.AddTable("PA10100");
            
            AddEquipmentLogTransactionEntityFields(pa10100);
            
            return entity;
        }
        public void AddEquipmentLogTransactionEntityFields(ConnectorTable pa10100)
        {
            var transactionType = pa10100.AddField("PAEQLTRX", "Transaction type", Connector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10100.AddField("PAEQLOGNO", "Equipment Log No.", Connector.FieldTypeIdString, true);
            pa10100.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa10100.AddField("PAEQUIPTID", "Equipment ID", Connector.FieldTypeIdString, true);
            pa10100.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa10100.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa10100.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa10100.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa10100.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa10100.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa10100.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa10100.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa10100.AddField("PAREFNO", "Reference Document No.", Connector.FieldTypeIdString);
            pa10100.AddField("PAREPD", "Reporting period", Connector.FieldTypeIdInteger);
            pa10100.AddField("PAREPDT", "Reporting date", Connector.FieldTypeIdDate);
            pa10100.AddField("PAPeriodEnddate", "Period End date", Connector.FieldTypeIdDate);
            pa10100.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa10100.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa10100.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa10100.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdString);
            pa10100.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa10100.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa10100.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa10100.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);
        }

        public ConnectorEntity GetIvTransferTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListIvTransferTrx), "Inventory transfer transactions", ParentConnector);
            
            var pa10900 = entity.AddTable("PA10900");
            
            AddIvTransferTransactionEntityFields(pa10900);
            
            return entity;
        }
        public void AddIvTransferTransactionEntityFields(ConnectorTable pa10900)
        {
            pa10900.AddField("PAIV_Document_No", "PA IV Document number", Connector.FieldTypeIdString, true);
            var ivTransferType = pa10900.AddField("PAIV_Transfer_Type", "IV Transfer type", Connector.FieldTypeIdEnum, true);
            ivTransferType.AddListItems(1, new List<string> { "Standard", "Return", "Return From Project" });
            pa10900.AddField("IVDOCNBR", "IV Document number", Connector.FieldTypeIdString, true);
            pa10900.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa10900.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            pa10900.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa10900.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa10900.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa10900.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa10900.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa10900.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa10900.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa10900.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa10900.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa10900.AddField("REQSTDBY", "Requested By", Connector.FieldTypeIdString);
            pa10900.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa10900.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
        }

        public ConnectorEntity GetMiscLogTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListMiscLogTrx), "Miscellaneous log transactions", ParentConnector);
            
            var pa10200 = entity.AddTable("PA10200");
            
            AddMiscLogTransactionEntityFields(pa10200);
            
            return entity;
        }
        public void AddMiscLogTransactionEntityFields(ConnectorTable pa10200)
        {
            var transactionType = pa10200.AddField("PSMISCLTRXTYPE", "Transaction type", Connector.FieldTypeIdEnum, true); 
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10200.AddField("PAMISCLDOCNO", "Miscellaneous Log No.", Connector.FieldTypeIdString, true);
            pa10200.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa10200.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa10200.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa10200.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa10200.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa10200.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa10200.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa10200.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa10200.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa10200.AddField("PAREFNO", "Reference Document No.", Connector.FieldTypeIdString);
            pa10200.AddField("PSMISCID", "Miscellaneous ID", Connector.FieldTypeIdString);
            pa10200.AddField("PAREPD", "Reporting period", Connector.FieldTypeIdInteger);
            pa10200.AddField("PAREPDT", "Reporting date", Connector.FieldTypeIdDate);
            pa10200.AddField("PAPeriodEnddate", "Period End date", Connector.FieldTypeIdDate);
            pa10200.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa10200.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa10200.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa10200.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdCurrency);
            pa10200.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa10200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa10200.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa10200.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);
        }
            
        public ConnectorEntity GetTimesheetTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListTimesheetTrx), "Timesheet transactions", ParentConnector);
            
            var pa10000 = entity.AddTable("PA10000");
            
            AddTimesheetTransactionEntityFields(pa10000);
            
            return entity;
        }
        public void AddTimesheetTransactionEntityFields(ConnectorTable pa10000)
        {
            var transactionType = pa10000.AddField("PATSTYP", "Transaction type", Connector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa10000.AddField("PATSNO", "Timesheet No.", Connector.FieldTypeIdString, true);
            pa10000.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate,true);
            pa10000.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            pa10000.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa10000.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa10000.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa10000.AddField("PDK_TS_No", "PDK Timesheet No.", Connector.FieldTypeIdString);
            pa10000.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa10000.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa10000.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa10000.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa10000.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            pa10000.AddField("PAREFNO", "Reference Doc No.", Connector.FieldTypeIdString);
            pa10000.AddField("PAREPD", "Reporting period", Connector.FieldTypeIdInteger);
            pa10000.AddField("PAREPDT", "Reporting date", Connector.FieldTypeIdDate);
            pa10000.AddField("PAPeriodEnddate", "Period End date", Connector.FieldTypeIdDate);
            pa10000.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa10000.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa10000.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa10000.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdString);
            pa10000.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa10000.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa10000.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa10000.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);

            var employedBy = pa10000.AddField("PA_Employed_By", "Employed By", Connector.FieldTypeIdEnum);
            employedBy.AddListItems(1, new List<string> { "Company", "Placement Agency", "Subcontractor" });
        }
            
        public ConnectorEntity GetMiscLogHistTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListMiscLogHistTrx), "Historical miscellaneous log transactions", ParentConnector);
            
            var pa30300 = entity.AddTable("PA30300");
            
            AddMiscLogHistTransactionEntityFields(pa30300);
            
            return entity;
        }
        public void AddMiscLogHistTransactionEntityFields(ConnectorTable pa30300)
        {
            var transactionType = pa30300.AddField("PSMISCLTRXTYPE", "Transaction type", Connector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30300.AddField("PAMISCLDOCNO", "Miscellaneous Log No.", Connector.FieldTypeIdString, true);
            pa30300.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa30300.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa30300.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa30300.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa30300.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa30300.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa30300.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa30300.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa30300.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa30300.AddField("PAREFNO", "Reference Document No.", Connector.FieldTypeIdString);
            pa30300.AddField("PSMISCID", "Miscellaneous ID", Connector.FieldTypeIdString);
            pa30300.AddField("PAREPD", "Reporting period", Connector.FieldTypeIdInteger);
            pa30300.AddField("PAREPDT", "Reporting date", Connector.FieldTypeIdDate);
            pa30300.AddField("PAPeriodEnddate", "Period End date", Connector.FieldTypeIdDate);
            pa30300.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa30300.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa30300.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa30300.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdCurrency);
            pa30300.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa30300.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa30300.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa30300.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);
        }
            
        public ConnectorEntity GetEquipmentLogHistTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListEquipmentLogHistTrx), "Historical equipment log transactions", ParentConnector);
            
            var pa30200 = entity.AddTable("PA30200");
            
            AddEquipmentLogHistTransactionEntityFields(pa30200);
            
            return entity;
        }
        public void AddEquipmentLogHistTransactionEntityFields(ConnectorTable pa30200)
        {
            var transactionType = pa30200.AddField("PAEQLTRX", "Transaction type", Connector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30200.AddField("PAEQLOGNO", "Equipment Log No.", Connector.FieldTypeIdString, true);
            pa30200.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa30200.AddField("PAEQUIPTID", "Equipment ID", Connector.FieldTypeIdString, true);
            pa30200.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa30200.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa30200.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa30200.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa30200.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa30200.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa30200.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa30200.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa30200.AddField("PAREFNO", "Reference Document No.", Connector.FieldTypeIdString);
            pa30200.AddField("PAREPD", "Reporting period", Connector.FieldTypeIdInteger);
            pa30200.AddField("PAREPDT", "Reporting date", Connector.FieldTypeIdDate);
            pa30200.AddField("PAPeriodEnddate", "Period End date", Connector.FieldTypeIdDate);
            pa30200.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa30200.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa30200.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa30200.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdString);
            pa30200.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa30200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa30200.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa30200.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);
        }
            
        public ConnectorEntity GetTimesheetHistTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListTimesheetHistTrx), "Historical timesheet transactions", ParentConnector);
            
            var pa30100 = entity.AddTable("PA30100");
            
            AddTimesheetHistTransactionEntityFields(pa30100);
            
            return entity;
        }
        public void AddTimesheetHistTransactionEntityFields(ConnectorTable pa30100)
        {
            var transactionType = pa30100.AddField("PATSTYP", "Transaction type", Connector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30100.AddField("PATSNO", "Timesheet No.", Connector.FieldTypeIdString, true);
            pa30100.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa30100.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            pa30100.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa30100.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa30100.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa30100.AddField("PDK_TS_No", "PDK Timesheet No.", Connector.FieldTypeIdString);
            pa30100.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa30100.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa30100.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa30100.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa30100.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            pa30100.AddField("PAREFNO", "Reference Doc No.", Connector.FieldTypeIdString);
            pa30100.AddField("PAREPD", "Reporting period", Connector.FieldTypeIdInteger);
            pa30100.AddField("PAREPDT", "Reporting date", Connector.FieldTypeIdDate);
            pa30100.AddField("PAPeriodEnddate", "Period End date", Connector.FieldTypeIdDate);
            pa30100.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa30100.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa30100.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa30100.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdString);
            pa30100.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa30100.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa30100.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa30100.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);

            var employedBy = pa30100.AddField("PA_Employed_By", "Employed By", Connector.FieldTypeIdEnum);
            employedBy.AddListItems(1, new List<string> { "Company", "Placement Agency", "Subcontractor" });
        }
            
        public ConnectorEntity GetIvTransferHistTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListIvTransferHistTrx), "Historical inventory transfer transactions", ParentConnector);
            
            var pa30900 = entity.AddTable("PA30900");
            
            AddIvTransferHistTransactionEntityFields(pa30900);
            
            return entity;
        }
        public void AddIvTransferHistTransactionEntityFields(ConnectorTable pa30900)
        {
            pa30900.AddField("PAIV_Document_No", "PA IV Document number", Connector.FieldTypeIdString, true);
            var ivTransferType = pa30900.AddField("PAIV_Transfer_Type", "IV Transfer type", Connector.FieldTypeIdEnum, true);
            ivTransferType.AddListItems(1, new List<string> { "Standard", "Return", "Return From Project" });
            pa30900.AddField("IVDOCNBR", "IV Document number", Connector.FieldTypeIdString, true);
            pa30900.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa30900.AddField("LOCNCODE", "Location code", Connector.FieldTypeIdString, true);
            pa30900.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa30900.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa30900.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency, true);
            pa30900.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa30900.AddField("USERID", "User ID", Connector.FieldTypeIdString);
            pa30900.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa30900.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa30900.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa30900.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa30900.AddField("REQSTDBY", "Requested By", Connector.FieldTypeIdString);
            pa30900.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa30900.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
        }
            
        public ConnectorEntity GetEmployeeExpenseHistTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListEmployeeExpenseHistTrx), "Historical employee expense transactions", ParentConnector);
            
            var pa30500 = entity.AddTable("PA30500");
            
            AddEmployeeExpenseHistTransactionEntityFields(pa30500);
            
            return entity;
        }
        public void AddEmployeeExpenseHistTransactionEntityFields(ConnectorTable pa30500)
        {
            var transactionType = pa30500.AddField("PAertrxtype", "Transaction type", Connector.FieldTypeIdEnum, true);
            transactionType.AddListItems(1, new List<string> { "Standard", "Referenced" });
            pa30500.AddField("PAerdocnumber", "Empl Expense Document No.", Connector.FieldTypeIdString, true);
            pa30500.AddField("PDK_EE_No", "PDK EE No.", Connector.FieldTypeIdString, true);
            pa30500.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa30500.AddField("USERID", "User ID", Connector.FieldTypeIdString, true);
            pa30500.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            pa30500.AddField("PAStartdate", "Start date", Connector.FieldTypeIdDate, true);
            pa30500.AddField("PAEndate", "End date", Connector.FieldTypeIdDate, true);
            pa30500.AddField("PATQTY", "Total quantity", Connector.FieldTypeIdQuantity, true);
            pa30500.AddField("PAtotcosts", "Total Costs", Connector.FieldTypeIdCurrency, true);
            pa30500.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency, true);
            pa30500.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa30500.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa30500.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa30500.AddField("PAREFNO", "Reference Document No.", Connector.FieldTypeIdString);
            pa30500.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pa30500.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa30500.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa30500.AddField("PAVENADDRESSID", "Address ID", Connector.FieldTypeIdString);
            pa30500.AddField("PAreptsuff", "Reporting Suffix", Connector.FieldTypeIdString);
            pa30500.AddField("PAYR", "Year", Connector.FieldTypeIdString);
            pa30500.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            pa30500.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            pa30500.AddField("DSCDLRAM", "Discount amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORDDLRAT", "Originating Discount amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            pa30500.AddField("DISAMTAV", "Discount Amount Available", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ODISAMTAV", "Originating Discount Amount Available", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PRCTDISC", "Percent Discount", Connector.FieldTypeIdPercentage);
            pa30500.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa30500.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa30500.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            pa30500.AddField("PAEXTCOST", "Extended cost", Connector.FieldTypeIdCurrency);
            pa30500.AddField("OREXTCST", "Originating Extended cost", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PAORIGTOTCOSTS", "Originating Total Costs", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PAREIMBURSTAXAMT", "Reimbursable tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PAORIGREIMTAXAMT", "Originating Reimbursable tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("BKTPURAM", "Backout Purchases amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PATACRV", "Total Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PAORIACCRREV", "Originating Accrued Revenues", Connector.FieldTypeIdCurrency);
            pa30500.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            pa30500.AddField("FRTAMNT", "Freight amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORFRTAMT", "Originating freight amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORFRTTAX", "Originating Freight tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdCurrency);
            pa30500.AddField("MSCCHAMT", "Misc Charges amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("OMISCAMT", "Originating Misc Charges amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORMSCTAX", "Originating Misc tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORDOCAMT", "Originating Document amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("TEN99AMNT", "1099 amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("OR1099AM", "Originating 1099 amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("UN1099AM", "Unapplied 1099 amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("CASHAMNT", "Cash amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORCASAMT", "Originating Cash amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("CAMCBKID", "Cash Amount Checkbook ID", Connector.FieldTypeIdString);
            pa30500.AddField("CDOCNMBR", "Cash Document number", Connector.FieldTypeIdString);
            pa30500.AddField("CAMTdate", "Cash Amount date", Connector.FieldTypeIdDate);
            pa30500.AddField("CAMPMTNM", "Cash Amount Payment number", Connector.FieldTypeIdString);
            pa30500.AddField("CHRGAMNT", "Charge amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("OCHGAMT", "Originating Charge amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("CHEKAMNT", "Check amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORCHKAMT", "Originating Check amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString);
            pa30500.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            pa30500.AddField("CAMPYNBR", "Check Amount Payment number", Connector.FieldTypeIdString);
            pa30500.AddField("CHAMCBID", "Check Amount Checkbook ID", Connector.FieldTypeIdString);
            pa30500.AddField("CARDNAME", "Card name", Connector.FieldTypeIdString);
            pa30500.AddField("CRCRDAMT", "Credit Card amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORCCDAMT", "Originating Credit Card amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("CRCARDDT", "Credit Card date", Connector.FieldTypeIdDate);
            pa30500.AddField("CCAMPYNM", "Credit Card Amount Payment number", Connector.FieldTypeIdString);
            pa30500.AddField("CCRCTNUM", "Credit Card Receipt number", Connector.FieldTypeIdString);
            pa30500.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORDISTKN", "Originating Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORDAVFRT", "Originating Discount Available Freight", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ODAVPUR", "Originating Discount Available Purchases", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORDAVMSC", "Originating Discount Available Misc", Connector.FieldTypeIdCurrency);
            pa30500.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            pa30500.AddField("TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("BCKTXAMT", "Backout tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("OBTAXAMT", "Originating Backout tax amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PAReimbursableAmount", "PA Reimbursable amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PAOrigReimbursableAmt", "PA Originating Reimbursable amount", Connector.FieldTypeIdCurrency);
            pa30500.AddField("PAPD", "PA Post date", Connector.FieldTypeIdDate);
            pa30500.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pa30500.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            pa30500.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            pa30500.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);

            var freightTaxablePurchase = pa30500.AddField("PAFreight_Taxable_P", "Freight Taxable - Purchase", Connector.FieldTypeIdEnum);
            freightTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });

            var miscTaxablePurchase = pa30500.AddField("PAMisc_Taxable_P", "Misc Taxable - Purchase", Connector.FieldTypeIdEnum);
            miscTaxablePurchase.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on vendor" });
        }
            
        public ConnectorEntity GetBillingHistTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListBillingHistTrx), "Billing transactions (history)", ParentConnector);
            
            var pa33100 = entity.AddTable("PA33100");
            
            var rm00101 = entity.AddTable("RM00101", "PA33100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddBillingHistTransactionEntityFields(pa33100, rm00101);
            
            return entity;
        }
        public void AddBillingHistTransactionEntityFields(ConnectorTable pa33100, ConnectorTable rm00101)
        {
            var billerTrxType = pa33100.AddField("PABILLTRXT", "Biller Transaction type", Connector.FieldTypeIdEnum, true);
            billerTrxType.AddListItems(1, new List<string> { "Invoice", "TM Return" });
            pa33100.AddField("PADocnumber20", "Document number", Connector.FieldTypeIdString, true);
            pa33100.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa33100.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            pa33100.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString, true);
            pa33100.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency, true);
            pa33100.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa33100.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa33100.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa33100.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa33100.AddField("PAuserid", "User ID", Connector.FieldTypeIdString);
            pa33100.AddField("LSTEDTDT", "Last Edit date", Connector.FieldTypeIdDate);
            pa33100.AddField("LSTUSRED", "Last User to Edit", Connector.FieldTypeIdString);
            pa33100.AddField("PAREFNO", "Reference Document number", Connector.FieldTypeIdString);
            pa33100.AddField("PRBTADCD", "Primary Billto Address code", Connector.FieldTypeIdString);
            pa33100.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            pa33100.AddField("PABILLFORMAT", "Bill Format", Connector.FieldTypeIdString);
            pa33100.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa33100.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa33100.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa33100.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa33100.AddField("CASHAMNT", "Cash amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("CBKIDCSH", "Checkbook ID Cash", Connector.FieldTypeIdString);
            pa33100.AddField("CASHdate", "Cash date", Connector.FieldTypeIdDate);
            pa33100.AddField("DCNUMCSH", "Document Number Cash", Connector.FieldTypeIdString);
            pa33100.AddField("CHEKAMNT", "Check amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("CBKIDCHK", "Checkbook ID Check", Connector.FieldTypeIdString);
            pa33100.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString);
            pa33100.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            pa33100.AddField("DCNUMCHK", "Document Number Check", Connector.FieldTypeIdString);
            pa33100.AddField("CRCRDAMT", "Credit Card amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("CRCRDNAM", "Credit Card name", Connector.FieldTypeIdString);
            pa33100.AddField("RCTNCCRD", "Receipt Number Credit Card", Connector.FieldTypeIdString);
            pa33100.AddField("CRCARDDT", "Credit Card date", Connector.FieldTypeIdDate);
            pa33100.AddField("DCNUMCRD", "Document Number Credit Card", Connector.FieldTypeIdString);
            pa33100.AddField("DSCPCTAM", "Discount Percent", Connector.FieldTypeIdPercentage);
            pa33100.AddField("DISCAMNT", "Discount amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("DISAVAMT", "Discount Available amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            pa33100.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("DISCRTND", "Discount Returned", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PABillingAmount", "Billing amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PA_Freight_Amount", "Freight amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PA_Misc_Amount", "Misc amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PATOTTAX", "Total Tax", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PABACKBILL", "Backout Bill amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("BKTFRTAM", "Backout freight amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("BKTMSCAM", "Backout Misc amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            pa33100.AddField("APPLDAMT", "Applied amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PALastDateApplied", "Last Date Applied", Connector.FieldTypeIdDate);
            pa33100.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            pa33100.AddField("SALSTERR", "Sales Territory", Connector.FieldTypeIdString);
            pa33100.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            pa33100.AddField("COMDLRAM", "Commission amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("CUTOFDAT", "Cutoff date", Connector.FieldTypeIdDate);
            pa33100.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pa33100.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdString);
            pa33100.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            pa33100.AddField("PARetentionFeeAmount", "Retention Fee amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PARetainer_Fee_Amount", "Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAtotbillings", "Total Billings", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAlastprtby", "Last Printed By", Connector.FieldTypeIdString);
            pa33100.AddField("PAlastprtdate", "Last Printed date", Connector.FieldTypeIdDate);
            pa33100.AddField("ORCASAMT", "Originating Cash amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORCHKAMT", "Originating Check amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORCCDAMT", "Originating Credit Card amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORDDLRAT", "Originating Discount amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORDAVAMT", "Originating Discount Available amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORFRTTAX", "Originating Freight tax amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORMSCTAX", "Originating Misc tax amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORBKTFRT", "Originating Backout freight amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORBKTMSC", "Originating Backout Misc amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORDOCAMT", "Originating Document amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORAPPAMT", "Originating Applied amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("ORCOMAMT", "Originating Commission amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigBillAmount", "Originating Billing amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigFreightAmt", "Originating freight amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigMiscAmt", "Originating Misc amount", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigTotalTaxAmt", "Originating Total Tax", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigBackoutBillAmt", "Originating Backout Bill Amt", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigRetentionFeeAmt", "Originating Retention Fee Amt", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigRetainerFeeAmt", "Originating Retainer Fee Amt", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigTotalBilings", "Originating Total Billings", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigDiscTakenAmt", "Originating Discount Taken Amt", Connector.FieldTypeIdCurrency);
            pa33100.AddField("PAOrigDiscRetAmt", "Originating Discount Returned Amt", Connector.FieldTypeIdCurrency);
            pa33100.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pa33100.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            pa33100.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            pa33100.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            rm00101.AddField("RM00101.CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var miscTaxable = pa33100.AddField("MISCTXBL", "Misc Taxable", Connector.FieldTypeIdEnum); 
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
            
            var freightTaxable = pa33100.AddField("FRGTTXBL", "Freight Taxable", Connector.FieldTypeIdEnum); 
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
        }
            
        public ConnectorEntity GetBillingWorkTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListBillingWorkTrx), "Billing transactions (work)", ParentConnector);
            
            var pa13100 = entity.AddTable("PA13100");
            
            var rm00101 = entity.AddTable("RM00101", "PA13100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddBillingHistTransactionEntityFields(pa13100, rm00101);
            
            return entity;
        }
        public void AddBillingWorkTransactionEntityFields(ConnectorTable pa13100, ConnectorTable rm00101)
        {
            var billerTrxType = pa13100.AddField("PABILLTRXT", "Biller Transaction type", Connector.FieldTypeIdEnum, true);
            billerTrxType.AddListItems(1, new List<string> { "Invoice", "TM Return" });
            pa13100.AddField("PADocnumber20", "Document number", Connector.FieldTypeIdString, true);
            pa13100.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa13100.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            pa13100.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString, true);
            pa13100.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency, true);
            pa13100.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa13100.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa13100.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa13100.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa13100.AddField("PAuserid", "User ID", Connector.FieldTypeIdString);
            pa13100.AddField("LSTEDTDT", "Last Edit date", Connector.FieldTypeIdDate);
            pa13100.AddField("LSTUSRED", "Last User to Edit", Connector.FieldTypeIdString);
            pa13100.AddField("PAREFNO", "Reference Document number", Connector.FieldTypeIdString);
            pa13100.AddField("PRBTADCD", "Primary Billto Address code", Connector.FieldTypeIdString);
            pa13100.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            pa13100.AddField("PABILLFORMAT", "Bill Format", Connector.FieldTypeIdString);
            pa13100.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa13100.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa13100.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa13100.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa13100.AddField("CASHAMNT", "Cash amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("CBKIDCSH", "Checkbook ID Cash", Connector.FieldTypeIdString);
            pa13100.AddField("CASHdate", "Cash date", Connector.FieldTypeIdDate);
            pa13100.AddField("DCNUMCSH", "Document Number Cash", Connector.FieldTypeIdString);
            pa13100.AddField("CHEKAMNT", "Check amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("CBKIDCHK", "Checkbook ID Check", Connector.FieldTypeIdString);
            pa13100.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString);
            pa13100.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            pa13100.AddField("DCNUMCHK", "Document Number Check", Connector.FieldTypeIdString);
            pa13100.AddField("CRCRDAMT", "Credit Card amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("CRCRDNAM", "Credit Card name", Connector.FieldTypeIdString);
            pa13100.AddField("RCTNCCRD", "Receipt Number Credit Card", Connector.FieldTypeIdString);
            pa13100.AddField("CRCARDDT", "Credit Card date", Connector.FieldTypeIdDate);
            pa13100.AddField("DCNUMCRD", "Document Number Credit Card", Connector.FieldTypeIdString);
            pa13100.AddField("DSCPCTAM", "Discount Percent", Connector.FieldTypeIdPercentage);
            pa13100.AddField("DISCAMNT", "Discount amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("DISAVAMT", "Discount Available amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            pa13100.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("DISCRTND", "Discount Returned", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PABillingAmount", "Billing amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PA_Freight_Amount", "Freight amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PA_Misc_Amount", "Misc amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PATOTTAX", "Total Tax", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PABACKBILL", "Backout Bill amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("BKTFRTAM", "Backout freight amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("BKTMSCAM", "Backout Misc amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            pa13100.AddField("APPLDAMT", "Applied amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PALastDateApplied", "Last Date Applied", Connector.FieldTypeIdDate);
            pa13100.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            pa13100.AddField("SALSTERR", "Sales Territory", Connector.FieldTypeIdString);
            pa13100.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            pa13100.AddField("COMDLRAM", "Commission amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("CUTOFDAT", "Cutoff date", Connector.FieldTypeIdDate);
            pa13100.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pa13100.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdString);
            pa13100.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            pa13100.AddField("PARetentionFeeAmount", "Retention Fee amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PARetainer_Fee_Amount", "Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAtotbillings", "Total Billings", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAlastprtby", "Last Printed By", Connector.FieldTypeIdString);
            pa13100.AddField("PAlastprtdate", "Last Printed date", Connector.FieldTypeIdDate);
            pa13100.AddField("ORCASAMT", "Originating Cash amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORCHKAMT", "Originating Check amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORCCDAMT", "Originating Credit Card amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORDDLRAT", "Originating Discount amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORDAVAMT", "Originating Discount Available amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORFRTTAX", "Originating Freight tax amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORMSCTAX", "Originating Misc tax amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORBKTFRT", "Originating Backout freight amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORBKTMSC", "Originating Backout Misc amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORDOCAMT", "Originating Document amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORAPPAMT", "Originating Applied amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("ORCOMAMT", "Originating Commission amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigBillAmount", "Originating Billing amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigFreightAmt", "Originating freight amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigMiscAmt", "Originating Misc amount", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigTotalTaxAmt", "Originating Total Tax", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigBackoutBillAmt", "Originating Backout Bill Amt", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigRetentionFeeAmt", "Originating Retention Fee Amt", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigRetainerFeeAmt", "Originating Retainer Fee Amt", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigTotalBilings", "Originating Total Billings", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigDiscTakenAmt", "Originating Discount Taken Amt", Connector.FieldTypeIdCurrency);
            pa13100.AddField("PAOrigDiscRetAmt", "Originating Discount Returned Amt", Connector.FieldTypeIdCurrency);
            pa13100.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pa13100.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            pa13100.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            pa13100.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            rm00101.AddField("RM00101.CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var miscTaxable = pa13100.AddField("MISCTXBL", "Misc Taxable", Connector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var freightTaxable = pa13100.AddField("FRGTTXBL", "Freight Taxable", Connector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
        }
            
        public ConnectorEntity GetBillingOpenTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(ProjectSmartListBillingOpenTrx), "Billing transactions (open)", ParentConnector);
            
            var pa23100 = entity.AddTable("PA23100");
            
            var rm00101 = entity.AddTable("RM00101", "PA23100");
            rm00101.AddJoinFields("CUSTNMBR", "CUSTNMBR");
            
            AddBillingHistTransactionEntityFields(pa23100, rm00101);
            
            return entity;
        }
        public void AddBillingOpenTransactionEntityFields(ConnectorTable pa23100, ConnectorTable rm00101)
        {
            var billerTrxType = pa23100.AddField("PABILLTRXT", "Biller Transaction type", Connector.FieldTypeIdEnum, true);
            billerTrxType.AddListItems(1, new List<string> { "Invoice", "TM Return" });
            pa23100.AddField("PADocnumber20", "Document number", Connector.FieldTypeIdString, true);
            pa23100.AddField("PADOCDT", "Document date", Connector.FieldTypeIdDate, true);
            pa23100.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            pa23100.AddField("CSTPONBR", "Customer PO number", Connector.FieldTypeIdString, true);
            pa23100.AddField("DOCAMNT", "Document amount", Connector.FieldTypeIdCurrency, true);
            pa23100.AddField("PAPD", "Post date", Connector.FieldTypeIdDate);
            pa23100.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            pa23100.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            pa23100.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            pa23100.AddField("PAuserid", "User ID", Connector.FieldTypeIdString);
            pa23100.AddField("LSTEDTDT", "Last Edit date", Connector.FieldTypeIdDate);
            pa23100.AddField("LSTUSRED", "Last User to Edit", Connector.FieldTypeIdString);
            pa23100.AddField("PAREFNO", "Reference Document number", Connector.FieldTypeIdString);
            pa23100.AddField("PRBTADCD", "Primary Billto Address code", Connector.FieldTypeIdString);
            pa23100.AddField("PYMTRMID", "Payment Terms ID", Connector.FieldTypeIdString);
            pa23100.AddField("PABILLFORMAT", "Bill Format", Connector.FieldTypeIdString);
            pa23100.AddField("PACOMM", "Comment", Connector.FieldTypeIdString);
            pa23100.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            pa23100.AddField("PAUD1", "User defined 1", Connector.FieldTypeIdString);
            pa23100.AddField("PAUD2", "User defined 2", Connector.FieldTypeIdString);
            pa23100.AddField("CASHAMNT", "Cash amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("CBKIDCSH", "Checkbook ID Cash", Connector.FieldTypeIdString);
            pa23100.AddField("CASHdate", "Cash date", Connector.FieldTypeIdDate);
            pa23100.AddField("DCNUMCSH", "Document Number Cash", Connector.FieldTypeIdString);
            pa23100.AddField("CHEKAMNT", "Check amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("CBKIDCHK", "Checkbook ID Check", Connector.FieldTypeIdString);
            pa23100.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString);
            pa23100.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            pa23100.AddField("DCNUMCHK", "Document Number Check", Connector.FieldTypeIdString);
            pa23100.AddField("CRCRDAMT", "Credit Card amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("CRCRDNAM", "Credit Card name", Connector.FieldTypeIdString);
            pa23100.AddField("RCTNCCRD", "Receipt Number Credit Card", Connector.FieldTypeIdString);
            pa23100.AddField("CRCARDDT", "Credit Card date", Connector.FieldTypeIdDate);
            pa23100.AddField("DCNUMCRD", "Document Number Credit Card", Connector.FieldTypeIdString);
            pa23100.AddField("DSCPCTAM", "Discount Percent", Connector.FieldTypeIdPercentage);
            pa23100.AddField("DISCAMNT", "Discount amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("DISAVAMT", "Discount Available amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("DISCdate", "Discount date", Connector.FieldTypeIdDate);
            pa23100.AddField("DISTKNAM", "Discount Taken amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("DISCRTND", "Discount Returned", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PABillingAmount", "Billing amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("TRDISAMT", "Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PA_Freight_Amount", "Freight amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PA_Misc_Amount", "Misc amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("FRTTXAMT", "Freight tax amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("MSCTXAMT", "Misc tax amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PATOTTAX", "Total Tax", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PABACKBILL", "Backout Bill amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("BKTFRTAM", "Backout freight amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("BKTMSCAM", "Backout Misc amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("DUEdate", "Due date", Connector.FieldTypeIdDate);
            pa23100.AddField("APPLDAMT", "Applied amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PALastDateApplied", "Last Date Applied", Connector.FieldTypeIdDate);
            pa23100.AddField("SLPRSNID", "Salesperson ID", Connector.FieldTypeIdString);
            pa23100.AddField("SALSTERR", "Sales Territory", Connector.FieldTypeIdString);
            pa23100.AddField("TAXSCHID", "Tax schedule ID", Connector.FieldTypeIdString);
            pa23100.AddField("COMDLRAM", "Commission amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("CUTOFDAT", "Cutoff date", Connector.FieldTypeIdDate);
            pa23100.AddField("SHIPMTHD", "Shipping method", Connector.FieldTypeIdString);
            pa23100.AddField("MSCSCHID", "Misc Schedule ID", Connector.FieldTypeIdString);
            pa23100.AddField("FRTSCHID", "Freight Schedule ID", Connector.FieldTypeIdString);
            pa23100.AddField("PARetentionFeeAmount", "Retention Fee amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PARetainer_Fee_Amount", "Retainer Fee amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAtotbillings", "Total Billings", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAlastprtby", "Last Printed By", Connector.FieldTypeIdString);
            pa23100.AddField("PAlastprtdate", "Last Printed date", Connector.FieldTypeIdDate);
            pa23100.AddField("ORCASAMT", "Originating Cash amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORCHKAMT", "Originating Check amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORCCDAMT", "Originating Credit Card amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORDDLRAT", "Originating Discount amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORDAVAMT", "Originating Discount Available amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORTDISAM", "Originating Trade Discount amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORFRTTAX", "Originating Freight tax amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORMSCTAX", "Originating Misc tax amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORBKTFRT", "Originating Backout freight amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORBKTMSC", "Originating Backout Misc amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORDOCAMT", "Originating Document amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORAPPAMT", "Originating Applied amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("ORCOMAMT", "Originating Commission amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigBillAmount", "Originating Billing amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigFreightAmt", "Originating freight amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigMiscAmt", "Originating Misc amount", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigTotalTaxAmt", "Originating Total Tax", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigBackoutBillAmt", "Originating Backout Bill Amt", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigRetentionFeeAmt", "Originating Retention Fee Amt", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigRetainerFeeAmt", "Originating Retainer Fee Amt", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigTotalBilings", "Originating Total Billings", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigDiscTakenAmt", "Originating Discount Taken Amt", Connector.FieldTypeIdCurrency);
            pa23100.AddField("PAOrigDiscRetAmt", "Originating Discount Returned Amt", Connector.FieldTypeIdCurrency);
            pa23100.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            pa23100.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            pa23100.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            pa23100.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            rm00101.AddField("RM00101.CUSTNAME", "Customer name", Connector.FieldTypeIdString);

            var miscTaxable = pa23100.AddField("MISCTXBL", "Misc Taxable", Connector.FieldTypeIdEnum);
            miscTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });

            var freightTaxable = pa23100.AddField("FRGTTXBL", "Freight Taxable", Connector.FieldTypeIdEnum);
            freightTaxable.AddListItems(1, new List<string> { "Taxable", "Nontaxable", "Base on customers" });
        }

    }
}
