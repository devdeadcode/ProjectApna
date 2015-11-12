using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using eOne.Common.Connectors;

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
            var connector = (Connector)cboConnector.SelectedItem;
            connector.Initialise();
            var issuesList = new List<Issues>();
            var connectorIssues = new Issues
            {
                Errors = IssueHelper.CheckForConnectorErrors(connector),
                Warnings = IssueHelper.CheckForConnectorWarnings(connector)
            };
            issuesList.Add(connectorIssues);
            issuesList.AddRange(connector.Entities.Select(GetEntityIssues));
            foreach (var issues in issuesList)
            {
                var node = treeEntities.Nodes.Add(issues.Entity == null ? "Connector" : issues.Entity.Name);
                node.Tag = issues;
                node.ImageIndex = GetImageIndex(issues.Status);
                node.SelectedImageIndex = node.ImageIndex;
                node.StateImageIndex = node.ImageIndex;
            }
        }
        private static Issues GetEntityIssues(ConnectorEntity entity)
        {
            var issues = new Issues
            {
                Entity = entity,
                Errors = IssueHelper.CheckForEntityErrors(entity),
                Warnings = IssueHelper.CheckForEntityWarnings(entity)
            };
            return issues;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            var saveFile = new SaveFileDialog
            {
                FileName = cboConnector.Text,
                DefaultExt = "csv",
                AddExtension = true,
                Filter = @"CSV Files | *.csv"
            };
            var result = saveFile.ShowDialog();
            if (result == DialogResult.OK) 
            {
                if (!File.Exists(saveFile.FileName))
                {
                    using (var sw = File.CreateText(saveFile.FileName))
                    {
                        sw.WriteLine("Resource type, Entity name, Message type, Message");
                        foreach (TreeNode node in treeEntities.Nodes)
                        {
                            var issues = (Issues)node.Tag;
                            if (node.Text == @"Connector")
                            {
                                foreach (var error in issues.Errors) sw.WriteLine($"Connector, , Error, {error}");
                                foreach (var warning in issues.Warnings) sw.WriteLine($"Connector, , Warning, {warning}");
                            }
                            else
                            {
                                foreach (var error in issues.Errors) sw.WriteLine($"Entity, {node.Text}, Error, {error}");
                                foreach (var warning in issues.Warnings) sw.WriteLine($"Entity, {node.Text}, Warning, {warning}");
                            }
                        }
                    }
                }

            }
        }
    }
}
