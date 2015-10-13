using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using eOne.Common.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace eOne.Common.DataConnectors.Rest
{
    
    /// <summary>
    /// Base class for REST connectors
    /// </summary>
    public abstract class RestConnector: DataConnector
    {

        #region Enums

        public enum RestConnectorAuthenticationType { Basic, UrlParameter, OAuth1, OAuth2 }
        public enum RestConnectorSerializationType { Xml, Json }
        public enum RestConnectorMethod { Get, Post, Put, Delete }

        #endregion

        protected RestConnector()
        {
            ConnectorMethod = RestConnectorMethod.Get;
            SerializationType = RestConnectorSerializationType.Json;
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            RateLimits = new List<RestConnectorRateLimiting>();
        }

        #region Properties

        public RestConnectorAuthenticationType AuthenticationType { get; set; }
        public RestConnectorSerializationType SerializationType { get; set; }
        public RestConnectorMethod ConnectorMethod { get; set; }
        public List<RestConnectorRateLimiting> RateLimits { get; set; }
        public string CallbackUrl { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public string BaseUrl { get; set; }
        public string SitePrefix { get; set; }
        public List<Tuple<string, string>> Headers { get; set; }
        public List<Tuple<string, string>> UrlParameters { get; set; }
        public DateTime TokenExpiryDate { get; set; }
        public int TokenExpiryDays { get; set; }
        public string ClientId { get; set; }
        public string AuthorizationUri { get; set; }
        public string AccessTokenUri { get; set; }
        public string Scope { get; set; }
        
        #endregion

        #region Methods

        public abstract string GetEndpoint(ConnectorQuery query);
        public abstract IEnumerable<object> Deserialize(string data, ConnectorQuery query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual List<Tuple<string, string>> GetUrlParameters(ConnectorQuery query)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual List<Tuple<string, string>> GetHeaders(DataConnectorEntity entity)
        {
            return null;
        }
        
        public override IEnumerable<object> GetRecords(ConnectorQuery query)
        {
            var data = GetResponse(query);
            if (string.IsNullOrWhiteSpace(data)) return null;
            var records = Filtering.Filter(Deserialize(data, query), query);
            return HtmlFormatting.Format(records, query);
        }
        public string GetResponse(ConnectorQuery query)
        {
            var endpoint = GetEndpoint(query);
            Headers = GetHeaders(query.Entity);
            UrlParameters = GetUrlParameters(query);
            return GetResponse(endpoint);
        }
        public string GetResponse(string endpoint)  
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
            if (AuthenticationType == RestConnectorAuthenticationType.Basic) AddBasicAuthenticationHeader(request, Username, Password);
            if (AuthenticationType == RestConnectorAuthenticationType.OAuth2) AddOAuth2AuthenticationHeader(request, Token);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (Headers != null) AddHeaders(request, Headers);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK ? response.Content : string.Empty;
        }
        public static T DeserializeJson<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        
        public static T DeserializeJson<T>(string value, IContractResolver resolver)
        {
            var settings = new JsonSerializerSettings {ContractResolver = resolver};
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
        public static T DeserializeXml<T>(string value)
        {
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
        public void RunPutAction(string endpoint, object body)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint, Method.PUT);
            if (AuthenticationType == RestConnectorAuthenticationType.Basic) AddBasicAuthenticationHeader(request, Username, Password);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (Headers != null) AddHeaders(request, Headers);
            if (body != null) AddBody(request, body);
            client.Execute(request);
        }
        public void RunPostAction(string endpoint, object body)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint, Method.POST);
            if (AuthenticationType == RestConnectorAuthenticationType.Basic) AddBasicAuthenticationHeader(request, Username, Password);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (Headers != null) AddHeaders(request, Headers);
            if (body != null) AddBody(request, body);
            client.Execute(request);
        }
        public void RunDeleteAction(string endpoint)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint, Method.DELETE);
            if (AuthenticationType == RestConnectorAuthenticationType.Basic) AddBasicAuthenticationHeader(request, Username, Password);
            if (UrlParameters != null) AddUrlParameters(request, UrlParameters);
            if (Headers != null) AddHeaders(request, Headers);
            client.Execute(request);
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
        private static void AddBasicAuthenticationHeader(IRestRequest request, string username, string password)
        {
            var userpass = $"{username}:{password}";
            var plainTextBytes = Encoding.UTF8.GetBytes(userpass);
            var value = Convert.ToBase64String(plainTextBytes);
            request.AddHeader("Authorization", $"Basic {value}");
        }
        private static void AddOAuth2AuthenticationHeader(IRestRequest request, string token)
        {
            request.AddHeader("Authorization", $"Bearer {token}");
        }
        private void AddBody(IRestRequest request, object body)
        {
            switch (SerializationType)
            {
                case RestConnectorSerializationType.Json:
                    request.AddJsonBody(body);
                    break;
                case RestConnectorSerializationType.Xml:
                    request.AddXmlBody(body);
                    break;
            }
        }
        #endregion

    }
}