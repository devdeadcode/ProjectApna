using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillTemplate : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }
        
        [FieldSettings("From email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string from_email { get; set; }
        
        [FieldSettings("From name", DefaultField = true)]
        public string from_name { get; set; }

        #endregion

        #region Properties

        [FieldSettings("HTML code")]
        public string code { get; set; }

        [FieldSettings("Text")]
        public string text { get; set; }

        #endregion

        #region Hidden properties

        public string slug { get; set; }
        public List<string> labels { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string publish_name { get; set; }
        public string publish_code { get; set; }
        public string publish_subject { get; set; }
        public string publish_from_email { get; set; }
        public string publish_from_name { get; set; }
        public string publish_text { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Published at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime published_at_date => published_at.Date;

        [FieldSettings("Published at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime published_at_time => Time(published_at);

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_at_date => updated_at.Date;

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime updated_at_time => Time(updated_at);

        [FieldSettings("Labels")]
        public string label_list => CommaSeparatedValues(labels);

        #endregion

    }
}