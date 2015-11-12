namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxStatus : ConnectorEntityModel
    {
        public bool @default { get; set; }

        public string name { get; set; }

        public string behavior { get; set; }

        public int id { get; set; }

        public string color { get; set; }


    }
}
