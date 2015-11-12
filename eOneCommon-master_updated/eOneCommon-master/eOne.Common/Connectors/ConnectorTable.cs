using System;
using System.Collections.Generic;
using eOne.Common.Helpers;

namespace eOne.Common.Connectors
{
    public class ConnectorTable
    {

        #region Enums

        public enum ConnectorTableJoinType { Inner, Left }

        #endregion

        #region Classes

        public class ConnectorTableUnion
        {
            public ConnectorTableUnion()
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

        public ConnectorTable()
        {
            JoinFields = new List<Tuple<string, string>>();
            UnionTables = new List<ConnectorTableUnion>();
        }

        #region Properties

        public string Name { get; set; }
        public string Database { get; set; }
        public string Script { get; set; }
        public string Alias { get; set; }
        public ConnectorTable JoinToTable { get; set; }
        public List<Tuple<string, string>> JoinFields { get; set; }
        public ConnectorTableJoinType JoinType { get; set; }
        public ConnectorEntity ParentEntity { get; set; }
        public List<ConnectorTableUnion> UnionTables { get; set; }

        #endregion

        #region Methods

        public ConnectorField AddField(string name, string displayName, int typeId, bool defaultField = false)
        {
            return ParentEntity.AddField(name, displayName, typeId, Name, defaultField);
        }
        public void AddJoinFields(string fromField, string toField)
        {
            TupleHelper.AppendTupleStringList(JoinFields, fromField, toField);
        }

        #endregion

    }
}
