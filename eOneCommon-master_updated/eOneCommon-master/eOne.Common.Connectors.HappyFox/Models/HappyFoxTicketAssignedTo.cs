namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketAssignedTo : ConnectorEntityModel
    {
        public string name { get; set; }

        public string email { get; set; }

        public int id { get; set; }

        public bool active { get; set; }

        public HappyFoxStaffRole role { get; set; }

        public string role_name
        {
            get
            {
                try
                {
                    return role.name;
                }
                catch { return string.Empty; }
            }
        }

        public int role_id
        {
            get
            {
                try
                {
                    return role.id;
                }
                catch { return 0; }
            }
        }
    }
}
