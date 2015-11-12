using System.Collections.Generic;

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
            Id = 1;
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

        private ConnectorEntity GetAccountEntity()
        {
            var entity = new ConnectorEntity(GpSmartListAccounts, "Accounts", ParentConnector)
            {
                DefaultMaxRecords = 1000
            };

            var gl00100 = entity.AddTable("GL00100");

            var gl00105 = entity.AddTable("GL00105", "GL00100", ConnectorTable.ConnectorTableJoinType.Inner);
            gl00105.AddJoinFields("ACTINDX", "ACTINDX");

            AddAccountEntityFields(gl00100);

            return entity;
        }
        private static void AddAccountEntityFields(ConnectorTable gl00100)
        {
            gl00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            gl00100.AddField("ACTALIAS", "Account alias", Connector.FieldTypeIdString, true);
            gl00100.AddField("ACTDESCR", "Account description", Connector.FieldTypeIdString, true);
            var postingType = gl00100.AddField("PSTNGTYP", "Posting type", Connector.FieldTypeIdEnum, true); 
            postingType.AddListItems(0, new List<string> { "Balance sheet", "Profit and loss" });

            var indexField = gl00100.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger);
            indexField.KeyNumber = 1;

            gl00100.AddField("MNACSGMT", "Main account segment", Connector.FieldTypeIdString);
            for (int i = 1; i <= 11; i++) gl00100.AddField($"ACTNUMBR_{i}", $"Segment {i}", Connector.FieldTypeIdString);
            //gl00100.AddField("ACCATNUM", "Account Category number", list, "Account Category", 13, true);
            gl00100.AddField("ACTIVE", "Active", Connector.FieldTypeIdYesNo);
            gl00100.AddField("DECPLACS - 1", "Decimal places", Connector.FieldTypeIdInteger);
            gl00100.AddField("DSPLKUPS", "Display in lookups", Connector.FieldTypeIdString);
            gl00100.AddField("HSTRCLRT", "Historical rate", Connector.FieldTypeIdCurrency);
            gl00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);

            var createDate = gl00100.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            createDate.CreateDate = true;

            var modifyDate = gl00100.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            modifyDate.ModifyDate = true;

            gl00100.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            gl00100.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            gl00100.AddField("ADJINFL", "Adjust for inflation", Connector.FieldTypeIdYesNo);
            gl00100.AddField("INFLAEQU", "Inflation equity account index", Connector.FieldTypeIdInteger);
            gl00100.AddField("INFLAREV", "Inflation revenue account index", Connector.FieldTypeIdInteger);
            gl00100.AddField("USRDEFS1", "User defined string 1", Connector.FieldTypeIdString);
            gl00100.AddField("USRDEFS2", "User defined string 2", Connector.FieldTypeIdString);
            gl00100.AddField("ACCTENTR", "Allow account entry", Connector.FieldTypeIdYesNo);

            var accountType = gl00100.AddField("ACCTTYPE", "Account type", Connector.FieldTypeIdEnum);
            accountType.AddListItems(1, new List<string> { "Posting account", "Unit account", "Posting allocation account", "Unit allocation account" });

            var typicalBalance = gl00100.AddField("TPCLBLNC", "Typical balance", Connector.FieldTypeIdEnum);
            typicalBalance.AddListItems(0, new List<string> { "Debit", "Credit" });

            var fixedOrVariable = gl00100.AddField("FXDORVAR", "Fixed or variable", Connector.FieldTypeIdEnum);
            fixedOrVariable.AddListItems(1, new List<string> { "Fixed allocation", "Variable allocation" });

            var balanceForCalculation = gl00100.AddField("BALFRCLC", "Balance for calculation", Connector.FieldTypeIdEnum);
            balanceForCalculation.AddListItems(0, new List<string> { "Net change", "Period balances" });

            var conversionMethod = gl00100.AddField("CNVRMTHD", "Conversion method", Connector.FieldTypeIdEnum);
            conversionMethod.AddListItems(1, new List<string> { "Current", "Weighted average", "Historical" });

            var postSalesIn = gl00100.AddField("PostSlsIn", "Post sales in", Connector.FieldTypeIdEnum);
            postSalesIn.AddListItems(1, new List<string> { "Detail", "Summary" });

            var postInventoryIn = gl00100.AddField("PostIvIn", "Post inventory in", Connector.FieldTypeIdEnum);
            postInventoryIn.AddListItems(1, new List<string> { "Detail", "Summary" });

            var postPurchasingIn = gl00100.AddField("PostPurchIn", "Post purchasing in", Connector.FieldTypeIdEnum);
            postPurchasingIn.AddListItems(1, new List<string> { "Detail", "Summary" });

            var postPayrollIn = gl00100.AddField("PostPRIn", "Post payroll in", Connector.FieldTypeIdEnum);
            postPayrollIn.AddListItems(1, new List<string> { "Detail", "Summary" });
        }

        private ConnectorEntity GetMdaEntity()
        {
            var entity = new ConnectorEntity(GpSmartListMda, "MDA", ParentConnector);

            var dta10200 = entity.AddTable("DTA10200");

            var dta10100 = entity.AddTable("DTA10100", "DTA10200", ConnectorTable.ConnectorTableJoinType.Inner);
            dta10100.AddJoinFields("DTASERIES", "DTASERIES");
            dta10100.AddJoinFields("ACTINDX", "ACTINDX");
            dta10100.AddJoinFields("SEQNUMBR", "SEQNUMBR");
            dta10100.AddJoinFields("GROUPID", "GROUPID");

            var gl00100 = entity.AddTable("GL00100", "DTA10200", ConnectorTable.ConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            AddMdaEntityFields(dta10200, dta10100, gl00100);

            return entity;
        }
        private static void AddMdaEntityFields(ConnectorTable dta10200, ConnectorTable dta10100, ConnectorTable gl00100)
        {
            dta10200.AddField("GROUPID", "Group ID", Connector.FieldTypeIdString, true);
            dta10200.AddField("DOCNUMBR", "Document number", Connector.FieldTypeIdString, true);
            dta10100.AddField("JRNENTRY", "Journal entry", Connector.FieldTypeIdInteger, true);
            dta10200.AddField("TRXdate", "Transaction date", Connector.FieldTypeIdDate, true);
            dta10200.AddField("CODEID", "Code ID", Connector.FieldTypeIdString, true);
            dta10200.AddField("DTAQNTY", "Quantity", Connector.FieldTypeIdQuantity, true);
            dta10200.AddField("CODEAMT", "Amount", Connector.FieldTypeIdCurrency, true);
            gl00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            
            dta10200.AddField("DTAREF", "Reference", Connector.FieldTypeIdString);
            dta10200.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger);
            dta10200.AddField("SEQNUMBR", "Sequence number", Connector.FieldTypeIdInteger);
            dta10100.AddField("DTA_GL_Reference", "GL reference", Connector.FieldTypeIdString);
            dta10200.AddField("RMDTYPAL", "Document type", Connector.FieldTypeIdInteger);
            dta10100.AddField("GROUPAMT", "Group amount", Connector.FieldTypeIdCurrency);
            dta10200.AddField("POSTDESC", "Posting description", Connector.FieldTypeIdString);

            var series = dta10200.AddField("DTASERIES", "Series", Connector.FieldTypeIdEnum);
            series.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
            
            var postingStatus = dta10100.AddField("PSTGSTUS", "Posting status", Connector.FieldTypeIdEnum);
            postingStatus.AddListItems(0, new List<string> { "Unposted", "Posted", "Posted with error" });
            
        }

        private ConnectorEntity GetAccountTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListAccountTrx, "Account transactions", ParentConnector);

            var svGlTrx = entity.AddTable("svGLTrx");

            var gl00100 = entity.AddTable("GL00100", "svGLTrx", ConnectorTable.ConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            var mc40200 = entity.AddTable("MC40200", "svGLTrx");
            mc40200.AddJoinFields("CURNCYID", "CURNCYID");

            AddAccountTransactionEntityFields(svGlTrx, gl00100, mc40200);

            gl00100.AddField("DECPLACS - 1", "Decimal places from account master", Connector.FieldTypeIdInteger);

            return entity;
        }
        private static void AddAccountTransactionEntityFields(ConnectorTable svGlTrx, ConnectorTable gl00100, ConnectorTable mc40200)
        {
            var journalEntry = svGlTrx.AddField("JRNENTRY", "Journal entry", Connector.FieldTypeIdInteger, true);
            journalEntry.KeyNumber = 1;

            var series = svGlTrx.AddField("SERIES", "Series", Connector.FieldTypeIdEnum, true);
            series.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
            svGlTrx.AddField("TRXdate", "Transaction date", Connector.FieldTypeIdDate, true);
            gl00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            gl00100.AddField("ACTDESCR", "Account description", Connector.FieldTypeIdString, true);
            svGlTrx.AddField("CRDTAMNT", "Credit amount", Connector.FieldTypeIdCurrency, true);
            svGlTrx.AddField("DEBITAMT", "Debit amount", Connector.FieldTypeIdCurrency, true);

            mc40200.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            svGlTrx.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString);
            svGlTrx.AddField("DTA_GL_Status", "DTA GL status", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("DTA_index", "DTA index", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            svGlTrx.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svGlTrx.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            svGlTrx.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svGlTrx.AddField("HSTYEAR", "History year", Connector.FieldTypeIdString);
            svGlTrx.AddField("ICTRX", "Intercompany transaction", Connector.FieldTypeIdYesNo);
            svGlTrx.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("LSTDTEDT", "Last date edited", Connector.FieldTypeIdDate);
            svGlTrx.AddField("LASTUSER", "Last user", Connector.FieldTypeIdString);
            svGlTrx.AddField("INTERID", "Intercompany ID", Connector.FieldTypeIdString);
            svGlTrx.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("OPENYEAR", "Open year", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORCOMID", "Originating company ID", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORCTRNUM", "Originating control number", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORDOCNUM", "Originating document number", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORIGINJE", "Originating journal entry", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("ORMSTRID", "Originating master ID", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORMSTRNM", "Originating master name", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORPSTDDT", "Originating posted date", Connector.FieldTypeIdDate);
            svGlTrx.AddField("OrigSeqNum", "Originating sequence number", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("ORGNTSRC", "Originating source", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORTRXSRC", "Originating transaction source", Connector.FieldTypeIdString);
            svGlTrx.AddField("POLLDTRX", "Polled transaction", Connector.FieldTypeIdYesNo);
            svGlTrx.AddField("QKOFSET", "Quick offset", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            svGlTrx.AddField("RCTRXSEQ", "Recurring transaction sequence", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("REFRENCE", "Reference", Connector.FieldTypeIdString);
            svGlTrx.AddField("SQNCLINE", "Sequence number", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("SOURCDOC", "Source document", Connector.FieldTypeIdString);
            svGlTrx.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            svGlTrx.AddField("USWHPSTD", "User who posted", Connector.FieldTypeIdString);
            svGlTrx.AddField("ORCRDAMT", "Originating credit amount", Connector.FieldTypeIdCurrency);
            svGlTrx.AddField("ORDBTAMT", "Originating debit amount", Connector.FieldTypeIdCurrency);
            svGlTrx.AddField("DOCdate", "Document date", Connector.FieldTypeIdDate);
            svGlTrx.AddField("DENXRATE", "Denomination exchange rate", Connector.FieldTypeIdQuantity);
            svGlTrx.AddField("PPSGNMBR", "Period posting number", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("PSTGNMBR", "Posting number", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("PERIODID", "Period ID", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svGlTrx.AddField("RVRSNGDT", "Reversing date", Connector.FieldTypeIdDate);
            svGlTrx.AddField("RCRNGTRX", "Recurring transaction", Connector.FieldTypeIdYesNo);
            svGlTrx.AddField("RVTRXSRC", "Reversing transaction source", Connector.FieldTypeIdString);
            svGlTrx.AddField("DTAControlNum", "DTA control number", Connector.FieldTypeIdString);
            svGlTrx.AddField("CLOSEDYR", "Closed year", Connector.FieldTypeIdString);
            svGlTrx.AddField("HISTRX", "History transaction", Connector.FieldTypeIdYesNo);
            svGlTrx.AddField("REVPRDID", "Reversing period ID", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("REVYEAR", "Reversing year", Connector.FieldTypeIdString);
            svGlTrx.AddField("REVCLYR", "Reversing closed year", Connector.FieldTypeIdString);
            svGlTrx.AddField("REVHIST", "Reversing history transaction", Connector.FieldTypeIdYesNo);
            svGlTrx.AddField("ICDISTS", "Intercompany distributions", Connector.FieldTypeIdYesNo);
            svGlTrx.AddField("PRNTSTUS", "Printing status", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("DECPLACS - 1", "Decimal places", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("GLLINMSG", "GL line messages", Connector.FieldTypeIdString);
            svGlTrx.AddField("GLLINMS2", "GL line messages 2", Connector.FieldTypeIdString);
            svGlTrx.AddField("GLHDRMSG", "GL header messages", Connector.FieldTypeIdString);
            svGlTrx.AddField("GLHDRMS2", "GL header messages 2", Connector.FieldTypeIdString);
            svGlTrx.AddField("ERRSTATE", "Error state", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("DTATRXType", "DTA transaction type", Connector.FieldTypeIdInteger);
            gl00100.AddField("MNACSGMT", "Main account segment", Connector.FieldTypeIdString);
            gl00100.AddField("ACTDESCR", "Account description from account master", Connector.FieldTypeIdString);
            gl00100.AddField("ACCATNUM", "Account category number", Connector.FieldTypeIdString);
            gl00100.AddField("ACTIVE", "Active", Connector.FieldTypeIdYesNo);
            gl00100.AddField("DSPLKUPS", "Display in lookups", Connector.FieldTypeIdString);
            gl00100.AddField("HSTRCLRT", "Historical rate", Connector.FieldTypeIdCurrency);
            gl00100.AddField("NOTEINDX", "Note index from account master", Connector.FieldTypeIdInteger);

            var createDate = gl00100.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            createDate.CreateDate = true;

            var modifyDate = gl00100.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            modifyDate.ModifyDate = true;

            gl00100.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            gl00100.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            gl00100.AddField("ADJINFL", "Adjust for inflation", Connector.FieldTypeIdYesNo);
            gl00100.AddField("INFLAEQU", "Inflation equity account index", Connector.FieldTypeIdInteger);
            gl00100.AddField("INFLAREV", "Inflation revenue account index", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("CorrespondingUnit", "Corresponding unit", Connector.FieldTypeIdString);
            svGlTrx.AddField("Tax_date", "Tax date", Connector.FieldTypeIdDate);
            svGlTrx.AddField("BCHSOURC", "Batch source", Connector.FieldTypeIdString);
            svGlTrx.AddField("VOIDED", "Voided", Connector.FieldTypeIdYesNo);
            svGlTrx.AddField("Back_Out_JE", "Back out journal entry", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("Original_JE", "Original journal entry", Connector.FieldTypeIdInteger);
            svGlTrx.AddField("Correcting_JE", "Correcting journal entry", Connector.FieldTypeIdInteger);

            for (int i = 1; i <= 5; i++) gl00100.AddField($"ACTNUMBR_{i}", $"Segment {i}", Connector.FieldTypeIdString);

            var accountType = gl00100.AddField("ACCTTYPE", "Account type", Connector.FieldTypeIdEnum);
            accountType.AddListItems(1, new List<string> { "Posting account", "Unit account", "Posting allocation account", "Unit allocation account" });
            
            var documentStatus = svGlTrx.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Work", "Open", "History" });
            
            var balanceForCalculation = svGlTrx.AddField("BALFRCLC", "Balance for calculation", Connector.FieldTypeIdEnum);
            balanceForCalculation.AddListItems(0, new List<string> { "Net change", "Period balances" });
            
            var fixedOrVariable = svGlTrx.AddField("FXDORVAR", "Fixed or variable", Connector.FieldTypeIdEnum);
            fixedOrVariable.AddListItems(1, new List<string> { "Fixed allocation", "Variable allocation" });
            
            var originatingDtaSeries = svGlTrx.AddField("OrigDTASeries", "Originating DTA series", Connector.FieldTypeIdEnum);
            originatingDtaSeries.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
            
            var originatingTransactionType = svGlTrx.AddField("ORTRXTYP", "Originating transaction type", Connector.FieldTypeIdEnum);
            originatingTransactionType.AddListItems(1, new List<string> { "Standard", "Reversing", "Clearing" });
            
            var originatingType = svGlTrx.AddField("ORGNATYP", "Originating type", Connector.FieldTypeIdEnum);
            originatingType.AddListItems(1, new List<string> { "Normal", "Clearing", "Recurring", "Business form" });
            
            var rateCalculationMethod = svGlTrx.AddField("RTCLCMTD", "Rate calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var postingType = gl00100.AddField("PSTNGTYP", "Posting type", Connector.FieldTypeIdEnum);
            postingType.AddListItems(0, new List<string> { "Balance sheet", "Profit and loss" });
            
            var lineStatus = svGlTrx.AddField("LNESTAT", "Line status", Connector.FieldTypeIdEnum);
            lineStatus.AddListItems(1, new List<string> { "Gain or loss", "Normal rounding", "One sided", "Other rounding", "Standard", "Unit account", "Exchange rate variance" });
            
            var mcTransactionState = svGlTrx.AddField("MCTRXSTT", "MC transaction state", Connector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-denomination to non-denomination", "Non-denomination to Euro", "Non-denomination to denomination", "Denomination to non-denomination", "Denomination to denomination", "Denomination to Euro", "Euro to denomination", "Euro to non-denomination" });
            
            var transactionType = svGlTrx.AddField("TRXTYPE", "Transaction type", Connector.FieldTypeIdEnum);
            transactionType.AddListItems(1, new List<string> { "Standard", "Reversing", "Clearing" });
            
            var glHeaderValue = svGlTrx.AddField("GLHDRVAL", "GL header valid", Connector.FieldTypeIdEnum);
            glHeaderValue.AddListItems(1, new List<string> { "Valid account number", "Valid journal entry", "Valid posting date" });
            
            var glLineValid = svGlTrx.AddField("GLLINVAL", "GL line valid", Connector.FieldTypeIdEnum);
            glLineValid.AddListItems(1, new List<string> { "Valid account number", "Valid offset account number" });
            
            var accountTypeFromAccountMaster = gl00100.AddField("ACCTTYPE", "Account type from account master", Connector.FieldTypeIdEnum);
            accountTypeFromAccountMaster.AddListItems(1, new List<string> { "Posting account", "Unit account", "Posting allocation account", "Unit allocation account" });
            
            var postingTypeFromAccountMaster = gl00100.AddField("PSTNGTYP", "Posting type from account master", Connector.FieldTypeIdEnum);
            postingTypeFromAccountMaster.AddListItems(0, new List<string> { "Balance sheet", "Profit and loss" });
            
            var typicalBalance = gl00100.AddField("TPCLBLNC", "Typical balance", Connector.FieldTypeIdEnum);
            typicalBalance.AddListItems(0, new List<string> { "Debit", "Credit" });
            
            var fixedOrVariableFromAccountMaster = gl00100.AddField("FXDORVAR", "Fixed or variable from account master", Connector.FieldTypeIdEnum);
            fixedOrVariableFromAccountMaster.AddListItems(1, new List<string> { "Fixed allocation", "Variable allocation" });
            
            var balanceForCalculationFromAccountMaster = gl00100.AddField("BALFRCLC", "Balance for calculation from account master", Connector.FieldTypeIdEnum);
            balanceForCalculationFromAccountMaster.AddListItems(0, new List<string> { "Net change", "Period balances" });
            
            var conversionMethod = gl00100.AddField("CNVRMTHD", "Conversion method", Connector.FieldTypeIdEnum);
            conversionMethod.AddListItems(1, new List<string> { "Current", "Weighted average", "Historical" });
            
            var postSalesIn = gl00100.AddField("PostSlsIn", "Post sales in", Connector.FieldTypeIdEnum);
            postSalesIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postInventoryIn = gl00100.AddField("PostIvIn", "Post inventory in", Connector.FieldTypeIdEnum);
            postInventoryIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPurchasingIn = gl00100.AddField("PostPurchIn", "Post purchasing in", Connector.FieldTypeIdEnum);
            postPurchasingIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPayrollIn = gl00100.AddField("PostPRIn", "Post payroll in", Connector.FieldTypeIdEnum);
            postPayrollIn.AddListItems(1, new List<string> { "Detail", "Summary" });

        }

        private ConnectorEntity GetAccountSummaryEntity()
        {
            var entity = new ConnectorEntity(GpSmartListAccountSummary, "Account summary", ParentConnector);

            var svGlSum = entity.AddTable("svGLSum");

            var gl00100 = entity.AddTable("GL00100", "svGLSum", ConnectorTable.ConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            AddAccountSummaryEntityFields(svGlSum, gl00100);

            return entity;
        }
        private static void AddAccountSummaryEntityFields(ConnectorTable svGlSum, ConnectorTable gl00100)
        {
            
            var year = svGlSum.AddField("YEAR1", "Year", Connector.FieldTypeIdString, true);
            year.KeyNumber = 1;

            var period = svGlSum.AddField("PERIODID", "Period ID", Connector.FieldTypeIdInteger, true);
            period.KeyNumber = 2;

            gl00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex, true);
            gl00100.AddField("ACTDESCR", "Account description", Connector.FieldTypeIdString, true);
            svGlSum.AddField("CRDTAMNT", "Credit amount", Connector.FieldTypeIdCurrency, true);
            svGlSum.AddField("DEBITAMT", "Debit amount", Connector.FieldTypeIdCurrency, true);

            var accountIndex = svGlSum.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger);
            accountIndex.KeyNumber = 3;

            svGlSum.AddField("PERDBLNC", "Period balance", Connector.FieldTypeIdCurrency);
            gl00100.AddField("ACTALIAS", "Account alias", Connector.FieldTypeIdString);
            gl00100.AddField("ACTIVE", "Active", Connector.FieldTypeIdYesNo);
            svGlSum.AddField("ACCATNUM", "Account category number", Connector.FieldTypeIdString);
            gl00100.AddField("MNACSGMT", "Main account segment", Connector.FieldTypeIdString);
            gl00100.AddField("DECPLACS - 1", "Decimal places", Connector.FieldTypeIdInteger);
            gl00100.AddField("DSPLKUPS", "Display in lookups", Connector.FieldTypeIdString);
            gl00100.AddField("HSTRCLRT", "Historical rate", Connector.FieldTypeIdCurrency);
            gl00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            var createDate = gl00100.AddField("CREATDDT", "Created date", Connector.FieldTypeIdDate);
            createDate.CreateDate = true;

            var modifyDate = gl00100.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            modifyDate.ModifyDate = true;

            gl00100.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            gl00100.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            gl00100.AddField("ADJINFL", "Adjust for inflation", Connector.FieldTypeIdYesNo);
            gl00100.AddField("INFLAEQU", "Inflation equity account index", Connector.FieldTypeIdInteger);
            gl00100.AddField("INFLAREV", "Inflation revenue account index", Connector.FieldTypeIdInteger);

            for (var i = 1; i <= 5; i++) gl00100.AddField($"ACTNUMBR_{i}", $"Segment {i}", Connector.FieldTypeIdString);

            var documentStatus = svGlSum.AddField("ASI_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Current", "", "History" });
            
            var accountType = gl00100.AddField("ACCTTYPE", "Account type", Connector.FieldTypeIdEnum);
            accountType.AddListItems(1, new List<string> { "Posting account", "Unit account", "Posting allocation account", "Unit allocation account" });
            
            var postingType = gl00100.AddField("PSTNGTYP", "Posting type", Connector.FieldTypeIdEnum);
            postingType.AddListItems(0, new List<string> { "Balance sheet", "Profit and loss" });
            
            var typicalBalance = gl00100.AddField("TPCLBLNC", "Typical balance", Connector.FieldTypeIdEnum);
            typicalBalance.AddListItems(0, new List<string> { "Debit", "Credit" });
            
            var fixedOrVariable = gl00100.AddField("FXDORVAR", "Fixed or variable", Connector.FieldTypeIdEnum);
            fixedOrVariable.AddListItems(1, new List<string> { "Fixed allocation", "Variable allocation" });
            
            var balanceForCalculation = gl00100.AddField("BALFRCLC", "Balance for calculation", Connector.FieldTypeIdEnum);
            balanceForCalculation.AddListItems(0, new List<string> { "Net change", "Period balances" });
            
            var conversionMethod = gl00100.AddField("CNVRMTHD", "Conversion method", Connector.FieldTypeIdEnum);
            conversionMethod.AddListItems(1, new List<string> { "Current", "Weighted average", "Historical" });
            
            var postSalesIn = gl00100.AddField("PostSlsIn", "Post sales in", Connector.FieldTypeIdEnum);
            postSalesIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postInventoryIn = gl00100.AddField("PostIvIn", "Post inventory in", Connector.FieldTypeIdEnum);
            postInventoryIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPurchasingIn = gl00100.AddField("PostPurchIn", "Post purchasing in", Connector.FieldTypeIdEnum);
            postPurchasingIn.AddListItems(1, new List<string> { "Detail", "Summary" });
            
            var postPayrollIn = gl00100.AddField("PostPRIn", "Post payroll in", Connector.FieldTypeIdEnum);
            postPayrollIn.AddListItems(1, new List<string> { "Detail", "Summary" });

        }

        private ConnectorEntity GetBankTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListBankTrx, "Bank transaction", ParentConnector);

            var svBankTrx = entity.AddTable("svBankTrx");

            var cm20300 = entity.AddTable("CM20300", "svBankTrx");
            cm20300.AddJoinFields("CMRECNUM", "CMRECNUM");

            var cm20200 = entity.AddTable("CM20200", "svBankTrx");
            cm20200.AddJoinFields("CMRECNUM", "CMRECNUM");

            AddBankTransactionEntityFields(svBankTrx, cm20300, cm20200);

            return entity;
        }
        private static void AddBankTransactionEntityFields(ConnectorTable svBankTrx, ConnectorTable cm20300, ConnectorTable cm20200)
        {
            svBankTrx.AddField("CHEKBKID", "Checkbook ID", Connector.FieldTypeIdString, true);
            svBankTrx.AddField("GLPOSTDT", "GL posting date", Connector.FieldTypeIdDate, true);
            svBankTrx.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString, true);
            svBankTrx.AddField("SOURCDOC", "Source document", Connector.FieldTypeIdString, true);
            svBankTrx.AddField("Checkbook_Amount", "Checkbook amount", Connector.FieldTypeIdCurrency, true);

            var recordNumber = svBankTrx.AddField("CMRECNUM", "Record number", Connector.FieldTypeIdInteger);
            recordNumber.KeyNumber = 1;

            svBankTrx.AddField("sRecNum", "Record number string", Connector.FieldTypeIdString);
            cm20300.AddField("DEPOSITED", "Deposited", Connector.FieldTypeIdYesNo);
            svBankTrx.AddField("MDFUSRID", "Modified user ID", Connector.FieldTypeIdString);
            svBankTrx.AddField("MODIFDT", "Modified date", Connector.FieldTypeIdDate);
            cm20300.AddField("depositnumber", "Deposit number", Connector.FieldTypeIdString);
            cm20300.AddField("RCPTNMBR", "Receipt number", Connector.FieldTypeIdString);
            cm20300.AddField("receiptdate", "Receipt date", Connector.FieldTypeIdDate);
            cm20300.AddField("RCPTAMT", "Receipt amount", Connector.FieldTypeIdCurrency);
            cm20300.AddField("CARDNAME", "Card name", Connector.FieldTypeIdString);
            svBankTrx.AddField("CURNCYID", "Currency ID", Connector.FieldTypeIdString);
            cm20300.AddField("BANKNAME", "Bank name", Connector.FieldTypeIdString);
            cm20300.AddField("BNKBRNCH", "Bank branch", Connector.FieldTypeIdString);
            svBankTrx.AddField("POSTEDDT", "Posted date", Connector.FieldTypeIdDate);
            svBankTrx.AddField("PTDUSRID", "Posted user ID", Connector.FieldTypeIdString);
            svBankTrx.AddField("CMLinkID", "CM link ID", Connector.FieldTypeIdString);
            cm20300.AddField("RcvdFrom", "Received from", Connector.FieldTypeIdString);
            svBankTrx.AddField("VOIDED", "Voided", Connector.FieldTypeIdYesNo);
            svBankTrx.AddField("VOIDdate", "Void date", Connector.FieldTypeIdDate);
            svBankTrx.AddField("VOIDPdate", "Void GL posting date", Connector.FieldTypeIdDate);
            svBankTrx.AddField("VOIDDESC", "Void description", Connector.FieldTypeIdString);
            svBankTrx.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            svBankTrx.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            svBankTrx.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            svBankTrx.AddField("SRCDOCNUM", "Source document number", Connector.FieldTypeIdString);
            svBankTrx.AddField("SRCDOCTYP", "Source document type", Connector.FieldTypeIdInteger);
            svBankTrx.AddField("AUDITTRAIL", "Audit trail code", Connector.FieldTypeIdString);
            svBankTrx.AddField("ORIGAMT", "Originating amount", Connector.FieldTypeIdCurrency);
            svBankTrx.AddField("RATETPID", "Rate type ID", Connector.FieldTypeIdString);
            cm20300.AddField("RCVGRATETPID", "Receiving rate type ID", Connector.FieldTypeIdString);
            svBankTrx.AddField("EXGTBLID", "Exchange table ID", Connector.FieldTypeIdString);
            svBankTrx.AddField("XCHGRATE", "Exchange rate", Connector.FieldTypeIdQuantity);
            cm20300.AddField("Receiving_Exchange_Rate", "Receiving exchange rate", Connector.FieldTypeIdQuantity);
            svBankTrx.AddField("EXCHdate", "Exchange date", Connector.FieldTypeIdDate);
            svBankTrx.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            svBankTrx.AddField("EXPNdate", "Expiration date", Connector.FieldTypeIdDate);
            svBankTrx.AddField("CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            svBankTrx.AddField("DECPLCUR - 1 ", "Decimal places currency", Connector.FieldTypeIdInteger);
            cm20300.AddField("RLGANLOS", "Realized gain-loss amount", Connector.FieldTypeIdCurrency);
            cm20300.AddField("Cash_Account_index", "Cash account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            cm20300.AddField("Realized_GL_Account_Inde", "Realized GL account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            svBankTrx.AddField("DENXRATE", "Denomination exchange rate", Connector.FieldTypeIdQuantity);
            cm20300.AddField("Receiving_DenomEXRate", "Receiving denomination exchange rate", Connector.FieldTypeIdQuantity);
            cm20200.AddField("CMTrxNum", "CM transaction number", Connector.FieldTypeIdString);
            cm20200.AddField("TRXdate", "Transaction date", Connector.FieldTypeIdDate);
            cm20200.AddField("TRXAMNT", "Transaction amount", Connector.FieldTypeIdCurrency);
            cm20200.AddField("paidtorcvdfrom", "Paid to received from", Connector.FieldTypeIdString);
            cm20200.AddField("Recond", "Reconciled", Connector.FieldTypeIdYesNo);
            cm20200.AddField("RECONUM", "Reconcile number", Connector.FieldTypeIdInteger);
            cm20200.AddField("ClrdAmt", "Cleared amount", Connector.FieldTypeIdCurrency);
            cm20200.AddField("clearedate", "Cleared date", Connector.FieldTypeIdDate);
            cm20200.AddField("Xfr_Record_Number", "Transfer record number", Connector.FieldTypeIdInteger);

            var controlType = cm20300.AddField("CNTRLTYP", "Control type", Connector.FieldTypeIdEnum);
            controlType.AddListItems(1, new List<string> { "Transaction", "Receipt" });
            
            var receiptType = cm20300.AddField("RcpType", "Receipt type", Connector.FieldTypeIdEnum);
            receiptType.AddListItems(1, new List<string> { "Check", "Cash", "Credit card" });
            
            var recordStatus = svBankTrx.AddField("RCRDSTTS", "Record status", Connector.FieldTypeIdEnum);
            recordStatus.AddListItems(1, new List<string> { "Begin", "Commit", "Voiding" });
            
            var rateCalculationMethod = svBankTrx.AddField("RTCLCMTD", "Rate calculation method", Connector.FieldTypeIdEnum);
            rateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var receivingRateCalculationMethod = cm20300.AddField("RCVGRTCLCMTD", "Receiving rate calculation method", Connector.FieldTypeIdEnum);
            receivingRateCalculationMethod.AddListItems(0, new List<string> { "", "Multiply", "Divide" });
            
            var mcTransactionState = svBankTrx.AddField("MCTRXSTT", "Multicurrency transaction state", Connector.FieldTypeIdEnum);
            mcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-denomination to non-denomination", "Non-denomination to Euro", "Non-denomination to denomination", "Denomination to non-denomination", "Denomination to denomination", "Denomination to Euro", "Euro to denomination", "Euro to non-denomination" });
            
            var receivingMcTransactionState = cm20300.AddField("Receiving_MC_Transaction", "Receiving MC transaction state", Connector.FieldTypeIdEnum);
            receivingMcTransactionState.AddListItems(1, new List<string> { "No Euro", "Non-denomination to non-denomination", "Non-denomination to Euro", "Non-denomination to denomination", "Denomination to non-denomination", "Denomination to denomination", "Denomination to Euro", "Euro to denomination", "Euro to non-denomination" });
            
            var documentStatus = svBankTrx.AddField("ASI_CM_Document_Status", "Document status", Connector.FieldTypeIdEnum);
            documentStatus.AddListItems(0, new List<string> { "Receipt", "", "Transaction" });
            
            var cmTransactionType = cm20200.AddField("CMTrxType", "CM transaction type", Connector.FieldTypeIdEnum);
            cmTransactionType.AddListItems(1, new List<string> { "Deposit", "Receipt", "Check", "Withdrawl", "Increase adjustment", "Decrease adjustment", "Interest income", "Other income", "Other expense", "Service charge" });
            
            var depositType = cm20200.AddField("DEPTYPE", "Deposit type", Connector.FieldTypeIdEnum);
            depositType.AddListItems(1, new List<string> { "Deposit with receipts", "Deposit without receipts", "Clearing deposit" });

        }

    }
}
