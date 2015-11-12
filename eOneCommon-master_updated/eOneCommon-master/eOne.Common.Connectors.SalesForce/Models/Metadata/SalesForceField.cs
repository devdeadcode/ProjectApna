using System.Collections.Generic;

namespace eOne.Common.Connectors.SalesForce.Models.Metadata
{
    public class SalesForceField
    {

        public enum SalesForceFieldType
        {
            datetime,
            @int,
            picklist,
            @string,
            textarea,
            reference,
            date,
            @double,
            id,
            boolean,
            Type,
            phone,
            url,
            currency,
            email,
            percent,
            combobox,
            multipicklist
        }

        public bool autoNumber { get; set; }
        public int byteLength { get; set; }
        public bool calculated { get; set; }
        public string calculatedFormula { get; set; }
        public bool caseSensitive { get; set; }
        public string controllerName { get; set; }
        public bool createable { get; set; }
        public bool custom { get; set; }
        public string defaultValue { get; set; }
        public string defaultValueFormula { get; set; }
        public bool defaultedOnCreate { get; set; }
        public bool dependentPicklist { get; set; }
        public bool deprecatedAndHidden { get; set; }
        public int digits { get; set; }
        public string externalId { get; set; }
        public bool filterable { get; set; }
        public bool groupable { get; set; }
        public bool htmlFormatted { get; set; }
        public bool idLookup { get; set; }
        public string inlineHelpText { get; set; }
        public string label { get; set; }
        public int length { get; set; }
        public string name { get; set; }
        public bool nameField { get; set; }
        public bool namePointing { get; set; }
        public bool nillable { get; set; }
        public int precision { get; set; }
        public int scale { get; set; }
        public bool sortable { get; set; }
        public bool unique { get; set; }
        public bool updateable { get; set; }
        public bool writeRequiresMasterRead { get; set; }
        public bool restrictedPicklist { get; set; }
        public List<SalesForcePicklistValue> picklistValues { get; set; }
        public string soapType { get; set; }
        public SalesForceFieldType type { get; set; }
        public List<string> referenceTo { get; set; }
        public string relationshipName { get; set; }
        public string relationshipOrder { get; set; }

    }
}