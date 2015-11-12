using System;

namespace eOne.Common.Connectors.Sendloop.Helpers
{
    public class DataConversion
    {

        public static DateTime? ParseDateTime(string value)
        {
            if (value.Substring(0, 10) == "0000-00-00") return null;
            return DateTime.Parse(value);
        }

    }
}
