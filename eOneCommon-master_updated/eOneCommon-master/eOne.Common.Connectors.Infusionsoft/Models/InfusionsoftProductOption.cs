namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftProductOption : ConnectorEntityModel
    {

        public int? AllowSpaces { get; set; }
        public string CanContain { get; set; }
        public int? CanEndWith { get; set; }
        public string CanStartWith { get; set; }
        public int? Id { get; set; }
        public int? IsRequired { get; set; }
        public string Label { get; set; }
        public int? MaxChars { get; set; }
        public int? MinChars { get; set; }
        public string Name { get; set; }
        public string OptionType { get; set; }
        public int? Order { get; set; }
        public int? ProductId { get; set; }
        public int? TextMessage { get; set; }


    }
}
