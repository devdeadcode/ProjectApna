namespace eOne.Common.Connectors.Facebook.Models
{
    public class FaceBookPageDemographicsLocation : ConnectorEntityModel
    {

        [FieldSettings("City", DefaultField = true)]
        public string City { get; set; }

        [FieldSettings("State", DefaultField = true)]
        public string State { get; set; }

        [FieldSettings("Country", DefaultField = true)]
        public string Country { get; set; }

        [FieldSettings("Total likes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int Likes { get; set; }

        [FieldSettings("Talking about (last 28 days)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TalkingAbout28Days { get; set; }

        [FieldSettings("Total impressions (last 28 days)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int Impressions28Days { get; set; }

        [FieldSettings("Total check-ins (last 28 days)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int Checkins28Days { get; set; }

        [FieldSettings("Unique check-ins (last 28 days)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int UniqueCheckins28Days { get; set; }

        [FieldSettings("Mobile check-ins (last 28 days)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int MobileCheckins28Days { get; set; }

        [FieldSettings("Unique mobile check-ins (last 28 days)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int UniqueMobileCheckins28Days { get; set; }

    }
}
