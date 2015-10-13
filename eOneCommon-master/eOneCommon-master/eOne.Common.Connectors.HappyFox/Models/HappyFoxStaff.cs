using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxStaff : DataConnectorEntityModel
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

        //[FieldSettings("Categories", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        //public string category = string.Join(",", categories);

        //[FieldSettings("Permissions", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        //public string permission = string.Join(",", permissions);
        
        #endregion
    }
}
