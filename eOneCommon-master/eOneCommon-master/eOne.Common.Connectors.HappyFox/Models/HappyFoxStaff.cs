using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxStaff : DataConnectorEntityModel
    {
        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Staff ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Role", DefaultField = true)]
        public string role_name => role.name;

        [FieldSettings("Active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool active { get; set; }


        #region Hidden Properties
        public HappyFoxStaffRole role { get; set; }
        public List<int> categories { get; set; }
        public List<string> permissions { get; set; }
        #endregion

        #region Calculations

        [FieldSettings("Categories", Created = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public string category => string.Join(",", categories);

        [FieldSettings("Permissions", Created = true)]
        public string permission => string.Join(",", permissions);

        #endregion
    }
}
