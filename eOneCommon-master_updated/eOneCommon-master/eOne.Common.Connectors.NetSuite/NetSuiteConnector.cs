using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.NetSuite
{
    public class NetSuiteConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdAccount = 1;
        public const int EntityIdCustomer = 2;
        public const int EntityIdVendor = 3;
        public const int EntityIdInventoryItem = 4;

        #endregion

        #region Action IDs



        #endregion

        #endregion

        public NetSuiteConnector()
        {
            Name = "NetSuite";
            Group = ConnectorGroup.ERP;
            SecondaryGroup = ConnectorGroup.CRM;
        }

        public override void Initialise()
        {
            base.Initialise();
            AddEntities();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            throw new NotImplementedException();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        private void AddEntities()
        {
            var accounts = AddEntity(EntityIdAccount, "Accounts");
            var customers = AddEntity(EntityIdCustomer, "Customers");
            var vendors = AddEntity(EntityIdVendor, "Vendors");
            var invetoryItems = AddEntity(EntityIdInventoryItem, "Items");
        }

    }
}
