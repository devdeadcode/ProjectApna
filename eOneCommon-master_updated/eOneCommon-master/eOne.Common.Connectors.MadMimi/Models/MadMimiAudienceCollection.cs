using System.Collections.Generic;

namespace eOne.Common.Connectors.MadMimi.Models
{

    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot("audience", Namespace = "", IsNullable = false)]
    public class MadMimiAudienceCollection
    {

        public List<MadMimiAudience> member { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public int count { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public int total_pages { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public int per_page { get; set; }

        [System.Xml.Serialization.XmlAttribute]
        public int page { get; set; }

    }
}
