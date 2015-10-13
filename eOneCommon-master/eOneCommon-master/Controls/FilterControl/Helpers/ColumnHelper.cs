using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterControl.Classes;
using Telerik.WinControls.UI;

namespace FilterControl.Helpers
{
    public class ColumnHelper
    {

        public static void FillColumnList(List<FilterColumn> columns, RadDropDownList listField)
        {
            listField.Items.Clear();
            foreach (var column in columns.OrderBy(o => o.Label).ToList())
            {
                listField.Items.Add(new RadListDataItem(column.Label, column));
            }
        }

    }
}
