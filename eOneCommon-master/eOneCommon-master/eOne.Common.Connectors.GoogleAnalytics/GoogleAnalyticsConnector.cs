using System;
using System.Collections.Generic;
using eOne.Common.Connectors.GoogleAnalytics.Models;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.GoogleAnalytics
{
    public class GoogleAnalyticsConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityIdData = 1;
        public const int EntityIdAdwords = 2;
        public const int EntityIdAdsense = 3;
        public const int EntityIdSources = 4;
        public const int EntityIdSearchEngines = 5;
        public const int EntityIdKeywords = 6;
        public const int EntityIdUsers = 7;
        public const int EntityIdBrowsers = 8;
        public const int EntityIdMobileDevices = 9;
        public const int EntityIdSession = 10;
        public const int EntityIdLocations = 11;
        public const int EntityIdPages = 12;
        public const int EntityIdInternalSearches = 13;

        #endregion

        public GoogleAnalyticsConnector()
        {
            Name = "Google Analytics";
            Group = ConnectorGroup.Other;
            Key = "";
            Secret = "";
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            BaseUrl = "https://www.googleapis.com/analytics/v3/data/ga";
            AuthorizationUri = "https://accounts.google.com/o/oauth2/auth";
            AccessTokenUri = "https://www.googleapis.com/oauth2/v3/token";
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            // todo - create new model with each field as a separate property
            AddEntity(EntityIdData, "Analytics data", typeof(GoogleAnalyticsData));
            AddEntity(EntityIdAdwords, "Analytics adwords", typeof(GoogleAnalyticsAdwords));
            AddEntity(EntityIdAdsense, "Analytics adsense", typeof(GoogleAnalyticsAdsense));
            AddEntity(EntityIdSources, "Analytics sources", typeof(GoogleAnalyticsSources));
            AddEntity(EntityIdSearchEngines, "Analytics data", typeof(GoogleAnalyticsSearchEngines));
            AddEntity(EntityIdKeywords, "Analytics keywords", typeof(GoogleAnalyticsKeywords));
            AddEntity(EntityIdUsers, "Analytics users", typeof(GoogleAnalyticsUsers));
            AddEntity(EntityIdBrowsers, "Analytics browsers", typeof(GoogleAnalyticsBrowsers));
            AddEntity(EntityIdMobileDevices, "Analytics mobile devices", typeof(GoogleAnalyticsMobileDevices));
            AddEntity(EntityIdSession, "Analytics session", typeof(GoogleAnalyticsSessions));
            AddEntity(EntityIdLocations, "Analytics locations", typeof(GoogleAnalyticsLocations));
            AddEntity(EntityIdPages, "Analytics pages", typeof(GoogleAnalyticsPageTracking));
            AddEntity(EntityIdInternalSearches, "Analytics internal searches", typeof(GoogleAnalyticsInternalSearches));

        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;

            switch (query.Entity.Id)
            {
                case EntityIdData:
                    return "";
                case EntityIdAdwords:
                    return "";
                case EntityIdAdsense:
                    return "";
                case EntityIdSources:
                    return "";
                case EntityIdSearchEngines:
                    return "";
                case EntityIdKeywords:
                    return "";
                case EntityIdUsers:
                    return "";
                case EntityIdBrowsers:
                    return "";
                case EntityIdMobileDevices:
                    return "";
                case EntityIdSession:
                    return "";
                case EntityIdLocations:
                    return "";
                case EntityIdPages:
                    return "";
                case EntityIdInternalSearches:
                    return "";
            }

            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdData:
                    return DeserializeJson<List<GoogleAnalyticsData>>(data);
                case EntityIdAdwords:
                    return DeserializeJson<List<GoogleAnalyticsAdwords>>(data);
                case EntityIdAdsense:
                    return DeserializeJson<List<GoogleAnalyticsAdsense>>(data);
                case EntityIdSources:
                    return DeserializeJson<List<GoogleAnalyticsSources>>(data);
                case EntityIdSearchEngines:
                    return DeserializeJson<List<GoogleAnalyticsSearchEngines>>(data);
                case EntityIdKeywords:
                    return DeserializeJson<List<GoogleAnalyticsKeywords>>(data);
                case EntityIdUsers:
                    return DeserializeJson<List<GoogleAnalyticsUsers>>(data);
                case EntityIdBrowsers:
                    return DeserializeJson<List<GoogleAnalyticsBrowsers>>(data);
                case EntityIdMobileDevices:
                    return DeserializeJson<List<GoogleAnalyticsMobileDevices>>(data);
                case EntityIdSession:
                    return DeserializeJson<List<GoogleAnalyticsSessions>>(data);
                case EntityIdLocations:
                    return DeserializeJson<List<GoogleAnalyticsLocations>>(data);
                case EntityIdPages:
                    return DeserializeJson<List<GoogleAnalyticsPageTracking>>(data);
                case EntityIdInternalSearches:
                    return DeserializeJson<List<GoogleAnalyticsInternalSearches>>(data);
            }
            //var analyticsData = DeserializeJson<GoogleAnalyticsData>(data);
            // todo - convert into list of values
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
