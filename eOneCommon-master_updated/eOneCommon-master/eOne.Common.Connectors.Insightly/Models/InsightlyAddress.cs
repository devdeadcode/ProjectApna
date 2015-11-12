namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyAddress : ConnectorEntityModel
    {

        public int? ADDRESS_ID {get; set; }
        public string ADDRESS_TYPE {get; set; }
        public string STREET {get; set; }
        public string CITY {get; set; }
        public string STATE {get; set; }
        public string POSTCODE {get; set; }
        public string COUNTRY { get; set; }

        public string Address => BuildAddress(STREET, CITY, STATE, POSTCODE);

    }
}
