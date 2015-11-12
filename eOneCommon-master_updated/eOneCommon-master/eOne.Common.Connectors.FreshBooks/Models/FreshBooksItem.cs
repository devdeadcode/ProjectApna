using System.ComponentModel;

namespace eOne.Common.Connectors.FreshBooks.Models
{
    public class FreshBooksItem : ConnectorEntityModel
    {

        #region Enums

        public enum FreshBooksItemFolder
        {
            [Description("Active")]
            active,
            [Description("Archived")]
            archived,
            [Description("Deleted")]
            deleted
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name {get ; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description {get ; set; }

        [FieldSettings("Unit cost", FieldTypeId = Connector.FieldTypeIdCurrency, DefaultField = true)]
        public decimal unit_cost {get ; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int item_id { get; set; }

        [FieldSettings("Quantity", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int quantity {get ; set; }

        [FieldSettings("Folder", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(FreshBooksItemFolder))]
        public FreshBooksItemFolder folder { get; set; }

        #endregion

        #region Hidden properties

        public string SitePrefix {get;set;}
        public int? inventory { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("View item URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string Url => $"https://{SitePrefix}.freshbooks.com/showInvoiceItem?itemid={item_id}";

        [FieldSettings("Edit item URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string EditUrl => $"https://{SitePrefix}.freshbooks.com/updateInvoiceItem?itemid={item_id}";

        [FieldSettings("Current stock", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int CurrentStock => inventory ?? 0;

        [FieldSettings("Track inventory", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool TrackInventory => inventory == null;

        #endregion

    }
}
