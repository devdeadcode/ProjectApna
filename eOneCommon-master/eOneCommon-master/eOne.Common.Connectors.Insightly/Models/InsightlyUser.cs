using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyUser : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("First name", DefaultField = true)]
        public string FIRST_NAME { get; set; }

        [FieldSettings("Last name", DefaultField = true)]
        public string LAST_NAME { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string EMAIL_ADDRESS { get; set; }

        [FieldSettings("Administrator", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool Administrator => (ADMINISTRATOR != null) && (bool)ADMINISTRATOR;

        [FieldSettings("Account owner", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool AccountOwner => (ACCOUNT_OWNER != null) && (bool)ACCOUNT_OWNER;

        [FieldSettings("Active", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool Active => (ACTIVE != null) && (bool)ACTIVE;

        #endregion

        #region Properties

        [FieldSettings("Timezone ID")]
        public string TIMEZONE_ID { get; set; }

        [FieldSettings("Email dropbox identifier")]
        public string EMAIL_DROPBOX_IDENTIFIER { get; set; }

        [FieldSettings("User currency")]
        public string USER_CURRENCY { get; set; }

        [FieldSettings("Contact display")]
        public string CONTACT_DISPLAY { get; set; }

        [FieldSettings("Contact order")]
        public string CONTACT_ORDER { get; set; }

        [FieldSettings("User ID")]
        public int USER_ID { get; set; }

        #endregion

        #region Hidden properties

        public bool? ADMINISTRATOR { get; set; }
        public bool? ACCOUNT_OWNER { get; set; }
        public bool? ACTIVE { get; set; }
        public int? CONTACT_ID { get; set; }
        public DateTime? DATE_CREATED_UTC { get; set; }
        public DateTime? DATE_UPDATED_UTC { get; set; }

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

    }
}
