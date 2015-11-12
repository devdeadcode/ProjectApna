using System;
using eOne.Common.Query;

namespace eOne.Common.Helpers
{
    public class DateHelper
    {

        public static DateTime GetDateValue(ConnectorValue.ConnectorDateValueType dateValue)
        {
            switch (dateValue)
            {
                case ConnectorValue.ConnectorDateValueType.Today:
                    return DateTime.Today;
                case ConnectorValue.ConnectorDateValueType.Yesterday:
                    return DateTime.Today.AddDays(-1);
                case ConnectorValue.ConnectorDateValueType.Tomorrow:
                    return DateTime.Today.AddDays(1);
                case ConnectorValue.ConnectorDateValueType.StartOfWeek:
                    return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                case ConnectorValue.ConnectorDateValueType.EndOfWeek:
                    return DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek);
                case ConnectorValue.ConnectorDateValueType.StartOfMonth:
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                case ConnectorValue.ConnectorDateValueType.EndOfMonth:
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
                case ConnectorValue.ConnectorDateValueType.StartOfYear:
                    return new DateTime(DateTime.Today.Year, 1, 1);
                case ConnectorValue.ConnectorDateValueType.EndOfYear:
                    return new DateTime(DateTime.Today.Year, 12, 31);
                case ConnectorValue.ConnectorDateValueType.StartOfQuarter:
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month - (DateTime.Today.Month % 3) + 1, 1);
                case ConnectorValue.ConnectorDateValueType.EndOfQuarter:
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month - (DateTime.Today.Month % 3) + 1, 1).AddMonths(1).AddDays(-1);
                case ConnectorValue.ConnectorDateValueType.StartOfPeriod:
                    //todo
                case ConnectorValue.ConnectorDateValueType.EndOfPeriod:
                    //todo
                    return DateTime.MaxValue;
            }
            return DateTime.MaxValue;
        }



    }
}
