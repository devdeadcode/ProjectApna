namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookPaging
    {

        public FacebookPagingCursors cursors { get; set; }
        public string previous { get; set; }
        public string next { get; set; }
        public int limit { get; set; }

    }
}