using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOne.Common.DataConnectors
{
    public class DataConnectorEntityGroup
    {

        public DataConnectorEntityGroup(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentGroup { get; set; }
        public DataConnector ParentConnector { get; set; }

        public List<DataConnectorEntity> Entities
        {
            get
            {
                return ParentConnector.Entities.Where(entity => entity.Group.Id == Id).ToList();
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
