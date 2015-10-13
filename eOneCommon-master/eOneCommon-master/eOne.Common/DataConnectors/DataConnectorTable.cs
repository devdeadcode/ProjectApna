using System;
using System.Collections.Generic;

namespace eOne.Common.DataConnectors
{
    public class DataConnectorTable
    {

        #region Enums

        public enum DataConnectorTableJoinType { Inner, Left }

        #endregion

        #region Classes

        public class DataConnectorTableUnion
        {
            public DataConnectorTableUnion()
            {
                Fields = new List<Tuple<string, string>>();
            }
            public string Name { get; set; }
            public string Server { get; set; }
            public string Script { get; set; }
            public string Alias { get; set; }
            public List<Tuple<string, string>> Fields { get; set; }
        }

        #endregion

        public DataConnectorTable()
        {
            JoinFields = new List<Tuple<string, string>>();
            UnionTables = new List<DataConnectorTableUnion>();
        }

        #region Properties

        public string Name { get; set; }
        public string Database { get; set; }
        public string Script { get; set; }
        public string Alias { get; set; }
        public DataConnectorTable JoinToTable { get; set; }
        public List<Tuple<string, string>> JoinFields { get; set; }
        public DataConnectorTableJoinType JoinType { get; set; }
        public DataConnectorEntity ParentEntity { get; set; }
        public List<DataConnectorTableUnion> UnionTables { get; set; }

        #endregion

        #region Methods

        public DataConnectorField AddField(string name, string displayName, int typeId, bool defaultField = false)
        {
            return ParentEntity.AddField(name, displayName, typeId, Name, defaultField);
        }
        public void AddJoinFields(string fromField, string toField)
        {
            JoinFields.Add(new Tuple<string, string>(fromField, toField));
        }

        #endregion

    }
}
