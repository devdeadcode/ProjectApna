using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors
{
    public class ConnectorEntityGroup
    {

        public ConnectorEntityGroup(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentGroup { get; set; }
        public Connector ParentConnector { get; set; }

        public List<ConnectorEntity> Entities
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
