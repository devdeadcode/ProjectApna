using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctInventoryDocument : IntacctBase
    {

        #region Default properties

        [FieldSettings("Document type", DefaultField = true)]
        public string DOCPARID { get; set; }

        [FieldSettings("Document number", DefaultField = true)]
        public string DOCNO { get; set; }

        [FieldSettings("Reference number", DefaultField = true)]
        public string PONUMBER { get; set; }

        [FieldSettings("State", DefaultField = true)]
        public string STATE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Message")]
        public string MESSAGE { get; set; }

        [FieldSettings("Item totals", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? TOTAL { get; set; }

        [FieldSettings("Subtotals", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? SUBTOTAL { get; set; }

        [FieldSettings("Transaction subtotals", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? TRX_SUBTOTAL { get; set; }

        [FieldSettings("Transaction totals", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? TRX_TOTAL { get; set; }

        #endregion

        #region Hidden properties

        public string PRINTED { get; set; }
        public string VENDORDOCNO { get; set; }
        public string NOTE { get; set; }
        public string CURRENCY { get; set; }
        public string BASECURR { get; set; }
        public DateTime? EXCHRATEDATE { get; set; }
        public decimal? EXCHRATE { get; set; }
        public DateTime? WHENDUE { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Printed")]
        public bool IsPrinted => ToBoolean(PRINTED);

        #endregion

    }
}