namespace eOne.Common.Connectors.BombBomb.Models
{
    public class BombBombCollection
    {

        public enum BombBombCollectionStatus
        {
            success,
            failure
        }

        public BombBombCollectionStatus status { get; set; }
        public string methodName { get; set; }

    }
}
