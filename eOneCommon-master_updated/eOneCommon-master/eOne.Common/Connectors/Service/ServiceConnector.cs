using System;
using System.Collections.Generic;
using System.Text;

namespace eOne.Common.Connectors.Service
{
    /// <summary>
    /// Base class for web service connectors
    /// </summary>
    public abstract class ServiceConnector : Connector
    {

        #region Enums

        public enum ServiceConnectorAuthenticationType { Basic, UrlParameter, OAuth1, OAuth2, Custom }
        public enum ServiceConnectorSerializationType { Xml, Json }
        
        #endregion

        protected ServiceConnector()
        {
            SerializationType = ServiceConnectorSerializationType.Json;
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            RateLimits = new List<ServiceConnectorRateLimiting>();
        }

        #region Properties

        public ServiceConnectorAuthenticationType AuthenticationType { get; set; }
        public ServiceConnectorSerializationType SerializationType { get; set; }
        public List<ServiceConnectorRateLimiting> RateLimits { get; set; }
        public string CallbackUrl { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public string BaseUrl { get; set; }
        public string SitePrefix { get; set; }
        public DateTime TokenExpiryDate { get; set; }
        public int TokenExpiryDays { get; set; }
        public string ClientId { get; set; }
        public string AuthorizationUri { get; set; }
        public string AccessTokenUri { get; set; }
        public string Scope { get; set; }
        public string RefreshTokenUri { get; set; }
        public string AuthenticationValue
        {
            get
            {
                switch (AuthenticationType)
                {
                    case ServiceConnectorAuthenticationType.Basic:
                        return BasicAuthenticationValue;
                    case ServiceConnectorAuthenticationType.OAuth2:
                        return OAuth2AuthenticationValue;
                }
                return string.Empty;
            }
        }

        #endregion

        private string BasicAuthenticationValue
        {
            get
            {
                var userpass = $"{Username}:{Password}";
                var plainTextBytes = Encoding.UTF8.GetBytes(userpass);
                var value = Convert.ToBase64String(plainTextBytes);
                return $"Basic {value}";
            }
        }
        private string OAuth2AuthenticationValue => $"Bearer {Token}";
    }
}
