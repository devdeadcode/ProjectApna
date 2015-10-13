using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace eOne.Common.Helpers
{
    public class Filtering
    {

        #region Constants

        const string TypeBoolean = "boolean";
        const string TypeDateTime = "datetime";
        const string TypeDecimal = "decimal";
        const string TypeShort = "short";
        const string TypeInteger = "int32";
        const string TypeLong = "long";
        const string TypeUshort = "ushort";
        const string TypeUinteger = "uint32";
        const string TypeUlong = "ulong";

        #endregion

        public static IEnumerable<object> Filter(IEnumerable<object> records, ConnectorQuery query)
        {
            if (query == null) return records;
            var filteredRecords = records;
            query.Restrictions.Sort((x, y) => x.ConjunctiveOperator.CompareTo(y.ConjunctiveOperator));
            foreach (var restriction in query.Restrictions)
            {
                switch (restriction.ConjunctiveOperator)
                {
                    case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None:
                    case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And:
                        filteredRecords = Filter(filteredRecords, query.Entity.RecordType, restriction);
                        break;
                    case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or:
                        filteredRecords = filteredRecords.Union(Filter(records, query.Entity.RecordType, restriction)).ToList();
                        break;
                }
            }
            // todo - sort before applying max records
            return query.MaxRecords > 0 ? filteredRecords.Take(query.MaxRecords) : filteredRecords;
        }
        private static IEnumerable<object> Filter(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            if (restriction.FieldType == ConnectorRestriction.ConnectorRestrictionFieldType.Subquery)
            {
                //todo
                //return Filter(records, type, restriction.Subquery);
                return null;
            }
            switch (restriction.RestrictionType)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Equals:
                    return FilterEquals(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                    return FilterDoesNotEqual(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                    return FilterStartsWith(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                    return FilterEndsWith(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.Contains:
                    return FilterContains(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContain:
                    return FilterDoesNotStartWith(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                    return FilterDoesNotEndWith(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                    return FilterDoesNotContain(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThan:
                    return FilterGreaterThan(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo:
                    return FilterGreaterThanEqual(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.LessThan:
                    return FilterLessThan(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo:
                    return FilterLessThanEqual(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.Between:
                    return FilterBetween(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.NotBetween:
                    return FilterNotBetween(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.OneOfList:
                    return FilterOneOf(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.NotOneOfList:
                    return FilterNotOneOf(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.ContainsOneOfList:
                    return FilterContainsOneOf(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContainOneOfList:
                    return FilterDoesNotContainOneOf(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.StartsWithOneOfList:
                    return FilterStartsWithOneOf(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWithOneOfList:
                    return FilterDoesNotStartWithOneOf(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.EndsWithOneOfList:
                    return FilterEndsWithOneOf(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWithOneOfList:
                    return FilterDoesNotEndWithOneOf(records, type, restriction);
            }
            return records;
        }

        #region Filters

        private static PropertyInfo GetFieldPropertyInfo(Type type, string fieldName)
        {
            return type.GetProperty(fieldName);
        }

        private static IEnumerable<object> FilterEquals(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterEquals(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterEquals(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterEqualsValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterEqualsField(records, propertyInfo, restrictionPropertyInfo);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.DateTimeValue:
                    return FilterEqualsDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static IEnumerable<object> FilterDoesNotEqual(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var propertyInfo = GetFieldPropertyInfo(type, restriction.Field.Name);
            switch (restriction.Values[0].Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterDoesNotEqualValue(records, propertyInfo, restriction.Values[0].Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotEqualField(records, propertyInfo, restrictionPropertyInfo);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.DateTimeValue:
                    return FilterDoesNotEqualDate(records, propertyInfo, restriction.Values[0].DateValue);
            }
            return records;
        }
        private static IEnumerable<object> FilterStartsWith(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterStartsWith(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterStartsWith(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterStartsWithValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterStartsWithField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static IEnumerable<object> FilterEndsWith(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterEndsWith(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterEndsWith(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterEndsWithValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterEndsWithField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static IEnumerable<object> FilterContains(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterContains(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterContains(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterContainsValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterContainsField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static IEnumerable<object> FilterDoesNotStartWith(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var propertyInfo = GetFieldPropertyInfo(type, restriction.Field.Name);
            switch (restriction.Values[0].Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterDoesNotStartWithValue(records, propertyInfo, restriction.Values[0].Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotStartWithField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static IEnumerable<object> FilterDoesNotEndWith(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var propertyInfo = GetFieldPropertyInfo(type, restriction.Field.Name);
            switch (restriction.Values[0].Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterDoesNotEndWithValue(records, propertyInfo, restriction.Values[0].Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotEndWithField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static IEnumerable<object> FilterDoesNotContain(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var propertyInfo = GetFieldPropertyInfo(type, restriction.Field.Name);
            switch (restriction.Values[0].Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterDoesNotContainValue(records, propertyInfo, restriction.Values[0].Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotContainField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static IEnumerable<object> FilterGreaterThan(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterGreaterThan(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterGreaterThan(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterGreaterThanValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    var restrictionPropertyInfo = GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterGreaterThanField(records, propertyInfo, restrictionPropertyInfo);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.DateTimeValue:
                    return FilterGreaterThanDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static IEnumerable<object> FilterGreaterThanEqual(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterGreaterThanEqual(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterGreaterThanEqual(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterGreaterThanEqualValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    return FilterGreaterThanEqualField(records, propertyInfo, restrictionValue.Field.Name);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.DateTimeValue:
                    return FilterGreaterThanEqualDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static IEnumerable<object> FilterLessThan(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterLessThan(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterLessThan(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterLessThanValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    return FilterLessThanField(records, propertyInfo, restrictionValue.Field.Name);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.DateTimeValue:
                    return FilterLessThanDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static IEnumerable<object> FilterLessThanEqual(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterLessThanEqual(records, type, restriction.Field.Name, restriction.Values[0]);
        }
        private static IEnumerable<object> FilterLessThanEqual(IEnumerable<object> records, Type type, string fieldName, ConnectorRestrictionValue restrictionValue)
        {
            var propertyInfo = GetFieldPropertyInfo(type, fieldName);
            switch (restrictionValue.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return FilterLessThanEqualValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    return FilterLessThanEqualField(records, propertyInfo, restrictionValue.Field.Name);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.DateTimeValue:
                    return FilterLessThanEqualDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static IEnumerable<object> FilterBetween(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var greaterThanRecords = FilterGreaterThanEqual(records, type, restriction.Field.Name, restriction.Values[0]);
            return FilterLessThanEqual(greaterThanRecords, type, restriction.Field.Name, restriction.Values[1]);
        }
        private static IEnumerable<object> FilterNotBetween(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var lessThanRecords = FilterGreaterThan(records, type, restriction.Field.Name, restriction.Values[0]);
            var greaterThanRecords = FilterGreaterThan(records, type, restriction.Field.Name, restriction.Values[1]);
            return lessThanRecords.Concat(greaterThanRecords).ToList();
        }
        private static IEnumerable<object> FilterOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterEquals(records, type, restriction.Field.Name, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static IEnumerable<object> FilterNotOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterOneOf(records, type, restriction)).ToList();
        }
        private static IEnumerable<object> FilterContainsOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterContains(records, type, restriction.Field.Name, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static IEnumerable<object> FilterDoesNotContainOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterContainsOneOf(records, type, restriction)).ToList();
        }
        private static IEnumerable<object> FilterStartsWithOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterStartsWith(records, type, restriction.Field.Name, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static IEnumerable<object> FilterDoesNotStartWithOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterStartsWithOneOf(records, type, restriction)).ToList();
        }
        private static IEnumerable<object> FilterEndsWithOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterEndsWith(records, type, restriction.Field.Name, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static IEnumerable<object> FilterDoesNotEndWithOneOf(IEnumerable<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterEndsWithOneOf(records, type, restriction)).ToList();
        }

        #region Value filters

        private static IEnumerable<object> FilterEqualsValue(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.PropertyType.Name.ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) == DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) == decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeLong:
                case TypeUshort:
                case TypeUinteger:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) == long.Parse(equalsValue)).ToList();
                case TypeBoolean:
                    return records.Where(record => (bool)propertyInfo.GetValue(record, null) == bool.Parse(equalsValue)).ToList(); 
            }
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant() == equalsValue.ToLowerInvariant()).ToList(); 
        }
        private static IEnumerable<object> FilterDoesNotEqualValue(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) != DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) != decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeLong:
                case TypeUshort:
                case TypeUinteger:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) != long.Parse(equalsValue)).ToList();
                case TypeBoolean:
                    return records.Where(record => (bool)propertyInfo.GetValue(record, null) != bool.Parse(equalsValue)).ToList();
            }
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant() != equalsValue.ToLowerInvariant()).ToList();
        }
        private static IEnumerable<object> FilterStartsWithValue(IEnumerable<object> records, PropertyInfo propertyInfo, string startsWithValue)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().StartsWith(startsWithValue.ToLowerInvariant())).ToList();
        }
        private static IEnumerable<object> FilterEndsWithValue(IEnumerable<object> records, PropertyInfo propertyInfo, string endsWithValue)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().EndsWith(endsWithValue.ToLowerInvariant())).ToList();
        }
        private static IEnumerable<object> FilterContainsValue(IEnumerable<object> records, PropertyInfo propertyInfo, string containsValue)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().Contains(containsValue.ToLowerInvariant())).ToList();
        }
        private static IEnumerable<object> FilterDoesNotStartWithValue(IEnumerable<object> records, PropertyInfo propertyInfo, string startsWithValue)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().StartsWith(startsWithValue.ToLowerInvariant())).ToList();
        }
        private static IEnumerable<object> FilterDoesNotEndWithValue(IEnumerable<object> records, PropertyInfo propertyInfo, string endsWithValue)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().EndsWith(endsWithValue.ToLowerInvariant())).ToList();
        }
        private static IEnumerable<object> FilterDoesNotContainValue(IEnumerable<object> records, PropertyInfo propertyInfo, string containsValue)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().Contains(containsValue.ToLowerInvariant())).ToList();
        }
        private static IEnumerable<object> FilterGreaterThanValue(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) > DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) > decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeLong:
                case TypeUshort:
                case TypeUinteger:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) > long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) > 0).ToList();
        }
        private static IEnumerable<object> FilterGreaterThanEqualValue(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) >= DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) >= decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeLong:
                case TypeUshort:
                case TypeUinteger:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) >= long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) >= 0).ToList();
        }
        private static IEnumerable<object> FilterLessThanValue(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) < DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) < decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeLong:
                case TypeUshort:
                case TypeUinteger:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) < long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) < 0).ToList();
        }
        private static IEnumerable<object> FilterLessThanEqualValue(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) <= DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) <= decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeLong:
                case TypeUshort:
                case TypeUinteger:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) <= long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) <= 0).ToList();
        }

        #endregion

        #region Field filters

        private static IEnumerable<object> FilterEqualsField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString() == restrictionPropertyInfo.GetValue(record, null).ToString()).ToList();
        }
        private static IEnumerable<object> FilterDoesNotEqualField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString() != restrictionPropertyInfo.GetValue(record, null).ToString()).ToList();
        }
        private static IEnumerable<object> FilterStartsWithField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static IEnumerable<object> FilterEndsWithField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static IEnumerable<object> FilterContainsField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static IEnumerable<object> FilterDoesNotStartWithField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static IEnumerable<object> FilterDoesNotEndWithField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static IEnumerable<object> FilterDoesNotContainField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static IEnumerable<object> FilterGreaterThanField(IEnumerable<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) > (DateTime)restrictionPropertyInfo.GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) > (decimal)restrictionPropertyInfo.GetValue(record, null)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeLong:
                case TypeUshort:
                case TypeUinteger:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) > (long)restrictionPropertyInfo.GetValue(record, null)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), restrictionPropertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), StringComparison.Ordinal) > 0).ToList();
        }
        private static IEnumerable<object> FilterGreaterThanEqualField(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsFieldName)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) >= (DateTime)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) >= (decimal)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
            }
            return records.Where(record => (long)propertyInfo.GetValue(record, null) >= (long)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
        }
        private static IEnumerable<object> FilterLessThanField(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsFieldName)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) < (DateTime)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) < (decimal)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
            }
            return records.Where(record => (long)propertyInfo.GetValue(record, null) < (long)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
        }
        private static IEnumerable<object> FilterLessThanEqualField(IEnumerable<object> records, PropertyInfo propertyInfo, string equalsFieldName)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) <= (DateTime)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) <= (decimal)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
            }
            return records.Where(record => (long)propertyInfo.GetValue(record, null) <= (long)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
        }

        #endregion

        #region Date filters

        private static IEnumerable<object> FilterEqualsDate(IEnumerable<object> records, PropertyInfo propertyInfo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateValue)
        {
            if (propertyInfo.GetType() != typeof(DateTime)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) == date).ToList();
        }
        private static IEnumerable<object> FilterDoesNotEqualDate(IEnumerable<object> records, PropertyInfo propertyInfo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateValue)
        {
            if (propertyInfo.GetType() != typeof(DateTime)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) != date).ToList();
        }
        private static IEnumerable<object> FilterGreaterThanDate(IEnumerable<object> records, PropertyInfo propertyInfo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateValue)
        {
            if (propertyInfo.GetType() != typeof(DateTime)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) > date).ToList();
        }
        private static IEnumerable<object> FilterGreaterThanEqualDate(IEnumerable<object> records, PropertyInfo propertyInfo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateValue)
        {
            if (propertyInfo.GetType() != typeof(DateTime)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) >= date).ToList();
        }
        private static IEnumerable<object> FilterLessThanDate(IEnumerable<object> records, PropertyInfo propertyInfo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateValue)
        {
            if (propertyInfo.GetType() != typeof(DateTime)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) < date).ToList();
        }
        private static IEnumerable<object> FilterLessThanEqualDate(IEnumerable<object> records, PropertyInfo propertyInfo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateValue)
        {
            if (propertyInfo.GetType() != typeof(DateTime)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) <= date).ToList();
        }

        #endregion

        #endregion
    }
}
