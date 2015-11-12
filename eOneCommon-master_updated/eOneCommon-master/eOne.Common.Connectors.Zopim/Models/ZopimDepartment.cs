using System.Collections.Generic;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimDepartment
    {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool enabled { get; set; }
        public List<int> members { get; set; }
        // "settings" : {}

    }
}