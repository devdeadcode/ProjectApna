using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Formstack.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Formstack
{
    public class FormstackConnector : RestConnector
    {
        
        public FormstackConnector()
        {
            Name = "Formstack";
            Group = ConnectorGroup.Forms;
            BaseUrl = "https://www.formstack.com/api/v2";

            // add rate limit for 14400 requests per day
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 14400, ServiceConnectorRateLimiting.LimitPeriod.Day);
        }
        
        public override void Initialise()
        {
            base.Initialise();
            var forms = DeserializeJson<List<FormstackForm>>(GetResponse("form"));
            foreach (var form in forms)
            {
                var entity = AddEntity(form.id, form.name);
                var fieldData = GetResponse($"form/{form.id}/field");
                var fields = DeserializeJson<List<FormstackForm>>(fieldData);
                foreach (var field in fields)
                {
                    var entityField = new ConnectorField
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

        private static List<Dictionary<string, string>> GetSubmissionDictionaryList(ConnectorEntity entity, IEnumerable<FormstackSubmission> submissions)
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
