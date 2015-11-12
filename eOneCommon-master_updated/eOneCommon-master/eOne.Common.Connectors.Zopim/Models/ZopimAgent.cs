using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.Helpers;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimAgent : ConnectorEntityModel
    {

        #region Enums

        public enum ZopimAgentRole 
        {
            [Description("Owner")]
            owner,
            [Description("Administrator")]
            administrator,
            [Description("Agent")]
            agent
        }

        #endregion

        #region Default properties

        [FieldSettings("Display name", DefaultField = true, SearchPriority = 5)]
        public string display_name { get; set; }

        [FieldSettings("Email", DefaultField = true, SearchPriority = 4, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        #endregion

        #region Properties

        [FieldSettings("First name", SearchPriority = 4)]
        public string first_name { get; set; }

        [FieldSettings("Last name", SearchPriority = 4)]
        public string last_name { get; set; }

        [FieldSettings("Enabled", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public int enabled { get; set; }

        [FieldSettings("IM server ID")]
        public int im_server_id { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime create_date { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public List<int> departments { get; set; }
        public List<ZopimDepartment> department_objects { get; set; }
        public List<ZopimAgentRole> roles { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Roles")]
        public string role_list
        {
            get
            {
                return CommaSeparatedValues(roles.Select(role => role.GetAttributeDescription()).ToList());
            }
        }

        [FieldSettings("Departments")]
        public string department_list => CommaSeparatedValues((from department in department_objects where department.enabled select department.name).ToList());

        [FieldSettings("Name", SearchPriority = 0)]
        public string name => $"{first_name.Trim()} {last_name.Trim()}";

        #endregion

    }
}