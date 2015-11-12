using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceCase : SalesForceEntity
    {

        #region Enums

        public enum SalesForceCaseOrigin
        {
            Email,
            Phone,
            Web
        }
        public enum SalesForceCasePriority
        {
            High,
            Medium,
            Low
        }
        public enum SalesForceCaseStatus
        {
            New,
            Closed,
            Escalated
        }

        #endregion

        #region Default properties

        [FieldSettings("Case number", DefaultField = true, Description = true, KeyNumber = 1)]
        public string CaseNumber { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string Subject { get; set; }

        [FieldSettings("Priority", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(SalesForceCasePriority))]
        public SalesForceCasePriority Priority { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(SalesForceCaseStatus))]
        public SalesForceCaseStatus Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Closed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime ClosedDate { get; set; }

        [FieldSettings("Creator name")]
        public string CreatorName { get; set; }

        [FieldSettings("Has unread comments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool HasCommentsUnreadByOwner { get; set; }

        [FieldSettings("Has self-service comments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool HasSelfServiceComments { get; set; }

        [FieldSettings("Closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsClosed { get; set;  }

        [FieldSettings("Closed on create", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsClosedOnCreate { get; set; }

        [FieldSettings("Escalated", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsEscalated { get; set; }

        [FieldSettings("Self-service closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsSelfServiceClosed { get; set; }

        [FieldSettings("Stopped", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsStopped { get; set; }

        [FieldSettings("Visible in self-service", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsVisibleInSelfService { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime LastReferencedDate { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime LastViewedDate { get; set; }

        [FieldSettings("Origin", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(SalesForceCaseOrigin))]
        public SalesForceCaseOrigin Origin { get; set; }

        [FieldSettings("Reason")]
        public string Reason { get; set; }

        [FieldSettings("SLA start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime SlaStartDate { get; set; }

        [FieldSettings("Stop start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime StopStartDate { get; set; }

        [FieldSettings("Supplied company")]
        public string SuppliedCompany { get; set; }

        [FieldSettings("Supplied email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string SuppliedEmail { get; set; }

        [FieldSettings("Supplied name")]
        public string SuppliedName { get; set; }

        [FieldSettings("Supplied phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string SuppliedPhone { get; set; }

        [FieldSettings("Type")]
        public string Type { get; set; }

        #endregion

        #region Hidden properties

        public string AccountId { get; set; }
        public string CommunityId { get; set; }
        public string ConnectionReceivedId { get; set; }
        public string ConnectionSentId { get; set; }
        public string ContactId { get; set; }
        public string FeedItemId { get; set; }
        public string RecordTypeId { get; set; }
        public string ParentId { get; set; }
        public string QuestionId { get; set; }
        public string CreatorSmallPhotoUrl { get; set; }
        public string CreatorFullPhotoUrl { get; set; }

        public SalesForceAccount Account { get; set; }
        public SalesForceContact Contact { get; set; }

        #endregion

        #region Calculations

        public string AccountName => Account.Name;
        public string ContactName => Contact.Name;

        #endregion

    }
}