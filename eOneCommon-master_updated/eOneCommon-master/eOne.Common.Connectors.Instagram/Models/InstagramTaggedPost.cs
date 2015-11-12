namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramTaggedPost : InstagramPost
    {

        [FieldSettings("Tag", DefaultField = true)]
        public string tag { get; set; }

        [FieldSettings("Posted by username", DefaultField = true)]
        public string posted_by_username => user == null ? string.Empty : user.username;

        [FieldSettings("Posted by name")]
        public string posted_by_name => user == null ? string.Empty : user.full_name;

    }
}
