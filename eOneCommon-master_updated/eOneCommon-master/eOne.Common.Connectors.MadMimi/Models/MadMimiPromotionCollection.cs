using System.Collections.Generic;

namespace eOne.Common.Connectors.MadMimi.Models
{

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("promotions", Namespace = "", IsNullable = false)]
    public class MadMimiPromotionCollection
    {
        [System.Xml.Serialization.XmlElementAttribute("promotion")]
        public List<MadMimiPromotion> promotion { get; set; }
    }

}