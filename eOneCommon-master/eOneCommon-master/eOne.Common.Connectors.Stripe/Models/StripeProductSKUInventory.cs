using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeProductSKUInventory : DataConnectorEntityModel
    {
        #region Enum
        public enum InventoryType
        {
            [Description("Finite")]
            finite,
            [Description("Bucket")]
            bucket,
            [Description("Infinite")]
            infinite
        }

        public enum InventoryValue
        {
            None,
            [Description("In stock")]
            in_stock,
            [Description("Limited stock")]
            limited,
            [Description("Out of stock")]
            out_of_stock,
            
        }
        #endregion

        public int? quantity { get; set; }
        public InventoryType? type { get; set; }
        public InventoryValue? value { get; set; }
    }
}
