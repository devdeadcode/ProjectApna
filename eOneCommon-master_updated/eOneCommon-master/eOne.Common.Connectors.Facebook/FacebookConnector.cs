using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Facebook.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Facebook
{
    public class FacebookConnector : RestConnector
    {

        private const int EntityIdLocationDemographics = 1;
        private const int EntityIdAgeGenderDemographics = 2;
        private const int EntityIdPost = 3;

        public FacebookConnector()
        {
            Name = "Facebook";
            Group = ConnectorGroup.SocialMedia;
            Key = "";
            Secret = "";
            AuthorizationUri = "https://login.salesforce.com/services/oauth2/authorize";
            AccessTokenUri = "https://login.salesforce.com/services/oauth2/token";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            CallbackUrl = "http://www.popdock.com/callbacks/facebook";
            AddSetup();
        }

        public override void Initialise()
        {
            base.Initialise();

            // get all pages that user has access to
            var pageJson = GetResponse("me/accounts");
            var pages = DeserializeJson<FacebookPages>(pageJson);
            if (pages == null) return;
            
            // add company for each page
            foreach (var page in pages.data) AddCompany(page.id, page.name);

            AddEntity(EntityIdLocationDemographics, "Location", typeof(FaceBookPageDemographicsLocation));
            AddEntity(EntityIdAgeGenderDemographics, "Age/gender", typeof(FaceBookPageDemographicsLocation));
            AddEntity(EntityIdPost, "Posts", typeof(FacebookPost));
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
