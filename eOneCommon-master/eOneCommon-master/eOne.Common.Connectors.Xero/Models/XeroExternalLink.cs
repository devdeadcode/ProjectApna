namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroExternalLink
    {

        public string Url { get; set; }
        public string Description { get; set; }

        public string DisplayUrl => $"<a href='{Url}' target='_blank'>{(string.IsNullOrWhiteSpace(Description) ? Url : Description)}</a>";
    }
}
