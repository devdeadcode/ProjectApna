using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyTeam : ConnectorEntityModel
    {

        public int? TEAM_ID { get; set; }
        public string TEAM_NAME { get; set; }
        public bool ANONYMOUS_TEAM { get; set; }
        public DateTime DATE_CREATED_UTC { get; set; }
        public DateTime DATE_UPDATED_UTC { get; set; }
        public List<InsightlyTeamMember> TEAMMEMBERS { get; set; }

    }
}
