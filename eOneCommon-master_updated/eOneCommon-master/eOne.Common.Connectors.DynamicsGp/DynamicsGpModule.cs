using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp
{
    public abstract class DynamicsGpModule
    {

        protected DynamicsGpModule(DynamicsGpConnector connector)
        {
            ParentConnector = connector;
            Entities = new List<ConnectorEntity>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool Installed { get; set; }
        public List<ConnectorEntity> Entities { get; set; }
        public DynamicsGpConnector ParentConnector { get; set; }

        public abstract void AddEntities();

        public int GetEntityId(int entityId)
        {
            return Id * 10000 + entityId;
        }
    }
}
