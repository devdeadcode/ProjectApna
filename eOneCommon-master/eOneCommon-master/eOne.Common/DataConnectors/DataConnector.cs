 using System;
 using System.Collections.Generic;
 using System.IO;
 using System.Linq;
 using System.Reflection;
 using System.Text;
 using System.Xml;
 using Newtonsoft.Json;
 using Formatting = Newtonsoft.Json.Formatting;

namespace eOne.Common.DataConnectors
{
    
    /// <summary>
    /// Base class for connectors that return data
    /// </summary>
    public abstract class DataConnector : Connector
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

        public enum DataConnectorSummaryMethod { Count, Sum, Maximum, Minimum, Average }
        public enum DataConnectorSerializationType { Xml, Json }

        #endregion

        protected DataConnector()
        {
            FieldTypes = new List<DataConnectorFieldType>();
            Entities = new List<DataConnectorEntity>();
            Companies = new List<DataConnectorCompany>();
            DefaultFavorites = new List<ConnectorQuery>();
            Groups = new List<DataConnectorEntityGroup>();
            Setup = new ConnectorSetup();
            AddFieldTypes();
        }

        #region Properties

        public List<DataConnectorFieldType> FieldTypes { get; set; }
        public List<DataConnectorEntityGroup> Groups { get; set; }
        public List<DataConnectorEntity> Entities { get; set; }
        public List<DataConnectorCompany> Companies { get; set; }
        public bool Multicompany { get; set; }
        public string CompanyPrompt { get; set; }
        public string CompanyPluralPrompt { get; set; }
        public List<ConnectorQuery> DefaultFavorites { get; set; }

        #endregion

        #region Methods

