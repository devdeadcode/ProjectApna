using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using eOne.Common.Connectors;

namespace eOne.Common.SearchTest
{
    public partial class Search : Form
    {
        public Search()
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;

            dataResults.DataSource = null;
            dataResults.Columns.Clear();
            dataResults.Refresh();

            var connector = (Connector)cboConnector.SelectedItem;
            var entity = (ConnectorEntity)cboEntity.SelectedItem;
            var companyId = connector.Companies.Count == 0 ? 0 : connector.Companies[0].Id;
            var xml = connector.Search(txtSearch.Text, entity.Id, companyId, Connector.ConnectorSerializationType.Xml);

            var endTime = DateTime.Now;

            var xmlData = new DataSet();
            if (!string.IsNullOrWhiteSpace(xml))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlData.ReadXml(new XmlNodeReader(xmlDoc));
            }
            if (xmlData.Tables.Count > 0) dataResults.DataSource = xmlData.Tables[0];

            dataResults.Refresh();

            var duration = endTime - startTime;
            txtResponseTime.Text = duration.TotalSeconds.ToString(CultureInfo.InvariantCulture);

        }
    }
}
