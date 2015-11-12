namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyTag : ConnectorEntityModel
    {

        public int id { get; set; }
        public string name { get; set; }
        public int account_id { get; set; }
        public int project_id { get; set; }
        public int user_id { get; set; }

    }
}