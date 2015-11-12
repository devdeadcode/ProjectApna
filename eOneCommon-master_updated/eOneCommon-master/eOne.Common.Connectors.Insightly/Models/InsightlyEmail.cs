using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyEmail : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("To", DefaultField = true)]
        public string EMAIL_TO { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string SUBJECT { get; set; }

        [FieldSettings("Body", DefaultField = true)]
        public string BODY { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Email ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int EMAIL_ID { get; set; }

        [FieldSettings("Gmail message ID")]
        public string GMAIL_MESSAGE_ID { get; set; }

        [FieldSettings("From")]
        public string EMAIL_FROM { get; set; }

        [FieldSettings("CC")]
        public string EMAIL_CC { get; set; }

        [FieldSettings("Format")]
        public string FORMAT { get; set; }

        [FieldSettings("Size", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? SIZE { get; set; }

        [FieldSettings("Email date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EMAIL_DATE_UTC { get; set; }

        [FieldSettings("Date created", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_CREATED_UTC { get; set; }

        #endregion

        #region Hidden properties

        public int? OWNER_USER_ID { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }
        public List<InsightlyFileAttachment> FILE_ATTACHMENTS { get; set; }
        public List<InsightlyEmailLink> EMAILLINKS { get; set; }
        public InsightlyContact Contact { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Has attachments", FieldTypeId = Connector.FieldTypeIdDate)]
        public bool HasAttachments => FILE_ATTACHMENTS.Count > 0;

        [FieldSettings("Contact ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int ContactId => (from link in EMAILLINKS where link.CONTACT_ID != null select (int) link.CONTACT_ID).FirstOrDefault();

        #endregion

    }
}
