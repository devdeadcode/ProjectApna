using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.Zopim.Models;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Zopim
{
    public class ZopimConnector : RestConnector
    {

        #region Constants

        public const int EntityIdChat = 1;
        public const int EntityIdAgent = 2;
        public const int EntityIdVisitor = 3;
        public const int EntityIdChatHistory = 4;
        public const int EntityIdBan = 5;

        #endregion

        public ZopimConnector()
        {
            Name = "Zopim";
            Group = ConnectorGroup.Chat;
            Key = "";
            Secret = "";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            BaseUrl = "https://www.zopim.com/api/v2/";
            AuthorizationUri = $"{BaseUrl}oauth2/authorizations/new";
            AccessTokenUri = $"{BaseUrl}oauth2/token"; 
            CallbackUrl = "http://www.popdock.com/callbacks/zopim";

            // rate limited to 200 requests per minute
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 200, ServiceConnectorRateLimiting.LimitPeriod.Minute);

            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();

            var chatEntity = AddEntity(EntityIdChat, "Chats", typeof(ZopimChat));
            var agentEntity = AddEntity(EntityIdAgent, "Agents", typeof(ZopimAgent));
            var visitorEntity = AddEntity(EntityIdVisitor, "Visitors", typeof(ZopimVisitor));
            var chatHistoryEntity = AddEntity(EntityIdChatHistory, "Chat history", typeof(ZopimChatHistory));
            AddEntity(EntityIdBan, "Bans", typeof(ZopimBanItem));

            chatEntity.AddRelatedEntity("History", chatHistoryEntity, "id", "chat_id");
            visitorEntity.AddRelatedEntity("Chats", chatEntity, "id", "visitor_id");

        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {

        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdChat:
                case EntityIdChatHistory:
                    return "chats";
                case EntityIdAgent:
                    return "agents";
                case EntityIdVisitor:
                    return "visitors";
                case EntityIdBan:
                    return "bans";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdChat:
                    return DeserializeJson<List<ZopimChat>>(data);
                case EntityIdChatHistory:
                    var chats = DeserializeJson<List<ZopimChat>>(data);
                    return (from chat in chats from message in chat.history select new ZopimChatHistory(message, chat)).ToList();
                case EntityIdAgent:
                    return DeserializeJson<List<ZopimAgent>>(data);
                case EntityIdVisitor:
                    return DeserializeJson<List<ZopimVisitor>>(data);
                case EntityIdBan:
                    var bans = DeserializeJson<ZopimBans>(data);
                    var banitems = bans.visitors.Select(visitor => new ZopimBanItem(visitor)).ToList();
                    banitems.AddRange(bans.ips.Select(ip => new ZopimBanItem(ip)));
                    return banitems;
            }
            return null;
        }

        #endregion

    }
}
