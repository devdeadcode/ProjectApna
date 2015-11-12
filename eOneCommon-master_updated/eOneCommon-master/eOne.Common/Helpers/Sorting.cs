using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors;
using eOne.Common.Query;

namespace eOne.Common.Helpers
{
    class Sorting
    {

        public static List<object> SortRecords(List<object> records, Type type, List<Tuple<ConnectorField, ConnectorQuery.ConnectorQuerySortOrder>> fields)
        {
            // handle null record sets
            if (records == null) return null;

            // reverse the order of the sort fields, so the sorts are applied last to first
            fields.Reverse();

            // sort the records
            foreach (var field in fields)
            {
                var propertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, field.Item1.Name);
                switch (field.Item2)
                {
                    case ConnectorQuery.ConnectorQuerySortOrder.Ascending:
                        records = records.OrderBy(record => propertyInfo.GetValue(record, null)).ToList();
                        break;
                    case ConnectorQuery.ConnectorQuerySortOrder.Descending:
                        records = records.OrderByDescending(record => propertyInfo.GetValue(record, null)).ToList();
                        break;
                }
            }

            return records;
        }

        public static Tuple<ConnectorField, ConnectorQuery.ConnectorQuerySortOrder> BuildSortTuple(ConnectorField field, bool ascending = true)
        {
            var sortOrder = ascending ? ConnectorQuery.ConnectorQuerySortOrder.Ascending : ConnectorQuery.ConnectorQuerySortOrder.Descending;
            return new Tuple<ConnectorField, ConnectorQuery.ConnectorQuerySortOrder>(field, sortOrder);
        }

    }
}
