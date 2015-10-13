using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Helpers
{
    public class HtmlFormatting
    {

        public static IEnumerable<object> Format(IEnumerable<object> records, ConnectorQuery query)
        {
            if (query == null || !query.ApplyHtmlFormatting) return records;
            return query.Fields.Where(field => !string.IsNullOrWhiteSpace(field.Type.HtmlFormat)).Aggregate(records, (current, field) => Format(current, query.Entity, field));
        }

        private static IEnumerable<object> Format(IEnumerable<object> records, DataConnectorEntity entity, DataConnectorField field)
        {
            var propertyInfo = entity.RecordType.GetProperty(field.Name);
            var formattedRecords = records;
            foreach (var record in formattedRecords)
            {
                var unformatted = propertyInfo.GetValue(record).ToString();
                if (!string.IsNullOrWhiteSpace(unformatted))
                {
                    var formatted = string.Format(field.Type.HtmlFormat, unformatted);
                    propertyInfo.SetValue(record, formatted);
                }
            }
            return formattedRecords;
        }

    }
}
