using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Intacct.Helpers;
using eOne.Common.Connectors.Intacct.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Intacct
{
    public class IntacctConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdCustomer = 1;
        public const int EntityIdVendor = 2;
        public const int EntityIdItem = 3;
        public const int EntityIdContact = 4;
        public const int EntityIdProject = 5;
        public const int EntityIdProjectResource = 6;
        public const int EntityIdGlAccount = 7;
        public const int EntityIdStatAccount = 8;
        public const int EntityIdDepartment = 9;
        public const int EntityIdUser = 10;
        public const int EntityIdApBill = 11;
        public const int EntityIdApBillLine = 12;
        public const int EntityIdBudget = 13;
        public const int EntityIdJournalEntries = 14;
        public const int EntityIdProjectTask = 15;
        public const int EntityIdDeposit = 16;
        public const int EntityIdChargeCardTransaction = 17;
        public const int EntityIdFundsTransfer = 18;
        public const int EntityIdOtherReceipt = 19;
        public const int EntityIdCreditCardFee = 20;
        public const int EntityIdBankInterest = 21;
        public const int EntityIdManualCheck = 22;
        public const int EntityIdStatJournalEntry = 23;
        public const int EntityIdRecurringJournalEntry = 24;
        public const int EntityIdRecurringApBill = 25;
        public const int EntityIdArInvoice = 26;
        public const int EntityIdTimesheet = 27;
        public const int EntityIdEmployee = 28;
        public const int EntityIdLocation = 29;
        public const int EntityIdEmployeeExpense = 30;
        public const int EntityIdEmployeeExpenseEntry = 31;
        public const int EntityIdTimesheetDetail = 32;
        public const int EntityIdPriceList = 33;
        public const int EntityIdSalesDocument = 34;
        public const int EntityIdSalesDocumentItem = 35;
        public const int EntityIdInventoryDocument = 36;

        #endregion

        #endregion

        public IntacctConnector()
        {
            Name = "Intacct";
            Group = ConnectorGroup.ERP;
            ConnectorMethod = RestConnectorMethod.Post;
            BaseUrl = "https://api.intacct.com/ia/xml/xmlgw.phtml";
            AuthenticationType = ServiceConnectorAuthenticationType.Custom;
            Multicompany = true;
        }

        #region Methods

        public override void Initialise()
        {
            DefaultFavorites.Clear();
            Entities.Clear();
            Groups.Clear();
            AddEntities();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            return string.Empty;
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            var xmlRequest = RequestHelper.GetRequestXml(query);
            return TupleHelper.CreateTupleStringList("xmlrequest", xmlRequest);
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdCustomer:
                    return DeserializeJson<List<IntacctCustomer>>(data, query);
                case EntityIdVendor:
                    return DeserializeJson<List<IntacctVendor>>(data, query);
                case EntityIdItem:
                    return DeserializeJson<List<IntacctItem>>(data, query);
                case EntityIdContact:
                    return DeserializeJson<List<IntacctContact>>(data, query);
                case EntityIdGlAccount:
                    return DeserializeJson<List<IntacctGlAccount>>(data, query);
                case EntityIdStatAccount:
                    return DeserializeJson<List<IntacctStatisticalAccount>>(data, query);
                case EntityIdUser:
                    return DeserializeJson<List<IntacctUser>>(data, query);
                case EntityIdDepartment:
                    return DeserializeJson<List<IntacctDepartment>>(data, query);
                case EntityIdApBill:
                    return DeserializeJson<List<IntacctApBill>>(data, query);
                case EntityIdProjectTask:
                    return DeserializeJson<List<IntacctProjectTask>>(data, query);
                case EntityIdEmployee:
                    return DeserializeJson<List<IntacctEmployee>>(data, query);
                case EntityIdLocation:
                    return DeserializeJson<List<IntacctLocation>>(data, query);
                case EntityIdTimesheet:
                    return DeserializeJson<List<IntacctTimesheet>>(data, query);
                case EntityIdTimesheetDetail:
                    return DeserializeJson<List<IntacctTimesheetEntry>>(data, query);
                case EntityIdEmployeeExpense:
                    return DeserializeJson<List<IntacctEmployeeExpense>>(data, query);
                case EntityIdPriceList:
                    return DeserializePriceList(data, query);
                case EntityIdSalesDocument:
                    return DeserializeJson<List<IntacctSalesDocument>>(data, query);
                case EntityIdInventoryDocument:
                    return DeserializeJson<List<IntacctInventoryDocument>>(data, query);
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helpers

        private void AddEntities()
        {
            // add groups
            var salesGroup = AddGroup(1, "Accounts receivable");
            var purchasingGroup = AddGroup(2, "Accounts payable");
            var inventoryGroup = AddGroup(3, "Inventory");
            var projectGroup = AddGroup(4, "Projects");
            var financialGroup = AddGroup(5, "General ledger");
            var companyGroup = AddGroup(6, "Company");
            var cashGroup = AddGroup(7, "Cash management");
            var timeExpenseGroup = AddGroup(8, "Time and expense");

            // add entities
            var customerEntity = AddEntity(EntityIdCustomer, "Customers", "CUSTOMER", typeof(IntacctCustomer), salesGroup);
            var vendorEntity = AddEntity(EntityIdVendor, "Vendors", "VENDOR", typeof(IntacctVendor), purchasingGroup);
            var itemEntity = AddEntity(EntityIdItem, "Items", "ITEM", typeof(IntacctItem), inventoryGroup);
            var contactEntity = AddEntity(EntityIdContact, "Contacts", "CONTACT", typeof(IntacctContact), companyGroup);
            var projectEntity = AddEntity(EntityIdProject, "Projects", "PROJECT", typeof(IntacctProject), projectGroup);
            var projectResourceEntity = AddEntity(EntityIdProjectResource, "Project Resources", "PROJECTRESOURCES", typeof(IntacctProjectResource), projectGroup);
            var accountEntity = AddEntity(EntityIdGlAccount, "Accounts", "GLACCOUNT", typeof(IntacctGlAccount), financialGroup);
            var statAccountEntity = AddEntity(EntityIdStatAccount, "Statistical accounts", "STATACCOUNT", typeof(IntacctStatisticalAccount), financialGroup);
            var userEntity = AddEntity(EntityIdUser, "Users", "USERINFO", typeof(IntacctUser), companyGroup);
            var departmentEntity = AddEntity(EntityIdDepartment, "Departments", "DEPARTMENT", typeof(IntacctDepartment), companyGroup);
            var projectTaskEntity = AddEntity(EntityIdProjectTask, "Tasks", "TASK", typeof(IntacctProjectTask), projectGroup);
            var employeeEntity = AddEntity(EntityIdEmployee, "Employees", "EMPLOYEE", typeof(IntacctEmployee), timeExpenseGroup);
            var locationEntity = AddEntity(EntityIdLocation, "Locations", "LOCATION", typeof(IntacctLocation), companyGroup);
            var priceListEntity = AddEntity(EntityIdPriceList, "Price lists", "SOPRICELISTENTRY", typeof(IntacctPriceListDetail), inventoryGroup);
            var salesDocumentEntity = AddEntity(EntityIdSalesDocument, "Sales documents", "SODOCUMENT", typeof(IntacctSalesDocument), salesGroup);
            var inventoryDocumentEntity = AddEntity(EntityIdInventoryDocument, "Inventory documents", "INVDOCUMENT", typeof(IntacctInventoryDocument), inventoryGroup);

            var timesheetEntity = AddEntity(EntityIdTimesheet, "Timesheets", "TIMESHEET", typeof(IntacctTimesheet), timeExpenseGroup);
            timesheetEntity.RemoveField("STATUS");

            var timesheetEntryEntity = AddEntity(EntityIdTimesheetDetail, "Timesheet entries", "TIMESHEETENTRY", typeof(IntacctTimesheetEntry), timeExpenseGroup);
            timesheetEntryEntity.RemoveField("STATUS");

            var expenseEntity = AddEntity(EntityIdEmployeeExpense, "Employee expenses", "EEXPENSES", typeof(IntacctEmployeeExpense), timeExpenseGroup);
            expenseEntity.RemoveField("STATUS");
            ChangeCreateDateName(expenseEntity, "Date filed");

            var billEntity = AddEntity(EntityIdApBill, "Bills", "APBILL", typeof(IntacctApBill), purchasingGroup);
            ChangeCreateDateName(billEntity, "Bill date");

            // add relationships
            customerEntity.AddRelatedEntity("Projects", projectEntity, "CUSTOMERID", "CUSTOMERID");
            projectEntity.AddRelatedEntity("Resources", projectResourceEntity, "PROJECTID", "PROJECTID");
            projectEntity.AddRelatedEntity("Tasks", projectTaskEntity, "PROJECTID", "PROJECTID");
            vendorEntity.AddRelatedEntity("Bills", billEntity, "VENDORID", "VENDORID");
            departmentEntity.AddRelatedEntity("Employees", employeeEntity, "DEPARTMENTID", "DEPARTMENTID");
            locationEntity.AddRelatedEntity("Employees", employeeEntity, "LOCATIONID", "LOCATIONID");
            employeeEntity.AddRelatedEntity("Expenses", expenseEntity, "EMPLOYEEID", "EMPLOYEEID");
            employeeEntity.AddRelatedEntity("Timesheets", timesheetEntity, "EMPLOYEEID", "EMPLOYEEID");
            timesheetEntity.AddRelatedEntity("Entries", timesheetEntryEntity, "RECORDNO", "RECORDNO");
            itemEntity.AddRelatedEntity("Price lists", priceListEntity, "ITEMID", "ITEMID");
            customerEntity.AddRelatedEntity("Sales documents", salesDocumentEntity, "CUSTOMERID", "CUSTVENDID");

            // add favorites
            FavoriteHelper.AddApBillFavorites(billEntity);

            // set default max records
            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;
        }
        private ConnectorEntity AddEntity(int id, string name, string endpoint, Type model, ConnectorEntityGroup group)
        {
            var entity = AddEntity(id, name, model);
            entity.Endpoint = endpoint;
            entity.Group = group;
            return entity;
        }
        private static void ChangeCreateDateName(ConnectorEntity entity, string name)
        {
            var createDate = entity.FindField("WHENCREATED");
            if (createDate != null)
            {
                createDate.DisplayName = name;
                createDate.CreateDate = false;
            }
        }

        private List<IntacctPriceListDetail> DeserializePriceList(string data, ConnectorQuery query)
        {
            var priceListItems = DeserializeJson<List<IntacctPriceListDetail>>(data, query);
            // get items and price lists
            var items = DeserializeJson<List<IntacctItem>>(GetRequestData("ITEM"));
            var priceLists = DeserializeJson<List<IntacctPriceList>>(GetRequestData("SOPRICELIST"));
            // assign items and price lists to price list items
            foreach (var priceListItem in priceListItems)
            {
                foreach (var item in items.Where(item => item.ITEMID == priceListItem.ITEMID)) priceListItem.Item = item;
                foreach (var priceList in priceLists.Where(priceList => priceList.NAME == priceListItem.PRICELISTID)) priceListItem.PriceList = priceList;
            }
            // todo - create additional items from base price list

            return priceListItems;
        }
        private string GetRequestData(string endpoint, string fields = "*", string query = "")
        {
            var xmlRequest = RequestHelper.GetRequestXml(this, endpoint, fields, query);
            var parameters = TupleHelper.CreateTupleStringList("xmlrequest", xmlRequest);
            return GetResponse(string.Empty, parameters);
        }

        #endregion

    }
}
