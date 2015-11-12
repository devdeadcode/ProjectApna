using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.Twitter.Models;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Twitter
{
    public class TwitterConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdFollower = 1;
        public const int EntityIdMention = 2;
        public const int EntityIdRetweet = 3;
        public const int EntityIdDirectMessage = 4;
        public const int EntityIdList = 5;
        public const int EntityIdSubscriber = 6;
        public const int EntityIdListMember = 7;
        public const int EntityIdBlock = 8;
        

        #endregion

        #region Action IDs

        public const int ActionIdBlock = 1;
        public const int ActionIdUnblock = 2;

        #endregion

        #endregion


        public TwitterConnector()
        {
            Name = "Twitter";
            Group = ConnectorGroup.SocialMedia;
            Key = "";
            Secret = "";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth1;
            BaseUrl = "https://api.twitter.com/1.1/";

            // add rate limit for 180 requests every 15 minutes
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 180, ServiceConnectorRateLimiting.LimitPeriod.Minute, 15);
            // todo - add rate limits for each endpoint; see https://dev.twitter.com/rest/public/rate-limits

            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();

            var followerEntity = AddEntity(EntityIdFollower, "Followers", typeof(TwitterUser));
            var mentionEntity = AddEntity(EntityIdMention, "Mentions", typeof(TwitterTweet));
            var retweetEntity = AddEntity(EntityIdRetweet, "Retweets", typeof(TwitterTweet));
            var directMessageEntity = AddEntity(EntityIdDirectMessage, "Direct messages", typeof(TwitterDirectMessage));
            var listEntity = AddEntity(EntityIdList, "Lists", typeof(TwitterList));
            var subscriberEntity = AddEntity(EntityIdSubscriber, "Subscribers", typeof(TwitterUser));
            var listMemberEntity = AddEntity(EntityIdListMember, "List members", typeof(TwitterUser));
            var blockEntity = AddEntity(EntityIdBlock, "Blocks", typeof(TwitterUser));

            // todo - add advertising entities

            followerEntity.AddAction(ActionIdBlock, "Block");
            blockEntity.AddAction(ActionIdUnblock, "Unblock");

        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            // todo - handle query parameters
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdFollower:
                    return $"{BaseUrl}followers/list.json";
                case EntityIdMention:
                    return $"{BaseUrl}statuses/mentions_timeline.json";
                case EntityIdRetweet:
                    return $"{BaseUrl}statuses/retweets_of_me.json";
                case EntityIdDirectMessage:
                    return $"{BaseUrl}direct_messages.json";
                case EntityIdList:
                    return $"{BaseUrl}lists/list.json";
                case EntityIdSubscriber:
                    return $"{BaseUrl}lists/subscribers.json";
                case EntityIdListMember:
                    return $"{BaseUrl}lists/memberships.json";
                case EntityIdBlock:
                    return $"{BaseUrl}blocks/list.json";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdFollower:
                case EntityIdBlock:
                    var users = new List<TwitterUser>();
                    var userCollection = DeserializeJson<TwitterUserCollection>(data);
                    users.AddRange(userCollection.users);
                    while (userCollection.next_cursor != 0 && (query.Restrictions.Count != 0 || users.Count <= query.MaxRecords))
                    {
                        query.Cursor = userCollection.next_cursor_str;
                        var pageData = GetResponse(query);
                        userCollection = DeserializeJson<TwitterUserCollection>(pageData);
                        users.AddRange(userCollection.users);
                    }
                    return users;
                case EntityIdMention:
                case EntityIdRetweet:
                    return DeserializeJson<List<TwitterTweet>>(data);
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private new void AddSetup()
        {
            
        }

        #endregion
        
    }
}
