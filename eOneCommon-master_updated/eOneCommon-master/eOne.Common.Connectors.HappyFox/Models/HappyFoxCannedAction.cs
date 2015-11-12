using System.Collections.Generic;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCannedAction : ConnectorEntityModel
    {
        public string name { get; set; }

        public string tags { get; set; }

        public List<int> available_to { get; set; }

        public string time_spent { get; set; }

        public string reply { get; set; }

        public string html_reply { get; set; }

        public int canned_id { get; set; }

        public List<int> canned_category { get; set; }

        public string canned_description { get; set; }




    }

   
}
