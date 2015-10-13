using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimVisitor : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, SearchPriority = 5)]
        public string display_name { get; set; }

        [FieldSettings("Email", DefaultField = true, SearchPriority = 4, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Phone", DefaultField = true, SearchPriority = 3, FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string phone { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Notes", SearchPriority = 2)]
        public string notes { get; set; }

        [FieldSettings("Banned", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public int banned { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public DateTime created { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date => created.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_time => Time(created);

        #endregion

    }
}
