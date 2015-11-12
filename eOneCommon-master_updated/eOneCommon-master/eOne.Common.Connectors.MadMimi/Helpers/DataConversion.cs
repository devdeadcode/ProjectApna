using System;

namespace eOne.Common.Connectors.MadMimi.Helpers
{
    public class DataConversion
    {

        public static DateTime? ParseDateTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var parts = value.Split(' ');
            var dateParts = parts[0].Split('-');
            var year = int.Parse(dateParts[0]);
            var month = int.Parse(dateParts[1]);
            var day = int.Parse(dateParts[2]);
            var timeParts = parts[1].Split(':');
            var hour = int.Parse(timeParts[0]);
            var minute = int.Parse(timeParts[1]);
            var second = int.Parse(timeParts[2]);
            return new DateTime(year, month, day, hour, minute, second);
        }

    }
}
