using System.Collections.Generic;
using eOne.Common.Connectors.DynamicsGp;
using eOne.Common.Connectors.Insightly;
using eOne.Common.Connectors.MailChimp;
using eOne.Common.Connectors.SalesForce;
using eOne.Common.Connectors.Shopify;
using eOne.Common.Connectors.Timely;
using eOne.Common.Connectors.Trello;
using eOne.Common.Connectors.HappyFox;
using eOne.Common.Connectors.GoogleAnalytics;
using eOne.Common.DataConnectors;
using eOne.Common.Connectors.BaseCamp;

namespace eOne.Common.SampleConnectors
{
    public class SampleConnectors
    {

        public static List<DataConnector> GetConnectors()
        {

            var connectors = new List<DataConnector>();

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
                Token = "0ab787771c52f7a1bd0b82bd5860fd7f",
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
                Token = "00D280000011Crg!ARoAQIJuUbME1zLazhUWrTfL9RWX7ycWsqcPdncrrka8JAYI5bMV3kv56E3RUMkKsqDxvtkTRLEqADS3bV8W3aSTrlmo8gCt",
                SitePrefix = "ap2"
            };
            connectors.Add(salesforceConnector);

            // add HappyFox connector
            var happyFoxConnector = new HapppyFoxConnector
            {
                Key = "e8492df91c794eb6a2ca28c37cfb5ee9",
                Token = "66bf4655846e425c8026b7f14470fe38"
            };
            connectors.Add(happyFoxConnector);

            //add GoogleAnalytics connector
            var googleAnalyticsConnector = new GoogleAnalyticsConnector
            {
                Key = "1074745083580-j088f2g8bj4h5cssl5kqr8hkf2k2s324.apps.googleusercontent.com",
                Secret = "yzf3lb5frhf487ODoEMY8cM-"
            };
            connectors.Add(googleAnalyticsConnector);

            //add BaseCamp connector
            var basecampConnector = new BaseCampConnector
            {
                Key = "",
                Secret = ""
            };
            connectors.Add(basecampConnector);


            return connectors;

        }
    }
}
