using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors;
using eOne.Common.Query;

namespace eOne.Common.Helpers
{
    public class SearchHelper
    {

        private enum SearchTermType
        {
            String,
            Number,
            Exclude,
            Exact
        }

        public static List<ConnectorRestriction> GetSearchRestrictions(ConnectorSearch search)
        {
            var restrictions = search.Entity.SearchStringFields.Select(field => GetSearchRestriction(field, search.Terms, SearchTermType.String)).ToList();
            if (search.ExactTerms.Count > 0) restrictions.AddRange(search.Entity.SearchStringFields.Select(field => GetSearchRestriction(field, search.ExactTerms, SearchTermType.Exact)));
            if (search.NumericTerms.Count > 0) restrictions.AddRange(search.Entity.SearchNumericFields.Select(field => GetSearchRestriction(field, search.Terms, SearchTermType.Number)));
            if (search.ExcludeTerms.Count > 0) restrictions.AddRange(search.Entity.SearchStringFields.Select(field => GetSearchRestriction(field, search.ExcludeTerms, SearchTermType.Exclude)));
            return restrictions;
        }
        
        private static ConnectorRestriction GetSearchRestriction(ConnectorField field, IReadOnlyCollection<string> terms, SearchTermType type)
        {
            var restriction = new ConnectorRestriction
            {
                Field = field,
                ConjunctiveOperator = GetSearchConjunctive(type),
                RestrictionType = GetSearchRestrictionType(type, terms.Count)
            };
            foreach (var term in terms) restriction.Values.Add(GetSearchRestrictionValue(term));
            return restriction;
        }
        private static ConnectorValue GetSearchRestrictionValue(string term)
        {
            var value = new ConnectorValue
            {
                Type = ConnectorValue.ConnectorValueType.Constant,
                Constant = term
            };
            return value;
        }
        private static ConnectorRestriction.ConnectorRestrictionConjunctiveOperator GetSearchConjunctive(SearchTermType type)
        {
            switch (type)
            {
                case SearchTermType.Exclude:
                    return ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And;
                default:
                    return ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or;
            }
        }
        private static ConnectorRestriction.ConnectorRestrictionType GetSearchRestrictionType(SearchTermType type, int termsCount)
        {
            switch (type)
            {
                case SearchTermType.String:
                    //return ConnectorRestriction.ConnectorRestrictionType.Fuzzy;
                case SearchTermType.Exact:
                    return termsCount == 1 ? ConnectorRestriction.ConnectorRestrictionType.Contains : ConnectorRestriction.ConnectorRestrictionType.ContainsOneOfList;
                case SearchTermType.Number:
                    return termsCount == 1 ? ConnectorRestriction.ConnectorRestrictionType.Equals : ConnectorRestriction.ConnectorRestrictionType.OneOfList;
                case SearchTermType.Exclude:
                    return termsCount == 1 ? ConnectorRestriction.ConnectorRestrictionType.DoesNotContain : ConnectorRestriction.ConnectorRestrictionType.DoesNotContainOneOfList;
            }
            return ConnectorRestriction.ConnectorRestrictionType.Equals;
        }

    }
}
