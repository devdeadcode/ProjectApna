using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctUser
    {

        public enum IntacctUserType
        {
            business_user,
            employee_user,
            view_only_user,
            writeup_user,
            dashboard_user,
            project_manager_user,
            payment_approver,
            platform_user,
            CRM_user
        }
        public enum IntacctUserStatus
        {
            active
        }

        public string LOGINID { get; set; }
        public IntacctUserContactInfo CONTACTINFO { get; set; }
        public string DESCRIPTION { get; set; }
        public string USERTYPE { get; set; }
        public IntacctUserType USERTYPE_enum => (IntacctUserType)Enum.Parse(typeof(IntacctUserType), USERTYPE.Trim().Replace(' ', '_'));
        public bool ADMIN { get; set; }
        public IntacctUserStatus STATUS { get; set; }
        public bool PWDNEVEREXPIRES { get; set; }
        public bool PWDQLYNOTENFORCED { get; set; }
        public bool LOGINDISABLED { get; set; }
        public bool SSO_ENABLED { get; set; }
        public string SSO_FEDERATED_ID { get; set; }

    }
}