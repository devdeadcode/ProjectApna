namespace eOne.Common.Connectors.SalesForce.Models.Metadata
{
    public class SalesForceChildRelationship
    {

        public bool cascadeDelete { get; set; }
        public string childSObject { get; set; }
        public bool deprecatedAndHidden { get; set; }
        public string field { get; set; }
        public string relationshipName { get; set; }

    }
}