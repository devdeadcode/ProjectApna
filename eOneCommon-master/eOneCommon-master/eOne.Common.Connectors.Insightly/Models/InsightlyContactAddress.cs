using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyContactAddress : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Contact Name", DefaultField = true)]
        public string ContactName => $"{Contact.FIRST_NAME.Trim()} {Contact.LAST_NAME.Trim()}";

        [FieldSettings("Address type", DefaultField = true)]
        public string AddressType => Address.ADDRESS_TYPE;

        [FieldSettings("Street", DefaultField = true)]
        public string AddressStreet => Address.STREET;

        [FieldSettings("City", DefaultField = true)]
        public string AddressCity => Address.CITY;

        [FieldSettings("State", DefaultField = true)]
        public string AddressState => Address.STATE;

        [FieldSettings("ZIP", DefaultField = true)]
        public string AddressZip => Address.POSTCODE;

        [FieldSettings("Country", DefaultField = true)]
        public string AddressCountry => Address.COUNTRY;

        //[FieldSettings("test", DefaultField = true)]
        //public string test { get; set; }

        #endregion

        #region Hidden properties

        public InsightlyContact Contact { get; set; }
        public InsightlyAddress Address { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string AddressAdsress => Address.Address;

        [FieldSettings("Salutation")]
        public string ContactSalutation => Contact.SALUTATION;

        [FieldSettings("First name", DefaultField = true)]
        public string ContactFirstName => Contact.FIRST_NAME;

        [FieldSettings("Last name", DefaultField = true)]
        public string ContactLastName => Contact.LAST_NAME;

        [FieldSettings("Contact Id")]
        public int ContactId => Contact.CONTACT_ID;

        [FieldSettings("Background")]
        public string ContactBackground => Contact.BACKGROUND;

        [FieldSettings("Image", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string ContactImage => Contact.IMAGE_URL;

        [FieldSettings("Contact field 1")]
        public string ContactField1 => Contact.CONTACT_FIELD_1;

        [FieldSettings("Contact field 2")]
        public string ContactField2 => Contact.CONTACT_FIELD_2;

        [FieldSettings("Contact field 3")]
        public string ContactField3 => Contact.CONTACT_FIELD_3;

        [FieldSettings("Contact field 4")]
        public string ContactField4 => Contact.CONTACT_FIELD_4;

        [FieldSettings("Contact field 5")]
        public string ContactField5 => Contact.CONTACT_FIELD_5;

        [FieldSettings("Contact field 6")]
        public string ContactField6 => Contact.CONTACT_FIELD_6;

        [FieldSettings("Contact field 7")]
        public string ContactField7 => Contact.CONTACT_FIELD_7;

        [FieldSettings("Contact field 8")]
        public string ContactField8 => Contact.CONTACT_FIELD_8;

        [FieldSettings("Contact field 9")]
        public string ContactField9 => Contact.CONTACT_FIELD_9;

        [FieldSettings("Contact field 10")]
        public string ContactField10 => Contact.CONTACT_FIELD_10;

        [FieldSettings("Contact tags")]
        public string ContactTagList => Contact.TagList;

        [FieldSettings("Contact date created", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime ContactDateCreated => Contact.DateCreated;

        [FieldSettings("Contact date updated", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime ContactDateUpdated => Contact.DateUpdated;

        [FieldSettings("Home phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string ContactHomePhone => Contact.HomePhone;

        [FieldSettings("Work phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string ContactWorkPhone => Contact.WorkPhone;

        [FieldSettings("Mobile phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string ContactMobilePhone => Contact.MobilePhone;

        [FieldSettings("Fax")]
        public string ContactFax => Contact.Fax;

        [FieldSettings("Pager")]
        public string ContactPager => Contact.Pager;

        [FieldSettings("Y! Messenger")]
        public string ContactYahooMessenger => Contact.YahooMessenger;

        [FieldSettings("SIP")]
        public string ContactSip => Contact.SIP;

        [FieldSettings("Other phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string ContactOtherPhone => Contact.OtherPhone;

        [FieldSettings("Work email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string ContactWorkEmail => Contact.WorkEmail;

        [FieldSettings("Home email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string ContactHomeEmail => Contact.HomeEmail;

        [FieldSettings("Personal email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string ContactPersonalEmail => Contact.PersonalEmail;

        [FieldSettings("Other email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string ContactOtherEmail => Contact.OtherEmail;

        [FieldSettings("LinkedIn", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ContactLinkedIn => Contact.LinkedIn;

        [FieldSettings("Twitter", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ContactTwitter => Contact.Twitter;

        [FieldSettings("Home website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ContactHomeWebsite => Contact.HomeWebsite;

        [FieldSettings("Work website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ContactWorkWebsite => Contact.WorkWebsite;

        [FieldSettings("Blog", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ContactBlog => Contact.Blog;

        [FieldSettings("Personal website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ContactPersonalWebsite => Contact.PersonalWebsite;

        [FieldSettings("Other website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ContactOtherWebsite => Contact.OtherWebsite;

        #endregion

    }
}
