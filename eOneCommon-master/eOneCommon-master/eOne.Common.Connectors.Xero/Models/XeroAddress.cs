using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroAddress : DataConnectorEntityModel
    {

        public enum XeroAddressType
        {
            POBOX,
            STREET,
            DELIVERY
        }

        public XeroAddressType AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string AttentionTo { get; set; }

        public string Address => BuildAddress(AddressLine1, AddressLine2, AddressLine3, AddressLine4, City, Region, PostalCode);
        
    }
}