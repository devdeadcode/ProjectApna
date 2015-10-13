using System;
using System.Windows.Forms;
using FilterControl.Classes;

namespace FilterControl
{
    public class FilterElementCell : DataGridViewTextBoxCell
    {

        public FilterElementCell() : base()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            var ctl = DataGridView.EditingControl as FilterElementControl;
            if (Value == null)
            {
                if (ctl != null) ctl.Element = (FilterElement)DefaultNewRowValue;
            }
            else
            {
                if (ctl != null) ctl.Element = (FilterElement)Value;
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(FilterElementControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(FilterElement);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return new FilterElement();
            }
        }

    }
}
