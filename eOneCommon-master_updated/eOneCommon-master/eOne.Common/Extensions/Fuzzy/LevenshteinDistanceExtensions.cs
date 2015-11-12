using System.Linq;

namespace eOne.Common.Extensions.Fuzzy
{
    public static class LevenshteinDistanceExtensions
    {

        public static int LevenshteinDistance(this string input, string comparedTo, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(comparedTo)) return -1;
            if (!caseSensitive)
            {
                input = input.ToLower();
                comparedTo = comparedTo.ToLower();
            }
            var inputLen = input.Length;
            var comparedToLen = comparedTo.Length;

            var matrix = new int[inputLen, comparedToLen];

            //initialize
            for (var i = 0; i < inputLen; i++) matrix[i, 0] = i;
            for (var i = 0; i < comparedToLen; i++) matrix[0, i] = i;

            //analyze
            for (var i = 1; i < inputLen; i++)
            {
                var si = input[i - 1];
                for (var j = 1; j < comparedToLen; j++)
                {
                    var tj = comparedTo[j - 1];
                    var cost = (si == tj) ? 0 : 1;

                    var above = matrix[i - 1, j];
                    var left = matrix[i, j - 1];
                    var diag = matrix[i - 1, j - 1];
                    var cell = FindMinimum(above + 1, left + 1, diag + cost);

                    //transposition
                    if (i > 1 && j > 1)
                    {
                        var trans = matrix[i - 2, j - 2] + 1;
                        if (input[i - 2] != comparedTo[j - 1]) trans++;
                        if (input[i - 1] != comparedTo[j - 2]) trans++;
                        if (cell > trans) cell = trans;
                    }
                    matrix[i, j] = cell;
                }
            }
            return matrix[inputLen - 1, comparedToLen - 1];
        }

        private static int FindMinimum(params int[] p)
        {
            return p?.Concat(new[] {int.MaxValue}).Min() ?? int.MinValue;
        }
    }
}
