using System;

namespace eOne.Common.Helpers
{
    public class DateHelper
    {
        public static DateTime GetDateValue(ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateValue)
        {
            switch (dateValue)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Today:
                    return DateTime.Today;
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Yesterday:
                    return DateTime.Today.AddDays(-1);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Tomorrow:
                    return DateTime.Today.AddDays(1);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfWeek:
                    return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfWeek:
                    return DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfMonth:
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfMonth:
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfYear:
                    return new DateTime(DateTime.Today.Year, 1, 1);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfYear:
                    return new DateTime(DateTime.Today.Year, 12, 31);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfQuarter:
                    //todo
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfQuarter:
                    //todo
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfPeriod:
                    //todo
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfPeriod:
                    //todo
                    return DateTime.MaxValue;
            }
            return DateTime.MaxValue;
        }
    }
}
