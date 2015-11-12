using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Emma.Models
{
    public class EmmaGroup : ConnectorEntityModel
    {

        public enum EmmaGroupType
        {
            [Description("Group")]
            g,
            [Description("All")]
            all,
            [Description("Test")]
            t,
            [Description("Hidden")]
            h
        }

        [FieldSettings("Name", DefaultField = true)]
        public string group_name { get; set; }

        [FieldSettings("Active members", DefaultField = true, FieldTypeId = 1)]
        public int active_count { get; set; }

        public EmmaGroupType group_type { get; set; }

        public DateTime? deleted_at { get; set; }
        public int error_count { get; set; }
        public int optout_count { get; set; }
        
        public int member_group_id { get; set; }
        public DateTime? purged_at { get; set; }
        public int account_id { get; set; }

        public bool deleted => deleted_at != null;
        public bool purged => purged_at != null;

    }
}
