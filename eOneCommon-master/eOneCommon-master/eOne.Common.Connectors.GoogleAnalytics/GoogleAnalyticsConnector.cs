using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.GoogleAnalytics.Models;
using eOne.Common.DataConnectors.Rest;
using System.Reflection;
using eOne.Common.DataConnectors;

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
            Name = "Google Analytics"; https://www.getpostman.com/oauth2/callback
            Group = ConnectorGroup.Other;
            Key = "116220418769-l5u1185vkj8c0c8n6s354hrs17439mv0.apps.googleusercontent.com";
            Secret = "E_tpnLCqodRO8_6oB6OoK8Ts";
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            BaseUrl = "https://www.googleapis.com/analytics/v3/data/ga";
            AuthorizationUri = "https://accounts.google.com/o/oauth2/auth";
            AccessTokenUri = "https://www.googleapis.com/oauth2/v3/token";
            Scope = "https://www.googleapis.com/auth/analytics";
            CallbackUrl = "http://www.popdock.com/callbacks/googleAnalytics";
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            // todo - create new model with each field as a separate property
            AddEntity(EntityIdAdwords, "Analytics adwords", typeof(GoogleAnalyticsAdwords));
            AddEntity(EntityIdAdsense, "Analytics adsense", typeof(GoogleAnalyticsAdsense));
            AddEntity(EntityIdSources, "Analytics sources", typeof(GoogleAnalyticsSources));
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
                case EntityIdAdwords:
                    var adWordQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(adWordsName => adWordsName == "Search query" | adWordsName == "Network" | adWordsName == "Date"))
                    {
                        adWordQuery += "&dimensions=";
                    }
                    foreach (var adWordsName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (adWordsName)
                        {
                            case "Search query":
                                adWordQuery += "ga:adMatchedQuery,";
                                break;
                            case "Network":
                                adWordQuery += "ga:adDistributionNetwork,";
                                break;
                            case "Date":
                                adWordQuery += "ga:date,";
                                break;

                                //search query and network result no rows
                        }
                    }
                    adWordQuery = adWordQuery.Remove(adWordQuery.Length - 1);
                    adWordQuery += "&metrics=";

                    foreach (var adWordsName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (adWordsName)
                        {
                            case "Impressions":
                                adWordQuery += "ga:impressions,";
                                break;
                            case "Clicks":
                                adWordQuery += "ga:adClicks,";
                                break;
                            case "Cost":
                                adWordQuery += "ga:adCost,";
                                break;
                        }
                    }

                    adWordQuery = adWordQuery.Remove(adWordQuery.Length - 1);
                    adWordQuery += "&start-date=2015-10-13" + "&end-date=today";

                    return adWordQuery;
                //return "?ids=ga:101647905" +
                //    "&dimensions=ga:adMatchedQuery,ga:adDistributionNetwork,ga:date" +
                //    "&metrics=ga:impressions,ga:adCost,ga:adClicks" +
                //    "&start-date=2015-10-16" +
                //    "&end-date=today";


                case EntityIdAdsense:
                    var adsenseQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(adSenseName => adSenseName == "Search query" | adSenseName == "Network" | adSenseName == "Date"))
                    {
                        adsenseQuery += "&dimensions=";
                    }
                    foreach (var adSenseName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (adSenseName)
                        {
                            case "Search query":
                                adsenseQuery += "ga:adMatchedQuery,";
                                break;
                            case "Network":
                                adsenseQuery += "ga:adDistributionNetwork,";
                                break;
                            case "Date":
                                adsenseQuery += "ga:date,";
                                break;
                        }
                    }
                    adsenseQuery = adsenseQuery.Remove(adsenseQuery.Length - 1);
                    adsenseQuery += "&metrics=";

                    foreach (var adSenseName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (adSenseName)
                        {
                            case "Impressions":
                                adsenseQuery += "ga:impressions,";
                                break;
                            case "Clicks":
                                adsenseQuery += "ga:adClicks,";
                                break;
                            case "Cost":
                                adsenseQuery += "ga:adCost,";
                                break;
                            case "Revenue":
                                adsenseQuery += "ga:adsenseRevenue,";
                                break;
                        }
                    }
                    adsenseQuery = adsenseQuery.Remove(adsenseQuery.Length - 1);
                    adsenseQuery += "&start-date=2015-10-13" + "&end-date=today";
                    return adsenseQuery;


                case EntityIdSources:
                    var sourcesQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(sourcesName => sourcesName == "Source" | sourcesName == "Social network" | sourcesName == "Campaign" | sourcesName == "Medium" | sourcesName == "Date"))
                    {
                        sourcesQuery += "&dimensions=";
                    }
                    foreach (var sourcesName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (sourcesName)
                        {
                            case "Source":
                                sourcesQuery += "ga:source,";
                                break;
                            case "Social network":
                                sourcesQuery += "ga:socialNetwork,";
                                break;
                            case "Campaign":
                                sourcesQuery += "ga:campaign,";
                                break;
                            case "Medium":
                                sourcesQuery += "ga:medium,";
                                break;
                            case "Date":
                                sourcesQuery += "ga:date,";
                                break;
                        }
                    }
                    sourcesQuery = sourcesQuery.Remove(sourcesQuery.Length - 1);
                    sourcesQuery += "&metrics=";
                    foreach (var sourcesName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (sourcesName)
                        {
                            case "Number of users":
                                sourcesQuery += "ga:users,";
                                break;
                        }
                    }
                    sourcesQuery = sourcesQuery.Remove(sourcesQuery.Length - 1);
                    sourcesQuery += "&start-date=2015-10-28" + "&end-date=today";
                    return sourcesQuery;


                case EntityIdSearchEngines:
                    return "";
                case EntityIdKeywords:
                    var keywordQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(keywordName => keywordName == "Keyword" | keywordName == "Date"))
                    {
                        keywordQuery += "&dimensions=";
                    }
                    foreach (var keywordName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (keywordName)
                        {
                            case "Keyword":
                                keywordQuery += "ga:keyword,";
                                break;
                            case "Date":
                                keywordQuery += "ga:date,";
                                break;

                        }
                    }
                    keywordQuery = keywordQuery.Remove(keywordQuery.Length - 1);
                    keywordQuery += "&metrics=";
                    foreach (var keywordName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (keywordName)
                        {
                            case "Number of searches":
                                keywordQuery += "ga:organicSearches,";
                                break;
                        }
                    }
                    keywordQuery = keywordQuery.Remove(keywordQuery.Length - 1);
                    keywordQuery += "&start-date=2015-10-28" + "&end-date=today";
                    return keywordQuery;


                case EntityIdUsers:
                    var userQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(name => name == "Date" | name == "User type" | name == "Gender" | name == "Age bracket"))
                    {
                        userQuery += "&dimensions=";
                    }
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Date":
                                userQuery += "ga:date,";
                                break;
                            case "User type":
                                userQuery += "ga:userType,";
                                break;
                            case "Gender":
                                userQuery += "ga:userGender,";
                                break;
                            case "Age bracket":
                                userQuery += "ga:userAgebracket,";
                                break;
                        }
                    }
                    userQuery = userQuery.Remove(userQuery.Length - 1);
                    userQuery += "&metrics=";
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Number of users":
                                userQuery += "ga:users,";
                                break;
                            case "Page views":
                                userQuery += "ga:pageviews,";
                                break;
                            case "Number of new users":
                                userQuery += "ga:newUsers,";
                                break;
                        }
                    }
                    userQuery = userQuery.Remove(userQuery.Length - 1);
                    userQuery += "&start-date=2015-10-13" + "&end-date=today";
                    return userQuery;


                case EntityIdBrowsers:
                    var browserQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(browserName => browserName == "Browser" | browserName == "Opertating system" | browserName == "Device category" | browserName == "Browser version" | browserName == "Operation system version" | browserName == "Date"))
                    {
                        browserQuery += "&dimensions=";
                    }
                    foreach (var browserName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (browserName)
                        {
                            case "Browser":
                                browserQuery += "ga:browser,";
                                break;
                            case "Opertating system":
                                browserQuery += "ga:operatingSystem,";
                                break;
                            case "Device category":
                                browserQuery += "ga:deviceCategory,";
                                break;
                            case "Browser version":
                                browserQuery += "ga:browserVersion,";
                                break;
                            case "Operation system version":
                                browserQuery += "ga:operatingSystemVersion,";
                                break;
                            case "Date":
                                browserQuery += "ga:date,";
                                break;
                        }
                    }
                    browserQuery = browserQuery.Remove(browserQuery.Length - 1);
                    browserQuery += "&metrics=";
                    foreach (var browserName in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (browserName)
                        {
                            case "Number of users":
                                browserQuery += "ga:users,";
                                break;
                        }
                    }
                    browserQuery = browserQuery.Remove(browserQuery.Length - 1);
                    browserQuery += "&start-date=2015-10-13" + "&end-date=today";
                    return browserQuery;


                case EntityIdMobileDevices:
                    var mobileQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(name => name == "Date" | name == "Brand" | name == "Model" | name == "Name"))
                    {
                        mobileQuery += "&dimensions=";
                    }
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Date":
                                mobileQuery += "ga:date,";
                                break;
                            case "Brand":
                                mobileQuery += "ga:mobileDeviceBranding,";
                                break;
                            case "Model":
                                mobileQuery += "ga:mobileDeviceModel,";
                                break;
                            case "Name":
                                mobileQuery += "ga:mobileDeviceMarketingName,";
                                break;
                        }
                    }
                    mobileQuery = mobileQuery.Remove(mobileQuery.Length - 1);
                    mobileQuery += "&metrics=";
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Number of users":
                                mobileQuery += "ga:users,";
                                break;
                            case "Page views":
                                mobileQuery += "ga:pageviews,";
                                break;
                        }
                    }
                    mobileQuery = mobileQuery.Remove(mobileQuery.Length - 1);
                    mobileQuery += "&start-date=2015-10-13" + "&end-date=today";
                    return mobileQuery;


                case EntityIdSession:
                    var sessionQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(name => name == "Date"))
                    {
                        sessionQuery += "&dimensions=";
                    }
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Date":
                                sessionQuery += "ga:date,";
                                break;
                        }
                    }
                    sessionQuery = sessionQuery.Remove(sessionQuery.Length - 1);
                    sessionQuery += "&metrics=";
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Number of sessions":
                                sessionQuery += "ga:sessions,";
                                break;
                            case "Duration":
                                sessionQuery += "ga:sessionDuration,";
                                break;
                            case "Bounces":
                                sessionQuery += "ga:bounces,";
                                break;
                            case "Hits":
                                sessionQuery += "ga:hits,";
                                break;
                        }
                    }
                    sessionQuery = sessionQuery.Remove(sessionQuery.Length - 1);
                    sessionQuery += "&start-date=2015-10-28" + "&end-date=today";
                    return sessionQuery;


                case EntityIdLocations:
                    var locationQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(name => name == "Date" | name == "Country" | name == "Region" | name == "City"))
                    {
                        locationQuery += "&dimensions=";
                    }
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Date":
                                locationQuery += "ga:date,";
                                break;
                            case "Country":
                                locationQuery += "ga:country,";
                                break;
                            case "Region":
                                locationQuery += "ga:region,";
                                break;
                            case "City":
                                locationQuery += "ga:city,";
                                break;
                        }
                    }
                    locationQuery = locationQuery.Remove(locationQuery.Length - 1);
                    locationQuery += "&metrics=";
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Number of users":
                                locationQuery += "ga:users,";
                                break;
                            case "Page views":
                                locationQuery += "ga:pageviews,";
                                break;
                        }
                    }
                    locationQuery = locationQuery.Remove(locationQuery.Length - 1);
                    locationQuery += "&start-date=2015-10-13" + "&end-date=today";
                    return locationQuery;


                case EntityIdPages:
                    var pagesQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(name => name == "Path" | name == "Title" | name == "Date"))
                    {
                        pagesQuery += "&dimensions=";
                    }
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Date":
                                pagesQuery += "ga:date,";
                                break;
                            case "Path":
                                pagesQuery += "ga:pagePath,";
                                break;
                            case "Title":
                                pagesQuery += "ga:pageTitle,";
                                break;
                        }
                    }
                    pagesQuery = pagesQuery.Remove(pagesQuery.Length - 1);
                    pagesQuery += "&metrics=";
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Page views":
                                pagesQuery += "ga:users,";
                                break;
                            case "Unique page views":
                                pagesQuery += "ga:pageviews,";
                                break;
                            case "Time on page":
                                pagesQuery += "ga:pageviews,";
                                break;
                        }
                    }
                    pagesQuery = pagesQuery.Remove(pagesQuery.Length - 1);
                    pagesQuery += "&start-date=2015-10-13" + "&end-date=today";
                    return pagesQuery;


                case EntityIdInternalSearches:
                    var searchQuery = "?ids=ga:101647905";
                    if (query.Fields.Select(t => t.DisplayName).Any(name => name == "Keyword" | name == "Category" | name == "Date"))
                    {
                        searchQuery += "&dimensions=";
                    }
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Date":
                                searchQuery += "ga:date,";
                                break;
                            case "Keyword":
                                searchQuery += "ga:searchKeyword,";
                                break;
                            case "Category":
                                searchQuery += "ga:searchCategory,";
                                break;
                        }
                    }
                    searchQuery = searchQuery.Remove(searchQuery.Length - 1);
                    searchQuery += "&metrics=";
                    foreach (var name in query.Entity.Fields.Select(t => t.DisplayName))
                    {
                        switch (name)
                        {
                            case "Refinements":
                                searchQuery += "ga:searchRefinements,";
                                break;
                            case "Number of searches":
                                searchQuery += "ga:searchResultViews,";
                                break;
                            case "Time after search":
                                searchQuery += "ga:searchDuration,";
                                break;
                        }
                    }
                    searchQuery = searchQuery.Remove(searchQuery.Length - 1);
                    searchQuery += "&start-date=2015-10-13" + "&end-date=today";
                    return searchQuery;
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
                    data = "[\n    " + data + "    \n]";
                    var adWords = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaAdWordsData = new List<GoogleAnalyticsAdwords>();
                    var adWordsdata = new List<string>();
                    if (adWords[0].rows != null)
                    {
                        for (var i = 0; i < adWords[0].rows.Count; i++)
                        {
                            adWordsdata.AddRange(adWords[0].rows[i]);
                            gaAdWordsData.Add(new GoogleAnalyticsAdwords { });
                            for (var j = 0; j < adWordsdata.Count; j++)
                            {
                                var name = adWords[0].columnHeaders[j].updatedName;
                                switch (name)
                                {
                                    case "adMatchedQuery":
                                        gaAdWordsData[i].adMatchedQuery = adWordsdata[j];
                                        break;
                                    case "adDistributionNetwork":
                                        gaAdWordsData[i].adDistributionNetwork = adWordsdata[j];
                                        break;
                                    case "date":
                                        gaAdWordsData[i].date = adWordsdata[j];
                                        break;
                                    case "impressions":
                                        gaAdWordsData[i].impressions = Convert.ToInt16(adWordsdata[j]);
                                        break;
                                    case "adClicks":
                                        gaAdWordsData[i].adClicks = Convert.ToInt16(adWordsdata[j]);
                                        break;
                                    case "adCost":
                                        gaAdWordsData[i].adCost = Convert.ToDecimal(adWordsdata[j]);
                                        break;
                                }
                            }
                            gaAdWordsData.Add(new GoogleAnalyticsAdwords()
                            {
                                adMatchedQuery = adWordsdata[0],
                                adDistributionNetwork = adWordsdata[1],
                                date = adWordsdata[2],
                                impressions = Convert.ToInt16(adWordsdata[3]),
                                adCost = Convert.ToDecimal(adWordsdata[4]),
                                adClicks = Convert.ToInt16(adWordsdata[5])
                            });
                            adWordsdata.Clear();
                        }
                    }
                    return gaAdWordsData;


                case EntityIdAdsense:
                    data = "[\n    " + data + "    \n]";
                    var adsenseData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaAdsense = new List<GoogleAnalyticsAdsense>();
                    var adsensedata = new List<string>();
                    for (var i = 0; i < adsenseData[0].rows.Count; i++)
                    {
                        adsensedata.AddRange(adsenseData[0].rows[i]);
                        gaAdsense.Add(new GoogleAnalyticsAdsense { });
                        for (var j = 0; j < adsensedata.Count; j++)
                        {
                            var name = adsenseData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaAdsense[i].date = adsensedata[j];
                                    break;
                                case "adsenseAdsClicks":
                                    gaAdsense[i].adsenseAdsClicks = Convert.ToInt16(adsensedata[j]);
                                    break;
                                case "adsenseRevenue":
                                    gaAdsense[i].adsenseRevenue = adsensedata[j];
                                    break;
                                case "adsenseAdsViewed":
                                    gaAdsense[i].adsenseAdsViewed = Convert.ToInt16(adsensedata[j]);
                                    break;
                                case "adsensePageImpressions":
                                    gaAdsense[i].adsensePageImpressions = Convert.ToInt16(adsensedata[j]);
                                    break;
                            }
                        }
                        adsensedata.Clear();
                    }
                    return gaAdsense;


                case EntityIdSources:
                    data = "[\n    " + data + "    \n]";
                    var sourcesData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaSources = new List<GoogleAnalyticsSources>();
                    var sourcesdata = new List<string>();
                    for (var i = 0; i < sourcesData[0].rows.Count; i++)
                    {
                        sourcesdata.AddRange(sourcesData[0].rows[i]);
                        gaSources.Add(new GoogleAnalyticsSources { });
                        for (var j = 0; j < sourcesdata.Count; j++)
                        {
                            var name = sourcesData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "source":
                                    gaSources[i].source = sourcesdata[j];
                                    break;
                                case "users":
                                    gaSources[i].users = Convert.ToInt16(sourcesdata[j]);
                                    break;
                                case "socialNetwork":
                                    gaSources[i].socialNetwork = sourcesdata[j];
                                    break;
                                case "campaign":
                                    gaSources[i].campaign = sourcesdata[j];
                                    break;
                                case "medium":
                                    gaSources[i].medium = sourcesdata[j];
                                    break;
                                case "date":
                                    gaSources[i].date = sourcesdata[j];
                                    break;
                            }
                        }
                        sourcesdata.Clear();
                    }
                    return gaSources;


                case EntityIdSearchEngines:
                    return DeserializeJson<List<GoogleAnalyticsSearchEngines>>(data);
                case EntityIdKeywords:
                    data = "[\n    " + data + "    \n]";
                    var keywordData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaKeyword = new List<GoogleAnalyticsKeywords>();
                    var keyworddata = new List<string>();
                    for (var i = 0; i < keywordData[0].rows.Count; i++)
                    {
                        keyworddata.AddRange(keywordData[0].rows[i]);
                        gaKeyword.Add(new GoogleAnalyticsKeywords { });
                        for (var j = 0; j < keyworddata.Count; j++)
                        {
                            var name = keywordData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaKeyword[i].date = keyworddata[j];
                                    break;
                                case "keyword":
                                    gaKeyword[i].keyword = keyworddata[j];
                                    break;
                                case "organicSearches":
                                    gaKeyword[i].organicSearches = Convert.ToInt16(keyworddata[j]);
                                    break;
                            }
                        }
                        keyworddata.Clear();
                    }
                    return gaKeyword;


                case EntityIdUsers:
                    data = "[\n    " + data + "    \n]";
                    var userData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaUsers = new List<GoogleAnalyticsUsers>();
                    var userdata = new List<string>();
                    for (var i = 0; i < userData[0].rows.Count; i++)
                    {
                        userdata.AddRange(userData[0].rows[i]);
                        gaUsers.Add(new GoogleAnalyticsUsers { });
                        for (var j = 0; j < userdata.Count; j++)
                        {
                            var name = userData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaUsers[i].date = userdata[j];
                                    break;
                                case "userType":
                                    gaUsers[i].userType = userdata[j];
                                    break;
                                case "userGender":
                                    gaUsers[i].userGender = userdata[j];
                                    break;
                                case "userAgebracket":
                                    gaUsers[i].userAgebracket = userdata[j];
                                    break;
                                case "pageviews":
                                    gaUsers[i].pageviews = Convert.ToInt16(userdata[j]);
                                    break;
                                case "users":
                                    gaUsers[i].users = Convert.ToInt16(userdata[j]);
                                    break;
                                case "newUsers":
                                    gaUsers[i].newUsers = Convert.ToInt16(userdata[j]);
                                    break;
                            }
                        }
                        userdata.Clear();
                    }
                    return gaUsers;


                case EntityIdBrowsers:
                    data = "[\n    " + data + "    \n]";
                    var browserData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaBrowsers = new List<GoogleAnalyticsBrowsers>();
                    var browserdata = new List<string>();
                    for (var i = 0; i < browserData[0].rows.Count; i++)
                    {
                        browserdata.AddRange(browserData[0].rows[i]);
                        gaBrowsers.Add(new GoogleAnalyticsBrowsers { });
                        for (var j = 0; j < browserdata.Count; j++)
                        {
                            var name = browserData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaBrowsers[i].date = browserdata[j];
                                    break;
                                case "browser":
                                    gaBrowsers[i].browser = browserdata[j];
                                    break;
                                case "operatingSystem":
                                    gaBrowsers[i].operatingSystem = browserdata[j];
                                    break;
                                case "deviceCategory":
                                    gaBrowsers[i].deviceCategory = browserdata[j];
                                    break;
                                case "browserVersion":
                                    gaBrowsers[i].browserVersion = browserdata[j];
                                    break;
                                case "users":
                                    gaBrowsers[i].users = Convert.ToInt16(browserdata[j]);
                                    break;
                                case "operatingSystemVersion":
                                    gaBrowsers[i].operatingSystemVersion = browserdata[j];
                                    break;
                            }
                        }
                        browserdata.Clear();
                    }
                    return gaBrowsers;


                case EntityIdMobileDevices:
                    data = "[\n    " + data + "    \n]";
                    var mobileData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaMobile = new List<GoogleAnalyticsMobileDevices>();
                    var mobiledata = new List<string>();
                    for (var i = 0; i < mobileData[0].rows.Count; i++)
                    {
                        mobiledata.AddRange(mobileData[0].rows[i]);
                        gaMobile.Add(new GoogleAnalyticsMobileDevices { });
                        for (var j = 0; j < mobiledata.Count; j++)
                        {
                            var name = mobileData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaMobile[i].date = mobiledata[j];
                                    break;
                                case "mobileDeviceBranding":
                                    gaMobile[i].mobileDeviceBranding = mobiledata[j];
                                    break;
                                case "mobileDeviceModel":
                                    gaMobile[i].mobileDeviceModel = mobiledata[j];
                                    break;
                                case "mobileDeviceMarketingName":
                                    gaMobile[i].mobileDeviceMarketingName = mobiledata[j];
                                    break;
                                case "pageviews":
                                    gaMobile[i].pageviews = Convert.ToInt16(mobiledata[j]);
                                    break;
                                case "users":
                                    gaMobile[i].users = Convert.ToInt16(mobiledata[j]);
                                    break;
                            }
                        }
                        mobiledata.Clear();
                    }
                    return gaMobile;


                case EntityIdSession:
                    data = "[\n    " + data + "    \n]";
                    var sessionData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaSessions = new List<GoogleAnalyticsSessions>();
                    var sessiondata = new List<string>();
                    for (var i = 0; i < sessionData[0].rows.Count; i++)
                    {
                        sessiondata.AddRange(sessionData[0].rows[i]);
                        gaSessions.Add(new GoogleAnalyticsSessions { });
                        for (var j = 0; j < sessiondata.Count; j++)
                        {
                            var name = sessionData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaSessions[i].date = sessiondata[j];
                                    break;
                                case "sessions":
                                    gaSessions[i].sessions = Convert.ToInt16(sessiondata[j]);
                                    break;
                                case "sessionDuration":
                                    gaSessions[i].sessionDuration = sessiondata[j];
                                    break;
                                case "bounces":
                                    gaSessions[i].bounces = Convert.ToInt16(sessiondata[j]);
                                    break;
                                case "hits":
                                    gaSessions[i].hits = Convert.ToInt16(sessiondata[j]);
                                    break;
                            }
                        }
                        sessiondata.Clear();
                    }
                    return gaSessions;


                case EntityIdLocations:
                    data = "[\n    " + data + "    \n]";
                    var locationData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaLocation = new List<GoogleAnalyticsLocations>();
                    var locationdata = new List<string>();
                    for (var i = 0; i < locationData[0].rows.Count; i++)
                    {
                        locationdata.AddRange(locationData[0].rows[i]);
                        gaLocation.Add(new GoogleAnalyticsLocations { });
                        for (var j = 0; j < locationdata.Count; j++)
                        {
                            var name = locationData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaLocation[i].date = locationdata[j];
                                    break;
                                case "country":
                                    gaLocation[i].country = locationdata[j];
                                    break;
                                case "region":
                                    gaLocation[i].region = locationdata[j];
                                    break;
                                case "city":
                                    gaLocation[i].city = locationdata[j];
                                    break;
                                case "pageviews":
                                    gaLocation[i].pageviews = Convert.ToInt16(locationdata[j]);
                                    break;
                                case "users":
                                    gaLocation[i].users = Convert.ToInt16(locationdata[j]);
                                    break;
                            }
                        }
                        locationdata.Clear();
                    }
                    return gaLocation;


                case EntityIdPages:
                    data = "[\n    " + data + "    \n]";
                    var pageData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaPage = new List<GoogleAnalyticsPageTracking>();
                    var pagedata = new List<string>();
                    for (var i = 0; i < pageData[0].rows.Count; i++)
                    {
                        pagedata.AddRange(pageData[0].rows[i]);
                        gaPage.Add(new GoogleAnalyticsPageTracking { });
                        for (var j = 0; j < pagedata.Count; j++)
                        {
                            var name = pageData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaPage[i].date = pagedata[j];
                                    break;
                                case "pagePath":
                                    gaPage[i].pagePath = pagedata[j];
                                    break;
                                case "pageTitle":
                                    gaPage[i].pageTitle = pagedata[j];
                                    break;
                                case "timeOnPage":
                                    gaPage[i].timeOnPage = pagedata[j];
                                    break;
                                case "uniquePageviews":
                                    gaPage[i].uniquePageviews = Convert.ToInt16(pagedata[j]);
                                    break;
                                case "pageviews":
                                    gaPage[i].pageviews = Convert.ToInt16(pagedata[j]);
                                    break;
                            }
                        }
                        pagedata.Clear();
                    }
                    return gaPage;

                case EntityIdInternalSearches:
                    data = "[\n    " + data + "    \n]";
                    var searchData = DeserializeJson<List<GoogleAnalyticsData>>(data);
                    var gaSearch = new List<GoogleAnalyticsInternalSearches>();
                    var searchdata = new List<string>();
                    if (searchData[0].rows == null) return gaSearch;
                    for (var i = 0; i < searchData[0].rows.Count; i++)
                    {
                        searchdata.AddRange(searchData[0].rows[i]);
                        gaSearch.Add(new GoogleAnalyticsInternalSearches { });
                        for (var j = 0; j < searchdata.Count; j++)
                        {
                            var name = searchData[0].columnHeaders[j].updatedName;
                            switch (name)
                            {
                                case "date":
                                    gaSearch[i].date = searchdata[j];
                                    break;
                                case "searchCategory":
                                    gaSearch[i].searchCategory = searchdata[j];
                                    break;
                                case "searchKeyword":
                                    gaSearch[i].searchKeyword = searchdata[j];
                                    break;
                                case "searchRefinements":
                                    gaSearch[i].searchRefinements = searchdata[j];
                                    break;
                                case "searchResultViews":
                                    gaSearch[i].searchResultViews = Convert.ToInt16(searchdata[j]);
                                    break;
                                case "searchDuration":
                                    gaSearch[i].searchDuration = searchdata[j];
                                    break;
                            }
                        }
                        searchdata.Clear();
                    }
                    return gaSearch;
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
