using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using eOne.Common.DataConnectors;

namespace eOne.Common.ConnectorTest
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            AddConnectors();
        }

        private void AddConnectors()
        {
            var connectors = SampleConnectors.SampleConnectors.GetConnectors();
            foreach (var connector in connectors) cboConnector.Items.Add(connector);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            var connector = (DataConnector)cboConnector.SelectedItem;
            connector.Initialise();
            if (connector.Groups.Count > 0)
            {
                foreach (var group in connector.Groups)
                {
                    var groupNode = treeView1.Nodes.Add(group.Name);
                    foreach (var entity in group.Entities)
                    {
                        var entityNode = groupNode.Nodes.Add(entity.Name);
                        entityNode.Tag = entity;
                        AddFieldNodes(entity, entityNode);
                    }
                }
            }
            else
            {
                foreach (var entity in connector.Entities)
                {
                    var entityNode = treeView1.Nodes.Add(entity.Name);
                    entityNode.Tag = entity;
                    AddFieldNodes(entity, entityNode);
                }
            }
            
            if (connector.Multicompany)
            {
                foreach (var company in connector.Companies)
                {
                    treeView2.Nodes.Add(company.Name);
                }
            }
        }

        private void AddFieldNodes(DataConnectorEntity entity, TreeNode entityNode)
        {
            foreach (var field in entity.Fields)
            {
                string fieldName = field.DisplayName;
                if (field.DefaultField) fieldName = $"{fieldName} (default)";
                var fieldNode = entityNode.Nodes.Add(fieldName);
                fieldNode.Tag = field;
                if (field.ListItems.Count > 0)
                {
                    foreach (var listItem in field.ListItems) fieldNode.Nodes.Add(listItem.Value);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || cboConnector.SelectedItem == null) return;
            var connector = (DataConnector)cboConnector.SelectedItem;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            var entity = treeView1.SelectedNode.Tag as DataConnectorEntity;
            if (entity != null)
            {
                var query = entity.DefaultQuery;
                textBox1.Text = connector.GetData(query, DataConnector.DataConnectorSerializationType.Xml);
                var xmlData = new DataSet();
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(textBox1.Text);
                    xmlData.ReadXml(new XmlNodeReader(xmlDoc));
                }
                if (xmlData.Tables.Count > 0) dataGridView1.DataSource = xmlData.Tables[0];
                textBox2.Text = connector.GetData(query, DataConnector.DataConnectorSerializationType.Json);
            }
            else
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
            dataGridView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
