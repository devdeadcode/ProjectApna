namespace eOne.Common
{
    /// <summary>
    /// Base connector class
    /// </summary>
    public abstract class Connector
    {

        #region Enums

        public enum ConnectorAuthorisationType
        {
            Company, 
            User, 
            Personal
        }
        public enum ConnectorGroup
        {
            ERP,
            CRM,
            Helpdesk,
            MailingList,
            Invoicing,
            Database,
            Forms,
            IssueTracking,
            LandingPage,
            Payments,
            Payroll,
            POS,
            ProjectManagement,
            ToDoList,
            SocialMedia,
            TimeTracking,
            WebStore,
            Chat,
            Other
        }

        #endregion

        #region Properties

        public int Id;

        public string Name;

        public ConnectorGroup Group;

        public string Username { get; set; }

        public string Password { get; set; }

        public ConnectorSetup Setup { get; set; }

        public ConnectorAuthorisationType AuthorisationType { get; set; }

        #endregion

        #region Methods

        public abstract void Initialise();

        public void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "Please enter in a name for your new connector below. ",
                BottomDescription = $"Click Next to grant access to your {Name} account."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, Name, true));
            Setup.Steps.Add(step1);
            var step2 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 2,
                Header = "Complete installation",
                BottomDescription = "Click Finish to complete the installation."
            };
            Setup.Steps.Add(step2);
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
