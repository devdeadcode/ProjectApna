using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookPageInsightsAgeGender : ConnectorEntityModel
    {

        public List<string> values { get; set; }
        public List<FacebookAgeGender> AgeGenders
        {
            get
            {
                return values.Select(GetAgeGender).Where(ageGender => ageGender != null).ToList();
            }
        }

        private static FacebookAgeGender GetAgeGender(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var ageGender = new FacebookAgeGender();
            var splitCount = value.Split(':');
            ageGender.Count = int.Parse(splitCount[1]);
            var splitAgeGender = splitCount[0].Replace("\"", "").Split('.');
            ageGender.Gender = ParseEnum<FacebookAgeGender.FacebookGender>(splitAgeGender[0]);
            ageGender.Age = splitAgeGender[1];
            return ageGender;
        }

    }
}
