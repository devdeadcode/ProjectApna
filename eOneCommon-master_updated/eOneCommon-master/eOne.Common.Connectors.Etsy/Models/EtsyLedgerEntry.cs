namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyLedgerEntry : ConnectorEntityModel
    {

        public int ledger_entry_id { get; set; }
        public int ledger_id { get; set; }
        public int sequence { get; set; }
        public int credit_amount { get; set; }
        public int debit_amount { get; set; }
        public string entry_type { get; set; }
        public int reference_id { get; set; }
        public int running_balance { get; set; }
        public int create_date { get; set; }

    }
}