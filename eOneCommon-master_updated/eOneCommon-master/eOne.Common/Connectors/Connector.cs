using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using eOne.Common.Actions;
using eOne.Common.Query;
using eOne.Common.Setup;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace eOne.Common.Connectors
{
    
    /// <summary>
    /// Base class for connectors that return data
    /// </summary>
    public abstract class Connector
    {

        #region Constants

        public const int FieldTypeIdString = 1;
        public const int FieldTypeIdDate = 2;
        public const int FieldTypeIdCurrency = 3;
        public const int FieldTypeIdInteger = 4;
        public const int FieldTypeIdPhone = 5;
        public const int FieldTypeIdEnum = 6;
        public const int FieldTypeIdPercentage = 7;
        public const int FieldTypeIdYesNo = 8;
        public const int FieldTypeIdTime = 9;
        public const int FieldTypeIdQuantity = 10;
        public const int FieldTypeIdEmail = 11;
        public const int FieldTypeIdUrl = 12;
        public const int FieldTypeIdImage = 13;
        public const int FieldTypeIdSkype = 14;
        public const int FieldTypeIdTwitter = 15;
        public const int FieldTypeIdColor = 16;
        public const int FieldTypeIdAddress = 17;

        #endregion

        #region Enums

        public enum ConnectorSummaryMethod { Count, Sum, Maximum, Minimum, Average }
        public enum ConnectorSerializationType { Xml, Json, Csv }

        #endregion

        protected Connector()
        {
            FieldTypes = new List<ConnectorFieldType>();
            Entities = new List<ConnectorEntity>();
            Companies = new List<ConnectorCompany>();
            DefaultFavorites = new List<ConnectorQuery>();
            Groups = new List<ConnectorEntityGroup>();
            Actions = new List<ConnectorAction>();
            Setup = new ConnectorSetup();
            AddFieldTypes();
        }

        #region Enums

        public enum ConnectorAuthorisationType
        {
            Company,
            User,
            Personal
        }

        public enum ConnectorGroup
        {
            ERP,
            CRM,
            Helpdesk,
            MailingList,
            Invoicing,
            Database,
            Forms,
            IssueTracking,
            LandingPage,
            Payments,
            Payroll,
            POS,
            ProjectManagement,
            ToDoList,
            SocialMedia,
            TimeTracking,
            WebStore,
            Chat,
            Other
        }

        #endregion

        #region Properties

        public int Id;
        public string Name;
        public ConnectorGroup Group;
        public ConnectorGroup SecondaryGroup;
        public string Username { get; set; }
        public string Password { get; set; }
        public ConnectorSetup Setup { get; set; }
        public ConnectorAuthorisationType AuthorisationType { get; set; }
        public List<ConnectorFieldType> FieldTypes { get; set; }
        public List<ConnectorEntityGroup> Groups { get; set; }
        public List<ConnectorEntity> Entities { get; set; }
        public List<ConnectorCompany> Companies { get; set; }
        public bool Multicompany { get; set; }
        public string CompanyPrompt { get; set; }
        public string CompanyPluralPrompt { get; set; }
        public List<ConnectorQuery> DefaultFavorites { get; set; }
        public List<string> Tags { get; set; }
        public List<ConnectorAction> Actions { get; set; }

        #endregion

        #region Methods

        public virtual void Initialise()
        {
            Entities.Clear();
            Groups.Clear();
            Companies.Clear();
            DefaultFavorites.Clear();
        }

        public void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "Please enter in a name for your new connector below. ",
                BottomDescription = $"Click Next to grant access to your {Name} account."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, Name, true));
            Setup.Steps.Add(step1);
            var step2 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 2,
                Header = "Complete installation",
                BottomDescription = "Click Finish to complete the installation."
            };
            Setup.Steps.Add(step2);
        }

        public override string ToString()
        {
            return Name;
        }

        public void RunAction(int entityId, int actionId, List<Tuple<string, string>> parameters)
        {
            var entity = FindEntity(entityId);
            var action = entity.FindAction(actionId);
            if (action == null) return;
            RunAction(action, parameters);
        }
        public abstract void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters);

        public abstract IEnumerable<object> GetRecords(ConnectorQuery query);
        public virtual string GetData(string queryXml, ConnectorSerializationType type)
        {
            var query = new ConnectorQuery(this, queryXml);
            return Serialize(query, GetRecords(query), type);
        }
        public virtual string GetData(ConnectorQuery query, ConnectorSerializationType type)
        {
            return Serialize(query, GetRecords(query), type);
        }
        public virtual string Search(string queryString, int entityId, int companyId, ConnectorSerializationType type)
        {
            if (String.IsNullOrWhiteSpace(queryString)) return String.Empty;
            var search = new ConnectorSearch { Connector = this, Query = queryString, Entity = FindEntity(entityId) };
            search.Companies.Add(FindCompany(companyId));
            var query = new ConnectorQuery(search);
            return GetData(query, type);
        }
        public virtual string GetSummary(string queryXml, ConnectorSummaryMethod summaryMethod, ConnectorField summaryField = null)
        {
            var query = new ConnectorQuery(this, queryXml);
            var records = GetRecords(query);
            return GetSummary(records, query.Entity.RecordType, summaryMethod, summaryField).ToString();
        }

        public ConnectorAction AddAction(int id, string name, int fieldTypeId)
        {
            var action = new ConnectorAction(id, name) { BaseConnector = this };
            var fieldType = FindFieldType(fieldTypeId);
            if (fieldType != null)
            {
                var parameter = action.AddParameter(fieldType.Name, fieldType);
                parameter.Required = true;
            }
            Actions.Add(action);
            return action;
        }
        public ConnectorEntityGroup AddGroup(int id, string name)
        {
            var group = new ConnectorEntityGroup(id, name);
            group.ParentConnector = this;
            Groups.Add(group);
            return group;
        }
        public ConnectorEntity AddEntity()
        {
            var entity = new ConnectorEntity { ParentConnector = this };
            Entities.Add(entity);
            return entity;
        }
        public ConnectorEntity AddEntity(int id, string name)
        {
            var entity = new ConnectorEntity(id, name, this);
            Entities.Add(entity);
            return entity;
        }
        public ConnectorEntity AddEntity(int id, string name, Type model, bool cached = false)
        {
            var entity = new ConnectorEntity(id, name, model, this, cached);
            Entities.Add(entity);
            return entity;
        }
        public ConnectorCompany AddCompany()
        {
            var company = new ConnectorCompany { ParentConnector = this };
            Companies.Add(company);
            return company;
        }
        public ConnectorCompany AddCompany(int id, string name, string databaseName = "")
        {
            var company = new ConnectorCompany(id, name, databaseName) { ParentConnector = this };
            Companies.Add(company);
            return company;
        }
        public ConnectorEntity FindEntity(int entityId)
        {
            return Entities?.FirstOrDefault(entity => entity.Id == entityId);
        }
        public ConnectorCompany FindCompany(int companyId)
        {
            return Multicompany ? Companies?.FirstOrDefault(company => company.Id == companyId) : null;
        }

        public ConnectorFieldType FindFieldType(int fieldTypeId)
        {
            return FieldTypes?.FirstOrDefault(fieldType => fieldType.Id == fieldTypeId);
        }
        public static string FindParameterValue(IEnumerable<Tuple<string, string>> parameters, string name)
        {
            foreach (var parameter in parameters.Where(parameter => parameter.Item1 == name)) return parameter.Item2;
            return String.Empty;
        }
        public static string GetValidDisplayName(string name)
        {
            var words = name.Split(' ');
            var validWords = words.Select(word => word.ToUpper() == word ? word : word.ToLower()).ToList();
            validWords[0] = validWords[0].First().ToString().ToUpper() + validWords[0].Substring(1);
            return String.Join(" ", validWords);
        }

        #endregion

        #region Helpers

        private static object GetSummary(IEnumerable<object> records, Type type, ConnectorSummaryMethod method, ConnectorField field)
        {
            if (method == ConnectorSummaryMethod.Count) return records.Count();
            if (field == null) return 0;
            var propertyInfo = type.GetProperty(field.Name);
            switch (method)
            {
                case ConnectorSummaryMethod.Average:
                    return GetSummaryAverage(records, propertyInfo);
                case ConnectorSummaryMethod.Maximum:
                    return records.Max(record => record.GetType().GetProperty(field.Name).GetValue(record, null));
                case ConnectorSummaryMethod.Minimum:
                    return records.Min(record => record.GetType().GetProperty(field.Name).GetValue(record, null));
                case ConnectorSummaryMethod.Sum:
                    return GetSummarySum(records, propertyInfo);
            }
            return 0;
        }
        private static object GetSummarySum(IEnumerable<object> records, PropertyInfo propertyInfo)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "decimal":
                    return records.Sum(record => (decimal)propertyInfo.GetValue(record, null));
                case "short":
                case "int":
                case "long":
                case "ushort":
                case "uint":
                case "ulong":
                    return records.Sum(record => (long)propertyInfo.GetValue(record, null));
            }
            return 0;
        }
        private static object GetSummaryAverage(IEnumerable<object> records, PropertyInfo propertyInfo)
        {
            var objects = records as IList<object> ?? records.ToList();
            var enumerable = records as IList<object> ?? objects.ToList();
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "decimal":
                    return (decimal)GetSummarySum(enumerable, propertyInfo) / enumerable.Count;
                case "short":
                case "int":
                case "long":
                case "ushort":
                case "uint":
                case "ulong":
                    return (long)GetSummarySum(objects, propertyInfo) / enumerable.Count;
            }
            return 0;
        }
        public string Serialize(ConnectorQuery query, IEnumerable<object> records, ConnectorSerializationType type)
        {
            if (records == null) return String.Empty;
            switch (type)
            {
                case ConnectorSerializationType.Xml:
                    return SerializeXml(query, records);
                case ConnectorSerializationType.Json:
                    return SerializeJson(query, records);
                case ConnectorSerializationType.Csv:
                    return SerializeCsv(query, records);
            }
            return String.Empty;
        }

        private static string SerializeXml(ConnectorQuery query, IEnumerable<object> records)
        {
            if (records == null || query?.Fields == null || query.Fields.Count == 0) return String.Empty;
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var entityName = query.Entity.XmlName;
            using (var writer = XmlWriter.Create(sw))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement($"ArrayOf{entityName}");
                foreach (var record in records)
                {
                    writer.WriteStartElement(entityName);
                    var recordType = record.GetType();
                    foreach (var field in query.Fields)
                    {
                        var propertyInfo = recordType.GetProperty(field.Name);
                        var value = propertyInfo?.GetValue(record);
                        writer.WriteElementString(field.XmlName, GetFieldValue(value, field, ConnectorSerializationType.Xml));
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            var output = sw.ToString();
            sw.Close();
            return output;
        }
        private static string SerializeJson(ConnectorQuery query, IEnumerable<object> records)
        {
            if (records == null || query?.Fields == null || query.Fields.Count == 0) return String.Empty;
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartArray();
                foreach (var record in records)
                {
                    writer.WriteStartObject();
                    var recordType = record.GetType();
                    foreach (var field in query.Fields)
                    {
                        var propertyInfo = recordType.GetProperty(field.Name);
                        if (propertyInfo != null)
                        {
                            writer.WritePropertyName(field.DisplayName);
                            writer.WriteValue(GetFieldValue(propertyInfo.GetValue(record), field, ConnectorSerializationType.Json));
                        }
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.Close();
            }
            var output = sw.ToString();
            sw.Close();
            return output;
        }
        private static string SerializeCsv(ConnectorQuery query, IEnumerable<object> records)
        {
            if (records == null || query?.Fields == null || query.Fields.Count == 0) return String.Empty;
            var recordList = records as IList<object> ?? records.ToList();
            if (!recordList.Any()) return String.Empty;
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            // add field names
            sw.WriteLine(String.Join(",", query.Fields.Select(field => field.DisplayName).ToList()));
            // add record values
            var recordType = recordList[0].GetType();
            foreach (var record in recordList)
            {
                var fieldValues = query.Fields.Select(field => GetFieldValue(GetPropertyValue(record, recordType, field.Name), field, ConnectorSerializationType.Csv)).ToList();
                sw.WriteLine(String.Join(",", fieldValues));
            }
            var output = sw.ToString();
            sw.Close();
            return output;
        }

        private static object GetPropertyValue(object record, Type recordType, string fieldName)
        {
            var propertyInfo = recordType.GetProperty(fieldName);
            return propertyInfo?.GetValue(record);
        }

        private static string GetFieldValue(object value, ConnectorField field, ConnectorSerializationType type)
        {
            if (value == null) return String.Empty;
            switch (field.Type.Id)
            {
                case FieldTypeIdEnum:
                    return field.GetListDescription(value.ToString());
                case FieldTypeIdYesNo:
                    return (bool)value ? "Yes" : "No";
                case FieldTypeIdDate:
                    return $"{value:yyyy-MM-dd}";
                case FieldTypeIdTime:
                    return $"{value:hh:mm}";
                case FieldTypeIdCurrency:
                    switch (type)
                    {
                        case ConnectorSerializationType.Csv:
                            return $"{value:c}";
                        default:
                            return value.ToString();
                    }
                case FieldTypeIdPercentage:
                    switch (type)
                    {
                        case ConnectorSerializationType.Csv:
                            return $"{value:p}";
                        default:
                            return value.ToString();
                    }
            }
            var stringValue = value.ToString();
            if (type == ConnectorSerializationType.Csv && (stringValue.Contains(',') || stringValue.Contains('\n'))) stringValue = $"\"{stringValue}\"";
            return stringValue;
        }

        private void AddFieldTypes()
        {
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdString, "string", typeof(string)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdDate, "date", typeof(DateTime)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdCurrency, "currency", typeof(decimal)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdInteger, "integer", typeof(int)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdPhone, "phone", typeof(string)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdEnum, "list", typeof(string)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdPercentage, "percentage", typeof(decimal)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdYesNo, "boolean", typeof(bool)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdTime, "time", typeof(DateTime)));
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdQuantity, "quantity", typeof(decimal)));
            var emailFieldType = new ConnectorFieldType(FieldTypeIdEmail, "email", typeof(string))
            {
                HtmlFormat = "<a href='mailto:{0}' target='_blank'>{0}</a>"
            };
            FieldTypes.Add(emailFieldType);
            var urlFieldType = new ConnectorFieldType(FieldTypeIdUrl, "url", typeof(string))
            {
                HtmlFormat = "<a href='{0}' target='_blank'>{0}</a>"
            };
            FieldTypes.Add(urlFieldType);
            var imageFieldType = new ConnectorFieldType(FieldTypeIdImage, "image", typeof(string), 0, 0)
            {
                HtmlFormat = "<img src='{0}' width='60' />"
            };
            FieldTypes.Add(imageFieldType);
            var skypeFieldType = new ConnectorFieldType(FieldTypeIdSkype, "skype", typeof(string))
            {
                HtmlFormat = "{0} <a href='skype:{0}?chat'><i class='fa fa-weixin'></i></a>" +
                             "<a href='skype:{0}?call'><i class='fa fa-phone'></i></a>" +
                             "<a href='skype:{0}?call&video=true'><i class='fa fa-video-camera'></i></a>"
            };
            FieldTypes.Add(skypeFieldType);
            var twitterFieldType = new ConnectorFieldType(FieldTypeIdTwitter, "twitter", typeof(string))
            {
                HtmlFormat = "<a href='https://twitter.com/{0}' target='_blank'>{0}</a>"
            };
            FieldTypes.Add(twitterFieldType);
            var colorFieldType = new ConnectorFieldType(FieldTypeIdColor, "color", typeof(string), 0, 0)
            {
                HtmlFormat = "<p style=\"color:{0}\">{0}</p>"
            };
            FieldTypes.Add(colorFieldType);
            FieldTypes.Add(new ConnectorFieldType(FieldTypeIdAddress, "address", typeof(string)));
        }

        public bool IsFieldTypeString(int fieldTypeId)
        {
            switch (fieldTypeId)
            {
                case FieldTypeIdString:
                case FieldTypeIdAddress:
                case FieldTypeIdEmail:
                case FieldTypeIdEnum:
                case FieldTypeIdPhone:
                case FieldTypeIdSkype:
                case FieldTypeIdTwitter:
                case FieldTypeIdUrl:
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}
