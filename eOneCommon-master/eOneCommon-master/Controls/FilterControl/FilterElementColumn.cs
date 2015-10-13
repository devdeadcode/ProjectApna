using System;
using System.Windows.Forms;

namespace FilterControl
{
    public class FilterElementColumn : DataGridViewColumn
    {
        public FilterElementColumn() : base(new FilterElementCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(FilterElementCell)))
                {
                    throw new InvalidCastException("Cell is not a filter element");
                }
                base.CellTemplate = value;
            }
        }
    }
}
