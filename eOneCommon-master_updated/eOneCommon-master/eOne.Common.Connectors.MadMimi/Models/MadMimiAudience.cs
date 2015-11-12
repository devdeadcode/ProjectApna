using System;

namespace eOne.Common.Connectors.MadMimi.Models
{
    public class MadMimiAudience : ConnectorEntityModel
    {

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Name", DefaultField = true)]
        public string name => CombineFirstLastName(first_name, last_name);

        [FieldSettings("First name")]
        public string first_name { get; set; }

        [FieldSettings("Last name")]
        public string last_name { get; set; }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime created_at_date { get; set; }

        [FieldSettings("City")]
        public string city { get; set; }

        [FieldSettings("Phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string phone { get; set; }

        [FieldSettings("Company")]
        public string company { get; set; }

        [FieldSettings("Title")]
        public string title { get; set; }

        [FieldSettings("Address")]
        public string address { get; set; }

        [FieldSettings("State")]
        public string state { get; set; }

        [FieldSettings("Zip")]
        public string zip { get; set; }

        [FieldSettings("Country")]
        public string country { get; set; }

        //[FieldSettings("Confirmed")]
        //public string confirmed => columns.confirmed;

        //[FieldSettings("Lists")]
        //public string lists => CommaSeparatedValues(columns.lists);

    }
}
