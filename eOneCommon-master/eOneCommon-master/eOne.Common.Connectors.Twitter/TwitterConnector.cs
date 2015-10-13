using System;
using System.Collections.Generic;
using eOne.Common.Connectors.Twitter.Models;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.Twitter
{
    public class TwitterConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityIdFollower = 1;
        public const int EntityIdMention = 2;
        public const int EntityIdRetweet = 3;
        public const int EntityIdDirectMessage = 4;
        public const int EntityIdList = 5;
        public const int EntityIdSubscriber = 6;
        public const int EntityIdListMember = 7;
        public const int EntityIdListBlock = 8;

        #endregion

        public TwitterConnector()
        {
            Name = "Twitter";
            Group = ConnectorGroup.SocialMedia;
            Key = "";
            Secret = "";
            AuthenticationType = RestConnectorAuthenticationType.OAuth1;
            BaseUrl = "https://api.twitter.com/1.1/";
            var applicationRateLimit = new RestConnectorRateLimiting
            {
                AppliedTo = RestConnectorRateLimiting.LimitAppliedTo.Account,
                Period = RestConnectorRateLimiting.LimitPeriod.Minute,
                NumberOfPeriods = 15,
                Requests = 180
            };
            RateLimits.Add(applicationRateLimit);
            // todo - add rate limits for each endpoint; see https://dev.twitter.com/rest/public/rate-limits
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            AddEntity(EntityIdFollower, "Followers", typeof(TwitterUser));
            AddEntity(EntityIdMention, "Mentions", typeof(TwitterTweet));
            AddEntity(EntityIdRetweet, "Retweets", typeof(TwitterTweet));
            AddEntity(EntityIdDirectMessage, "Direct messages", typeof(TwitterDirectMessage));
            AddEntity(EntityIdList, "Lists", typeof(TwitterList));
            AddEntity(EntityIdSubscriber, "Subscribers", typeof(TwitterUser));
            AddEntity(EntityIdListMember, "List members", typeof(TwitterUser));
            AddEntity(EntityIdListBlock, "Blocks", typeof(TwitterUser));
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
                case EntityIdListBlock:
                    return $"{BaseUrl}blocks/list.json";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            throw new NotImplementedException();
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
