namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramUser : ConnectorEntityModel
    {

        [FieldSettings("Username", DefaultField = true, Description = true)]
        public string username { get; set; }

        [FieldSettings("Name", DefaultField = true)]
        public string full_name { get; set; }

        [FieldSettings("ID", KeyNumber = 1, SearchPriority = 0)]
        public string id { get; set; }

        [FieldSettings("Profile picture", SearchPriority = 0)]
        public string profile_picture { get; set; }

    }
}