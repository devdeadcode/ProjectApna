namespace eOne.Common.Connectors.Asana.Models
{
    public class AsanaWorkspace
    {
        [FieldSettings("ID")]
        public int id { get; set; }

        [FieldSettings("Name")]
        public string name { get; set; }
    }
}
