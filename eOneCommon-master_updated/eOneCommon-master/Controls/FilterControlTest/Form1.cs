using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FilterControl.Classes;

namespace FilterControlTest
{
    public partial class Form1 : Form
    {

        private List<FilterColumn> _columns;
        
        public Form1()
        {
            InitializeComponent();
            _columns = new List<FilterColumn>
            {
                new FilterColumn("Customer Number", "RM01001.CUSTNMBR", Enums.ColumnType.String),
                new FilterColumn("Created Date", "RM01001.CREATDT", Enums.ColumnType.Date),
                new FilterColumn("Customer Balance", "RM01003.CUSTBLNC", Enums.ColumnType.Currency),
            };
            var listColumn = new FilterColumn("Customer Priority", "RM01003.PRIORITY", Enums.ColumnType.Enum);
            listColumn.EnumValues.Add(new Tuple<int, string>(1, "High"));
            listColumn.EnumValues.Add(new Tuple<int, string>(2, "Medium"));
            listColumn.EnumValues.Add(new Tuple<int, string>(3, "Low"));
            _columns.Add(listColumn);
            filterElementControl1.LoadColumns(_columns);
        }

        private void filterElementControl1_ElementChanged(object sender, EventArgs e)
        {
            radTextBoxControl1.Text = filterElementControl1.Sql;
        }
    }
}
