using System;
using System.Collections.Generic;

namespace eOne.Common.DataConnectors
{
    public class DataConnectorFieldType
    {
        
        public DataConnectorFieldType(int id, string name, Type type)
        {
            Id = id;
            Name = name;
            Type = type;
            XmlDatatype = GetXmlDatatype();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public string XmlDatatype { get; set; }
        public string NullValue { get; set; }
        public string SqlFunction { get; set; }
        public bool ListField { get; set; }
        public string ListIdFormat { get; set; }
        public string StringMask { get; set; }
        public string MaskChar { get; set; }
        public bool AddTotals { get; set; }
        public bool UseDecimals { get; set; }
        public int DefaultDecimals { get; set; }
        public int MaxDecimals { get; set; }
        public int MinDecimals { get; set; }
        public bool AllowCalculation { get; set; }
        public List<ConnectorRestriction.ConnectorRestrictionType> AllowedRestrictionTypes { get; set; }
        public List<DataConnector.DataConnectorSummaryMethod> AllowedSummaryMethods { get; set; }
        public string HtmlFormat { get; set; }

        private string GetXmlDatatype()
        {
            switch (Type.Name)
            {
                case "DateTime":
                    return "date";
                case "decimal":
                    return "decimal";
                case "int":
                    return "integer";
            }
            return "string";
        }

    }
}
