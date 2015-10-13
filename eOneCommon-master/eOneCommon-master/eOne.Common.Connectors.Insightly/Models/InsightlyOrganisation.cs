using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyOrganisation : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name")]
        public string ORGANISATION_NAME { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Organization Id", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int ORGANISATION_ID { get; set; }

        [FieldSettings("Background")]
        public string BACKGROUND { get; set; }

        [FieldSettings("Image", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string IMAGE_URL { get; set; }

        [FieldSettings("Organization field 1")]
        public string ORGANISATION_FIELD_1 { get; set; }

        [FieldSettings("Organization field 2")]
        public string ORGANISATION_FIELD_2 { get; set; }

        [FieldSettings("Organization field 3")]
        public string ORGANISATION_FIELD_3 { get; set; }

        [FieldSettings("Organization field 4")]
        public string ORGANISATION_FIELD_4 { get; set; }

        [FieldSettings("Organization field 5")]
        public string ORGANISATION_FIELD_5 { get; set; }

        [FieldSettings("Organization field 6")]
        public string ORGANISATION_FIELD_6 { get; set; }

        [FieldSettings("Organization field 7")]
        public string ORGANISATION_FIELD_7 { get; set; }

        [FieldSettings("Organization field 8")]
        public string ORGANISATION_FIELD_8 { get; set; }

        [FieldSettings("Organization field 9")]
        public string ORGANISATION_FIELD_9 { get; set; }

        [FieldSettings("Organization field 10")]
        public string ORGANISATION_FIELD_10 { get; set; }

        #endregion

        #region Hidden properties

        public int? OWNER_USER_ID { get; set; }
        public DateTime? DATE_CREATED_UTC { get; set; }
        public DateTime? DATE_UPDATED_UTC { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }
        public List<InsightlyAddress> ADDRESSES { get; set; }
        public List<InsightlyContactInfo> CONTACTINFOS { get; set; }
        public List<InsightlyDate> DATES { get; set; }
        public List<InsightlyTag> TAGS { get; set; }
        public List<InsightlyLink> LINKS { get; set; }
        public List<InsightlyEmailLink> EMAILLINKS { get; set; }
        public List<InsightlyOrganisationLink> ORGANISATIONLINKS { get; set; }

        #endregion

        #region Private dummy properties

        private string TagListDummy { get; set; }
        private string HomePhoneDummy { get; set; }
        private string WorkPhoneDummy { get; set; }
        private string MobilePhoneDummy { get; set; }
        private string FaxDummy { get; set; }
        private string PagerDummy { get; set; }
        private string YahooMessengerDummy { get; set; }
        private string SIPDummy { get; set; }
        private string OtherPhoneDummy { get; set; }
        private string WorkEmailDummy { get; set; }
        private string HomeEmailDummy { get; set; }
        private string PersonalEmailDummy { get; set; }
        private string OtherEmailDummy { get; set; }
        private string LinkedInDummy { get; set; }
        private string TwitterDummy { get; set; }
        private string CorporateWebsiteDummy { get; set; }
        private string WorkWebsiteDummy { get; set; }
        private string BlogDummy { get; set; }
        private string PersonalWebsiteDummy { get; set; }
        private string OtherWebsiteDummy { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Tags")]
        public string TagList
        {
            get
            {
                return CommaSeparatedValues(TAGS.Select(tag => tag.TAG_NAME).ToList());
            }
            set
            {
                TagListDummy = value;
            }
        }

        [FieldSettings("Home phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string HomePhone
        {
            get
            {
                return GetContactInfoDetail("PHONE", "HOME");
            }
            set
            {
                HomePhoneDummy = value;
            }
        }

        [FieldSettings("Work phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string WorkPhone
        {
            get
            {
                return GetContactInfoDetail("PHONE", "WORK");
            }
            set
            {
                WorkPhoneDummy = value;
            }
        }

        [FieldSettings("Mobile phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string MobilePhone
        {
            get
            {
                return GetContactInfoDetail("PHONE", "MOBILE");
            }
            set
            {
                MobilePhoneDummy = value;
            }
        }

        [FieldSettings("Fax")]
        public string Fax
        {
            get
            {
                return GetContactInfoDetail("PHONE", "FAX");
            }
            set
            {
                FaxDummy = value;
            }
        }

        [FieldSettings("Pager")]
        public string Pager
        {
            get
            {
                return GetContactInfoDetail("PHONE", "PAGER");
            }
            set
            {
                PagerDummy = value;
            }
        }

        [FieldSettings("Y! Messenger")]
        public string YahooMessenger
        {
            get
            {
                return GetContactInfoDetail("PHONE", "YAHOO");
            }
            set
            {
                YahooMessengerDummy = value;
            }
        }

        [FieldSettings("SIP")]
        public string SIP
        {
            get
            {
                return GetContactInfoDetail("PHONE", "SIP");
            }
            set
            {
                SIPDummy = value;
            }
        }

        [FieldSettings("Other phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string OtherPhone
        {
            get
            {
                return GetContactInfoDetail("PHONE", "OTHER");
            }
            set
            {
                OtherPhoneDummy = value;
            }
        }

        [FieldSettings("Work email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string WorkEmail
        {
            get
            {
                return GetContactInfoDetail("EMAIL", "WORK");
            }
            set
            {
                WorkEmailDummy = value;
            }
        }

        [FieldSettings("Home email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string HomeEmail
        {
            get
            {
                return GetContactInfoDetail("EMAIL", "CORPORATE");
            }
            set
            {
                HomeEmailDummy = value;
            }
        }

        [FieldSettings("Personal email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string PersonalEmail
        {
            get
            {
                return GetContactInfoDetail("EMAIL", "PERSONAL");
            }
            set
            {
                PersonalEmailDummy = value;
            }
        }

        [FieldSettings("Other email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string OtherEmail
        {
            get
            {
                return GetContactInfoDetail("EMAIL", "OTHER");
            }
            set
            {
                OtherEmailDummy = value;
            }
        }

        [FieldSettings("LinkedIn", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string LinkedIn
        {
            get
            {
                return GetContactInfoDetail("SOCIAL", "LinkedInPublicProfileUrl");
            }
            set
            {
                LinkedInDummy = value;
            }
        }

        [FieldSettings("Twitter", FieldTypeId = DataConnector.FieldTypeIdTwitter)]
        public string Twitter
        {
            get
            {
                return GetContactInfoDetail("SOCIAL", "TwitterID");
            }
            set
            {
                TwitterDummy = value;
            }
        }

        [FieldSettings("Corporate website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string CorporateWebsite
        {
            get
            {
                return GetContactInfoDetail("WEBSITE", "CORPORATE");
            }
            set
            {
                CorporateWebsiteDummy = value;
            }
        }

        [FieldSettings("Work website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string WorkWebsite
        {
            get
            {
                return GetContactInfoDetail("WEBSITE", "WORK");
            }
            set
            {
                WorkWebsiteDummy = value;
            }
        }

        [FieldSettings("Blog", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string Blog
        {
            get
            {
                return GetContactInfoDetail("WEBSITE", "BLOG");
            }
            set
            {
                BlogDummy = value;
            }
        }

        [FieldSettings("Personal website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string PersonalWebsite
        {
            get
            {
                return GetContactInfoDetail("WEBSITE", "PERSONAL");
            }
            set
            {
                PersonalWebsiteDummy = value;
            }
        }

        [FieldSettings("Other website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string OtherWebsite
        {
            get
            {
                return GetContactInfoDetail("WEBSITE", "OTHER");
            }
            set
            {
                OtherWebsiteDummy = value;
            }
        }

        [FieldSettings("Date created", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime DateCreated
        {
            get
            {
                return DATE_CREATED_UTC ?? DateTime.MinValue;
            }
            set
            {
                DATE_CREATED_UTC = value;
            }
        }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime DateUpdated
        {
            get
            {
                return DATE_UPDATED_UTC ?? DateTime.MinValue;
            }
            set
            {
                DATE_UPDATED_UTC = value;
            }
        }

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
