using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using eOne.Common.Connectors;
using eOne.Common.Extensions;
using eOne.Common.Query;

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

        public static List<object> Filter(List<object> records, ConnectorQuery query)
        {
            // if the query is null, do no filtering
            if (query == null) return records;

            // return results of search 
            if (query.IsSearch) return Search(records, query);

            // apply filters
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

            // sort before applying max records
            if (query.OrderBy != null && query.OrderBy.Count > 0) filteredRecords = Sorting.SortRecords(filteredRecords, query.Entity.RecordType, query.OrderBy);
            return query.MaxRecords > 0 ? filteredRecords.Take(query.MaxRecords).ToList() : filteredRecords;
        }
        private static List<object> Filter(List<object> records, Type type, ConnectorRestriction restriction)
        {
            // if records are already null, don't need to filter
            if (records == null) return null;

            // don't attempt filter if restriction or field is null
            if (restriction?.Field == null) return records;

            // if the filter is a subquery, return the results of the subquery 
            if (restriction.FieldType == ConnectorRestriction.ConnectorRestrictionFieldType.Subquery) return Filter(records, restriction.Subquery);

            // get property info for field
            if (restriction.Field.propertyInfo == null)
            {
                restriction.Field.propertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restriction.Field.Name);
                // remove records where the field value being filtered on is null
                records = FilterNulls(records, restriction.Field.propertyInfo);
            }

            // filter based on the restriction type
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
                    return FilterDoesNotContain(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                    return FilterDoesNotEndWith(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                    return FilterDoesNotStartWith(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThan:
                    return FilterGreaterThan(records, type, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo:
                    return FilterGreaterThanEqual(records, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.LessThan:
                    return FilterLessThan(records, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo:
                    return FilterLessThanEqual(records, restriction);
                case ConnectorRestriction.ConnectorRestrictionType.Between:
                    return FilterBetween(records, restriction);
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
                case ConnectorRestriction.ConnectorRestrictionType.Fuzzy:
                    return FilterFuzzyEquals(records, restriction);
            }
            return records;
        }
        private static List<object> Search(List<object> records, ConnectorQuery query)
        {
            if (!query.HasOrConjunctives) return null;

            var filteredRecords = new List<object>();
            var remainingRecords = records.ToList();

            // sort by created date/time and updated date/time if they exist, so recently created/updated records are listed first
            if (query.Entity.CreateDateField != null)
            {
                var sortBy = new List<Tuple<ConnectorField, ConnectorQuery.ConnectorQuerySortOrder>>
                {
                    Sorting.BuildSortTuple(query.Entity.CreateDateField, false)
                };
                if (query.Entity.CreateTimeField != null) sortBy.Add(Sorting.BuildSortTuple(query.Entity.CreateTimeField, false));
                if (query.Entity.ModifyDateField != null)
                {
                    sortBy.Add(Sorting.BuildSortTuple(query.Entity.ModifyDateField, false));
                    if (query.Entity.ModifyTimeField != null) sortBy.Add(Sorting.BuildSortTuple(query.Entity.ModifyTimeField, false));
                }
                remainingRecords = Sorting.SortRecords(remainingRecords, query.Entity.RecordType, sortBy);
            }

            // add found records for each or restriction to the filtered records list
            foreach (var restriction in query.Restrictions.Where(restriction => restriction.ConjunctiveOperator == ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or))
            {
                var foundRecords = Filter(remainingRecords, query.Entity.RecordType, restriction);
                // remove all found records from the remaining records so we don't search them again
                foreach (var record in foundRecords)
                {
                    filteredRecords.Add(record);
                    remainingRecords.Remove(record);
                }
            }

            // filter the exclude terms
            foreach (var restriction in query.Restrictions.Where(restriction => restriction.ConjunctiveOperator == ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And))
            {
                filteredRecords = Filter(filteredRecords, query.Entity.RecordType, restriction);
            }

            return filteredRecords;
        }

        #region Filters

        private static List<object> FilterNulls(List<object> records, PropertyInfo propertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null) != null).ToList();
        }

        private static List<object> FilterEquals(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterEquals(records, type, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterEquals(List<object> records, Type type, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterEqualsValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterEqualsField(records, propertyInfo, restrictionPropertyInfo);
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return FilterEqualsDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static List<object> FilterDoesNotEqual(List<object> records, Type type, ConnectorRestriction restriction)
        {
            switch (restriction.Values[0].Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterDoesNotEqualValue(records, restriction.Field.propertyInfo, restriction.Values[0].Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotEqualField(records, restriction.Field.propertyInfo, restrictionPropertyInfo);
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return FilterDoesNotEqualDate(records, restriction.Field.propertyInfo, restriction.Values[0].DateValue);
            }
            return records;
        }
        private static List<object> FilterStartsWith(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterStartsWith(records, type, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterStartsWith(List<object> records, Type type, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterStartsWithValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterStartsWithField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static List<object> FilterEndsWith(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterEndsWith(records, type, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterEndsWith(List<object> records, Type type, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterEndsWithValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterEndsWithField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static List<object> FilterContains(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterContains(records, type, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterContains(List<object> records, Type type, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterContainsValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterContainsField(records, propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static List<object> FilterDoesNotStartWith(List<object> records, Type type, ConnectorRestriction restriction)
        {
            switch (restriction.Values[0].Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterDoesNotStartWithValue(records, restriction.Field.propertyInfo, restriction.Values[0].Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotStartWithField(records, restriction.Field.propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static List<object> FilterDoesNotEndWith(List<object> records, Type type, ConnectorRestriction restriction)
        {
            switch (restriction.Values[0].Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterDoesNotEndWithValue(records, restriction.Field.propertyInfo, restriction.Values[0].Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotEndWithField(records, restriction.Field.propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static List<object> FilterDoesNotContain(List<object> records, Type type, ConnectorRestriction restriction)
        {
            switch (restriction.Values[0].Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterDoesNotContainValue(records, restriction.Field.propertyInfo, restriction.Values[0].Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restriction.Values[0].Field.Name);
                    return FilterDoesNotContainField(records, restriction.Field.propertyInfo, restrictionPropertyInfo);
            }
            return records;
        }
        private static List<object> FilterGreaterThan(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return FilterGreaterThan(records, type, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterGreaterThan(List<object> records, Type type, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterGreaterThanValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    var restrictionPropertyInfo = FieldPropertyHelper.GetFieldPropertyInfo(type, restrictionValue.Field.Name);
                    return FilterGreaterThanField(records, propertyInfo, restrictionPropertyInfo);
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return FilterGreaterThanDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static List<object> FilterGreaterThanEqual(List<object> records, ConnectorRestriction restriction)
        {
            return FilterGreaterThanEqual(records, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterGreaterThanEqual(List<object> records, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterGreaterThanEqualValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    return FilterGreaterThanEqualField(records, propertyInfo, restrictionValue.Field.Name);
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return FilterGreaterThanEqualDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static List<object> FilterLessThan(List<object> records, ConnectorRestriction restriction)
        {
            return FilterLessThan(records, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterLessThan(List<object> records, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterLessThanValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    return FilterLessThanField(records, propertyInfo, restrictionValue.Field.Name);
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return FilterLessThanDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static List<object> FilterLessThanEqual(List<object> records, ConnectorRestriction restriction)
        {
            return FilterLessThanEqual(records, restriction.Field.propertyInfo, restriction.Values[0]);
        }
        private static List<object> FilterLessThanEqual(List<object> records, PropertyInfo propertyInfo, ConnectorValue restrictionValue)
        {
            switch (restrictionValue.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterLessThanEqualValue(records, propertyInfo, restrictionValue.Constant);
                case ConnectorValue.ConnectorValueType.Field:
                    return FilterLessThanEqualField(records, propertyInfo, restrictionValue.Field.Name);
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return FilterLessThanEqualDate(records, propertyInfo, restrictionValue.DateValue);
            }
            return records;
        }
        private static List<object> FilterBetween(List<object> records, ConnectorRestriction restriction)
        {
            var greaterThanRecords = FilterGreaterThanEqual(records, restriction.Field.propertyInfo, restriction.Values[0]);
            return FilterLessThanEqual(greaterThanRecords, restriction.Field.propertyInfo, restriction.Values[1]);
        }
        private static List<object> FilterNotBetween(List<object> records, Type type, ConnectorRestriction restriction)
        {
            var lessThanRecords = FilterLessThan(records, restriction.Field.propertyInfo, restriction.Values[0]);
            var greaterThanRecords = FilterGreaterThan(records, type, restriction.Field.propertyInfo, restriction.Values[1]);
            return lessThanRecords.Concat(greaterThanRecords).ToList();
        }
        private static List<object> FilterOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterEquals(records, type, restriction.Field.propertyInfo, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static List<object> FilterNotOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterOneOf(records, type, restriction)).ToList();
        }
        private static List<object> FilterContainsOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterContains(records, type, restriction.Field.propertyInfo, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static List<object> FilterDoesNotContainOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterContainsOneOf(records, type, restriction)).ToList();
        }
        private static List<object> FilterStartsWithOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterStartsWith(records, type, restriction.Field.propertyInfo, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static List<object> FilterDoesNotStartWithOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterStartsWithOneOf(records, type, restriction)).ToList();
        }
        private static List<object> FilterEndsWithOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            var filteredRecords = new List<object>();
            return restriction.Values.Select(value => FilterEndsWith(records, type, restriction.Field.propertyInfo, value)).Aggregate(filteredRecords, (current, orRecords) => current.Union(orRecords).ToList());
        }
        private static List<object> FilterDoesNotEndWithOneOf(List<object> records, Type type, ConnectorRestriction restriction)
        {
            return records.Except(FilterEndsWithOneOf(records, type, restriction)).ToList();
        }
        private static List<object> FilterFuzzyEquals(List<object> records, ConnectorRestriction restriction)
        {
            switch (restriction.Values[0].Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return FilterFuzzyEqualsValue(records, restriction.Field.propertyInfo, restriction.Values[0].Constant);
            }
            return records;
        }

        #region Value filters

        private static List<object> FilterEqualsValue(List<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.PropertyType.Name.ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) == DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) == decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeUshort:
                case TypeUinteger:
                    return records.Where(record => (int)propertyInfo.GetValue(record, null) == int.Parse(equalsValue)).ToList();
                case TypeUlong:
                case TypeLong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) == long.Parse(equalsValue)).ToList();
                case TypeBoolean:
                    return records.Where(record => (bool)propertyInfo.GetValue(record, null) == bool.Parse(equalsValue)).ToList(); 
            }
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant() == equalsValue.ToLowerInvariant()).ToList(); 
        }
        private static List<object> FilterDoesNotEqualValue(List<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) != DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) != decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeUshort:
                case TypeUinteger:
                    return records.Where(record => (int)propertyInfo.GetValue(record, null) != int.Parse(equalsValue)).ToList();
                case TypeLong:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) != long.Parse(equalsValue)).ToList();
                case TypeBoolean:
                    return records.Where(record => (bool)propertyInfo.GetValue(record, null) != bool.Parse(equalsValue)).ToList();
            }
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant() != equalsValue.ToLowerInvariant()).ToList();
        }
        private static List<object> FilterStartsWithValue(List<object> records, PropertyInfo propertyInfo, string startsWithValue)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().StartsWith(startsWithValue.ToLowerInvariant())).ToList();
        }
        private static List<object> FilterEndsWithValue(List<object> records, PropertyInfo propertyInfo, string endsWithValue)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().EndsWith(endsWithValue.ToLowerInvariant())).ToList();
        }
        private static List<object> FilterContainsValue(List<object> records, PropertyInfo propertyInfo, string containsValue)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().Contains(containsValue.ToLowerInvariant())).ToList();
        }
        private static List<object> FilterDoesNotStartWithValue(List<object> records, PropertyInfo propertyInfo, string startsWithValue)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().StartsWith(startsWithValue.ToLowerInvariant())).ToList();
        }
        private static List<object> FilterDoesNotEndWithValue(List<object> records, PropertyInfo propertyInfo, string endsWithValue)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().EndsWith(endsWithValue.ToLowerInvariant())).ToList();
        }
        private static List<object> FilterDoesNotContainValue(List<object> records, PropertyInfo propertyInfo, string containsValue)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().ToLowerInvariant().Contains(containsValue.ToLowerInvariant())).ToList();
        }
        private static List<object> FilterGreaterThanValue(List<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) > DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) > decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeUshort:
                case TypeUinteger:
                    return records.Where(record => (int)propertyInfo.GetValue(record, null) > int.Parse(equalsValue)).ToList();
                case TypeLong:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) > long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) > 0).ToList();
        }
        private static List<object> FilterGreaterThanEqualValue(List<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) >= DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) >= decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeUshort:
                case TypeUinteger:
                    return records.Where(record => (int)propertyInfo.GetValue(record, null) >= int.Parse(equalsValue)).ToList();
                case TypeLong:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) >= long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) >= 0).ToList();
        }
        private static List<object> FilterLessThanValue(List<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) < DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) < decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeUshort:
                case TypeUinteger:
                    return records.Where(record => (int)propertyInfo.GetValue(record, null) < int.Parse(equalsValue)).ToList();
                case TypeLong:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) < long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) < 0).ToList();
        }
        private static List<object> FilterLessThanEqualValue(List<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case TypeDateTime:
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) <= DateTime.Parse(equalsValue)).ToList();
                case TypeDecimal:
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) <= decimal.Parse(equalsValue)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeUshort:
                case TypeUinteger:
                    return records.Where(record => (int)propertyInfo.GetValue(record, null) <= int.Parse(equalsValue)).ToList();
                case TypeLong:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) <= long.Parse(equalsValue)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), equalsValue.ToLowerInvariant(), StringComparison.Ordinal) <= 0).ToList();
        }
        private static List<object> FilterFuzzyEqualsValue(List<object> records, PropertyInfo propertyInfo, string equalsValue)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().FuzzyEquals(equalsValue)).ToList();
        }

        #endregion

        #region Field filters

        private static List<object> FilterEqualsField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString() == restrictionPropertyInfo.GetValue(record, null).ToString()).ToList();
        }
        private static List<object> FilterDoesNotEqualField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString() != restrictionPropertyInfo.GetValue(record, null).ToString()).ToList();
        }
        private static List<object> FilterStartsWithField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static List<object> FilterEndsWithField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static List<object> FilterContainsField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static List<object> FilterDoesNotStartWithField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static List<object> FilterDoesNotEndWithField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static List<object> FilterDoesNotContainField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            return records.Where(record => !propertyInfo.GetValue(record, null).ToString().StartsWith(restrictionPropertyInfo.GetValue(record, null).ToString())).ToList();
        }
        private static List<object> FilterGreaterThanField(List<object> records, PropertyInfo propertyInfo, PropertyInfo restrictionPropertyInfo)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) > (DateTime)restrictionPropertyInfo.GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) > (decimal)restrictionPropertyInfo.GetValue(record, null)).ToList();
                case TypeShort:
                case TypeInteger:
                case TypeUshort:
                case TypeUinteger:
                    return records.Where(record => (int)propertyInfo.GetValue(record, null) > (int)restrictionPropertyInfo.GetValue(record, null)).ToList();
                case TypeLong:
                case TypeUlong:
                    return records.Where(record => (long)propertyInfo.GetValue(record, null) > (long)restrictionPropertyInfo.GetValue(record, null)).ToList();
            }
            return records.Where(record => string.Compare(propertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), restrictionPropertyInfo.GetValue(record, null).ToString().ToLowerInvariant(), StringComparison.Ordinal) > 0).ToList();
        }
        private static List<object> FilterGreaterThanEqualField(List<object> records, PropertyInfo propertyInfo, string equalsFieldName)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) >= (DateTime)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) >= (decimal)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
            }
            return records.Where(record => (int)propertyInfo.GetValue(record, null) >= (int)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
        }
        private static List<object> FilterLessThanField(List<object> records, PropertyInfo propertyInfo, string equalsFieldName)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) < (DateTime)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) < (decimal)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
            }
            return records.Where(record => (int)propertyInfo.GetValue(record, null) < (int)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
        }
        private static List<object> FilterLessThanEqualField(List<object> records, PropertyInfo propertyInfo, string equalsFieldName)
        {
            switch (propertyInfo.GetType().ToString().ToLower())
            {
                case "datetime":
                    return records.Where(record => (DateTime)propertyInfo.GetValue(record, null) <= (DateTime)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
                case "decimal":
                    return records.Where(record => (decimal)propertyInfo.GetValue(record, null) <= (decimal)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
            }
            return records.Where(record => (int)propertyInfo.GetValue(record, null) <= (int)record.GetType().GetProperty(equalsFieldName).GetValue(record, null)).ToList();
        }
        

        #endregion

        #region Date filters

        private static List<object> FilterEqualsDate(List<object> records, PropertyInfo propertyInfo, ConnectorValue.ConnectorDateValueType dateValue)
        {
            if (!IsDate(propertyInfo)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => ((DateTime)propertyInfo.GetValue(record, null)).Date == date.Date).ToList();
        }
        private static List<object> FilterDoesNotEqualDate(List<object> records, PropertyInfo propertyInfo, ConnectorValue.ConnectorDateValueType dateValue)
        {
            if (!IsDate(propertyInfo)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => ((DateTime)propertyInfo.GetValue(record, null)).Date != date.Date).ToList();
        }
        private static List<object> FilterGreaterThanDate(List<object> records, PropertyInfo propertyInfo, ConnectorValue.ConnectorDateValueType dateValue)
        {
            if (!IsDate(propertyInfo)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => ((DateTime)propertyInfo.GetValue(record, null)).Date > date.Date).ToList();
        }
        private static List<object> FilterGreaterThanEqualDate(List<object> records, PropertyInfo propertyInfo, ConnectorValue.ConnectorDateValueType dateValue)
        {
            if (!IsDate(propertyInfo)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => ((DateTime)propertyInfo.GetValue(record, null)).Date >= date.Date).ToList();
        }
        private static List<object> FilterLessThanDate(List<object> records, PropertyInfo propertyInfo, ConnectorValue.ConnectorDateValueType dateValue)
        {
            if (!IsDate(propertyInfo)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => ((DateTime)propertyInfo.GetValue(record, null)).Date < date.Date).ToList();
        }
        private static List<object> FilterLessThanEqualDate(List<object> records, PropertyInfo propertyInfo, ConnectorValue.ConnectorDateValueType dateValue)
        {
            if (!IsDate(propertyInfo)) return records;
            var date = DateHelper.GetDateValue(dateValue);
            return records.Where(record => ((DateTime)propertyInfo.GetValue(record, null)).Date <= date.Date).ToList();
        }
        private static bool IsDate(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;
            return type == typeof(DateTime);
        }

        #endregion

        #endregion

    }
}
