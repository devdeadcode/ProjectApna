using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyTaxonomy : DataConnectorEntityModel
    {

        public int id { get; set; }
        public int level { get; set; }
        public string name { get; set; }
        public string parent { get; set; }
        public int parent_id { get; set; }
        public string path { get; set; }
        public int category_id { get; set; }
        public List<EtsyTaxonomy> children { get; set; }
        public List<int> children_ids { get; set; }
        public List<int> full_path_taxonomy_ids { get; set; }

    }
}
