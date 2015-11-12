using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace eOne.Common.Helpers
{
    public class Summary
    {

        public static List<object> Summarize(List<object> records, Type recordType, List<string> groupFields, List<string> summaryFields)
        {
            if (records == null) return null;
            if (groupFields == null || groupFields.Count == 0) return records;

            var summaryRecords = ObjectHelper.CreateListObject(recordType);

            // get list of unique records based on field list
            foreach (var record in records)
            {
                var summaryRecord = ObjectHelper.CreateObject(recordType);
                foreach (var field in groupFields)
                {
                    var propertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(recordType, field);
                    propertyInfo.SetValue(summaryRecord, propertyInfo.GetValue(record));
                }
                // todo - this bit doesn't work
                if (!summaryRecords.Contains(summaryRecord)) summaryRecords.Add(summaryRecord);
            }

            // get summary values
            foreach (var summaryRecord in summaryRecords) 
            {
                foreach (var record in records)
                {
                    if (AllFieldsMatch(record, summaryRecord, recordType, groupFields))
                    {
                        foreach (var field in summaryFields)
                        {
                            var propertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(recordType, field);
                            var summaryValue = GetSummaryValue(record, summaryRecord, propertyInfo);
                            if (summaryValue != null) propertyInfo.SetValue(summaryRecord, summaryValue);
                        }
                    }
                }
            }
            return ((IEnumerable<object>)summaryRecords).ToList();
        }

        private static bool AllFieldsMatch(object record, object matchRecord, Type recordType, IEnumerable<string> fields)
        {
            // todo - this only seems to work for strings
            return fields.Select(field => FieldPropertyHelper.GetFieldPropertyInfo(recordType, field)).All(propertyInfo => propertyInfo.GetValue(record) == propertyInfo.GetValue(matchRecord));
        }

        private static object GetSummaryValue(object record, object summaryRecord, PropertyInfo propertyInfo)
        {
            var recordValue = propertyInfo.GetValue(record);
            var summaryValue = propertyInfo.GetValue(summaryRecord);
            if (summaryValue == null)
            {
                return recordValue;
            }
            switch (propertyInfo.PropertyType.Name.ToLower())
            {
                case "int32":
                    return (int)summaryValue + (int)recordValue;
                case "decimal":
                    return (decimal)summaryValue + (decimal)recordValue;
                case "double":
                    return (double)summaryValue + (double)recordValue;
                default:
                    return null;
            }
        }

    }
}
