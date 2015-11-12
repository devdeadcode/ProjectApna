using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyOrganisation : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Organization name", DefaultField = true, Description = true)]
        public string ORGANISATION_NAME { get; set; }

        [FieldSettings("Tags", DefaultField = true)]
        public string TagList => CommaSeparatedValues(TAGS.Select(tag => tag.TAG_NAME).ToList());

        #endregion

        #region Properties

        [FieldSettings("Organization ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int ORGANISATION_ID { get; set; }

        [FieldSettings("Background")]
        public string BACKGROUND { get; set; }

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage)]
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

        [FieldSettings("Date created", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_CREATED_UTC { get; set; }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_UPDATED_UTC { get; set; }

        [FieldSettings("Owner user ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? OWNER_USER_ID { get; set; }

        #endregion

        #region Hidden properties

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
        public InsightlyUser Owner { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Owner name")]
        public string OwnerName => Owner == null ? string.Empty : Owner.Name;

        [FieldSettings("Owner email")]
        public string OwnerEmail => Owner == null ? string.Empty : Owner.EMAIL_ADDRESS;

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
        public string HomeEmail => GetContactInfoDetail("EMAIL", "CORPORATE");

        [FieldSettings("Personal email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string PersonalEmail => GetContactInfoDetail("EMAIL", "PERSONAL");

        [FieldSettings("Other email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string OtherEmail => GetContactInfoDetail("EMAIL", "OTHER");

        [FieldSettings("LinkedIn", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string LinkedIn => GetContactInfoDetail("SOCIAL", "LinkedInPublicProfileUrl");

        [FieldSettings("Twitter", FieldTypeId = Connector.FieldTypeIdTwitter)]
        public string Twitter => GetContactInfoDetail("SOCIAL", "TwitterID");

        [FieldSettings("Corporate website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string CorporateWebsite => GetContactInfoDetail("WEBSITE", "CORPORATE");

        [FieldSettings("Work website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string WorkWebsite => GetContactInfoDetail("WEBSITE", "WORK");

        [FieldSettings("Blog", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string Blog => GetContactInfoDetail("WEBSITE", "BLOG");

        [FieldSettings("Personal website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string PersonalWebsite => GetContactInfoDetail("WEBSITE", "PERSONAL");

        [FieldSettings("Other website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string OtherWebsite => GetContactInfoDetail("WEBSITE", "OTHER");

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
