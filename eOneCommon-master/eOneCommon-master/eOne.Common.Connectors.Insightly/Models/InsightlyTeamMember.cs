using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyTeamMember : DataConnectorEntityModel
    {

        public int? PERMISSION_ID { get; set; }
        public int? TEAM_ID { get; set; }
        public int? MEMBER_USER_ID { get; set; }
        public int? MEMBER_TEAM_ID { get; set; }

    }
}
