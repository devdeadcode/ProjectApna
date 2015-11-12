namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyProjectUser : ConnectorEntityModel
    {

        [FieldSettings("Project name", DefaultField = true)]
        public string ProjectName => Project.name;

        [FieldSettings("User name", DefaultField = true)]
        public string UserName => User.name;

        [FieldSettings("Hourly rate", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
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

        [FieldSettings("User email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string UserEmail => User.email;

        [FieldSettings("User level", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(TimelyUser.TimelyUserLevel))]
        public TimelyUser.TimelyUserLevel UserLevel => User.user_level;

        [FieldSettings("Project active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ProjectActive => Project.active;

        [FieldSettings("User active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool UserActive => User.active;

        [FieldSettings("Project link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string project_url => $"https://timelyapp.com/{account_id}/projects/{project_id}";

        [FieldSettings("User link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string user_url => $"https://timelyapp.com/{account_id}/users/{user_id}";

        public TimelyProject Project { get; set; }
        public TimelyUser User { get; set; }

        [FieldSettings("Project ID", KeyNumber = 1)]
        public int project_id => Project.id;

        [FieldSettings("User ID", KeyNumber = 2)]
        public int user_id => User.id;
        
        public int account_id { get; set; }


    }
}
