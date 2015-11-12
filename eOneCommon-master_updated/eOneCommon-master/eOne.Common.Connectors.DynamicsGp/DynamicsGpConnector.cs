using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Database;
using eOne.Common.Connectors.DynamicsGp.Modules;
using eOne.Common.Setup;

namespace eOne.Common.Connectors.DynamicsGp
{
    public class DynamicsGpConnector : SqlServerConnector
    {

        public const int FieldTypeIdAccountIndex = 101;
        public const int FieldTypeIdNoteIndex = 102;

        public DynamicsGpConnector()
        {
            Name = "Dynamics GP";
            Group = ConnectorGroup.ERP;

            var accountIndexFieldType = new ConnectorFieldType(FieldTypeIdAccountIndex, "Account index", typeof(string), 0, 0)
            {
                SqlFunction = "isnull((select top 1 rtrim(ACTNUMST) from {1}..GL00105 where ACTINDX = {0}),'')"
            };
            FieldTypes.Add(accountIndexFieldType);
            var noteIndexFieldType = new ConnectorFieldType(FieldTypeIdNoteIndex, "Note index", typeof(string))
            {
                SqlFunction = "isnull((select top 1 TXTFIELD from {1}..SY03900 where NOTEINDX = {0}),'')"
            };
            FieldTypes.Add(noteIndexFieldType);
            Multicompany = true;
            CompanyPrompt = "Company";
            CompanyPluralPrompt = "Companies";
            AddSetup();
            Modules = new List<DynamicsGpModule>();
        }
        public new void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "",
                BottomDescription = "Click Finish to complete the installation. It will take a couple of minutes while PopDock connects to your Dynamics GP database and sets up your lists."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, "Dynamics GP", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameServer, "Server", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameUser, "Username", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            var password = new ConnectorSetupField(ConnectorSetupField.FieldNamePassword, "Password", ConnectorSetupField.ConnectorSetupFieldType.String, "", true)
            {
                StringOptions = {Password = true}
            };
            step1.Fields.Add(password);
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameDatabase, "System database", ConnectorSetupField.ConnectorSetupFieldType.String, "DYNAMICS", true));
            Setup.Steps.Add(step1);
        }

        public string SystemDatabase { get; set; }
        public List<DynamicsGpModule> Modules { get; set; } 

        public override void Initialise()
        {
            base.Initialise();

            AddModules();
            foreach (var module in Modules.Where(module => module.Installed))
            {
                var group = AddGroup(module.Id, module.Name);
                foreach (var entity in module.Entities)
                {
                    entity.Group = group;
                    entity.ParentConnector = this;
                    entity.DefaultMaxRecords = 1000;
                    Entities.Add(entity);
                }
            }
            AddCompanies();
        }

        private void AddModules()
        {
            Modules.Add(new DynamicsGpFinancialModule(this));
            Modules.Add(new DynamicsGpSalesModule(this));
            Modules.Add(new DynamicsGpPurchasingModule(this));
            Modules.Add(new DynamicsGpInventoryModule(this));
            Modules.Add(new DynamicsGpPayrollModule(this));
            Modules.Add(new DynamicsGpCompanyModule(this));
            Modules.Add(new FieldServiceModule(this));
            Modules.Add(new ProjectAccountingModule(this));
            Modules.Add(new ManufacturingModule(this));
            Modules.Add(new HumanResourcesModule(this));
            Modules.Add(new FixedAssetsModule(this));
            Modules.Add(new CollectionsManagementModule(this));
            Modules.Add(new AnalyticalAccountingModule(this));
            //Modules.Add(new ExtenderModule(this));
            Modules.Add(new SmartListBuilderModule(this));
            foreach (var module in Modules.Where(module => module.Installed)) module.AddEntities();
        }
        private void AddCompanies()
        {
            string getCompanySql = $"select CMPANYID, CMPNYNAM, INTERID from {Database}..SY01500";
            var companies = GetDataSet(getCompanySql).Tables[0];
            foreach (DataRow company in companies.Rows)
            {
                var companyId = int.Parse(company["CMPANYID"].ToString());
                var companyName = company["CMPNYNAM"].ToString().Trim();
                var database = company["INTERID"].ToString().Trim();
                var newCompany = AddCompany(companyId, companyName, database);
                newCompany.TestCompany = (companyId == -1);
            }
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

    }
}
