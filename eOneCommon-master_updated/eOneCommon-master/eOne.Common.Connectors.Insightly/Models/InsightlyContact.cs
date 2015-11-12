using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyContact : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Contact name", DefaultField = true, Description = true)]
        public string ContactName => CombineFirstLastName(FIRST_NAME, LAST_NAME);

        [FieldSettings("Organization name", DefaultField = true)]
        public string OrganisationName => Organisation == null ? string.Empty : Organisation.ORGANISATION_NAME;

        [FieldSettings("Tags", DefaultField = true)]
        public string TagList => CommaSeparatedValues(TAGS.Select(tag => tag.TAG_NAME).ToList());

        #endregion

        #region Properties

        [FieldSettings("Salutation")]
        public string SALUTATION { get; set; }

        [FieldSettings("First name")]
        public string FIRST_NAME { get; set; }

        [FieldSettings("Last name")]
        public string LAST_NAME { get; set; }

        [FieldSettings("Contact ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int CONTACT_ID { get; set; }

        [FieldSettings("Background")]
        public string BACKGROUND { get; set; }

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string IMAGE_URL { get; set; }

        [FieldSettings("Contact field 1")]
        public string CONTACT_FIELD_1 { get; set; }

        [FieldSettings("Contact field 2")]
        public string CONTACT_FIELD_2 { get; set; }

        [FieldSettings("Contact field 3")]
        public string CONTACT_FIELD_3 { get; set; }

        [FieldSettings("Contact field 4")]
        public string CONTACT_FIELD_4 { get; set; }

        [FieldSettings("Contact field 5")]
        public string CONTACT_FIELD_5 { get; set; }

        [FieldSettings("Contact field 6")]
        public string CONTACT_FIELD_6 { get; set; }

        [FieldSettings("Contact field 7")]
        public string CONTACT_FIELD_7 { get; set; }

        [FieldSettings("Contact field 8")]
        public string CONTACT_FIELD_8 { get; set; }

        [FieldSettings("Contact field 9")]
        public string CONTACT_FIELD_9 { get; set; }

        [FieldSettings("Contact field 10")]
        public string CONTACT_FIELD_10 { get; set; }

        [FieldSettings("Date created", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_CREATED_UTC { get; set; }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_UPDATED_UTC { get; set; }

        [FieldSettings("Organization ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? DEFAULT_LINKED_ORGANISATION { get; set; }

        #endregion

        #region Hidden properties

        public List<InsightlyAddress> ADDRESSES { get; set; }
        public List<InsightlyContactInfo> CONTACTINFOS { get; set; }
        public List<InsightlyDate> DATES { get; set; }
        public List<InsightlyTag> TAGS { get; set; }
        public List<InsightlyLink> LINKS { get; set; }
        public List<InsightlyContactLink> CONTACTLINKS { get; set; }
        public List<InsightlyEmailLink> EMAILLINKS { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }
        public InsightlyOrganisation Organisation { get; set; }

        #endregion

        #region Calculations
        
        #region Contact Info

        [FieldSettings("Home phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string HomePhone => GetContactInfoDetail("PHONE", "HOME");

        [FieldSettings("Work phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string WorkPhone => GetContactInfoDetail("PHONE", "WORK");

        [FieldSettings("Mobile phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string MobilePhone => GetContactInfoDetail("PHONE", "MOBILE");

        [FieldSettings("Fax")]
        public string Fax => GetContactInfoDetail("PHONE", "FAX");

        [FieldSettings("Pager")]
        public string Pager => GetContactInfoDetail("PHONE", "PAGER");

        [FieldSettings("Y! Messenger")]
        public string YahooMessenger => GetContactInfoDetail("PHONE", "YAHOO");

        [FieldSettings("SIP")]
        public string SIP => GetContactInfoDetail("PHONE", "SIP");

        [FieldSettings("Other phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string OtherPhone => GetContactInfoDetail("PHONE", "OTHER");

        [FieldSettings("Work email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string WorkEmail => GetContactInfoDetail("EMAIL", "WORK");

        [FieldSettings("Home email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string HomeEmail => GetContactInfoDetail("EMAIL", "HOME");

        [FieldSettings("Personal email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string PersonalEmail => GetContactInfoDetail("EMAIL", "PERSONAL");

        [FieldSettings("Other email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string OtherEmail => GetContactInfoDetail("EMAIL", "OTHER");

        [FieldSettings("LinkedIn", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string LinkedIn => GetContactInfoDetail("SOCIAL", "LinkedInPublicProfileUrl");

        [FieldSettings("Twitter", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string Twitter => GetContactInfoDetail("SOCIAL", "TwitterID");

        [FieldSettings("Home website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string HomeWebsite => GetContactInfoDetail("WEBSITE", "HOME");

        [FieldSettings("Work website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string WorkWebsite => GetContactInfoDetail("WEBSITE", "WORK");

        [FieldSettings("Blog", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string Blog => GetContactInfoDetail("WEBSITE", "BLOG");

        [FieldSettings("Personal website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string PersonalWebsite => GetContactInfoDetail("WEBSITE", "PERSONAL");

        [FieldSettings("Other website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string OtherWebsite => GetContactInfoDetail("WEBSITE", "OTHER");

        #endregion

        

        #endregion

        #region Helpers

        private string GetContactInfoDetail(string type, string label)
        {
            foreach (var contactInfo in CONTACTINFOS.Where(contactInfo => contactInfo.TYPE == type && contactInfo.LABEL == label)) return contactInfo.DETAIL;
            return string.Empty;
        }

        #endregion

    }
}
