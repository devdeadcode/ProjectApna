using System;
using eOne.Common.Connectors.Sendloop.Helpers;

namespace eOne.Common.Connectors.Sendloop.Models
{
    public class SendloopCampaign
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string CampaignName { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string Subject { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string CampaignStatus { get; set; }

        [FieldSettings("Total recipients", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalRecipients { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Campaign ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int CampaignID { get; set; }

        [FieldSettings("From name")]
        public string FromName { get; set; }

        [FieldSettings("From email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string FromEmail { get; set; }

        [FieldSettings("Reply to name")]
        public string ReplyToName { get; set; }

        [FieldSettings("Reply to email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string ReplyToEmail { get; set; }

        [FieldSettings("Total failed", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalFailed { get; set; }

        [FieldSettings("Total opens", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalOpens { get; set; }

        [FieldSettings("Unique opens", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int UniqueOpens { get; set; }

        [FieldSettings("Total clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalClicks { get; set; }

        [FieldSettings("Unique clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int UniqueClicks { get; set; }

        [FieldSettings("Total forwards", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalForwards { get; set; }

        [FieldSettings("Unique forwards", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int UniqueForwards { get; set; }

        [FieldSettings("Total views on browser", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalViewsOnBrowser { get; set; }

        [FieldSettings("Unique views on browser", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int UniqueViewsOnBrowser { get; set; }

        [FieldSettings("Total unsubscriptions", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalUnsubscriptions { get; set; }

        [FieldSettings("Total hard bounces", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalHardBounces { get; set; }

        [FieldSettings("Total soft bounces", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalSoftBounces { get; set; }

        [FieldSettings("Schedule type")]
        public string ScheduleType { get; set; }

        #endregion

        public string GoogleAnalyticsEnable { get; set; }
        public string GoogleAnalyticsDomains { get; set; }
        public string PublicTinyContentLink { get; set; }
        public bool ThumbnailURL { get; set; }
        public string FetchURL { get; set; }
        public string PlainContent { get; set; }
        public string HTMLContent { get; set; }

        #region Hidden properties

        public string SendDate { get; set; }
        public string SendTime { get; set; }
        public string SendTimeZone { get; set; }
        public string SendProcessFinishedOn { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Total delivered", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalDelivered => TotalRecipients - TotalFailed;

        [FieldSettings("Total bounces", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TotalBounces => TotalHardBounces + TotalSoftBounces;

        [FieldSettings("Send date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? SendDateValue => DataConversion.ParseDateTime(SendDate);

        [FieldSettings("Finished on date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? SendProcessFinishedOnDateValue => DataConversion.ParseDateTime(SendProcessFinishedOn);

        #endregion

    }
}