using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using eOne.Common.Actions;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Service
{
    public abstract class XmlRpcConnector : ServiceConnector
    {

        #region Classes

        [XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false)]
        public class methodResponse
        {
            public methodResponseParams @params { get; set; }
        }

        [XmlType(AnonymousType = true)]
        public class methodResponseParams
        {
            public methodResponseParamsParam param { get; set; }
        }

        [XmlType(AnonymousType = true)]
        public class methodResponseParamsParam
        {
            public methodResponseParamsParamValue value { get; set; }
        }

        [XmlType(AnonymousType = true)]
        public class methodResponseParamsParamValue
        {
            public methodResponseParamsParamValueArray array { get; set; }
        }

        [XmlType(AnonymousType = true)]
        public class methodResponseParamsParamValueArray
        {
            [XmlArrayItem("value", IsNullable = false)]
            public methodResponseParamsParamValueArrayValue[] data { get; set; }
        }

        [XmlType(AnonymousType = true)]
        public class methodResponseParamsParamValueArrayValue
        {
            [XmlArrayItem("member", IsNullable = false)]
            public methodResponseParamsParamValueArrayValueMember[] @struct { get; set; }
        }

        [XmlType(AnonymousType = true)]
        public class methodResponseParamsParamValueArrayValueMember
        {
            public string name { get; set; }
            public methodResponseParamsParamValueArrayValueMemberValue value { get; set; }
        }

        [XmlType(AnonymousType = true)]
        public class methodResponseParamsParamValueArrayValueMemberValue
        {
            public int? i4 { get; set; }
            public double? @double { get; set; }
            public string @string { get; set; }
            public int? @int { get; set; }

            [XmlElement("dateTime.iso8601")]
            public string dateTimeiso8601 { get; set; }

            [XmlText()]
            public string[] Text { get; set; }
        }

        #endregion

        #region Properties

        public string XmlEncoding { get; set; }

        #endregion

        #region Methods

        public abstract string GetRequestMethod(ConnectorQuery query);

        public abstract object GetRequestParameters(ConnectorQuery query);

        public abstract string GetActionMethod(ConnectorAction action);

        public abstract object GetActionParameters(ConnectorAction action, List<Tuple<string, string>> parameters);

        public override IEnumerable<object> GetRecords(ConnectorQuery query)
        {
            var methodName = GetRequestMethod(query);
            var parameters = GetRequestParameters(query);
            var requestXml = GetXml(methodName, parameters);
            // hack to get around forced encoding for some connectors
            if (!string.IsNullOrWhiteSpace(XmlEncoding)) requestXml = requestXml.Replace("encoding=\"utf-16\"", $"encoding=\"{XmlEncoding}\"");
            var request = GetWebRequest(requestXml);
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        var responseStr = new StreamReader(responseStream).ReadToEnd();
                        var serializer = new XmlSerializer(typeof(methodResponse));
                        using (TextReader reader = new StringReader(responseStr))
                        {
                            var resp = (methodResponse)serializer.Deserialize(reader);
                            var records = ConvertPropertiesForResponse(query.Entity.RecordType, resp);
                            return (IEnumerable<object>)records;
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            var methodName = GetActionMethod(action);
            var actionParameters = GetActionParameters(action, parameters);
            var actionXml = GetXml(methodName, actionParameters);
            var request = GetWebRequest(actionXml);
            request.GetResponse();
        }

        #endregion

        #region Helpers

        private HttpWebRequest GetWebRequest(string xml)
        {
            var request = (HttpWebRequest)WebRequest.Create(BaseUrl);
            request.Method = "POST";
            if (!string.IsNullOrWhiteSpace(AuthenticationValue)) request.Headers.Add(HttpRequestHeader.Authorization, AuthenticationValue);
            var bytes = Encoding.ASCII.GetBytes(xml);
            request.ContentType = "application/xml";
            var requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            return request;
        }

        private static string GetXml(string methodName, object parameters)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            using (var writer = XmlWriter.Create(sw))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("methodCall");
                writer.WriteElementString("methodName", methodName);
                var propertyInfos = parameters.GetType().GetProperties();
                if (propertyInfos.Length > 0)
                {
                    writer.WriteStartElement("params");
                    foreach (var item in propertyInfos)
                    {
                        var property = item.GetValue(parameters);
                        if (property != null)
                        {
                            writer.WriteStartElement("param");
                            writer.WriteStartElement("value");
                            var type = item.PropertyType.Name.ToLower();
                            if (property is IEnumerable && type != "string")
                            {
                                writer.WriteStartElement("array");
                                writer.WriteStartElement("data");
                                foreach (var listitem in property as IEnumerable)
                                {
                                    writer.WriteStartElement("value");
                                    // todo - handle arrays of other field types
                                    writer.WriteElementString("string", listitem.ToString());
                                    writer.WriteEndElement(); // value
                                }
                                writer.WriteEndElement(); // data
                                writer.WriteEndElement(); // array
                            }
                            else
                            {
                                if (IsStruct(type))
                                {
                                    writer.WriteStartElement("struct");
                                    var structPropertyInfos = property.GetType().GetProperties();
                                    foreach (var structItem in structPropertyInfos)
                                    {
                                        if (structItem.GetValue(property) != null && structItem.CanWrite)
                                        {
                                            writer.WriteStartElement("member");
                                            writer.WriteElementString("name", structItem.Name);
                                            writer.WriteStartElement("value");
                                            var typeName = GetParameterValueTypeName(structItem.PropertyType.Name.ToLower());
                                            // todo - handle date formatting
                                            writer.WriteElementString(typeName, structItem.GetValue(property).ToString());
                                            writer.WriteEndElement(); // value
                                            writer.WriteEndElement(); // member
                                        }
                                    }
                                    writer.WriteEndElement(); // struct
                                }
                                else
                                {
                                    var typeName = GetParameterValueTypeName(type);
                                    writer.WriteElementString(typeName, property.ToString());
                                }
                            }
                            writer.WriteEndElement(); // value
                            writer.WriteEndElement(); // param
                        }
                    }
                    writer.WriteEndElement(); // params
                }
                writer.WriteEndElement(); // methodCall
                writer.WriteEndDocument();
                writer.Close();
            }
            var output = sw.ToString();
            sw.Close();
            return output;
        }

        private static bool IsStruct(string systemTypeName)
        {
            switch (systemTypeName)
            {
                case "int32":
                case "datetime":
                case "decimal":
                case "boolean":
                case "string":
                    return false;
                default:
                    return true;
            }
        }

        private static string GetParameterValueTypeName(string systemTypeName)
        {
            switch (systemTypeName)
            {
                case "int32":
                    return "int";
                case "datetime":
                    return "dateTime.iso8601";
                case "decimal":
                    return "double";
                case "boolean":
                    return "boolean";
                default:
                    return "string";
            }
        }

        private static IList ConvertPropertiesForResponse(Type recordType, methodResponse response)
        {
            var records = ObjectHelper.CreateListObject(recordType);
            foreach (var data in response.@params.param.value.array.data)
            {
                var record = ObjectHelper.CreateObject(recordType);
                foreach (var member in data.@struct)
                {
                    SetPropertyValue(record, recordType, member);
                }
                records.Add(record);
            }
            return records;
        }

        private static void SetPropertyValue(object item, Type type, methodResponseParamsParamValueArrayValueMember member)
        {
            var property = type.GetProperty(member.name);
            if (property == null) return;
            var typeName = property.PropertyType.Name;
            if (Nullable.GetUnderlyingType(property.PropertyType) != null)
            {
                typeName = Nullable.GetUnderlyingType(property.PropertyType).Name;
            }
            switch (typeName)
            {
                case "Double":
                    if (member.value.@double != null) property.SetValue(item, member.value.@double);
                    break;
                case "Decimal":
                    if (member.value.@double != null) property.SetValue(item, (decimal)member.value.@double);
                    break;
                case "Int":
                case "Int32":
                case "Integer":
                    if (member.value.i4 != null) property.SetValue(item, member.value.i4, null);
                    if (member.value.@int != null) property.SetValue(item, member.value.@int, null);
                    break;
                case "DateTime":
                    property.SetValue(item, ParseDateTime(member.value.dateTimeiso8601), null);
                    break;
                default:
                    property.SetValue(item, member.value.@string ?? member.value.Text[0], null);
                    break;
            }
        }

        private static DateTime ParseDateTime(string value)
        {
            var parts = value.Split('T');
            var year = int.Parse(parts[0].Substring(0, 4));
            var month = int.Parse(parts[0].Substring(4, 2));
            var day = int.Parse(parts[0].Substring(6, 2));
            var timeParts = parts[1].Split(':');
            var hour = int.Parse(timeParts[0]);
            var minute = int.Parse(timeParts[1]);
            var second = int.Parse(timeParts[2]);
            return new DateTime(year, month, day, hour, minute, second);
        }

        #endregion

    }
}