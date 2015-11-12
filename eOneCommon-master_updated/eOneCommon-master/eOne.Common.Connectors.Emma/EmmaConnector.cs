using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Emma.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Emma
{
    public class EmmaConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdGroup = 1;
        public const int EntityIdMember = 2;
        public const int EntityIdMailing = 3;
        public const int EntityIdResponse = 4;

        #endregion

        #region Action IDs



        #endregion

        #endregion

        public EmmaConnector()
        {
            Name = "Emma";
            Group = ConnectorGroup.MailingList;
            AuthenticationType = ServiceConnectorAuthenticationType.Basic;

            // rate limited to 180 calls per minute
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 180, ServiceConnectorRateLimiting.LimitPeriod.Minute);
        }

        public override void Initialise()
        {
            base.Initialise();

            var groupEntity = AddEntity(EntityIdGroup, "Groups", typeof(EmmaGroup));
            var memberEntity = AddEntity(EntityIdMember, "Members");
            var mailingEntity = AddEntity(EntityIdMailing, "Mailings");
            var responseEntity = AddEntity(EntityIdResponse, "Responses");

            groupEntity.AddRelatedEntity("Members", memberEntity, "", "");
            mailingEntity.AddRelatedEntity("Response", responseEntity, "", "");

        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdGroup:
                    return "groups";
            }
            return string.Empty;
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdGroup:
                    return TupleHelper.CreateTupleStringList("group_types", "all");
            }
            return null;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            throw new NotImplementedException();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

    }
}