        public void RunAction(int entityId, int actionId, List<Tuple<string, string>> parameters)
        {
            var entity = FindEntity(entityId);
            var action = entity.FindAction(actionId);
            if (action == null) return;
            RunAction(action, parameters);
        }
        public abstract void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters);

        public abstract IEnumerable<object> GetRecords(ConnectorQuery query);
        public virtual string GetData(string queryXml, DataConnectorSerializationType type)
        {
            var query = new ConnectorQuery(this, queryXml);
            return Serialize(query, GetRecords(query), type);
        }
        public virtual string Search(string queryString, int entityId, int companyId, DataConnectorSerializationType type)
        {
            if (string.IsNullOrWhiteSpace(queryString)) return string.Empty;
            var search = new ConnectorSearch { Connector = this, Query = queryString, Entity = FindEntity(entityId) };
            search.Companies.Add(FindCompany(companyId));
            var query = new ConnectorQuery(search);
            return Serialize(query, GetRecords(query), type);
        }
        public virtual string GetSummary(string queryXml, DataConnectorSummaryMethod summaryMethod, DataConnectorField summaryField = null)
        {
            var query = new ConnectorQuery(this, queryXml);
            var records = GetRecords(query);
            return GetSummary(records, query.Entity.RecordType, summaryMethod, summaryField).ToString();
        }

        public DataConnectorEntityGroup AddGroup(int id, string name)
        {
            var group = new DataConnectorEntityGroup(id, name);
            group.ParentConnector = this;
            Groups.Add(group);
            return group;
        }
        public DataConnectorEntity AddEntity()
        {
            var entity = new DataConnectorEntity { ParentConnector = this };
            Entities.Add(entity);
            return entity;
        }
        public DataConnectorEntity AddEntity(int id, string name)
        {
            var entity = new DataConnectorEntity(id, name, this);
            Entities.Add(entity);
            return entity;
        }
        public DataConnectorEntity AddEntity(int id, string name, Type model, bool cached = false)
        {
            var entity = new DataConnectorEntity(id, name, model, this, cached);
            Entities.Add(entity);
            return entity;
        }
        public DataConnectorCompany AddCompany()
        {
            var company = new DataConnectorCompany { ParentConnector = this };
            Companies.Add(company);
            return company;
        }
        public DataConnectorCompany AddCompany(int id, string name, string databaseName = "")
        {
            var company = new DataConnectorCompany(id, name, databaseName) { ParentConnector = this };
            Companies.Add(company);
            return company;
        }
        public DataConnectorEntity FindEntity(int entityId)
        {
            return Entities?.FirstOrDefault(entity => entity.Id == entityId);
        }
        public DataConnectorCompany FindCompany(int companyId)
        {
            return Multicompany ? Companies?.FirstOrDefault(company => company.Id == companyId) : null;
        }

        public DataConnectorFieldType FindFieldType(int fieldTypeId)
        {
            return FieldTypes?.FirstOrDefault(fieldType => fieldType.Id == fieldTypeId);
        }
        public static string FindParameterValue(IEnumerable<Tuple<string, string>> parameters, string name)
        {
            foreach (var parameter in parameters.Where(parameter => parameter.Item1 == name)) return parameter.Item2;
            return string.Empty;
        }
        public static string GetValidDisplayName(string name)
        {
            var words = name.Split(' ');
            var validWords = words.Select(word => word.ToUpper() == word ? word : word.ToLower()).ToList();
            validWords[0] = validWords[0].First().ToString().ToUpper() + validWords[0].Substring(1);
            return string.Join(" ", validWords);
        }

        #endregion

        #region Helpers

        private static object GetSummary(IEnumerable<object> records, Type type, DataConnectorSummaryMethod method, DataConnectorField field)
        {
            if (method == DataConnectorSummaryMethod.Count) return records.Count();
            if (field == null) return 0;
            var propertyInfo = type.GetProperty(field.Name);
            switch (method)
            {
                case DataConnectorSummaryMethod.Average:
                    return GetSummaryAverage(records, propertyInfo);
                case DataConnectorSummaryMethod.Maximum:
                    return records.Max(record => record.GetType().GetProperty(field.Name).GetValue(record, null));
                case DataConnectorSummaryMethod.Minimum:
                    return records.Min(record => record.GetType().GetProperty(field.Name).GetValue(record, null));
                case DataConnectorSummaryMethod.Sum:
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
        private static string Serialize(ConnectorQuery query, IEnumerable<object> records, DataConnectorSerializationType type)
        {
            if (records == null) return string.Empty;
            switch (type)
            {
                case DataConnectorSerializationType.Xml:
                    return SerializeXml(query, records);
                case DataConnectorSerializationType.Json:
                    return SerializeJson(query, records);
            }
            return string.Empty;
        }

        private static string SerializeXml(ConnectorQuery query, IEnumerable<object> records)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var entityName = GetXmlEntityName(query.Entity.Name);
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
                        if (value != null) writer.WriteElementString(GetXmlFieldName(field.DisplayName), value.ToString());
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
        private static string GetXmlEntityName(string entityName)
        {
            return StripInvalidXmlChars(entityName.Replace(" ", "_"));
        }
        private static string GetXmlFieldName(string fieldName)
        {
            return StripInvalidXmlChars(fieldName.Replace(" ", "_x0020_"));
        }
        private static string StripInvalidXmlChars(string value)
        {
            var stripped = value.Replace("<", "");
            stripped = stripped.Replace(">", "");
            stripped = stripped.Replace("&", "");
            return stripped;
        }

        private static string SerializeJson(ConnectorQuery query, IEnumerable<object> records)
        {
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
                            writer.WriteValue(propertyInfo.GetValue(record));
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


        private void AddFieldTypes()
        {
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdString, "string", typeof(string)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdDate, "date", typeof(DateTime)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdCurrency, "currency", typeof(decimal)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdInteger, "integer", typeof(int)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdPhone, "phone", typeof(string)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdEnum, "list", typeof(string)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdPercentage, "percentage", typeof(decimal)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdYesNo, "boolean", typeof(bool)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdTime, "time", typeof(DateTime)));
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdQuantity, "quantity", typeof(decimal)));
            var emailFieldType = new DataConnectorFieldType(FieldTypeIdEmail, "email", typeof(string))
            {
                HtmlFormat = "<a href='mailto:{0}' target='_blank'>{0}</a>"
            };
            FieldTypes.Add(emailFieldType);
            var urlFieldType = new DataConnectorFieldType(FieldTypeIdUrl, "url", typeof(string))
            {
                HtmlFormat = "<a href='{0}' target='_blank'>{0}</a>"
            };
            FieldTypes.Add(urlFieldType);
            var imageFieldType = new DataConnectorFieldType(FieldTypeIdImage, "image", typeof(string))
            {
                HtmlFormat = "<img src='{0}' width='60' />"
            };
            FieldTypes.Add(imageFieldType);
            var skypeFieldType = new DataConnectorFieldType(FieldTypeIdSkype, "skype", typeof(string))
            {
                HtmlFormat = "{0} <a href='skype:{0}?chat'><i class='fa fa-weixin'></i></a>" +
                             "<a href='skype:{0}?call'><i class='fa fa-phone'></i></a>" +
                             "<a href='skype:{0}?call&video=true'><i class='fa fa-video-camera'></i></a>"
            };
            FieldTypes.Add(skypeFieldType);
            var twitterFieldType = new DataConnectorFieldType(FieldTypeIdTwitter, "twitter", typeof(string))
            {
                HtmlFormat = "<a href='https://twitter.com/{0}' target='_blank'>{0}</a>"
            };
            FieldTypes.Add(twitterFieldType);
            var colorFieldType = new DataConnectorFieldType(FieldTypeIdColor, "color", typeof(string))
            {
                HtmlFormat = "<p style=\"color:{0}\">{0}</p>"
            };
            FieldTypes.Add(colorFieldType);
            FieldTypes.Add(new DataConnectorFieldType(FieldTypeIdAddress, "address", typeof(string)));
        }

        #endregion

    }
}
