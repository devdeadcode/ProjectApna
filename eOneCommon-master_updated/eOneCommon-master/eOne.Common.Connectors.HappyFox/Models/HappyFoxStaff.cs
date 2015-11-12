using System.Collections.Generic;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxStaff : ConnectorEntityModel
    {
        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Email", DefaultField = true)]
        public string email { get; set; }

        [FieldSettings("Staff ID")]
        public int id { get; set; }

        public HappyFoxStaffRole role { get; set; }

        [FieldSettings("Role", DefaultField = true)]
        public string role_name => role.name;

        [FieldSettings("Active")]
        public bool active { get; set; }

        #region Hidden Properties

        public static List<int> categories { get; set; }
        public static List<string> permissions { get; set; }
        #endregion

        #region Calculations

        //[FieldSettings("Categories", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        //public string category = string.Join(",", categories);

        //[FieldSettings("Permissions", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        //public string permission = string.Join(",", permissions);
        
        #endregion
    }
}
