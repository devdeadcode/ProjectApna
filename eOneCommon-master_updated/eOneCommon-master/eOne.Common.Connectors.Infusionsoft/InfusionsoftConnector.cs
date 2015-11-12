using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Infusionsoft.Models;
using eOne.Common.Connectors.Infusionsoft.Models.Query;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Infusionsoft
{
    public class InfusionsoftConnector : XmlRpcConnector
    {

        public const int EntityIdContact = 1;
        public const int EntityIdCompany = 2;
        public const int EntityIdContactAction = 3;
        public const int EntityIdUser = 4;
        public const int EntityIdOpportunity = 5;
        public const int EntityIdProduct = 6;
        public const int EntityIdReferralPartner = 7;

        public InfusionsoftConnector()
        {
            Name = "Infusionsoft";
            Group = ConnectorGroup.CRM;
            Key = "ev3mxj53cbbnghh39jazf8p2";
            Secret = "Y7PUuaMQJu";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            AuthorizationUri = "https://signin.infusionsoft.com/app/oauth/authorize";
            AccessTokenUri = "https://api.infusionsoft.com/token";
            CallbackUrl = "http://www.popdock.com/callback/infusionsoft";
            BaseUrl = "https://api.infusionsoft.com/crm/xmlrpc/v1";
            XmlEncoding = "UTF-8";
        }

        public override void Initialise()
        {
            base.Initialise();

            var contactEntity = AddEntity(EntityIdContact, "Contacts", typeof(InfusionsoftContact));
            var opportunityEntity = AddEntity(EntityIdOpportunity, "Opportunities", typeof(InfusionsoftOpportunity));
            var userEntity = AddEntity(EntityIdUser, "Users", typeof(InfusionsoftUser));
            var companyEntity = AddEntity(EntityIdCompany, "Companies", typeof(InfusionsoftCompany));
            var productEntity = AddEntity(EntityIdProduct, "Products", typeof(InfusionsoftProduct));
            var referralPartnerEntity = AddEntity(EntityIdReferralPartner, "Referral partners", typeof(InfusionsoftAffiliate));

            contactEntity.AddRelatedEntity("Opportunities", opportunityEntity, "Id", "ContactID");
            userEntity.AddRelatedEntity("Opportunities", opportunityEntity, "Id", "UserID");
            referralPartnerEntity.AddRelatedEntity("Opportunities", opportunityEntity, "Id", "AffiliateID");

            foreach (var entity in Entities) entity.DefaultMaxRecords = 1000;
        }

        public override string GetRequestMethod(ConnectorQuery query)
        {
            return "DataService.query";
        }

        public override object GetRequestParameters(ConnectorQuery query)
        {
            var queryParameters = new InfusionSoftDataServiceQuery
            {
                privateKey = Key,
                limit = query.MaxRecords,
                page = 0
            };
            foreach (var field in query.FieldNamesUsed) queryParameters.selectedFields.Add(field);
            switch (query.Entity.Id)
            {
                case EntityIdContact:
                    queryParameters.table = "Contact";
                    var queryContact = new InfusionsoftContact();
                    SetQueryValues(query, queryContact, "Company");
                    queryParameters.queryData = queryContact;
                    break;
                case EntityIdOpportunity:
                    queryParameters.table = "Lead";
                    var queryOpportunity = new InfusionsoftOpportunity();
                    SetQueryValues(query, queryOpportunity, "OpportunityTitle");
                    queryParameters.queryData = queryOpportunity;
                    break;
                case EntityIdUser:
                    queryParameters.table = "User";
                    var queryUser = new InfusionsoftUser();
                    SetQueryValues(query, queryUser, "City");
                    queryParameters.queryData = queryUser;
                    break;
                case EntityIdCompany:
                    queryParameters.table = "Company";
                    var queryCompany = new InfusionsoftCompany();
                    SetQueryValues(query, queryCompany, "City");
                    queryParameters.queryData = queryCompany;
                    break;
                case EntityIdProduct:
                    queryParameters.table = "Product";
                    var queryProduct = new InfusionsoftProduct();
                    SetQueryValues(query, queryProduct, "ProductName");
                    queryParameters.queryData = queryProduct;
                    break;
            }
            return queryParameters;
        }

        public override string GetActionMethod(ConnectorAction action)
        {
            throw new NotImplementedException();
        }

        public override object GetActionParameters(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        private static void SetQueryValues(ConnectorQuery query, object queryObject, string defaultField)
        {
            var noFilter = true;
            if (!query.HasOrConjunctives)
            {
                foreach (var restriction in query.Restrictions)
                {
                    if (restriction.FieldType == ConnectorRestriction.ConnectorRestrictionFieldType.Field && !restriction.Field.IsCalculation)
                    {
                        var propertyInfo = queryObject.GetType().GetProperty(restriction.Field.Name);
                        switch (restriction.Field.Type.Id)
                        {
                            case FieldTypeIdString:
                                switch (restriction.RestrictionType)
                                {
                                    case ConnectorRestriction.ConnectorRestrictionType.Equals:
                                        propertyInfo.SetValue(queryObject, restriction.Values[0].Constant);
                                        noFilter = false;
                                        break;
                                    case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                                        propertyInfo.SetValue(queryObject, $"{restriction.Values[0].Constant}%");
                                        noFilter = false;
                                        break;
                                    case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                                        propertyInfo.SetValue(queryObject, $"%{restriction.Values[0].Constant}");
                                        noFilter = false;
                                        break;
                                    case ConnectorRestriction.ConnectorRestrictionType.Contains:
                                        propertyInfo.SetValue(queryObject, $"%{restriction.Values[0].Constant}%");
                                        noFilter = false;
                                        break;
                                }
                                break;
                            case FieldTypeIdInteger:
                                propertyInfo.SetValue(queryObject, int.Parse(restriction.Values[0].Constant));
                                noFilter = false;
                                break;
                            case FieldTypeIdCurrency:
                            case FieldTypeIdQuantity:
                            case FieldTypeIdPercentage:
                                propertyInfo.SetValue(queryObject, decimal.Parse(restriction.Values[0].Constant));
                                noFilter = false;
                                break;
                            case FieldTypeIdDate:
                                // todo - handle date values
                                noFilter = false;
                                break;
                        }
                    }
                }
            }
            if (noFilter)
            {
                var propertyInfo = queryObject.GetType().GetProperty(defaultField);
                propertyInfo.SetValue(queryObject, "%");
            }
        }

    }
}
