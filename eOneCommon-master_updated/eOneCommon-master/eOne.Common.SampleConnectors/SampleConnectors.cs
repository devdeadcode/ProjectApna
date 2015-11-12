using System.Collections.Generic;
using eOne.Common.Connectors.DynamicsGp;
using eOne.Common.Connectors.Insightly;
using eOne.Common.Connectors.Instagram;
using eOne.Common.Connectors.MailChimp;
using eOne.Common.Connectors.SalesForce;
using eOne.Common.Connectors.Shopify;
using eOne.Common.Connectors.Timely;
using eOne.Common.Connectors.Trello;
using eOne.Common.Connectors.Zendesk;
using System.Linq;
using eOne.Common.Connectors.BombBomb;
using eOne.Common.Connectors.HappyFox;
using eOne.Common.Connectors.HubSpot;
using eOne.Common.Connectors.Infusionsoft;
using eOne.Common.Connectors.Intacct;
using eOne.Common.Connectors.MadMimi;
using eOne.Common.Connectors.Sendloop;
using eOne.Common.Connectors.Slack;

namespace eOne.Common.SampleConnectors
{
    public class SampleConnectors
    {

        public static List<Connectors.Connector> GetConnectors()
        {

            var connectors = new List<Connectors.Connector>();

            // add dynamics gp connector
            var gpConnector = new DynamicsGpConnector
            {
                Server = "ANDREW-EONE",
                Database = "DYNAMICS",
                Username = "sa",
                Password = "pass@word1"
            };
            connectors.Add(gpConnector);

            // add trello connector
            var trelloConnector = new TrelloConnector
            {
                Token = "d8611e1a98e0ab814e9553c5dc6d71d07b02479f39c89e703dd47b0ed1f9bdea",
                Username = "andrewbrown67"
            };
            connectors.Add(trelloConnector);

            // add shopify connector
            var shopifyConnector = new ShopifyConnector
            {
                Token = "23ed7b6a283f63be7cbfce44bf01a6c1",
            };
            shopifyConnector.Setup.Steps[0].Fields[1].Value = "eone-test";
            connectors.Add(shopifyConnector);

            // add timely connector
            var timelyConnector = new TimelyConnector
            {
                Token = "15947af958d5487a6c16ef8aba9e0790127b7c3e46e73492ad806382863e7bc0"
            };
            connectors.Add(timelyConnector);

            // add insightly connector
            var insightlyConnector = new InsightlyConnector
            {
                Token = "001465dc-7af0-4325-888b-dead79c611e8"
            };
            connectors.Add(insightlyConnector);

            // add mailchimp connector
            var mailChimpConnector = new MailChimpConnector
            {
                Token = "ed9a35ae4edcce6f18a23ad12eb6f0ea",
                Key = "84c675474a7124c84ee6b562428e2469-us9"
            };
            connectors.Add(mailChimpConnector);

            // add salesforce connector
            var salesforceConnector = new SalesForceConnector
            {
                Token = "00D280000011Crg!ARoAQGkD9Lz06E1mdxHesrF_YHPwX9viuA3RNVMLpAvNUUalGJDkVUwlM_qe_Qe1nRi.WwiaLf87fgBbAY5B9JxsgjKmvuVt",
                SitePrefix = "ap2"
            };
            connectors.Add(salesforceConnector);

            // add instagram connector
            var instagramConnector = new InstagramConnector
            {
                Token = "3178691.d0ef35c.0fc54bca3945441ba90b673e1faded2a",
                Tags = new List<string> { "katemartinmusic" }
            };
            connectors.Add(instagramConnector);

            // add zendesk connector
            var zendeskConnector = new ZendeskConnector
            {
                SitePrefix = "popdock",
                Username = "andrew.brown@eonesolutions.com",
                Password = "popdock"
            };
            connectors.Add(zendeskConnector);

            // add hubspot connector
            var hubspotConnector = new HubspotConnector()
            {
                //Token = "demooooo-oooo-oooo-oooo-oooooooooooo"
                Token = "805338c5-c4a5-4736-a4f9-f2bfef35bfda"
            };
            connectors.Add(hubspotConnector);

            // add infusionsoft connector
            var infusionsoftConnector = new InfusionsoftConnector()
            {
                Token = "sxdwp2ud75fpk62ca82qa6je"
            };
            connectors.Add(infusionsoftConnector);

            // add bombbomb connector
            var bombbombConnector = new BombBombConnector()
            {
                Username = "andrew.brown@eonesolutions.com",
                Password = "popdock"
            };
            connectors.Add(bombbombConnector);

            // add mad mimi connector
            var madMimiConnector = new MadMimiConnector
            {
                Key = "cb1850d75dabe532bc3242751c2cd8b7",
                Username = "andrew.brown@eonesolutions.com"
            };
            connectors.Add(madMimiConnector);

            // add sendloop connector
            var sendloopConnector = new SendloopConnector
            {
                Key = "139f-b63d-19df-4a39-4554-5d86-cc98-bc66",
                SitePrefix = "a291054-6ffe2e"
            };
            connectors.Add(sendloopConnector);

            // add slack connector
            var slackConnector = new SlackConnector
            {
                Token = "xoxp-10968504501-10972678129-13298014050-2fd669e55f"
            };
            connectors.Add(slackConnector);

            // add intacct connector
            var intacctConnector = new IntacctConnector
            {
                Username = "admin",
                Password = "@Popdock1"
            };
            intacctConnector.Companies.Add(new Connectors.ConnectorCompany(1, "eOne", "eOne-DEV"));
            connectors.Add(intacctConnector);

            // add HappyFox connector
            var happyFoxConnector = new HapppyFoxConnector
            {
                Key = "e8492df91c794eb6a2ca28c37cfb5ee9",
                Token = "66bf4655846e425c8026b7f14470fe38"
            };
            connectors.Add(happyFoxConnector);

            return connectors.OrderBy(connector => connector.Name).ToList();

        }

    }
}
