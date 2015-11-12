using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceActivity : SalesForceEntity
    {

        #region Enums

        public enum SalesForceActivityRecurrenceType
        {
            RecursEveryWeekday,
            RecursDaily,
            RecursMonthly,
            RecursMonthlyNth,
            RecursWeekly,
            RecursYearly,
            RecursYearlyNth
        }

        public enum SalesForceActivityFrequency
        {
            [Description("Not recurring")]
            NotRecurring,
            [Description("Daily")]
            Daily,
            [Description("Weekly")]
            Weekly,
            [Description("Monthly")]
            Monthly,
            [Description("Yearly")]
            Yearly
        }

        #endregion

        #region Default properties

        [FieldSettings("Subject", DefaultField = true, Description = true)]
        public string Subject { get; set; }

        [FieldSettings("Due date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ActivityDate { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Comments")]
        public string Description { get; set; }

        [FieldSettings("Account ID")]
        public string AccountId { get; set; }

        [FieldSettings("Reminder date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ReminderDateTime { get; set; }

        [FieldSettings("Archived", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsArchived { get; set; }

        [FieldSettings("Recurring", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsRecurrence { get; set; }

        [FieldSettings("Reminder set", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsReminderSet { get; set; }

        [FieldSettings("Recurring start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? RecurrenceStartDateOnly { get; set; }

        [FieldSettings("Recurring end date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? RecurrenceEndDateOnly { get; set; }

        public string WhoId { get; set; }

        public string WhatId { get; set; }

        #endregion

        #region Hidden properties

        public SalesForceActivityRecurrenceType? RecurrenceType { get; set; }
        public int? RecurrenceDayOfWeekMask { get; set; }
        public int? RecurrenceInterval { get; set; }
        public int? RecurrenceDayOfMonth { get; set; }
        public string RecurrenceInstance { get; set; }
        public string RecurrenceMonthOfYear { get; set; }
        public string DaysOfWeek
        {
            get
            {
                if (RecurrenceDayOfWeekMask == null || RecurrenceDayOfWeekMask == 0) return string.Empty;
                var days = new bool[7];
                var daysMask = RecurrenceDayOfWeekMask;
                var dayId = 64;
                var dayNumber = 6;
                while (dayNumber > 0)
                {
                    if (daysMask >= dayId)
                    {
                        days[dayNumber] = true;
                        daysMask = daysMask - dayId;
                    }
                    dayId = dayId / 2;
                    dayNumber = dayNumber - 1;
                }
                var dayNames = new List<string>();
                for (var i = 0; i < 7; i++)
                {
                    if (days[i]) dayNames.Add(Enum.GetName(typeof(DayOfWeek), i));
                }
                if (Frequency == SalesForceActivityFrequency.Weekly) return dayNames.Count == 0 ? string.Empty : string.Join(", ", dayNames);
                return dayNames.Count == 1 ? dayNames[0] : "day";
            }
        }

        #endregion

        #region Calculations

        [FieldSettings("Recurring option", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(SalesForceActivityFrequency), FieldsRequiredForCalculation = "RecurrenceType, RecurrenceDayOfWeekMask, RecurrenceInterval, RecurrenceDayOfMonth, RecurrenceInstance, RecurrenceMonthOfYear")]
        public string RecurringOption
        {
            get
            {
                switch (RecurrenceType)
                {
                    case SalesForceActivityRecurrenceType.RecursEveryWeekday:
                        return "Every weekday";
                    case SalesForceActivityRecurrenceType.RecursDaily:
                        return $"Every {RecurrenceInterval} day(s)";
                    case SalesForceActivityRecurrenceType.RecursWeekly:
                        return $"Recurs every {RecurrenceInterval} week(s) on {DaysOfWeek}";
                    case SalesForceActivityRecurrenceType.RecursMonthly:
                        return $"On day {RecurrenceDayOfMonth} of every {RecurrenceInterval} month(s)";
                    case SalesForceActivityRecurrenceType.RecursMonthlyNth:
                        return $"On the {RecurrenceInstance} {DaysOfWeek} of every {RecurrenceInterval} month(s)";
                    case SalesForceActivityRecurrenceType.RecursYearly:
                        return $"On every {RecurrenceMonthOfYear} {RecurrenceInterval}";
                    case SalesForceActivityRecurrenceType.RecursYearlyNth:
                        return $"On the {RecurrenceInstance} {DaysOfWeek} of {RecurrenceMonthOfYear}";
                    default:
                        return string.Empty;
                }
            }
        }

        [FieldSettings("Frequency", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(SalesForceActivityFrequency), FieldsRequiredForCalculation = "RecurrenceType")]
        public SalesForceActivityFrequency Frequency
        {
            get
            {
                switch (RecurrenceType)
                {
                    case SalesForceActivityRecurrenceType.RecursEveryWeekday:
                    case SalesForceActivityRecurrenceType.RecursDaily:
                        return SalesForceActivityFrequency.Daily;
                    case SalesForceActivityRecurrenceType.RecursMonthly:
                    case SalesForceActivityRecurrenceType.RecursMonthlyNth:
                        return SalesForceActivityFrequency.Monthly;
                    case SalesForceActivityRecurrenceType.RecursWeekly:
                        return SalesForceActivityFrequency.Weekly;
                    case SalesForceActivityRecurrenceType.RecursYearly:
                    case SalesForceActivityRecurrenceType.RecursYearlyNth:
                        return SalesForceActivityFrequency.Yearly;
                    default:
                        return SalesForceActivityFrequency.NotRecurring;
                }
            }
        }

        [FieldSettings("Reminder time", FieldTypeId = Connector.FieldTypeIdTime, FieldsRequiredForCalculation = "ReminderDateTime")]
        public DateTime? ReminderTime => ReminderDateTime;

        #endregion

    }
}
