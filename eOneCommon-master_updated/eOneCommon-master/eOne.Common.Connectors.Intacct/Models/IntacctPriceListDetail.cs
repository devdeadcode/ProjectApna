using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctPriceListDetail : IntacctBase
    {

        #region Enums

        public enum IntacctPriceListQtyOrValue
        {
            [Description("Quantity")]
            Q,
            [Description("Value")]
            V
        }

        #endregion

        #region Default properties

        [FieldSettings("Price list ID", DefaultField = true, KeyNumber = 1)]
        public string PRICELISTID { get; set; }

        [FieldSettings("Item ID", DefaultField = true, KeyNumber = 2)]
        public string ITEMID { get; set; }

        [FieldSettings("Employee ID", KeyNumber = 3)]
        public string EMPLOYEEID { get; set; }

        [FieldSettings("Product line ID", KeyNumber = 4)]
        public string PRODUCTLINEID { get; set; }

        [FieldSettings("Start date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATEFROM { get; set; }

        [FieldSettings("End date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATETO { get; set; }

        [FieldSettings("From quantity", DefaultField = true, FieldTypeId = Connector.FieldTypeIdQuantity)]
        public int QTYLIMITMIN { get; set; }

        [FieldSettings("To quantity", DefaultField = true, FieldTypeId = Connector.FieldTypeIdQuantity)]
        public int QTYLIMITMAX { get; set; }

        [FieldSettings("Price", FieldTypeId = Connector.FieldTypeIdCurrency, DefaultField = true)]
        public decimal? Price
        {
            get
            {
                if (BasePriceListItem == null || PRICELISTID == BasePriceListItem.PRICELISTID) return VALUETYPE == "Actual" ? VALUE : 0;
                switch (VALUETYPE)
                {
                    case "Actual":
                        return VALUE;
                    case "% Discount":
                        return BasePriceListItem.VALUE - BasePriceListItem.VALUE * VALUE / 100;
                    case "% Markup":
                        return BasePriceListItem.VALUE + BasePriceListItem.VALUE * VALUE / 100;
                    case "Dollar Discount":
                        return BasePriceListItem.VALUE - VALUE;
                    case "Dollar Markup":
                        return BasePriceListItem.VALUE + VALUE;
                    default:
                        return 0;
                }
            }
        }

        #endregion

        #region Properties

        [FieldSettings("Currency")]
        public string CURRENCY { get; set; }

        [FieldSettings("Employee name")]
        public string EMPLOYEENAME { get; set; }

        [FieldSettings("Item name")]
        public string ITEMNAME { get; set; }

        [FieldSettings("Quantity or value")]
        public IntacctPriceListQtyOrValue QTY_OR_VALUE { get; set; }

        [FieldSettings("Value type")]
        public string VALUETYPE { get; set; }

        [FieldSettings("Value", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? VALUE { get; set; }

        #endregion

        #region Hidden properties

        public string FIXED { get; set; }
        public IntacctPriceList PriceList { get; set; }
        public IntacctPriceListDetail BasePriceListItem { get; set; }
        public IntacctItem Item { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Fixed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsFixed => ToBoolean(FIXED);

        [FieldSettings("Application")]
        public string PriceList_SALEPURCHASE => PriceList.SALEPURCHASE;

        [FieldSettings("Item type")]
        public string Item_ITEMTYPE => Item.ITEMTYPE;

        [FieldSettings("Item product line")]
        public string Item_PRODUCTLINEID => Item.PRODUCTLINEID;

        [FieldSettings("Base price", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? BasePrice
        {
            get
            {
                if (BasePriceListItem == null || BasePriceListItem.ITEMID == ITEMID) return VALUE;
                return BasePriceListItem.VALUE;
            }
        }

        #endregion

    }
}