namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroAttachment
    {

        public string AttachmentID { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public int ContentLength { get; set; }
        public bool IncludeOnline { get; set; }

    }
}