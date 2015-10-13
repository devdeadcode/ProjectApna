namespace eOne.Common.Connectors.Formstack.Models
{
    public class FormstackNotification
    {

        public enum FormstackNotificationEmailFormat
        {
            html, 
            plaintext
        }
        public enum FormstackNotificationFromType
        {
            noreply, 
            custom, 
            field
        }
        public enum FormstackNotificationType
        {
            data, 
            link, 
            fields
        }

        public int id { get; set; }
        public int form { get; set; }
        public string name { get; set; }
        public string recipients { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public bool hide_empty { get; set; }
        public bool show_section { get; set; }
        public int attach_limit { get; set; }
        public FormstackNotificationEmailFormat format { get; set; }
        public FormstackNotificationFromType from_type { get; set; }
        public string from_value { get; set; }
        public FormstackNotificationType type { get; set; }

    }
}