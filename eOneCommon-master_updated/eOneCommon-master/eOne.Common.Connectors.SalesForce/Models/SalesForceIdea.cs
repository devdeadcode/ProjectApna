using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceIdea : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true, Description = true)]
        public string Title { get; set; }

        [FieldSettings("Posted by", DefaultField = true)]
        public string CreatorName { get; set; }

        [FieldSettings("Points", DefaultField = true, FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? VoteTotal { get; set; }

        [FieldSettings("Number of comments", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumComments { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Idea ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Description")]
        public string Body { get; set; }

        [FieldSettings("Categories")]
        public string Categories { get; set; }

        [FieldSettings("Status")]
        public string Status { get; set; }

        public string ParentIdeaId { get; set; }

        [FieldSettings("Last comment date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastCommentDate { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

        [FieldSettings("Is HTML", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsHtml { get; set; }

        [FieldSettings("Merged", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsMerged { get; set; }
        
        public decimal? VoteScore { get; set; }
        public string RecordTypeId { get; set; }
        public string CommunityId { get; set; }

        #endregion

    }
}