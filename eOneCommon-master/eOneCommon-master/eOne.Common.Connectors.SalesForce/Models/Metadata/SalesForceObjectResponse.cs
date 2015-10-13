using System.Collections.Generic;

namespace eOne.Common.Connectors.SalesForce.Models.Metadata
{
    public class SalesForceObjectResponse
    {

        public string encoding { get; set; }
        public string maxBatchSize { get; set; }
        public List<SalesForceObject> sobjects { get; set; }

}
}
