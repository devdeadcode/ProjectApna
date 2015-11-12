using System.Text;

namespace eOne.Common.Extensions.Fuzzy
{
    public class MetaphoneData
    {

        readonly StringBuilder _primary = new StringBuilder(5);
        readonly StringBuilder _secondary = new StringBuilder(5);

        #region Properties

        internal bool Alternative { get; set; }
        internal int PrimaryLength => _primary.Length;
        internal int SecondaryLength => _secondary.Length;

        #endregion

        internal void Add(string main)
        {
            if (main != null)
            {
                _primary.Append(main);
                _secondary.Append(main);
            }
        }
        internal void Add(string main, string alternative)
        {
            if (main != null) _primary.Append(main);
            if (alternative != null)
            {
                Alternative = true;
                if (alternative.Trim().Length > 0) _secondary.Append(alternative);
            }
            else
            {
                if (main != null && main.Trim().Length > 0) _secondary.Append(main);
            }
        }
        public override string ToString()
        {
            var ret = (Alternative ? _secondary : _primary).ToString();
            // only give back 4 char metaphone
            if (ret.Length > 4) ret = ret.Substring(0, 4);
            return ret;
        }

    }
}
