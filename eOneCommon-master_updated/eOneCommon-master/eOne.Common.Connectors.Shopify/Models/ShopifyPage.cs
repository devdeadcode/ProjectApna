using System;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyPage : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true, Description = true)]
        public string title { get; set; }

        [FieldSettings("Author", DefaultField = true)]
        public string author { get; set; }

        [FieldSettings("Published", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool published => published_at != null;

        #endregion

        #region Properties

        [FieldSettings("Body")]
        public string body_html { get; set; }

        [FieldSettings("Handle")]
        public string handle { get; set; }

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int id { get; set; }

        [FieldSettings("Template suffix")]
        public string template_suffix { get; set; }

        [FieldSettings("Created at date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime created_at { get; set; }

        [FieldSettings("Published at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? published_at { get; set; }

        [FieldSettings("Updated at date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime? updated_at { get; set; }

        #endregion

        #region Hidden properties

        public int shop_id { get; set; }

        #endregion

        #region Calculations
        
        [FieldSettings("Created at time", FieldTypeId = Connector.FieldTypeIdTime, Created = true)]
        public DateTime create_at_time => created_at;

        [FieldSettings("Updated at time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime? updated_at_time => updated_at;

        [FieldSettings("Published at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime? published_at_time => published_at;

        #endregion

    }
}

//metafield
//{ "key" : "new" }
//{ "value" : "newvalue" }
//{ "value_type" : "string" }
//{ "namespace" : "global" }
//Attaches additional information to a shop's resources:

//key(required): Identifier for the metafield(maximum of 30 characters).
//namespace (required): Container for a set of metadata.Namespaces help distinguish between metadata you created and metadata created by another individual with a similar namespace (maximum of 20 characters).
//value(required): Information to be stored as metadata.
//value_type (required): States whether the information in the value is stored as a 'string' or 'integer.'
//description(optional): Additional information about the metafield.
//
