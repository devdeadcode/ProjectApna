namespace eOne.Common.Connectors.Formstack.Models
{
    public class FormstackConfirmation
    {

        public int id { get; set; }
        public int form { get; set; }
        public string name { get; set; }
        public int to_field { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string delay { get; set; }

    }
}

//{
//  "confirmations": [{
//    "type": "data",
//    "format": "html",
//    "hide_empty": "0",
//    "show_section": "0",
//  }]
//}
