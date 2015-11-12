using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using eOne.Common.Connectors;

namespace eOne.Common.Query
{
    public class ConnectorSearch
    {

        public ConnectorSearch()
        {
            Companies = new List<ConnectorCompany>();
        }

        public Connector Connector { get; set; }
        public ConnectorEntity Entity { get; set; }
        public List<ConnectorCompany> Companies { get; set; }
        public string Query { get; set; }
        public List<string> Terms => AllTerms.Where(term => !term.StartsWith("-") && !term.StartsWith("\"")).ToList();
        public List<string> ExcludeTerms => AllTerms.Where(term => term.StartsWith("-")).ToList().Select(term => term.Substring(1)).ToList();
        public List<string> ExactTerms => AllTerms.Where(term => term.StartsWith("\"")).ToList().Select(term => term.Replace("\"", "")).ToList();
        public List<string> NumericTerms
        {
            get
            {
                double value;
                return Terms.Where(term => double.TryParse(term, out value)).ToList();
            }
        }

        private IEnumerable<string> AllTerms
        {
            get
            {
                var spaceSplit = new Regex("(?:^| )(\"(?:[^\"]+|\"\")*\"|[^ ]*)", RegexOptions.Compiled);
                var matches = (from Match match in spaceSplit.Matches(Query) select match.Value.Trim()).ToList();
                return RemoveIgnoreWords(matches);
            }
        }

        private static List<string> RemoveIgnoreWords(IEnumerable<string> terms)
        {
            var ignoreWords = GetIgnoreWords();
            return terms.Where(term => term.Length > 1).Where(term => !ignoreWords.Contains(term)).ToList();
        }
        private static List<string> GetIgnoreWords()
        {
            return new List<string> {"the", "and", "it", "yes", "no", "you", "he", "she", "is", "on", "of", "an", "am",
                "as", "at", "be", "by", "do", "et", "go", "hi", "if", "in", "me", "mr", "st", "my", "ok", "or", "so",
                "up", "us", "we", "all", "are", "but", "can", "did", "end", "get", "got", "has", "him", "her", "his",
                "how", "let", "its", "let", "may", "mrs", "new", "not", "off", "old", "one", "our", "out", "own", "per",
                "too", "use", "who", "why", "way", "was", "yet"};
        }

    }
}
