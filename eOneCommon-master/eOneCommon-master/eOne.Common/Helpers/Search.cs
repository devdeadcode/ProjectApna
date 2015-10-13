using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Helpers
{
    public class Search
    {

        public static List<ConnectorRestriction> GetSearchRestrictions(ConnectorSearch search)
        {
            var restrictions = search.Entity.SearchStringFields.Select(field => GetSearchRestriction(field, search.Terms)).ToList();
            if (search.NumericTerms.Count > 0) restrictions.AddRange(search.Entity.SearchNumericFields.Select(field => GetSearchRestriction(field, search.Terms, true)));
            if (restrictions.Count > 0) restrictions[0].ConjunctiveOperator = ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None;
            return restrictions;
        }
        
        private static ConnectorRestriction GetSearchRestriction(DataConnectorField field, IEnumerable<string> terms, bool numeric = false)
        {
            var restriction = new ConnectorRestriction
            {
                Field = field,
                RestrictionType = numeric ? ConnectorRestriction.ConnectorRestrictionType.OneOfList : ConnectorRestriction.ConnectorRestrictionType.ContainsOneOfList,
                ConjunctiveOperator = ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or
            };
            foreach (var term in terms) restriction.Values.Add(GetSearchRestrictionValue(term));
            return restriction;
        }
        private static ConnectorRestrictionValue GetSearchRestrictionValue(string term)
        {
            var value = new ConnectorRestrictionValue
            {
                Type = ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant,
                Constant = term
            };
            return value;
        }
        
    }
}
