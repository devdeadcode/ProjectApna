using System;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimVisitor : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, SearchPriority = 5)]
        public string display_name { get; set; }

        [FieldSettings("Email", DefaultField = true, SearchPriority = 4, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Phone", DefaultField = true, SearchPriority = 3, FieldTypeId = Connector.FieldTypeIdPhone)]
        public string phone { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Notes", SearchPriority = 2)]
        public string notes { get; set; }

        [FieldSettings("Banned", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public int banned { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_time => created;

        #endregion

    }
}
