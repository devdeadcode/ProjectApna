using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceUser : DataConnectorEntityModel
    {

        #region Enums

        public enum SalesForceUserDigestFrequency
        {
            // todo - get other digest frequencies
            [Description("Daily")]
            D
        }

        #endregion

        #region Default properties

        [FieldSettings("Username", DefaultField = true)]
        public string Username { get; set; }

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Company name", DefaultField = true)]
        public string CompanyName { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string Email { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID")]
        public string Id { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Division")]
        public string Division { get; set; }

        [FieldSettings("Department")]
        public string Department { get; set; }

        [FieldSettings("Title")]
        public string Title { get; set; }

        [FieldSettings("Street")]
        public string Street { get; set; }

        [FieldSettings("City")]
        public string City { get; set; }

        [FieldSettings("State")]
        public string State { get; set; }

        [FieldSettings("Postal code")]
        public string PostalCode { get; set; }

        [FieldSettings("Country")]
        public string Country { get; set; }

        [FieldSettings("Phone")]
        public string Phone { get; set; }

        [FieldSettings("Fax")]
        public string Fax { get; set; }

        [FieldSettings("Mobile phone")]
        public string MobilePhone { get; set; }

        [FieldSettings("Alias")]
        public string Alias { get; set; }

        [FieldSettings("Community nickname")]
        public string CommunityNickname { get; set; }

        [FieldSettings("Active")]
        public bool IsActive { get; set; }

        [FieldSettings("Timezone")]
        public string TimeZoneSidKey { get; set; }

        public string UserRoleId { get; set; }

        [FieldSettings("Locale")]
        public string LocaleSidKey { get; set; }

        [FieldSettings("Receives info emails")]
        public bool ReceivesInfoEmails { get; set; }

        [FieldSettings("Receives admin info emails")]
        public bool ReceivesAdminInfoEmails { get; set; }

        public string EmailEncodingKey { get; set; }

        public string ProfileId { get; set; }

        [FieldSettings("User type")]
        public string UserType { get; set; }

        [FieldSettings("Language")]
        public string LanguageLocaleKey { get; set; }

        [FieldSettings("Employee number")]
        public string EmployeeNumber { get; set; }

        public string DelegatedApproverId { get; set; }

        [FieldSettings("Marketing user")]
        public bool UserPermissionsMarketingUser { get; set; }

        [FieldSettings("Offline user")]
        public bool UserPermissionsOfflineUser { get; set; }

        [FieldSettings("Call center auto-login")]
        public bool UserPermissionsCallCenterAutoLogin { get; set; }

        [FieldSettings("Mobile user")]
        public bool UserPermissionsMobileUser { get; set; }

        [FieldSettings("Content user")]
        public bool UserPermissionsSFContentUser { get; set; }

        [FieldSettings("Knowledge user")]
        public bool UserPermissionsKnowledgeUser { get; set; }

        [FieldSettings("Interaction user")]
        public bool UserPermissionsInteractionUser { get; set; }

        [FieldSettings("Support user")]
        public bool UserPermissionsSupportUser { get; set; }

        [FieldSettings("Forecast enabled")]
        public bool ForecastEnabled { get; set; }

        public bool UserPreferencesActivityRemindersPopup { get; set; }
        public bool UserPreferencesEventRemindersCheckboxDefault { get; set; }
        public bool UserPreferencesTaskRemindersCheckboxDefault { get; set; }
        public bool UserPreferencesReminderSoundOff { get; set; }
        public bool UserPreferencesDisableAutoSubForFeeds { get; set; }
        public bool UserPreferencesApexPagesDeveloperMode { get; set; }
        public bool UserPreferencesHideCSNGetChatterMobileTask { get; set; }
        public bool UserPreferencesHideCSNDesktopTask { get; set; }
        public bool UserPreferencesOptOutOfTouch { get; set; }
        public string Extension { get; set; }
        public string FederationIdentifier { get; set; }

        [FieldSettings("About me")]
        public string AboutMe { get; set; }

        [FieldSettings("Current status")]
        public string CurrentStatus { get; set; }
        public string FullPhotoUrl { get; set; }
        public string SmallPhotoUrl { get; set; }

        [FieldSettings("Digest frequency", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(SalesForceUserDigestFrequency))]
        public SalesForceUserDigestFrequency DigestFrequency { get; set; }

        #endregion

        #region Hidden properties

        public string ManagerId { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime SystemModstamp { get; set; }
        public DateTime OfflineTrialExpirationDate { get; set; }
        public DateTime OfflinePdaTrialExpirationDate { get; set; }
        public string ContactId { get; set; }
        public string AccountId { get; set; }
        public string CallCenterId { get; set; }

        #endregion

        #region Calculations

        public string Address => BuildAddress(Street, City, State, PostalCode);

        #endregion

    }
}
