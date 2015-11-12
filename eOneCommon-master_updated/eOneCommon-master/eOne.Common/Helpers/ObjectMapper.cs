using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Helpers
{
    public class ObjectMapper
    {

        public static void Map(IEnumerable<object> fromList, Type fromType, IEnumerable<object> toList, Type toType, string toObjectName, List<Tuple<string, string>> mapFields)
        {
            if (fromList == null || toList == null) return;
            var propertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(toType, toObjectName);
            var toEnumerable = toList as IList<object> ?? toList.ToList();
            foreach (var fromItem in fromList)
            {
                var fromMapFieldValues = GetFieldValues(fromItem, fromType, TupleHelper.CreateStringListFromTupleItem(mapFields, 1));
                foreach (var toItem in toEnumerable)
                {
                    var toMapFieldValues = GetFieldValues(toItem, toType, TupleHelper.CreateStringListFromTupleItem(mapFields, 2));
                    if (!fromMapFieldValues.Except(toMapFieldValues).Any()) propertyInfo.SetValue(toItem, fromItem);
                }
            }
        }

        private static List<object> GetFieldValues(object item, Type type, IEnumerable<string> fieldNames)
        {
            return fieldNames.Select(fieldName => FieldPropertyHelper.GetFieldPropertyInfo(type, fieldName)).Select(propertyInfo => propertyInfo.GetValue(item, null)).ToList();
        }

    }
}
