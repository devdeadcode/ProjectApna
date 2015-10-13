using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class DynamicsGpFinancialModule : DynamicsGpModule
    {

        private const short GpSmartListAccounts = 1;
        private const short GpSmartListAccountTrx = 12;
        private const short GpSmartListAccountSummary = 13;
        private const short GpSmartListMda = 14;
        private const short GpSmartListBankTrx = 22;
        
        public DynamicsGpFinancialModule(DynamicsGpConnector connector) : base(connector)
        {
            Name = "Financial";
            Installed = true;
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetAccountEntity());
            Entities.Add(GetMdaEntity());
            Entities.Add(GetAccountTransactionEntity());
            Entities.Add(GetAccountSummaryEntity());
            Entities.Add(GetBankTransactionEntity());
        }

        private DataConnectorEntity GetAccountEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListAccounts, "Accounts", ParentConnector);
            
            var gl00100 = entity.AddTable("GL00100");

            var gl00105 = entity.AddTable("GL00105", "GL00100", DataConnectorTable.DataConnectorTableJoinType.Inner);
            gl00105.AddJoinFields("ACTINDX", "ACTINDX");

            AddAccountEntityFields(gl00100);

            return entity;
        }
        private static void AddAccountEntityFields(DataConnectorTable gl00100)
        {
            gl00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            gl00100.AddField("ACTALIAS", "Account Alias", DataConnector.FieldTypeIdString, true);
            gl00100.AddField("ACTDESCR", "Account Description", DataConnector.FieldTypeIdString, true);
            var postingType = gl00100.AddField("PSTNGTYP", "Posting Type", DataConnector.FieldTypeIdEnum, true); 
            postingType.AddListItems(0, new List<string> { "Balance Sheet", "Profit and Loss" });

            gl00100.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("MNACSGMT", "Main Account Segment", DataConnector.FieldTypeIdString);
            for (int i = 1; i <= 11; i++) gl00100.AddField($"ACTNUMBR_{i}", $"Segment {i}", DataConnector.FieldTypeIdString);
            //gl00100.AddField("ACCATNUM", "Account Category Number", list, "Account Category", 13, true);
            gl00100.AddField("ACTIVE", "Active", DataConnector.FieldTypeIdYesNo);
            gl00100.AddField("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("DSPLKUPS", "Display In Lookups", DataConnector.FieldTypeIdString);
            gl00100.AddField("HSTRCLRT", "Historical Rate", DataConnector.FieldTypeIdCurrency);
            gl00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            gl00100.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            gl00100.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            gl00100.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            gl00100.AddField("ADJINFL", "Adjust for Inflation", DataConnector.FieldTypeIdYesNo);
            gl00100.AddField("INFLAEQU", "Inflation Equity Account Index", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("INFLAREV", "Inflation Revenue Account Index", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("USRDEFS1", "User Defined String 1", DataConnector.FieldTypeIdString);
            gl00100.AddField("USRDEFS2", "User Defined String 2", DataConnector.FieldTypeIdString);
            gl00100.AddField("ACCTENTR", "Allow Account Entry", DataConnector.FieldTypeIdYesNo);

            var accountType = gl00100.AddField("ACCTTYPE", "Account Type", DataConnector.FieldTypeIdEnum);
            accountType.AddListItems(1, new List<string> { "Posting Account", "Unit Account", "Posting Allocation Account", "Unit Allocation Account" });

            var typicalBalance = gl00100.AddField("TPCLBLNC", "Typical Balance", DataConnector.FieldTypeIdEnum);
            typicalBalance.AddListItems(0, new List<string> { "Debit", "Credit" });

            var fixedOrVariable = gl00100.AddField("FXDORVAR", "Fixed Or Variable", DataConnector.FieldTypeIdEnum);
            fixedOrVariable.AddListItems(1, new List<string> { "Fixed Allocation", "Variable Allocation" });

            var balanceForCalculation = gl00100.AddField("BALFRCLC", "Balance For Calculation", DataConnector.FieldTypeIdEnum);
            balanceForCalculation.AddListItems(0, new List<string> { "Net Change", "Period Balances" });

            var conversionMethod = gl00100.AddField("CNVRMTHD", "Conversion Method", DataConnector.FieldTypeIdEnum);
            conversionMethod.AddListItems(1, new List<string> { "Current", "Weighted Average", "Historical" });

            var postSalesIn = gl00100.AddField("PostSlsIn", "Post Sales In", DataConnector.FieldTypeIdEnum);
            postSalesIn.AddListItems(1, new List<string> { "Detail", "Summary" });

            var postInventoryIn = gl00100.AddField("PostIvIn", "Post Inventory In", DataConnector.FieldTypeIdEnum);
            postInventoryIn.AddListItems(1, new List<string> { "Detail", "Summary" });

            var postPurchasingIn = gl00100.AddField("PostPurchIn", "Post Purchasing In", DataConnector.FieldTypeIdEnum);
            postPurchasingIn.AddListItems(1, new List<string> { "Detail", "Summary" });

            var postPayrollIn = gl00100.AddField("PostPRIn", "Post Payroll In", DataConnector.FieldTypeIdEnum);
            postPayrollIn.AddListItems(1, new List<string> { "Detail", "Summary" });
        }

        private DataConnectorEntity GetMdaEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListMda, "MDA", ParentConnector);

            var dta10200 = entity.AddTable("DTA10200");

            var dta10100 = entity.AddTable("DTA10100", "DTA10200", DataConnectorTable.DataConnectorTableJoinType.Inner);
            dta10100.AddJoinFields("DTASERIES", "DTASERIES");
            dta10100.AddJoinFields("ACTINDX", "ACTINDX");
            dta10100.AddJoinFields("SEQNUMBR", "SEQNUMBR");
            dta10100.AddJoinFields("GROUPID", "GROUPID");

            var gl00100 = entity.AddTable("GL00100", "DTA10200", DataConnectorTable.DataConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            AddMdaEntityFields(dta10200, dta10100, gl00100);

            return entity;
        }
        private static void AddMdaEntityFields(DataConnectorTable dta10200, DataConnectorTable dta10100, DataConnectorTable gl00100)
        {
            dta10200.AddField("GROUPID", "Group ID", DataConnector.FieldTypeIdString, true);
            dta10200.AddField("DOCNUMBR", "Document Number", DataConnector.FieldTypeIdString, true);
            dta10100.AddField("JRNENTRY", "Journal Entry", DataConnector.FieldTypeIdInteger, true);
            dta10200.AddField("TRXDATE", "Transaction Date", DataConnector.FieldTypeIdDate, true);
            dta10200.AddField("CODEID", "Code ID", DataConnector.FieldTypeIdString, true);
            dta10200.AddField("DTAQNTY", "Quantity", DataConnector.FieldTypeIdQuantity, true);
            dta10200.AddField("CODEAMT", "Amount", DataConnector.FieldTypeIdCurrency, true);
            gl00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            
            dta10200.AddField("DTAREF", "Reference", DataConnector.FieldTypeIdString);
            dta10200.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger);
            dta10200.AddField("SEQNUMBR", "Sequence Number", DataConnector.FieldTypeIdInteger);
            dta10100.AddField("DTA_GL_Reference", "GL Reference", DataConnector.FieldTypeIdString);
            dta10200.AddField("RMDTYPAL", "Document Type", DataConnector.FieldTypeIdInteger);
            dta10100.AddField("GROUPAMT", "Group Amount", DataConnector.FieldTypeIdCurrency);
            dta10200.AddField("POSTDESC", "Posting Description", DataConnector.FieldTypeIdString);

            var series = dta10200.AddField("DTASERIES", "Series", DataConnector.FieldTypeIdEnum);
            series.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
            
            var postingStatus = dta10100.AddField("PSTGSTUS", "Posting Status", DataConnector.FieldTypeIdEnum);
            postingStatus.AddListItems(0, new List<string> { "Unposted", "Posted", "Posted With Error" });
            
        }

        private DataConnectorEntity GetAccountTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListAccountTrx, "Account transactions", ParentConnector);

            var svGlTrx = entity.AddTable("svGLTrx");

            var gl00100 = entity.AddTable("GL00100", "svGLTrx", DataConnectorTable.DataConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            var mc40200 = entity.AddTable("MC40200", "svGLTrx");
            mc40200.AddJoinFields("CURNCYID", "CURNCYID");

            AddAccountTransactionEntityFields(svGlTrx, gl00100, mc40200);

            gl00100.AddField("DECPLACS - 1", "Decimal Places from Account Master", DataConnector.FieldTypeIdInteger);

            return entity;
        }
        private static void AddAccountTransactionEntityFields(DataConnectorTable svGlTrx, DataConnectorTable gl00100, DataConnectorTable mc40200)
        {
            svGlTrx.AddField("JRNENTRY", "Journal Entry", DataConnector.FieldTypeIdInteger, true);
            var series = svGlTrx.AddField("SERIES", "Series", DataConnector.FieldTypeIdEnum, true);
            series.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
            svGlTrx.AddField("TRXDATE", "Transaction Date", DataConnector.FieldTypeIdDate, true);
            gl00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            gl00100.AddField("ACTDESCR", "Account Description", DataConnector.FieldTypeIdString, true);
            svGlTrx.AddField("CRDTAMNT", "Credit Amount", DataConnector.FieldTypeIdCurrency, true);
            svGlTrx.AddField("DEBITAMT", "Debit Amount", DataConnector.FieldTypeIdCurrency, true);

            mc40200.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("DTA_GL_Status", "DTA GL Status", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("DTA_Index", "DTA Index", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svGlTrx.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svGlTrx.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("HSTYEAR", "History Year", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ICTRX", "Intercompany Transaction", DataConnector.FieldTypeIdYesNo);
            svGlTrx.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("LSTDTEDT", "Last Date Edited", DataConnector.FieldTypeIdDate);
            svGlTrx.AddField("LASTUSER", "Last User", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("INTERID", "Intercompany ID", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("OPENYEAR", "Open Year", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORCOMID", "Originating Company ID", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORCTRNUM", "Originating Control Number", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORDOCNUM", "Originating Document Number", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORIGINJE", "Originating Journal Entry", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("ORMSTRID", "Originating Master ID", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORMSTRNM", "Originating Master Name", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORPSTDDT", "Originating Posted Date", DataConnector.FieldTypeIdDate);
            svGlTrx.AddField("OrigSeqNum", "Originating Sequence Number", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("ORGNTSRC", "Originating Source", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORTRXSRC", "Originating Transaction Source", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("POLLDTRX", "Polled Transaction", DataConnector.FieldTypeIdYesNo);
            svGlTrx.AddField("QKOFSET", "Quick Offset", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("RCTRXSEQ", "Recurring Transaction Sequence", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("REFRENCE", "Reference", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("SQNCLINE", "Sequence Number", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("SOURCDOC", "Source Document", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("USWHPSTD", "User Who Posted", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ORCRDAMT", "Originating Credit Amount", DataConnector.FieldTypeIdCurrency);
            svGlTrx.AddField("ORDBTAMT", "Originating Debit Amount", DataConnector.FieldTypeIdCurrency);
            svGlTrx.AddField("DOCDATE", "Document Date", DataConnector.FieldTypeIdDate);
            svGlTrx.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svGlTrx.AddField("PPSGNMBR", "Period Posting Number", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("PSTGNMBR", "Posting Number", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("PERIODID", "Period ID", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svGlTrx.AddField("RVRSNGDT", "Reversing Date", DataConnector.FieldTypeIdDate);
            svGlTrx.AddField("RCRNGTRX", "Recurring Transaction", DataConnector.FieldTypeIdYesNo);
            svGlTrx.AddField("RVTRXSRC", "Reversing Transaction Source", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("DTAControlNum", "DTA Control Number", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("CLOSEDYR", "Closed Year", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("HISTRX", "History Transaction", DataConnector.FieldTypeIdYesNo);
            svGlTrx.AddField("REVPRDID", "Reversing Period ID", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("REVYEAR", "Reversing Year", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("REVCLYR", "Reversing Closed Year", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("REVHIST", "Reversing History Transaction", DataConnector.FieldTypeIdYesNo);
            svGlTrx.AddField("ICDISTS", "ICDists", DataConnector.FieldTypeIdYesNo);
            svGlTrx.AddField("PRNTSTUS", "Printing Status", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("GLLINMSG", "GL LINE Messages", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("GLLINMS2", "GL LINE Messages2", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("GLHDRMSG", "GL HDR Messages", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("GLHDRMS2", "GL HDR Messages2", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("ERRSTATE", "Error State", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("DTATRXType", "DTA Transaction Type", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("MNACSGMT", "Main Account Segment", DataConnector.FieldTypeIdString);
            gl00100.AddField("ACTDESCR", "Account Description from Account Master", DataConnector.FieldTypeIdString);
            gl00100.AddField("ACCATNUM", "Account Category Number", DataConnector.FieldTypeIdString);
            gl00100.AddField("ACTIVE", "Active", DataConnector.FieldTypeIdYesNo);
            gl00100.AddField("DSPLKUPS", "Display In Lookups", DataConnector.FieldTypeIdString);
            gl00100.AddField("HSTRCLRT", "Historical Rate", DataConnector.FieldTypeIdCurrency);
            gl00100.AddField("NOTEINDX", "Note Index from Account Master", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            gl00100.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            gl00100.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            gl00100.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            gl00100.AddField("ADJINFL", "Adjust for Inflation", DataConnector.FieldTypeIdYesNo);
            gl00100.AddField("INFLAEQU", "Inflation Equity Account Index", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("INFLAREV", "Inflation Revenue Account Index", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("CorrespondingUnit", "CorrespondingUnit", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            svGlTrx.AddField("BCHSOURC", "Batch Source", DataConnector.FieldTypeIdString);
            svGlTrx.AddField("VOIDED", "Voided", DataConnector.FieldTypeIdYesNo);
            svGlTrx.AddField("Back_Out_JE", "Back Out JE", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("Original_JE", "Original JE", DataConnector.FieldTypeIdInteger);
            svGlTrx.AddField("Correcting_JE", "Correcting JE", DataConnector.FieldTypeIdInteger);

            for (int i = 1; i <= 5; i++) gl00100.AddField($"ACTNUMBR_{i}", $"Segment {i}", DataConnector.FieldTypeIdString);

            var accountType = gl00100.AddField("ACCTTYPE", "Account Type", DataConnector.FieldTypeIdEnum);
            accountType.AddListItems(1, new List<string> { "Posting Account", "Unit Account", "Posting Allocation Account", "Unit Allocation Account" });
            
            var documentStatus = svGlTrx.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Work", "Open", "History" });
            
            var balanceForCalculation = svGlTrx.AddField("BALFRCLC", "Balance For Calculation", DataConnector.FieldTypeIdEnum);
            balanceForCalculation.AddListItems(0, new List<string> { "Net Change", "Period Balances" });
            
            var fixedOrVariable = svGlTrx.AddField("FXDORVAR", "Fixed Or Variable", DataConnector.FieldTypeIdEnum);
            fixedOrVariable.AddListItems(1, new List<string> { "Fixed Allocation", "Variable Allocation" });
            
            var originatingDtaSeries = svGlTrx.AddField("OrigDTASeries", "Originating DTA Series", DataConnector.FieldTypeIdEnum);
            originatingDtaSeries.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
            
            var originatingTransactionType = svGlTrx.AddField("ORTRXTYP", "Originating Transaction Type", DataConnector.FieldTypeIdEnum);
            originatingTransactionType.AddListItems(1, new List<string> { "Standard", "Reversing", "Clearing" });
            
            var originatingType = svGlTrx.AddField("ORGNATYP", "Originating Type", DataConnector.FieldTypeIdEnum);
            originatingType.AddListItems(1, new List<string> { "Normal", "Clearing", "Recurring", "Business Form" });
            
            var rateCalculationMethod = svGlTrx.AddField("RTCLCMTD", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var postingType = gl00100.AddField("PSTNGTYP", "Posting Type", DataConnector.FieldTypeIdEnum);
            postingType.AddListItems(0, new List<string> { "Balance Sheet", "Profit and Loss" });
            
            var lineStatus = svGlTrx.AddField("LNESTAT", "Line Status", DataConnector.FieldTypeIdEnum);
            lineStatus.AddListItems(1, new List<string> { "Gain Or Loss", "Normal Rounding", "One Sided", "Other Rounding", "Standard", "Unit Account", "Exchange Rate Variance" });
            
            var mcTransactionState = svGlTrx.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var transactionType = svGlTrx.AddField("TRXTYPE", "Transaction Type", DataConnector.FieldTypeIdEnum);
            transactionType.AddListItems(1, new List<string> { "Standard", "Reversing", "Clearing" });
            
            var glHeaderValue = svGlTrx.AddField("GLHDRVAL", "GL Header Valid", DataConnector.FieldTypeIdEnum);
            glHeaderValue.AddListItems(1, new List<string> { "Valid Account Number", "Valid Journal Entry", "Valid Posting Date" });
            
            var glLineValid = svGlTrx.AddField("GLLINVAL", "GL Line Valid", DataConnector.FieldTypeIdEnum);
            glLineValid.AddListItems(1, new List<string> { "Valid Account Number", "Valid Offset Account Number" });
            
            var accountTypeFromAccountMaster = gl00100.AddField("ACCTTYPE", "Account Type from Account Master", DataConnector.FieldTypeIdEnum);
            accountTypeFromAccountMaster.AddListItems(1, new List<string> { "Posting Account", "Unit Account", "Posting Allocation Account", "Unit Allocation Account" });
            
            var postingTypeFromAccountMaster = gl00100.AddField("PSTNGTYP", "Posting Type from Account Master", DataConnector.FieldTypeIdEnum);
            postingTypeFromAccountMaster.AddListItems(0, new List<string> { "Balance Sheet", "Profit and Loss" });
            
            var typicalBalance = gl00100.AddField("TPCLBLNC", "Typical Balance", DataConnector.FieldTypeIdEnum);
            typicalBalance.AddListItems(0, new List<string> { "Debit", "Credit" });
            
            var fixedOrVariableFromAccountMaster = gl00100.AddField("FXDORVAR", "Fixed Or Variable from Account Master", DataConnector.FieldTypeIdEnum);
            fixedOrVariableFromAccountMaster.AddListItems(1, new List<string> { "Fixed Allocation", "Variable Allocation" });
            
            var balanceForCalculationFromAccountMaster = gl00100.AddField("BALFRCLC", "Balance For Calculation from Account Master", DataConnector.FieldTypeIdEnum);
            balanceForCalculationFromAccountMaster.AddListItems(0, new List<string> { "Net Change", "Period Balances" });
            
            var conversionMethod = gl00100.AddField("CNVRMTHD", "Conversion Method", DataConnector.FieldTypeIdEnum);
            conversionMethod.AddListItems(1, new List<string> { "Current", "Weighted Average", "Historical" });
            
            var postSalesIn = gl00100.AddField("PostSlsIn", "Post Sales In", DataConnector.FieldTypeIdEnum);
            postSalesIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postInventoryIn = gl00100.AddField("PostIvIn", "Post Inventory In", DataConnector.FieldTypeIdEnum);
            postInventoryIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPurchasingIn = gl00100.AddField("PostPurchIn", "Post Purchasing In", DataConnector.FieldTypeIdEnum);
            postPurchasingIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPayrollIn = gl00100.AddField("PostPRIn", "Post Payroll In", DataConnector.FieldTypeIdEnum);
            postPayrollIn.AddListItems(1, new List<string> { "Detail", "Summary" });

        }

        private DataConnectorEntity GetAccountSummaryEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListAccountSummary, "Account summary", ParentConnector);

            var svGlSum = entity.AddTable("svGLSum");

            var gl00100 = entity.AddTable("GL00100", "svGLSum", DataConnectorTable.DataConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            AddAccountSummaryEntityFields(svGlSum, gl00100);

            return entity;
        }
        private static void AddAccountSummaryEntityFields(DataConnectorTable svGlSum, DataConnectorTable gl00100)
        {
            
            svGlSum.AddField("YEAR1", "Year", DataConnector.FieldTypeIdString, true);
            svGlSum.AddField("PERIODID", "Period ID", DataConnector.FieldTypeIdInteger, true);
            gl00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            gl00100.AddField("ACTDESCR", "Account Description", DataConnector.FieldTypeIdString, true);
            svGlSum.AddField("CRDTAMNT", "Credit Amount", DataConnector.FieldTypeIdCurrency, true);
            svGlSum.AddField("DEBITAMT", "Debit Amount", DataConnector.FieldTypeIdCurrency, true);

            svGlSum.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger);
            svGlSum.AddField("PERDBLNC", "Period Balance", DataConnector.FieldTypeIdCurrency);
            gl00100.AddField("ACTALIAS", "Account Alias", DataConnector.FieldTypeIdString);
            gl00100.AddField("ACTIVE", "Active", DataConnector.FieldTypeIdYesNo);
            svGlSum.AddField("ACCATNUM", "Account Category Number", DataConnector.FieldTypeIdString);
            gl00100.AddField("MNACSGMT", "Main Account Segment", DataConnector.FieldTypeIdString);
            gl00100.AddField("DECPLACS - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("DSPLKUPS", "Display In Lookups", DataConnector.FieldTypeIdString);
            gl00100.AddField("HSTRCLRT", "Historical Rate", DataConnector.FieldTypeIdCurrency);
            gl00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("CREATDDT", "Created Date", DataConnector.FieldTypeIdDate);
            gl00100.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            gl00100.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            gl00100.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            gl00100.AddField("ADJINFL", "Adjust for Inflation", DataConnector.FieldTypeIdYesNo);
            gl00100.AddField("INFLAEQU", "Inflation Equity Account Index", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("INFLAREV", "Inflation Revenue Account Index", DataConnector.FieldTypeIdInteger);

            for (var i = 1; i <= 5; i++) gl00100.AddField($"ACTNUMBR_{i}", $"Segment {i}", DataConnector.FieldTypeIdString);

            var documentStatus = svGlSum.AddField("ASI_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Current", "", "History" });
            
            var accountType = gl00100.AddField("ACCTTYPE", "Account Type", DataConnector.FieldTypeIdEnum);
            accountType.AddListItems(1, new List<string> { "Posting Account", "Unit Account", "Posting Allocation Account", "Unit Allocation Account" });
            
            var postingType = gl00100.AddField("PSTNGTYP", "Posting Type", DataConnector.FieldTypeIdEnum);
            postingType.AddListItems(0, new List<string> { "Balance Sheet", "Profit and Loss" });
            
            var typicalBalance = gl00100.AddField("TPCLBLNC", "Typical Balance", DataConnector.FieldTypeIdEnum);
            typicalBalance.AddListItems(0, new List<string> { "Debit", "Credit" });
            
            var fixedOrVariable = gl00100.AddField("FXDORVAR", "Fixed Or Variable", DataConnector.FieldTypeIdEnum);
            fixedOrVariable.AddListItems(1, new List<string> { "Fixed Allocation", "Variable Allocation" });
            
            var balanceForCalculation = gl00100.AddField("BALFRCLC", "Balance For Calculation", DataConnector.FieldTypeIdEnum);
            balanceForCalculation.AddListItems(0, new List<string> { "Net Change", "Period Balances" });
            
            var conversionMethod = gl00100.AddField("CNVRMTHD", "Conversion Method", DataConnector.FieldTypeIdEnum);
            conversionMethod.AddListItems(1, new List<string> { "Current", "Weighted Average", "Historical" });
            
            var postSalesIn = gl00100.AddField("PostSlsIn", "Post Sales In", DataConnector.FieldTypeIdEnum);
            postSalesIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postInventoryIn = gl00100.AddField("PostIvIn", "Post Inventory In", DataConnector.FieldTypeIdEnum);
            postInventoryIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPurchasingIn = gl00100.AddField("PostPurchIn", "Post Purchasing In", DataConnector.FieldTypeIdEnum);
            postPurchasingIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPayrollIn = gl00100.AddField("PostPRIn", "Post Payroll In", DataConnector.FieldTypeIdEnum);
            postPayrollIn.AddListItems(1, new List<string> { "Detail", "Summary" });

        }

        private DataConnectorEntity GetBankTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListBankTrx, "Bank transaction", ParentConnector);

            var svBankTrx = entity.AddTable("svBankTrx");

            var cm20300 = entity.AddTable("CM20300", "svBankTrx");
            cm20300.AddJoinFields("CMRECNUM", "CMRECNUM");

            var cm20200 = entity.AddTable("CM20200", "svBankTrx");
            cm20200.AddJoinFields("CMRECNUM", "CMRECNUM");

            AddBankTransactionEntityFields(svBankTrx, cm20300, cm20200);

            return entity;
        }
        private static void AddBankTransactionEntityFields(DataConnectorTable svBankTrx, DataConnectorTable cm20300, DataConnectorTable cm20200)
        {
            svBankTrx.AddField("CHEKBKID", "Checkbook ID", DataConnector.FieldTypeIdString, true);
            svBankTrx.AddField("GLPOSTDT", "GL Posting Date", DataConnector.FieldTypeIdDate, true);
            svBankTrx.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString, true);
            svBankTrx.AddField("SOURCDOC", "Source Document", DataConnector.FieldTypeIdString, true);
            svBankTrx.AddField("Checkbook_Amount", "Checkbook Amount", DataConnector.FieldTypeIdCurrency, true);

            svBankTrx.AddField("CMRECNUM", "Record Number", DataConnector.FieldTypeIdInteger);
            svBankTrx.AddField("sRecNum", "Record Number String", DataConnector.FieldTypeIdString);
            cm20300.AddField("DEPOSITED", "Deposited", DataConnector.FieldTypeIdYesNo);
            svBankTrx.AddField("MDFUSRID", "Modified User ID", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("MODIFDT", "Modified Date", DataConnector.FieldTypeIdDate);
            cm20300.AddField("depositnumber", "Deposit Number", DataConnector.FieldTypeIdString);
            cm20300.AddField("RCPTNMBR", "Receipt Number", DataConnector.FieldTypeIdString);
            cm20300.AddField("receiptdate", "Receipt Date", DataConnector.FieldTypeIdDate);
            cm20300.AddField("RCPTAMT", "Receipt Amount", DataConnector.FieldTypeIdCurrency);
            cm20300.AddField("CARDNAME", "Card Name", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("CURNCYID", "Currency ID", DataConnector.FieldTypeIdString);
            cm20300.AddField("BANKNAME", "Bank Name", DataConnector.FieldTypeIdString);
            cm20300.AddField("BNKBRNCH", "Bank Branch", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("POSTEDDT", "Posted Date", DataConnector.FieldTypeIdDate);
            svBankTrx.AddField("PTDUSRID", "Posted User ID", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("CMLinkID", "CMLinkID", DataConnector.FieldTypeIdString);
            cm20300.AddField("RcvdFrom", "Received From", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("VOIDED", "Voided", DataConnector.FieldTypeIdYesNo);
            svBankTrx.AddField("VOIDDATE", "Void Date", DataConnector.FieldTypeIdDate);
            svBankTrx.AddField("VOIDPDATE", "Void GL Posting Date", DataConnector.FieldTypeIdDate);
            svBankTrx.AddField("VOIDDESC", "Void Description", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            svBankTrx.AddField("SRCDOCNUM", "Source Doc Number", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("SRCDOCTYP", "Source Doc Type", DataConnector.FieldTypeIdInteger);
            svBankTrx.AddField("AUDITTRAIL", "Audit Trail Code", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("ORIGAMT", "Originating Amount", DataConnector.FieldTypeIdCurrency);
            svBankTrx.AddField("RATETPID", "Rate Type ID", DataConnector.FieldTypeIdString);
            cm20300.AddField("RCVGRATETPID", "Receiving Rate Type ID", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("EXGTBLID", "Exchange Table ID", DataConnector.FieldTypeIdString);
            svBankTrx.AddField("XCHGRATE", "Exchange Rate", DataConnector.FieldTypeIdQuantity);
            cm20300.AddField("Receiving_Exchange_Rate", "Receiving Exchange Rate", DataConnector.FieldTypeIdQuantity);
            svBankTrx.AddField("EXCHDATE", "Exchange Date", DataConnector.FieldTypeIdDate);
            svBankTrx.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            svBankTrx.AddField("EXPNDATE", "Expiration Date", DataConnector.FieldTypeIdDate);
            svBankTrx.AddField("CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            svBankTrx.AddField("DECPLCUR - 1 ", "Decimal Places Currency", DataConnector.FieldTypeIdInteger);
            cm20300.AddField("RLGANLOS", "Realized Gain-Loss Amount", DataConnector.FieldTypeIdCurrency);
            cm20300.AddField("Cash_Account_Index", "Cash Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            cm20300.AddField("Realized_GL_Account_Inde", "Realized GL Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svBankTrx.AddField("DENXRATE", "Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            cm20300.AddField("Receiving_DenomEXRate", "Receiving Denomination Exchange Rate", DataConnector.FieldTypeIdQuantity);
            cm20200.AddField("CMTrxNum", "CM Transaction Number", DataConnector.FieldTypeIdString);
            cm20200.AddField("TRXDATE", "Transaction Date", DataConnector.FieldTypeIdDate);
            cm20200.AddField("TRXAMNT", "Transaction Amount", DataConnector.FieldTypeIdCurrency);
            cm20200.AddField("paidtorcvdfrom", "Paid ToRcvd From", DataConnector.FieldTypeIdString);
            cm20200.AddField("Recond", "Reconciled", DataConnector.FieldTypeIdYesNo);
            cm20200.AddField("RECONUM", "Reconcile Number", DataConnector.FieldTypeIdInteger);
            cm20200.AddField("ClrdAmt", "Cleared Amount", DataConnector.FieldTypeIdCurrency);
            cm20200.AddField("clearedate", "Cleared Date", DataConnector.FieldTypeIdDate);
            cm20200.AddField("Xfr_Record_Number", "Xfr Record Number", DataConnector.FieldTypeIdInteger);

            var controlType = cm20300.AddField("CNTRLTYP", "Control Type", DataConnector.FieldTypeIdEnum);
            controlType.AddListItems(1, new List<string> { "Transaction", "Receipt" });
            
            var receiptType = cm20300.AddField("RcpType", "Receipt Type", DataConnector.FieldTypeIdEnum);
            receiptType.AddListItems(1, new List<string> { "Check", "Cash", "Credit Card" });
            
            var recordStatus = svBankTrx.AddField("RCRDSTTS", "Record Status", DataConnector.FieldTypeIdEnum);
            recordStatus.AddListItems(1, new List<string> { "Begin", "Commit", "Voiding" });
            
            var rateCalculationMethod = svBankTrx.AddField("RTCLCMTD", "Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var receivingRateCalculationMethod = cm20300.AddField("RCVGRTCLCMTD", "Receiving Rate Calculation Method", DataConnector.FieldTypeIdEnum);
            receivingRateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svBankTrx.AddField("MCTRXSTT", "MC Transaction State", DataConnector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var receivingMcTransactionState = cm20300.AddField("Receiving_MC_Transaction", "Receiving MC Transaction State", DataConnector.FieldTypeIdEnum);
            receivingMcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-Denomination to Non-Denomination", "Non-Denomination to Euro", "Non-Denomination to Denomination", "Denomination to Non-Denomination", "Denomination to Denomination", "Denomination to Euro", "Euro to Denomination", "Euro to Non-Denomination" });
            
            var documentStatus = svBankTrx.AddField("ASI_CM_Document_Status", "Document Status", DataConnector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Receipt", "", "Transaction" });
            
            var cmTransactionType = cm20200.AddField("CMTrxType", "CM Transaction Type", DataConnector.FieldTypeIdEnum);
            cmTransactionType.AddListItems(1, new List<string> { "Deposit", "Receipt", "Check", "Withdrawl", "Increase Adjustment", "Decrease Adjustment", "Interest Income", "Other Income", "Other Expense", "Service Charge" });
            
            var depositType = cm20200.AddField("DEPTYPE", "Deposit Type", DataConnector.FieldTypeIdEnum);
            depositType.AddListItems(1, new List<string> { "Deposit with Receipts", "Deposit without Receipts", "Clearing Deposit" });

        }

    }
}
