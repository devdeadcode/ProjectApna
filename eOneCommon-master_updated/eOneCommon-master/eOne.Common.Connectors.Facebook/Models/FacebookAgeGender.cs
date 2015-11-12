using System.ComponentModel;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookAgeGender
    {

        public enum FacebookGender
        {
            [Description("Male")]
            M,
            [Description("Female")]
            F,
            [Description("Unknown")]
            U
        }

        public string Age { get; set; }
        public FacebookGender Gender { get; set; }
        public int Count { get; set; }

    }
}
