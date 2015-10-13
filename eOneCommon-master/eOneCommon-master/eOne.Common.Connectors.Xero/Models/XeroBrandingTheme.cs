using System;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroBrandingTheme
    {

        public string BrandingThemeID { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedDateUTC { get; set; }

    }
}