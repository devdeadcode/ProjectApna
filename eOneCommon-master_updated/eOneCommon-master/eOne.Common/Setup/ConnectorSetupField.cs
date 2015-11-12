using System.Collections.Generic;

namespace eOne.Common.Setup
{
    public class ConnectorSetupField
    {

        public const string FieldNameName = "name";
        public const string FieldNamePrefix = "prefix";
        public const string FieldNameUser = "username";
        public const string FieldNamePassword = "password";
        public const string FieldNameServer = "server";
        public const string FieldNamePort = "port";
        public const string FieldNameDatabase = "database";
        public const string FieldNameKey = "key";
        public const string FieldNameToken = "token";

        public class ConnectorSetupFieldStringOptions
        {
            public int MinLength { get; set; }
            public int MaxLength { get; set; }
            public string Mask { get; set; }
            public bool Password { get; set; }
        }
        public class ConnectorSetupFieldListOptions
        {
            public List<ConnectorSetupFieldListItem> ListItems { get; set; }
        }
        public class ConnectorSetupFieldListItem
        {
            public string Id { get; set; }
            public string Description { get; set; }
        }
        public class ConnectorSetupFieldInstruction
        {

            public string Header { get; set; }
            public string Body { get; set; }
            public string Url { get; set; }

        }

        public enum ConnectorSetupFieldType { String, Checkbox, List, Label }

        public ConnectorSetupField()
        {
            Instructions = new List<ConnectorSetupFieldInstruction>();
        }
        public ConnectorSetupField(string name, string description, ConnectorSetupFieldType type, string defaultValue = "", bool required = false)
        {
            Name = name;
            Description = description;
            Type = type;
            DefaultValue = defaultValue;
            Required = required;
            Instructions = new List<ConnectorSetupFieldInstruction>();
            StringOptions = new ConnectorSetupFieldStringOptions();
            ListOptions = new ConnectorSetupFieldListOptions();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ConnectorSetupFieldType Type { get; set; }
        public string DefaultValue { get; set; }
        public bool Required { get; set; }
        public ConnectorSetupFieldStringOptions StringOptions { get; set; }
        public ConnectorSetupFieldListOptions ListOptions { get; set; }
        public List<ConnectorSetupFieldInstruction> Instructions { get; set; }
        public string Value;

    }
}
