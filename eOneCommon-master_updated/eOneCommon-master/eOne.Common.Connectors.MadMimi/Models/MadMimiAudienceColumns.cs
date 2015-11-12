namespace eOne.Common.Connectors.MadMimi.Models
{
    public class MadMimiAudienceColumns : ConnectorEntityModel
    {

        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public MadMimiDate created_at { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
        public string company { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string confirmed { get; set; }
        //public List<string> lists { get; set; }

    }
}