using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyEmail : DataConnectorEntityModel
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

        [FieldSettings("Email Id", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int EMAIL_ID { get; set; }

        [FieldSettings("GMail message Id")]
        public string GMAIL_MESSAGE_ID { get; set; }

        [FieldSettings("From")]
        public string EMAIL_FROM { get; set; }

        [FieldSettings("CC")]
        public string EMAIL_CC { get; set; }

        [FieldSettings("Format")]
        public string FORMAT { get; set; }

        [FieldSettings("Size", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? SIZE { get; set; }

        #endregion

        #region Hidden properties

        public DateTime? EMAIL_DATE_UTC { get; set; }
        public DateTime? DATE_CREATED_UTC { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }
        public List<InsightlyFileAttachment> FILE_ATTACHMENTS { get; set; }
        public List<InsightlyEmailLink> EMAILLINKS { get; set; }

        #endregion

        #region Calculations

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

        [FieldSettings("Email date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime EmailDate
        {
            get
            {
                return EMAIL_DATE_UTC ?? DateTime.MinValue;
            }
            set
            {
                EMAIL_DATE_UTC = value;
            }
        }

        [FieldSettings("Has attachments", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public bool HasAttachments => FILE_ATTACHMENTS.Count > 0;

        #endregion

    }
}
