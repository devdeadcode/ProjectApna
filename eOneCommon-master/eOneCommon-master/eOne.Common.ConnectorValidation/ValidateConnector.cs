using System;
using System.Collections.Generic;
using System.Windows.Forms;
using eOne.Common.DataConnectors;

namespace eOne.Common.ConnectorValidation
{
    public partial class ValidateConnector : Form
    {

        public ValidateConnector()
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

        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            treeEntities.Nodes.Clear();
            var connector = (DataConnector)cboConnector.SelectedItem;
            connector.Initialise();
            var issuesList = new List<Issues>();
            var connectorIssues = new Issues
            {
                Errors = IssueHelper.CheckForConnectorErrors(connector),
                Warnings = IssueHelper.CheckForConnectorWarnings(connector)
            };
            issuesList.Add(connectorIssues);
            foreach (var entity in connector.Entities)
            {
                var issues = new Issues
                {
                    Entity = entity,
                    Errors = IssueHelper.CheckForEntityErrors(entity),
                    Warnings = IssueHelper.CheckForEntityWarnings(entity)
                };
                issuesList.Add(issues);
            }
            foreach (var issues in issuesList)
            {
                var node = treeEntities.Nodes.Add(issues.Entity == null ? "Connector" : issues.Entity.Name);
                node.Tag = issues;
                node.ImageIndex = GetImageIndex(issues.Status);
                node.SelectedImageIndex = node.ImageIndex;
                node.StateImageIndex = node.ImageIndex;
            }
        }

        private static int GetImageIndex(Issues.IssueStatus status)
        {
            switch (status)
            {
                case Issues.IssueStatus.OK:
                    return 0;
                case Issues.IssueStatus.Warnings:
                    return 1;
                case Issues.IssueStatus.Errors:
                    return 2;
                case Issues.IssueStatus.ErrorsAndWarnings:
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private void treeEntities_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var issues = (Issues)e.Node.Tag;
            txtErrors.Text = IssueHelper.GetIssuesText(issues.Errors);
            txtWarnings.Text = IssueHelper.GetIssuesText(issues.Warnings);
        }

    }
}
