using System.Collections.Generic;

namespace eOne.Common.Connectors.Infusionsoft.Models.Query
{
    public class InfusionSoftDataServiceQuery
    {

        public InfusionSoftDataServiceQuery()
        {
            selectedFields = new List<string>();
        }

        public string privateKey { get; set; }
        public string table { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public object queryData { get; set; }
        public List<string> selectedFields { get; set; }
        public string orderBy { get; set; }
        public bool? ascending { get; set; }

    }
}
