namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketPriority : ConnectorEntityModel
    {
        public bool @default { get; set; }

        public string name { get; set; }

        public int id { get; set; }

        public int order { get; set; }
    }
}
