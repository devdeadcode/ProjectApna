using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.SalesForce.Helpers;
using eOne.Common.Connectors.SalesForce.Models;
using eOne.Common.Connectors.SalesForce.Models.Metadata;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eOne.Common.Connectors.SalesForce
{
    public class SalesForceConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

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
        public const int EntityIdTask = 17;
        public const int EntityIdNote = 18;
        public const int EntityIdIdea = 19;
        public const int EntityIdIdeaComment = 20;
        public const int EntityIdEvent = 21;

        #endregion

        #region Action IDs

        public const int ActionIdDelete = 1;
        public const int ActionIdUpdateEmail = 2;
        public const int ActionIdUpdatePhone = 3;
        public const int ActionIdUpdateAddress = 4;

        #endregion

        #endregion

        public SalesForceConnector()
        {
            Name = "SalesForce";
            Group = ConnectorGroup.CRM;

            Key = "3MVG9ZL0ppGP5UrBCLiKk9AeUGhxs7yiGhIJiWFzK19XcOgn9oNcyp2mOoqD7aIw53uIG9ffu_xQuxwYrMTbC";
            Secret = "8438895233972733050";
            AuthorizationUri = "https://login.salesforce.com/services/oauth2/authorize";
            AccessTokenUri = "https://login.salesforce.com/services/oauth2/token";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            CallbackUrl = "http://www.popdock.com/callbacks/salesforce";

            AddSetup();
        }

        public override void Initialise()
        {
            base.Initialise();

            BaseUrl = $"https://{SitePrefix}.salesforce.com/services/data/v29.0";

            // get rate limits 
            var limitsJson = GetResponse("limits");
            var limits = DeserializeJson<SalesForceLimits>(limitsJson);
            if (limits?.DailyApiRequests != null) AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, limits.DailyApiRequests.Max, ServiceConnectorRateLimiting.LimitPeriod.Day);

            AddEntities();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            return query.IsSearch ? $"search/?q={SalesForceQueryHelper.GetSoslQuery(query)}" : $"query/?q={SalesForceQueryHelper.GetSoqlQuery(query)}";
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            if (query.IsSearch)
            {
                data = "{ \"records\":" + data + "}";
                // remove search restrictions so they don't get filtered twice - still need to keep the exclusions as salesforce search doesn't handle these
                var excludeRestrictions = query.Restrictions.Where(restriction => restriction.ConjunctiveOperator == ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And).ToList();
                query.Restrictions = excludeRestrictions;
                query.IsSearch = false;
            }
            var jobj = (JObject)JsonConvert.DeserializeObject(data);
            var records = jobj["records"].ToString();
            var objects = DeserializeObjects(query.Entity.Id, records, query);
            var objectList = objects as IList<object> ?? objects.ToList();

            // assign users
            if (query.Entity.Id != EntityIdUser)
            {
                var users = GetUsers();
                foreach (SalesForceEntity entity in objectList)
                {
                    entity.LastModifiedBy = FindUser(users, entity.LastModifiedById);
                    entity.CreatedBy = FindUser(users, entity.CreatedById);
                    entity.Owner = FindUser(users, entity.OwnerId);
                }
            }
            return objectList;
        }

        private List<SalesForceUser> GetUsers()
        {
            const string endpoint = "query/?q=select Id, Username, Name, CompanyName, Email from User";
            var userData = GetResponse(endpoint);
            var jobj = (JObject)JsonConvert.DeserializeObject(userData);
            return DeserializeJson<List<SalesForceUser>>(jobj["records"].ToString());
        }
        private static SalesForceUser FindUser(IEnumerable<SalesForceUser> users, string userId)
        {
            return string.IsNullOrWhiteSpace(userId) ? null : users.FirstOrDefault(user => user.Id == userId);
        }

        private static IEnumerable<object> DeserializeObjects(int entityId, string records, ConnectorQuery query)
        {
            switch (entityId)
            {
                case EntityIdAccount:
                    return DeserializeJson<List<SalesForceAccount>>(records, query);
                case EntityIdCampaign:
                    return DeserializeJson<List<SalesForceCampaign>>(records, query);
                case EntityIdCase:
                    return DeserializeJson<List<SalesForceCase>>(records, query);
                case EntityIdContact:
                    return DeserializeJson<List<SalesForceContact>>(records, query);
                case EntityIdContract:
                    return DeserializeJson<List<SalesForceContract>>(records, query);
                case EntityIdLead:
                    return DeserializeJson<List<SalesForceLead>>(records, query);
                case EntityIdOpportunity:
                    return DeserializeJson<List<SalesForceOpportunity>>(records, query);
                case EntityIdOrder:
                    return DeserializeJson<List<SalesForceOrder>>(records, query);
                case EntityIdPriceList:
                    return DeserializeJson<List<SalesForcePriceList>>(records, query);
                case EntityIdProduct:
                    return DeserializeJson<List<SalesForceProduct>>(records, query);
                case EntityIdSolution:
                    return DeserializeJson<List<SalesForceSolution>>(records, query);
                case EntityIdUser:
                    return DeserializeJson<List<SalesForceUser>>(records, query);
                case EntityIdAsset:
                    return DeserializeJson<List<SalesForceAsset>>(records, query);
                case EntityIdContractLine:
                    return DeserializeJson<List<SalesForceContractLine>>(records, query);
                case EntityIdOpportunityLine:
                    return DeserializeJson<List<SalesForceOpportunityLine>>(records, query);
                case EntityIdOrderItem:
                    return DeserializeJson<List<SalesForceOrderItem>>(records, query);
                case EntityIdTask:
                    return DeserializeJson<List<SalesForceTask>>(records, query);
                case EntityIdNote:
                    return DeserializeJson<List<SalesForceNote>>(records, query);
                case EntityIdIdea:
                    return DeserializeJson<List<SalesForceIdea>>(records, query);
                case EntityIdIdeaComment:
                    return DeserializeJson<List<SalesForceIdeaComment>>(records, query);
                default:
                    return DeserializeJson<List<SalesForceCustomEntity>>(records, query);
            }
        }

        private void AddEntities()
        {

            var salesGroup = AddGroup(1, "Sales");
            var supportGroup = AddGroup(2, "Support");
            var userGroup = AddGroup(3, "Users");
            var marketingGroup = AddGroup(4, "Marketing");
            //var chatterGroup = AddGroup(5, "Chatter");

            var accountEntity = AddEntity(EntityIdAccount, "Accounts", "Account", typeof(SalesForceAccount), salesGroup);
            var campaignEntity = AddEntity(EntityIdCampaign, "Campaigns", "Campaign", typeof(SalesForceCampaign), marketingGroup);
            var caseEntity = AddEntity(EntityIdCase, "Cases", "Case", typeof(SalesForceCase), supportGroup);
            var contactEntity = AddEntity(EntityIdContact, "Contacts", "Contact", typeof(SalesForceContact), salesGroup);
            var contractEntity = AddEntity(EntityIdContract, "Contracts", "Contract", typeof(SalesForceContract), salesGroup);
            var leadEntity = AddEntity(EntityIdLead, "Leads", "Lead", typeof(SalesForceLead), marketingGroup);
            var opportunityEntity = AddEntity(EntityIdOpportunity, "Opportunities", "Opportunity", typeof(SalesForceOpportunity), marketingGroup);
            var orderEntity = AddEntity(EntityIdOrder, "Orders", "Order", typeof(SalesForceOrder), salesGroup);
            var priceListEntity = AddEntity(EntityIdPriceList, "Price lists", "Pricebook2", typeof(SalesForcePriceList), salesGroup);
            var productEntity = AddEntity(EntityIdProduct, "Products", "Product2", typeof(SalesForceProduct), salesGroup);
            var solutionEntity = AddEntity(EntityIdSolution, "Solutions", "Solution", typeof(SalesForceSolution), supportGroup);
            var userEntity = AddEntity(EntityIdUser, "Users", "User", typeof(SalesForceUser), userGroup);
            var assetEntity = AddEntity(EntityIdAsset, "Assets", "Asset", typeof(SalesForceAsset), supportGroup);
            //var contractLineEntity = AddEntity(EntityIdContractLine, "Contract lines", "ContractLineItem", typeof(SalesForceContractLine), salesGroup);
            //var opportunityLineEntity = AddEntity(EntityIdOpportunityLine, "Opportunity lines", "OpportunityLineItem", typeof(SalesForceOpportunityLine), marketingGroup);
            //var orderLineEntity = AddEntity(EntityIdOrderItem, "Order items", "OrderItem", typeof(SalesForceOrderItem), salesGroup);
            var taskEntity = AddEntity(EntityIdTask, "Tasks", "Task", typeof(SalesForceTask), userGroup);
            var noteEntity = AddEntity(EntityIdNote, "Notes", "Note", typeof(SalesForceNote), salesGroup);
            var ideaEntity = AddEntity(EntityIdIdea, "Ideas", "Idea", typeof(SalesForceIdea), supportGroup);
            var ideaCommentEntity = AddEntity(EntityIdIdeaComment, "Idea comments", "IdeaComment", typeof(SalesForceIdeaComment), supportGroup);
            var eventEntity = AddEntity(EntityIdEvent, "Events", "Event", typeof(SalesForceEvent), userGroup);

            AddCustomEntities();
            foreach (var entity in Entities)
            {
                var salesforceObject = GetSalesForceObjectDefinition(entity.Endpoint);
                AddCustomProperties(entity, salesforceObject);
                AddDefaultActions(entity, salesforceObject);
                if (AttachNotes(entity.Id)) entity.AddRelatedEntity("Notes", noteEntity, "Id", "ParentId");
                entity.DefaultMaxRecords = 1000;
            }

            // add relationships
            accountEntity.AddRelatedEntity("Contacts", contactEntity, "Id", "AccountId");
            accountEntity.AddRelatedEntity("Contracts", contractEntity, "Id", "AccountId");
            accountEntity.AddRelatedEntity("Opportunities", opportunityEntity, "Id", "AccountId");
            accountEntity.AddRelatedEntity("Orders", orderEntity, "Id", "AccountId");
            accountEntity.AddRelatedEntity("Assets", assetEntity, "Id", "AccountId");
            accountEntity.AddRelatedEntity("Tasks", taskEntity, "Id", "AccountId");
            accountEntity.AddRelatedEntity("Tasks", taskEntity, "Id", "AccountId");
            contactEntity.AddRelatedEntity("Events", eventEntity, "Id", "ContactId");
            productEntity.AddRelatedEntity("Assets", assetEntity, "Id", "Product2Id");
            campaignEntity.AddRelatedEntity("Opportunities", opportunityEntity, "Id", "CampaignId");
            contractEntity.AddRelatedEntity("Orders", orderEntity, "Id", "ContractId");
            ideaEntity.AddRelatedEntity("Comments", ideaCommentEntity, "Id", "IdeaId");

            // add favorites
            FavoriteHelper.AddLeadFavorites(leadEntity);
            FavoriteHelper.AddCaseFavorites(caseEntity);
            FavoriteHelper.AddOpportunityFavorites(opportunityEntity);
            FavoriteHelper.AddOrderFavorites(orderEntity);
            FavoriteHelper.AddSolutionFavorites(solutionEntity);

            // todo - add actions

        }

        private ConnectorEntity AddEntity(int id, string name, string endpoint, Type model, ConnectorEntityGroup group)
        {
            var entity = AddEntity(id, name, model);
            entity.Endpoint = endpoint;
            entity.Group = group;
            return entity;
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

        private SalesForceObject GetSalesForceObjectDefinition(string endpoint)
        {
            var objectJson = GetResponse($"sobjects/{endpoint}/describe");
            return DeserializeJson<SalesForceObject>(objectJson);
        }

        private static void AddCustomProperties(ConnectorEntity entity, SalesForceObject salesforceObject)
        {
            if (salesforceObject == null) return;

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
                    if (nameField != null)
                    {
                        nameField.DisplayName = GetValidDisplayName(field.label);
                        nameField.DisplayString = true;
                    }
                }
            }
        }

        private static void AddDefaultActions(ConnectorEntity entity, SalesForceObject salesforceObject)
        {
            if (salesforceObject == null) return;

            entity.AddDeleteAction(ActionIdDelete, $"Delete {salesforceObject.label}", $"Are you sure you want to delete this {salesforceObject.label}?");

            var emailFields = entity.FindFieldsByType(FieldTypeIdEmail);
            if (emailFields.Count == 1) entity.AddUpdateFieldsAction(ActionIdUpdateEmail, "Update email", emailFields[0].Name);

            var phoneFields = entity.FindFieldsByType(FieldTypeIdPhone);
            if (phoneFields.Count == 1) entity.AddUpdateFieldsAction(ActionIdUpdatePhone, "Update phone", phoneFields[0].Name);
        }

        private static bool AttachNotes(int entityId)
        {
            switch (entityId)
            {
                case EntityIdNote:
                case EntityIdUser:
                case EntityIdSolution:
                case EntityIdIdea:
                case EntityIdIdeaComment:
                    return false;
                default:
                    return true;
            }
        }

    }
}
