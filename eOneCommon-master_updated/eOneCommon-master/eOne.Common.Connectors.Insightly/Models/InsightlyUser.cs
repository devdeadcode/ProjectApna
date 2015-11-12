using System;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyUser : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name => CombineFirstLastName(FIRST_NAME, LAST_NAME);

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string EMAIL_ADDRESS { get; set; }

        [FieldSettings("Administrator", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool Administrator => (ADMINISTRATOR != null) && (bool)ADMINISTRATOR;

        [FieldSettings("Account owner", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool AccountOwner => (ACCOUNT_OWNER != null) && (bool)ACCOUNT_OWNER;

        [FieldSettings("Active", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool Active => (ACTIVE != null) && (bool)ACTIVE;

        #endregion

        #region Properties

        [FieldSettings("First name")]
        public string FIRST_NAME { get; set; }

        [FieldSettings("Last name")]
        public string LAST_NAME { get; set; }

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

        [FieldSettings("User ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int USER_ID { get; set; }

        [FieldSettings("Date created", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_CREATED_UTC { get; set; }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_UPDATED_UTC { get; set; }

        #endregion

        #region Hidden properties

        public bool? ADMINISTRATOR { get; set; }
        public bool? ACCOUNT_OWNER { get; set; }
        public bool? ACTIVE { get; set; }
        public int? CONTACT_ID { get; set; }

        #endregion

    }
}
