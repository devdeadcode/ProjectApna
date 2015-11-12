using System.ComponentModel;
using System.Windows.Forms;

namespace eOne.Common.ConnectorTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cboConnector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cbCsv = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbXml = new System.Windows.Forms.CheckBox();
            this.cbJson = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRecordCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimeTaken = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboConnector
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cboConnector, 2);
            this.cboConnector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboConnector.FormattingEnabled = true;
            this.cboConnector.Location = new System.Drawing.Point(73, 3);
            this.cboConnector.Name = "cboConnector";
            this.cboConnector.Size = new System.Drawing.Size(210, 21);
            this.cboConnector.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Size = new System.Drawing.Size(64, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connector";
            // 
            // treeView1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.treeView1, 3);
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 57);
            this.treeView1.Name = "treeView1";
            this.tableLayoutPanel1.SetRowSpan(this.treeView1, 3);
            this.treeView1.Size = new System.Drawing.Size(280, 300);
            this.treeView1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "init";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.treeView2, 3);
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Location = new System.Drawing.Point(3, 387);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(280, 184);
            this.treeView2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(289, 387);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 184);
            this.textBox1.TabIndex = 5;
            this.textBox1.WordWrap = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(399, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "get data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.cboConnector, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.button2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 7, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.cbCsv, 7, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbXml, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbJson, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.button1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRecordCount, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtTimeTaken, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.button3, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1056, 574);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 30);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3);
            this.label6.Size = new System.Drawing.Size(64, 24);
            this.label6.TabIndex = 15;
            this.label6.Text = "Lists:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label8, 2);
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 360);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(3);
            this.label8.Size = new System.Drawing.Size(210, 24);
            this.label8.TabIndex = 17;
            this.label8.Text = "Companies:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 6);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(289, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.dataGridView1, 3);
            this.dataGridView1.Size = new System.Drawing.Size(764, 300);
            this.dataGridView1.TabIndex = 7;
            // 
            // textBox3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox3, 2);
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(801, 387);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(252, 184);
            this.textBox3.TabIndex = 11;
            this.textBox3.WordWrap = false;
            // 
            // textBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox2, 2);
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(545, 387);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(250, 184);
            this.textBox2.TabIndex = 8;
            this.textBox2.WordWrap = false;
            // 
            // cbCsv
            // 
            this.cbCsv.AutoSize = true;
            this.cbCsv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCsv.Location = new System.Drawing.Point(801, 363);
            this.cbCsv.Name = "cbCsv";
            this.cbCsv.Size = new System.Drawing.Size(104, 18);
            this.cbCsv.TabIndex = 19;
            this.cbCsv.Text = "CSV";
            this.cbCsv.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(289, 30);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(3);
            this.label7.Size = new System.Drawing.Size(104, 24);
            this.label7.TabIndex = 16;
            this.label7.Text = "Data:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbXml
            // 
            this.cbXml.AutoSize = true;
            this.cbXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbXml.Location = new System.Drawing.Point(289, 363);
            this.cbXml.Name = "cbXml";
            this.cbXml.Size = new System.Drawing.Size(104, 18);
            this.cbXml.TabIndex = 20;
            this.cbXml.Text = "XML";
            this.cbXml.UseVisualStyleBackColor = true;
            // 
            // cbJson
            // 
            this.cbJson.AutoSize = true;
            this.cbJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbJson.Location = new System.Drawing.Point(545, 363);
            this.cbJson.Name = "cbJson";
            this.cbJson.Size = new System.Drawing.Size(104, 18);
            this.cbJson.TabIndex = 18;
            this.cbJson.Text = "JSON";
            this.cbJson.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(545, 30);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(104, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Number of records:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRecordCount
            // 
            this.txtRecordCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecordCount.Location = new System.Drawing.Point(655, 33);
            this.txtRecordCount.Name = "txtRecordCount";
            this.txtRecordCount.Size = new System.Drawing.Size(140, 20);
            this.txtRecordCount.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(801, 30);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(104, 24);
            this.label3.TabIndex = 21;
            this.label3.Text = "Time taken:";
            // 
            // txtTimeTaken
            // 
            this.txtTimeTaken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTimeTaken.Location = new System.Drawing.Point(911, 33);
            this.txtTimeTaken.Name = "txtTimeTaken";
            this.txtTimeTaken.Size = new System.Drawing.Size(142, 20);
            this.txtTimeTaken.TabIndex = 22;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(545, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 24);
            this.button3.TabIndex = 23;
            this.button3.Text = "Check all fields";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 574);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox cboConnector;
        private Label label1;
        private TreeView treeView1;
        private Button button1;
        private TreeView treeView2;
        private TextBox textBox1;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataGridView1;
        private TextBox textBox2;
        private Label label2;
        private TextBox txtRecordCount;
        private TextBox textBox3;
        private Label label6;
        private Label label7;
        private Label label8;
        private CheckBox cbJson;
        private CheckBox cbCsv;
        private CheckBox cbXml;
        private Label label3;
        private TextBox txtTimeTaken;
        private Button button3;
    }
}

