using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class AnalyticalAccountingModule : DynamicsGpModule
    {

        private const short AnalyticalAccountingTrxDimensions = 1;
        private const short AnalyticalAccountingTrxDimensionCodes = 2;
        private const short AnalyticalAccountingAccountingClasses = 3;
        private const short AnalyticalAccountingTrees = 4;
        private const short AnalyticalAccountingDistributionQueries = 6;
        private const short AnalyticalAccountingMultilevelQueries = 7;
        private const short AnalyticalAccountingTransactions = 8;
        private const short AnalyticalAccountingDimensionBalances = 9;
        
        public AnalyticalAccountingModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 3180;
            Name = "Analytical Accounting";
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetTrxDimensionEntity());
            Entities.Add(GetTrxDimensionCodeEntity());
            Entities.Add(GetAccountingClassEntity());
            Entities.Add(GetTreeEntity());
            Entities.Add(GetDistributionQueryEntity());
            Entities.Add(GetMultilevelQueryEntity());
            Entities.Add(GetTransactionEntity());
            Entities.Add(GetDimensionBalanceEntity());
        }

        public ConnectorEntity GetTrxDimensionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingTrxDimensions), "Transaction dimensions", ParentConnector);
            var aag00400 = entity.AddTable("AAG00400");
            AddTrxDimensionEntityFields(aag00400);
            return entity;
        }
        public void AddTrxDimensionEntityFields(ConnectorTable aag00400)
        {
            var dimension = aag00400.AddField("aaTrxDim", "Transaction dimension", Connector.FieldTypeIdString, true);
            dimension.KeyNumber = 1;

            var dataType = aag00400.AddField("aaDataType", "Data type", Connector.FieldTypeIdEnum, true);
            dataType.AddListItems(1, new List<string> { "Alphanumeric", "Numeric", "Yes/no", "Date" });
            aag00400.AddField("aaTrxDimDescr", "Description 1", Connector.FieldTypeIdString, true);
            aag00400.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo, true);
            aag00400.AddField("aaTrxDimID", "Transaction dimension ID", Connector.FieldTypeIdInteger);
            aag00400.AddField("aaTrxDimDescr2", "Description 2", Connector.FieldTypeIdString);
            aag00400.AddField("aaOrder", "Order", Connector.FieldTypeIdInteger);
            aag00400.AddField("DECPLQTY - 1", "Decimal places", Connector.FieldTypeIdInteger);
            aag00400.AddField("UOMSCHDL", "U of M schedule ID", Connector.FieldTypeIdString);
            aag00400.AddField("aaAddCodesOnFly", "Create new codes on the fly", Connector.FieldTypeIdYesNo);
            aag00400.AddField("aaDontAskForNewCodes", "Create new codes in background", Connector.FieldTypeIdYesNo);
            
        }
                    
        public ConnectorEntity GetTrxDimensionCodeEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingTrxDimensionCodes), "Transaction dimension codes", ParentConnector);

            var aag00401 = entity.AddTable("AAG00401");

            var aag00400 = entity.AddTable("AAG00400", "AAG00401", ConnectorTable.ConnectorTableJoinType.Inner);
            aag00400.AddJoinFields("aaTrxDimID", "aaTrxDimID");

            var aag00401V = entity.AddTable("AAG00401V", "AAG00401");
            aag00401V.AddJoinFields("aaTrxDimID", "aaTrxDimID");
            aag00401V.AddJoinFields("aaTrxDimCodeID", "aaTrxDimCodeID");

            AddTrxDimensionCodeEntityFields(aag00401, aag00400, aag00401V);

            return entity;
        }
        private void AddTrxDimensionCodeEntityFields(ConnectorTable aag00401, ConnectorTable aag00400, ConnectorTable aag00401V)
        {
            var dimension = aag00400.AddField("aaTrxDim", "Transaction dimension", Connector.FieldTypeIdString, true);
            dimension.KeyNumber = 1;

            var code = aag00401.AddField("aaTrxDimCode", "Transaction dimension code", Connector.FieldTypeIdString, true);
            code.KeyNumber = 2;

            aag00401.AddField("aaTrxDimCodeDescr", "Description 1", Connector.FieldTypeIdString, true);
            aag00401V.AddField("aaNode", "Node", Connector.FieldTypeIdString, true);
            aag00401.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo, true);
            aag00401.AddField("aaTrxDimID", "Transaction dimension ID", Connector.FieldTypeIdInteger);
            aag00401.AddField("aaTrxDimCodeID", "Transaction dimension code ID", Connector.FieldTypeIdInteger);
            aag00401.AddField("aaTrxDimCodeDescr2", "Description 2", Connector.FieldTypeIdString);
        }
                    
        public ConnectorEntity GetAccountingClassEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingAccountingClasses), "Accounting classes", ParentConnector);
            var aag00201 = entity.AddTable("AAG00201");
            AddAccountingClassEntityFields(aag00201);
            return entity;
        }
        public void AddAccountingClassEntityFields(ConnectorTable aag00201)
        {
            var classId = aag00201.AddField("aaAcctClassID", "Account class ID", Connector.FieldTypeIdInteger, true);
            classId.KeyNumber = 1;

            aag00201.AddField("aaAccountClass", "Class ID", Connector.FieldTypeIdString, true);
            aag00201.AddField("aaAcctClassDescr", "Description 1", Connector.FieldTypeIdString, true);
            aag00201.AddField("aaAcctClassDescr2", "Description 2", Connector.FieldTypeIdString, true);
        }
                    
        public ConnectorEntity GetTreeEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingTrees), "Trees", ParentConnector);

            var aag00600 = entity.AddTable("AAG00600");

            var aag00400 = entity.AddTable("AAG00400", "AAG00600");
            aag00400.AddJoinFields("aaTrxDimID", "aaDimID");

            AddTreeEntityFields(aag00600, aag00400);

            return entity;
        }
        public void AddTreeEntityFields(ConnectorTable aag00600, ConnectorTable aag00400)
        {
            var tree = aag00600.AddField("aaTree", "Tree", Connector.FieldTypeIdString, true);
            tree.KeyNumber = 1;

            aag00600.AddField("aaLinkType", "Tree type", Connector.FieldTypeIdEnum, true);
            aag00400.AddField("aaTrxDim", "Transaction dimension", Connector.FieldTypeIdString, true);
            aag00600.AddField("aaTreeDescr", "Description", Connector.FieldTypeIdString, true);
            aag00600.AddField("aaTreeMain", "Main tree", Connector.FieldTypeIdYesNo, true);
            aag00600.AddField("aaTreeID", "Tree ID", Connector.FieldTypeIdInteger);
            aag00600.AddField("aaDimID", "Dimension ID", Connector.FieldTypeIdInteger);
            aag00600.AddField("aaTreeInclAllRec", "Tree includes all records", Connector.FieldTypeIdYesNo);
        }
                    
        public ConnectorEntity GetDistributionQueryEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingDistributionQueries), "Distribution queries", ParentConnector);
            var aag00300 = entity.AddTable("AAG00300");
            AddDistributionQueryEntityFields(aag00300);
            return entity;
        }
        public void AddDistributionQueryEntityFields(ConnectorTable aag00300)
        {
            var queryId = aag00300.AddField("aaDistrQuery", "Query ID", Connector.FieldTypeIdString, true);
            queryId.KeyNumber = 1;

            aag00300.AddField("aaDistrQueryDescr", "Description", Connector.FieldTypeIdString, true);
            var searchType = aag00300.AddField("aaMatchType", "Search type", Connector.FieldTypeIdEnum, true);
            searchType.AddListItems(1, new List<string> { "Match all", "Match 1 or more" });
            aag00300.AddField("aaDistrQueryID", "Dist query ID", Connector.FieldTypeIdInteger);
        }
                    
        public ConnectorEntity GetMultilevelQueryEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingMultilevelQueries), "Multilevel queries", ParentConnector);
            var aag00310 = entity.AddTable("AAG00310");
            AddMultilevelQueryEntityFields(aag00310);
            return entity;
        }
        public void AddMultilevelQueryEntityFields(ConnectorTable aag00310)
        {
            var queryId = aag00310.AddField("aaMLQuery", "Query ID", Connector.FieldTypeIdString, true);
            queryId.KeyNumber = 1;

            aag00310.AddField("aaMLQueryDescr", "Description", Connector.FieldTypeIdString, true);
            var searchType = aag00310.AddField("aaMatchType", "Search type", Connector.FieldTypeIdEnum, true);
            searchType.AddListItems(1, new List<string> { "Match all", "Match 1 or more" });
            aag00310.AddField("aaMLQueryID", "MLQ query ID", Connector.FieldTypeIdInteger);
        }
                    
        public ConnectorEntity GetTransactionEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingTransactions), "Transactions", ParentConnector);

            var aag30003 = entity.AddTable("AAG30003");

            var aag30002 = entity.AddTable("AAG30002", "AAG30003", ConnectorTable.ConnectorTableJoinType.Inner);
            aag30002.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30002.AddJoinFields("aaGLHdrID", "aaGLHdrID");
            aag30002.AddJoinFields("aaGLAssignID", "aaGLAssignID");

            var aag30001 = entity.AddTable("AAG30001", "AAG30003", ConnectorTable.ConnectorTableJoinType.Inner);
            aag30001.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30001.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var aag30000 = entity.AddTable("AAG30000", "AAG30003", ConnectorTable.ConnectorTableJoinType.Inner);
            aag30000.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var gl00100 = entity.AddTable("GL00100", "AAG30001", ConnectorTable.ConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");
            var aag00401 = entity.AddTable("AAG00401", "AAG30003");
            aag00401.AddJoinFields("aaTrxDimID", "aaTrxDimID");
            aag00401.AddJoinFields("aaTrxDimCodeID", "aaTrxCodeID");

            AddTransactionEntityFields(aag30003, aag30002, aag30001, aag30000, gl00100, aag00401);

            return entity;
        }
        public void AddTransactionEntityFields(ConnectorTable aag30003, ConnectorTable aag30002, ConnectorTable aag30001, ConnectorTable aag30000, ConnectorTable gl00100, ConnectorTable aag00401)
        {
            var journalEntry = aag30000.AddField("JRNENTRY", "Journal entry", Connector.FieldTypeIdInteger, true);
            journalEntry.KeyNumber = 1;

            aag30000.AddField("aaTRXSource", "AA transaction source", Connector.FieldTypeIdString, true);
            aag30002.AddField("DEBITAMT", "Debit amount", Connector.FieldTypeIdCurrency, true);
            aag30002.AddField("CRDTAMNT", "Credit amount", Connector.FieldTypeIdCurrency, true);
            aag30001.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger, true);
            gl00100.AddField("ACTDESCR", "Account description", Connector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCode", "Transaction dimension code", Connector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCodeDescr", "Description 1", Connector.FieldTypeIdString);
            aag30000.AddField("YEAR1", "Year", Connector.FieldTypeIdString);
            aag30000.AddField("aaGLTRXSource", "Transaction source", Connector.FieldTypeIdString);
            aag30001.AddField("aaChangedate", "Change date", Connector.FieldTypeIdDate);
            aag30001.AddField("aaChangeTime", "Change time", Connector.FieldTypeIdTime);
            aag30002.AddField("ORDBTAMT", "Originating debit amount", Connector.FieldTypeIdCurrency);
            aag30002.AddField("ORCRDAMT", "Originating credit amount", Connector.FieldTypeIdCurrency);
            aag30002.AddField("aaAssignedPercent", "Assignment percentage", Connector.FieldTypeIdPercentage);
            aag30002.AddField("DistRef", "Distribution reference", Connector.FieldTypeIdString);
            gl00100.AddField("ACTNUMBR", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            aag30000.AddField("Ledger_ID", "Reporting ledger", Connector.FieldTypeIdInteger);
        }
            
        public ConnectorEntity GetDimensionBalanceEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(AnalyticalAccountingDimensionBalances), "Dimension balances", ParentConnector);

            var aag30003 = entity.AddTable("AAG30003");

            var aag30002 = entity.AddTable("AAG30002", "AAG30003", ConnectorTable.ConnectorTableJoinType.Inner);
            aag30002.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30002.AddJoinFields("aaGLHdrID", "aaGLHdrID");
            aag30002.AddJoinFields("aaGLAssignID", "aaGLAssignID");

            var aag30001 = entity.AddTable("AAG30001", "AAG30003", ConnectorTable.ConnectorTableJoinType.Inner);
            aag30001.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30001.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var aag30000 = entity.AddTable("AAG30000", "AAG30003", ConnectorTable.ConnectorTableJoinType.Inner);
            aag30000.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var gl00100 = entity.AddTable("GL00100", "AAG30001", ConnectorTable.ConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            var aag00401 = entity.AddTable("AAG00401", "AAG30003");
            aag00401.AddJoinFields("aaTrxDimID", "aaTrxDimID");
            aag00401.AddJoinFields("aaTrxDimCodeID", "aaTrxCodeID");

            AddDimensionBalanceFields(aag30003, aag30002, aag30001, aag30000, gl00100, aag00401);

            return entity;
        }
        public void AddDimensionBalanceFields(ConnectorTable aag30003, ConnectorTable aag30002, ConnectorTable aag30001, ConnectorTable aag30000, ConnectorTable gl00100, ConnectorTable aag00401)
        {
            aag30002.AddField("DEBITAMT", "Debit amount", Connector.FieldTypeIdCurrency, true);
            aag30002.AddField("CRDTAMNT", "Credit amount", Connector.FieldTypeIdCurrency, true);

            var accountIndex =  aag30001.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger, true);
            accountIndex.KeyNumber = 1;

            gl00100.AddField("ACTDESCR", "Account description", Connector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCode", "Transaction dimension code", Connector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCodeDescr", "Description 1", Connector.FieldTypeIdString, true);
            aag30000.AddField("JRNENTRY", "Journal entry", Connector.FieldTypeIdInteger);
            aag30000.AddField("YEAR1", "Year", Connector.FieldTypeIdString);
            aag30000.AddField("aaGLTRXSource", "Transaction source", Connector.FieldTypeIdString);
            aag30000.AddField("aaTRXSource", "AA transaction source", Connector.FieldTypeIdString);
            aag30001.AddField("aaChangedate", "Change date", Connector.FieldTypeIdDate);
            aag30001.AddField("aaChangeTime", "Change time", Connector.FieldTypeIdTime);
            aag30002.AddField("ORDBTAMT", "Originating debit amount", Connector.FieldTypeIdCurrency);
            aag30002.AddField("ORCRDAMT", "Originating credit amount", Connector.FieldTypeIdCurrency);
            aag30002.AddField("aaAssignedPercent", "Assignment percentage", Connector.FieldTypeIdPercentage);
            aag30002.AddField("DistRef", "Distribution reference", Connector.FieldTypeIdString);
            gl00100.AddField("ACTNUMBR", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
        }

    }
}
