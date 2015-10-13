using System.Data;
using System.Globalization;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class SmartListBuilderModule : DynamicsGpModule
    {

        #region Constants

        const int SmartListFieldTypeIdInteger = 1;
        const int SmartListFieldTypeIdLongInteger = 2;
        const int SmartListFieldTypeIdDate = 3;
        const int SmartListFieldTypeIdCurrency = 4;
        const int SmartListFieldTypeIdString = 5;
        const int SmartListFieldTypeIdBoolean = 6;
        const int SmartListFieldTypeIdList = 7;
        const int SmartListFieldTypeIdIndex = 8;
        const int SmartListFieldTypeIdAccount = 9;
        const int SmartListFieldTypeIdPhone = 10;
        const int SmartListFieldTypeIdSsn = 11;
        const int SmartListFieldTypeIdTime = 12;

        #endregion

        public SmartListBuilderModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 3830;
            ParentConnector = connector;
            Installed = connector.ObjectExists("SLB10000", "U");
        }

        #region Methods

        public override void AddEntities()
        {
            string getSmartListSql = $"select * from {ParentConnector.SystemDatabase}..SLB10000";
            var smartLists = ParentConnector.GetDataSet(getSmartListSql).Tables[0];
            foreach (DataRow smartList in smartLists.Rows)
            {
                var smartListId = smartList["SmartList_ID"].ToString().Trim();
                var smartListNumber = int.Parse(smartList["SmartList_Number"].ToString());
                var smartListName = smartList["SmartList_Name"].ToString().Trim();
                var itemName = smartList["SmartList_Item_Name"].ToString().Trim();
                var summarySmartList = int.Parse(smartList["Summary_SmartList_CB"].ToString()) == 1;
                var productId = int.Parse(smartList["PRODID"].ToString());
                var seriesNumber = int.Parse(smartList["Series_Number"].ToString());
                var entity = new DataConnectorEntity(GetEntityId(smartListNumber), smartListName, ParentConnector)
                {
                    ItemName = itemName,
                    SummaryList = summarySmartList,
                    Group = FindGroup(productId, seriesNumber)
                };
                AddEntityTables(entity, smartListId);
                AddEntityFields(entity, smartListId);

                entity.ParentConnector = ParentConnector;
                ParentConnector.Entities.Add(entity);
            }
        }
        private void AddEntityTables(DataConnectorEntity entity, string smartlistId)
        {
            string getTableSql = $"select * from {ParentConnector.Database}..SLB10100 where SmartList_ID = {ParentConnector.FormatString(smartlistId)}";
            var tables = ParentConnector.GetDataSet(getTableSql).Tables[0];
            foreach (var name in from DataRow table in tables.Rows select table["TBLPHYSNM"].ToString().Trim()) entity.AddTable(name);
        }
        private void AddEntityFields(DataConnectorEntity entity, string smartlistId)
        {
            string getFieldSql = string.Format("select SLB10200.Table_Number, SLB10100.TBLPHYSNM as TableName, SLB10200.SmartList_Display_Name as FieldAlias, SLB10200.TBLPHYSNM as FieldName, " +
                                               "SLB10200.FIELDTYPE, SLB10200.Default_Display, SLB10200.TXTFIELD, SLB10200.Field_Number " +
                                               "from {0}..SLB10200 inner join {0}..SLB10100 on SLB10200.SmartList_ID = SLB10100.SmartList_ID and SLB10200.Table_Number = SLB10100.Table_Number " +
                                               "where SLB10200.SmartList_ID = {1} and SLB10200.Display_Field = 1", ParentConnector.Database, ParentConnector.FormatString(smartlistId));
            var fields = ParentConnector.GetDataSet(getFieldSql).Tables[0];
            foreach (DataRow field in fields.Rows)
            {
                var tableNumber = int.Parse(field["Table_Number"].ToString());
                var fieldNumber = int.Parse(field["Field_Number"].ToString());
                var tableName = field["TableName"].ToString().Trim();
                var fieldAlias = field["FieldAlias"].ToString().Trim();
                var defaultField = int.Parse(field["Default_Display"].ToString()) == 1;
                var fieldType = GetFieldType(int.Parse(field["FIELDTYPE"].ToString()), false);
                DataConnectorField newField;
                if (tableNumber == 0)
                {
                    // calculated field
                    var calculation = field["TXTFIELD"].ToString().Trim();
                    newField = entity.AddCalculation(calculation, fieldAlias, fieldType, defaultField);
                }
                else
                {
                    var fieldName = field["FieldName"].ToString().Trim();
                    newField = entity.AddField(fieldName, fieldAlias, fieldType, tableName, defaultField);
                }
                if (fieldType == DataConnector.FieldTypeIdEnum) AddListItems(smartlistId, tableNumber, fieldNumber, newField);
            }
        }
        private void AddListItems(string smartlistId, int tableNumber, int fieldNumber, DataConnectorField field)
        {
            string getListItemSql = "select LNITMSEQ, STRGA255 from {0}..SLB10700 where SmartList_ID = {1} and Table_Number = {2} and Field_Number = {3}";
            getListItemSql = string.Format(getListItemSql, ParentConnector.Database, ParentConnector.FormatString(smartlistId), tableNumber, fieldNumber);
            var listItems = ParentConnector.GetDataSet(getListItemSql).Tables[0];
            foreach (DataRow listItem in listItems.Rows)
            {
                var item = int.Parse(listItem["LNITMSEQ"].ToString());
                var value = listItem["STRGA255"].ToString().Trim();
                field.ListItems.Add(new DataConnectorField.ConnectorFieldListItem(item.ToString(CultureInfo.InvariantCulture), value));
            }
        }

        private static int GetFieldType(int smartlistFieldType, bool showCurrencySymbol)
        {
            switch (smartlistFieldType)
            {
                case SmartListFieldTypeIdInteger:
                case SmartListFieldTypeIdLongInteger:
                    return DataConnector.FieldTypeIdInteger;
                case SmartListFieldTypeIdDate:
                    return DataConnector.FieldTypeIdDate;
                case SmartListFieldTypeIdCurrency:
                    return showCurrencySymbol ? DataConnector.FieldTypeIdCurrency : DataConnector.FieldTypeIdQuantity;
                case SmartListFieldTypeIdString:
                    return DataConnector.FieldTypeIdString;
                case SmartListFieldTypeIdBoolean:
                    return DataConnector.FieldTypeIdYesNo;
                case SmartListFieldTypeIdList:
                    return DataConnector.FieldTypeIdEnum;
                case SmartListFieldTypeIdIndex:
                case SmartListFieldTypeIdAccount:
                    return DynamicsGpConnector.FieldTypeIdAccountIndex;
                case SmartListFieldTypeIdPhone:
                    return DataConnector.FieldTypeIdPhone;
                case SmartListFieldTypeIdSsn:
                    return DataConnector.FieldTypeIdString;
                case SmartListFieldTypeIdTime:
                    return DataConnector.FieldTypeIdTime;
            }
            return 0;
        }
        private static DataConnectorEntityGroup FindGroup(int productId, int seriesNumber)
        {
            return null;
        }

        #endregion

    }
}
