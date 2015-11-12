using System;
using System.Collections.Generic;
using eOne.Common.Query;

namespace eOne.Common.Connectors
{
    public class ConnectorFieldType
    {
        
        public ConnectorFieldType(int id, string name, Type type, int searchPriority = 1, int defaultSearchPriority = 2)
        {
            Id = id;
            Name = name;
            Type = type;
            XmlDatatype = GetXmlDatatype();
            SearchPriority = searchPriority;
            DefaultSearchPriority = defaultSearchPriority;
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
        public List<Connector.ConnectorSummaryMethod> AllowedSummaryMethods { get; set; }
        public string HtmlFormat { get; set; }
        public int SearchPriority { get; set; }
        public int DefaultSearchPriority { get; set; }

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
