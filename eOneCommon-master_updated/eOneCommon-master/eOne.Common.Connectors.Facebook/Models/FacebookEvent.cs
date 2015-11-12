using System;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookEvent : FacebookCore
    {

        public string description { get; set; }
        public string name { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public string category { get; set; }
        public FacebookEventPlace place { get; set; }

        public string place_name => place.name;
        public string place_address => place.location.address;
        public string place_city => place.location.city;
        public string place_country => place.location.country;
        public string place_street => place.location.street;
        public string place_state => place.location.state;
        public string place_zip => place.location.zip;

    }
}
