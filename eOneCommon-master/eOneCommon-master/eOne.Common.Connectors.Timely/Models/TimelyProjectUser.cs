using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyProjectUser : DataConnectorEntityModel
    {

        [FieldSettings("Project name", DefaultField = true)]
        public string ProjectName => Project.name;

        [FieldSettings("User name", DefaultField = true)]
        public string UserName => User.name;

        [FieldSettings("Hourly rate", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal HourlyRate
        {
            get
            {
                switch (Project.rate_type_enum)
                {
                    case TimelyProject.TimelyProjectRateType.project:
                        return Project.hour_rate;
                    case TimelyProject.TimelyProjectRateType.user:
                        return User.hour_rate;
                    default:
                        return 0;
                }
            }
        }

        [FieldSettings("User email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string UserEmail => User.email;

        [FieldSettings("User level", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(TimelyUser.TimelyUserLevel))]
        public TimelyUser.TimelyUserLevel UserLevel => User.user_level;

        [FieldSettings("Project active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool ProjectActive => Project.active;

        [FieldSettings("User active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool UserActive => User.active;

        [FieldSettings("Project link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string project_url => $"https://timelyapp.com/{account_id}/projects/{project_id}";

        [FieldSettings("User link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string user_url => $"https://timelyapp.com/{account_id}/users/{user_id}";

        public TimelyProject Project { get; set; }
        public TimelyUser User { get; set; }

        public int project_id => Project.id;
        public int user_id => User.id;
        public int account_id { get; set; }


    }
}
