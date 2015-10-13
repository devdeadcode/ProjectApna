namespace eOne.Common.DataConnectors
{
    public class DataConnectorCompany
    {

        public DataConnectorCompany() { }
        public DataConnectorCompany(int id, string name, string databaseName = "")
        {
            Id = id;
            Name = name;
            DatabaseName = databaseName;
            Enabled = true;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DatabaseName { get; set; }
        public bool TestCompany { get; set; }
        public bool Enabled { get; set; }
        public Connector ParentConnector { get; set; }

    }
}
