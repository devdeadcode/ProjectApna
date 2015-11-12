namespace eOne.Common.Connectors.MailChimp.Models.List 
{
    public class MailChimpListDefaults : ConnectorEntityModel
    {

        public string from_name { get; set; }
        public string from_email { get; set; }
        public string subject { get; set; }
        public IsoLanguage language { get; set; }

    }
}