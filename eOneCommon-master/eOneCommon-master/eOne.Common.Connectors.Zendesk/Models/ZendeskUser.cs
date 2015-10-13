using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskUser : DataConnectorEntityModel
    {

        #region Enums

        public enum ZendeskUserRole
        {
            end_user,
            agent,
            admin
        }
        public enum ZendeskUserTicketRestriction
        {
            organization,
            groups,
            assigned,
            requested
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true, SearchPriority = 5)]
        public string name { get; set; }

        [FieldSettings("Email", DefaultField = true, SearchPriority = 3, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Phone", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string phone { get; set; }

        [FieldSettings("Role", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskUserRole))]
        public ZendeskUserRole role { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Url", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("External ID")]
        public string external_id { get; set; }

        [FieldSettings("Alias")]
        public string alias { get; set; }

        [FieldSettings("Active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool active { get; set; }

        [FieldSettings("Verified", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool verified { get; set; }

        [FieldSettings("Shared", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool shared { get; set; }

        [FieldSettings("Shared agent", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool shared_agent { get; set; }

        [FieldSettings("Locale")]
        public string locale { get; set; }

        [FieldSettings("Timezone")]
        public string time_zone { get; set; }

        [FieldSettings("Signature")]
        public string signature { get; set; }

        [FieldSettings("Details")]
        public string details { get; set; }

        [FieldSettings("Notes")]
        public string notes { get; set; }

        [FieldSettings("Moderator", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool moderator { get; set; }

        [FieldSettings("Can only create private comments", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool only_private_comments { get; set; }

        [FieldSettings("Restricted agent", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool restricted_agent { get; set; }

        [FieldSettings("Suspended", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool suspended { get; set; }

        [FieldSettings("Tickets the user has access to", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public ZendeskUserTicketRestriction ticket_restriction { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public int locale_id { get; set; }
        public int organization_id { get; set; }
        public int custom_role_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime last_login_at { get; set; }
        public List<string> tags { get; set; }
        public ZendeskAttachment photo { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Tags")]
        public string tag_list => CommaSeparatedValues(tags);

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Modifed at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_at_date => updated_at.Date;

        [FieldSettings("Last login at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime last_login_at_date => last_login_at.Date;

        #endregion

    }
}

