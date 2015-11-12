using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillTemplate : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }
        
        [FieldSettings("From email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string from_email { get; set; }
        
        [FieldSettings("From name", DefaultField = true)]
        public string from_name { get; set; }

        #endregion

        #region Properties

        [FieldSettings("HTML code")]
        public string code { get; set; }

        [FieldSettings("Text")]
        public string text { get; set; }

        [FieldSettings("Published at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime published_at { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        #endregion

        #region Hidden properties

        public string slug { get; set; }
        public List<string> labels { get; set; }
        public string publish_name { get; set; }
        public string publish_code { get; set; }
        public string publish_subject { get; set; }
        public string publish_from_email { get; set; }
        public string publish_from_name { get; set; }
        public string publish_text { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Published at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime published_at_time => published_at;

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime updated_at_time => updated_at;

        [FieldSettings("Labels")]
        public string label_list => CommaSeparatedValues(labels);

        #endregion

    }
}