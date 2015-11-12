namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxCategory : ConnectorEntityModel
    {
        public int id { get; set; }
        
        public string description { get; set; }
        
        public string name { get; set; }
        
        public bool @public { get; set; } 
    }
}
