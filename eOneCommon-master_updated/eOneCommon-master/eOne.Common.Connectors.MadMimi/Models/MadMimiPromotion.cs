using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.MadMimi.Helpers;

namespace eOne.Common.Connectors.MadMimi.Models
{
    public class MadMimiPromotion : ConnectorEntityModel
    {

        #region Default properties

        [System.Xml.Serialization.XmlAttribute]
        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Number of mailings", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int number_of_mailings => mailings?.Count ?? 0;

        [FieldSettings("Total sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_sent => mailings?.Sum(mailing => mailing.count) ?? 0;

        #endregion

        #region Properties

        [System.Xml.Serialization.XmlAttribute]
        [FieldSettings("ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int id { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        [FieldSettings("Hidden", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool hidden { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        [FieldSettings("Thumbnail", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string thumbnail { get; set; }

        #endregion

        #region Hidden properties

        [System.Xml.Serialization.XmlAttribute]
        public string mimio { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public string updated_at { get; set; }

        [System.Xml.Serialization.XmlElement("mailing")]
        public List<MadMimiMailing> mailings { get; set; }

        public List<DateTime> send_dates => (from mailing in mailings where mailing.started_send_date != null select (DateTime)mailing.started_send_date).ToList();

        #endregion

        #region Calculations

        [FieldSettings("First send date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? first_send_date
        {
            get
            {
                if (send_dates.Count == 0) return null;
                return send_dates.Min();
            }
        }

        [FieldSettings("Last send date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? last_send_date
        {
            get
            {
                if (send_dates.Count == 0) return null;
                return send_dates.Min();
            }
        }

        [FieldSettings("Updated at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? updated_at_date => DataConversion.ParseDateTime(updated_at);

        #endregion

    }
}

