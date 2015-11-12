using System.Collections.Generic;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCannedActionsAssignedTo
    {
        public string name { get; set; }

        public string email { get; set; }

        public HappyFoxStaffRole role { get; set; }

        public bool active { get; set; }

        public int id { get; set; }

        public List<int> categories { get; set; }

        public List<string> permisssions { get; set; }
    }
}
