using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroContact : DataConnectorEntityModel
    {

        #region Enums

        public enum XeroContactStatus
        {
            [Description("Active")]
            ACTIVE,
            [Description("Archived")]
            ARCHIVED
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Contact person")]
        public string Person => $"{FirstName.Trim()} {LastName.Trim()}";

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string EmailAddress { get; set; }

        [FieldSettings("Supplier", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsSupplier { get; set; }

        [FieldSettings("Customer", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsCustomer { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroContactStatus))]
        public XeroContactStatus ContactStatus { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Contact ID")]
        public string ContactID { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("Skype", FieldTypeId = DataConnector.FieldTypeIdSkype)]
        public string SkypeUserName { get; set; }

        [FieldSettings("Bank account details")]
        public string BankAccountDetails { get; set; }

        [FieldSettings("Tax number")]
        public string TaxNumber { get; set; }

        [FieldSettings("Default currency")]
        public string DefaultCurrency { get; set; }

        [FieldSettings("Accounts receivable tax type")]
        public string AccountsReceivableTaxType { get; set; }

        [FieldSettings("Accounts payable tax type")]
        public string AccountsPayableTaxType { get; set; }

        #endregion

        #region Hidden properties

        public List<XeroAddress> Addresses { get; set; }
        public List<XeroPhone> Phones { get; set; }
        public List<XeroContactPerson> ContactPersons { get; set; }
        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Default phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string DefaultPhoneNumber
        {
            get
            {
                if (Phones == null) return string.Empty;
                foreach (var phone in Phones.Where(phone => phone.PhoneType == XeroPhone.XeroPhoneType.DEFAULT)) return phone.PhoneNumber;
                return string.Empty;
            }
        }

        [FieldSettings("Mobile phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string MobilePhoneNumber
        {
            get
            {
                if (Phones == null) return string.Empty;
                foreach (var phone in Phones.Where(phone => phone.PhoneType == XeroPhone.XeroPhoneType.MOBILE)) return phone.PhoneNumber;
                return string.Empty;
            }
        }

        [FieldSettings("Fax number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string FaxNumber
        {
            get
            {
                if (Phones == null) return string.Empty;
                foreach (var phone in Phones.Where(phone => phone.PhoneType == XeroPhone.XeroPhoneType.FAX)) return phone.PhoneNumber;
                return string.Empty;
            }
        }

        [FieldSettings("DDI phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string DdiPhoneNumber
        {
            get
            {
                if (Phones == null) return string.Empty;
                foreach (var phone in Phones.Where(phone => phone.PhoneType == XeroPhone.XeroPhoneType.DDI)) return phone.PhoneNumber;
                return string.Empty;
            }
        }

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime UpdatedDate => UpdatedDateUTC.Date;

        [FieldSettings("Updated time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime UpdatedTime => Time(UpdatedDateUTC);

        [FieldSettings("Street address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string StreetAddress
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.Address;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Line 1")]
        public string StreetAddress1
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.AddressLine1;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Line 2")]
        public string StreetAddress2
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.AddressLine2;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Line 3")]
        public string StreetAddress3
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.AddressLine3;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Line 4")]
        public string StreetAddress4
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.AddressLine4;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - City")]
        public string StreetCity
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.City;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Postal code")]
        public string StreetPostalCode
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.PostalCode;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Attention to")]
        public string StreetAttentionTo
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.AttentionTo;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Country")]
        public string StreetCountry
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.Country;
                return string.Empty;
            }
        }

        [FieldSettings("Street address - Region")]
        public string StreetRegion
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.STREET)) return address.Region;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string PoBoxAddress
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.Address;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Line 1")]
        public string PoBoxAddress1
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.AddressLine1;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Line 2")]
        public string PoBoxAddress2
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.AddressLine2;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Line 3")]
        public string PoBoxAddress3
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.AddressLine3;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Line 4")]
        public string PoBoxAddress4
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.AddressLine4;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - City")]
        public string PoBoxCity
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.City;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Postal code")]
        public string PoBoxPostalCode
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.PostalCode;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Attention to")]
        public string PoBoxAttentionTo
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.AttentionTo;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Country")]
        public string PoBoxCountry
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.Country;
                return string.Empty;
            }
        }

        [FieldSettings("PO Box address - Region")]
        public string PoBoxRegion
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.POBOX)) return address.Region;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string DeliveryAddress
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.Address;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Line 1")]
        public string DeliveryAddress1
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.AddressLine1;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Line 2")]
        public string DeliveryAddress2
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.AddressLine2;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Line 3")]
        public string DeliveryAddress3
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.AddressLine3;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Line 4")]
        public string DeliveryAddress4
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.AddressLine4;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - City")]
        public string DeliveryCity
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.City;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Postal code")]
        public string DeliveryPostalCode
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.PostalCode;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Attention to")]
        public string DeliveryAttentionTo
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.AttentionTo;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Country")]
        public string DeliveryCountry
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.Country;
                return string.Empty;
            }
        }

        [FieldSettings("Delivery address - Region")]
        public string DeliveryRegion
        {
            get
            {
                if (Addresses == null) return string.Empty;
                foreach (var address in Addresses.Where(address => address.AddressType == XeroAddress.XeroAddressType.DELIVERY)) return address.Region;
                return string.Empty;
            }
        }

        #endregion

    }
}