using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampGroup
    {

        public DateTime created_at { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public DateTime updated_at { get; set; }
        public List<BaseCampGroup> subgroups { get; set; }
        public List<BaseCampPerson> memberships { get; set; }

        public int number_of_subgroups => subgroups?.Count ?? 0;
        public bool has_subgroups => number_of_subgroups > 0;

        public int number_of_members => memberships?.Count ?? 0;
        public bool has_members => number_of_members > 0;
    }
}