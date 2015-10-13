using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class IndexModel
    {
        public List<EntityInfo> Entities { get; set;}

        public IndexModel()
        {
            Entities = new List<EntityInfo>();
        }
    }

    public class EntityInfo
    {
        public string EntityName {get; set;}
        public List<string> AttributeNames {get; set;}

        public EntityInfo()
        {
            AttributeNames = new List<string>();
        }
    }
}