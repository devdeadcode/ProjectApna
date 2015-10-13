using System;
using System.Collections.Generic;
using eOne.Common.Connectors.Formstack.Models;
using eOne.Common.DataConnectors;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.Formstack
{
    public class FormstackConnector : RestConnector
    {
        
        public FormstackConnector()
        {
            Name = "Formstack";
            Group = ConnectorGroup.Forms;
            BaseUrl = "https://www.formstack.com/api/v2";
            var dailyRateLimit = new RestConnectorRateLimiting
            {
                AppliedTo = RestConnectorRateLimiting.LimitAppliedTo.Account,
                NumberOfPeriods = 1,
                Period = RestConnectorRateLimiting.LimitPeriod.Day,
                Requests = 14400
            };
            RateLimits.Add(dailyRateLimit);
        }
        
        public override void Initialise()
        {
            var forms = DeserializeJson<List<FormstackForm>>(GetResponse("form"));
            foreach (var form in forms)
            {
                var entity = AddEntity(form.id, form.name);
                var fieldData = GetResponse($"form/{form.id}/field");
                var fields = DeserializeJson<List<FormstackForm>>(fieldData);
                foreach (var field in fields)
                {
                    var entityField = new DataConnectorField
                    {
                        Name = field.name,
                        Id = field.id,
                        Type = FindFieldType(FieldTypeIdString)
                    };
                    entity.Fields.Add(entityField);
                }
            }
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            return $"form/{query.Entity.Id}/submission?data=true";
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            var submissions = DeserializeJson<List<FormstackSubmission>>(data);
            return GetSubmissionDictionaryList(query.Entity, submissions);
        }

        private static IEnumerable<Dictionary<string, string>> GetSubmissionDictionaryList(DataConnectorEntity entity, IEnumerable<FormstackSubmission> submissions)
        {
            var list = new List<Dictionary<string, string>>();
            foreach (var submission in submissions)
            {
                var dictionary = new Dictionary<string, string>();
                foreach (var field in submission.data)
                {
                    var entityField = entity.FindField(field.field);
                    dictionary.Add(entityField.Name, field.value);
                }
                list.Add(dictionary);
            }
            return list;
        }

    }
}
