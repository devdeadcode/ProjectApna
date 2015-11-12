using eOne.Common.Query;

namespace eOne.Common
{
    public class Favorite
    {

        public Favorite()
        {
            Query = new ConnectorQuery();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ConnectorQuery Query { get; set; }

    }
}
