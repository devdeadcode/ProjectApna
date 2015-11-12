using System.ComponentModel;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftProduct : ConnectorEntityModel
    {

        #region Enum

        public enum InfusionsoftProductType
        {
            [Description("Digital")]
            Digital,
            [Description("Product")]
            Product
        }
        public enum InfusionsoftProductStatus
        {
            [Description("Active")]
            Active,
            [Description("Inactive")]
            Inactive
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string ProductName { get; set; }

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InfusionsoftProductType), FieldsRequiredForCalculation = "NeedsDigitalDelivery")]
        public InfusionsoftProductType ProductType => NeedsDigitalDelivery == 0 ? InfusionsoftProductType.Product : InfusionsoftProductType.Digital;

        [FieldSettings("Short description", DefaultField = true)]
        public string ShortDescription { get; set; }

        [FieldSettings("Price", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? ProductPrice { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InfusionsoftProductStatus), FieldsRequiredForCalculation = "Status")]
        public InfusionsoftProductStatus ProductStatus => Status == 1 ? InfusionsoftProductStatus.Active : InfusionsoftProductStatus.Inactive;

        #endregion

        #region Properties

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int? Id { get; set; }

        [FieldSettings("SKU")]
        public string Sku { get; set; }

        [FieldSettings("Top HTML")]
        public string TopHTML { get; set; }

        [FieldSettings("Bottom HTML")]
        public string BottomHTML { get; set; }

        [FieldSettings("Weight", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal Weight { get; set; }

        #endregion

        #region Hidden properties

        public int? InventoryLimit { get; set; }
        public int? IsPackage { get; set; }
        public string ShippingTime { get; set; }
        public string InventoryNotifiee { get; set; }

        public int? NeedsDigitalDelivery { get; set; }
        public int? Status { get; set; }
        public int? HideInStore { get; set; }
        public int? Shippable { get; set; }
        public int? Taxable { get; set; }
        public int? CityTaxable { get; set; }
        public int? CountryTaxable { get; set; }
        public int? StateTaxable { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Hide in store", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ProductHideInStore => HideInStore == 1;

        [FieldSettings("Shipping", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ProductShippable => Shippable == 1;

        [FieldSettings("Taxable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ProductTaxable => Taxable == 1;

        [FieldSettings("City taxable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ProductCityTaxable => CityTaxable == 1;

        [FieldSettings("Country taxable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ProductCountryTaxable => CountryTaxable == 1;

        [FieldSettings("State taxable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ProductStateTaxable => StateTaxable == 1;

        #endregion

    }
}
