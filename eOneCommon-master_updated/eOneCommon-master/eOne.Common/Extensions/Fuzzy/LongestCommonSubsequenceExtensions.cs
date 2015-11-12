using System;

namespace eOne.Common.Extensions.Fuzzy
{
    public static class LongestCommonSubsequenceExtensions
    {

        private enum LcsDirection
        {
            None,
            North,
            West,
            NorthWest
        }

        public static Tuple<string, double> LongestCommonSubsequence(this string input, string comparedTo, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(comparedTo)) return new Tuple<string, double>(string.Empty, 0.0d);
            if (!caseSensitive)
            {
                input = input.ToLower();
                comparedTo = comparedTo.ToLower();
            }

            var inputLen = input.Length;
            var comparedToLen = comparedTo.Length;

            var lcs = new int[inputLen + 1, comparedToLen + 1];
            var tracks = new LcsDirection[inputLen + 1, comparedToLen + 1];
            int[,] w = new int[inputLen + 1, comparedToLen + 1];
            int i, j;

            for (i = 0; i <= inputLen; ++i)
            {
                lcs[i, 0] = 0;
                tracks[i, 0] = LcsDirection.North;

            }
            for (j = 0; j <= comparedToLen; ++j)
            {
                lcs[0, j] = 0;
                tracks[0, j] = LcsDirection.West;
            }

            for (i = 1; i <= inputLen; ++i)
            {
                for (j = 1; j <= comparedToLen; ++j)
                {
                    if (input[i - 1].Equals(comparedTo[j - 1]))
                    {
                        var k = w[i - 1, j - 1];
                        lcs[i, j] = lcs[i - 1, j - 1] + Square(k + 1) - Square(k);
                        tracks[i, j] = LcsDirection.NorthWest;
                        w[i, j] = k + 1;
                    }
                    else
                    {
                        lcs[i, j] = lcs[i - 1, j - 1];
                        tracks[i, j] = LcsDirection.None;
                    }

                    if (lcs[i - 1, j] >= lcs[i, j])
                    {
                        lcs[i, j] = lcs[i - 1, j];
                        tracks[i, j] = LcsDirection.North;
                        w[i, j] = 0;
                    }

                    if (lcs[i, j - 1] >= lcs[i, j])
                    {
                        lcs[i, j] = lcs[i, j - 1];
                        tracks[i, j] = LcsDirection.West;
                        w[i, j] = 0;
                    }
                }
            }

            i = inputLen;
            j = comparedToLen;

            var subseq = "";
            double p = lcs[i, j];

            // trace the backtracking matrix
            while (i > 0 || j > 0)
            {
                switch (tracks[i, j])
                {
                    case LcsDirection.NorthWest:
                        i--;
                        j--;
                        subseq = input[i] + subseq;
                        break;
                    case LcsDirection.North:
                        i--;
                        break;
                    case LcsDirection.West:
                        j--;
                        break;
                }
            }

            var coef = p / (inputLen * comparedToLen);

            return new Tuple<string, double>(subseq, coef);
        }

        private static int Square(int p)
        {
            return p * p;
        }

    }
}
