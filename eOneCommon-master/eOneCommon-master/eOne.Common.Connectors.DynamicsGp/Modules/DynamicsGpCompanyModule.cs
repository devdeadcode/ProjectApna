using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class DynamicsGpCompanyModule : DynamicsGpModule
    {

        private const short GpSmartListTaxDetailTrx = 29;

        public DynamicsGpCompanyModule(DynamicsGpConnector connector) : base(connector)
        {
            Name = "Company";
            Installed = true;
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetTaxDetailTransactionEntity());
        }

        public DataConnectorEntity GetTaxDetailTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListTaxDetailTrx, "Tax detail transactions", ParentConnector);

            var tx30000 = entity.AddTable("TX30000");
            
            var tx00201 = entity.AddTable("TX00201", "TX30000", DataConnectorTable.DataConnectorTableJoinType.Inner);
            tx00201.AddJoinFields("TAXDTLID", "TAXDTLID");
            
            var gl00100 = entity.AddTable("GL00100", "TX00201");
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");
            
            var rm00101 = entity.AddTable("RM00101", "TX30000");
            rm00101.AddJoinFields("CUSTNMBR", "CustomerVendor_ID");
            
            var pm00200 = entity.AddTable("PM00200", "TX30000");
            pm00200.AddJoinFields("VENDORID", "CustomerVendor_ID");

            AddTaxDetailTransactionEntityFields(tx30000, tx00201, gl00100);

            entity.AddCalculation("case when TX30000.TXDTLAMT > 0 then TX30000.TXDTLAMT else TX30000.TXDTLPCT end", "Tax Detail Percent/Amount", DataConnector.FieldTypeIdPercentage, true);
            entity.AddCalculation("case when TX30000.SERIES in (1, 2, 3) then RM00101.CUSTNAME when TX30000.SERIES in (4, 12) then PM00200.VENDNAME else '' end", "Customer/Vendor Name", DataConnector.FieldTypeIdString, true);

            return entity;
        }
        public void AddTaxDetailTransactionEntityFields(DataConnectorTable tx30000, DataConnectorTable tx00201, DataConnectorTable gl00100)
        {
            tx30000.AddField("TX30000.TAXDTLID", "Tax Detail ID", DataConnector.FieldTypeIdString, true);
            tx00201.AddField("TX00201.TXDTLDSC", "Tax Detail Description", DataConnector.FieldTypeIdString, true);
            var taxDetailType = tx00201.AddField("TX00201.TXDTLTYP", "Tax Detail Type", DataConnector.FieldTypeIdEnum, true);
            taxDetailType.AddListItems(1, new List<string> { "Sales", "Purchases" });
            tx30000.AddField("TX30000.DOCNUMBR", "Document Number", DataConnector.FieldTypeIdString, true);
            tx30000.AddField("TX30000.CustomerVendor_ID", "Customer/Vendor ID", DataConnector.FieldTypeIdString, true);
            tx30000.AddField("TX30000.DOCDATE", "Document Date", DataConnector.FieldTypeIdDate, true);
            tx30000.AddField("TX30000.PSTGDATE", "Posting Date", DataConnector.FieldTypeIdDate, true);
            tx30000.AddField("TX30000.DOCAMNT", "Total Sales/Purchases", DataConnector.FieldTypeIdCurrency, true);
            tx30000.AddField("TX30000.Taxable_Amount", "Taxable Sales/Purchases", DataConnector.FieldTypeIdCurrency, true);
            tx30000.AddField("TX30000.TAXAMNT", "Tax Amount", DataConnector.FieldTypeIdCurrency, true);
            tx30000.AddField("TX30000.DOCTYPE", "Document Type", DataConnector.FieldTypeIdInteger);
            gl00100.AddField("GL00100.ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            tx30000.AddField("TX30000.Tax_Date", "Tax Date", DataConnector.FieldTypeIdDate);
            tx30000.AddField("TX30000.ORTAXAMT", "Originating Tax Amount", DataConnector.FieldTypeIdCurrency);
            tx30000.AddField("TX30000.Originating_Taxable_Amt", "Originating Taxable Sales/Purchases", DataConnector.FieldTypeIdCurrency);
            tx30000.AddField("TX30000.ORDOCAMT", "Originating Total Sales/Purchases", DataConnector.FieldTypeIdCurrency);
            tx30000.AddField("TX30000.ECTRX", "EC Transaction", DataConnector.FieldTypeIdYesNo);
            tx30000.AddField("TX30000.VOIDSTTS", "Voided", DataConnector.FieldTypeIdYesNo);
            tx30000.AddField("TX30000.CURRNIDX", "Currency Index", DataConnector.FieldTypeIdInteger);
            tx30000.AddField("TX30000.Included_On_Return", "Included On Return", DataConnector.FieldTypeIdYesNo);
            tx30000.AddField("TX30000.Tax_Return_ID", "Tax Return ID", DataConnector.FieldTypeIdString);

            var series = tx30000.AddField("TX30000.SERIES", "Series", DataConnector.FieldTypeIdEnum);
            series.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
        }

    }
}
