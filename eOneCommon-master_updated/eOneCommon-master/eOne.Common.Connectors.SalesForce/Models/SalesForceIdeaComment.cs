namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceIdeaComment : SalesForceEntity
    {

        [FieldSettings("Idea", DefaultField = true)]
        public string IdeaTitle => Idea == null ? string.Empty : Idea.Title;

        [FieldSettings("Comment", DefaultField = true, Description = true)]
        public string CommentBody { get; set; }

        [FieldSettings("Posted by", DefaultField = true)]
        public string CreatorName { get; set; }

        [FieldSettings("Votes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? UpVotes { get; set; }

        [FieldSettings("Comment ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Idea ID")]
        public string IdeaId { get; set; }

        [FieldSettings("Community ID")]
        public string CommunityId { get; set; }

        [FieldSettings("Is HTML")]
        public bool IsHtml { get; set; }

        public SalesForceIdea Idea { get; set; }

    }
}