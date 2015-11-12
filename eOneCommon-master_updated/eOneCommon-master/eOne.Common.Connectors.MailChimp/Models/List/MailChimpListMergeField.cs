namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListMergeField : ConnectorEntityModel
    {

        public int merge_id { get; set; }
        public string tag { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
        public string default_value { get; set; }
        public bool @public { get; set; }
        public int display_order { get; set; }
        public string help_text { get; set; }
        public string list_id { get; set; }
        public MailChimpListMergeFieldOptions options { get; set; }

    }
}



