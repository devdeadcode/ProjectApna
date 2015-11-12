using System;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftExpense : ConnectorEntityModel
    {

        public int? ContactId { get; set; }
        public DateTime? DateIncurred { get; set; }
        public decimal? ExpenseAmt { get; set; }
        public string ExpenseType { get; set; } // todo - convert to enum
        public int? Id { get; set; }
        public int? TypeId { get; set; }

    }
}
