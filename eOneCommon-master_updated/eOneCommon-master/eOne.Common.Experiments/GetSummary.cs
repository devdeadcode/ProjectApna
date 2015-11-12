using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using eOne.Common.Connectors;
using eOne.Common.Helpers;

namespace eOne.Common.Experiments
{
    public partial class GetSummary : Form
    {
        public GetSummary()
        {
            InitializeComponent();
            AddConnectors();
        }

        private void AddConnectors()
        {
            var connectors = SampleConnectors.SampleConnectors.GetConnectors();
            foreach (var connector in connectors) cboConnector.Items.Add(connector);
        }

        private void cboConnector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connector = (Connector)cboConnector.SelectedItem;
            connector.Initialise();
            cboEntity.Items.Clear();
            foreach (var entity in connector.Entities) cboEntity.Items.Add(entity);
        }

        private void cboEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entity = (ConnectorEntity)cboEntity.SelectedItem;
            treeGroupFields.Nodes.Clear();
            treeSummaryFields.Nodes.Clear();
            foreach (var field in entity.Fields)
            {
                var groupNode = treeGroupFields.Nodes.Add(field.DisplayName);
                groupNode.Tag = field;
                var summaryNode = treeSummaryFields.Nodes.Add(field.DisplayName);
                summaryNode.Tag = field;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            summaryData.DataSource = null;
            summaryData.Columns.Clear();
            summaryData.Refresh();

            rawData.DataSource = null;
            rawData.Columns.Clear();
            rawData.Refresh();

            var connector = (Connector)cboConnector.SelectedItem;
            var entity = (ConnectorEntity)cboEntity.SelectedItem;
            var query = entity.DefaultQuery;
            query.Fields.Clear();

            var groupFields = new List<string>();
            foreach (var field in from TreeNode node in treeGroupFields.Nodes where node.Checked select (ConnectorField)node.Tag)
            {
                groupFields.Add(field.Name);
                query.Fields.Add(field);
            }

            if (groupFields.Count == 0)
            {
                MessageBox.Show("No group fields have been selected");
                return;
            }

            var sumFields = new List<string>();
            foreach (var field in from TreeNode node in treeSummaryFields.Nodes where node.Checked select (ConnectorField)node.Tag)
            {
                sumFields.Add(field.Name);
                query.Fields.Add(field);
            }

            var data = connector.GetRecords(query).ToList();
            var xml = connector.Serialize(query, data, Connector.ConnectorSerializationType.Xml);

            var xmlData = new DataSet();
            if (!string.IsNullOrWhiteSpace(xml))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlData.ReadXml(new XmlNodeReader(xmlDoc));
            }
            if (xmlData.Tables.Count > 0) rawData.DataSource = xmlData.Tables[0];
            rawData.Refresh();
            txtRawCount.Text = rawData.RowCount.ToString();

            data = Summary.Summarize(data, entity.RecordType, groupFields, sumFields);
            xml = connector.Serialize(query, data, Connector.ConnectorSerializationType.Xml);

            var summaryXmlData = new DataSet();
            if (!string.IsNullOrWhiteSpace(xml))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                summaryXmlData.ReadXml(new XmlNodeReader(xmlDoc));
            }
            if (summaryXmlData.Tables.Count > 0) summaryData.DataSource = summaryXmlData.Tables[0];
            summaryData.Refresh();
            txtSummaryCount.Text = summaryData.RowCount.ToString();

        }
    }
}
