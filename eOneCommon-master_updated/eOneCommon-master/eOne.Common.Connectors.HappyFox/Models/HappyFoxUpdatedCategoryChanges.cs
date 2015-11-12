namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedCategoryChanges : ConnectorEntityModel
    {
        public string from_category { get; set; }

        public string to_category { get; set; }
    }
}
