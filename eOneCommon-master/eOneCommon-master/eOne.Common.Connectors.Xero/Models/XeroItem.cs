using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroItem : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Item code", DefaultField = true)]
        public string Code { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string Description { get; set; }

        #endregion

        #region Hidden properties

        public string ItemID { get; set; }
        public XeroItemDetails PurchaseDetails { get; set; }
        public XeroItemDetails SalesDetails { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Purchase unit price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal PurchaseUnitPrice => PurchaseDetails.UnitPrice;

        [FieldSettings("Purchase account code")]
        public string PurchaseAccountCode => PurchaseDetails.AccountCode;

        [FieldSettings("Sales unit price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal SalesUnitPrice => SalesDetails.UnitPrice;

        [FieldSettings("Sales account code")]
        public string SalesAccountCode => SalesDetails.AccountCode;

        #endregion

    }
}
