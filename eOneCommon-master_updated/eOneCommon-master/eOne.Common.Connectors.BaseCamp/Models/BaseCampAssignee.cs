namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampAssignee
    {

        public enum BaseCampAssigneeType
        {
            Person
        }

        public int id { get; set; }
        public BaseCampAssigneeType type { get; set; }
        public string name { get; set; }

    }
}