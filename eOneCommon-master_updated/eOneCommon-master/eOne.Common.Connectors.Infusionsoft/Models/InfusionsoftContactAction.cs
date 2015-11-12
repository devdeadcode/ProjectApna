using System;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftContactAction : ConnectorEntityModel
    {

        public int? Accepted { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ActionDescription { get; set; }
        public string ActionType { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? ContactId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationNotes { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Id { get; set; }
        public int? IsAppointment { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string ObjectType { get; set; } // change to enum
        public int? OpportunityId { get; set; }
        public DateTime? PopupDate { get; set; }
        public int? Priority { get; set; }
        public int? UserID { get; set; }

    }
}
