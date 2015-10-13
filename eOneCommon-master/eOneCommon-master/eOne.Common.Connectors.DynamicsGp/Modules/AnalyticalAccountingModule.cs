using System.Collections.Generic;
using eOne.Common.DataConnectors;

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

        public DataConnectorEntity GetTrxDimensionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingTrxDimensions), "Transaction dimensions", ParentConnector);
            var aag00400 = entity.AddTable("AAG00400");
            AddTrxDimensionEntityFields(aag00400);
            return entity;
        }
        public void AddTrxDimensionEntityFields(DataConnectorTable aag00400)
        {
            aag00400.AddField("aaTrxDim", "Trx Dimension", DataConnector.FieldTypeIdString, true);
            var dataType = aag00400.AddField("aaDataType", "Data Type", DataConnector.FieldTypeIdEnum, true);
            dataType.AddListItems(1, new List<string> { "Alphanumeric", "Numeric", "YesNo", "Date" });
            aag00400.AddField("aaTrxDimDescr", "Description 1", DataConnector.FieldTypeIdString, true);
            aag00400.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo, true);
            aag00400.AddField("aaTrxDimID", "Trx Dim ID", DataConnector.FieldTypeIdInteger);
            aag00400.AddField("aaTrxDimDescr2", "Description 2", DataConnector.FieldTypeIdString);
            aag00400.AddField("aaOrder", "Order", DataConnector.FieldTypeIdInteger);
            aag00400.AddField("DECPLQTY - 1", "Decimal Places", DataConnector.FieldTypeIdInteger);
            aag00400.AddField("UOMSCHDL", "U of M Schedule ID", DataConnector.FieldTypeIdString);
            aag00400.AddField("aaAddCodesOnFly", "Create New Codes On The Fly", DataConnector.FieldTypeIdYesNo);
            aag00400.AddField("aaDontAskForNewCodes", "Create New Codes In Background", DataConnector.FieldTypeIdYesNo);
            
        }
                    
        public DataConnectorEntity GetTrxDimensionCodeEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingTrxDimensionCodes), "Transaction dimension codes", ParentConnector);

            var aag00401 = entity.AddTable("AAG00401");

            var aag00400 = entity.AddTable("AAG00400", "AAG00401", DataConnectorTable.DataConnectorTableJoinType.Inner);
            aag00400.AddJoinFields("aaTrxDimID", "aaTrxDimID");

            var aag00401V = entity.AddTable("AAG00401V", "AAG00401");
            aag00401V.AddJoinFields("aaTrxDimID", "aaTrxDimID");
            aag00401V.AddJoinFields("aaTrxDimCodeID", "aaTrxDimCodeID");

            AddTrxDimensionCodeEntityFields(aag00401, aag00400, aag00401V);

            return entity;
        }
        private void AddTrxDimensionCodeEntityFields(DataConnectorTable aag00401, DataConnectorTable aag00400, DataConnectorTable aag00401V)
        {
            aag00400.AddField("aaTrxDim", "Trx Dimension", DataConnector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCode", "Trx Dimension Code", DataConnector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCodeDescr", "Description 1", DataConnector.FieldTypeIdString, true);
            aag00401V.AddField("aaNode", "Node", DataConnector.FieldTypeIdString, true);
            aag00401.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo, true);
            aag00401.AddField("aaTrxDimID", "Trx Dim ID", DataConnector.FieldTypeIdInteger);
            aag00401.AddField("aaTrxDimCodeID", "Trx Dim Code ID", DataConnector.FieldTypeIdInteger);
            aag00401.AddField("aaTrxDimCodeDescr2", "Description 2", DataConnector.FieldTypeIdString);
        }
                    
        public DataConnectorEntity GetAccountingClassEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingAccountingClasses), "Accounting classes", ParentConnector);
            var aag00201 = entity.AddTable("AAG00201");
            AddAccountingClassEntityFields(aag00201);
            return entity;
        }
        public void AddAccountingClassEntityFields(DataConnectorTable aag00201)
        {
            aag00201.AddField("aaAcctClassID", "Account Class ID", DataConnector.FieldTypeIdInteger, true);
            aag00201.AddField("aaAccountClass", "Class ID", DataConnector.FieldTypeIdString, true);
            aag00201.AddField("aaAcctClassDescr", "Description 1", DataConnector.FieldTypeIdString, true);
            aag00201.AddField("aaAcctClassDescr2", "Description 2", DataConnector.FieldTypeIdString, true);
        }
                    
        public DataConnectorEntity GetTreeEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingTrees), "Trees", ParentConnector);

            var aag00600 = entity.AddTable("AAG00600");

            var aag00400 = entity.AddTable("AAG00400", "AAG00600");
            aag00400.AddJoinFields("aaTrxDimID", "aaDimID");

            AddTreeEntityFields(aag00600, aag00400);

            return entity;
        }
        public void AddTreeEntityFields(DataConnectorTable aag00600, DataConnectorTable aag00400)
        {
            aag00600.AddField("aaTree", "Tree", DataConnector.FieldTypeIdString, true);
            aag00600.AddField("aaLinkType", "Tree Type", DataConnector.FieldTypeIdEnum, true);
            aag00400.AddField("aaTrxDim", "Trx Dimension", DataConnector.FieldTypeIdString, true);
            aag00600.AddField("aaTreeDescr", "Description", DataConnector.FieldTypeIdString, true);
            aag00600.AddField("aaTreeMain", "Main Tree", DataConnector.FieldTypeIdYesNo, true);
            aag00600.AddField("aaTreeID", "Tree ID", DataConnector.FieldTypeIdInteger);
            aag00600.AddField("aaDimID", "Dim ID", DataConnector.FieldTypeIdInteger);
            aag00600.AddField("aaTreeInclAllRec", "Tree Includes All Records", DataConnector.FieldTypeIdYesNo);
        }
                    
        public DataConnectorEntity GetDistributionQueryEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingDistributionQueries), "Distribution queries", ParentConnector);
            var aag00300 = entity.AddTable("AAG00300");
            AddDistributionQueryEntityFields(aag00300);
            return entity;
        }
        public void AddDistributionQueryEntityFields(DataConnectorTable aag00300)
        {
            aag00300.AddField("aaDistrQuery", "Query ID", DataConnector.FieldTypeIdString, true);
            aag00300.AddField("aaDistrQueryDescr", "Description", DataConnector.FieldTypeIdString, true);
            var searchType = aag00300.AddField("aaMatchType", "Search Type", DataConnector.FieldTypeIdEnum, true);
            searchType.AddListItems(1, new List<string> { "Match all", "Match 1 or more" });
            aag00300.AddField("aaDistrQueryID", "Dist Query ID", DataConnector.FieldTypeIdInteger);
        }
                    
        public DataConnectorEntity GetMultilevelQueryEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingMultilevelQueries), "Multilevel queries", ParentConnector);
            var aag00310 = entity.AddTable("AAG00310");
            AddMultilevelQueryEntityFields(aag00310);
            return entity;
        }
        public void AddMultilevelQueryEntityFields(DataConnectorTable aag00310)
        {
            aag00310.AddField("aaMLQuery", "Query ID", DataConnector.FieldTypeIdString, true);
            aag00310.AddField("aaMLQueryDescr", "Description", DataConnector.FieldTypeIdString, true);
            var searchType = aag00310.AddField("aaMatchType", "Search Type", DataConnector.FieldTypeIdEnum, true);
            searchType.AddListItems(1, new List<string> { "Match all", "Match 1 or more" });
            aag00310.AddField("aaMLQueryID", "MLQ Query ID", DataConnector.FieldTypeIdInteger);
        }
                    
        public DataConnectorEntity GetTransactionEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingTransactions), "Transactions", ParentConnector);

            var aag30003 = entity.AddTable("AAG30003");

            var aag30002 = entity.AddTable("AAG30002", "AAG30003", DataConnectorTable.DataConnectorTableJoinType.Inner);
            aag30002.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30002.AddJoinFields("aaGLHdrID", "aaGLHdrID");
            aag30002.AddJoinFields("aaGLAssignID", "aaGLAssignID");

            var aag30001 = entity.AddTable("AAG30001", "AAG30003", DataConnectorTable.DataConnectorTableJoinType.Inner);
            aag30001.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30001.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var aag30000 = entity.AddTable("AAG30000", "AAG30003", DataConnectorTable.DataConnectorTableJoinType.Inner);
            aag30000.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var gl00100 = entity.AddTable("GL00100", "AAG30001", DataConnectorTable.DataConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");
            var aag00401 = entity.AddTable("AAG00401", "AAG30003");
            aag00401.AddJoinFields("aaTrxDimID", "aaTrxDimID");
            aag00401.AddJoinFields("aaTrxDimCodeID", "aaTrxCodeID");

            AddTransactionEntityFields(aag30003, aag30002, aag30001, aag30000, gl00100, aag00401);

            return entity;
        }
        public void AddTransactionEntityFields(DataConnectorTable aag30003, DataConnectorTable aag30002, DataConnectorTable aag30001, DataConnectorTable aag30000, DataConnectorTable gl00100, DataConnectorTable aag00401)
        {
            aag30000.AddField("JRNENTRY", "Journal Entry", DataConnector.FieldTypeIdInteger, true);
            aag30000.AddField("aaTRXSource", "AA Transaction Source", DataConnector.FieldTypeIdString, true);
            aag30002.AddField("DEBITAMT", "Debit Amount", DataConnector.FieldTypeIdCurrency, true);
            aag30002.AddField("CRDTAMNT", "Credit Amount", DataConnector.FieldTypeIdCurrency, true);
            aag30001.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger, true);
            gl00100.AddField("ACTDESCR", "Account Description", DataConnector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCode", "Trx Dimension Code", DataConnector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCodeDescr", "Description 1", DataConnector.FieldTypeIdString);
            aag30000.AddField("YEAR1", "Year", DataConnector.FieldTypeIdString);
            aag30000.AddField("aaGLTRXSource", "Transaction Source", DataConnector.FieldTypeIdString);
            aag30001.AddField("aaChangeDate", "aaChangeDate", DataConnector.FieldTypeIdDate);
            aag30001.AddField("aaChangeTime", "aaChangeTime", DataConnector.FieldTypeIdTime);
            aag30002.AddField("ORDBTAMT", "Originating Debit Amount", DataConnector.FieldTypeIdCurrency);
            aag30002.AddField("ORCRDAMT", "Originating Credit Amount", DataConnector.FieldTypeIdCurrency);
            aag30002.AddField("aaAssignedPercent", "Assignment Percentage", DataConnector.FieldTypeIdPercentage);
            aag30002.AddField("DistRef", "Distribution Reference", DataConnector.FieldTypeIdString);
            gl00100.AddField("ACTNUMBR", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            aag30000.AddField("Ledger_ID", "Reporting Ledger", DataConnector.FieldTypeIdInteger);
        }
            
        public DataConnectorEntity GetDimensionBalanceEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(AnalyticalAccountingDimensionBalances), "Dimension balances", ParentConnector);

            var aag30003 = entity.AddTable("AAG30003");

            var aag30002 = entity.AddTable("AAG30002", "AAG30003", DataConnectorTable.DataConnectorTableJoinType.Inner);
            aag30002.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30002.AddJoinFields("aaGLHdrID", "aaGLHdrID");
            aag30002.AddJoinFields("aaGLAssignID", "aaGLAssignID");

            var aag30001 = entity.AddTable("AAG30001", "AAG30003", DataConnectorTable.DataConnectorTableJoinType.Inner);
            aag30001.AddJoinFields("aaGLDistID", "aaGLDistID");
            aag30001.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var aag30000 = entity.AddTable("AAG30000", "AAG30003", DataConnectorTable.DataConnectorTableJoinType.Inner);
            aag30000.AddJoinFields("aaGLHdrID", "aaGLHdrID");

            var gl00100 = entity.AddTable("GL00100", "AAG30001", DataConnectorTable.DataConnectorTableJoinType.Inner);
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");

            var aag00401 = entity.AddTable("AAG00401", "AAG30003");
            aag00401.AddJoinFields("aaTrxDimID", "aaTrxDimID");
            aag00401.AddJoinFields("aaTrxDimCodeID", "aaTrxCodeID");

            AddDimensionBalanceFields(aag30003, aag30002, aag30001, aag30000, gl00100, aag00401);

            return entity;
        }
        public void AddDimensionBalanceFields(DataConnectorTable aag30003, DataConnectorTable aag30002, DataConnectorTable aag30001, DataConnectorTable aag30000, DataConnectorTable gl00100, DataConnectorTable aag00401)
        {
            aag30002.AddField("DEBITAMT", "Debit Amount", DataConnector.FieldTypeIdCurrency, true);
            aag30002.AddField("CRDTAMNT", "Credit Amount", DataConnector.FieldTypeIdCurrency, true);
            aag30001.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger, true);
            gl00100.AddField("ACTDESCR", "Account Description", DataConnector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCode", "Trx Dimension Code", DataConnector.FieldTypeIdString, true);
            aag00401.AddField("aaTrxDimCodeDescr", "Description 1", DataConnector.FieldTypeIdString, true);
            aag30000.AddField("JRNENTRY", "Journal Entry", DataConnector.FieldTypeIdInteger);
            aag30000.AddField("YEAR1", "Year", DataConnector.FieldTypeIdString);
            aag30000.AddField("aaGLTRXSource", "Transaction Source", DataConnector.FieldTypeIdString);
            aag30000.AddField("aaTRXSource", "AA Transaction Source", DataConnector.FieldTypeIdString);
            aag30001.AddField("aaChangeDate", "aaChangeDate", DataConnector.FieldTypeIdDate);
            aag30001.AddField("aaChangeTime", "aaChangeTime", DataConnector.FieldTypeIdTime);
            aag30002.AddField("ORDBTAMT", "Originating Debit Amount", DataConnector.FieldTypeIdCurrency);
            aag30002.AddField("ORCRDAMT", "Originating Credit Amount", DataConnector.FieldTypeIdCurrency);
            aag30002.AddField("aaAssignedPercent", "Assignment Percentage", DataConnector.FieldTypeIdPercentage);
            aag30002.AddField("DistRef", "Distribution Reference", DataConnector.FieldTypeIdString);
            gl00100.AddField("ACTNUMBR", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
        }

    }
}
