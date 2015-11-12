using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class DynamicsGpCompanyModule : DynamicsGpModule
    {

        private const short GpSmartListTaxDetailTrx = 29;

        public DynamicsGpCompanyModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 9;
            Name = "Company";
            Installed = true;
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetTaxDetailTransactionEntity());
        }

        public ConnectorEntity GetTaxDetailTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListTaxDetailTrx, "Tax detail transactions", ParentConnector);

            var tx30000 = entity.AddTable("TX30000");
            
            var tx00201 = entity.AddTable("TX00201", "TX30000", ConnectorTable.ConnectorTableJoinType.Inner);
            tx00201.AddJoinFields("TAXDTLID", "TAXDTLID");
            
            var gl00100 = entity.AddTable("GL00100", "TX00201");
            gl00100.AddJoinFields("ACTINDX", "ACTINDX");
            
            var rm00101 = entity.AddTable("RM00101", "TX30000");
            rm00101.AddJoinFields("CUSTNMBR", "CustomerVendor_ID");
            
            var pm00200 = entity.AddTable("PM00200", "TX30000");
            pm00200.AddJoinFields("VENDORID", "CustomerVendor_ID");

            AddTaxDetailTransactionEntityFields(tx30000, tx00201, gl00100);

            entity.AddCalculation("case when TX30000.TXDTLAMT > 0 then TX30000.TXDTLAMT else TX30000.TXDTLPCT end", "Tax Detail Percent/Amount", Connector.FieldTypeIdPercentage, true);
            entity.AddCalculation("case when TX30000.SERIES in (1, 2, 3) then RM00101.CUSTNAME when TX30000.SERIES in (4, 12) then PM00200.VENDNAME else '' end", "Customer/Vendor name", Connector.FieldTypeIdString, true);

            return entity;
        }
        public void AddTaxDetailTransactionEntityFields(ConnectorTable tx30000, ConnectorTable tx00201, ConnectorTable gl00100)
        {
            var detailId = tx30000.AddField("TX30000.TAXDTLID", "Tax detail ID", Connector.FieldTypeIdString, true);
            detailId.KeyNumber = 1;

            tx00201.AddField("TX00201.TXDTLDSC", "Tax detail description", Connector.FieldTypeIdString, true);
            var taxDetailType = tx00201.AddField("TX00201.TXDTLTYP", "Tax detail type", Connector.FieldTypeIdEnum, true);
            taxDetailType.AddListItems(1, new List<string> { "Sales", "Purchases" });
            tx30000.AddField("TX30000.DOCNUMBR", "Document number", Connector.FieldTypeIdString, true);
            tx30000.AddField("TX30000.CustomerVendor_ID", "Customer/vendor ID", Connector.FieldTypeIdString, true);
            tx30000.AddField("TX30000.DOCdate", "Document date", Connector.FieldTypeIdDate, true);
            tx30000.AddField("TX30000.PSTGdate", "Posting date", Connector.FieldTypeIdDate, true);
            tx30000.AddField("TX30000.DOCAMNT", "Total sales/purchases", Connector.FieldTypeIdCurrency, true);
            tx30000.AddField("TX30000.Taxable_Amount", "Taxable sales/purchases", Connector.FieldTypeIdCurrency, true);
            tx30000.AddField("TX30000.TAXAMNT", "Tax amount", Connector.FieldTypeIdCurrency, true);
            tx30000.AddField("TX30000.DOCTYPE", "Document type", Connector.FieldTypeIdInteger);
            gl00100.AddField("GL00100.ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            tx30000.AddField("TX30000.Tax_date", "Tax date", Connector.FieldTypeIdDate);
            tx30000.AddField("TX30000.ORTAXAMT", "Originating tax amount", Connector.FieldTypeIdCurrency);
            tx30000.AddField("TX30000.Originating_Taxable_Amt", "Originating taxable sales/purchases", Connector.FieldTypeIdCurrency);
            tx30000.AddField("TX30000.ORDOCAMT", "Originating total sales/purchases", Connector.FieldTypeIdCurrency);
            tx30000.AddField("TX30000.ECTRX", "EC transaction", Connector.FieldTypeIdYesNo);
            tx30000.AddField("TX30000.VOIDSTTS", "Voided", Connector.FieldTypeIdYesNo);
            tx30000.AddField("TX30000.CURRNIDX", "Currency index", Connector.FieldTypeIdInteger);
            tx30000.AddField("TX30000.Included_On_Return", "Included on return", Connector.FieldTypeIdYesNo);
            tx30000.AddField("TX30000.Tax_Return_ID", "Tax return ID", Connector.FieldTypeIdString);

            var series = tx30000.AddField("TX30000.SERIES", "Series", Connector.FieldTypeIdEnum);
            series.AddListItems(1, new List<string> { "All", "Financial", "Sales", "Purchasing", "Inventory", "Payroll", "Project" });
        }

    }
}
