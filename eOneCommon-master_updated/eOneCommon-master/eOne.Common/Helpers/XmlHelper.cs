using System;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace eOne.Common.Helpers
{
    class XmlHelper
    {

        public const string TrueString = "TRUE";

        public static bool GetXmlNodeBool(XElement parentNode, string nodeName)
        {
            return (GetXmlNodeString(parentNode, nodeName).ToUpper() == TrueString);
        }
        public static int GetXmlNodeInt(XElement parentNode, string nodeName)
        {
            string stringValue = GetXmlNodeString(parentNode, nodeName);
            return String.IsNullOrWhiteSpace(stringValue) ? 0 : Int32.Parse(stringValue);
        }
        public static DateTime GetXmlNodeDateTime(XElement parentNode, string nodeName)
        {
            string stringValue = GetXmlNodeString(parentNode, nodeName);
            return String.IsNullOrWhiteSpace(stringValue) ? DateTime.MinValue : DateTime.Parse(stringValue);
        }
        public static string GetXmlNodeString(XElement parentNode, string nodeName)
        {
            var childNodes = from el in parentNode.Descendants(nodeName) select el;
            var xElements = childNodes as XElement[] ?? childNodes.ToArray();
            return xElements.Any() ? xElements.First().Value : String.Empty;
        }
        public static XElement GetChildNode(XElement parentNode, string nodeName)
        {
            var childNodes = from el in parentNode.Descendants(nodeName) select el;
            var xElements = childNodes as XElement[] ?? childNodes.ToArray();
            return xElements.Any() ? xElements.First() : null;
        }

        public static string CreateXmlTag(string tag, string value)
        {
            return String.Format("<{0}>{1}</{0}>", tag, value);
        }

        public static DataSet XmlToDataSet(string xml)
        {
            var xmlData = new DataSet();
            if (!String.IsNullOrWhiteSpace(xml))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlData.ReadXml(new XmlNodeReader(xmlDoc));
            }
            return xmlData;
        }

        public static string GetXmlEntityName(string entityName)
        {
            return StripInvalidXmlChars(entityName.Replace(" ", "_"));
        }

        public static string GetXmlFieldName(string fieldName)
        {
            return XmlConvert.EncodeName(fieldName);
            //return StripInvalidXmlChars(fieldName.Replace(" ", "_x0020_"));
        }

        private static string StripInvalidXmlChars(string value)
        {
            var stripped = value.Replace("<", "");
            stripped = stripped.Replace(">", "");
            stripped = stripped.Replace("&", "");
            return stripped;
        }
    }
}
