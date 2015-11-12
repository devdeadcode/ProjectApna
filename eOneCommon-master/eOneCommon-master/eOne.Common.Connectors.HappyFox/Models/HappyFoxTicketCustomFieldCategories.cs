using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketCustomFieldCategories : DataConnectorEntityModel
    {
        public int category { get; set; }
        public int order { get; set; }
    }
}
