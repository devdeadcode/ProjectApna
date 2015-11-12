using System;
using System.Linq;
using System.Windows.Forms;
using eOne.Common.Connectors;
using eOne.Common.Helpers;

namespace eOne.Common.Experiments
{
    public partial class CompareRecords : Form
    {

        public CompareRecords()
        {
            InitializeComponent();
            AddConnectors();
            cboType.Items.Add("Differences");
            cboType.Items.Add("Similarities");
        }

        private void AddConnectors()
        {
            var connectors = SampleConnectors.SampleConnectors.GetConnectors();
            foreach (var connector in connectors)
            {
                cboConnectorFrom.Items.Add(connector);
                cboConnectorTo.Items.Add(connector);
            }
        }

        private void cboConnectorFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboListFrom.Items.Clear();
            cboFieldFrom.Items.Clear();
            var connector = (Connector)cboConnectorFrom.SelectedItem;
            connector.Initialise();
            foreach (var entity in connector.Entities) cboListFrom.Items.Add(entity);
        }

        private void cboConnectorTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboListTo.Items.Clear();
            cboFieldTo.Items.Clear();
            var connector = (Connector)cboConnectorTo.SelectedItem;
            connector.Initialise();
            foreach (var entity in connector.Entities) cboListTo.Items.Add(entity);
        }

        private void cboListFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFieldFrom.Items.Clear();
            var entity = (ConnectorEntity)cboListFrom.SelectedItem;
            foreach (var field in entity.Fields.Where(field => field.DefaultField)) cboFieldFrom.Items.Add(field);
        }

        private void cboListTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFieldTo.Items.Clear();
            var entity = (ConnectorEntity)cboListTo.SelectedItem;
            foreach (var field in entity.Fields.Where(field => field.DefaultField)) cboFieldTo.Items.Add(field);
        }

        private void cboFieldFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboFieldTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            var fromConnector = (Connector)cboConnectorFrom.SelectedItem;
            var toConnector = (Connector)cboConnectorTo.SelectedItem;
            var fromEntity = (ConnectorEntity)cboListFrom.SelectedItem;
            var toEntity = (ConnectorEntity)cboListTo.SelectedItem;
            var fromField = (ConnectorField)cboFieldFrom.SelectedItem;
            var toField = (ConnectorField)cboFieldTo.SelectedItem;
            var fromRecords = fromConnector.GetRecords(fromEntity.DefaultQuery);
            var toRecords = toConnector.GetRecords(toEntity.DefaultQuery);
            var fieldNames = TupleHelper.CreateTupleStringList(fromField.Name, toField.Name);
            switch (cboType.SelectedIndex)
            {
                case 0:
                    Comparison.GetDifferences(fromRecords, fromEntity.ListType, toRecords, toEntity.ListType, fieldNames);
                    break;
                case 1:
                    Comparison.GetSimilarities(fromRecords, fromEntity.ListType, toRecords, toEntity.ListType, fieldNames);
                    break;
            }
        }

    }
}
