using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskUser : ConnectorEntityModel
    {

        #region Enums

        public enum ZendeskUserRole
        {
            [Description("End user")]
            end_user,
            [Description("Agent")]
            agent,
            [Description("Administrator")]
            admin
        }
        public enum ZendeskUserTicketRestriction
        {
            [Description("Organization")]
            organization,
            [Description("Groups")]
            groups,
            [Description("Assigned")]
            assigned,
            [Description("Requested")]
            requested
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true, SearchPriority = 5, Description = true)]
        public string name { get; set; }

        [FieldSettings("Email", DefaultField = true, SearchPriority = 3, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Phone", DefaultField = true, FieldTypeId = Connector.FieldTypeIdPhone)]
        public string phone { get; set; }

        [FieldSettings("Role", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskUserRole))]
        public ZendeskUserRole role { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("External ID")]
        public string external_id { get; set; }

        [FieldSettings("Alias")]
        public string alias { get; set; }

        [FieldSettings("Active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool active { get; set; }

        [FieldSettings("Verified", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool verified { get; set; }

        [FieldSettings("Shared", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool shared { get; set; }

        [FieldSettings("Shared agent", FieldTypeId = Connector.FieldTypeIdYesNo)]
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

        [FieldSettings("Moderator", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool moderator { get; set; }

        [FieldSettings("Can only create private comments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool only_private_comments { get; set; }

        [FieldSettings("Restricted agent", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool restricted_agent { get; set; }

        [FieldSettings("Suspended", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool suspended { get; set; }

        [FieldSettings("Tickets the user has access to", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public ZendeskUserTicketRestriction? ticket_restriction { get; set; }

        [FieldSettings("User ID", KeyNumber = 1)]
        public int id { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        [FieldSettings("Last login at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime last_login_at { get; set; }

        #endregion

        #region Hidden properties

        public int locale_id { get; set; }
        public int organization_id { get; set; }
        public int? custom_role_id { get; set; }
        public List<string> tags { get; set; }
        public ZendeskAttachment photo { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Tags")]
        public string tag_list => CommaSeparatedValues(tags);

        #endregion

    }
}

