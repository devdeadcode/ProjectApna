using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Helpers
{
    public class Comparison
    {

        public static IEnumerable<object> GetDifferences(IEnumerable<object> records, Type type, IEnumerable<object> compareRecords, Type compareType, List<Tuple<string, string>> fields)
        {
            if (fields.Count <= 0) return records;
            var comparison = records;
            foreach (var fieldPair in fields)
            {
                var propertyInfo = type.GetProperty(fieldPair.Item1);
                var comparePropertyInfo = compareType.GetProperty(fieldPair.Item2);
                comparison = (from record in comparison where !(compareRecords.Any(compareRecord => comparePropertyInfo.GetValue(compareRecord, null) == propertyInfo.GetValue(record, null))) select record).ToList();
            }
            return comparison;
        }

        public static IEnumerable<object> GetSimilarities(IEnumerable<object> records, Type type, IEnumerable<object> compareRecords, Type compareType, List<Tuple<string, string>> fields)
        {
            if (fields.Count <= 0) return records;
            var comparison = records;
            foreach (var fieldPair in fields)
            {
                var propertyInfo = type.GetProperty(fieldPair.Item1);
                var comparePropertyInfo = compareType.GetProperty(fieldPair.Item2);
                comparison = (from record in comparison where compareRecords.Any(compareRecord => comparePropertyInfo.GetValue(compareRecord, null) == propertyInfo.GetValue(record, null)) select record).ToList();
            }
            return comparison;
        }

    }
}
