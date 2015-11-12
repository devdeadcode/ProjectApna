namespace eOne.Common.Connectors.MadMimi.Models
{

    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot("mailing", Namespace = "", IsNullable = false)]
    public class MadMimiMailingStatistics
    {

        public int sent { get; set; }

        public int views { get; set; }

        public int untraced { get; set; }

        public int clicked { get; set; }

        public int forwarded { get; set; }

        public int bounced { get; set; }

        public int unsubscribed { get; set; }

        public int abused { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public bool all_audience { get; set; }

        //[System.Xml.Serialization.XmlArrayItem("link", IsNullable = false)]
        //public mailingLink[] links { get; set; }

        //public mailingAudience_lists audience_lists { get; set; }

        //[System.Xml.Serialization.XmlType(AnonymousType = true)]
        //public partial class mailingLink
        //{
        //    /// <remarks/>
        //    public byte clicks { get; set; }

        //    /// <remarks/>
        //    [System.Xml.Serialization.XmlAttributeAttribute()]
        //    public string url { get; set; }

        //    /// <remarks/>
        //    [System.Xml.Serialization.XmlAttributeAttribute()]
        //    public uint id { get; set; }
        //}

        //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        //public partial class mailingAudience_lists
        //{
        //    /// <remarks/>
        //    public mailingAudience_listsAudience_list audience_list { get; set; }
        //}

        //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        //public partial class mailingAudience_listsAudience_list
        //{
        //    /// <remarks/>
        //    [System.Xml.Serialization.XmlAttributeAttribute()]
        //    public uint id { get; set; }

        //    /// <remarks/>
        //    [System.Xml.Serialization.XmlAttributeAttribute()]
        //    public string name { get; set; }
        //}

    }
}
