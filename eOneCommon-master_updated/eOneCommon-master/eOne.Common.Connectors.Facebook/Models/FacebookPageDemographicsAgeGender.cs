namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookPageDemographicsAgeGender : ConnectorEntityModel
    {

        [FieldSettings("Age", DefaultField = true)]
        public string Age { get; set; }

        [FieldSettings("Gender", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(FacebookAgeGender.FacebookGender))]
        public FacebookAgeGender.FacebookGender Gender { get; set; }

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
