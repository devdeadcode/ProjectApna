using System;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyContactAddress : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Contact name", DefaultField = true)]
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

        #endregion

        #region Hidden properties

        public InsightlyContact Contact { get; set; }
        public InsightlyAddress Address { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string AddressAddress => Address.Address;

        [FieldSettings("Address ID", KeyNumber = 2, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? AddressId => Address.ADDRESS_ID;

        [FieldSettings("Salutation")]
        public string ContactSalutation => Contact.SALUTATION;

        [FieldSettings("First name", DefaultField = true)]
        public string ContactFirstName => Contact.FIRST_NAME;

        [FieldSettings("Last name", DefaultField = true)]
        public string ContactLastName => Contact.LAST_NAME;

        [FieldSettings("Contact ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int ContactId => Contact.CONTACT_ID;

        [FieldSettings("Background")]
        public string ContactBackground => Contact.BACKGROUND;

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage)]
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

        [FieldSettings("Contact date created", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ContactDateCreated => Contact.DATE_CREATED_UTC;

        [FieldSettings("Contact date updated", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ContactDateUpdated => Contact.DATE_UPDATED_UTC;

        [FieldSettings("Home phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string ContactHomePhone => Contact.HomePhone;

        [FieldSettings("Work phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string ContactWorkPhone => Contact.WorkPhone;

        [FieldSettings("Mobile phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string ContactMobilePhone => Contact.MobilePhone;

        [FieldSettings("Fax")]
        public string ContactFax => Contact.Fax;

        [FieldSettings("Pager")]
        public string ContactPager => Contact.Pager;

        [FieldSettings("Y! Messenger")]
        public string ContactYahooMessenger => Contact.YahooMessenger;

        [FieldSettings("SIP")]
        public string ContactSip => Contact.SIP;

        [FieldSettings("Other phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string ContactOtherPhone => Contact.OtherPhone;

        [FieldSettings("Work email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string ContactWorkEmail => Contact.WorkEmail;

        [FieldSettings("Home email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string ContactHomeEmail => Contact.HomeEmail;

        [FieldSettings("Personal email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string ContactPersonalEmail => Contact.PersonalEmail;

        [FieldSettings("Other email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string ContactOtherEmail => Contact.OtherEmail;

        [FieldSettings("LinkedIn", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ContactLinkedIn => Contact.LinkedIn;

        [FieldSettings("Twitter", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ContactTwitter => Contact.Twitter;

        [FieldSettings("Home website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ContactHomeWebsite => Contact.HomeWebsite;

        [FieldSettings("Work website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ContactWorkWebsite => Contact.WorkWebsite;

        [FieldSettings("Blog", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ContactBlog => Contact.Blog;

        [FieldSettings("Personal website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ContactPersonalWebsite => Contact.PersonalWebsite;

        [FieldSettings("Other website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ContactOtherWebsite => Contact.OtherWebsite;

        #endregion

    }
}
