using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using eOne.Common.Connectors;
using eOne.Common.Query;

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
            var connector = (Connector)cboConnector.SelectedItem;
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
                        AddFavoriteNotes(entity, entityNode);
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
                    AddFavoriteNotes(entity, entityNode);
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

        private static void AddFieldNodes(ConnectorEntity entity, TreeNode entityNode)
        {
            if (entity.Fields.Count == 0) return;
            var fieldsNode = entityNode.Nodes.Add("Fields");
            foreach (var field in entity.Fields)
            {
                string fieldName = field.DisplayName;
                if (field.DefaultField) fieldName = $"{fieldName} (default)";
                var fieldNode = fieldsNode.Nodes.Add(fieldName);
                fieldNode.Tag = field;
                if (field.ListItems.Count > 0)
                {
                    foreach (var listItem in field.ListItems) fieldNode.Nodes.Add(listItem.Value);
                }
            }
        }

        private static void AddFavoriteNotes(ConnectorEntity entity, TreeNode entityNode)
        {
            if (entity.Favorites.Count == 0) return;
            var favoritesNode = entityNode.Nodes.Add("Favorites");
            foreach (var favorite in entity.Favorites)
            {
                var favoriteNode = favoritesNode.Nodes.Add(favorite.Name);
                favoriteNode.Tag = favorite;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            var endTime = DateTime.Now;

            string xml = string.Empty;

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;

            if (treeView1.SelectedNode == null || cboConnector.SelectedItem == null) return;

            var connector = (Connector)cboConnector.SelectedItem;

            var entity = treeView1.SelectedNode.Tag as ConnectorEntity;
            if (entity != null)
            {
                var query = entity.DefaultQueryXml;
                xml = connector.GetData(query, Connector.ConnectorSerializationType.Xml);
                endTime = DateTime.Now;
                if (cbJson.Checked) textBox2.Text = connector.GetData(query, Connector.ConnectorSerializationType.Json);
                if (cbCsv.Checked) textBox3.Text = connector.GetData(query, Connector.ConnectorSerializationType.Csv);
            }

            var favorite = treeView1.SelectedNode.Tag as Favorite;
            if (favorite != null)
            {
                var query = favorite.Query;
                if (query.Entity.ParentConnector.Multicompany) query.Companies.Add(query.Entity.ParentConnector.Companies[0]);
                xml = connector.GetData(query, Connector.ConnectorSerializationType.Xml);
                endTime = DateTime.Now;
                if (cbJson.Checked) textBox2.Text = connector.GetData(query, Connector.ConnectorSerializationType.Json);
                if (cbCsv.Checked) textBox3.Text = connector.GetData(query, Connector.ConnectorSerializationType.Csv);
            }

            var field = treeView1.SelectedNode.Tag as ConnectorField;
            if (field != null)
            {
                var query = new ConnectorQuery
                {
                    Connector = connector,
                    Entity = field.ParentEntity
                };
                if (connector.Multicompany) query.Companies.Add(connector.Companies[0]);
                query.AddField(field.Id);
                xml = connector.GetData(query, Connector.ConnectorSerializationType.Xml);
                endTime = DateTime.Now;
                if (cbJson.Checked) textBox2.Text = connector.GetData(query, Connector.ConnectorSerializationType.Json);
                if (cbCsv.Checked) textBox3.Text = connector.GetData(query, Connector.ConnectorSerializationType.Csv);
            }

            if (cbXml.Checked) textBox1.Text = xml;

            var xmlData = new DataSet();
            if (!string.IsNullOrWhiteSpace(xml))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlData.ReadXml(new XmlNodeReader(xmlDoc));
            }
            if (xmlData.Tables.Count > 0) dataGridView1.DataSource = xmlData.Tables[0];

            dataGridView1.Refresh();
            txtRecordCount.Text = dataGridView1.RowCount.ToString();

            var duration = endTime - startTime;
            txtTimeTaken.Text = duration.TotalSeconds.ToString(CultureInfo.InvariantCulture);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            var connector = (Connector)cboConnector.SelectedItem;
            var entity = treeView1.SelectedNode.Tag as ConnectorEntity;
            if (entity == null) return;
            
            var fields = new DataTable();
            fields.Columns.Add("Field name", typeof(string));
            fields.Columns.Add("Display name", typeof(string));
            fields.Columns.Add("Number of records", typeof(int));
            dataGridView1.DataSource = fields;

            var query = new ConnectorQuery { Entity = entity, Connector = connector };
            if (connector.Multicompany) query.Companies.Add(connector.Companies[0]);
            foreach (var field in entity.Fields)
            {
                query.Fields.Clear();
                query.Page = 1;
                query.AddField(field.Id);
                fields.Rows.Add(field.Name, field.DisplayName, GetRecordCount(query));
                dataGridView1.Refresh();
            }
        }

        private static int GetRecordCount(ConnectorQuery query)
        {
            var xml = query.Connector.GetData(query, Connector.ConnectorSerializationType.Xml);
            if (string.IsNullOrWhiteSpace(xml)) return 0;

            var xmlData = new DataSet();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlData.ReadXml(new XmlNodeReader(xmlDoc));
            return xmlData.Tables.Count > 0 ? xmlData.Tables[0].Rows.Count : 0;
        }

    }
}
