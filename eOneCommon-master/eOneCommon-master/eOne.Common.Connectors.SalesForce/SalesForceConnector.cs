using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.SalesForce.Helpers;
using eOne.Common.Connectors.SalesForce.Models;
using eOne.Common.Connectors.SalesForce.Models.ContractResolver;
using eOne.Common.Connectors.SalesForce.Models.Metadata;
using eOne.Common.DataConnectors;
using eOne.Common.DataConnectors.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eOne.Common.Connectors.SalesForce
{
    public class SalesForceConnector : RestConnector
    {

        public const int EntityIdAccount = 1;
        public const int EntityIdCampaign = 2;
        public const int EntityIdCase = 3;
        public const int EntityIdContact = 4;
        public const int EntityIdContract = 5;
        public const int EntityIdLead = 6;
        public const int EntityIdOpportunity = 7;
        public const int EntityIdOrder = 8;
        public const int EntityIdPriceList = 9;
        public const int EntityIdProduct = 10;
        public const int EntityIdSolution = 11;
        public const int EntityIdUser = 12;
        public const int EntityIdAsset = 13;
        public const int EntityIdContractLine = 14;
        public const int EntityIdOpportunityLine = 15;
        public const int EntityIdOrderItem = 16;
        public const int EntityIdQuote = 17;
        public const int EntityIdQuoteLine = 18;
        public const int EntityIdTask = 19;
        public const int EntityIdNote = 20;

        public SalesForceConnector()
        {
            Name = "SalesForce";
            Group = ConnectorGroup.CRM;
            Key = "da73103697355f6cf28c8772cd4f07254ad67817ac00f8d67831523042c2178b";
            Secret = "961ea7228155e6c0d320080c022e81df29d3c6c54fd94deb46bd503feb60befb";
            AuthorizationUri = "https://login.salesforce.com/services/oauth2/authorize";
            AccessTokenUri = "https://login.salesforce.com/services/oauth2/token";
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            CallbackUrl = "http://www.popdock.com/callbacks/salesforce";
            AddSetup();
        }

        public override void Initialise()
        {
            BaseUrl = $"https://{SitePrefix}.salesforce.com/services/data/v20.0";
            AddEntities();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            return $"query/?q={SalesForceQueryHelper.GetSoqlQuery(query)}";
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            var resolver = new SalesForceCustomContractResolver(query.Entity);
            var jobj = (JObject)JsonConvert.DeserializeObject(data);
            var records = jobj["records"].ToString();
            switch (query.Entity.Id)
            {
                case EntityIdAccount:
                    return DeserializeJson<List<SalesForceAccount>>(records, resolver);
                case EntityIdCampaign:
                    return DeserializeJson<List<SalesForceCampaign>>(records, resolver);
                case EntityIdCase:
                    return DeserializeJson<List<SalesForceCase>>(records, resolver);
                case EntityIdContact:
                    return DeserializeJson<List<SalesForceContact>>(records, resolver);
                case EntityIdContract:
                    return DeserializeJson<List<SalesForceContract>>(records, resolver);
                case EntityIdLead:
                    return DeserializeJson<List<SalesForceLead>>(records, resolver);
                case EntityIdOpportunity:
                    return DeserializeJson<List<SalesForceOpportunity>>(records, resolver);
                case EntityIdOrder:
                    return DeserializeJson<List<SalesForceOrder>>(records, resolver);
                case EntityIdPriceList:
                    return DeserializeJson<List<SalesForcePriceList>>(records, resolver);
                case EntityIdProduct:
                    return DeserializeJson<List<SalesForceProduct>>(records, resolver);
                case EntityIdSolution:
                    return DeserializeJson<List<SalesForceSolution>>(records, resolver);
                case EntityIdUser:
                    return DeserializeJson<List<SalesForceUser>>(records, resolver);
                case EntityIdAsset:
                    return DeserializeJson<List<SalesForceAsset>>(records, resolver);
                case EntityIdContractLine:
                    return DeserializeJson<List<SalesForceContractLine>>(records, resolver);
                case EntityIdOpportunityLine:
                    return DeserializeJson<List<SalesForceOpportunityLine>>(records, resolver);
                case EntityIdOrderItem:
                    return DeserializeJson<List<SalesForceOrderItem>>(records, resolver);
                case EntityIdQuote:
                    return DeserializeJson<List<SalesForceQuote>>(records, resolver);
                case EntityIdQuoteLine:
                    return DeserializeJson<List<SalesForceQuoteLine>>(records, resolver);
                case EntityIdTask:
                    return DeserializeJson<List<SalesForceTask>>(records, resolver);
                case EntityIdNote:
                    return DeserializeJson<List<SalesForceNote>>(records, resolver);
                default:
                    return DeserializeJson<List<SalesForceCustomEntity>>(records, resolver);
            }
        }

        private void AddEntities()
        {

            var salesGroup = AddGroup(1, "Sales");
            var supportGroup = AddGroup(2, "Support");
            var userGroup = AddGroup(3, "Users");
            var marketingGroup = AddGroup(4, "Marketing");
            var chatterGroup = AddGroup(5, "Chatter");

            AddEntity(EntityIdAccount, "Accounts", "Account", typeof(SalesForceAccount), salesGroup);
            AddEntity(EntityIdCampaign, "Campaigns", "Campaign", typeof(SalesForceCampaign), marketingGroup);
            AddEntity(EntityIdCase, "Cases", "Case", typeof(SalesForceCase), supportGroup);
            AddEntity(EntityIdContact, "Contacts", "Contact", typeof(SalesForceContact), salesGroup);
            AddEntity(EntityIdContract, "Contracts", "Contract", typeof(SalesForceContract), salesGroup);
            AddEntity(EntityIdLead, "Leads", "Lead", typeof(SalesForceLead), marketingGroup);
            AddEntity(EntityIdOpportunity, "Opportunities", "Opportunity", typeof(SalesForceOpportunity), marketingGroup);
            AddEntity(EntityIdOrder, "Orders", "Order", typeof(SalesForceOrder), salesGroup);
            AddEntity(EntityIdPriceList, "Price lists", "Pricebook2", typeof(SalesForcePriceList), salesGroup);
            AddEntity(EntityIdProduct, "Products", "Product2", typeof(SalesForceProduct), salesGroup);
            AddEntity(EntityIdSolution, "Solutions", "Solution", typeof(SalesForceSolution), supportGroup);
            AddEntity(EntityIdUser, "Users", "User", typeof(SalesForceUser), userGroup);
            AddEntity(EntityIdAsset, "Assets", "Asset", typeof(SalesForceAsset), supportGroup);
            AddEntity(EntityIdContractLine, "Contract lines", "ContractLineItem", typeof(SalesForceContractLine), salesGroup);
            AddEntity(EntityIdOpportunityLine, "Opportunity lines", "OpportunityLineItem", typeof(SalesForceOpportunityLine), marketingGroup);
            AddEntity(EntityIdOrderItem, "Order items", "OrderItem", typeof(SalesForceOrderItem), salesGroup);
            AddEntity(EntityIdQuote, "Quotes", "Quote", typeof(SalesForceQuote), salesGroup);
            AddEntity(EntityIdQuoteLine, "Quote lines", "QuoteLineItem", typeof(SalesForceQuoteLine), salesGroup);
            AddEntity(EntityIdTask, "Tasks", "Task", typeof(SalesForceTask), userGroup);
            AddEntity(EntityIdNote, "Notes", "Note", typeof(SalesForceNote), salesGroup);

            AddCustomEntities();
            foreach (var entity in Entities)
            {
                AddCustomProperties(entity);
                entity.DefaultMaxRecords = 1000;
            }

            // todo - add relationships
            // todo - add favorites
        }

        private void AddEntity(int id, string name, string endpoint, Type model, DataConnectorEntityGroup group)
        {
            var entity = AddEntity(id, name, model);
            entity.Endpoint = endpoint;
            entity.Group = group;
        }

        private void AddCustomEntities()
        {
            var objectJson = GetResponse("sobjects");
            var salesforceObjects = DeserializeJson<SalesForceObjectResponse>(objectJson);
            if (salesforceObjects == null) return;

            var customGroup = AddGroup(100, "Custom");

            var entityId = 100;
            foreach (var salesforceObject in salesforceObjects.sobjects.Where(salesforceObject => salesforceObject.custom))
            {
                AddEntity(entityId, GetValidDisplayName(salesforceObject.labelPlural), salesforceObject.name, typeof(SalesForceCustomEntity), customGroup);
                entityId++;
            }
        }

        private void AddCustomProperties(DataConnectorEntity entity)
        {
            var objectJson = GetResponse($"sobjects/{entity.Endpoint}/describe");
            var salesforceObject = DeserializeJson<SalesForceObject>(objectJson);
            if (salesforceObject != null)
            {
                var fieldId = entity.GetNextFieldId();
                var fieldNumber = 1;
                foreach (var field in salesforceObject.fields)
                {
                    if (field.custom)
                    {
                        var entityField = SalesForceMetadataHelper.GetEntityField(entity, field, fieldId, fieldNumber);
                        entity.Fields.Add(entityField);
                        fieldId++;
                        fieldNumber++;
                    }
                    // set custom display name for Name field
                    if (field.name == "Name" && salesforceObject.custom)
                    {
                        var nameField = entity.Fields.FirstOrDefault(entityField => entityField.Name == "Name");
                        if (nameField != null) nameField.DisplayName = GetValidDisplayName(field.label);
                    }
                }
            }
        }

    }
}
