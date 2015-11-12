using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctItem : IntacctBase
    {

        #region Default properties

        [FieldSettings("Item number", DefaultField = true, KeyNumber = 1)]
        public string ITEMID { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string NAME { get; set; }

        [FieldSettings("Product line", DefaultField = true)]
        public string PRODUCTLINEID { get; set; }

        [FieldSettings("Item type", DefaultField = true)]
        public string ITEMTYPE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Extended description")]
        public string EXTENDED_DESCRIPTION { get; set; }

        [FieldSettings("Cost method")]
        public string COST_METHOD { get; set; }

        [FieldSettings("Note")]
        public string NOTE { get; set; }

        [FieldSettings("UPC")]
        public string UPC { get; set; }

        [FieldSettings("GL group")]
        public string GLGROUP { get; set; }

        [FieldSettings("Substitute item")]
        public string SUBSTITUTEID { get; set; }

        [FieldSettings("Default warehouse")]
        public string DEFAULT_WAREHOUSE { get; set; }

        [FieldSettings("Standard cost", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? STANDARD_COST { get; set; }

        [FieldSettings("Average cost", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? AVERAGE_COST { get; set; }

        [FieldSettings("Shipping weight", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? SHIP_WEIGHT { get; set; }

        [FieldSettings("Has start and end dates", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool? HASSTARTENDDATES { get; set; }

        [FieldSettings("Serial tracked", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool? ENABLE_SERIALNO { get; set; }

        [FieldSettings("Lot tracked", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool? ENABLE_LOT_CATEGORY { get; set; }

        [FieldSettings("Bin tracked", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool? ENABLE_BINS { get; set; }

        [FieldSettings("Expiration tracked", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool? ENABLE_EXPIRATION { get; set; }

        [FieldSettings("Taxable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool? TAXABLE { get; set; }

        [FieldSettings("Date last sold", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENLASTSOLD { get; set; }

        [FieldSettings("Date last received", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENLASTRECEIVED { get; set; }

        [FieldSettings("Number of periods", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? TOTALPERIODS { get; set; }

        [FieldSettings("Default renewal macro")]
        public string RENEWALMACROID { get; set; }

        [FieldSettings("Fair value category")]
        public string VSOECATEGORY { get; set; }

        [FieldSettings("Default delivery status")]
        public string VSOEDLVRSTATUS { get; set; }

        [FieldSettings("Default deferral status")]
        public string VSOEREVDEFSTATUS { get; set; }

        [FieldSettings("Unit of measure")]
        public string UOMGRP { get; set; }

        [FieldSettings("Periods meeasured in")]
        public string TERMPERIOD { get; set; }

        [FieldSettings("Inventory unit precision")]
        public int? INV_PRECISION { get; set; }

        [FieldSettings("Purchasing unit precision")]
        public int? PO_PRECISION { get; set; }

        [FieldSettings("Sales unit precision")]
        public int? SO_PRECISION { get; set; }

        #endregion

    }
}