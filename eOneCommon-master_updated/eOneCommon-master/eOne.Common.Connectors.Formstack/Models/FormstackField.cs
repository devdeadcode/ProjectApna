namespace eOne.Common.Connectors.Formstack.Models
{
    public class FormstackField
    {

        public enum FormstackFieldType
        {
            text,
            select
        }

        public int id { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public FormstackFieldType type { get; set; }
        public string @default { get; set; }
        public int required { get; set; }

    }
}
