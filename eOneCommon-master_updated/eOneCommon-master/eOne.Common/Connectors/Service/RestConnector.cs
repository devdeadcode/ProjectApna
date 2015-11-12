using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using eOne.Common.Helpers;
using eOne.Common.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace eOne.Common.Connectors.Service
{
    
    /// <summary>
    /// Base class for REST connectors
    /// </summary>
    public abstract class RestConnector: ServiceConnector
    {

        #region Enums

        public enum RestConnectorMethod { Get, Post, Put, Delete }

        #endregion

        protected RestConnector()
        {
            ConnectorMethod = RestConnectorMethod.Get;
            SerializationType = ServiceConnectorSerializationType.Json;
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            RateLimits = new List<ServiceConnectorRateLimiting>();
        }

        #region Properties

        public RestConnectorMethod ConnectorMethod { get; set; }
        public List<Tuple<string, string>> Headers { get; set; }
        public List<Tuple<string, string>> UrlParameters { get; set; }

        #endregion

        #region Methods

        public abstract string GetEndpoint(ConnectorQuery query);
        public abstract IEnumerable<object> Deserialize(string data, ConnectorQuery query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual List<Tuple<string, string>> GetHeaders(ConnectorQuery query)
        {
            return null;
        }

        public override IEnumerable<object> GetRecords(ConnectorQuery query)
        {
            var data = GetResponse(query);
            if (string.IsNullOrWhiteSpace(data)) return null;
            var records = Deserialize(data, query);
            if (records == null) return null;
            records = Filtering.Filter(records.ToList(), query);
            return HtmlFormatting.Format(records, query);
        }
        public string GetResponse(ConnectorQuery query)
        {
            var endpoint = GetEndpoint(query);
            Headers = GetHeaders(query);
            UrlParameters = GetParameters(query);
            return GetResponse(endpoint);
        }
        public string GetResponse(string endpoint, List<Tuple<string, string>> urlParameters = null)  
        {
            var client = new RestClient(BaseUrl);
            RestRequest request;
            switch (ConnectorMethod)
            {
                case RestConnectorMethod.Get:
                    request = new RestRequest(endpoint, Method.GET);
                    break;
                case RestConnectorMethod.Post:
                    request = new RestRequest(endpoint, Method.POST);
                    break;
                default:
                    return string.Empty;
            }
            AddAuthenticationHeaders(request);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (urlParameters != null) AddUrlParameters(request, urlParameters);
            if (Headers != null) AddHeaders(request, Headers);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK ? response.Content : string.Empty;
        }
        public static T DeserializeJson<T>(string value)
        {
            return IsJson(value) ? JsonConvert.DeserializeObject<T>(value) : default(T);
        }

        public static T DeserializeJson<T>(string value, ConnectorQuery query)
        {
            if (!IsJson(value)) return default(T);
            if (query.ApiFieldsUsed)
            {
                var resolver = new CustomJsonContractResolver(query.Entity);
                var settings = new JsonSerializerSettings { ContractResolver = resolver };
                return JsonConvert.DeserializeObject<T>(value, settings);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
        public static T DeserializeJson<T>(string value, IContractResolver resolver)
        {
            if (!IsJson(value)) return default(T);
            var settings = new JsonSerializerSettings {ContractResolver = resolver};
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
        public static T DeserializeXml<T>(string value)
        {
            if (!IsXml(value)) return default(T);
            var type = typeof(T);
            var serializer = new XmlSerializer(type);
            var stream = ToStream(value);
            return (T)serializer.Deserialize(stream);
        }
        public static Stream ToStream(string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public void RunPutAction(string endpoint, object body = null, List<Tuple<string, string>> parameters = null)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint, Method.PUT);
            AddAuthenticationHeaders(request);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (parameters != null) AddUrlParameters(request, parameters);
            if (Headers != null) AddHeaders(request, Headers);
            if (body != null) AddBody(request, body);
            client.Execute(request);
        }
        public void RunPostAction(string endpoint, object body = null, List<Tuple<string, string>> parameters = null)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint, Method.POST);
            AddAuthenticationHeaders(request);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (parameters != null) AddUrlParameters(request, parameters);
            if (Headers != null) AddHeaders(request, Headers);
            if (body != null) AddBody(request, body);
            client.Execute(request);
        }
        public void RunDeleteAction(string endpoint, List<Tuple<string, string>> parameters = null)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint, Method.DELETE);
            AddAuthenticationHeaders(request);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (parameters != null) AddUrlParameters(request, parameters);
            if (Headers != null) AddHeaders(request, Headers);
            client.Execute(request);
        }
        public ServiceConnectorRateLimiting AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo appliedTo, int requests, ServiceConnectorRateLimiting.LimitPeriod period, int numberOfPeriods = 1)
        {
            var rateLimit = new ServiceConnectorRateLimiting
            {
                AppliedTo = appliedTo,
                Period = period,
                NumberOfPeriods = numberOfPeriods
            };
            RateLimits.Add(rateLimit);
            return rateLimit;
        }
        public ServiceConnectorRateLimiting AddRateLimit(string endpoint, int requests, ServiceConnectorRateLimiting.LimitPeriod period, int numberOfPeriods = 1)
        {
            var rateLimit = new ServiceConnectorRateLimiting
            {
                AppliedTo = ServiceConnectorRateLimiting.LimitAppliedTo.Endpoint,
                Endpoint = endpoint,
                Period = period,
                NumberOfPeriods = numberOfPeriods
            };
            RateLimits.Add(rateLimit);
            return rateLimit;
        }

        #endregion

        #region Helpers

        private static void AddHeaders(IRestRequest request, IEnumerable<Tuple<string, string>> headers)
        {
            foreach (var header in headers) request.AddHeader(header.Item1, header.Item2);
        }
        private static void AddUrlParameters(IRestRequest request, IEnumerable<Tuple<string, string>> parameters)
        {
            foreach (var parameter in parameters) request.AddParameter(parameter.Item1, parameter.Item2);
        }
        private void AddAuthenticationHeaders(IRestRequest request)
        {
            if (!string.IsNullOrWhiteSpace(AuthenticationValue)) request.AddHeader("Authorization", AuthenticationValue);
        }
        private void AddBody(IRestRequest request, object body)
        {
            switch (SerializationType)
            {
                case ServiceConnectorSerializationType.Json:
                    request.AddJsonBody(body);
                    break;
                case ServiceConnectorSerializationType.Xml:
                    request.AddXmlBody(body);
                    break;
            }
        }
        private static bool IsJson(string value)
        {
            value = value.Trim();
            return (value.StartsWith("{") && value.EndsWith("}")) || (value.StartsWith("[") && value.EndsWith("]"));
        }
        private static bool IsXml(string value)
        {
            value = value.Trim();
            return value.StartsWith("<") && value.EndsWith(">");
        }

        #endregion

    }
}